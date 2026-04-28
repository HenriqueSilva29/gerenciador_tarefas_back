using Application.Interfaces.Context;
using System.Security.Claims;

namespace API.Context
{
    public class UsuarioContexto : IUsuarioContexto
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UsuarioContexto(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public bool EstaAutenticado =>
            _httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;

        public int? IdUsuario
        {
            get
            {
                var value = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (int.TryParse(value, out var id))
                    return id;

                return null;
            }
        }

        public string? Nome =>
            _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value;
    }
}
