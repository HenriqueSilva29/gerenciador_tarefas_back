namespace Application.Interfaces.Context
{
    public interface IUsuarioContexto
    {
        int? IdUsuario { get; }
        string? Nome { get; }
        bool EstaAutenticado { get; }
    }
}
