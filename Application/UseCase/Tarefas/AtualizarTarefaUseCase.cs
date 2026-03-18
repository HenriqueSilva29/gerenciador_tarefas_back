using Application.Dtos.Tarefas;
using Application.Interfaces.UseCases.Tarefas;
using Application.Mappers;
using Application.Utils.Transacao;
using Repository.TarefaRep;

namespace Application.UseCase.Tarefas
{
    public class AtualizarTarefaUseCase : IAtualizarTarefaUseCase
    {
        private readonly IRepTarefa _rep;
        private readonly IUnitOfWork _unitOfWork;

        public AtualizarTarefaUseCase(
            IRepTarefa rep,
            IUnitOfWork unitOfWork)
        {
            _rep = rep;
            _unitOfWork = unitOfWork;
        }
        public async Task Executar(int id, UpdateTarefaRequest dto)
        {
            await _unitOfWork.BeginTransactionAsync();

            var Tarefa = await _rep.RecuperarPorId(id);

            if (Tarefa is null) throw new ApplicationException("Registro não encontrado");

            Tarefa = MapTarefa.AtualizarTarefaDto(Tarefa, dto);

            _rep.Atualizar(Tarefa);

            await _unitOfWork.CommitTransactionAsync();
        }
    }
}
