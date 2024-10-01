using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositorioTurno.Entities
{
    public class DetalleTurno
    {
        public int ServicioId { get; set; }
        public string Observaciones { get; set; }
        public int IdTurno { get; set; }
    }
}
