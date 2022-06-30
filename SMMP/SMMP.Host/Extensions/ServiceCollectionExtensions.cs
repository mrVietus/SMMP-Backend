using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace SMMP.Host.Extensions
{
    public static class ServiceCollectionExtensions
    {
        private const string DefaultVersion = "1.0";
        private const string ApiTitle = "Social Media Management Api";

        public static void AddSettings<TSettings>(this IServiceCollection services)
            where TSettings : class, new()
        {
            services.AddSingleton(provider =>
            {
                var configuration = provider.GetService<IConfiguration>();
                var settings = configuration.GetSettings<TSettings>();

                return settings;
            });
        }

        public static void AddSettings<TSettings>(this IServiceCollection services, Action<IConfiguration, TSettings> map)
            where TSettings : class, new()
        {
            services.AddSingleton(provider =>
            {
                var configuration = provider.GetService<IConfiguration>();
                var settings = configuration.GetSettings<TSettings>(map);

                return settings;
            });
        }

        public static void AddScopedSettings<TSettings>(this IServiceCollection services, Action<IConfiguration, IServiceProvider, TSettings> map)
           where TSettings : class, new()
        {
            services.AddScoped(provider =>
            {
                var configuration = provider.GetService<IConfiguration>();
                var settings = configuration.GetSettings(provider, map);
                return settings;
            });
        }

        public static void AddSwagger(this IServiceCollection services)
        {
            //string authority = $"{authOptions.Instance}/{authOptions.TenantDomain}/{authOptions.SignInPolicyDefault}/oauth2/v2.0";

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc($"v{DefaultVersion}", new OpenApiInfo { Title = ApiTitle, Version = $"v{DefaultVersion}" });
                //c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                //{
                //    Type = SecuritySchemeType.OAuth2,
                //    Flows = new OpenApiOAuthFlows
                //    {
                //        Implicit = new OpenApiOAuthFlow
                //        {
                //            AuthorizationUrl = new Uri($"{authority}/authorize"),
                //            Scopes = new Dictionary<string, string>
                //            {
                //                { authOptions.ClientId, "Client Id" }
                //            }
                //        }
                //    },
                //    In = ParameterLocation.Query
                //});

                //c.AddSecurityRequirement(new OpenApiSecurityRequirement
                //{
                //    {
                //        new OpenApiSecurityScheme
                //        {
                //            Reference = new OpenApiReference
                //            {
                //                Type = ReferenceType.SecurityScheme,
                //                Id = "oauth2"
                //            },
                //            Scheme = "oauth2",
                //            Name = "Bearer",
                //            In = ParameterLocation.Header
                //        },
                //        new List<string>()
                //    }
                //});
            });
        }

        public static void UseSwagger(this IApplicationBuilder app,  string uiVersion)
        {
            app.UseSwagger().UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/v{DefaultVersion}/swagger.json", $"{ApiTitle} v{DefaultVersion}");
                c.DocExpansion(DocExpansion.None);
                //c.OAuthClientId(authOptions.ClientId);
                //c.OAuthScopeSeparator(" ");
                //c.OAuthAdditionalQueryStringParams(new Dictionary<string, string> { { "ui-version", uiVersion } });
            });
        }
    }
}
