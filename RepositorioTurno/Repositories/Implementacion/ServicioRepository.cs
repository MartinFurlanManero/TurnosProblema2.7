using RepositorioTurno.Entities;
using RepositorioTurno.Repositories.Interfaces;
using RepositorioTurno.Utils;
using System.Data;
using System.Data.SqlClient;

namespace RepositorioTurno.Repositories.Implementacion
{
    public class ServicioRepository : IServicioRepository
    {
        private readonly DataHelper _dataHelper;
        public ServicioRepository()
        {
            _dataHelper = DataHelper.GetInstance();
        }
        public bool Create(Servicio servicio)
        {
            bool result = true;
            SqlConnection? cnn = null;

            try
            {
                cnn = DataHelper.GetInstance().GetConnection();
                cnn.Open();

                var cmd = new SqlCommand("SP_INSERTAR_SERVICIO", cnn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@id", servicio.Id);
                cmd.Parameters.AddWithValue("@nombre", servicio.Nombre);
                cmd.Parameters.AddWithValue("@costo", servicio.Costo);
                cmd.Parameters.AddWithValue("@enPromocion", servicio.EnPromocion);

                cmd.ExecuteNonQuery();
            }
            catch (SqlException)
            {
                result = false;
            }
            finally
            {
                if (cnn != null && cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                }
            }
            return result;
        }

        public bool Delete(Servicio servicio)
        {
            bool result = true;
            SqlConnection? cnn = null;

            try
            {
                cnn = DataHelper.GetInstance().GetConnection();
                cnn.Open();  

                var cmd = new SqlCommand("SP_ELIMINAR_SERVICIO", cnn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@id", servicio.Id);

                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                result = false;
                Console.WriteLine("Error al eliminar el servicio: " + ex.Message);
                throw;  
            }
            finally
            {
                if (cnn != null && cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                }
            }
            return result;
        }


        public List<Servicio> GetAll()
        {
            var result = _dataHelper.ExecuteSPQuery("SP_CONSULTAR_SERVICIOS", null);
            var lstServicios = new List<Servicio>();
            foreach (DataRow row in result.Rows)
            {
                var servicio = new Servicio
                {
                    Id = Convert.ToInt32(row[0]),
                    Nombre = row[1].ToString(),
                    Costo = Convert.ToInt32(row[2]),
                    EnPromocion = row[3].ToString()
                };
                lstServicios.Add(servicio);
            }
            return lstServicios;
        }

        public Servicio GetById(int id)
        {
            var parameters = new List<ParameterSQL>();
            parameters.Add(new ParameterSQL("@id", id));

            // Pasa los parámetros correctos
            DataTable t = DataHelper.GetInstance().ExecuteSPQuery("SP_RECUPERAR_SERVICIO_POR_ID", parameters);

            if (t != null && t.Rows.Count == 1)
            {
                DataRow row = t.Rows[0];
                int cod = Convert.ToInt32(row["id"]);
                string nombre = Convert.ToString(row["nombre"]);
                int precio = (int)row["costo"];
                string promocion = Convert.ToString(row["enPromocion"]);

                Servicio servicio = new Servicio()
                {
                    Id = cod,
                    Nombre = nombre,
                    Costo = precio,
                    EnPromocion = promocion
                };
                return servicio;
            }
            return null;
        }


        public List<Servicio> GetByFilter(int costo, string enPromocion)
        {
            var parameters = new List<ParameterSQL>();
            parameters.Add(new ParameterSQL("@costo", costo));
            parameters.Add(new ParameterSQL("@enPromocion", enPromocion));

            DataTable t = DataHelper.GetInstance().ExecuteSPQuery("SP_RECUPERAR_SERVICIO_FILTRADO", parameters);

            if (t == null || t.Rows.Count == 0)
                return new List<Servicio>();

            List<Servicio> servicios = new List<Servicio>();

            foreach (DataRow row in t.Rows)
            {
                int cod = Convert.ToInt32(row["id"]);
                string nombre = Convert.ToString(row["nombre"]);
                int precio = (int)row["costo"];
                string promocion = Convert.ToString(row["enPromocion"]);

                Servicio servicio = new Servicio()
                {
                    Id = cod,
                    Nombre = nombre,
                    Costo = precio,
                    EnPromocion = promocion
                };

                servicios.Add(servicio);
            }

            return servicios;
        }

        public bool Update(Servicio servicio)
        {
            bool result = true;
            SqlConnection cnn = null;

            try
            {
                // Inicializa la conexión a la base de datos
                cnn = DataHelper.GetInstance().GetConnection();
                cnn.Open();  // Asegúrate de abrir la conexión antes de ejecutar cualquier comando

                // Crea el comando SQL y asocia la conexión
                var cmd = new SqlCommand("SP_ACTUALIZAR_SERVICIO", cnn);
                cmd.CommandType = CommandType.StoredProcedure;

                // Añade los parámetros necesarios
                cmd.Parameters.AddWithValue("@id", servicio.Id);
                cmd.Parameters.AddWithValue("@nombre", servicio.Nombre);
                cmd.Parameters.AddWithValue("@costo", servicio.Costo);
                cmd.Parameters.AddWithValue("@enPromocion", servicio.EnPromocion);

                // Ejecuta el comando
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                result = false;
                // Puedes registrar el error aquí
                Console.WriteLine("Error en la ejecución de la consulta: " + ex.Message);
                throw;  // Propaga el error para ser manejado en el controlador
            }
            finally
            {
                // Cierra la conexión si está abierta
                if (cnn != null && cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                }
            }
            return result;
        }
        //public bool Update(Servicio servicio)
        //{
        //    bool result = true;
        //    SqlConnection cnn = null;

        //    try
        //    {
        //        cnn = DataHelper.GetInstance().GetConnection();
        //        cnn.Open();

        //        var cmd = new SqlCommand("SP_ACTUALIZAR_SERVICIO");
        //        cmd.CommandType = CommandType.StoredProcedure;

        //        cmd.Parameters.AddWithValue("@id", servicio.Id);
        //        cmd.Parameters.AddWithValue("@nombre", servicio.Nombre);
        //        cmd.Parameters.AddWithValue("@costo", servicio.Costo);
        //        cmd.Parameters.AddWithValue("@enPromocion", servicio.EnPromocion);

        //        cmd.ExecuteNonQuery();
        //    }
        //    catch (SqlException)
        //    {
        //        result = false;
        //    }
        //    finally
        //    {
        //        if (cnn != null && cnn.State == ConnectionState.Open)
        //        {
        //            cnn.Close();
        //        }
        //    }
        //    return result;

        //}
    }
}
