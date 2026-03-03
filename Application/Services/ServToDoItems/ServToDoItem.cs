using Application.Dtos.FiltroDtos;
using Application.Dtos.ToDoItemDtos;
using Application.Interfaces.UseCases.ToDoItems;
using Application.Services.ServToDoItems;
using Application.Utils.Paginacao;
using Domain.Entities;

namespace Application.Services.ToDoItemServices
{
    public class ServToDoItem : IServToDoItem
    {
        private readonly IAdicionarToDoItemUseCase _adicionarToDoItem;
        private readonly IAtualizarPrioridadeToDoItemUseCase _atualizarPrioridadeToDoItem;
        private readonly IAtualizarToDoItemUseCase _atualizarToDoItem;
        private readonly IRemoverToDoItemUseCase _removerToDoItem;
        private readonly IListarToDoItemUseCase _listarToDoItem;
        private readonly IListarToDoItemVencidosUseCase _listarToDoItemVencidos;

        public ServToDoItem(
            IAdicionarToDoItemUseCase adicionarToDoItem,
            IAtualizarPrioridadeToDoItemUseCase atualizarPrioridadeToDoItem,
            IRemoverToDoItemUseCase removerToDoItem) : base()
        {
            _adicionarToDoItem = adicionarToDoItem;
            _atualizarPrioridadeToDoItem = atualizarPrioridadeToDoItem;
            _removerToDoItem = removerToDoItem;
        }

        public async Task AdicionarTarefa(AdicionarToDoItemDto dto)
            => await _adicionarToDoItem.Executar(dto);

        public async Task AtualizarTarefa(int id, AtualizarToDoItemDto dto)
            => await _atualizarToDoItem.Executar(id,dto);

        public async Task AtualizarPrioridade(int id, AtualizarPrioridadeToDoItemDto dto)
            => await _atualizarPrioridadeToDoItem.Executar(id, dto);

        public async Task RemoverTarefa(int id)
            => await _removerToDoItem.Executar(id);
        
        public async Task<PaginacaoHelper<ToDoItem>> RecuperarTarefasVencidas(int pagina, int quantidade)
            => await _listarToDoItemVencidos.Executar(pagina, quantidade );

        public async Task<PaginacaoHelper<ToDoItem>> ListarTarefas(FiltroToDoItemDto parametros)
            => await _listarToDoItem.Executar(parametros);
    }
}
