using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Mensageria.RabbitMQ.Publicadores
{
    public interface IPublicadorDeMensagens
    {
         Task PublicarAsync(object mensagem);
    }
}
