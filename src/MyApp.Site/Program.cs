using System;
using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using MyApp.Domain;
using MyApp.Site.Infrastructure;
using Serilog;

namespace MyApp.Site
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
               .Build();

            Serilog.Debugging.SelfLog.Enable(Console.Error);

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .Enrich.FromLogContext()
                .CreateLogger();

            try
            {
                WebHost.CreateDefaultBuilder(args)
                    .UseConfiguration(configuration)
                    .UseSerilog()
                    .UseStartup<Startup>()
                    .Build()
                    .CreateDatabase<MyAppContext>((context, services) =>
                    {
                        context.Database.EnsureDeleted();
                        context.Database.EnsureCreated();
                    })
                    .Run();
            }
            catch (Exception exc)
            {
                Log.Fatal(exc, "Application start-up failed");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}
