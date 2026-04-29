using Application.Funcionalidades.Tarefas.Dtos;

namespace Application.Funcionalidades.Tarefas.Contratos.CasosDeUso
{
    public interface IAdicionarTarefaCasoDeUso
    {
        public Task<TarefaResposta> Executar(CriarTarefaRequisicao dto);
    }
}

