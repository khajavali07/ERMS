using ERMS.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ERMS.API.Data;

public class ApplicationDbContext : DbContext
{
  public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
  {
  }

  public DbSet<Employee> Employees { get; set; }
  public DbSet<Department> Departments { get; set; }
  public DbSet<Role> Roles { get; set; }
  public DbSet<User> Users { get; set; }
  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);
    modelBuilder.Entity<Role>().HasData(
       new Role { Id = 1, Name = "Admin" },
       new Role { Id = 2, Name = "HR" },
       new Role { Id = 3, Name = "Manager" },
       new Role { Id = 4, Name = "Employee" }
    );
    modelBuilder.Entity<User>()
      .HasOne<Role>()
      .WithMany()
      .HasForeignKey(u => u.RoleId)
      .OnDelete(DeleteBehavior.Restrict);
    modelBuilder.Entity<User>()
       .HasOne<Employee>()
       .WithMany()
       .HasForeignKey(u => u.EmployeeId)
       .OnDelete(DeleteBehavior.Restrict);

    modelBuilder.Entity<User>().HasData(
      new User
      {
        Id =1,
        Username = "admin",
        PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin@123"),
        EmployeeId=null,
        RoleId=1,
        IsActive= true
      }
    );
  }
}
