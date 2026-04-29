using Application.Funcionalidades.Notificacoes.Dtos;
using Application.Funcionalidades.Notificacoes.Contratos.CasosDeUso;
using Application.Utils.Transacao;
using Domain.Entidades;
using Repository.Repositorios.Notificacoes;

namespace Application.Funcionalidades.Notificacoes.CasosDeUso
{
    public class CriarNotificacaoCasoDeUso : ICriarNotificacaoCasoDeUso
    {
        private readonly IRepNotificacao _repNotificacao;
        private readonly IUnitOfWork _unitOfWork;

        public CriarNotificacaoCasoDeUso(IRepNotificacao repNotificacao, IUnitOfWork unitOfWork)
        {
            _repNotificacao = repNotificacao;
            _unitOfWork = unitOfWork;
        }

        public async Task<Notificacao> ExecuteAsync(CriarNotificacaoRequisicao dto)
        {
            var notificacao = Notificacao.Criar(
                dto.CodigoUsuario,
                dto.Tipo,
                dto.Titulo,
                dto.Mensagem);

            await _unitOfWork.BeginTransactionAsync();
            _repNotificacao.Adicionar(notificacao);
            await _unitOfWork.CommitTransactionAsync();

            return notificacao;
        }
    }
}



