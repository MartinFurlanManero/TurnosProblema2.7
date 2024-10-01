using RepositorioTurno.Entities;
using RepositorioTurno.Repositories.Interfaces;
using RepositorioTurno.Utils;
using System.Data;
using System.Data.SqlClient;


namespace RepositorioTurno.Repositories.Implementacion
{
    public class TurnoRepositorio : ITurnoRepository
    {
        private readonly DataHelper _dataHelper;
        public TurnoRepositorio()
        {
            _dataHelper = DataHelper.GetInstance();
        }

        public int ContarTurnos(string fecha, string hora)
        {
            var cnn = DataHelper.GetInstance().GetConnection();
            cnn.Open();

            var cmd = new SqlCommand("SP_CONTAR_TURNOS", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            //parámetro de entrada:
            cmd.Parameters.AddWithValue("@fecha", fecha);
            cmd.Parameters.AddWithValue("@hora", hora);
          
            //parámetro de salida:
            SqlParameter param = new SqlParameter("@ctd_turnos", SqlDbType.Int);
            param.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(param);
            cmd.ExecuteNonQuery();
            cnn.Close();

            int countTurnos = (int)param.Value;

            return countTurnos;

        }

        public bool InsertarMaestroDetalle(Turno turno)
        {
            bool result = true;
            SqlTransaction? t = null;
            SqlConnection? cnn = null;

            try
            {
                cnn = DataHelper.GetInstance().GetConnection();
                cnn.Open();
                t = cnn.BeginTransaction();

                var cmd = new SqlCommand("SP_INSERTAR_TURNO", cnn, t);
                cmd.CommandType = CommandType.StoredProcedure;

                //parámetro de entrada:
                cmd.Parameters.AddWithValue("@fecha",turno.Fecha);
                cmd.Parameters.AddWithValue("@hora", turno.Hora);
                cmd.Parameters.AddWithValue("@cliente", turno.Cliente);

                //parámetro de salida:
                SqlParameter param = new SqlParameter("@id_turno", SqlDbType.Int);
                param.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(param);
                cmd.ExecuteNonQuery();

                int idTurno = (int)param.Value;

                foreach (var detalle in turno.detalles)
                {
                    var cmdDetail = new SqlCommand("SP_INSERTAR_DETALLES", cnn, t);
                    cmdDetail.CommandType = CommandType.StoredProcedure;
                    cmdDetail.Parameters.AddWithValue("@id_turno", idTurno);
                    cmdDetail.Parameters.AddWithValue("@id_servicio",detalle.ServicioId );
                    cmdDetail.Parameters.AddWithValue("@observaciones",detalle.Observaciones);
                    cmdDetail.ExecuteNonQuery();
                }

                t.Commit();
            }
            catch (SqlException)
            {
                if (t != null)
                {
                    t.Rollback();
                }
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

        public List<Servicio> ObtenerServicios()
        {
            // TODO:
            var result = _dataHelper.ExecuteSPQuery("SP_CONSULTAR_SERVICIOS", null);
            var lstServicios = new List<Servicio>();
            foreach(DataRow row in result.Rows)
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
    }
}
