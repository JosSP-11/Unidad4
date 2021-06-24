using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace ClinicaSORIANO.Models
{
    //[Table("Paciente")]
    public class Paciente
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        [NotNull ]
        public string Nombre { get; set; }
        [NotNull]
        public string ApellidoP { get; set; }
        [NotNull]
        public string  ApellidoM { get; set; }
        [NotNull]
        public string Telefono { get; set; }
        
        public string Correo { get; set; }
        [NotNull]
        public DateTime FechaN { get; set; }=DateTime.Now.Date;
        [NotNull]
        public string Tratamiento { get; set; }
        [NotNull]
        public DateTime FechaCita { get; set; } = DateTime.Now.Date;
        [NotNull]
        public string Hora { get; set; }

    }
}
