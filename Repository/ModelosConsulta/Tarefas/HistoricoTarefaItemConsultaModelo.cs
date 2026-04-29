namespace Repository.ModelosConsulta.Tarefas
{
    public class HistoricoTarefaItemConsultaModelo
    {
        public int IdAuditoria { get; set; }
        public int IdTarefaPrincipal { get; set; }
        public int IdTarefaRelacionada { get; set; }
        public string EscopoEvento { get; set; } = string.Empty;
        public string TipoEvento { get; set; } = string.Empty;
        public string Acao { get; set; } = string.Empty;
        public string? TituloRelacionado { get; set; }
        public string? IdUsuario { get; set; }
        public DateTimeOffset DataOcorrencia { get; set; }
        public string? Alteracoes { get; set; }
    }
}

