namespace Application.Funcionalidades.Lembretes.Dtos
{
    public class LembreteMensagemRequisicao
    {
        public Guid IdLembrete { get; set; }
        public int IdTarefa { get; set; }
        public string Texto { get; set; } = string.Empty;
    }
}

