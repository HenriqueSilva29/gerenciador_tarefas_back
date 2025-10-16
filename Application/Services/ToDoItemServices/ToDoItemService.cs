using Application.Services.Dto;
using Application.Services.Mappers.ToDoItems;
using Domain.ToDoItem;
using Repository.ToDoItemRep;

namespace Application.Services.ToDoItemServices
{
    public class ToDoItemService
    {
        private readonly IRepToDoItem _rep;

        public ToDoItemService(IRepToDoItem toDoItemRepository)
        {
            _rep = toDoItemRepository;
        }

        public async Task<IEnumerable<ToDoItem>> RecuperarTodos()
        {
            return await _rep.RecuperarTodos();
        }

        public async Task Adicionar(ToDoItem todoItem)
        {          
            await _rep.Adicionar(todoItem);
        }

        public async Task<IEnumerable<ToDoItem>> Filtrar(string category, bool? isCompleted)
        {
            return await _rep.Filtrar(category, isCompleted);
        }

        public async Task Atualizar(int id, ToDoItemDto dto)
        {

            var ToDoItem = await _rep.RecuperarPorId(id);

            if (ToDoItem is null) throw new Exception("Registro não encontrado");

            ToDoItem = new MapToDoItem().Mapear(ToDoItem, dto);

            await _rep.Atualizar(ToDoItem);
        }

        public async Task Remover(int id)
        {
            var toDoItem = await _rep.RecuperarPorId(id);

            if (toDoItem is null) throw new Exception("Registro não encontrado");

            await _rep.Remover(toDoItem);
        }
    }
}
