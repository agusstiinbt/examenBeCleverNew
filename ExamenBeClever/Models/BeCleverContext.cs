using Microsoft.EntityFrameworkCore;
using ExamenBeClever.Models;

namespace ExamenBeClever.Models
{
    public class BeCleverContext:DbContext
    {
        public BeCleverContext(DbContextOptions<BeCleverContext>options):base(options){}
        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<Registro> Registros { get; set; }
        public DbSet<BusinessLocation> BusinessLocations { get; set; }           
    }
}
