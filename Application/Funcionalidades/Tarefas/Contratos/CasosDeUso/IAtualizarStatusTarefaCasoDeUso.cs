using Application.Funcionalidades.Tarefas.Dtos;

namespace Application.Funcionalidades.Tarefas.Contratos.CasosDeUso
{
    public interface IAtualizarStatusTarefaCasoDeUso
    {
        public Task Executar(int id, AtualizarStatusTarefaRequisicao dto);
    }
}

