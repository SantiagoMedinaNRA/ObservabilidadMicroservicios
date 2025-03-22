using Microsoft.EntityFrameworkCore;
using Products.Brokers;
using Products.Services;
using Products.Storage;

namespace Products
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var builder = WebApplication.CreateBuilder(args);

      // Add services to the container.
      builder.Services.AddControllers();

      // Register DbContext with in-memory database
      builder.Services.AddDbContext<ProductDbContext>(options =>
          options.UseInMemoryDatabase("ProductDb"));

      // Register broker and service layers
      builder.Services.AddScoped<IStorageBroker, StorageBroker>();
      builder.Services.AddTransient<IProductService, ProductService>();

      var app = builder.Build();

      // Seed initial data
      using (var scope = app.Services.CreateScope())
      {
        var dbContext = scope.ServiceProvider.GetRequiredService<ProductDbContext>();
        dbContext.Database.EnsureCreated(); // Ensure the database is created
      }

      // Configure the HTTP request pipeline.
      if (app.Environment.IsDevelopment())
      {
        app.MapOpenApi();
      }

      app.UseHttpsRedirection();

      app.UseAuthorization();

      app.MapControllers();

      app.Run();
    }
  }
}