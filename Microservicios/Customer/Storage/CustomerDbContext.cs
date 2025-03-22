using Customers.Models;
using Microsoft.EntityFrameworkCore;

namespace Customers.Storage
{
  public class CustomerDbContext : DbContext
  {
    public DbSet<Customer> Customers { get; set; }

    public CustomerDbContext(DbContextOptions<CustomerDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      // Seed initial data
      modelBuilder.Entity<Customer>().HasData(
          new Customer { Id = 1, Name = "John Doe", Email = "john@example.com", Address = "123 Main St" },
          new Customer { Id = 2, Name = "Jane Smith", Email = "jane@example.com", Address = "456 Elm St" }
      );
    }
  }
}