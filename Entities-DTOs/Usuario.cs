using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace Entities_DTOs
{
    public class Usuario : BaseDTO
    {

        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime BirthDate { get; set; }
        public int Id { get; set; }
        public string Status { get; set; }
        public string Rol { get; set; }

    }
}