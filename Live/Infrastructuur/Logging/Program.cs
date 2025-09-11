using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Logging;

internal class Program
{
    static void Main(string[] args)
    {
        var builder = new ConfigurationBuilder();
        builder.SetBasePath(Environment.CurrentDirectory);
        builder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        var config2 = builder.Build();
      
        var factory = LoggerFactory.Create(config => {
            // In code:
            //config.AddFilter((cat, lvl) =>
            //{
            //    return cat == "MyCat" && lvl >= LogLevel.Trace;
            //});
            // In config:
            config.AddConfiguration(config2.GetSection("Blaat"));

            //config.ClearProviders();
            // From package: Microsoft.Extensions.Logging.Console
            config.AddConsole();
            config.AddDebug();
            config.AddEventLog();
        });

        ILogger logger = factory.CreateLogger("MyCat");

        //logger.BeginScope()
        logger.LogCritical("Kritiek");
        logger.LogError("Fout!");
        logger.LogWarning("Waarschuwing");
        logger.LogInformation("Info");
        logger.LogDebug("Debugger");
        logger.LogTrace("Trees");

        Debug.WriteLine("In debug mode");

        ILogger<MyClass> logger2 = factory.CreateLogger<MyClass>();
        var c = new MyClass(logger2);

        c.DoeIets();
        Console.ReadLine();
    }
}
