using Application.Funcionalidades.Tarefas.Contratos.CasosDeUso;
using Application.Funcionalidades.UsuarioAutenticado.Servicos;
using Application.Utils.Transacao;
using Domain.Enumeradores;
using Domain.Excecoes;
using Microsoft.AspNetCore.Http;
using Repository.Repositorios.Tarefas;

namespace Application.Funcionalidades.Tarefas.CasosDeUso
{
    public class RemoverTarefaCasoDeUso : IRemoverTarefaCasoDeUso
    {
        private readonly IRepTarefa _rep;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IServicoUsuarioAutenticado _servUsuarioAutenticado;

        public RemoverTarefaCasoDeUso(
            IRepTarefa rep,
            IUnitOfWork unitOfWork,
            IServicoUsuarioAutenticado servUsuarioAutenticado)
        {
            _rep = rep;
            _unitOfWork = unitOfWork;
            _servUsuarioAutenticado = servUsuarioAutenticado;
        }

        public async Task Executar(int id)
        {
            var idUsuario = _servUsuarioAutenticado.ObterIdUsuarioLogado();

            var tarefa = await _rep.ObterPorIdDoUsuarioAsync(id, idUsuario);

            if (tarefa is null)
                throw new ExcecaoAplicacao(
                    EnumCodigosDeExcecao.RegistroNaoEncontrado,
                    $"Tarefa nao encontrada no banco de dados {id}",
                    StatusCodes.Status404NotFound);

            await _unitOfWork.BeginTransactionAsync();
            _rep.Remover(tarefa);
            await _unitOfWork.CommitTransactionAsync();
        }
    }
}



