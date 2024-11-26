using ENT;
using DALD;
namespace ListadoBDTASP.Models
{
    public class ClsPersonaConNombreDepartamento : ClsPersona
    {
        private string nombreDept;

        public string NombreDept
        {
            get { return nombreDept; }
            set { nombreDept = value; }
        }

        public ClsPersonaConNombreDepartamento(ClsPersona persona, List<ClsDepartamento> departamentos)
        {
            this.Id = persona.Id;
            this.Nombre = persona.Nombre;
            this.Apellidos = persona.Apellidos;
            this.Telefono = persona.Telefono;
            this.Direccion = persona.Direccion;
            this.Foto = persona.Foto;
            this.FechaNacimiento = persona.FechaNacimiento;
            this.IDDepartamento = persona.IDDepartamento;

            string nombreDepartamento = departamentos.FirstOrDefault(x => x.Id == persona.IDDepartamento).Nombre;
            if (nombreDepartamento != null)
            {
                this.nombreDept = nombreDepartamento;
            }
        }

        public ClsPersonaConNombreDepartamento(int idPersona)
        {
            ClsPersona persona = ClsManejadora.obtenerPersonaPorID(idPersona);

            if (persona != null)
            {
                this.Id = persona.Id;
                this.Nombre = persona.Nombre;
                this.Apellidos = persona.Apellidos;
                this.Telefono = persona.Telefono;
                this.Direccion = persona.Direccion;
                this.Foto = persona.Foto;
                this.FechaNacimiento = persona.FechaNacimiento;
                this.IDDepartamento = persona.IDDepartamento;

                ClsDepartamento dep = ClsManejadora.obtenerDepartamentoPorID(persona.IDDepartamento);

                if (dep != null)
                {
                    this.nombreDept = dep.Nombre;
                }
            }

        }

    }
}
