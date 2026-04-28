using Application.Dtos.Notificacoes;
using Application.Events.Notificacoes;
using Application.Interfaces.Messaging;
using Application.Interfaces.UseCases.Lembretes;
using Application.Interfaces.UseCases.Notificacoes;
using Domain.Entities;
using Infra.Messaging.RabbitMQ.Publicadores;
using Microsoft.EntityFrameworkCore;
using Repository.Repositorys.LembreteRep;
using Repository.Repositorys.ParamGeralRep;

namespace Application.UseCase.Lembretes
{
    public class DispararLembreteUseCase : IDispararLembreteUseCase
    {
        private readonly IRepParamGeral _repParamGeral;
        private readonly IRepLembrete _repRepLembrete;
        private readonly IEnviarLembretePorEmailUseCase _enviarLembretePorEmailUseCase;
        private readonly ICriarNotificacaoUseCase _criarNotificacaoUseCase;
        private readonly IRabbitEventPublisher _publisher;

        public DispararLembreteUseCase(
            IRepParamGeral repParamGeral,
            IRepLembrete repRepLembrete,
            IEnviarLembretePorEmailUseCase enviarLembretePorEmailUseCase,
            ICriarNotificacaoUseCase criarNotificacaoUseCase,
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
            var lembrete = await _repRepLembrete.AsQueryable()
                .Include(l => l.Tarefa)
                .Where(l => l.Id == id)
                .FirstOrDefaultAsync();

            var paramGeral = await _repParamGeral.AsQueryable().FirstOrDefaultAsync();

            if (lembrete is null || paramGeral is null)
                return;

            if (paramGeral.ReceberNotificacaoPorEmail)
            {
                await _enviarLembretePorEmailUseCase.ExecuteAsync(lembrete, paramGeral.Email);

                var notificacao = await _criarNotificacaoUseCase.ExecuteAsync(new CriarNotificacaoRequest
                {
                    CodigoUsuario = lembrete.Tarefa.CodigoUsuario,
                    Titulo = lembrete.Tarefa.Titulo,
                    Mensagem = $"Realizar Tarefa em {paramGeral.QuantidadeDateTimeAntesDoInicio}",
                    Tipo = EnumTipoNotificacao.LembreteNotificado
                });

                if (notificacao.CodigoUsuario.HasValue)
                {
                    await _publisher.PublishAsync(new NotificacaoCriadaEvent(notificacao.Id));
                }
            }
        }
    }
}
