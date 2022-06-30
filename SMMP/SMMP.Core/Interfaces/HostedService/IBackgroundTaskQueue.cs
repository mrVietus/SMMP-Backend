using System;
using System.Threading;
using System.Threading.Tasks;
using SMMP.Core.Mediator;

namespace SMMP.Core.Interfaces.HostedService
{
    public interface IBackgroundTaskQueue
    {
        Task QueueBackgroundWorkItem(AsyncCommandBase command);
        Task<AsyncCommandBase> DequeueAsync(CancellationToken cancellationToken);
    }
}
