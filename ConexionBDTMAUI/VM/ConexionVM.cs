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
        private DelegateCommand insertar;
        private DelegateCommand deletear;
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
                    deletear.RaiseCanExecuteChanged();
                }
            }
        }
        public DelegateCommand Deletear
        {
            get { return deletear; }
        }
        public DelegateCommand Insertar
        {
            get { return insertar; }
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
            insertar = new DelegateCommand(InsertarCommandExecuted);
            deletear = new DelegateCommand(DeleteCommand_Executed, deleteCommandCanExecute);


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

        private async void InsertarCommandExecuted()
        {
            await Shell.Current.GoToAsync("///insertarPersona");
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

        private async void DeleteCommand_Executed() 
        {
            // Mostrar un cuadro de confirmación
            bool isConfirmed = await Application.Current.MainPage.DisplayAlert(
                "Confirmar Borrado",
                "¿Estás seguro de que deseas borrar a " + personaSeleccionada.Nombre + "?",
                "Sí",
                "No");

            // Si el usuario confirma, ejecutar la lógica de borrado
            
            if (isConfirmed)
            {
                try
                {
                    int lineas = ClsManejadora.eliminarPersona(personaSeleccionada.Id);
                    CancellationTokenSource token = new CancellationTokenSource();
                    
                    personaSeleccionada = null;
                    editar.RaiseCanExecuteChanged();
                    deletear.RaiseCanExecuteChanged();
                    
                }
                catch (Exception ex)
                {

                }
               
                
            }
                
            
        }

        private bool deleteCommandCanExecute()
        {
            bool canExecute = false;

            if (personaSeleccionada != null)
            {
                canExecute = true;
            }

            return canExecute;
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
