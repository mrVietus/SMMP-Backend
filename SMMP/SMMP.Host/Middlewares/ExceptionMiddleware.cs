using System;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SMMP.Core.Exceptions;
using SMMP.Core.Interfaces.PipelineServices;
using SMMP.Core.Models.Pipeline;

namespace SMMP.Host.Middlewares
{
    public class ExceptionMiddleware
    {
        private const string InfrastructureExceptionDisplayMessage = "Error in communicating with internal server.";
        private const string UnhandledExceptionDisplayMessage = "There was an error while making a request.";

        private const string DetailsMessageKey = "detailsMessage";

        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, ICorrelationIdService correlationIdService)
        {
            var correlationId = correlationIdService.GetCorrelationId();

            try
            {
                await _next(context);
            }
            catch (InfrastructureException ex)
            {
                var errorResponse = InfrastructureExceptionToApiErrorResponse(ex);
                errorResponse.CorrelationId = correlationId;

                await HandleException(context, ex, errorResponse);
            }
            catch (NotFoundException ex)
            {
                var errorResponse = NotFoundExceptionToApiErrorResponse(ex);
                errorResponse.CorrelationId = correlationId;

                await HandleException(context, ex, errorResponse);
            }
            catch (CoreException ex)
            {
                var errorResponse = CoreExceptionToApiErrorResponse(ex);
                errorResponse.CorrelationId = correlationId;

                await HandleException(context, ex, errorResponse);
            }
            catch (Exception ex)
            {
                var errorResponse = StandardExceptionToApiErrorResponse(ex);
                errorResponse.CorrelationId = correlationId;

                await HandleException(context, ex, errorResponse);
            }
        }

        private ApiErrorResponse InfrastructureExceptionToApiErrorResponse(InfrastructureException exception)
        {
            var errorResponse = new ApiErrorResponse
            {
                Code = exception.StatusCode,
                DisplayMessage = InfrastructureExceptionDisplayMessage,
            };
            AddDetailsMessageToApiErrorResponse(ref errorResponse, exception.Message);

            return errorResponse;
        }

        private ApiErrorResponse NotFoundExceptionToApiErrorResponse(NotFoundException exception)
        {
            return new ApiErrorResponse
            {
                Code = exception.StatusCode,
                DisplayMessage = exception.Message,
            };
        }

        private ApiErrorResponse CoreExceptionToApiErrorResponse(CoreException exception)
        {
            var errorResponse = new ApiErrorResponse
            {
                Code = exception.StatusCode,
                DisplayMessage = UnhandledExceptionDisplayMessage,
            };
            AddDetailsMessageToApiErrorResponse(ref errorResponse, exception.Message);

            return errorResponse;
        }

        private ApiErrorResponse StandardExceptionToApiErrorResponse(Exception exception)
        {
            var errorResponse = new ApiErrorResponse
            {
                Code = (int)HttpStatusCode.InternalServerError,
                DisplayMessage = UnhandledExceptionDisplayMessage,
            };

            return errorResponse;
        }

        private void AddDetailsMessageToApiErrorResponse(ref ApiErrorResponse response, string message)
        {
            response.Details.Add(DetailsMessageKey, message);
        }

        private async Task HandleException(HttpContext context, Exception exception, ApiErrorResponse errorResponse)
        {
            _logger.LogError(exception, exception.Message);

            context.Response.StatusCode = errorResponse.Code;

            var fullErrorBody = new
            {
                error = errorResponse
            };
            var serializedFullErrorBody = JsonSerializer.Serialize(fullErrorBody);

            await context.Response.Body.WriteAsync(Encoding.UTF8.GetBytes(serializedFullErrorBody));
        }
    }
}
