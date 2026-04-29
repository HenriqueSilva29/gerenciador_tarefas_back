using Application.Funcionalidades.Tarefas.Eventos;

namespace Application.Funcionalidades.Lembretes.Contratos.CasosDeUso
{
    public interface IGerarLembreteCasoDeUso
    {
        public Task ExecuteAsync(TarefaCriadaEvento idtarefa);
    }
}

