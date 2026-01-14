using Application.Dtos.SubtarefaDtos;
using Domain.Common.ValueObjects;
using Domain.Entities.ToDoItems;

namespace Application.Mappers
{
    public static class MapSubtarefa
    {
        public static ToDoItem AdicionarSubtarefa(AdicionarSubtarefaDto dto)
        {
            var toDoItem = new ToDoItem()
            {
                Titulo = dto.Titulo,
                Descricao = dto.Descricao,
                DataCriacao = UtcDateTime.Now(),
                DataVencimento = dto.DataVencimento,
                Status = dto.Status,
                Prioridade = dto.Prioridade,
                Categoria = dto.Categoria,
                CodigoToDoItemPai = dto.CodigoToDoItemPai,
            };

            return toDoItem;
        }
    }
}
