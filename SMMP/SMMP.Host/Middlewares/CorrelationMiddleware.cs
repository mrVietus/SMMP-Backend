using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SMMP.Core.Interfaces.PipelineServices;

namespace SMMP.Host.Middlewares
{
    public class CorrelationMiddleware
    {
        public const string TransactionIdKey = "x-transactionid";

        private readonly RequestDelegate _next;
        private readonly ILogger<CorrelationMiddleware> _logger;

        public CorrelationMiddleware(RequestDelegate next, ILogger<CorrelationMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public Task InvokeAsync(HttpContext context, ICorrelationIdService correlationIdService)
        {
            var transactionId = Guid.NewGuid().ToString();
            var correlationId = correlationIdService.GetCorrelationId();

            using var transactionIdScope = _logger.BeginScope("TransactionId:{@TransactionId}", transactionId);
            using var correlationIdScope = _logger.BeginScope("CorrelationId:{@CorrelationId}", correlationId);
            context.Request.Headers.Add(TransactionIdKey, transactionId);

            correlationIdService.SetCorrelationId(correlationId);
            context.Response.Headers.Add(TransactionIdKey, transactionId);

            return _next(context);
        }
    }
}
