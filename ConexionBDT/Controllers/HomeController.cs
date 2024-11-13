using ConexionBDT.Models;
using DALD;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Diagnostics;

namespace ConexionBDT.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ClsConexion _conexion;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _conexion = new ClsConexion();
        }

        public IActionResult Index()
        {
            ViewBag.estado = "Pulsa el botón para verificar la conexión";  // Mensaje predeterminado
            return View();
        }

        [HttpPost]
        public ActionResult Conexion()
        {
            try
            {
                using (SqlConnection conexion = _conexion.getConexion())
                {
                    if (conexion.State == System.Data.ConnectionState.Open)
                    {
                        ViewBag.estado = "Conexión exitosa";
                    }
                    else
                    {
                        ViewBag.estado = "Error: la conexión no pudo establecerse";
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al intentar conectar con la base de datos: {0}", ex.Message);
                ViewBag.estado = "Error al intentar conectar con la base de datos";
            }

            return View("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
