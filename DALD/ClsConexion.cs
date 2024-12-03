using Microsoft.Data.SqlClient;

namespace DALD
{
    public class ClsConexion
    {

        /// <summary>
        /// Funcion para conectarte a la base de datos de azure
        /// </summary>
        /// <returns>Devuelve la conexion abierta</returns>
        public static SqlConnection getConexion()
        {
            SqlConnection miConexion = new SqlConnection();

            try

            {

                miConexion.ConnectionString = "server=campana.database.windows.net;database=HectorBD;uid=usuario;pwd=LaCampana123;trustServerCertificate = true;";

                miConexion.Open();

            }
            catch (Exception ex) {
                throw;
            }
            

            return miConexion;


        }
    }
}
