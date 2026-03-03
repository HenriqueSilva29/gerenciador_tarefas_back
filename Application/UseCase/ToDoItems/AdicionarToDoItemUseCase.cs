using Application.Dtos.ToDoItemDtos;
using Application.Interfaces.UseCases.Lembretes;
using Application.Interfaces.UseCases.ToDoItems;
using Application.Mappers;
using Application.Utils.Transacao;
using Repository.ToDoItemRep;

public class AdicionarToDoItemUseCase : IAdicionarToDoItemUseCase
{
    private readonly IRepToDoItem _rep;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICriarLembreteUseCase _criarLembrete;

    public AdicionarToDoItemUseCase(
        IRepToDoItem rep,
        IUnitOfWork unitOfWork,
        ICriarLembreteUseCase criarLembrete)
    {
        _rep = rep;
        _unitOfWork = unitOfWork;
        _criarLembrete = criarLembrete;
    }

    public async Task Executar(AdicionarToDoItemDto dto)
    {
        await _unitOfWork.BeginTransactionAsync();

        var toDoItem = MapToDoItem.AdicionarToDoItemDto(dto);

        _rep.Adicionar(toDoItem);

        if (dto.AvisarVencimento)
        {
            await _criarLembrete.CriarLembrete(
                toDoItem.Id,
                dto.DataVencimento,
                dto.DiasAntesDoVencimento
            );
        }

        await _unitOfWork.CommitTransactionAsync();


    }
}