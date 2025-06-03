using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aerolinea.Models
{
    public class Pasajero
    {
        [Key]
        public int id_pasajero { get; set; }

        [ForeignKey("Login")]
        public int? id_login { get; set; }

        [Required]
        public string documento { get; set; }

        public Login Login { get; set; }
    }
}
