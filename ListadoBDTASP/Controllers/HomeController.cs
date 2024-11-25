using DALD;
using ENT;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ListadoBDTASP.Controllers
{
    public class HomeController : Controller
    {
        // GET: HomeController
        public ActionResult Index()
        {
            List<ClsPersona> listadoPersona = ListadosDAL.ListadoCompletoPersonasDAL();
            return View(listadoPersona);
        }

        // GET: HomeController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

       

       

       

        

        
    }
}
