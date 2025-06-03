using System.ComponentModel.DataAnnotations;

public class Avion
{
    [Key]
    public int IdAvion { get; set; }
    [Required]
    [StringLength(100)]
    public string Modelo { get; set; }
    [Required]
    [StringLength(100)]
    public string Capacidad { get; set; }
}