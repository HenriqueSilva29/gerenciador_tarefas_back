using Application.Dtos.Tarefas.Subtarefas;
using Domain.Common.ValueObjects;
using Domain.Entities;

namespace Application.Mappers
{
    public static class MapSubtarefa
    {
        public static Tarefa ParaTarefa(AdicionarSubtarefaRequest dto)
        {
            var Tarefa = new Tarefa()
            {
                Titulo = dto.Titulo,
                Descricao = dto.Descricao,
                DataCriacao = UtcDateTime.Now(),
                DataTarefa = dto.DataTarefa,
                HoraInicio = dto.HoraInicio,
                HoraFim = dto.HoraFim,
                Prioridade = dto.Prioridade,
                Categoria = dto.Categoria,
                CodigoTarefaPai = dto.CodigoTarefaPai,
            };

            return Tarefa;
        }
    }
}
