using Application.Dtos.ParamGerals;

namespace Application.Interfaces.UseCases.ParamGerals
{
    public  interface IAtualizarParamGeralUseCase
    {
        Task ExecuteAsync(int id, UpdateParamGeralRequest dto);
    }
}
