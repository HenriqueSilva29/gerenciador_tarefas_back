using Application.Funcionalidades.Notificacoes.Dtos;
using Application.Funcionalidades.Notificacoes.Eventos;
using Application.Interfaces.Messaging;
using Application.Funcionalidades.Lembretes.Contratos.CasosDeUso;
using Application.Funcionalidades.Notificacoes.Contratos.CasosDeUso;
using Domain.Entidades;
using Repository.Repositorios.Lembretes;
using Repository.Repositorios.ParamGerais;

namespace Application.Funcionalidades.Lembretes.CasosDeUso
{
    public class DispararLembreteCasoDeUso : IDispararLembreteCasoDeUso
    {
        private readonly IRepParamGeral _repParamGeral;
        private readonly IRepLembrete _repRepLembrete;
        private readonly IEnviarLembretePorEmailCasoDeUso _enviarLembretePorEmailUseCase;
        private readonly ICriarNotificacaoCasoDeUso _criarNotificacaoUseCase;
        private readonly IRabbitEventPublisher _publisher;

        public DispararLembreteCasoDeUso(
            IRepParamGeral repParamGeral,
            IRepLembrete repRepLembrete,
            IEnviarLembretePorEmailCasoDeUso enviarLembretePorEmailUseCase,
            ICriarNotificacaoCasoDeUso criarNotificacaoUseCase,
            IRabbitEventPublisher publisher)
        {
            _repParamGeral = repParamGeral;
            _repRepLembrete = repRepLembrete;
            _enviarLembretePorEmailUseCase = enviarLembretePorEmailUseCase;
            _criarNotificacaoUseCase = criarNotificacaoUseCase;
            _publisher = publisher;
        }

        public async Task ExecuteAsync(int id)
        {
            var lembrete = await _repRepLembrete.ObterComTarefaPorIdAsync(id);

            if (lembrete is null || !lembrete.Tarefa.CodigoUsuario.HasValue)
                return;

            var paramGeral = await _repParamGeral.ObterPorUsuarioAsync(lembrete.Tarefa.CodigoUsuario.Value);

            if (paramGeral is null)
                return;

            if (paramGeral.ReceberNotificacaoPorEmail)
            {
                await _enviarLembretePorEmailUseCase.ExecuteAsync(lembrete, paramGeral);

                var notificacao = await _criarNotificacaoUseCase.ExecuteAsync(new CriarNotificacaoRequisicao
                {
                    CodigoUsuario = lembrete.Tarefa.CodigoUsuario,
                    Titulo = lembrete.Tarefa.Titulo,
                    Mensagem = "Voce tem uma nova notificacao",
                    Tipo = EnumTipoNotificacao.LembreteNotificado
                });

                if (notificacao.CodigoUsuario.HasValue)
                {
                    await _publisher.PublishAsync(new NotificacaoCriadaEvento(notificacao.Id));
                }
            }
        }
    }
}



