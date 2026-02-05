using System.Text.Json;
using TodoListManagementSystem.Shared.Exceptions;

namespace TodoListManagementSystem.RESTFulApi.Middlewares
{
    public sealed class ExceptionHandlingMiddleware(
        RequestDelegate next,
        ILogger<ExceptionHandlingMiddleware> logger)
    {
        private readonly RequestDelegate _next = next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger = logger;

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleAsync(context, ex);
            }
        }

        private async Task HandleAsync(HttpContext context, Exception exception)
        {
            switch (exception)
            {
                case ISolutionException solutionException:
                    await HandleSolutionException(context, solutionException);
                    break;

                default:
                    await HandleUnknownException(context, exception);
                    break;
            }
        }

        private async Task HandleSolutionException(
            HttpContext context,
            ISolutionException exception)
        {
            var statusCode = (int)exception.ErrorType;

            var response = new
            {
                statusCode,
                error = exception.ErrorType.ToString(),
                message = exception.Message,
                detail = ExtractDetail(exception),
                traceId = context.TraceIdentifier
            };

            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(
                JsonSerializer.Serialize(response));
        }

        private async Task HandleUnknownException(
            HttpContext context,
            Exception exception)
        {
            _logger.LogError(
                exception,
                "Unhandled exception");

            var response = new
            {
                statusCode = StatusCodes.Status500InternalServerError,
                error = ErrorType.InternalServerError.ToString(),
                message = "An unexpected error occurred.",
                traceId = context.TraceIdentifier
            };

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(
                JsonSerializer.Serialize(response));
        }

        private static object? ExtractDetail(ISolutionException exception)
        {
            var type = exception.GetType();

            if (!type.IsGenericType)
                return null;

            var detailProperty = type.GetProperty("Detail");
            return detailProperty?.GetValue(exception);
        }
    }
}
