using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class PagoVuelo
{
    [Key]
    public int IdPago { get; set; }

    [ForeignKey("Pasajero")]
    public int IdUsuario { get; set; }

    [ForeignKey("Vuelo")]
    public int IdVuelo { get; set; }

    [ForeignKey("Tarifas")]
    public int IdTarifas { get; set; }

    [Required]
    [Range(0.01, 1000000)]
    public float Total { get; set; }

    public Pasajero Pasajero { get; set; }
    public Vuelo Vuelo { get; set; }
    public Tarifas Tarifas { get; set; }
}