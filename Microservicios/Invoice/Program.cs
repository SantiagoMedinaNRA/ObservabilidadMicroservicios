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
      try
      {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddControllers();

        builder.Services.AddDbContext<InvoiceDbContext>(options =>
            options.UseInMemoryDatabase("InvoiceDb"));

        builder.Services.AddScoped<IStorageBroker, StorageBroker>();
        builder.Services.AddTransient<IInvoiceService, InvoiceService>();

        var app = builder.Build();

        using (var scope = app.Services.CreateScope())
        {
          var dbContext = scope.ServiceProvider.GetRequiredService<InvoiceDbContext>();
          dbContext.Database.EnsureCreated();
        }

        if (app.Environment.IsDevelopment())
        {
          app.MapOpenApi();
        }

        if (!app.Environment.IsProduction())
        {
          app.UseHttpsRedirection();
        }

        app.UseAuthorization();
        app.MapControllers();

        app.Run();
      }
      catch (Exception ex)
      {
        Console.WriteLine("Fatal error:");
        Console.WriteLine(ex.ToString());
        throw;
      }
    }

  }
}