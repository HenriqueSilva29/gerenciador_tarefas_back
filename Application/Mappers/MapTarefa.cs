using Application.Dtos.TarefaDtos;
using Application.Views;
using Domain.Common.ValueObjects;
using Domain.Entities;

namespace Application.Mappers
{
    public static class MapTarefa
    {
        public static Tarefa Mapear(Tarefa Tarefa, AdicionarTarefaDto dto)
        {
            Tarefa.Titulo = dto.Titulo;
            Tarefa.Descricao = dto.Descricao;
            Tarefa.DataCriacao = UtcDateTime.Now();
            Tarefa.DataVencimento = dto.DataVencimento;
            Tarefa.Status = dto.Status;
            Tarefa.Categoria = dto.Categoria;
            Tarefa.Prioridade = dto.Prioridade;

            return Tarefa;
        }

        public static Tarefa ToTarefa(AdicionarTarefaDto dto)
        {
            return new Tarefa
            {
                Titulo = dto.Titulo,
                Descricao = dto.Descricao,
                DataCriacao = UtcDateTime.Now(),
                DataVencimento = dto.DataVencimento,
                Prioridade = dto.Prioridade,
                Categoria = dto.Categoria
            };
        }

        public static Tarefa AtualizarTarefaDto(Tarefa Tarefa, AtualizarTarefaDto dto)
        {
            Tarefa.Titulo = dto.Titulo;
            Tarefa.Descricao = dto.Descricao;
            Tarefa.DataVencimento = dto.DataVencimento;
            Tarefa.Prioridade = dto.Prioridade;
            Tarefa.Categoria = dto.Categoria;
            Tarefa.Status = dto.Status;
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
                DataVencimento = Tarefa.DataVencimento,
                Status = Tarefa.Status,
                Prioridade = Tarefa.Prioridade,
                Categoria = Tarefa.Categoria,
                CodigoTarefaPai = Tarefa.CodigoTarefaPai,
                SubTarefas = Tarefa.SubTarefas?.Select(st => st.Id).ToList()
            };
        }
    }
}
