using Application.Dtos.FiltroDtos;
using Application.Dtos.TarefaDtos;
using Application.Interfaces.UseCases.Tarefas;
using Application.Services.ServTarefas;
using Application.Utils.Paginacao;
using Domain.Entities;

namespace Application.Services.TarefaServices
{
    public class ServTarefa : IServTarefa
    {
        private readonly IAdicionarTarefaUseCase _adicionarTarefa;
        private readonly IAtualizarPrioridadeTarefaUseCase _atualizarPrioridadeTarefa;
        private readonly IAtualizarTarefaUseCase _atualizarTarefa;
        private readonly IRemoverTarefaUseCase _removerTarefa;
        private readonly IListarTarefaUseCase _listarTarefa;
        private readonly IListarTarefasVencidasUseCase _ListarTarefasVencidas;

        public ServTarefa(
            IAdicionarTarefaUseCase adicionarTarefa,
            IAtualizarPrioridadeTarefaUseCase atualizarPrioridadeTarefa,
            IRemoverTarefaUseCase removerTarefa,
            IListarTarefaUseCase listarTarefa,
            IListarTarefasVencidasUseCase ListarTarefasVencidas,
            IAtualizarTarefaUseCase atualizarTarefa) : base()
        {
            _adicionarTarefa = adicionarTarefa;
            _atualizarPrioridadeTarefa = atualizarPrioridadeTarefa;
            _removerTarefa = removerTarefa;
            _listarTarefa = listarTarefa;
            _ListarTarefasVencidas = ListarTarefasVencidas;
            _atualizarTarefa = atualizarTarefa;
        }

        public async Task AdicionarTarefa(AdicionarTarefaDto dto)
            => await _adicionarTarefa.Executar(dto);

        public async Task AtualizarTarefa(int id, AtualizarTarefaDto dto)
            => await _atualizarTarefa.Executar(id,dto);

        public async Task AtualizarPrioridade(int id, AtualizarPrioridadeTarefaDto dto)
            => await _atualizarPrioridadeTarefa.Executar(id, dto);

        public async Task RemoverTarefa(int id)
            => await _removerTarefa.Executar(id);
        
        public async Task<PaginacaoHelper<Tarefa>> RecuperarTarefasVencidas(int pagina, int quantidade)
            => await _ListarTarefasVencidas.Executar(pagina, quantidade );

        public async Task<PaginacaoHelper<Tarefa>> ListarTarefas(FiltroTarefaDto parametros)
            => await _listarTarefa.Executar(parametros);
    }
}
