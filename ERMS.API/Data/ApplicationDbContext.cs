using ERMS.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ERMS.API.Data;

public class ApplicationDbContext : DbContext
{
  public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
  {
  }
  public DbSet<Employee> Employees { get; set; }
}
