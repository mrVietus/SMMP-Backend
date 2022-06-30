using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using SMMP.Core.Interfaces.HostedService;
using SMMP.Core.Interfaces.ParallelCommand;
using SMMP.Core.Mediator;

namespace SMMP.Application.Configuration.PipelineBehavior
{
    public class GenericPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<TRequest> _logger;
        private readonly IBackgroundTaskQueue _backgroundTaskQueue;
        private readonly IParallelCommandService _parallelCommandService;

        public GenericPipelineBehavior(
            ILogger<TRequest> logger,
            IBackgroundTaskQueue backgroundTaskQueue,
            IParallelCommandService parallelCommandService)
        {
            _logger = logger;
            _backgroundTaskQueue = backgroundTaskQueue;
            _parallelCommandService = parallelCommandService;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            _logger.LogInformation("Execution started for {requestType}", typeof(TRequest));

            if (request is AsyncCommandBase asyncCommand)
            {
                await _backgroundTaskQueue.QueueBackgroundWorkItem(asyncCommand);

                _logger.LogInformation("Async execution submitted for {requestType}", typeof(TRequest));

                return default;
            }
            else if (request is ParallelCommandBase parallelCommand)
            {
                _parallelCommandService.ProcessCommandMessagePararelly(parallelCommand);

                _logger.LogInformation("Parallel execution has been started for {requestType}", typeof(TRequest));

                return default;
            }

            var response = await next();
            _logger.LogInformation("Execution finished for {requestType}", typeof(TRequest));

            return response;
        }
    }
}
