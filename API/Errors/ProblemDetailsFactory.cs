using Domain.Excecoes;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace API.Errors
{
    public static class ProblemDetailsFactory
    {
        public static ProblemDetails Create(Exception exception, HttpContext context)
        {
            if (exception is ExcecaoBase ex)
            {
                return CreateFromBaseException(ex, context);
            }

            if (exception is ArgumentException argEx)
            {
                return CreateBadRequestProblem(argEx, context);
            }

            return CreateInternalServerError(context);
        }

        private static ProblemDetails CreateFromBaseException(
            ExcecaoBase exception,
            HttpContext context)
        {
            return CriarProblemDetails(
                context,
                exception.Code,
                exception.Title,
                exception.StatusCode,
                exception.Message);
        }

        private static ProblemDetails CreateBadRequestProblem(
            Exception exception,
            HttpContext context)
        {
            return CriarProblemDetails(
                context,
                "invalid-argument",
                "Requisição inválida",
                StatusCodes.Status400BadRequest,
                exception.Message);
        }

        private static ProblemDetails CreateInternalServerError(HttpContext context)
        {
            return CriarProblemDetails(
                context,
                "internal-server-error",
                "Erro interno no servidor",
                StatusCodes.Status500InternalServerError,
                "Ocorreu um erro inesperado.");
        }

        private static ProblemDetails CriarProblemDetails(
            HttpContext context,
            string type,
            string title,
            int status,
            string detail)
        {
            var problemDetails = new ProblemDetails
            {
                Type = type,
                Title = title,
                Status = status,
                Detail = detail,
                Instance = context.Request.Path
            };

            problemDetails.Extensions["traceId"] =
                Activity.Current?.TraceId.ToString() ?? context.TraceIdentifier;
            problemDetails.Extensions["correlationId"] = ObterCorrelationId(context);

            return problemDetails;
        }

        private static string? ObterCorrelationId(HttpContext context)
        {
            if (context.Items.TryGetValue("CorrelationId", out var itemValue))
            {
                return itemValue?.ToString();
            }

            return context.Response.Headers.TryGetValue("X-Correlation-ID", out var responseValue)
                ? responseValue.ToString()
                : context.Request.Headers.TryGetValue("X-Correlation-ID", out var requestValue)
                    ? requestValue.ToString()
                    : null;
        }
    }
}
