using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Pasajero
{
    [Key]
    public int IdPasajero { get; set; }

    [ForeignKey("Login")]
    public int IdLogin { get; set; }

    [Required]
    [StringLength(100)]
    public string Documento { get; set; }

    public Login Login { get; set; }
}