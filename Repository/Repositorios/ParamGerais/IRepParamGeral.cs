using Domain.Entidades;

namespace Repository.Repositorios.ParamGerais
{
    public interface IRepParamGeral : IRepositorio<ParamGeral, int>
    {
        public Task<ParamGeral?> ObterPorUsuarioAsync(int idUsuario);
    }
}


