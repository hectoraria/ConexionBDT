using DALD;
using ENT;
using ListadoBDTASP.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ListadoBDTASP.Controllers
{
    public class HomeController : Controller
    {
        // GET: HomeController
        public ActionResult Index()
        {
            ClsListadoPersonasConNombreDept personas = new ClsListadoPersonasConNombreDept();
            return View(personas.ListadoPersonasNombreDept);
        }

        // GET: HomeController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

       

       

       

        

        
    }
}
