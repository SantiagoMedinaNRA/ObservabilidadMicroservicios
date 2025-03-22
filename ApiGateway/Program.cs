using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace ApiGateway
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var builder = WebApplication.CreateBuilder(args);

      // Add Ocelot
      builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
      builder.Services.AddOcelot(builder.Configuration);

      builder.Services.AddLogging(loggingBuilder =>
      {
        loggingBuilder.AddConsole();
        loggingBuilder.AddDebug();
      });

      var app = builder.Build();

      if (app.Environment.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      // Use Ocelot middleware
      app.UseOcelot().Wait();

      app.Run();
    }
  }
}