using Application.Dtos.Tarefas;
using Application.Events.Tarefas;
using Application.Interfaces.Context;
using Application.Interfaces.UseCases.Tarefas;
using Application.Mappers;
using Application.Utils.Transacao;
using Infra.Messaging.RabbitMQ.Publicadores;
using Repository.TarefaRep;

public class AdicionarTarefaUseCase : IAdicionarTarefaUseCase
{
    private readonly IRepTarefa _rep;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRabbitEventPublisher _publisher;
    private readonly IUsuarioContexto _usuarioContexto;

    public AdicionarTarefaUseCase(
        IRepTarefa rep,
        IUnitOfWork unitOfWork,
        IRabbitEventPublisher publisher,
        IUsuarioContexto usuarioContexto)
    {
        _rep = rep;
        _unitOfWork = unitOfWork;
        _publisher = publisher;
        _usuarioContexto = usuarioContexto;
    }

    public async Task<TarefaResponse> Executar(CreateTarefaRequest dto)
    {
        await _unitOfWork.BeginTransactionAsync();

        var tarefa = MapTarefa.ToTarefa(dto);
        tarefa.CodigoUsuario = _usuarioContexto.IdUsuario;

        _rep.Adicionar(tarefa);

        await _unitOfWork.CommitTransactionAsync();

        await _publisher.PublishAsync(new TarefaCriadaEvent(tarefa.Id));

        return new TarefaResponse { Id = tarefa.Id };
    }
}
