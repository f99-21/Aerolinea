using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Reserva
{
    [Key]
    public int IdReserva { get; set; }

    [ForeignKey("PagoVuelo")]
    public int IdPago { get; set; }

    public DateTime FechaHora { get; set; }

    public PagoVuelo PagoVuelo { get; set; }
}