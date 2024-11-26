using ENT;


namespace ConexionBDTMAUI.VM.Models
{
    public class clsPersonaNombreDept : ClsPersona
    {
        #region Atributos
        private string nombreDept;
        #endregion

        #region Propiedades
        public string NombreDept { get { return nombreDept; } }
        #endregion

        #region Constructores
        public clsPersonaNombreDept(ClsPersona persona, List<ClsDepartamento> listaDepartamentos)
        {
            if (persona != null)
            {
                Id = persona.Id;
                Nombre = persona.Nombre;
                Apellidos = persona.Apellidos;
                Telefono = persona.Telefono;
                Direccion = persona.Direccion;
                Foto = persona.Foto;
                FechaNacimiento = persona.FechaNacimiento;
                IDDepartamento = persona.IDDepartamento;

                nombreDept = listaDepartamentos.First(dep => dep.Id == persona.IDDepartamento).Nombre;
            }
        }
        #endregion

    }
}
