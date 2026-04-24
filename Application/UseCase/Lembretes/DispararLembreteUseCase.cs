using Application.Interfaces.UseCases.Lembretes;
using Microsoft.EntityFrameworkCore;
using Repository.Repositorys.LembreteRep;
using Repository.Repositorys.ParamGeralRep;

namespace Application.UseCase.Lembretes
{
    public class DispararLembreteUseCase : IDispararLembreteUseCase
    {
        private readonly IRepParamGeral _repParamGeral;
        private readonly IRepLembrete _repRepLembrete;
        private readonly IEnviarLembretePorEmailUseCase _enviarLembretePorEmailUseCase;
        public DispararLembreteUseCase
        ( 
            IRepParamGeral repParamGeral, 
            IRepLembrete repRepLembrete, 
            IEnviarLembretePorEmailUseCase enviarLembretePorEmailUseCase
        )
        {
           _repParamGeral = repParamGeral;
           _repRepLembrete = repRepLembrete;
            _enviarLembretePorEmailUseCase = enviarLembretePorEmailUseCase;
        }
        public async Task Execute(int id)
        {
            var lembrete = await _repRepLembrete.AsQueryable().Where(l => l.Id == id).FirstOrDefaultAsync();

            var paramGeral = await _repParamGeral.AsQueryable().FirstOrDefaultAsync();

            if (paramGeral.ReceberNotificacaoPorEmail)
                await _enviarLembretePorEmailUseCase.ExecuteAsync(lembrete, paramGeral.Email);

            //if (paramGeral.ReceberNotificacaoPorWhatsApp)
            //    await _enviarLembretePorEmailUseCase.ExecuteAsync(lembrete);
        }
    }
}
