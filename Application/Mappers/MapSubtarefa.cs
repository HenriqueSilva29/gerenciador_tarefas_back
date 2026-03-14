using Application.Dtos.SubtarefaDtos;
using Domain.Common.ValueObjects;
using Domain.Entities;

namespace Application.Mappers
{
    public static class MapSubtarefa
    {
        public static Tarefa AdicionarSubtarefa(AdicionarSubtarefaDto dto)
        {
            var Tarefa = new Tarefa()
            {
                Titulo = dto.Titulo,
                Descricao = dto.Descricao,
                DataCriacao = UtcDateTime.Now(),
                DataVencimento = dto.DataVencimento,
                Status = dto.Status,
                Prioridade = dto.Prioridade,
                Categoria = dto.Categoria,
                CodigoTarefaPai = dto.CodigoTarefaPai,
            };

            return Tarefa;
        }
    }
}
