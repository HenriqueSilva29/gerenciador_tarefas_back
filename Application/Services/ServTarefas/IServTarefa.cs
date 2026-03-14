using Application.Dtos.FiltroDtos;
using Application.Dtos.TarefaDtos;
using Application.Utils.Paginacao;
using Application.Views;
using Domain.Entities;

namespace Application.Services.ServTarefas
{
    public interface IServTarefa
    {
        Task AdicionarTarefa(AdicionarTarefaDto dto);
        Task AtualizarTarefa(int id, AtualizarTarefaDto dto);
        Task RemoverTarefa(int id);
        Task<PaginacaoHelper<Tarefa>> ListarTarefas(FiltroTarefaDto parametros);
        Task<PaginacaoHelper<Tarefa>> RecuperarTarefasVencidas(int pagina, int quantidade);
        Task AtualizarPrioridade(int id, AtualizarPrioridadeTarefaDto dto);
    }
}
