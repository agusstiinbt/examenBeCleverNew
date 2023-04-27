using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ExamenBeClever.Models
{
    public class Registro
    {
        [Key]
        [Column("Id", Order = 1)]
        public int IdRegistro { get; set; }
        public int IdEmpleado { get; set; }
        [ForeignKey("IdEmpleado")]
        public virtual Empleado Empleado { get; set; }
        public int IdBusinessLocation { get; set; }
        [ForeignKey("IdBusinessLocation")]
        public virtual BusinessLocation BusinessLocation { get; set; }
        public string RegisterType { get; set; }
        public DateTime Date { get; set; }
    }
}
