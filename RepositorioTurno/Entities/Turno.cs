using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositorioTurno.Entities
{
    public class Turno
    {
        public int Id { get; set; }
        public string Fecha { get; set; }
        public string Hora { get; set; }

        public string Cliente { get; set; }

        public List<DetalleTurno> detalles {get; set;}

        public Turno()
        {
            detalles = new List<DetalleTurno>();
        }

    }
}
