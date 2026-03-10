using AppCore;
using Entities_DTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioManager _usuarioManager;

        public UsuarioController(UsuarioManager usuarioManager)
        {
            _usuarioManager = usuarioManager;
        }

        // CREATE → POST /api/usuario
        [HttpPost]
        public IActionResult Create([FromBody] Usuario usuario)
        {
            try
            {
                _usuarioManager.Create(usuario);
                return CreatedAtAction(nameof(GetById), new { id = usuario.Id }, usuario);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        // READ ALL → GET /api/usuario
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var usuarios = _usuarioManager.RetrieveAll();
                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        // READ BY ID → GET /api/usuario/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var usuario = _usuarioManager.RetrieveById(id);
                if (usuario == null) return NotFound(new { message = "Usuario no encontrado" });
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        // UPDATE → PUT /api/usuario/{id}
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Usuario usuario)
        {
            if (id != usuario.Id) return BadRequest(new { error = "El ID no coincide" });

            try
            {
                _usuarioManager.Update(usuario);
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        // DELETE → DELETE /api/usuario/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var usuario = _usuarioManager.RetrieveById(id);
                if (usuario == null) return NotFound(new { message = "Usuario no encontrado" });

                _usuarioManager.Delete(usuario);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}
