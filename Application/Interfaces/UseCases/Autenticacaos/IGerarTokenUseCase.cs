
using Domain.Entities;

namespace Application.Interfaces.UseCases.Autenticacaos
{
    public interface IGerarTokenUseCase 
    {
        public string Executar(Usuario usuario);
    }
}
