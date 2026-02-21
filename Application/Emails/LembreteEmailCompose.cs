using Application.Dtos.LembreteDtos;
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
                Body = $"Olá! A tarefa está perto de vencer!"
            };
        }
    }
}
