using Application.Interfaces.Messaging;
using Application.Interfaces.UseCases;
using Application.Utils.Transacao;
using Infra.Messaging.RabbitMQ.Publicadores;
using Repository.Repositorys.LembreteRep;
using static Domain.Entities.Lembretes.Lembrete;

namespace Application.UseCase.Lembrete
{
    public class DispararLembreteUseCase : IDispararLembreteUseCase
    {
        private readonly IRepLembrete _rep;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRabbitEventPublisher _publisher;

        public DispararLembreteUseCase(
            IRepLembrete rep,
            IUnitOfWork unitOfWork,
            IRabbitEventPublisher publisher)
        {
            _rep = rep;
            _unitOfWork = unitOfWork;
            _publisher = publisher;
        }

        public async Task ExecuteAsync(Guid lembreteId)
        {
            await _unitOfWork.BeginTransactionAsync();

            var lembrete = await _rep.RecuperarPorGuid(lembreteId);
            if (lembrete is null)
                return;

            if (lembrete.Status != LembreteStatus.Pendente)
                return;

            lembrete.Executar();

            await _rep.Atualizar(lembrete);

            await _unitOfWork.CommitTransactionAsync();

            await _publisher.PublishAsync("lembrete.email", lembrete);

        }
    }
}
