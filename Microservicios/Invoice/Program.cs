using Invoices.Brokers;
using Invoices.Storage;
using InvoiceS.Services;
using Microsoft.EntityFrameworkCore;

namespace Invoices
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var builder = WebApplication.CreateBuilder(args);

      // Add services to the container.
      builder.Services.AddControllers();

      // Register DbContext with in-memory database
      builder.Services.AddDbContext<InvoiceDbContext>(options =>
        options.UseInMemoryDatabase("InvoiceDb"));

      // Register broker and service layers
      builder.Services.AddScoped<IStorageBroker, StorageBroker>();
      builder.Services.AddTransient<IInvoiceService, InvoiceService>();

      // Register HTTP clients for inter-service communication
      builder.Services.AddHttpClient<ICustomerBroker, CustomerBroker>(client =>
      {
        client.BaseAddress = new Uri("https://localhost:44303"); // Customer Microservice URL
      });

      builder.Services.AddHttpClient<IProductBroker, ProductBroker>(client =>
      {
        client.BaseAddress = new Uri("https://localhost:44329"); // Product Microservice URL
      });

      var app = builder.Build();

      // Seed initial data
      using (var scope = app.Services.CreateScope())
      {
        var dbContext = scope.ServiceProvider.GetRequiredService<InvoiceDbContext>();
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