using DALD;
using ENT;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ListadoBDTASP.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonaController : ControllerBase
    {
        // GET: api/<PersonaController>
        [HttpGet]
        public IActionResult Get()
        {
            IActionResult salida;
            try
            {
                List<ClsPersona> listadoCompleto = ListadosDAL.ListadoCompletoPersonasDAL();
                if (listadoCompleto.Count() == 0)
                {
                    salida = NoContent();
                }
                else
                {
                    salida = Ok(listadoCompleto);
                }
            }
            catch
            {
                salida = BadRequest();
            }
            return salida;

        }


        // GET api/<PersonaController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            IActionResult salida;
            try
            {
                ClsPersona persona = ClsManejadora.obtenerPersonaPorID(id);
                if (persona.Nombre == "")
                {
                    salida = NoContent();
                }
                else
                {
                    salida = Ok(persona);
                }
            }
            catch
            {
                salida = BadRequest();
            }
            return salida;
        }

        // POST api/<PersonaController>
        [HttpPost]
        public IActionResult Post([FromBody] ClsPersona persona)
        {
            int filasAfectadas;
            IActionResult salida;
            if (persona.Nombre == "")
            {
                salida = BadRequest();
            }
            else
            {
                try
                {
                    filasAfectadas = ClsManejadora.insertarPersonaDAL(persona);
                    if (filasAfectadas == 0)
                    {
                        salida = BadRequest();
                    }
                    else
                    {
                        salida = Ok(filasAfectadas);
                    }
                }
                catch
                {
                    salida = BadRequest();
                }
            }
            return salida;
        }

        // PUT api/<PersonaController>/5
        [HttpPut("{id}")]
        public IActionResult Put(ClsPersona persona)
        {
            int filasAfectadas;
            IActionResult salida;
            if (persona.Nombre == "")
            {
                salida = BadRequest();
            }
            else
            {
                try
                {
                    filasAfectadas = ClsManejadora.editarPersonaDAL(persona);
                    if (filasAfectadas == 0)
                    {
                        salida = BadRequest();
                    }
                    else
                    {
                        salida = Ok(filasAfectadas);
                    }
                }
                catch
                {
                    salida = BadRequest();
                }
            }
            return salida;
        }

        // DELETE api/<PersonaController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            IActionResult salida;
            int numFilasAfectadas;

            try
            {
                numFilasAfectadas = ClsManejadora.eliminarPersona(id);
                if (numFilasAfectadas == 0)
                {
                    salida = NotFound();
                }
                else
                {
                    salida = Ok(numFilasAfectadas);
                }
            }
            catch (Exception e)
            {
                salida = BadRequest();
            }

            return salida;

        }
    }
}
