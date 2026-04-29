using Application.Funcionalidades.Tarefas.Dtos;
using Application.Funcionalidades.Tarefas.Contratos.CasosDeUso;
using Application.Funcionalidades.UsuarioAutenticado.Servicos;
using Application.Utils.Transacao;
using Domain.Entidades;
using Domain.Enumeradores;
using Domain.Excecoes;
using Microsoft.AspNetCore.Http;
using Repository.Repositorios.Tarefas;
using static Domain.Entidades.Tarefa;

namespace Application.Funcionalidades.Tarefas.CasosDeUso
{
    public class AtualizarStatusTarefaCasoDeUso : IAtualizarStatusTarefaCasoDeUso
    {
        private readonly IRepTarefa _rep;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IServicoUsuarioAutenticado _servUsuarioAutenticado;

        public AtualizarStatusTarefaCasoDeUso(
            IRepTarefa rep,
            IUnitOfWork unitOfWork,
            IServicoUsuarioAutenticado servUsuarioAutenticado)
        {
            _rep = rep;
            _unitOfWork = unitOfWork;
            _servUsuarioAutenticado = servUsuarioAutenticado;
        }

        public async Task Executar(int id, AtualizarStatusTarefaRequisicao dto)
        {
            var idUsuario = _servUsuarioAutenticado.ObterIdUsuarioLogado();

            var tarefa = await _rep.ObterPorIdDoUsuarioAsync(id, idUsuario);

            if (tarefa is null)
                throw new ExcecaoAplicacao(
                    EnumCodigosDeExcecao.RegistroNaoEncontrado,
                    "Tarefa nao encontrada no banco de dados",
                    StatusCodes.Status404NotFound);

            var subtarefas = await _rep.RecuperarSubtarefasVinculadasAhTarefa(id, idUsuario);

            await _unitOfWork.BeginTransactionAsync();

            if (subtarefas.Count > 0)
                AtualizarStatusSubtarefas(subtarefas, dto.Status);

            tarefa.AtualizarStatus(dto.Status);
            _rep.Atualizar(tarefa);

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



