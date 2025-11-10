using Application.Dtos.SubtarefaDto;
using Domain.ToDoItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                DataCriacao = DateTime.Now,
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
