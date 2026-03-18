using Application.Dtos.FiltroDtos;
using Application.Dtos.Tarefas;
using Application.Utils.Paginacao;
using Application.Views;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Application.Services.ServTarefas
{
    public interface IServTarefa
    {
        Task<TarefaResponse> AdicionarTarefa(CreateTarefaRequest dto);
        Task AtualizarTarefa(int id, UpdateTarefaRequest dto);
        Task RemoverTarefa(int id);
        Task<PaginacaoHelper<Tarefa>> ListarTarefas(TarefaFiltroRequest parametros);
        Task<PaginacaoHelper<Tarefa>> RecuperarTarefasVencidas(int pagina, int quantidade);
        Task AtualizarPrioridade(int id, UpdatePrioridadeTarefaRequest dto);
        Task<TarefaResponse> ObterPorId(int id);
    }
}
