using ERMS.API.Data;
using ERMS.API.Models;
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
  public async Task<ActionResult<Employee>> CreateEmployee(Employee employee)
  {
    _context.Employees.Add(employee);

    await _context.SaveChangesAsync();

    return CreatedAtAction(
        nameof(GetEmployee),
        new { id = employee.Id },
        employee
    );
  }

  // PUT: api/employees/{id}
  // Updates an employee only if they are active
  [HttpPut("{id}")]
  public async Task<IActionResult> UpdateEmployee(int id, Employee employee)
  {
    if (id != employee.Id)
    {
      return BadRequest();
    }

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
