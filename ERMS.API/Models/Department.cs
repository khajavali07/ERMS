using System.ComponentModel.DataAnnotations;

namespace ERMS.API.Models;

public class Department
{
  public int Id { get; set; }

  [Required]
  public string Name { get; set; } = string.Empty;

  public string? Head { get; set; }
  public int EmployeeCount { get; set; }
}
