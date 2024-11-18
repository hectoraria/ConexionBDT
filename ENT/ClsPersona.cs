namespace ENT
{
    public class ClsPersona
    {
        #region Propiedades
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public DateTime FechaNac { get; set; }
        public string Foto { get; set; }
        public string Direccion { get; set; }
        public int Telefono { get; set; }
        #endregion

        #region constructores
        public ClsPersona() { }
        public ClsPersona(string nombre, string apellidos, DateTime fechaNac, string foto, string direccion, int telefono)
        {
            Nombre = nombre;
            Apellidos = apellidos;
            FechaNac = fechaNac;
            Foto = foto;
            Direccion = direccion;
            Telefono = telefono;
        }
        #endregion
    }
}
