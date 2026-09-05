using ERMS.API.Data;
using ERMS.API.Models;
using ERMS.API.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ERMS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController : ControllerBase
{
  private readonly ApplicationDbContext _context;

  public EmployeesController(ApplicationDbContext context)
  {
    _context = context;
  }

  // GET: api/employees
  // Returns only active employees
  [HttpGet]
  public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
  {
    var employees = await _context.Employees
        .Where(e => e.IsActive)
        .ToListAsync();

    return Ok(employees);
  }

  // GET: api/employees/{id}
  // Returns an employee only if they are active
  [HttpGet("{id}")]
  public async Task<ActionResult<Employee>> GetEmployee(int id)
  {
    var employee = await _context.Employees
        .FirstOrDefaultAsync(e => e.Id == id && e.IsActive);

    if (employee == null)
    {
      return NotFound();
    }

    return Ok(employee);
  }

  // POST: api/employees
  // Creates a new employee
  [HttpPost]
  public async Task<ActionResult<Employee>> CreateEmployee( CreateEmployeeDto employee)
  {
    var newEmployee = new Employee
    {
      EmployeeCode = employee.EmployeeCode,
      FirstName = employee.FirstName,
      LastName = employee.LastName,
      DateOfBirth = employee.DateOfBirth,
      Gender = employee.Gender,
      MaritalStatus = employee.MaritalStatus,
      BloodGroup = employee.BloodGroup,
      ProfilePhoto = employee.ProfilePhoto,

      Email = employee.Email,
      Phone = employee.Phone,
      EmergencyContact = employee.EmergencyContact,
      Address = employee.Address,
      City = employee.City,
      State = employee.State,
      Pincode = employee.Pincode,

      DepartmentId = employee.DepartmentId,
      Designation = employee.Designation,
      ManagerId = employee.ManagerId,
      JoiningDate = employee.JoiningDate,
      EmploymentType = employee.EmploymentType,
      Salary = employee.Salary,
      Shift = employee.Shift,
      WorkLocation = employee.WorkLocation,

      IsActive = true
    };

    _context.Employees.Add(newEmployee);

    await _context.SaveChangesAsync();

    return CreatedAtAction(
        nameof(GetEmployee),
        new { id = newEmployee.Id },
        newEmployee
    );
  }

  // PUT: api/employees/{id}
  // Updates an employee only if they are active
  [HttpPut("{id}")]
  public async Task<IActionResult> UpdateEmployee(
      int id,
      UpdateEmployeeDto employee)
  {
    var existingEmployee = await _context.Employees
        .FirstOrDefaultAsync(e => e.Id == id && e.IsActive);

    if (existingEmployee == null)
    {
      return NotFound();
    }

    existingEmployee.EmployeeCode = employee.EmployeeCode;
    existingEmployee.FirstName = employee.FirstName;
    existingEmployee.LastName = employee.LastName;
    existingEmployee.DateOfBirth = employee.DateOfBirth;
    existingEmployee.Gender = employee.Gender;
    existingEmployee.MaritalStatus = employee.MaritalStatus;
    existingEmployee.BloodGroup = employee.BloodGroup;
    existingEmployee.ProfilePhoto = employee.ProfilePhoto;

    existingEmployee.Email = employee.Email;
    existingEmployee.Phone = employee.Phone;
    existingEmployee.EmergencyContact = employee.EmergencyContact;
    existingEmployee.Address = employee.Address;
    existingEmployee.City = employee.City;
    existingEmployee.State = employee.State;
    existingEmployee.Pincode = employee.Pincode;

    existingEmployee.DepartmentId = employee.DepartmentId;
    existingEmployee.Designation = employee.Designation;
    existingEmployee.ManagerId = employee.ManagerId;
    existingEmployee.JoiningDate = employee.JoiningDate;
    existingEmployee.EmploymentType = employee.EmploymentType;
    existingEmployee.Salary = employee.Salary;
    existingEmployee.Shift = employee.Shift;
    existingEmployee.WorkLocation = employee.WorkLocation;

    await _context.SaveChangesAsync();

    return NoContent();
  }

  // DELETE: api/employees/{id}
  // Soft delete: marks employee as inactive
  [HttpDelete("{id}")]
  public async Task<IActionResult> DeleteEmployee(int id)
  {
    var employee = await _context.Employees
        .FirstOrDefaultAsync(e => e.Id == id && e.IsActive);

    if (employee == null)
    {
      return NotFound();
    }

    employee.IsActive = false;

    await _context.SaveChangesAsync();

    return NoContent();
  }
}
