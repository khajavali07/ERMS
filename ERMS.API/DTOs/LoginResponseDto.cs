using System.ComponentModel.DataAnnotations;
namespace ERMS.API.DTOs;
public class LoginResponseDto
{
  public string Token { get; set; } = string.Empty;
  public int UserId { get; set; } = 0;
  public string Username { get; set; } = string.Empty;
  public string Role { get; set; } = string.Empty;
  public int? EmployeeId { get; set; }
}
