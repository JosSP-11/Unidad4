using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace ClinicaSORIANO.Models
{
   public class ClinicaCRUD
    {
        public SQLiteConnection Conexion { get; set; }
        //public string ruta = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "/clinicaSORIANOdb";
         
        public ClinicaCRUD()
        {
            Conexion = new SQLiteConnection("./Data/clinicaSORIANO.db");
            //Conexion = new SQLiteConnection("C:/Users/Jos3a/source/repos/Unidad4/ClinicaSORIANO/Data/clinicaSORIANO.db");
        }
        public List<string> Errores { get; set; }
        public bool Insert(Paciente p)
        {
            Errores = new List<string>();
            
            if (string.IsNullOrWhiteSpace(p.Nombre))
            {
                Errores.Add("Llene el campo nombre");
            }
            if (string.IsNullOrWhiteSpace(p.ApellidoP))
            {
                Errores.Add("Llene el campo apellido paterno");
            }
            if (string.IsNullOrWhiteSpace(p.ApellidoM))
            {
                Errores.Add("Llene el campo apellido materno");
            }
            if (string.IsNullOrWhiteSpace(p.Telefono))
            {
                Errores.Add("Llene el campo telefono");
            }
            if (p.FechaN.Date < DateTime.MinValue)
            {
                Errores.Add( "Esta fecha de nacimiento no es valida");
            }
            if (string.IsNullOrWhiteSpace(p.Tratamiento))
            {
                Errores.Add("Llene el campo tratamiento");
            }
            if (p.FechaCita.Date < DateTime.Today)
            {
                Errores.Add("Esta fecha de la cita ya no es valida");
            }
            if (string.IsNullOrWhiteSpace(p.Hora))
            {
                Errores.Add("Llene el campo hora");
            }
            if (Errores.Count > 0)  return false;
            

            Conexion.Insert(p); 
            
            return true;
        }
        public IEnumerable<Paciente> GetALL()
        {
            return Conexion.Table<Paciente>().OrderBy(x=>x.FechaCita);
        }
        public Paciente Get(int id)
        {
            return Conexion.Table<Paciente>().FirstOrDefault(x => x.ID == id);
        }
        public Paciente GetFecha(DateTime fecha)
        {
            return Conexion.Table<Paciente>().FirstOrDefault(x => x.FechaCita == fecha);
        }
        public bool  Update(Paciente clon)
        {
            Errores = new List<string>();
            if (clon.ID < 0)
            {
                Errores.Add("Falta el ID");
            }
            //if (string.IsNullOrWhiteSpace(clon.Nombre))
            //{
            //    Errores.Add("Llene el campo nombre");
            //}
            //if (string.IsNullOrWhiteSpace(clon.ApellidoP))
            //{
            //    Errores.Add("Llene el campo apellido paterno");
            //}
            //if (string.IsNullOrWhiteSpace(clon.ApellidoM))
            //{
            //    Errores.Add("Llene el campo apellido materno");
            //}
            //if (string.IsNullOrWhiteSpace(clon.Telefono))
            //{
            //    Errores.Add("Llene el campo telefono");
            //}
            //if (string.IsNullOrWhiteSpace(clon.FechaN))
            //{
            //    Errores.Add("Llene el campo fecha de nacimiento");
            //}
            //if (string.IsNullOrWhiteSpace(clon.Tratamiento))
            //{
            //    Errores.Add("Llene el campo tratamiento");
            //}
            //if (string.IsNullOrWhiteSpace(clon.FechaCita))
            //{
            //    Errores.Add("Llene el campo nombre");
            //}
            //if (string.IsNullOrWhiteSpace(clon.FechaCita))
            //{
            //    Errores.Add("Llene el campo nombre");
            //}
            //if (string.IsNullOrWhiteSpace(clon.Hora))
            //{
            //    Errores.Add("Llene el campo hora");
            //}
            if (string.IsNullOrWhiteSpace(clon.Nombre))
            {
                Errores.Add("Llene el campo nombre");
            }
            if (string.IsNullOrWhiteSpace(clon.ApellidoP))
            {
                Errores.Add("Llene el campo apellido paterno");
            }
            if (string.IsNullOrWhiteSpace(clon.ApellidoM))
            {
                Errores.Add("Llene el campo apellido materno");
            }
            if (string.IsNullOrWhiteSpace(clon.Telefono))
            {
                Errores.Add("Llene el campo telefono");
            }
            if (clon.FechaN.Date < DateTime.MinValue)
            {
                Errores.Add("Esta fecha de nacimiento no es valida");
            }
            if (string.IsNullOrWhiteSpace(clon.Tratamiento))
            {
                Errores.Add("Llene el campo tratamiento");
            }
            if (clon.FechaCita.Date < DateTime.Today)
            {
                Errores.Add("Esta fecha de la cita ya no es valida");
            }
            if (string.IsNullOrWhiteSpace(clon.Hora))
            {
                Errores.Add("Llene el campo hora");
            }
            if (Errores.Count > 0)
            {
                return false;
            }
            var pacienteDB = Get(clon.ID);

            pacienteDB.ID = clon.ID;
            pacienteDB.Nombre = clon.Nombre;
            pacienteDB.ApellidoP = clon.ApellidoP;
            pacienteDB.ApellidoM = clon.ApellidoM;
            pacienteDB.Telefono = clon.Telefono;
            pacienteDB.Correo = clon.Correo;
            pacienteDB.FechaN = clon.FechaN;
            pacienteDB.Tratamiento = clon.Tratamiento;
            pacienteDB.FechaCita = clon.FechaCita;
            pacienteDB.Hora = clon.Hora;

            Conexion.Update(pacienteDB);
            return true;
            
        }
        public void Delete( Paciente p)
        {
            Conexion.Delete(p);
        }
    }
}
