using System;
using Serilog.Core;
using SMMP.Core.Models.Enums;

namespace SMMP.Application.Services.Interfaces
{
    public interface ILoggingService
    {
        LoggingLevel GetDefaultLoggingLevel();
        void SetLoggingLevel(LoggingLevel logLevel);
        void SetDefaultLoggingLevel(string logLevel);
        void SetLoggingLeveSwitch(LoggingLevelSwitch loggingLevelSwitch);
    }
}
