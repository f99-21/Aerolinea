using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aerolinea.Models
{
    [Table("avion")] 
    public class Avion
    {
        [Key]
        [Column("id_avion")]
        public int id_avion { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("modelo")]
        public string modelo { get; set; }

        [Required]
        [Column("capacidad")]
        public string capacidad { get; set; } 
    }
}
