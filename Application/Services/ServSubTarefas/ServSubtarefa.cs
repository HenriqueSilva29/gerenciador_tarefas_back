using Application.Dtos.SubtarefaDtos;
using Application.Dtos.SubtarefaDtos;
using Application.Dtos.ToDoItemDtos;
using Application.Mappers;
using Repository.ToDoItemRep;

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
