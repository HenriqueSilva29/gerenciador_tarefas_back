using Application.Dtos.Tarefas;
using Application.Interfaces.UseCases.Lembretes;
using Application.Interfaces.UseCases.Tarefas;
using Application.Mappers;
using Application.Utils.Transacao;
using Repository.TarefaRep;

public class AdicionarTarefaUseCase : IAdicionarTarefaUseCase
{
    private readonly IRepTarefa _rep;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICriarLembreteUseCase _criarLembrete;

    public AdicionarTarefaUseCase(
        IRepTarefa rep,
        IUnitOfWork unitOfWork,
        ICriarLembreteUseCase criarLembrete)
    {
        _rep = rep;
        _unitOfWork = unitOfWork;
        _criarLembrete = criarLembrete;
    }

    public async Task<TarefaResponse> Executar(CreateTarefaRequest dto)
    {
        await _unitOfWork.BeginTransactionAsync();

        var Tarefa = MapTarefa.ToTarefa(dto);

        _rep.Adicionar(Tarefa);

        if (dto.AvisarVencimento)
        {
             _criarLembrete.CriarLembrete(
                Tarefa,
                dto.DataVencimento,
                dto.DiasAntesDoVencimento
            );
        }

        await _unitOfWork.CommitTransactionAsync();

        return new TarefaResponse { Id = Tarefa.Id};

    }
}