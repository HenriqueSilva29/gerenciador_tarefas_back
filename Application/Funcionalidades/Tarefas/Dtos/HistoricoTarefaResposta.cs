
namespace Application.Funcionalidades.Tarefas.Dtos
{
     public class HistoricoTarefaResposta
    {
        public HistoricoTarefaResposta()
        {
            Itens = new List<HistoricoTarefaItemResponse>();
        }

        public List<HistoricoTarefaItemResponse> Itens { get; set; }
    }

    public class HistoricoTarefaItemResponse
    {
        public int IdAuditoria { get; set; }
        public DateTimeOffset DataOcorrencia { get; set; }
        public string AcaoExecutada { get; set; } = string.Empty;
        public string TipoEvento { get; set; } = string.Empty;
    }
}

