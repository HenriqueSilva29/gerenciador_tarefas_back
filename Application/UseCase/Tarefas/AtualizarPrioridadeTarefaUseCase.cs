using Application.Dtos.Tarefas;
using Application.Interfaces.UseCases.Tarefas;
using Application.Utils.Transacao;
using Domain.Enums;
using Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Repository.TarefaRep;

namespace Application.UseCase.Tarefas
{
    public class AtualizarPrioridadeTarefaUseCase : IAtualizarPrioridadeTarefaUseCase
    {
        private readonly IRepTarefa _rep;
        private readonly IUnitOfWork _unitOfWork;

        public AtualizarPrioridadeTarefaUseCase(
            IRepTarefa rep,
            IUnitOfWork unitOfWork)
        {
            _rep = rep;
            _unitOfWork = unitOfWork;;
        }

        public async Task Executar(int id, UpdatePrioridadeTarefaRequest dto)
        {
            await _unitOfWork.BeginTransactionAsync();

            var Tarefa = await _rep.RecuperarPorIdAsync(id);

            if (Tarefa == null)
                throw new ExceptionApplication(EnumCodigosDeExcecao.RegistroNaoEncontrado, "Tarefa não encontrada no banco de dados", StatusCodes.Status409Conflict);

            Tarefa.DefinirPrioridade(dto.Prioridade);

            _rep.Atualizar(Tarefa);

            await _unitOfWork.CommitTransactionAsync();
        }
    }
}
