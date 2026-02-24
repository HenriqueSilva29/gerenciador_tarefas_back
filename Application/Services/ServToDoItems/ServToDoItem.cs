using Application.Dtos.FiltroDtos;
using Application.Dtos.ToDoItemDtos;
using Application.Interfaces.UseCases;
using Application.Mappers;
using Application.Services.ServToDoItems;
using Application.Utils.Filtro;
using Application.Utils.Ordenacao;
using Application.Utils.Paginacao;
using Application.Utils.Queryable;
using Application.Utils.Transacao;
using Domain.Common.ValueObjects;
using Domain.Entities.ToDoItems;
using Repository.ToDoItemRep;

namespace Application.Services.ToDoItemServices
{
    public class ServToDoItem : IServToDoItem
    {
        private readonly IRepToDoItem _rep;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICriarLembreteUseCase _criarLembrete;

        public ServToDoItem(IRepToDoItem rep,
                            IUnitOfWork unitOfWork,
                            ICriarLembreteUseCase criarLembrete
                            ) : base()
            {
                _rep = rep;
                _unitOfWork = unitOfWork;
                _criarLembrete = criarLembrete;
            }

            public async Task CriarTarefa(AdicionarToDoItemDto dto)
            {

                await _unitOfWork.BeginTransactionAsync();    

                var toDoItem = MapToDoItem.AdicionarToDoItemDto(dto);

                await _rep.Adicionar(toDoItem);

                await _unitOfWork.CommitTransactionAsync();

                if (dto.AvisarVencimento)
                {
                    await _criarLembrete.CriarLembrete(toDoItem.Id, dto.DataVencimento, dto.DiasAntesDoVencimento);
                }          

            }

            public async Task AtualizarTarefa(int id, AtualizarToDoItemDto dto)
            {
                await _unitOfWork.BeginTransactionAsync();

                var ToDoItem = await _rep.RecuperarPorId(id);

                if (ToDoItem is null) throw new Exception("Registro não encontrado");

                ToDoItem = MapToDoItem.AtualizarToDoItemDto(ToDoItem, dto);

                await _rep.Atualizar(ToDoItem);

                await _unitOfWork.CommitTransactionAsync();
            }

            public async Task RemoverTarefa(int id)
            {
                await _unitOfWork.BeginTransactionAsync();

                var toDoItem = await _rep.RecuperarPorId(id);

                if (toDoItem is null) throw new Exception("Registro não encontrado");

                await _rep.Remover(toDoItem);

                await _unitOfWork.CommitTransactionAsync();
            }
        
            public async Task<PaginacaoHelper<ToDoItem>> RecuperarTarefasVencidas(int pagina, int quantidade)
            {
                var agoraUtc = UtcDateTime.Now();

                var query = _rep.AsQueryable()
                                .Where(t => t.DataVencimento.Value < agoraUtc.Value);

                return await query.PaginarAsync(pagina,quantidade);
            }

            public async Task AtualizarPrioridade(int id, AtualizarPrioridadeDto dto)
            {
            await _unitOfWork.BeginTransactionAsync();

            var toDoItem = await _rep.RecuperarPorId(id);

                if (toDoItem == null)
                    throw new Exception("Tarefa não encontrada.");

                toDoItem.DefinirPrioridade(dto.Prioridade);

                await _rep.Atualizar(toDoItem);

                await _unitOfWork.CommitTransactionAsync();
            }

            public async Task<PaginacaoHelper<ToDoItem>> ListarTarefas(FiltroToDoItemDto parametros)
            {
                var query = _rep.AsQueryable();

                query = query.AplicarFiltros(parametros);
                query = query.AplicarOrdenacao(parametros);

                return await query.PaginarAsync(parametros.Pagina,parametros.QuantidadePorPagina);
            }

        }
    }
