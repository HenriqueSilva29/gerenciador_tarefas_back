using Application.Dtos.LembreteDtos;
using Domain.Entities.Lembretes;

namespace Application.Emails
{
    public class LembreteEmailCompose
    {
        public EmailMessage Compose(Lembrete entity)
        {
             return new EmailMessage
            {
                To = "Não existe ainda",
                Subject = $"Lembrete: {entity.Texto}",
                Body = $"Olá! A tarefa está perto de vencer!"
            };
        }
    }
}
