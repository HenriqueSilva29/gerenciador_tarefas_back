using Application.Interfaces.UseCases.Tarefas;
using Application.Mappers;
using Application.Utils.Transacao;
using Domain.Enums;
using Domain.Exceptions;
using Microsoft.AspNetCore.Http;
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

            var Tarefa = await _rep.RecuperarPorIdAsync(id);

            if (Tarefa is null) throw new ExceptionApplication(EnumCodigosDeExcecao.RegistroNaoEncontrado, $"Tarefa não encontrada no banco de dados {id}", StatusCodes.Status409Conflict);


            _rep.Remover(Tarefa);

            await _unitOfWork.CommitTransactionAsync();
        }
    }
}
