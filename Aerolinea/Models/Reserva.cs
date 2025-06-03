using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aerolinea.Models
{
    [Table("reserva")]
    public class Reserva
    {
        [Key]
        [Column("id_reserva")]
        public int id_reserva { get; set; }

        [ForeignKey("Pago")]
        [Column("id_pago")]
        public int id_pago { get; set; }

        [Column("fecha_hora")]
        public DateTime fecha_hora { get; set; }
       
       
        public virtual Pago_Vuelo Pago { get; set; }
       
    }
}
