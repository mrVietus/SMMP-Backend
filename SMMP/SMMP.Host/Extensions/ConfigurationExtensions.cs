using System;
using Microsoft.Extensions.Configuration;

namespace SMMP.Host.Extensions
{
    public static class ConfigurationExtensions
    {
        public static TSettings GetSettings<TSettings>(this IConfiguration configuration)
            where TSettings : new()
        {
            var settings = new TSettings();
            var settingsKey = typeof(TSettings).Name;
            configuration.Bind(settingsKey, settings);

            return settings;
        }

        public static TSettings GetSettings<TSettings>(this IConfiguration configuration, Action<IConfiguration, TSettings> map)
            where TSettings : new()
        {
            var settings = new TSettings();
            var settingsKey = typeof(TSettings).Name;
            configuration.Bind(settingsKey, settings);

            map(configuration, settings);

            return settings;
        }

        public static TSettings GetSettings<TSettings>(this IConfiguration configuration, IServiceProvider serviceProvider, Action<IConfiguration, IServiceProvider, TSettings> map)
          where TSettings : new()
        {
            var settings = new TSettings();
            var settingsKey = typeof(TSettings).Name;
            configuration.Bind(settingsKey, settings);

            map(configuration, serviceProvider, settings);

            return settings;
        }
    }
}
