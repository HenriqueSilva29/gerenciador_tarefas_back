using Application.Dtos.LembreteDtos;
using Application.Emails;
using Application.Interfaces.Email;
using Application.Utils.Transacao;
using Microsoft.Extensions.Logging;
using Repository.Repositorys.LembreteRep;

namespace Application.UseCase.Lembrete
{
    public class EnviarLembretePorEmailUseCase
    {
        private readonly ILogger _logger;
        private readonly LembreteEmailCompose _lembreteEmailCompose;
        private readonly IEmailSender _emailSender;


        public EnviarLembretePorEmailUseCase
        (   
            ILogger logger, 
            LembreteEmailCompose lembreteEmailCompose,
            IEmailSender emailSender
        )
        {
            _logger = logger;
            _lembreteEmailCompose = lembreteEmailCompose;
            _emailSender = emailSender;
        }

        public async Task ExecuteAsync(LembreteMensagemDto message)
        {

            _logger.LogInformation("Iniciando montagem do email");

            var email =  _lembreteEmailCompose.Compose(message);

            await _emailSender.SendAsync(email);

            _logger.LogInformation("Email enviado");

        }
    }
}
