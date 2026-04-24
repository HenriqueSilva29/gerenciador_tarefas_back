
namespace Application.Dtos.Tarefas
{
     public class HistoricoTarefaResponse
    {
        public HistoricoTarefaResponse()
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
