using System.ComponentModel.DataAnnotations;
using ERMS.API.Models;

namespace ERMS.API.DTOs
{
  public class UpdateDepartmentDto
  {
    [Required]
    public string Name { get; set; } = string.Empty;
    public string? Head { get; set; }
  }
}
