using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Mensagens
{
    public class MensagemDeLembrete
    {
        public Guid CodigoLembrete { get; set; }
        public Guid CodigoToDoItem { get; set; }
        public string Texto { get; set; } = string.Empty;
    }
}
