using System;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SMMP.Application;
using SMMP.Core;
using SMMP.Core.Models.Settings;
using SMMP.Host.Extensions;
using SMMP.Host.HostedService;
using SMMP.Host.Middlewares;
using SMMP.Infrastructure.DataAccess;
using SMMP.Infrastructure.Database;
using SMMP.WebApi;
using SMMP.WebApi.Controllers;

namespace SMMP.Host
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
               .AddApplicationPart(typeof(WeatherForecastController).Assembly)
               .AddJsonOptions(options =>
               {
                   options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                   options.JsonSerializerOptions.IgnoreNullValues = true;
               });

            services.AddSwagger();
            services.AddApplicationServices();
            services.AddDataAccessServices();
            services.AddDatabaseServices(_configuration);
            services.AddPipelineServices();
            services.AddWebApiServices();
            services.AddHostedService();
            services.AddCoreServices();

            services.AddSettings<Core.Models.Settings.SerilogSettings.Serilog>();
            services.AddSettings<JWTSettings>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<CorrelationMiddleware>();
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseSwagger(_configuration["uiVersion"]);
            app.UseRouting();
            app.UseAuthorization();
            app.UseHttpsRedirection();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
