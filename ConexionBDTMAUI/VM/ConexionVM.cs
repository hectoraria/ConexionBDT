using ConexionBDTMAUI.VM.Utils;
using DALD;
using ENT;
using Microsoft.Data.SqlClient;
using ConexionBDTMAUI.VM.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;


namespace ConexionBDTMAUI.VM 
{
    public class ConexionVM : INotifyPropertyChanged
    {
        #region atributos

        private DelegateCommand conexion;
        private List<ClsPersona> listadoPersonas;
        private String estado;
        private ObservableCollection<clsPersonaNombreDept> listadoPersonasNombreDept;
        private clsPersonaNombreDept personaSeleccionada;

        #endregion

        public clsPersonaNombreDept PersonaSelecionada
        {
            get { return personaSeleccionada; }
            set
            {
                if (value != null)
                {
                    personaSeleccionada = value;
                    NotifyPropertyChanged("PersonaSeleccionada");
                    //editarCommand.RaiseCanExecuteChanged();
                    //borrarCommand.RaiseCanExecuteChanged();
                }
            }
        }
        public DelegateCommand Conexion
        { get { return conexion; } }

        public String Estado
        {
            get { return estado; }
            set { estado = value;
                  
                conexion.RaiseCanExecuteChanged();
                }
        }

        public ObservableCollection<clsPersonaNombreDept> ListadoPersonasNombreDept
        {
            get { return listadoPersonasNombreDept; }
        }

        


        #region Notify
        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
        #region Constructor
        public ConexionVM()
        {
            cargarListado();
            conexion = new DelegateCommand(Execute, CanExecute);
            
        }
        #endregion

        #region eventos
        public event EventHandler? CanExecuteChanged;
        private bool CanExecute()
        {
            bool sePuede = true;
           
            return sePuede;
        }

        public void Execute()
        {
            SqlConnection conexion = new SqlConnection();
            
            try
            {
                conexion = ClsConexion.getConexion();


                if (conexion.State == System.Data.ConnectionState.Open)
                {
                    estado = "Conexión exitosa";
                }
                else
                {
                    estado = "Error: la conexión no pudo establecerse";
                }


            }
            catch (Exception ex)
            {

                estado = "Error al intentar conectar con la base de datos";
            }
            finally
            {
                NotifyPropertyChanged("Estado");
                conexion.Close();
            }
        }

        private async void EditarCommand_Executed()
        {

            Dictionary<System.String, object> diccionarioMandar = new Dictionary<String, object>();

            diccionarioMandar.Add("Persona", personaSeleccionada);

            await Shell.Current.GoToAsync("DetallePersona", diccionarioMandar);

        }

        private void DeleteCommand_Executed() 
        {
            
        }

        #endregion


        #region Metodos
        /// <summary>
        /// Función que carga el listado de la base de datos,
        /// le añade el nombre de departamento a las personas y las 
        /// añade al listado final.
        /// <br></br>
        /// Pre: Ninguna
        /// <br></br>
        /// Post: Ninguna
        /// </summary>
        public async void cargarListado()
        {
            if (listadoPersonas != null && listadoPersonas.Count > 0)
            {
                listadoPersonas.Clear();
            }

            try
            {
                listadoPersonas = ListadosDAL.ListadoCompletoPersonasDAL();
                listadoPersonasNombreDept = new ObservableCollection<clsPersonaNombreDept>();

                // se crea una lista de departamentos
                List<ClsDepartamento> listaDept = ListadosDAL.ListadoCompletoDepartamentosDAL();

                foreach (ClsPersona persona in listadoPersonas)
                {
                    clsPersonaNombreDept personaNombreDept = new clsPersonaNombreDept(persona, listaDept);
                    listadoPersonasNombreDept.Add(personaNombreDept);
                }

                NotifyPropertyChanged("ListadoPersonasNombreDept");
            }
            catch (Exception e)
            {
                throw;
            }
        }
        #endregion
    }
}
