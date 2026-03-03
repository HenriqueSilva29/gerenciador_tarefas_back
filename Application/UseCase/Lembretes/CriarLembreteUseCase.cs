using Application.Interfaces.UseCases.Lembretes;
using Application.Utils.Transacao;
using Domain.Entities;
using Repository.Repositorys.LembreteRep;

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

            _repLembrete.Adicionar(lembrete);

            await _unitOfWork.CommitTransactionAsync();
;
        }

    }
}
