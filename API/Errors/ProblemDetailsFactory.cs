using Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Buffers.Text;

namespace API.Errors
{
    public static class ProblemDetailsFactory
    {
        public static ProblemDetails Create(Exception exception, HttpContext context)
        {
            if (exception is ExceptionBase ex)
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
            ExceptionBase exception,
            HttpContext context)
        {
            return new ProblemDetails
            {
                Type = exception.Code,
                Title = exception.Title,
                Status = exception.StatusCode,
                Detail = exception.Message,
                Instance = context.Request.Path,
                Extensions =
                    {
                        ["traceId"] = context.TraceIdentifier
                    }
            };
        }

        private static ProblemDetails CreateBadRequestProblem(
            Exception exception,
            HttpContext context)
        {
            return new ProblemDetails
            {
                Type = "invalid-argument",
                Title = "Requisição inválida",
                Status = StatusCodes.Status400BadRequest,
                Detail = exception.Message,
                Instance = context.Request.Path
            };
        }

        private static ProblemDetails CreateInternalServerError(HttpContext context)
        {
            return new ProblemDetails
            {
                Title = "Erro interno no servidor",
                Status = StatusCodes.Status500InternalServerError,
                Detail = "Ocorreu um erro inesperado.",
                Instance = context.Request.Path,
                Type = "internal-server-error"
            };
        }
    }
}
