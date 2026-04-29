using Application.Funcionalidades.Tarefas.Dtos.Subtarefas;
using Application.Funcionalidades.Tarefas.Eventos;
using Application.Funcionalidades.Tarefas.Contratos.CasosDeUso.Subtarefas;
using Application.Funcionalidades.Tarefas.Mapeadores;
using Application.Funcionalidades.UsuarioAutenticado.Servicos;
using Application.Utils.Transacao;
using Domain.Enumeradores;
using Domain.Excecoes;
using Application.Interfaces.Messaging;
using Microsoft.AspNetCore.Http;
using Repository.Repositorios.Tarefas;

namespace Application.Funcionalidades.Tarefas.CasosDeUso.Subtarefa
{
    public class AdicionarSubtarefaCasoDeUso : IAdicionarSubtarefaCasoDeUso
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepTarefa _rep;
        private readonly IRabbitEventPublisher _publisher;
        private readonly IServicoUsuarioAutenticado _servUsuarioAutenticado;

        public AdicionarSubtarefaCasoDeUso(
            IUnitOfWork unitOfWork,
            IRepTarefa rep,
            IRabbitEventPublisher publisher,
            IServicoUsuarioAutenticado servUsuarioAutenticado)
        {
            _unitOfWork = unitOfWork;
            _rep = rep;
            _publisher = publisher;
            _servUsuarioAutenticado = servUsuarioAutenticado;
        }

        public async Task<SubtarefaResposta> Executar(AdicionarSubtarefaRequisicao dto)
        {
            var idUsuario = _servUsuarioAutenticado.ObterIdUsuarioLogado();

            if (dto.CodigoTarefaPai.HasValue)
            {
                var tarefaPaiExiste = await _rep.ExistePorIdDoUsuarioAsync(dto.CodigoTarefaPai.Value, idUsuario);

                if (!tarefaPaiExiste)
                    throw new ExcecaoAplicacao(
                        EnumCodigosDeExcecao.RegistroNaoEncontrado,
                        "Tarefa pai nao encontrada no banco de dados",
                        StatusCodes.Status404NotFound);
            }

            var tarefa = MapeadorSubtarefa.ParaTarefa(dto);
            tarefa.VincularUsuario(idUsuario);

            await _unitOfWork.BeginTransactionAsync();
            _rep.Adicionar(tarefa);
            await _unitOfWork.CommitTransactionAsync();

            await _publisher.PublishAsync(new TarefaCriadaEvento(tarefa.Id));

            return new SubtarefaResposta { Id = tarefa.Id };
        }
    }
}



