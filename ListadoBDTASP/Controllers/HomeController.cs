using DALD;
using ENT;
using ListadoBDTASP.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ListadoBDTASP.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<ClsPersona> listadoPersona = ListadosDAL.ListadoCompletoPersonasDAL();
            return View(listadoPersona);
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
