using API.Errors;
using System.Text.Json;

namespace API.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(
            RequestDelegate next,
            ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Erro nao tratado na requisicao {Metodo} {Path}",
                    context.Request.Method,
                    context.Request.Path);

                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(
            HttpContext context,
            Exception exception)
        {
            var problemDetails = ProblemDetailsFactory.Create(exception, context);

            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = problemDetails.Status ?? 500;

            var json = JsonSerializer.Serialize(problemDetails);
            await context.Response.WriteAsync(json);
        }
    }
}
