using Application.Dtos.LembreteDtos;
using Application.Interfaces.Messaging;
using Application.Interfaces.UseCases;
using Application.UseCase.Lembrete;

namespace Application.Massaging.MessageHandlers
{
    public class EnviarLembretePorEmailMessageHandler : IMessageHandler<LembreteMensagemDto>
    {
        private readonly IEnviarLembretePorEmailUseCase _useCase;

        public EnviarLembretePorEmailMessageHandler(IEnviarLembretePorEmailUseCase useCase)
        {
            _useCase = useCase;
        }

        public async Task HandleAsync
        (
            LembreteMensagemDto message,
            CancellationToken cancellationToken
        )
        {
            await _useCase.ExecuteAsync(message);
        }

    }
}
