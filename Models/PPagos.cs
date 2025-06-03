using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class PPagos
{
    [Key]
    public int IdPago { get; set; }
    public string Detalle { get; set; }
    public float Total { get; set; }

    [ForeignKey("Admi")]
    public int IdAdmi { get; set; }

    public string MetodoPago { get; set; }

    public Admi Admi { get; set; }
}