using Domain.Entidades;

namespace Application.Funcionalidades.Lembretes.Contratos.CasosDeUso
{
    public interface IEnviarLembretePorEmailCasoDeUso
    {
        Task ExecuteAsync(Lembrete lembrete, ParamGeral paramGeral);
    }
}


