using ConexionBDT.Models;
using DALD;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Diagnostics;

namespace ConexionBDT.Controllers
{
    public class HomeController : Controller
    {
        
        

        

        public IActionResult Index()
        {
            
            return View();
        }

        [HttpPost]
        public ActionResult Conexion()
        {

            SqlConnection conexion = new SqlConnection(); 
                try
            {
                conexion = ClsConexion.getConexion();

                
                    if (conexion.State == System.Data.ConnectionState.Open)
                    {
                        ViewBag.estado = "Conexión exitosa";
                    }
                    else
                    {
                        ViewBag.estado = "Error: la conexión no pudo establecerse";
                    }
                    
                
            }
            catch (Exception ex)
            {
               
                ViewBag.estado = "Error al intentar conectar con la base de datos";
            }
            finally
            {
                conexion.Close();
            }

            return View("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

       
    }
}
