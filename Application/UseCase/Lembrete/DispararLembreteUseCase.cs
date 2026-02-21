using Application.Interfaces.Messaging;
using Application.Interfaces.UseCases;
using Application.Utils.Transacao;
using Repository.Repositorys.LembreteRep;
using static Domain.Entities.Lembretes.Lembrete;

namespace Application.UseCase.Lembrete
{
    public class DispararLembreteUseCase : IDispararLembreteUseCase
    {
        private readonly IRepLembrete _rep;
        private readonly IUnitOfWork _unitOfWork;
        private readonly INotificacaoPublisher _notificacaoPublisher;

        public DispararLembreteUseCase(
            IRepLembrete rep,
            IUnitOfWork unitOfWork,
            INotificacaoPublisher notificacaoPublisher)
        {
            _rep = rep;
            _unitOfWork = unitOfWork;
            _notificacaoPublisher = notificacaoPublisher;
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

            await _notificacaoPublisher.PublicarAsync(lembrete);

            await _unitOfWork.CommitTransactionAsync();
        }
    }
}
