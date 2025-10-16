using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ToDoItem
{
    public class ToDoItem
    {
        public int idToDoItem { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public bool Concluido { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataVencimento { get; set; }
        public int Prioridade { get; set; }
        public string Categoria { get; set; }
    }
}
