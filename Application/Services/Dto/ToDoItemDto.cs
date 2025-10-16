namespace Application.Services.Dto
{
    public class ToDoItemDto
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public bool Concluido { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataVencimento { get; set; }
        public int Prioridade { get; set; }
        public string Categoria { get; set; }
    }
}
