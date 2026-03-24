using Application.Dtos.Usuarios;
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
        public async Task<IActionResult> RegistrarUsuario([FromBody] RegistrarUsuarioRequest dto)
        {
            var result = await _registrarUsuarioUseCase.Executar(dto);

            return Created($"/usuarios/{result.id}", new { result.id });
        }
    }
}
