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
                        oPersona.IDDepartamento = (int)miLector["IDDepartamento"];
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
        /// Obtiene un listado completo de los departamentos de la base de datos
        /// <br></br>
        /// Pre: Ninguna
        /// <br></br>
        /// Post: Puede devolver una lista vacía si no encuentra nada en la base de datos
        /// </summary>
        /// <returns>Lista de departamentos</returns>
        public static List<ClsDepartamento> listadoCompletoDepartamentosDAL()
        {
            List<ClsDepartamento> listado = new List<ClsDepartamento>();

            SqlConnection conexion = new SqlConnection();
            SqlCommand miComando = new SqlCommand();
            SqlDataReader miLector;
            ClsDepartamento departamento;

            try
            {
                conexion = ClsConexion.getConexion();
                if (conexion.State == System.Data.ConnectionState.Open)
                {
                    miComando.CommandText = "SELECT * FROM departamentos";
                    miComando.Connection = conexion;
                    miLector = miComando.ExecuteReader();

                    if (miLector.HasRows)
                    {
                        while (miLector.Read())
                        {
                            departamento = new ClsDepartamento();
                            departamento.Id = (int)miLector["ID"];
                            departamento.Nombre = (string)miLector["Nombre"];

                            listado.Add(departamento);
                        }
                    }
                    miLector.Close();
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
            finally
            {
                conexion.Close();
            }

            return listado;
        }







    }
}

