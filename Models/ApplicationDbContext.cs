using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<Avion> Aviones { get; set; }
    public DbSet<Vuelo> Vuelos { get; set; }
    public DbSet<VueloInfo> VuelosInfo { get; set; }
    public DbSet<Tripulacion> Tripulaciones { get; set; }
    public DbSet<Login> Logins { get; set; }
    public DbSet<Admi> Admins { get; set; }
    public DbSet<Pasajero> Pasajeros { get; set; }
    public DbSet<Tarifas> Tarifas { get; set; }
    public DbSet<PagoVuelo> PagosVuelo { get; set; }
    public DbSet<Reserva> Reservas { get; set; }
    public DbSet<PPagos> Pagos { get; set; }
}