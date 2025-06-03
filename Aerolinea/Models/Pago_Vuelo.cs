using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aerolinea.Models
{
    [Table("pago_vuelo")]
    public class Pago_Vuelo
    {

        [Key]
        [Column("id_pago")]
        public int id_pago { get; set; }

        [ForeignKey("Pasajero")]
        [Column("id_usuario")]
        public int id_usuario { get; set; }

       

        [ForeignKey("Vuelo")]
        [Column("id_vuelo")]
        public int id_vuelo { get; set; }

        [ForeignKey("Tarifa")]
        [Column("id_tarifas")]
        public int id_tarifas { get; set; }

        [Column("total")]
        public decimal total { get; set; }



        public Pasajero Pasajero { get; set; }
        public Vuelo Vuelo { get; set; }
        public Tarifa Tarifa { get; set; }
    }
}
