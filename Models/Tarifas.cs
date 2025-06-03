using System.ComponentModel.DataAnnotations;

public class Tarifas
{
    [Key]
    public int IdTarifas { get; set; }
    [Required]
    [StringLength(100)]
    public string Tipo { get; set; }
    [Required]
    [StringLength(100)]
    public string Descripcion { get; set; }
    [Required]
    [Range(0.01, 1000000)]
    public float Precio { get; set; }
}