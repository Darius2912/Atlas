using System;
using System.Collections.Generic;
using System.Text;

namespace Entities_DTOs
{
    public class Prestamo : BaseDTO
    {
        public string Isbn { get; set; }           
        public int UsuarioId { get; set; }      
        public DateTime FechaPrestamo { get; set; } 
        public DateTime FechaLimite { get; set; }     
        public DateTime? FechaDevolucion { get; set; } 
        public string Estado { get; set; }            
        public DateTime created { get; set; }
        
    }
}

