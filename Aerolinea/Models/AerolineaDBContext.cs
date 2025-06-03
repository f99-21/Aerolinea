using Microsoft.EntityFrameworkCore;
namespace Aerolinea.Models
{
    public class AerolineaDBContext : DbContext
    {
    

        public AerolineaDBContext(DbContextOptions<AerolineaDBContext> options) : base(options) { }

        public DbSet<Vuelo> Vuelo { get; set; }
        public DbSet<Pasajero> pasajero { get; set; }
        public DbSet<Login> login { get; set; }
        public DbSet<Reserva> reserva { get; set; }
    
    public DbSet<Admi> Admi { get; set; }
        public DbSet<Tarifa> tarifas { get; set; }
        public DbSet<Pago_Vuelo> pago_vuelo { get; set; }
        public DbSet<Pago> ppagos { get; set; }
        public DbSet<Avion> avion { get; set; }
        public DbSet<VueloInfo> vuelo_info { get; set; }
        public DbSet<Tripulacion> tripulacion { get; set; }
        public DbSet<Login> Logins { get; set; } // Puede tener cualquier nombre
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Login>().ToTable("login"); // Mapeo explícito
        }

    }
}
   
