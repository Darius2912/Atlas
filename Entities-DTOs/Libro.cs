using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace Entities_DTOs
{
    public class Libro : BaseDTO
    {

        public int Id { get; set; }
        public string Isbn { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public string Categoria { get; set; }
        public int Copias { get; set; }
        public int Disponibles { get; set; }    
    }
}