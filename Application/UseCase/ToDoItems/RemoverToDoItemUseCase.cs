using Application.Interfaces.UseCases.ToDoItems;
using Application.Mappers;
using Application.Utils.Transacao;
using Repository.ToDoItemRep;

namespace Application.UseCase.ToDoItems
{
    public class RemoverToDoItemUseCase : IRemoverToDoItemUseCase
    {
        private readonly IRepToDoItem _rep;
        private readonly IUnitOfWork _unitOfWork;

        public RemoverToDoItemUseCase(
            IRepToDoItem rep,
            IUnitOfWork unitOfWork)
        {
            _rep = rep;
            _unitOfWork = unitOfWork;
        }

        public async Task Executar(int id)
        {
            await _unitOfWork.BeginTransactionAsync();

            var toDoItem = await _rep.RecuperarPorId(id);

            if (toDoItem is null) throw new Exception("Registro não encontrado");

            _rep.Remover(toDoItem);

            await _unitOfWork.CommitTransactionAsync();
        }
    }
}
