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
        private DelegateCommand editar;
        private List<ClsPersona> listadoPersonas;
        private String estado;
        private ObservableCollection<clsPersonaNombreDept> listadoPersonasNombreDept;
        private clsPersonaNombreDept personaSeleccionada;

        #endregion

        public clsPersonaNombreDept PersonaSeleccionada
        {
            get { return personaSeleccionada; }
            set
            {
                if (value != null)
                {
                    personaSeleccionada = value;
                    NotifyPropertyChanged(nameof(PersonaSeleccionada));
                    editar.RaiseCanExecuteChanged();
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

        public DelegateCommand Editar
        { get { return editar; } }
        


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
            editar = new DelegateCommand(EditarCommandExecuted, editarCommandCanExecute);


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

        private async void EditarCommandExecuted()
        {

            if (personaSeleccionada != null)
            {
                ClsPersona persona = ClsManejadora.obtenerPersonaPorID(personaSeleccionada.Id);
                var queryParams = new Dictionary<string, object>
                {
                    { "persona", persona}
                };

                await Shell.Current.GoToAsync("///editarPersona", queryParams);
            }

        }

        /// <summary>
        /// Función que comprueba cuando puede mostrarse el command
        /// <br></br>
        /// Pre: Ninguna
        /// <br></br>
        /// Post: Ninguna
        /// </summary>
        /// <returns>Devuelve si puede mostrarse o no</returns>
        public bool editarCommandCanExecute()
        {
            bool canExecute = false;

            if (personaSeleccionada != null)
            {
                canExecute = true;
            }

            return canExecute;
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
