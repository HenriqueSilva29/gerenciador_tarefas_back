using Application.Emails;
using Application.Interfaces.Email;
using Application.Interfaces.UseCases.Lembretes;
using Microsoft.Extensions.Logging;
using Repository.Repositorys.LembreteRep;

namespace Application.UseCase.Lembretes
{
    public class EnviarLembretePorEmailUseCase : IEnviarLembretePorEmailUseCase
    {
        private readonly ILogger<EnviarLembretePorEmailUseCase> _logger;
        private readonly LembreteEmailCompose _lembreteEmailCompose;
        private readonly IEmail _email;
        private readonly IRepLembrete _repLembrete;

        public EnviarLembretePorEmailUseCase
        (   
            ILogger<EnviarLembretePorEmailUseCase> logger, 
            LembreteEmailCompose lembreteEmailCompose,
            IEmail email,
            IRepLembrete repLembrete
        )
        {
            _logger = logger;
            _lembreteEmailCompose = lembreteEmailCompose;
            _email = email;
            _repLembrete = repLembrete;
        }

        public async Task ExecuteAsync(int lembreteId)
        {
            var lembrete = await _repLembrete.RecuperarPorId(lembreteId);

            _logger.LogInformation("Iniciando montagem do email");

            var email =  _lembreteEmailCompose.Compose(lembrete);

            await _email.SendAsync(email);

            _logger.LogInformation("Email enviado");

        }
    }
}
