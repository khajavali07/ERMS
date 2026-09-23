using System.ComponentModel.DataAnnotations;
namespace ERMS.API.Models;

public class Role
{
  public int Id { get; set; }

  [Required]
  public string Name { get; set; } = string.Empty;
}
