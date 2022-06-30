using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using SMMP.Core.Interfaces.HostedService;
using SMMP.Core.Mediator;

namespace SMMP.Host.HostedService
{
    public class BackgroundTaskQueue : IBackgroundTaskQueue
    {
        private readonly ConcurrentQueue<AsyncCommandBase> _workItems = new ConcurrentQueue<AsyncCommandBase>();
        private readonly SemaphoreSlim _signal = new SemaphoreSlim(0);

        public Task QueueBackgroundWorkItem(AsyncCommandBase command)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            _workItems.Enqueue(command);
            _signal.Release();
            return Task.CompletedTask;
        }

        public async Task<AsyncCommandBase> DequeueAsync(CancellationToken cancellationToken)
        {
            await _signal.WaitAsync(cancellationToken);
            _workItems.TryDequeue(out var workItem);

            return workItem;
        }
    }
}
