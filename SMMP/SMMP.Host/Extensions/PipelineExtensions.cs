using System;
using Microsoft.Extensions.DependencyInjection;
using SMMP.Core.Interfaces.PipelineServices;
using SMMP.Host.PipelineServices;

namespace SMMP.Host.Extensions
{
    public static class PipelineExtensions
    {
        public static void AddPipelineServices(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddScoped<ICorrelationIdService, HttpContextCorrelationIdService>();
        }
    }
}
