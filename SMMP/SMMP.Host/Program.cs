using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Core;
using SMMP.Host.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMMP.Host
{
    public class Program
    {
        private static readonly LoggingLevelSwitch _loggingLevelSwitch = new LoggingLevelSwitch();
        private static readonly ICollection<Exception> _serilogConfigurationExceptions = new List<Exception>();

        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args)
                .Build()
                .SetLoggingService(_loggingLevelSwitch)
                .MigrateDatabase();

            foreach (var serilogConfigurationException in _serilogConfigurationExceptions)
            {
                Log.Logger.Error(serilogConfigurationException, "There was an error while configuring Serilog.");
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddCommandLine(args);
                    config.AddJsonFile("appsettings.json");
                    config.AddEnvironmentVariables();
                    config.AddUserSecrets(typeof(Program).Assembly);
                })
                .UseSerilog(
                    (hostingContext, loggerConfiguration) =>
                    {
                        try
                        {
                            loggerConfiguration
                                .ReadFrom.Configuration(hostingContext.Configuration)
                                .MinimumLevel.ControlledBy(_loggingLevelSwitch);
                        }
                        catch (Exception e)
                        {
                            _serilogConfigurationExceptions.Add(e);
                        }
                    },
                    writeToProviders: true
                )
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
