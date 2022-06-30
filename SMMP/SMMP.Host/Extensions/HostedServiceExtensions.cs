using System;
using Microsoft.Extensions.DependencyInjection;
using SMMP.Core.Interfaces.HostedService;
using SMMP.Host.HostedService;

namespace SMMP.Host.Extensions
{
    public static class HostedServiceExtensions
    {
        public static void AddHostedService(this IServiceCollection services)
        {
            services.AddHostedService<QueuedHostedService>();
            services.AddSingleton<IBackgroundTaskQueue, BackgroundTaskQueue>();
        }
    }
}
