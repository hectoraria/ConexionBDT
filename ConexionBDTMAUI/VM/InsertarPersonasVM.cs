using ConexionBDTMAUI.VM.Utils;
using DALD;
using ENT;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ConexionBDTMAUI.VM
{
    public class InsertarPersonasVM : INotifyPropertyChanged
    {


        #region Atributos
        private string nombre;
        private string apellidos;
        private string telefono;
        private string direccion;
        private string foto;
        private DateTime fechaNac;
        private string nombreDept;
        private List<ClsDepartamento> departamentos;
        private ClsDepartamento departamentoSeleccionado;
        private DelegateCommand insertarCommand;
        private DelegateCommand volverCommand;
        #endregion

        #region Propiedades
        public string Nombre
        {
            get { return nombre; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    nombre = value;
                    NotifyPropertyChanged(nameof(Nombre));
                    insertarCommand.RaiseCanExecuteChanged();
                }
            }
        }
        public string Apellidos
        {
            get { return apellidos; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    apellidos = value;
                    NotifyPropertyChanged(nameof(Apellidos));
                    insertarCommand.RaiseCanExecuteChanged();
                }
            }
        }
        public string Telefono
        {
            get { return telefono; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    telefono = value;
                    NotifyPropertyChanged(nameof(Telefono));
                    insertarCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public string Direccion
        {
            get { return direccion; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    direccion = value;
                    NotifyPropertyChanged(nameof(Direccion));
                    insertarCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public string Foto
        {
            get { return foto; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    foto = value;
                    NotifyPropertyChanged(nameof(Foto));
                    insertarCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public DateTime FechaNac
        {
            get { return fechaNac; }
            set
            {
                fechaNac = value;
                NotifyPropertyChanged(nameof(FechaNac));
                insertarCommand.RaiseCanExecuteChanged();
            }
        }

        public string NombreDept
        {
            get { return nombreDept; }
        }

        public List<ClsDepartamento> Departamentos
        {
            get { return departamentos; }
        }

        public ClsDepartamento DepartamentoSeleccionado
        {
            get { return departamentoSeleccionado; }
            set
            {
                if (value != null)
                {
                    departamentoSeleccionado = value;
                    NotifyPropertyChanged();
                    insertarCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public DelegateCommand InsertarCommand
        {
            get { return insertarCommand; }
        }

        public DelegateCommand VolverCommand
        {
            get { return volverCommand; }
        }


        #endregion

        #region Constructor
        public InsertarPersonasVM()
        {
            fechaNac = DateTime.Now;
            insertarCommand = new DelegateCommand(insertarCommandExecuted, insertarCommandCanExecute);
            volverCommand = new DelegateCommand(volverCommandExecute);
            departamentos = ListadosDAL.ListadoCompletoDepartamentosDAL();
            
            
        }
        #endregion

        #region Commands
        /// <summary>
        /// Función que inserta una persona en la base de datos
        /// <br></br>
        /// Pre: Todos los campos rellenos
        /// <br></br>
        /// Post: Ninguna
        /// </summary>
        public void insertarCommandExecuted()
        {
            
                ClsPersona persona = new ClsPersona(1, nombre, apellidos, telefono, direccion, foto, fechaNac, departamentoSeleccionado.Id);
                ClsManejadora.insertarPersonaDAL(persona);
            
           

        }

        public async void volverCommandExecute()
        {
            await Shell.Current.GoToAsync("///mainPage");
        }

        /// <summary>
        /// Función que comprueba cuando puede mostrarse el command
        /// <br></br>
        /// Pre: Ninguna
        /// <br></br>
        /// Post: Ninguna
        /// </summary>
        /// <returns></returns>
        public bool insertarCommandCanExecute()
        {
            bool canExecute = false;

            if (!string.IsNullOrEmpty(nombre) && !string.IsNullOrEmpty(apellidos) && !string.IsNullOrEmpty(telefono)
                && !string.IsNullOrEmpty(direccion) && !string.IsNullOrEmpty(foto) && fechaNac != null && departamentoSeleccionado != null)
            {
                canExecute = true;
            }

            return canExecute;
        }
        #endregion

        #region Notify
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")

        {

            PropertyChanged?.Invoke(this, new
            PropertyChangedEventArgs(propertyName));

        }
        #endregion

    }
}
