using Application.Dtos.LembreteDtos;
using Application.Interfaces.Messaging;
using Application.Interfaces.UseCases;

public class EnviarLembretePorEmailMessageHandler
    : IMessageHandler<LembreteMensagemDto>
{
    public EnviarLembretePorEmailMessageHandler(IEnviarLembretePorEmailUseCase useCase)
    {
        _useCase = useCase;
    }

    private readonly IEnviarLembretePorEmailUseCase _useCase;

    public Task HandleAsync(LembreteMensagemDto message, CancellationToken cancellationToken)
        => _useCase.ExecuteAsync(message);
}