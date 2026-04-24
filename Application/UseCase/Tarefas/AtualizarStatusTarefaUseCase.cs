using Application.Dtos.Tarefas;
using Application.Interfaces.UseCases.Tarefas;
using Application.Utils.Transacao;
using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Repository.TarefaRep;
using static Domain.Entities.Tarefa;

namespace Application.UseCase.Tarefas
{
    public class AtualizarStatusTarefaUseCase : IAtualizarStatusTarefaUseCase
    {
        private readonly IRepTarefa _rep;
        private readonly IUnitOfWork _unitOfWork;

        public AtualizarStatusTarefaUseCase(
            IRepTarefa rep,
            IUnitOfWork unitOfWork)
        {
            _rep = rep;
            _unitOfWork = unitOfWork; ;
        }

        public async Task Executar(int id, UpdateStatusTarefaRequest dto)
        {
            await _unitOfWork.BeginTransactionAsync();

            var Tarefa = await _rep.RecuperarPorIdAsync(id);

            if (Tarefa == null)
                throw new ExceptionApplication(EnumCodigosDeExcecao.RegistroNaoEncontrado, "Tarefa não encontrada no banco de dados", StatusCodes.Status409Conflict);

            var subtarefas = await _rep.AsQueryable()
                                        .Where(t => t.CodigoTarefaPai == id)
                                        .ToListAsync();

            if (subtarefas.Count > 0) AtualizarStatusSubtarefas(subtarefas, dto.Status);

            Tarefa.AtualizarStatus(dto.Status);

            _rep.Atualizar(Tarefa);

            await _unitOfWork.CommitTransactionAsync();
        }

        private void AtualizarStatusSubtarefas(List<Tarefa> subtarefas, EnumStatusTarefa status)
        {
            foreach (var tarefa in subtarefas)
            {
                tarefa.AtualizarStatus(status);
                _rep.Atualizar(tarefa);
            }
        }

    }
}
