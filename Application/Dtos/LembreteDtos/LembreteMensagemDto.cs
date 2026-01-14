namespace Application.Dtos.LembreteDtos
{
    public class LembreteMensagemDto
    {
        public Guid IdLembrete { get; set; }
        public int IdTarefa { get; set; }
        public string Texto { get; set; } = string.Empty;
    }
}
