using Application.Funcionalidades.Tarefas.Dtos;
using Application.Funcionalidades.Tarefas.Visoes;
using Domain.Comum.ObjetosDeValor;
using Domain.Entidades;

namespace Application.Funcionalidades.Tarefas.Mapeadores
{
    public static class MapeadorTarefa
    {
        public static Tarefa Mapear(Tarefa Tarefa, CriarTarefaRequisicao dto)
        {
            Tarefa.Titulo = dto.Titulo;
            Tarefa.Descricao = dto.Descricao;
            Tarefa.DataCriacao = UtcDateTime.Now();
            Tarefa.DataTarefa= dto.DataTarefa;
            Tarefa.HoraInicio = dto.HoraInicio;
            Tarefa.HoraFim = dto.HoraFim;
            Tarefa.Categoria = dto.Categoria;
            Tarefa.Prioridade = dto.Prioridade;

            return Tarefa;
        }

        public static Tarefa ToTarefa(CriarTarefaRequisicao dto)
        {
            return new Tarefa
            {
                Titulo = dto.Titulo,
                Descricao = dto.Descricao,
                DataCriacao = UtcDateTime.Now(),
                DataTarefa = dto.DataTarefa,
                HoraInicio = dto.HoraInicio,
                HoraFim = dto.HoraFim,
                Prioridade = dto.Prioridade,
                Categoria = dto.Categoria
            };
        }

        public static Tarefa AtualizarTarefaDto(Tarefa Tarefa, AtualizarTarefaRequisicao dto)
        {
            Tarefa.Titulo = dto.Titulo;
            Tarefa.Descricao = dto.Descricao;
            Tarefa.DataTarefa= dto.DataTarefa;
            Tarefa.HoraInicio = dto.HoraInicio;
            Tarefa.HoraFim = dto.HoraFim;
            Tarefa.Prioridade = dto.Prioridade;
            Tarefa.Categoria = dto.Categoria;
            Tarefa.AtualizarStatus(dto.Status);

            return Tarefa;
        }

        public static TarefaView MapearParaView(Tarefa Tarefa)
        {
            return new TarefaView
            {
                CodigoTarefa = Tarefa.Id,
                Titulo = Tarefa.Titulo,
                Descricao = Tarefa.Descricao,
                DataCriacao = Tarefa.DataCriacao,
                Status = Tarefa.Status,
                Prioridade = Tarefa.Prioridade,
                Categoria = Tarefa.Categoria,
                CodigoTarefaPai = Tarefa.CodigoTarefaPai,
                SubTarefas = Tarefa.SubTarefas?.Select(st => st.Id).ToList()
            };
        }
    }
}


