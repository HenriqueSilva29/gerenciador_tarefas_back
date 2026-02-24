using Application.Interfaces.UseCases;
using Application.Utils.Transacao;
using Domain.Entities.Lembretes;
using Repository.Repositorys.LembreteRep;
using Repository.ToDoItemRep;

namespace Application.UseCase.Lembretes
{
    public class CriarLembreteUseCase : ICriarLembreteUseCase
    {
        private readonly IRepLembrete _repLembrete;
        private readonly IUnitOfWork _unitOfWork;

        public CriarLembreteUseCase
        (
             IUnitOfWork unitOfWork,
             IRepLembrete repLembrete
        )
        {
            _unitOfWork = unitOfWork;
            _repLembrete = repLembrete;
        }

        public async Task CriarLembrete(int id, DateTimeOffset dataVencimento, int diasAntesDoVencimento)
        {
            await _unitOfWork.BeginTransactionAsync();

            var lembrete = new Lembrete(
                id,
                dataVencimento,
                diasAntesDoVencimento,
                "Seu vencimento está próximo"
            );

            await _repLembrete.Adicionar(lembrete);

            await _unitOfWork.CommitTransactionAsync();
;
        }

    }
}
