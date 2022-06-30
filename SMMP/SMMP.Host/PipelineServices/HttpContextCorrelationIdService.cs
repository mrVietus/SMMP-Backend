using System;
using Microsoft.AspNetCore.Http;
using SMMP.Core.Interfaces.PipelineServices;

namespace SMMP.Host.PipelineServices
{
    public class HttpContextCorrelationIdService : ICorrelationIdService
    {
        public const string CorrelationIdKey = "x-correlationid";

        private readonly HttpContext _httpContext;

        public HttpContextCorrelationIdService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContext = httpContextAccessor.HttpContext;
        }

        public string GetCorrelationId()
        {
            var isCorrelationIdAlreadySetInRequest = _httpContext.Request.Headers.TryGetValue(CorrelationIdKey, out var correlationId);

            if (!isCorrelationIdAlreadySetInRequest)
            {
                correlationId = Guid.NewGuid().ToString();
                _httpContext.Request.Headers.Add(CorrelationIdKey, correlationId);
            }

            return correlationId;
        }

        public void SetCorrelationId(string correlationId)
        {
            _httpContext.Response.Headers.Add(CorrelationIdKey, correlationId);
        }
    }
}
