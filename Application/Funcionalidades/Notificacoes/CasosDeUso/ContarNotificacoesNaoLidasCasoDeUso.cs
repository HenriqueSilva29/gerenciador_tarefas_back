using Application.Interfaces.Context;
using Application.Funcionalidades.Notificacoes.Contratos.CasosDeUso;
using Domain.Enumeradores;
using Domain.Excecoes;
using Microsoft.AspNetCore.Http;
using Repository.Repositorios.Notificacoes;

namespace Application.Funcionalidades.Notificacoes.CasosDeUso
{
    public class ContarNotificacoesNaoLidasCasoDeUso : IContarNotificacoesNaoLidasCasoDeUso
    {
        private readonly IRepNotificacao _repNotificacao;
        private readonly IUsuarioContexto _usuarioContexto;

        public ContarNotificacoesNaoLidasCasoDeUso(
            IRepNotificacao repNotificacao,
            IUsuarioContexto usuarioContexto)
        {
            _repNotificacao = repNotificacao;
            _usuarioContexto = usuarioContexto;
        }

        public async Task<int> ExecuteAsync()
        {
            var idUsuario = ObterUsuarioLogado();

            return await _repNotificacao.ContarNaoLidasDoUsuarioAsync(idUsuario);
        }

        private int ObterUsuarioLogado()
        {
            if (_usuarioContexto.IdUsuario.HasValue)
                return _usuarioContexto.IdUsuario.Value;

            throw new ExcecaoAplicacao(
                EnumCodigosDeExcecao.CredenciaisInvalidas,
                "Usuario autenticado nao identificado.",
                StatusCodes.Status401Unauthorized);
        }
    }
}



