using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aerolinea.Models
{
    [Table("vuelo_info")]
    public class VueloInfo
    {
        [Key]
        [Column("id_info")]
        public int id_info { get; set; }

        [Column("numeroVuelo")]
        public int numeroVuelo { get; set; }

        [ForeignKey("Avion")]
        [Column("id_avion")]
        public int id_avion { get; set; }

        [Column("espacios_ocupados")]
        public int espacios_ocupados { get; set; }

        [Column("espacios_vacios")]
        public int espacios_vacios { get; set; }

        public Avion Avion { get; set; }
    }
}
