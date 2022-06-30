using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace SMMP.Application.Configuration.Queries
{
    // returning TRequest means value from query (every query needs to return the value)
    public abstract class BaseQueryHandler<TQuery, TResult> : IQueryHandler<TQuery, TResult>
        where TQuery : IRequest<TResult>
    {
        private readonly ILogger<TQuery> _logger;

        public abstract Task<TResult> Handle(TQuery request, CancellationToken cancellationToken);

        protected BaseQueryHandler(ILogger<TQuery> logger)
        {
            _logger = logger;
        }

        protected async Task Execute(Func<CancellationToken, Task> func, Guid? executionId)
        {
            try
            {
                executionId ??= Guid.NewGuid();
                await func.Invoke(CancellationToken.None);
                _logger.LogInformation("{querryType} execution completed for executionId: {executionId}", typeof(TQuery), executionId);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{querryType} execution completed.", typeof(TQuery));
            }
        }
    }
}
