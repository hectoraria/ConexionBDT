using ENT;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DALD
{
    public class ClsManejadora
    {
        /// <summary>
        /// Funcion para eliminar una persona de la base de datos
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Devuelve un int que corresponde al numero de filas afectadas</returns>
        /// <summary>
        /// Elimina una persona de la base de datos
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int eliminarPersona(int id)
        {

            int numeroFilasAfectadas = 0;

            SqlConnection miConexion = new SqlConnection();

            SqlCommand miComando = new SqlCommand();

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
        /// Funcion para crear un persona y introducirla en la base de datos de azure
        /// </summary>
        /// <param name="persona"></param>
        /// <returns>Devuelve un int del numero de las filas afectadas</returns>
        public static int crearPersona(ClsPersona persona)
        {

            int numeroFilasAfectadas = 0;

            SqlConnection miConexion = new SqlConnection();

            List<ClsPersona> listadoPersonas = new List<ClsPersona>();

            SqlCommand miComando = new SqlCommand();

            SqlDataReader miLector;

            ClsPersona oPersona;

            miComando.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = persona.Id;
            miComando.Parameters.Add("@nombre", System.Data.SqlDbType.VarChar).Value = persona.Nombre;
            miComando.Parameters.Add("@apellido", System.Data.SqlDbType.VarChar).Value = persona.Apellidos;
            miComando.Parameters.Add("@telefono", System.Data.SqlDbType.VarChar).Value = persona.Telefono;
            miComando.Parameters.Add("@direccion", System.Data.SqlDbType.VarChar).Value = persona.Direccion;
            miComando.Parameters.Add("@fechanac", System.Data.SqlDbType.DateTime).Value = persona.FechaNacimiento;
            miComando.Parameters.Add("@idDepartamento", System.Data.SqlDbType.Int).Value = persona.IDDepartamento;
            

            try

            {
                miConexion = ClsConexion.getConexion();

                miComando.CommandText = "INSERT INTO Personas (ID, Nombre, Apellido, Telefono, Direccion, FechaNacimiento, IDDepartamento) VALUES (@id, @nombre, @apellido, @telefono, @direccion, @fechanac, @idDepartamento)";

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
        /// Funcion para modificar un persona y cambiarla en la base de datos de azure
        /// </summary>
        /// <param name="persona"></param>
        /// <returns>Devuelve un int del numero de las filas afectadas</returns>
        public static int modificaPersona(ClsPersona persona)
        {

            int numeroFilasAfectadas = 0;

            SqlConnection miConexion = new SqlConnection();

            List<ClsPersona> listadoPersonas = new List<ClsPersona>();

            SqlCommand miComando = new SqlCommand();

            SqlDataReader miLector;

            ClsPersona oPersona;

            miComando.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = persona.Id;
            miComando.Parameters.Add("@nombre", System.Data.SqlDbType.VarChar).Value = persona.Nombre;
            miComando.Parameters.Add("@apellido", System.Data.SqlDbType.VarChar).Value = persona.Apellidos;
            miComando.Parameters.Add("@telefono", System.Data.SqlDbType.VarChar).Value = persona.Telefono;
            miComando.Parameters.Add("@direccion", System.Data.SqlDbType.VarChar).Value = persona.Direccion;
            miComando.Parameters.Add("@fechanac", System.Data.SqlDbType.DateTime).Value = persona.FechaNacimiento;
            miComando.Parameters.Add("@idDepartamento", System.Data.SqlDbType.Int).Value = persona.IDDepartamento;


            try
            {
                miConexion = ClsConexion.getConexion();

                miComando.CommandText = "UPDATE Personas SET Nombre = @nombre, Apellido = @apellido, Telefono = @telefono, Direccion = @direccion, FechaNacimiento = @fechanac, IDDepartamento = @idDepartamento, WHERE ID = @id";

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

            ClsPersona oPersona = null;



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
                        oPersona.IDDepartamento = (int)miLector["IDDepartamento"];

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
