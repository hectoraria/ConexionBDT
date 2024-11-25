using DALD;
using ENT;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ListadoBDTASP.Controllers
{
    public class PersonaController : Controller
    {
        // GET: PersonaController
        public ActionResult Index()
        {

            List<ClsPersona> listadoPersona = ListadosDAL.ListadoCompletoPersonasDAL();
            return View(listadoPersona);
        }

        // GET: PersonaController/Details/5
        public ActionResult Details(int id)
        {
            ClsPersona persona = ClsManejadora.obtenerPersonaPorID(id);
            return View(persona);
        }

        // GET: PersonaController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PersonaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ClsPersona persona)
        {
            try
            {
                ClsManejadora.insertarPersonaDAL(persona);
                return RedirectToAction("Index", "Persona");
            }
            catch
            {
                return View();
            }
        }

        // GET: PersonaController/Edit/5
        public ActionResult Edit(int id)
        {
            ClsPersona persona = ClsManejadora.obtenerPersonaPorID(id);
            return View(persona);
        }

        // POST: PersonaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ClsPersona persona)
        {
            try
            {
                ClsManejadora.editarPersonaDAL(persona);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PersonaController/Delete/5
        public ActionResult Delete(int id)
        {
            ClsPersona personaE = ClsManejadora.obtenerPersonaPorID(id);
            return View(personaE);
        }

        // POST: PersonaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                ClsManejadora.eliminarPersona(id);
                return RedirectToAction("Index","Home");
            }
            catch
            {
                return View();
            }
        }
    }
}
