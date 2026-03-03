using Application.Dtos.ToDoItemDtos;
using Application.Interfaces.UseCases.ToDoItems;
using Application.Mappers;
using Application.Utils.Transacao;
using Repository.ToDoItemRep;

namespace Application.UseCase.ToDoItems
{
    public class AtualizarToDoItemUseCase : IAtualizarToDoItemUseCase
    {
        private readonly IRepToDoItem _rep;
        private readonly IUnitOfWork _unitOfWork;

        public AtualizarToDoItemUseCase(
            IRepToDoItem rep,
            IUnitOfWork unitOfWork)
        {
            _rep = rep;
            _unitOfWork = unitOfWork;
        }
        public async Task Executar(int id, AtualizarToDoItemDto dto)
        {
            await _unitOfWork.BeginTransactionAsync();

            var ToDoItem = await _rep.RecuperarPorId(id);

            if (ToDoItem is null) throw new Exception("Registro não encontrado");

            ToDoItem = MapToDoItem.AtualizarToDoItemDto(ToDoItem, dto);

            _rep.Atualizar(ToDoItem);

            await _unitOfWork.CommitTransactionAsync();
        }
    }
}
