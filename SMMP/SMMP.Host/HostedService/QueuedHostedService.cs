using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SMMP.Core.Interfaces.HostedService;
using SMMP.Core.Mediator;

namespace SMMP.Host.HostedService
{
    public class QueuedHostedService : BackgroundService
    {
        private readonly ILogger<QueuedHostedService> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly IBackgroundTaskQueue _taskQueue;

        public QueuedHostedService(ILogger<QueuedHostedService> logger, IServiceProvider serviceProvider, IBackgroundTaskQueue taskQueue)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _taskQueue = taskQueue;
        }

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation($"Queued Hosted Service is stopping.");
            await base.StopAsync(stoppingToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation($"Queued Hosted Service is running.");
            await BackgroundProcessing(stoppingToken);
        }

        private async Task BackgroundProcessing(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var command = await _taskQueue.DequeueAsync(cancellationToken);

                try
                {
                    using var scope = _serviceProvider.CreateScope();
                    var handler = ResolveCommandHandler(scope, command);

                    if (handler == null)
                    {
                        _logger.LogError("Solution set up error. Handler not found for command {commandName}.", command.GetType().Name);
                        return;
                    }

                    await ExecuteCommandHandler(handler, command, cancellationToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred handling {commandClassName}.", nameof(command));
                }
            }
        }

        private object ResolveCommandHandler(IServiceScope scope, AsyncCommandBase command)
        {
            var commandType = command.GetType();
            var handlerType = typeof(ICommandHandler<>).MakeGenericType(commandType);
            var handler = scope.ServiceProvider.GetService(handlerType);

            return handler;
        }

        private async Task ExecuteCommandHandler(object handler, AsyncCommandBase command, CancellationToken cancellationToken)
        {
            var methodName = nameof(ICommandHandler<CommandBase>.Handle);
            var method = handler.GetType().GetMethod(methodName, new[] { command.GetType(), typeof(CancellationToken) });
            var task = (Task)method?.Invoke(handler, new object[] { command, cancellationToken });

            if (task == null)
            {
                _logger.LogError("Solution set up error. Method {methodName} not found for {handlerName}.", methodName, handler.GetType().Name);

                return;
            }

            await task.ConfigureAwait(false);
        }
    }
}
