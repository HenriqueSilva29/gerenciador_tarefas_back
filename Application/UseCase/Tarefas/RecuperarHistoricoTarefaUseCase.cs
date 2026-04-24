using Application.Dtos.Tarefas;
using Application.Interfaces.UseCases.Tarefas;
using Application.Mappers;
using Microsoft.EntityFrameworkCore;
using Repository.QueryModels.Tarefas;
using Repository.Repositorys.AuditoriaRep;
using Repository.TarefaRep;

namespace Application.UseCase.Tarefas
{
    public class RecuperarHistoricoTarefaUseCase : IRecuperarHistoricoTarefaUseCase
    {
        private readonly IRepAuditoria _rep;

        public RecuperarHistoricoTarefaUseCase
        (
            IRepAuditoria rep,
            IRepTarefa repTarefa
        )
        {
            _rep = rep;
        }
        public async Task<HistoricoTarefaResponse> Executar(int id)
        {
            var historico = await _rep.RecuperarHistoricoDaTarefa(id);

            return new HistoricoTarefaResponse
            {
                Itens = historico
                    .OrderBy(x => x.DataOcorrencia)
                    .Select(HistoricoTarefaMapper.Mapear)
                    .ToList()
            };

        }

    }
}
