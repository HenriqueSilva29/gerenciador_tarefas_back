using Application.Funcionalidades.Notificacoes.Dtos;
using Domain.Entidades;

namespace Application.Funcionalidades.Notificacoes.Contratos.CasosDeUso
{
    public interface ICriarNotificacaoCasoDeUso
    {
        Task<Notificacao> ExecuteAsync(CriarNotificacaoRequisicao dto);
    }
}


