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

        public void CriarLembrete(Tarefa tarefa, DateTimeOffset dataVencimento, int diasAntesDoVencimento)
        {
            var lembrete = new Lembrete(
                tarefa.Id,
                dataVencimento,
                diasAntesDoVencimento,
                "Seu vencimento está próximo"
            );

            tarefa.Lembretes.Add( lembrete );

            _repLembrete.Adicionar(lembrete);
;
        }

    }
}
