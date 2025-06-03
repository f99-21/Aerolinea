using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Tripulacion
{
    [Key]
    public int IdTripulacion { get; set; }

    [ForeignKey("VueloInfo")]
    public int IdInfo { get; set; }

    [Required]
    [StringLength(100)]
    public string Nombre { get; set; }
    [Required]
    [StringLength(100)]
    public string Rol { get; set; }

    public VueloInfo VueloInfo { get; set; }
}