using Application.Dtos.Tarefas;
using Repository.QueryModels.Tarefas;
using static Domain.Entities.Tarefa;

namespace Application.Mappers
{
    public static class HistoricoTarefaMapper
    {
        public static HistoricoTarefaItemResponse Mapear(HistoricoTarefaItemQueryModel item)
        {
            var tipoEvento = ParseTipoEvento(item.TipoEvento);

            return new HistoricoTarefaItemResponse
            {
                IdAuditoria = item.IdAuditoria,
                DataOcorrencia = item.DataOcorrencia,
                TipoEvento = tipoEvento.ToString(),
                AcaoExecutada = MontarAcaoExecutada(tipoEvento, item.TituloRelacionado)
            };
        }

        private static TipoEventoHistoricoTarefa ParseTipoEvento(string tipoEvento)
        {
            return Enum.TryParse<TipoEventoHistoricoTarefa>(tipoEvento, true, out var resultado)
                ? resultado
                : TipoEventoHistoricoTarefa.TarefaAtualizada;
        }

        private static string MontarAcaoExecutada(TipoEventoHistoricoTarefa tipoEvento, string? tituloRelacionado)
        {
            return tipoEvento switch
            {
                TipoEventoHistoricoTarefa.TarefaCriada => "Tarefa criada",
                TipoEventoHistoricoTarefa.TarefaAtualizada => "Tarefa atualizada",
                TipoEventoHistoricoTarefa.TarefaConcluida => "Tarefa concluida",
                TipoEventoHistoricoTarefa.TarefaReaberta => "Tarefa reaberta",
                TipoEventoHistoricoTarefa.TarefaExcluida => "Tarefa excluida",

                TipoEventoHistoricoTarefa.SubtarefaCriada => $"Criada subtarefa \"{tituloRelacionado}\"",
                TipoEventoHistoricoTarefa.SubtarefaAtualizada => $"Subtarefa \"{tituloRelacionado}\" atualizada",
                TipoEventoHistoricoTarefa.SubtarefaConcluida => $"Subtarefa \"{tituloRelacionado}\" concluida",
                TipoEventoHistoricoTarefa.SubtarefaReaberta => $"Subtarefa \"{tituloRelacionado}\" reaberta",
                TipoEventoHistoricoTarefa.SubtarefaExcluida => $"Subtarefa \"{tituloRelacionado}\" excluida",
                TipoEventoHistoricoTarefa.SubtarefaVinculada => $"Subtarefa \"{tituloRelacionado}\" vinculada a tarefa",
                TipoEventoHistoricoTarefa.SubtarefaDesvinculada => $"Subtarefa \"{tituloRelacionado}\" removida da tarefa",

                _ => "Evento registrado"
            };
        }
    }
}
