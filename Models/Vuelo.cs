using System.ComponentModel.DataAnnotations;

public class Vuelo
{
    [Key]
    public int IdVuelo { get; set; }
    [Required]
    public int NumeroVuelo { get; set; }
    [Required]
    [StringLength(100)]
    public string Origen { get; set; }
    [Required]
    [StringLength(100)]
    public string Destino { get; set; }
    [Required]
    [StringLength(100)]
    public string Escala { get; set; }
    [Required]
    [Range(0.01, 1000000)]
    public float Precio { get; set; }
    public DateTime Horario { get; set; }
}