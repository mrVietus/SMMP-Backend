using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog.Core;
using SMMP.Application.Services.Interfaces;

namespace SMMP.Host.Extensions
{
    public static class LoggerConfigurationExtensions
    {
        public static IHost SetLoggingService(this IHost host, LoggingLevelSwitch loggingLevelSwitch)
        {
            using IServiceScope scope = host.Services.CreateScope();
            var loggingService = scope.ServiceProvider.GetRequiredService<ILoggingService>();
            var loggingSettings = scope.ServiceProvider.GetRequiredService<Core.Models.Settings.SerilogSettings.Serilog>();

            loggingService.SetLoggingLeveSwitch(loggingLevelSwitch);
            loggingService.SetDefaultLoggingLevel(loggingSettings.MinimumLevel.Default);

            return host;
        }
    }
}
