using Application.Funcionalidades.Tarefas.Dtos;

namespace Application.Funcionalidades.Tarefas.Contratos.CasosDeUso
{
    public interface IRecuperarTarefaPorIdCasoDeUso
    {
        Task<TarefaResposta> Executar(int id);
    }
}

