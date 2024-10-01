using RepositorioTurno.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositorioTurno.Repositories.Interfaces
{
    public interface ITurnoRepository
    {
        List<Servicio> ObtenerServicios();
        int ContarTurnos(string fecha, string hora);

        bool InsertarMaestroDetalle(Turno turno);
    }
}
