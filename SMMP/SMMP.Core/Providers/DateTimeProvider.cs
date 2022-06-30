using System;
using SMMP.Core.Interfaces.Providers;

namespace SMMP.Core.Providers
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime GetCurrentUtc()
        {
            return DateTime.UtcNow;
        }
    }
}
