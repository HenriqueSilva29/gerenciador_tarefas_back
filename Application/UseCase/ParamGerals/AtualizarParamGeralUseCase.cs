using Application.Dtos.ParamGerals;
using Application.Interfaces.UseCases.ParamGerals;
using Application.Mappers;
using Application.Utils.Transacao;
using Repository.Repositorys.ParamGeralRep;

namespace Application.UseCase.ParamGerals
{
    public class AtualizarParamGeralUseCase : IAtualizarParamGeralUseCase
    {
        private readonly IRepParamGeral _rep;
        private readonly IUnitOfWork _unitOfWork;

        public AtualizarParamGeralUseCase(IRepParamGeral rep, IUnitOfWork unitOfWork)
        {
            _rep = rep;
            _unitOfWork = unitOfWork;
        }
        public async Task ExecuteAsync(int id, UpdateParamGeralRequest dto)
        {
            await _unitOfWork.BeginTransactionAsync();

            var paramGeral = await _rep.RecuperarPorIdAsync(id);

            paramGeral = ParamGeralMapper.AtualizarEntidade(paramGeral, dto);

            _rep.Atualizar(paramGeral);

            await _unitOfWork.CommitTransactionAsync();

        }
    }
}
