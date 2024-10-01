using Microsoft.AspNetCore.Mvc;
using RepositorioTurno.Entities;
using RepositorioTurno.Repositories.Interfaces;
using RepositorioTurno.Services.Implementacion;
using RepositorioTurno.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiTurno.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiciosController : ControllerBase
    {
        private readonly IServicoService _services;

        public ServiciosController()
        {
            _services = new ServicioService();
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_services.GetAll());
            }
            catch (Exception)
            {
                return StatusCode(500, "Ha ocurrido un error interno");
            }
        }

        // GET api/<ServiciosController>/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                return Ok(_services.GetById(id));
            }
            catch (Exception)
            {
                return StatusCode(500, "Ha ocurrido un error interno");
            }
        }

        // POST api/<ServiciosController>
        [HttpPost]
        public IActionResult Post([FromBody] Servicio servicio)
        {
            try
            {
                return Ok(_services.Create(servicio));
            }
            catch (Exception)
            {
                return StatusCode(500, "Ha ocurrido un error interno");
            }
        }

        // PUT api/<ServiciosController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Servicio servicio)
        {
            try
            {
                if (id != servicio.Id)
                {
                    return BadRequest("El ID en la URL no coincide con el ID del servicio.");
                }

                bool isUpdated = _services.Update(servicio);

                if (isUpdated)
                {
                    return Ok("Servicio actualizado correctamente.");
                }
                else
                {
                    return StatusCode(500, "No se pudo actualizar el servicio.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ha ocurrido un error interno: {ex.Message}");
            }
        }


        // DELETE api/<ServiciosController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, Servicio servicio)
        {
            try
            {
                if (id != servicio.Id)
                {
                    return BadRequest("El ID en la URL no coincide con el ID del servicio.");
                }

                bool isDelete = _services.Delete(servicio);

                if (isDelete)
                {
                    return Ok("Servicio eliminado correctamente");
                }
                else
                {
                    return StatusCode(500, "No se pudo actualizar el servicio.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ha ocurrido un error interno: {ex.Message}");
            }
        }

        // GET: api/Servicios/filter
        [HttpGet("filter")]
        public IActionResult GetByFilter([FromQuery] int costo, [FromQuery] string enPromocion)
        {
            try
            {
                var servicios = _services.GetByFilter(costo, enPromocion);

                if (servicios == null || servicios.Count == 0)
                    return NotFound("No se encontraron servicios con los filtros proporcionados.");

                return Ok(servicios);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

    }
}
