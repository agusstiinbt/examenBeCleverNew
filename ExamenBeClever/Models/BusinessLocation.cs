using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ExamenBeClever.Models
{
    public class BusinessLocation
    {
        [Key]
        [Column(Order = 1)]
        public int IdBusinessLocation { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public virtual ICollection<Empleado> Empleado { get; set; }
    }
}
