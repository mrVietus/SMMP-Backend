using System;

namespace SMMP.Core.Interfaces.Providers
{
    public interface IDateTimeProvider
    {
        DateTime GetCurrentUtc();
    }
}
