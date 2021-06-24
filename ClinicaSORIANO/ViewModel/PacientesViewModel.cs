using ClinicaSORIANO.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClinicaSORIANO.Views;
using System.Windows.Controls;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using SQLite;
using System.Windows;

namespace ClinicaSORIANO.ViewModel
{
    public class PacientesViewModel : INotifyPropertyChanged
    {
        //comandos
        public ICommand NuevoPacienteCommand { get; set; }
        public ICommand CancelarCommand { get; set; }
        public ICommand AgregarCommand { get; set; }
        public ICommand VerCommand { get; set; }
        public ICommand EditarCommand { get; set; }
        public ICommand GuardarCommand { get; set; }
        public ICommand EliminarCommand { get; set; }
       
        //objetos enlazados
        public ObservableCollection<Paciente> Lista { get; set; } = new ObservableCollection<Paciente>();
       

        private Paciente paciente;

        public Paciente Paciente
        {
            get { return paciente; }
            set { paciente = value;
                Actualizar(nameof(Paciente)); }
        }

        private  string mensaje;

        public string Mensaje
        {
            get { return mensaje; }
            set { mensaje = value;
                Actualizar(nameof(Mensaje));
            }
        }
        private Control control;

        public Control Control
        {
            get { return control; }
            set { control = value;
                Actualizar(nameof(Control));
            }
        }


        //vistas
        ListPacientesView listaview;
        AgregarPacienteView agregarview;
        DetallesPacienteView detalleview;
        EditarPacienteView editarview;
       
        // clases dal

        ClinicaCRUD clinica = new ClinicaCRUD();
        public PacientesViewModel()
        {
            listaview = new ListPacientesView() { DataContext = this };
            agregarview = new AgregarPacienteView() { DataContext = this };
            detalleview = new DetallesPacienteView() { DataContext = this };
            editarview = new EditarPacienteView() { DataContext = this };
            
            

            Control = listaview;
            NuevoPacienteCommand = new RelayCommand(nuevo);
            CancelarCommand = new RelayCommand(cancelar);
            AgregarCommand = new RelayCommand(agregar);
            VerCommand = new RelayCommand<Paciente>(ver);
            EditarCommand = new RelayCommand<Paciente>(editar);
            GuardarCommand = new RelayCommand(guardar);
            EliminarCommand = new RelayCommand<Paciente>(eliminar);
            

            CargarDatos();
            
           
        }

        

       

        private DateTime fechaC;

        public DateTime FechaC
        {
            get { return fechaC; }
            set { fechaC = value; Actualizar(nameof(FechaC)); }
        }

        

        private void eliminar(Paciente obj)
        {
          
            if (MessageBox.Show("¿Esta seguro de eliminar la receta seleccionada", "Confirme", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {

                clinica.Delete(obj);
                CargarDatos();

            }
            
        }

        private void editar(Paciente obj)
        {
            Paciente = obj;
            Control = editarview;
            Mensaje = " ";
        }

        private void ver(Paciente obj)
        {
            Paciente = obj;
            Control = detalleview;
        }

        private void guardar()
        {
            Mensaje = " ";
            try
            {
                if (clinica.Update(Paciente))
                {
                    Control = listaview;
                    CargarDatos();
                }
               
                else
                {
                    Mensaje = string.Join("\n", clinica.Errores);
                }
                
            }
            catch(Exception ex)
            {
                Mensaje = ex.Message;
            }
        }

        

        private void CargarDatos()
        {
            Lista.Clear();
            foreach(var p in clinica.GetALL())
            {
                Lista.Add(p);
            }
        }

        private void agregar()
        {
            Mensaje = "";
            if (clinica.Insert(Paciente))
            {
                Control = listaview;
                CargarDatos();
            }
            else
            {
                Mensaje = string.Join("\n", clinica.Errores);
            }
        }

        private void cancelar()
        {
            Control = listaview;
            Paciente = null;
            Mensaje = null;
        }

        private void nuevo()
        {
            paciente = new Paciente();
            Control = agregarview;
           
            
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void Actualizar(string name=null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
        }
    }
}
