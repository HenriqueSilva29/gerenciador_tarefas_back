using Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace API.Errors
{
    public static class ProblemDetailsFactory
    {
        public static ProblemDetails Create(Exception exception, HttpContext context)
        {
            return exception switch
            {
                DomainException domainEx => CreateDomainProblem(domainEx, context),
                ArgumentException argEx => CreateBadRequestProblem(argEx, context),
                KeyNotFoundException keyEx => CreateNotFoundProblem(keyEx, context),
                _ => CreateInternalServerError(exception, context)
            };
        }

        private static ProblemDetails CreateDomainProblem(
            DomainException exception,
            HttpContext context)
        {
            return new ProblemDetails
            {
                Type = "domain-error",
                Title = "Erro de regra de negócio",
                Status = StatusCodes.Status400BadRequest,
                Detail = exception.Message,
                Instance = context.Request.Path
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

        private static ProblemDetails CreateNotFoundProblem(
            Exception exception,
            HttpContext context)
        {
            return new ProblemDetails
            {
                Type = "not-found",
                Title = "Recurso não encontrado",
                Status = StatusCodes.Status404NotFound,
                Detail = exception.Message,
                Instance = context.Request.Path
            };
        }

        private static ProblemDetails CreateInternalServerError(
            Exception exception,
            HttpContext context)
        {
            return new ProblemDetails
            {
                Type = "internal-server-error",
                Title = "Erro interno no servidor",
                Status = StatusCodes.Status500InternalServerError,
                Detail = "Ocorreu um erro inesperado.",
                Instance = context.Request.Path
            };
        }
    }
}
