using Application.Dtos.ToDoItemDtos;
using Application.Interfaces.UseCases.ToDoItems;
using Application.Utils.Transacao;
using Repository.ToDoItemRep;

namespace Application.UseCase.ToDoItems
{
    public class AtualizarPrioridadeToDoItemUseCase : IAtualizarPrioridadeToDoItemUseCase
    {
        private readonly IRepToDoItem _rep;
        private readonly IUnitOfWork _unitOfWork;

        public AtualizarPrioridadeToDoItemUseCase(
            IRepToDoItem rep,
            IUnitOfWork unitOfWork)
        {
            _rep = rep;
            _unitOfWork = unitOfWork;;
        }

        public async Task Executar(int id, AtualizarPrioridadeToDoItemDto dto)
        {
            await _unitOfWork.BeginTransactionAsync();

            var toDoItem = await _rep.RecuperarPorId(id);

            if (toDoItem == null)
                throw new Exception("Tarefa não encontrada.");

            toDoItem.DefinirPrioridade(dto.Prioridade);

            _rep.Atualizar(toDoItem);

            await _unitOfWork.CommitTransactionAsync();
        }
    }
}
