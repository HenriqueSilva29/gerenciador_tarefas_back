using Application.Dtos.UsuarioDtos;
using Application.Interfaces.UseCases.Usuarios;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IRegistrarUsuarioUseCase _registrarUsuarioUseCase;
        public UsuarioController(IRegistrarUsuarioUseCase registrarUsuarioUseCase)
        {
            _registrarUsuarioUseCase = registrarUsuarioUseCase;
        }

        [HttpPost("registrar")]
        public async Task RegistrarUsuario(RegistrarUsuarioDto dto)
        {
            await _registrarUsuarioUseCase.Executar(dto);
        }
    }
}
