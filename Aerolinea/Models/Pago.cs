using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aerolinea.Models
{
    public class Pago
    {
        [Key]
        [Column("id_pago")]
        public int id_pago { get; set; }

        [Required]
        [MaxLength(200)]
        public string detalle { get; set; }

        [Column("total")]
        public double total { get; set; }


        [Required]
        [ForeignKey("Admi")]
        public int id_admi { get; set; }

        [Required]
        [MaxLength(50)]
        public string metodo_pago { get; set; }

        public Admi Admi { get; set; }
    }
}
