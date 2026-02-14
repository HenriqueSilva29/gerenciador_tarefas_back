using Application.Dtos.LembreteDtos;
using Domain.Entities.Lembretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Application.Emails
{
    public class LembreteEmailCompose
    {
        public EmailMessage Compose(LembreteMensagemDto dto)
        {
             return new EmailMessage
            {
                To = "Não existe ainda",
                Subject = $"Lembrete: {dto.Texto}",
                Body = $"Olá! Você tem um lembrete agendado!"
            };
        }
    }
}
