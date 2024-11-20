using ENT;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALD
{
    public class ListadosDAL
    {
        /// <summary>
        /// Devuelve un listado de la base de datos de azure
        /// </summary>
        public static List<ClsPersona> ListadoCompletoPersonasDAL()
        {

           
            SqlConnection miConexion = new SqlConnection();

            List<ClsPersona> listadoPersonas = new List<ClsPersona>();

            SqlCommand miComando = new SqlCommand();

            SqlDataReader miLector;

            ClsPersona oPersona;

            try
            {
                miConexion = ClsConexion.getConexion();
                miComando.CommandText = "SELECT * FROM personas";
                miComando.Connection= miConexion;
                miLector = miComando.ExecuteReader();

                if (miLector.HasRows)
                {
                    while (miLector.Read())
                    {
                        oPersona = new ClsPersona();
                        oPersona.Id = (int)miLector["ID"];
                        oPersona.Nombre = (string)miLector["Nombre"];
                        oPersona.Apellidos = (string)miLector["Apellidos"];
                        oPersona.Foto = (string)miLector["Foto"];
                        if (miLector["FechaNacimiento"] != System.DBNull.Value)
                        {
                            oPersona.FechaNacimiento = (DateTime)miLector["FechaNacimiento"];
                        }
                        oPersona.Direccion = (string)miLector["Direccion"];
                        oPersona.Telefono = (string)miLector["Telefono"];
                        listadoPersonas.Add(oPersona);
                    }
                }
                miLector.Close();

            }
            catch (Exception ex) {
                throw;
            }
            finally
            {
                miConexion.Close();
            }

            return listadoPersonas;
        }

        /// <summary>
        /// Elimina una persona de la base de datos
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int eliminarPersona(int id)
        {

            int numeroFilasAfectadas = 0;

            SqlConnection miConexion = new SqlConnection();

            List<ClsPersona> listadoPersonas = new List<ClsPersona>();

            SqlCommand miComando = new SqlCommand();

            SqlDataReader miLector;

            ClsPersona oPersona;

            miComando.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = id;

            try

            {
                miConexion = ClsConexion.getConexion();

                miComando.CommandText = "DELETE FROM Personas WHERE ID=@id";

                miComando.Connection = miConexion;

                numeroFilasAfectadas = miComando.ExecuteNonQuery();

            }

            catch (Exception ex)

            {

                throw ex;

            }

            return numeroFilasAfectadas;
        }

        /// <summary>
        /// Devuelve un listado de la base de datos de azure
        /// </summary>
        public static ClsPersona obtenerPersonaPorID(int id)
        {


            SqlConnection miConexion = new SqlConnection();

            List<ClsPersona> listadoPersonas = new List<ClsPersona>();

            SqlCommand miComando = new SqlCommand();

            SqlDataReader miLector;

            ClsPersona oPersona=null;
           


            miComando.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = id;

            try
            {
                miConexion = ClsConexion.getConexion();
                miComando.CommandText = "SELECT * FROM personas where id=@id";
                miComando.Connection = miConexion;
                miLector = miComando.ExecuteReader();

                if (miLector.HasRows)
                {
                    while (miLector.Read())
                    {
                        oPersona = new ClsPersona();
                        oPersona.Id = (int)miLector["ID"];
                        oPersona.Nombre = (string)miLector["Nombre"];
                        oPersona.Apellidos = (string)miLector["Apellidos"];
                        oPersona.Foto = (string)miLector["Foto"];
                        if (miLector["FechaNacimiento"] != System.DBNull.Value)
                        {
                            oPersona.FechaNacimiento = (DateTime)miLector["FechaNacimiento"];
                        }
                        oPersona.Direccion = (string)miLector["Direccion"];
                        oPersona.Telefono = (string)miLector["Telefono"];
                        
                    }
                }
                miLector.Close();

            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                miConexion.Close();
            }

            return oPersona;
        }

    }
}

