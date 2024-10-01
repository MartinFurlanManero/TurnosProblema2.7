using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepositorioTurno.Entities;

namespace RepositorioTurno.Repositories.Interfaces
{
    public interface IServicioRepository
    {
        List<Servicio> GetAll();    
        List<Servicio> GetByFilter(int costo, string enPromocion);
        Servicio GetById(int id);
        bool Create(Servicio servicio);
        bool Update(Servicio servicio);
        bool Delete(Servicio servicio);

    }
}
