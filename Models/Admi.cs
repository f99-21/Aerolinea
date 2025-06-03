using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Admi
{
    [Key]
    public int IdAdmi { get; set; }

    [ForeignKey("Login")]
    public int IdLogin { get; set; }

    public string Contraseña { get; set; }

    public Login Login { get; set; }
}