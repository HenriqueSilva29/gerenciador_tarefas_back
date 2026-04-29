using Application.Funcionalidades.ParamGerais.Dtos;
using Domain.Entidades;

namespace Application.Funcionalidades.ParamGerais.Servicos
{
    public interface IServicoParamGeral
    {
        Task<ParamGeral> Obter();
        Task Atualizar(AtualizarParamGeralRequisicao dto);
    }
}


