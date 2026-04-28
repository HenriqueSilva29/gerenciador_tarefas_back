using Application.Interfaces.Context;
using Application.Interfaces.UseCases.Notificacoes;
using Domain.Enums;
using Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Repository.Repositorys.NotificacaoRep;

namespace Application.UseCase.Notificacoes
{
    public class ContarNotificacoesNaoLidasUseCase : IContarNotificacoesNaoLidasUseCase
    {
        private readonly IRepNotificacao _repNotificacao;
        private readonly IUsuarioContexto _usuarioContexto;

        public ContarNotificacoesNaoLidasUseCase(
            IRepNotificacao repNotificacao,
            IUsuarioContexto usuarioContexto)
        {
            _repNotificacao = repNotificacao;
            _usuarioContexto = usuarioContexto;
        }

        public async Task<int> ExecuteAsync()
        {
            var idUsuario = ObterUsuarioLogado();

            return await _repNotificacao.AsQueryable()
                .AsNoTracking()
                .CountAsync(n => n.CodigoUsuario == idUsuario && !n.Lida);
        }

        private int ObterUsuarioLogado()
        {
            if (_usuarioContexto.IdUsuario.HasValue)
                return _usuarioContexto.IdUsuario.Value;

            throw new ExceptionApplication(
                EnumCodigosDeExcecao.CredenciaisInvalidas,
                "Usuario autenticado nao identificado.",
                StatusCodes.Status401Unauthorized);
        }
    }
}
