using Application.Funcionalidades.Tarefas.Eventos;
using Application.Funcionalidades.Lembretes.Contratos.CasosDeUso;
using Application.Utils.Transacao;
using Domain.Comum.ObjetosDeValor;
using Domain.Entidades;
using Repository.Repositorios.Lembretes;
using Repository.Repositorios.ParamGerais;
using Repository.Repositorios.Tarefas;
using static Domain.Entidades.Lembrete;

namespace Application.Funcionalidades.Lembretes.CasosDeUso
{
    public class TarefaCriadaGerarLembreteCasoDeUso : IGerarLembreteCasoDeUso
    {
        private readonly IRepLembrete _rep;
        private readonly IRepTarefa _repTarefa;
        private readonly IRepParamGeral _repParamGeral;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAgendadorJobLembrete _agendarLembreteJobScheduler;

        public TarefaCriadaGerarLembreteCasoDeUso(
             IUnitOfWork unitOfWork,
             IRepLembrete repLembrete,
             IRepTarefa repTarefa,
             IRepParamGeral repParamGeral,
             IAgendadorJobLembrete agendarLembreteJobScheduler)
        {
            _unitOfWork = unitOfWork;
            _rep = repLembrete;
            _repTarefa = repTarefa;
            _repParamGeral = repParamGeral;
            _agendarLembreteJobScheduler = agendarLembreteJobScheduler;
        }

        public async Task ExecuteAsync(TarefaCriadaEvento evento)
        {
            var tarefa = await _repTarefa.RecuperarPorIdAsync(evento.IdTarefa);

            if (tarefa is null || !tarefa.CodigoUsuario.HasValue)
                return;

            var paramGeral = await _repParamGeral.ObterPorUsuarioAsync(tarefa.CodigoUsuario.Value);

            if (paramGeral is null || paramGeral.NotificarTarefasAntesDoInicio is false)
                return;

            await _unitOfWork.BeginTransactionAsync();

            var dataDisparo = CalcularDataDisparo(tarefa.DataTarefa, tarefa.HoraInicio, paramGeral);

            var lembrete = new Lembrete(tarefa.Id, dataDisparo)
            {
                Tarefa = tarefa,
                Status = EnumLembreteStatus.Pendente,
                Descricao = tarefa.Descricao
            };

            tarefa.Lembretes.Add(lembrete);

            _repTarefa.Atualizar(tarefa);
            _rep.Adicionar(lembrete);

            await _unitOfWork.CommitTransactionAsync();

            await _agendarLembreteJobScheduler.ExecuteAsync(lembrete.Id, dataDisparo);
        }

        public UtcDateTime CalcularDataDisparo(DateOnly dataTarefa, TimeOnly horaInicio, ParamGeral paramGeral)
        {
            var antecedencia = AntecedenciaLembrete
                .From(paramGeral.QuantidadeDateTimeAntesDoInicio, paramGeral.Unidade)
                .Value;

            var dateTimeTarefaUtc = UtcDateTime.From(dataTarefa, horaInicio);

            var dataDisparo = dateTimeTarefaUtc.Subtract(antecedencia);

            return dataDisparo;
        }
    }
}



