using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class VueloInfo
{
    [Key]
    public int IdInfo { get; set; }
    public int NumeroVuelo { get; set; }

    [ForeignKey("Avion")]
    public int IdAvion { get; set; }
    public int EspaciosOcupados { get; set; }
    public int EspaciosVacios { get; set; }

    public Avion Avion { get; set; }
}