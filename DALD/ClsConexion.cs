using Microsoft.Data.SqlClient;

namespace DALD
{
    public class ClsConexion
    {
        public SqlConnection getConexion()
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
