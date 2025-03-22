using Customers.Brokers;
using Customers.Services;
using Customers.Storage;
using Microsoft.EntityFrameworkCore;

namespace Customers
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var builder = WebApplication.CreateBuilder(args);

      // Add services to the container.

      builder.Services.AddControllers();

      // Register DbContext with in-memory database
      builder.Services.AddDbContext<CustomerDbContext>(options =>
          options.UseInMemoryDatabase("CustomerDb"));

      // Register broker and service layers
      builder.Services.AddScoped<IStorageBroker, StorageBroker>();
      builder.Services.AddTransient<ICustomerService, CustomerService>();

      var app = builder.Build();

      // Seed initial data
      using (var scope = app.Services.CreateScope())
      {
        var dbContext = scope.ServiceProvider.GetRequiredService<CustomerDbContext>();
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