using Application.Dtos.SubtarefaDtos;
using Application.Mappers;
using Application.Utils.Transacao;
using Repository.ToDoItemRep;

namespace Application.Services.ServSubTarefas
{
    public class ServSubtarefa : IServSubtarefa
    {
        private readonly IRepToDoItem _rep;
        private readonly IUnitOfWork _unitOfWork;
        public ServSubtarefa(IRepToDoItem rep, IUnitOfWork unitOfWork)
        {
            _rep = rep;
            _unitOfWork = unitOfWork;
        }
        public async Task AdicionarSubtarefa(AdicionarSubtarefaDto dto)
        {
            await _unitOfWork.BeginTransactionAsync();
            
            var toDoItem = MapSubtarefa.AdicionarSubtarefa(dto);

            await _rep.Adicionar(toDoItem);

            await _unitOfWork.CommitTransactionAsync();
        }
    }
}
