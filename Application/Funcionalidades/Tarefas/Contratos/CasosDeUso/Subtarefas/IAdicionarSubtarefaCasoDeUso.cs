using Application.Funcionalidades.Tarefas.Dtos.Subtarefas;

namespace Application.Funcionalidades.Tarefas.Contratos.CasosDeUso.Subtarefas
{
    public interface IAdicionarSubtarefaCasoDeUso
    {
        public Task<SubtarefaResposta> Executar(AdicionarSubtarefaRequisicao dto);
    }
}

