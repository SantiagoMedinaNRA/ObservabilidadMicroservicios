using Microsoft.EntityFrameworkCore;
using Products.Models;

namespace Products.Storage
{
  public class ProductDbContext : DbContext
  {
    public DbSet<Product> Products { get; set; }

    public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      // Seed initial data
      modelBuilder.Entity<Product>().HasData(
          new Product { Id = 1, Name = "Laptop", Price = 1000.00m },
          new Product { Id = 2, Name = "Mouse", Price = 20.00m }
      );
    }
  }
}