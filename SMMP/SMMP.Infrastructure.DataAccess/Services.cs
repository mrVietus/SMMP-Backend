using System;
using Microsoft.Extensions.DependencyInjection;
using SMMP.Core.Interfaces.Repositories;
using SMMP.Infrastructure.DataAccess.Repositories;

namespace SMMP.Infrastructure.DataAccess
{
    public static class Services
    {
        public static void AddDataAccessServices(this IServiceCollection services)
        {
            services.AddScoped<IExecutionEfRepository, ExecutionEfRepository>();
            services.AddScoped<IUserEfRepository, UserEfRepository>();
            services.AddScoped<IUserProfileEfRepository, UserProfileEfRepository>();
        }
    }
}
