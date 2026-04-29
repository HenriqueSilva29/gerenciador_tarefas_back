using Application.Funcionalidades.Tarefas.Dtos;
using Application.Funcionalidades.Tarefas.Contratos.CasosDeUso;
using Application.Funcionalidades.UsuarioAutenticado.Servicos;
using Domain.Enumeradores;
using Domain.Excecoes;
using Microsoft.AspNetCore.Http;
using Repository.Repositorios.Tarefas;

namespace Application.Funcionalidades.Tarefas.CasosDeUso
{
    public class RecuperarTarefaPorIdCasoDeUso : IRecuperarTarefaPorIdCasoDeUso
    {
        private readonly IRepTarefa _rep;
        private readonly IServicoUsuarioAutenticado _servUsuarioAutenticado;

        public RecuperarTarefaPorIdCasoDeUso(
            IRepTarefa rep,
            IServicoUsuarioAutenticado servUsuarioAutenticado)
        {
            _rep = rep;
            _servUsuarioAutenticado = servUsuarioAutenticado;
        }

        public async Task<TarefaResposta> Executar(int id)
        {
            var idUsuario = _servUsuarioAutenticado.ObterIdUsuarioLogado();

            var tarefa = await _rep.ObterPorIdDoUsuarioAsync(id, idUsuario);

            if (tarefa is null)
                throw new ExcecaoAplicacao(
                    EnumCodigosDeExcecao.RegistroNaoEncontrado,
                    $"Tarefa nao encontrada no banco de dados. Id: {id}",
                    StatusCodes.Status404NotFound);

            return new TarefaResposta
            {
                Id = tarefa.Id
            };
        }
    }
}



