using RepositorioTurno.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositorioTurno.Services.Interfaces
{
    public interface IServicoService
    {
        List<Servicio> GetAll();
        List<Servicio> GetByFilter(int costo, string enPromocion);
        Servicio GetById(int id);
        bool Create(Servicio servicio);
        bool Update(Servicio servicio);
        bool Delete(Servicio servicio);
    }
}
