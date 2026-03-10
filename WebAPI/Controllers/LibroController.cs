using AppCore;
using Entities_DTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LibroController : ControllerBase
    {
        private readonly LibroManager _libroManager;

        public LibroController(LibroManager libroManager)
        {
            _libroManager = libroManager;
        }

        // CREATE → POST /api/libro
        [HttpPost]
        public IActionResult Create([FromBody] Libro libro)
        {
            try
            {
                _libroManager.Create(libro);
                return CreatedAtAction(nameof(GetById), new { id = libro.Id }, libro);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        // UPDATE → PUT /api/libro/{id}
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Libro libro)
        {
            if (id != libro.Id) return BadRequest(new { error = "El ID no coincide." });

            try
            {
                _libroManager.Update(libro);
                return Ok(libro);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        // DELETE → DELETE /api/libro/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var libro = _libroManager.RetrieveById(id);
                if (libro == null) return NotFound(new { message = "Libro no encontrado." });

                _libroManager.Delete(libro);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        // READ ALL → GET /api/libro
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var libros = _libroManager.RetrieveAll();
                return Ok(libros);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        // READ BY ID → GET /api/libro/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var libro = _libroManager.RetrieveById(id);
                if (libro == null) return NotFound(new { message = "Libro no encontrado." });
                return Ok(libro);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}
