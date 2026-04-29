using Application.Funcionalidades.Tarefas.Dtos;
using Application.Funcionalidades.Tarefas.Eventos;
using Application.Interfaces.Context;
using Application.Funcionalidades.Tarefas.Contratos.CasosDeUso;
using Application.Funcionalidades.Tarefas.Mapeadores;
using Application.Utils.Transacao;
using Domain.Enumeradores;
using Domain.Excecoes;
using Application.Interfaces.Messaging;
using Microsoft.AspNetCore.Http;
using Repository.Repositorios.Tarefas;

public class AdicionarTarefaCasoDeUso : IAdicionarTarefaCasoDeUso
{
    private readonly IRepTarefa _rep;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRabbitEventPublisher _publisher;
    private readonly IUsuarioContexto _usuarioContexto;

    public AdicionarTarefaCasoDeUso(
        IRepTarefa rep,
        IUnitOfWork unitOfWork,
        IRabbitEventPublisher publisher,
        IUsuarioContexto usuarioContexto)
    {
        _rep = rep;
        _unitOfWork = unitOfWork;
        _publisher = publisher;
        _usuarioContexto = usuarioContexto;
    }

    public async Task<TarefaResposta> Executar(CriarTarefaRequisicao dto)
    {
        if (!_usuarioContexto.IdUsuario.HasValue)
            throw new ExcecaoAplicacao(
                EnumCodigosDeExcecao.UsuarioNaoAutenticado,
                "Usuario autenticado nao identificado.",
                StatusCodes.Status401Unauthorized);

        await _unitOfWork.BeginTransactionAsync();

        var tarefa = MapeadorTarefa.ToTarefa(dto);
        tarefa.VincularUsuario(_usuarioContexto.IdUsuario.Value);

        _rep.Adicionar(tarefa);

        await _unitOfWork.CommitTransactionAsync();

        await _publisher.PublishAsync(new TarefaCriadaEvento(tarefa.Id));

        return new TarefaResposta { Id = tarefa.Id };
    }
}



