using Application.Interfaces.UseCases;
using Application.Utils.Transacao;
using Infra.Messaging.RabbitMQ.Publicadores;
using Repository.Repositorys.LembreteRep;

namespace Application.UseCase.Lembretes
{
    public class VerificarLembretesVencendoUseCase : IVerificarLembretesVencendoUseCase
    {
        private readonly IRepLembrete _rep;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRabbitEventPublisher _publisher;

        public VerificarLembretesVencendoUseCase(
            IRepLembrete rep,
            IUnitOfWork unitOfWork,
            IRabbitEventPublisher publisher)
        {
            _rep = rep;
            _unitOfWork = unitOfWork;
            _publisher = publisher;
        }

        public async Task ExecuteAsync()
        {
            var agora = DateTime.UtcNow;

            var lembretes = await _rep.ObterPendentesParaDisparo(agora);

            foreach (var lembrete in lembretes)
            {
                await _unitOfWork.BeginTransactionAsync();

                lembrete.Executar();

                _rep.Atualizar(lembrete);

                await _unitOfWork.CommitTransactionAsync();

                await _publisher.PublishAsync(
                    new LembreteVencimentoAtingidoEvent(lembrete.Id));
            }
        }
    }
}

