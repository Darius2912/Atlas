using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
namespace Entities_DTOs
{
    public class BaseDTO
    {
        /*
         * clase base para los DTOs o Pojos
         */
        public int Id { get; set; }

        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}
