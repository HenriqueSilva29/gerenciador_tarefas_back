using Application.Funcionalidades.Tarefas.Dtos;

namespace Application.Funcionalidades.Tarefas.Contratos.CasosDeUso
{
    public interface IAtualizarPrioridadeTarefaCasoDeUso
    {
        public Task Executar(int id, AtualizarPrioridadeTarefaRequisicao dto);
    }
}

