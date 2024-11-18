using ConexionBDTMAUI.VM.Utils;
using DALD;
using ENT;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ConexionBDTMAUI.VM 
{
    public class ConexionVM : INotifyPropertyChanged
    {
        #region atributos

        private DelegateCommand conexion;
        private String estado;
        private List<ClsPersona> listadoPersonas;

        #endregion

        public DelegateCommand Conexion
        { get { return conexion; } }

        public String Estado
        {
            get { return estado; }
            set { estado = value;
                  
                conexion.RaiseCanExecuteChanged();
                }
        }

        public List<ClsPersona> ListadoPersonas {  get { return listadoPersonas; } }


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
            conexion = new DelegateCommand(Execute, CanExecute);
            listadoPersonas = ListadosDAL.ListadoCompletoPersonasDAL();
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
        #endregion
    }
}
