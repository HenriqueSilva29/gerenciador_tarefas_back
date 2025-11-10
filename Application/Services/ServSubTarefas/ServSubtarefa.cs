using Application.Dtos.SubtarefaDto;
using Application.Dtos.ToDoItemDtos;
using Application.Mappers;
using Repository.ToDoItemRep;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.ServSubTarefas
{
    public class ServSubtarefa : IServSubtarefa
    {
        private readonly IRepToDoItem _rep;
        public ServSubtarefa(IRepToDoItem rep)
        {
            _rep = rep;
        }
        public async Task AdicionarSubtarefa(AdicionarSubtarefaDto dto)
        {
            var toDoItem = MapSubtarefa.AdicionarSubtarefa(dto);

            await _rep.Adicionar(toDoItem);
        }
    }
}
