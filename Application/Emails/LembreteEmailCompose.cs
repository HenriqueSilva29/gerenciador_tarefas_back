using Application.Dtos.LembreteDtos;
using Domain.Entities;

namespace Application.Emails
{
    public class LembreteEmailCompose
    {
        public EmailMessage Compose(Lembrete entity, string emailDestinatario)
        {
            return new EmailMessage
            {
                To = emailDestinatario,
                Subject = $"Lembrete: {entity.Descricao}",
                Body = $"""
                <p>Sua tarefa está próxima do horário previsto.</p>
                <p><strong>Descrição:</strong> {entity.Descricao}</p>
                <p><strong>Disparo:</strong> {entity.DataDisparo:dd/MM/yyyy HH:mm}</p>
                """,
                IsHtml = true
            };
        }
    }
}
