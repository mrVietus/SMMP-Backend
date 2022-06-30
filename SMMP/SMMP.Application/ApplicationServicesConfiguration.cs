using System;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SMMP.Application.Configuration.PipelineBehavior;
using SMMP.Application.ParallelCommand;
using SMMP.Application.Services.Implementation;
using SMMP.Application.Services.Interfaces;
using SMMP.Core.Interfaces.ParallelCommand;

namespace SMMP.Application
{
    public static class ApplicationServicesConfiguration
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatrServices();

            services.AddSingleton<ILoggingService, LoggingService>();
        }

        public static void AddMediatrServices(this IServiceCollection services)
        {
            services.AddMediatR(typeof(ApplicationServicesConfiguration).Assembly);
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(GenericPipelineBehavior<,>));
            services.AddScoped<IParallelCommandService, ParallelCommandService>();
        }
    }
}
