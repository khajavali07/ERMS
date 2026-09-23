using System.ComponentModel.DataAnnotations;

namespace ERMS.API.Models;

public class User
{
  public int Id { get; set; }

  [Required]
  public string Username { get; set; } = string.Empty;

  [Required]
  public string PasswordHash { get; set; } = string.Empty;

  public int? EmployeeId { get; set; }

  public int RoleId { get; set; }

  public bool IsActive { get; set; } = true;
}
