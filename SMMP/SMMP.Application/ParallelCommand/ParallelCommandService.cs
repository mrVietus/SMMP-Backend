using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SMMP.Core.Interfaces.ParallelCommand;
using SMMP.Core.Mediator;

namespace SMMP.Application.ParallelCommand
{
    public class ParallelCommandService : IParallelCommandService
    {
        private readonly ILogger<ParallelCommandService> _logger;
        private readonly IServiceProvider _serviceProvider;

        public ParallelCommandService(ILogger<ParallelCommandService> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        public void ProcessCommandMessagePararelly(ParallelCommandBase command)
        {
            try
            {
                Task.Run(() => ProcessCommand(command));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during start parallel command {commandName}.", nameof(command));
                throw;
            }
        }

        private async Task ProcessCommand(ParallelCommandBase command)
        {
            using var scope = _serviceProvider.CreateScope();
            var handler = ResolveCommandHandler(scope, command);

            if (handler == null)
            {
                _logger.LogError("Solution set up error. Handler not found for command {commandName}.", command.GetType().Name);
                return;
            }

            await ExecuteCommandHandler(handler, command);
        }

        private object ResolveCommandHandler(IServiceScope scope, ParallelCommandBase command)
        {
            var commandType = command.GetType();
            var handlerType = typeof(ICommandHandler<>).MakeGenericType(commandType);

            var handler = scope.ServiceProvider.GetService(handlerType);

            return handler;
        }

        private async Task ExecuteCommandHandler(object handler, ParallelCommandBase command)
        {
            var methodName = nameof(ICommandHandler<CommandBase>.Handle);
            var method = handler.GetType().GetMethod(methodName, new[] { command.GetType(), typeof(CancellationToken) });
            var task = (Task)method?.Invoke(handler, new object[] { command, CancellationToken.None });

            if (task == null)
            {
                _logger.LogError("Solution set up error. Method {methodName} not found for {handlerName}.", methodName, handler.GetType().Name);

                return;
            }

            await task.ConfigureAwait(false);
        }
    }
}
