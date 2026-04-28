using Application.Dtos.Usuarios;
using Application.Interfaces.UseCases.Usuarios;
using Application.Services.ServUsuarios;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IServUsuario _aplic;
        public UsuarioController(IServUsuario aplic)
        {
            _aplic = aplic;
        }

        [HttpPost("registrar")]
        public  async Task<ActionResult> RegistrarUsuario([FromBody] RegistrarUsuarioRequest dto)
        {
            var result =  await _aplic.RegistrarUsuario(dto);

            return Created($"/usuarios/{result.id}", new { result.id });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> AtualizarNomeUsuario([FromRoute] int id, [FromBody] AtualizarNomeUsuarioRequest dto)
        {
            await _aplic.AtualizarNomeUsuario(id, dto);

            return NoContent();
        }
    }
}
