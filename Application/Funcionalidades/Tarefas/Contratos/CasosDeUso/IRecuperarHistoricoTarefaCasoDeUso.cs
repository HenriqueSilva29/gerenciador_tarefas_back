using Application.Funcionalidades.Tarefas.Dtos;

namespace Application.Funcionalidades.Tarefas.Contratos.CasosDeUso
{
    public interface IRecuperarHistoricoTarefaCasoDeUso
    {
        Task<HistoricoTarefaResposta> Executar(int id);
    }
}

