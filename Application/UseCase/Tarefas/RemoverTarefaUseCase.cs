using Application.Interfaces.UseCases.Tarefas;
using Application.Mappers;
using Application.Utils.Transacao;
using Repository.TarefaRep;

namespace Application.UseCase.Tarefas
{
    public class RemoverTarefaUseCase : IRemoverTarefaUseCase
    {
        private readonly IRepTarefa _rep;
        private readonly IUnitOfWork _unitOfWork;

        public RemoverTarefaUseCase(
            IRepTarefa rep,
            IUnitOfWork unitOfWork)
        {
            _rep = rep;
            _unitOfWork = unitOfWork;
        }

        public async Task Executar(int id)
        {
            await _unitOfWork.BeginTransactionAsync();

            var Tarefa = await _rep.RecuperarPorId(id);

            if (Tarefa is null) throw new ApplicationException("Registro não encontrado");

            _rep.Remover(Tarefa);

            await _unitOfWork.CommitTransactionAsync();
        }
    }
}
