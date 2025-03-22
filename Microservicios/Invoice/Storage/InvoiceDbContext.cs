using Invoices.Models;
using Microsoft.EntityFrameworkCore;

namespace Invoices.Storage
{
  public class InvoiceDbContext : DbContext
  {
    public DbSet<Invoice> Invoices { get; set; }

    public InvoiceDbContext(DbContextOptions<InvoiceDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      // Seed initial data
      modelBuilder.Entity<Invoice>().HasData(
          new Invoice
          {
            Id = 1,
            CustomerId = 1,
            TotalAmount = 100.00m,
            Status = "Pending",
            CreatedAt = DateTime.UtcNow
          },
          new Invoice
          {
            Id = 2,
            CustomerId = 2,
            TotalAmount = 200.00m,
            Status = "Paid",
            CreatedAt = DateTime.UtcNow
          }
      );
    }
  }
}