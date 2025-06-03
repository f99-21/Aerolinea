using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aerolinea.Models
{
    public class Tarifa
    {
        [Key]
        [Column("id_tarifas")]
        public int Id_Tarifas { get; set; }

        [Required]
        [MaxLength(50)]
        public string Tipo { get; set; }

        [Required]
        [MaxLength(200)]
        public string Descripcion { get; set; }

        [Required]
        [Range(0, 9999.99)]
        public double Precio { get; set; } 

    }
}
