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
    [QueryProperty(nameof(Persona),"persona")]
    public class ClsEditarPersonaVM : INotifyPropertyChanged
    {
        #region Atributos
        private ClsPersona persona;
        private List<ClsDepartamento> departamentos;
        private ClsDepartamento departamentoSeleccionado;
        private DelegateCommand volverCommand;
        private DelegateCommand guardarCommand;
        #endregion


        #region Propiedades
        public ClsPersona Persona
        {
            get { return persona; }
            set { persona = value;
                departamentoSeleccionado = ClsManejadora.obtenerDepartamentoPorID(value.IDDepartamento);
                NotifyPropertyChanged("Persona");
                NotifyPropertyChanged("DepartamentoSeleccionado");
            }
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
                }
            }
        }

        public DelegateCommand VolverCommand
        {

            get { return volverCommand; }

        }

        public DelegateCommand GuardarCommand
        {
            get { return guardarCommand; }
        }

        #endregion


        #region Constructores

        public ClsEditarPersonaVM()
        {
            volverCommand = new DelegateCommand(volverCommandExecuted);
            guardarCommand = new DelegateCommand(guardarCommandExecuted);
            departamentos = ListadosDAL.ListadoCompletoDepartamentosDAL();
        }

        #endregion


        #region Command

        public async void volverCommandExecuted()
        {
            await Shell.Current.GoToAsync("///mainPage");
        }

        public async void guardarCommandExecuted()
        {
            persona.IDDepartamento = departamentoSeleccionado.Id;
            int filasAfectadas = ClsManejadora.editarPersonaDAL(persona);

            await Shell.Current.GoToAsync("///mainPage");

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
