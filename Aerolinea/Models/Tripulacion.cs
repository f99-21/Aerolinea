using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aerolinea.Models
{
    [Table("tripulacion")] 
    public class Tripulacion
    {
        [Key]
        [Column("id_tripulacion")]
        public int id_tripulacion { get; set; }

        [ForeignKey("vuelo_info")]
        [Column("id_info")]
        public int id_info { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("nombre")]
        public string nombre { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("rol")]
        public string rol { get; set; }

        public VueloInfo vuelo_info { get; set; }
    }
}
