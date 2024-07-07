using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Sinks.Email;
using Serilog.Events;
using System;
using BrainstormSessions;

public class Program
{
    public static void Main(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        var emailConnectionInfo = new EmailConnectionInfo
        {
            FromEmail = configuration["Serilog:WriteTo:0:Args:connectionInfo:FromEmail"],
            ToEmail = configuration["Serilog:WriteTo:0:Args:connectionInfo:ToEmail"],
            MailServer = configuration["Serilog:WriteTo:0:Args:connectionInfo:MailServer"],
            EnableSsl = bool.Parse(configuration["Serilog:WriteTo:0:Args:connectionInfo:EnableSsl"]),
            Port = int.Parse(configuration["Serilog:WriteTo:0:Args:connectionInfo:Port"]),
            EmailSubject = configuration["Serilog:WriteTo:0:Args:connectionInfo:EmailSubject"],
            NetworkCredentials = new System.Net.NetworkCredential(configuration["Serilog:WriteTo:0:Args:connectionInfo:NetworkCredentials:UserName"], configuration["Serilog:WriteTo:0:Args:connectionInfo:NetworkCredentials:Password"])
        };

        Log.Logger = new LoggerConfiguration()
            .WriteTo.Email(
                emailConnectionInfo,
                restrictedToMinimumLevel: LogEventLevel.Warning)
            .CreateLogger();

        try
        {
            Log.Information("Starting web host");
            CreateHostBuilder(args).Build().Run();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Host terminated unexpectedly");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .UseSerilog() // Add this line to use Serilog
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}
