using ERMS.API.Models;
using System.ComponentModel.DataAnnotations;

namespace ERMS.API.DTOs
{
  public class CreateDepartmentDto
  {
    [Required]
    public string Name { get; set; } = string.Empty;
    public string? Head { get; set; }
  }
}
