using Application.Dtos.Notificacoes;
using Application.Events.Notificacoes;
using Application.Interfaces.Notificacoes;
using Repository.Repositorys.NotificacaoRep;

namespace Application.UseCase.Notificacoes
{
    public class NotificarUsuarioUseCase : INotificarUsuarioUseCase
    {
        private readonly INotificacaoTempoReal _notificacaoTempoReal;
        private readonly IRepNotificacao _rep;
        public NotificarUsuarioUseCase(INotificacaoTempoReal notificacaoTempoReal, IRepNotificacao rep)
        {
            _notificacaoTempoReal = notificacaoTempoReal;
            _rep = rep;
        }
        public async Task ExecuteAsync(NotificacaoCriadaEvent evento)
        {
            var notificacao = await _rep.RecuperarPorIdAsync(evento.IdNotificacao);

            await _notificacaoTempoReal.NotificarUsuarioAsync(
                                        notificacao.CodigoUsuario,
                                        new NotificacaoTempoRealResponse
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
