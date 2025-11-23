using System.Net;
using System.Text.Json;

namespace SGE.Application.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next,
                                       ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Recurso no encontrado");
                await WriteErrorAsync(context, ex.Message, HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado");
                await WriteErrorAsync(context, "Ha ocurrido un error interno", HttpStatusCode.InternalServerError);
            }
        }

        private static Task WriteErrorAsync(HttpContext context, string message, HttpStatusCode statusCode)
        {
            var problem = new
            {
                success = false,
                error = message,
                statusCode = (int)statusCode
            };

            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "application/json";

            return context.Response.WriteAsync(JsonSerializer.Serialize(problem));
        }
    }
}
