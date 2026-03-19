using Application.Events.Tarefas;
using Application.Interfaces.UseCases.Lembretes;
using Application.Utils.Transacao;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Repositorys.LembreteRep;
using Repository.Repositorys.ParamGeralRep;
using Repository.TarefaRep;

namespace Application.UseCase.Lembretes
{
    public class TarefaCriadaGerarLembreteUseCase : ITarefaCriadaGerarLembreteUseCase
    {
        private readonly IRepLembrete _rep;
        private readonly IRepTarefa _repTarefa;
        private readonly IRepParamGeral _repParamGeral;
        private readonly IUnitOfWork _unitOfWork;

        public TarefaCriadaGerarLembreteUseCase
        (
             IUnitOfWork unitOfWork,
             IRepLembrete repLembrete,
             IRepTarefa repTarefa,
             IRepParamGeral repParamGeral
        )
        {
            _unitOfWork = unitOfWork;
            _rep = repLembrete;
            _repTarefa = repTarefa;
            _repParamGeral = repParamGeral;
        }

        public async Task ExecuteAsync(TarefaCriadaEvent evento)
        {
            await _unitOfWork.BeginTransactionAsync();

            var tarefa = await _repTarefa.RecuperarPorId(evento.IdTarefa);

            var paramGeral =  await _repParamGeral.AsQueryable().FirstOrDefaultAsync();

            if (paramGeral.AvisarVencimento is false) return;

            var lembrete = new Lembrete(
                tarefa.Id,
                tarefa.DataVencimento,
                paramGeral.DiasAntesDoVencimento,
                "A tarefa " + tarefa.Titulo + " está próximo de vencer" 
            );

            tarefa.Lembretes.Add( lembrete );

            _rep.Adicionar(lembrete);
;
            await _unitOfWork.CommitTransactionAsync();
        }

    }
}
