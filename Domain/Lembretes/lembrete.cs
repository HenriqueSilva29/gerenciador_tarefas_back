using Domain.ToDoItems;

namespace Domain.Lembretes
{
    public class Lembrete
    {
        public Guid CodigoLembrete { get; set; }
        public Guid CodigoToDoItem { get; set; }
        public ToDoItem ToDoItem { get; set; }
        public DateTime DataDoLembrete { get; set; }
        public string Texto { get; set; } = string.Empty;

        public bool FoiEnviado { get; private set; }
        public DateTime? DataDeEnvio { get; private set; }

        public void MarcarComoEnviado()
        {
            FoiEnviado = true;
            DataDeEnvio = DateTime.UtcNow;
        }
    }
}
