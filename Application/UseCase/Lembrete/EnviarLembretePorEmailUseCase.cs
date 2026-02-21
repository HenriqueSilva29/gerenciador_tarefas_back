using Application.Dtos.LembreteDtos;
using Application.Emails;
using Application.Interfaces.Email;
using Application.Interfaces.UseCases;
using Microsoft.Extensions.Logging;

namespace Application.UseCase.Lembrete
{
    public class EnviarLembretePorEmailUseCase : IEnviarLembretePorEmailUseCase
    {
        private readonly ILogger<EnviarLembretePorEmailUseCase> _logger;
        private readonly LembreteEmailCompose _lembreteEmailCompose;
        private readonly IEmail _email;


        public EnviarLembretePorEmailUseCase
        (   
            ILogger<EnviarLembretePorEmailUseCase> logger, 
            LembreteEmailCompose lembreteEmailCompose,
            IEmail email
        )
        {
            _logger = logger;
            _lembreteEmailCompose = lembreteEmailCompose;
            _email = email;
        }

        public async Task ExecuteAsync(LembreteMensagemDto message)
        {

            _logger.LogInformation("Iniciando montagem do email");

            var email =  _lembreteEmailCompose.Compose(message);

            await _email.SendAsync(email);

            _logger.LogInformation("Email enviado");

        }
    }
}
