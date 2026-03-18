using Application.Dtos.Subtarefas;
using Application.Mappers;
using Application.Utils.Transacao;
using Repository.TarefaRep;

namespace Application.Services.ServSubTarefas
{
    public class ServSubtarefa : IServSubtarefa
    {
        private readonly IRepTarefa _rep;
        private readonly IUnitOfWork _unitOfWork;
        public ServSubtarefa(IRepTarefa rep, IUnitOfWork unitOfWork)
        {
            _rep = rep;
            _unitOfWork = unitOfWork;
        }
        public async Task AdicionarSubtarefa(AdicionarSubtarefaRequest dto)
        {
            await _unitOfWork.BeginTransactionAsync();
            
            var Tarefa = MapSubtarefa.AdicionarSubtarefa(dto);

            _rep.Adicionar(Tarefa);

            await _unitOfWork.CommitTransactionAsync();
        }
    }
}
