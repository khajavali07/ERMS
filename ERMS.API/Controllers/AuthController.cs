using ERMS.API.Data;
using ERMS.API.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BCrypt.Net;

namespace ERMS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
  private readonly ApplicationDbContext _context;
  private readonly IConfiguration _configuration;

  public AuthController(
      ApplicationDbContext context,
      IConfiguration configuration)
  {
    _context = context;
    _configuration = configuration;
  }

  [HttpPost("login")]
  public async Task<ActionResult<LoginResponseDto>> Login(
      LoginRequestDto request)
  {
    // 1. Find the user
    var user = await _context.Users
        .FirstOrDefaultAsync(u =>
            u.Username == request.Username &&
            u.IsActive);

    if (user == null)
    {
      return Unauthorized("Invalid username or password.");
    }

    // 2. Verify password
    bool passwordValid = BCrypt.Net.BCrypt.Verify(
        request.Password,
        user.PasswordHash);

    if (!passwordValid)
    {
      return Unauthorized("Invalid username or password.");
    }

    // 3. Get user's role
    var role = await _context.Roles
        .FirstOrDefaultAsync(r => r.Id == user.RoleId);

    if (role == null)
    {
      return Unauthorized("User role not found.");
    }

    // 4. Create JWT token
    var token = GenerateJwtToken(
        user.Id,
        user.Username,
        role.Name,
        user.EmployeeId);

    // 5. Return login response
    var response = new LoginResponseDto
    {
      Token = token,
      UserId = user.Id,
      Username = user.Username,
      Role = role.Name,
      EmployeeId = user.EmployeeId
    };

    return Ok(response);
  }

  private string GenerateJwtToken(
      int userId,
      string username,
      string role,
      int? employeeId)
  {
    var key = _configuration["Jwt:Key"]!;

    var claims = new List<Claim>
        {
            new Claim(
                ClaimTypes.NameIdentifier,
                userId.ToString()),

            new Claim(
                ClaimTypes.Name,
                username),

            new Claim(
                ClaimTypes.Role,
                role)
        };

    if (employeeId.HasValue)
    {
      claims.Add(
          new Claim(
              "EmployeeId",
              employeeId.Value.ToString()));
    }

    var securityKey = new SymmetricSecurityKey(
        Encoding.UTF8.GetBytes(key));

    var credentials = new SigningCredentials(
        securityKey,
        SecurityAlgorithms.HmacSha256);

    var expiryMinutes =
        int.Parse(_configuration["Jwt:ExpiryMinutes"]!);

    var token = new JwtSecurityToken(
        issuer: _configuration["Jwt:Issuer"],
        audience: _configuration["Jwt:Audience"],
        claims: claims,
        expires: DateTime.UtcNow.AddMinutes(expiryMinutes),
        signingCredentials: credentials);

    return new JwtSecurityTokenHandler()
        .WriteToken(token);
  }
}
