using System.ComponentModel.DataAnnotations;

public class Login
{
    [Key]
    public int IdLogin { get; set; }
    [Required]
    [StringLength(100)]
    public string Nombre { get; set; }
    [Required]
    [StringLength(100)]
    public string Apellido { get; set; }
    public string Correo { get; set; }
    [Required]
    [StringLength(100)]
    public string Rol { get; set; }
}