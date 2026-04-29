using Application.Funcionalidades.Notificacoes.Dtos;
using Application.Funcionalidades.Notificacoes.Eventos;
using Application.Funcionalidades.Notificacoes.Contratos.TempoReal;
using Domain.Enumeradores;
using Domain.Excecoes;
using Microsoft.AspNetCore.Http;
using Repository.Repositorios.Notificacoes;

namespace Application.Funcionalidades.Notificacoes.CasosDeUso
{
    public class NotificarUsuarioCasoDeUso : INotificarUsuarioCasoDeUso
    {
        private readonly INotificacaoTempoReal _notificacaoTempoReal;
        private readonly IRepNotificacao _rep;

        public NotificarUsuarioCasoDeUso(INotificacaoTempoReal notificacaoTempoReal, IRepNotificacao rep)
        {
            _notificacaoTempoReal = notificacaoTempoReal;
            _rep = rep;
        }

        public async Task ExecuteAsync(NotificacaoCriadaEvento evento)
        {
            var notificacao = await _rep.RecuperarPorIdAsync(evento.IdNotificacao);

            if (notificacao is null)
                throw new ExcecaoAplicacao(
                    EnumCodigosDeExcecao.RegistroNaoEncontrado,
                    "Notificacao nao encontrada.",
                    StatusCodes.Status404NotFound);

            if (!notificacao.CodigoUsuario.HasValue)
                throw new ExcecaoAplicacao(
                    EnumCodigosDeExcecao.RegistroSemUsuarioVinculado,
                    "Notificacao sem usuario vinculado.",
                    StatusCodes.Status409Conflict);

            await _notificacaoTempoReal.NotificarUsuarioAsync(
                notificacao.CodigoUsuario.Value,
                new NotificacaoTempoRealResposta
                {
                    Id = notificacao.Id,
                    Tipo = (int)notificacao.Tipo,
                    Titulo = notificacao.Titulo,
                    Mensagem = notificacao.Mensagem,
                    DataCriacao = notificacao.DataCriacao,
                    Lida = notificacao.Lida
                });
        }
    }
}



