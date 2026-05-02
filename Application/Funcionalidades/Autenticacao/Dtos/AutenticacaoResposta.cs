namespace Application.Funcionalidades.Autenticacao.Dtos
{
    public class AutenticacaoResposta
    {
        public int Id { get; set; }
        public string Email { get; set; } = default!;
        public string Role { get; set; } = default!;
        public string? Nome { get; set; }
    }
}
