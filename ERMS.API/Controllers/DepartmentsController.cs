using ERMS.API.Models;
using ERMS.API.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ERMS.API.Data;

namespace ERMS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DepartmentsController : ControllerBase
{
  private readonly ApplicationDbContext _context;

  public DepartmentsController(ApplicationDbContext context)
  {
    _context = context;
  }

  // GET: api/departments
  [HttpGet]
  public async Task<ActionResult<IEnumerable<Department>>> GetDepartments()
  {
    var departments = await _context.Departments.ToListAsync();
    return Ok(departments);
  }

  [HttpGet("{id}")]
  public async Task<ActionResult<Department>> GetDepartment(int id)
  {
    var department = await _context.Departments.FirstOrDefaultAsync(d => d.Id == id);

    if (department == null)
    {
      return NotFound();
    }

    return Ok(department);
  }

  // POST: api/departments
  [HttpPost]
  public async Task<ActionResult<Department>> CreateDepartment(
      CreateDepartmentDto department)
  {
    var newDepartment = new Department
    {
      Name = department.Name,
      Head = department.Head,
      EmployeeCount = 0
    };

    _context.Departments.Add(newDepartment);

    await _context.SaveChangesAsync();

    return CreatedAtAction(
        nameof(GetDepartment),
        new { id = newDepartment.Id },
        newDepartment
    );
  }

  // PUT: api/departments/{id}
  [HttpPut("{id}")]
  public async Task<IActionResult> UpdateDepartment(
      int id,
      UpdateDepartmentDto department)
  {
    var existingDepartment = await _context.Departments
        .FirstOrDefaultAsync(d => d.Id == id);

    if (existingDepartment == null)
    {
      return NotFound();
    }

    existingDepartment.Name = department.Name;
    existingDepartment.Head = department.Head;

    await _context.SaveChangesAsync();

    return NoContent();
  }

  // DELETE: api/departments/{id}
  [HttpDelete("{id}")]
  public async Task<IActionResult> DeleteDepartment(int id)
  {
    var department = await _context.Departments
        .FirstOrDefaultAsync(d => d.Id == id);

    if (department == null)
    {
      return NotFound();
    }

    _context.Departments.Remove(department);

    await _context.SaveChangesAsync();

    return NoContent();
  }
}
