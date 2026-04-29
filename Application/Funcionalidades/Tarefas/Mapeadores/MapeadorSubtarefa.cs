using Application.Funcionalidades.Tarefas.Dtos.Subtarefas;
using Domain.Comum.ObjetosDeValor;
using Domain.Entidades;

namespace Application.Funcionalidades.Tarefas.Mapeadores
{
    public static class MapeadorSubtarefa
    {
        public static Tarefa ParaTarefa(AdicionarSubtarefaRequisicao dto)
        {
            var Tarefa = new Tarefa()
            {
                Titulo = dto.Titulo,
                Descricao = dto.Descricao,
                DataCriacao = UtcDateTime.Now(),
                DataTarefa = dto.DataTarefa,
                HoraInicio = dto.HoraInicio,
                HoraFim = dto.HoraFim,
                Prioridade = dto.Prioridade,
                Categoria = dto.Categoria,
                CodigoTarefaPai = dto.CodigoTarefaPai,
            };

            return Tarefa;
        }
    }
}


