using Application.Events.Tarefas;
using Application.Interfaces.UseCases.Lembretes;
using Application.Utils.Transacao;
using Domain.Common.ValueObjects;
using Domain.Entities;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using Repository.Repositorys.LembreteRep;
using Repository.Repositorys.ParamGeralRep;
using Repository.TarefaRep;
using static Domain.Entities.Lembrete;

namespace Application.UseCase.Lembretes
{
    public class TarefaCriadaGerarLembreteUseCase : IGerarLembreteUseCase
    {
        private readonly IRepLembrete _rep;
        private readonly IRepTarefa _repTarefa;
        private readonly IRepParamGeral _repParamGeral;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAgendarLembreteJobScheduler _agendarLembreteJobScheduler;

        public TarefaCriadaGerarLembreteUseCase
        (
             IUnitOfWork unitOfWork,
             IRepLembrete repLembrete,
             IRepTarefa repTarefa,
             IRepParamGeral repParamGeral,
             IAgendarLembreteJobScheduler agendarLembreteJobScheduler
        )
        {
            _unitOfWork = unitOfWork;
            _rep = repLembrete;
            _repTarefa = repTarefa;
            _repParamGeral = repParamGeral;
            _agendarLembreteJobScheduler = agendarLembreteJobScheduler;
        }

        public async Task ExecuteAsync(TarefaCriadaEvent evento)
        {
            var paramGeral =  await _repParamGeral.AsQueryable().FirstOrDefaultAsync();

            if (paramGeral.NotificarTarefasAntesDoInicio is false) return;

            await _unitOfWork.BeginTransactionAsync();

            var tarefa = await _repTarefa.RecuperarPorIdAsync(evento.IdTarefa);

            var dataDisparo = CalcularDataDisparo(tarefa.DataTarefa, tarefa.HoraInicio);

            var lembrete = new Lembrete(tarefa.Id, dataDisparo)
            {
                Tarefa = tarefa,
                Status = EnumLembreteStatus.Pendente,
                Descricao = tarefa.Descricao
            };

            tarefa.Lembretes.Add( lembrete );

            _repTarefa.Atualizar(tarefa);

            _rep.Adicionar(lembrete);
;
            await _unitOfWork.CommitTransactionAsync();

            await _agendarLembreteJobScheduler.ExecuteAsync(lembrete.Id, dataDisparo);

        }

        public UtcDateTime CalcularDataDisparo(DateOnly dataTarefa, TimeOnly horaInicio)
        {
            var avisarAntesDe = _repParamGeral.AsQueryable().Select(p => p.QuantidadeDateTimeAntesDoInicio).FirstOrDefault();
            var unidade = _repParamGeral.AsQueryable().Select(p => p.Unidade).FirstOrDefault();

            var antecedencia = AntecedenciaLembrete.From(avisarAntesDe, unidade).Value;

            var dateTimeTarefaUtc = UtcDateTime.From(dataTarefa, horaInicio);

            var dataDisparo = dateTimeTarefaUtc.Subtract(antecedencia);

            return dataDisparo;

        }
    }
}
