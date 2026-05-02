using Application.Observabilidade;
using Serilog.Context;
using System.Diagnostics;

namespace API.Middlewares
{
    public class RastreabilidadeMiddleware
    {
        private const string CorrelationHeader = "X-Correlation-ID";
        private readonly RequestDelegate _next;

        public RastreabilidadeMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(
            HttpContext context,
            ICorrelationContextAccessor correlationContextAccessor,
            ILogger<RastreabilidadeMiddleware> logger)
        {
            var correlationId = ObterCorrelationId(context);
            correlationContextAccessor.Context = new CorrelationContext
            {
                CorrelationId = correlationId,
                TraceId = Activity.Current?.TraceId.ToString(),
                TraceParent = Activity.Current?.Id,
                TraceState = Activity.Current?.TraceStateString
            };
            context.Items["CorrelationId"] = correlationId;

            context.Response.OnStarting(() =>
            {
                context.Response.Headers[CorrelationHeader] = correlationId;
                return Task.CompletedTask;
            });

            Activity.Current?.SetTag("correlation.id", correlationId);

            using (logger.BeginScope(new Dictionary<string, object>
            {
                ["CorrelationId"] = correlationId,
                ["TraceId"] = Activity.Current?.TraceId.ToString() ?? correlationId
            }))
            using (LogContext.PushProperty("CorrelationId", correlationId))
            using (LogContext.PushProperty("TraceId", Activity.Current?.TraceId.ToString() ?? correlationId))
            {
                await _next(context);
            }
        }

        private static string ObterCorrelationId(HttpContext context)
        {
            if (context.Request.Headers.TryGetValue(CorrelationHeader, out var value) &&
                !string.IsNullOrWhiteSpace(value))
            {
                return value.ToString();
            }

            return Activity.Current?.TraceId.ToString() ?? Guid.NewGuid().ToString("N");
        }
    }
}
