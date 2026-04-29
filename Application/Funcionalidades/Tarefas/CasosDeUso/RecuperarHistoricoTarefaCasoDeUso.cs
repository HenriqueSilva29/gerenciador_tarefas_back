using Application.Funcionalidades.Tarefas.Dtos;
using Application.Funcionalidades.Tarefas.Contratos.CasosDeUso;
using Application.Funcionalidades.Tarefas.Mapeadores;
using Microsoft.EntityFrameworkCore;
using Repository.ModelosConsulta.Tarefas;
using Repository.Repositorios.Auditorias;
using Repository.Repositorios.Tarefas;

namespace Application.Funcionalidades.Tarefas.CasosDeUso
{
    public class RecuperarHistoricoTarefaCasoDeUso : IRecuperarHistoricoTarefaCasoDeUso
    {
        private readonly IRepAuditoria _rep;

        public RecuperarHistoricoTarefaCasoDeUso
        (
            IRepAuditoria rep,
            IRepTarefa repTarefa
        )
        {
            _rep = rep;
        }
        public async Task<HistoricoTarefaResposta> Executar(int id)
        {
            var historico = await _rep.RecuperarHistoricoDaTarefa(id);

            return new HistoricoTarefaResposta
            {
                Itens = historico
                    .OrderBy(x => x.DataOcorrencia)
                    .Select(HistoricoTarefaMapeador.Mapear)
                    .ToList()
            };

        }

    }
}



