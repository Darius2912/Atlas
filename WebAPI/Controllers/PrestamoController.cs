using AppCore;
using Entities_DTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PrestamoController : ControllerBase
    {
        private readonly PrestamoManager _prestamoManager;

        public PrestamoController(PrestamoManager prestamoManager)
        {
            _prestamoManager = prestamoManager;
        }

        // CREATE → POST /api/prestamo
        [HttpPost]
        public IActionResult Create([FromBody] Prestamo prestamo)
        {
            try
            {
                _prestamoManager.Create(prestamo);
                return CreatedAtAction(nameof(GetById), new { id = prestamo.Id }, prestamo);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        // UPDATE → PUT /api/prestamo/{id}
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Prestamo prestamo)
        {
            if (id != prestamo.Id) return BadRequest(new { error = "El ID no coincide." });

            try
            {
                _prestamoManager.Update(prestamo);
                return Ok(prestamo);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        // DELETE → DELETE /api/prestamo/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var prestamo = _prestamoManager.RetrieveById(id);
                if (prestamo == null) return NotFound(new { message = "Préstamo no encontrado." });

                _prestamoManager.Delete(prestamo);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        // READ ALL → GET /api/prestamo
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var prestamos = _prestamoManager.RetrieveAll();
                return Ok(prestamos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        // READ BY ID → GET /api/prestamo/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var prestamo = _prestamoManager.RetrieveById(id);
                if (prestamo == null) return NotFound(new { message = "Préstamo no encontrado." });
                return Ok(prestamo);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}
