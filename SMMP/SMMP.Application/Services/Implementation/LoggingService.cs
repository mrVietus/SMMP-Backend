using System;
using Serilog.Core;
using Serilog.Events;
using SMMP.Application.Services.Interfaces;
using SMMP.Core.Models.Enums;

namespace SMMP.Application.Services.Implementation
{
    public class LoggingService : ILoggingService
    {
        private LoggingLevelSwitch _loggingLevelSwitch;
        private LoggingLevel _defaultLogLevel;

        public void SetLoggingLeveSwitch(LoggingLevelSwitch loggingLevelSwitch)
        {
            _loggingLevelSwitch = loggingLevelSwitch;
        }

        public LoggingLevel GetDefaultLoggingLevel()
        {
            return _defaultLogLevel;
        }

        public void SetLoggingLevel(LoggingLevel logLevel)
        {
            switch (logLevel)
            {
                case LoggingLevel.Verbose:
                    _loggingLevelSwitch.MinimumLevel = LogEventLevel.Verbose;
                    break;
                case LoggingLevel.Debug:
                    _loggingLevelSwitch.MinimumLevel = LogEventLevel.Debug;
                    break;
                case LoggingLevel.Information:
                    _loggingLevelSwitch.MinimumLevel = LogEventLevel.Information;
                    break;
                case LoggingLevel.Warning:
                    _loggingLevelSwitch.MinimumLevel = LogEventLevel.Warning;
                    break;
                case LoggingLevel.Error:
                    _loggingLevelSwitch.MinimumLevel = LogEventLevel.Error;
                    break;
                case LoggingLevel.Fatal:
                    _loggingLevelSwitch.MinimumLevel = LogEventLevel.Fatal;
                    break;
                default:
                    // If unknown value detected then change it to Error for being safe
                    _loggingLevelSwitch.MinimumLevel = LogEventLevel.Error;
                    break;
            }
        }

        public void SetDefaultLoggingLevel(string logLevel)
        {
            switch (logLevel)
            {
                case "Verbose":
                    _loggingLevelSwitch.MinimumLevel = LogEventLevel.Verbose;
                    _defaultLogLevel = LoggingLevel.Verbose;
                    break;
                case "Debug":
                    _loggingLevelSwitch.MinimumLevel = LogEventLevel.Debug;
                    _defaultLogLevel = LoggingLevel.Debug;
                    break;
                case "Information":
                    _loggingLevelSwitch.MinimumLevel = LogEventLevel.Information;
                    _defaultLogLevel = LoggingLevel.Information;
                    break;
                case "Warning":
                    _loggingLevelSwitch.MinimumLevel = LogEventLevel.Warning;
                    _defaultLogLevel = LoggingLevel.Warning;
                    break;
                case "Error":
                    _loggingLevelSwitch.MinimumLevel = LogEventLevel.Error;
                    _defaultLogLevel = LoggingLevel.Error;
                    break;
                case "Fatal":
                    _loggingLevelSwitch.MinimumLevel = LogEventLevel.Fatal;
                    _defaultLogLevel = LoggingLevel.Fatal;
                    break;
                default:
                    // If unknown value detected then change it to Error for being safe
                    _loggingLevelSwitch.MinimumLevel = LogEventLevel.Error;
                    _defaultLogLevel = LoggingLevel.Error;
                    break;
            }
        }
    }
}
