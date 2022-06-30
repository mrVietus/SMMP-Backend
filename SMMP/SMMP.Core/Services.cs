using System;
using Microsoft.Extensions.DependencyInjection;
using SMMP.Core.Interfaces.Providers;
using SMMP.Core.Providers;

namespace SMMP.Core
{
    public static class Services
    {
        public static void AddCoreServices(this IServiceCollection services)
        {
            services.AddScoped<IDateTimeProvider, DateTimeProvider>();
        }
    }
}
