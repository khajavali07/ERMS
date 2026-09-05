using System.ComponentModel.DataAnnotations;
namespace ERMS.API.Models;

public class Employee
{

  public int Id { get; set; }

  // Employee identification
  [Required]
  public string EmployeeCode { get; set; } = string.Empty;

  // Personal details
  [Required]
  public string FirstName { get; set; } = string.Empty;
  [Required]
  public string LastName { get; set; } = string.Empty;
  public DateTime? DateOfBirth { get; set; }
  public string? Gender { get; set; }
  public string? MaritalStatus { get; set; }
  public string? BloodGroup { get; set; }
  public string? ProfilePhoto { get; set; }

  // Contact details
  [Required]
  [EmailAddress]
  public string Email { get; set; } = string.Empty;
  [StringLength(15)]
  public string? Phone { get; set; }
  [StringLength(15)]
  public string? EmergencyContact { get; set; }
  public string? Address { get; set; }
  public string? City { get; set; }
  public string? State { get; set; }
  [StringLength(10)]
  public string? Pincode { get; set; }

  // Professional details
  public int? DepartmentId { get; set; }
  public string? Designation { get; set; }
  public int? ManagerId { get; set; }
  public DateTime? JoiningDate { get; set; }
  public string? EmploymentType { get; set; }
  [Range(0, double.MaxValue, ErrorMessage = "Salary must be a non-negative value.")]
  public decimal? Salary { get; set; }
  public string? Shift { get; set; }
  public string? WorkLocation { get; set; }

  // Employee status
  public bool IsActive { get; set; } = true;
}
