using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SMMP.Infrastructure.Database;

namespace SMMP.Host.Extensions
{
    public static class HostExtensions
    {
        public static IHost MigrateDatabase(this IHost host)
        {
            using IServiceScope scope = host.Services.CreateScope();
            using var appContext = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
            appContext.Database.Migrate();

            return host;
        }
    }
}
