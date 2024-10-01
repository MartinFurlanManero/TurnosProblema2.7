using RepositorioTurno.Entities;
using RepositorioTurno.Repositories.Implementacion;
using RepositorioTurno.Repositories.Interfaces;
using RepositorioTurno.Services.Interfaces;

namespace RepositorioTurno.Services.Implementacion
{
    public class TurnoService : ITurnoService
    {
        private readonly ITurnoRepository _turnoRepository;
        public TurnoService()
        {
            _turnoRepository = new TurnoRepositorio();
        }
        public int ContarTurnos(string fecha, string hora)
        {
            return _turnoRepository.ContarTurnos(fecha, hora);
        }

        public bool InsertarMaestroDetalle(Turno turno)
        {
            return _turnoRepository.InsertarMaestroDetalle(turno);
        }

        public List<Servicio> ObtenerServicios()
        {
            return _turnoRepository.ObtenerServicios();
        }
    }
}
