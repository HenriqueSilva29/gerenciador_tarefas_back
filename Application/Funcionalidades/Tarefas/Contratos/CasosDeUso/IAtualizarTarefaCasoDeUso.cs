using Application.Funcionalidades.Tarefas.Dtos;

namespace Application.Funcionalidades.Tarefas.Contratos.CasosDeUso
{
    public interface IAtualizarTarefaCasoDeUso
    {
        public Task Executar(int id, AtualizarTarefaRequisicao dto);
    }
}

