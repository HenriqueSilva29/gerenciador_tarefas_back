using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Messaging
{
    public class MessageEnvelope
    {
        public string Type { get; set; }  
        public string CorrelationId { get; set; } 
        public DateTime CreatedAt { get; set; }
        public string Payload { get; set; }   
    }
}
