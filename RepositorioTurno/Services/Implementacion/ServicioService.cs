using RepositorioTurno.Entities;
using RepositorioTurno.Repositories.Implementacion;
using RepositorioTurno.Repositories.Interfaces;
using RepositorioTurno.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositorioTurno.Services.Implementacion
{
    public class ServicioService : IServicoService
    {
        private readonly IServicioRepository _servicioRepository;

        public ServicioService()
        {
            _servicioRepository = new ServicioRepository();
        }
        public bool Create(Servicio servicio)
        {
            return _servicioRepository.Create(servicio);
        }

        public bool Delete(Servicio servicio)
        {
            return _servicioRepository.Delete(servicio);
        }

        public List<Servicio> GetAll()
        {
            return _servicioRepository.GetAll();
        }

        public List<Servicio> GetByFilter(int costo, string enPromocion)
        {
            return _servicioRepository.GetByFilter(costo, enPromocion);
        }

        public Servicio GetById(int id)
        {
            return _servicioRepository.GetById(id);
        }

        public bool Update(Servicio servicio)
        {
            return _servicioRepository.Update(servicio);
        }
    }
}
