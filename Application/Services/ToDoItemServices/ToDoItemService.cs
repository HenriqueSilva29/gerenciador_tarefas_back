using Domain.ToDoItem;
using Repository.ToDoItemRep;

namespace Application.Services.ToDoItemServices
{
    public class ToDoItemService
    {
        private readonly IRepToDoItem _ToDoItemRepository;

        public ToDoItemService(IRepToDoItem toDoItemRepository)
        {
            _ToDoItemRepository = toDoItemRepository;
        }

        public async Task<IEnumerable<ToDoItem>> RecuperarTodos()
        {
            return await _ToDoItemRepository.RecuperarTodos();
        }

        public async Task Adicionar(ToDoItem todoItem)
        {
            // Aqui você pode adicionar validações ou lógica de negócios, como garantir que o título não exista
            await _ToDoItemRepository.Adicionar(todoItem);
        }

        public async Task<IEnumerable<ToDoItem>> Filtrar(string category, bool? isCompleted)
        {
            return await _ToDoItemRepository.Filtrar(category, isCompleted);
        }

        public async Task Atualizar(ToDoItem todoItem)
        {
            await _ToDoItemRepository.Atualizar(todoItem);
        }

        public async Task Remover(int id)
        {
            await _ToDoItemRepository.Remover(id);
        }
    }
}
