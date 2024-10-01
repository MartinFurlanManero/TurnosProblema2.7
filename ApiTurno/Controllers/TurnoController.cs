using Microsoft.AspNetCore.Mvc;
using RepositorioTurno.Entities;
using RepositorioTurno.Services.Implementacion;
using RepositorioTurno.Services.Interfaces;

namespace ApiTurno.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TurnoController : Controller
    {
        private readonly ITurnoService _turnoService;
        public TurnoController()
        {
            _turnoService = new TurnoService();
        }
        [HttpGet("servicios")]
        public IActionResult GetServicios()
        {
            return Ok(_turnoService.ObtenerServicios());
        }
        [HttpGet("contar-turnos")]
        public IActionResult CountTurnos([FromQuery] string fecha, [FromQuery] string hora)
        {
            return Ok(_turnoService.ContarTurnos(fecha, hora));
        }
        [HttpPost("crear-turno")]
        public IActionResult CrearTurno([FromBody] Turno turno)
        {
            DateTime fechaReserva = Convert.ToDateTime(turno.Fecha);
            DateTime fechaActual = DateTime.Now.Date.AddDays(1);
            if (fechaReserva <= fechaActual
                || fechaReserva > fechaActual.AddDays(45))
            {
                return BadRequest("La fecha reserva debe ser mayor al dia de hoy y no puede ser mayor a 45 dias.");
            }
            if (turno.detalles == null || turno.detalles.Count == 0)
            {
                return BadRequest("Debe ingresar al menos un detalle.");
            }
            var serviciosId = new List<int>();
            foreach (var item in turno.detalles)
            {
                if (serviciosId.Contains(item.ServicioId))
                {
                    return BadRequest("No se puede agregar dos veces el mismo servicio.");
                }
                serviciosId.Add(item.ServicioId);
            }
            var existeUnTurno = _turnoService.ContarTurnos(turno.Fecha, turno.Hora);
            if (existeUnTurno > 0)
            {
                return BadRequest("Ya existe un turno en esa fecha y hora.");
            }
            var result = _turnoService.InsertarMaestroDetalle(turno);
            if (result)
            {
                return Ok(new { Message = "Turno creado con exito." });
            }
            else
            {
                return BadRequest("Error al crear el turno, intente nuevamente.");
            }

        }
    }
}
