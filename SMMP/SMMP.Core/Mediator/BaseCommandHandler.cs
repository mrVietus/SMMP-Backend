using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.ApplicationInsights;
using System.Threading;

namespace SMMP.Core.Mediator
{
    // returning TRequest means value from command
    public abstract class BaseCommandHandler<TCommand, TRequest> : ICommandHandler<TCommand, TRequest>
        where TCommand : IRequest<TRequest>
    {
        private readonly ILogger<TCommand> _logger;
        private readonly TelemetryClient _telemetryClient;

        public abstract Task<TRequest> Handle(TCommand request, CancellationToken cancellationToken);

        protected BaseCommandHandler(ILogger<TCommand> logger, TelemetryClient telemetryClient)
        {
            _logger = logger;
            _telemetryClient = telemetryClient;
        }
    }

    // void returning from command
    public abstract class BaseCommandHandler<TCommand> : ICommandHandler<TCommand>
        where TCommand : IRequest
    {
        private readonly ILogger<TCommand> _logger;
        private readonly TelemetryClient _telemetryClient;

        protected BaseCommandHandler(ILogger<TCommand> logger, TelemetryClient telemetryClient)
        {
            _logger = logger;
            _telemetryClient = telemetryClient;
        }

        public async Task<Unit> Handle(TCommand request, CancellationToken cancellationToken)
        {
            var executionId = Guid.NewGuid();
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            try
            {
                _telemetryClient.Context.GlobalProperties["ExecutionId"] = executionId.ToString();
                _telemetryClient.Context.GlobalProperties["CommandType"] = typeof(TCommand).Name.ToString();
                await HandleAsync(request, cancellationToken);
            }
            catch (Exception e)
            {
                _telemetryClient.TrackException(e);
            }

            stopwatch.Stop();
            _telemetryClient.Context.GlobalProperties["RequestSec"] = stopwatch.Elapsed.TotalSeconds.ToString();
            _telemetryClient.TrackTrace($"{typeof(TCommand)} execution completed for executionId: {executionId}");
            _telemetryClient.Flush();
            return Unit.Value;
        }

        public abstract Task HandleAsync(TCommand command, CancellationToken cancellationToken);
    }
}
