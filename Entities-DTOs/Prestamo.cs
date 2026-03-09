using System;
using System.Collections.Generic;
using System.Text;

namespace Entities_DTOs
{
    internal class Prestamo : BaseDTO
    {
        public string Isbn { get; set; }              // Identificador del libro
        public int UsuarioId { get; set; }            // Identificador del usuario (int para BD)

        public DateTime FechaPrestamo { get; set; }   // Inicio del préstamo
        public DateTime FechaLimite { get; set; }     // Fecha límite de devolución
        public DateTime? FechaDevolucion { get; set; } // Fecha real de devolución (nullable)
        public string Estado { get; set; }            // "prestado" / "devuelto"
    }
}
