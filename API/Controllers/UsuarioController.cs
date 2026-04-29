using Application.Funcionalidades.Usuarios.Dtos;
using Application.Funcionalidades.Usuarios.Contratos.CasosDeUso;
using Application.Funcionalidades.Usuarios.Servicos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IServicoUsuario _aplic;
        public UsuarioController(IServicoUsuario aplic)
        {
            _aplic = aplic;
        }

        [HttpPost("registrar")]
        public  async Task<ActionResult> RegistrarUsuario([FromBody] RegistrarUsuarioRequisicao dto)
        {
            var result =  await _aplic.RegistrarUsuario(dto);

            return Created($"/usuarios/{result.id}", new { result.id });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> AtualizarNomeUsuario([FromRoute] int id, [FromBody] AtualizarNomeUsuarioRequisicao dto)
        {
            await _aplic.AtualizarNomeUsuario(id, dto);

            return NoContent();
        }
    }
}


