using Application.Funcionalidades.Tarefas.Filtros;
using Application.Utils.Paginacao;
using Domain.Entidades;

namespace Application.Funcionalidades.Tarefas.Contratos.CasosDeUso
{
    public interface IListarTarefasCasoDeUso
    {
        public Task<PaginacaoHelper<Tarefa>> Executar(TarefaFiltroRequisicao parametros);
    }
}


