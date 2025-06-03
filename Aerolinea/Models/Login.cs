using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aerolinea.Models
{
    [Table("login")]
    public class Login
    {
        [Key]
        public int id_login { get; set; }

        [Required]
        public string nombre { get; set; }

        [Required]
        public string apellido { get; set; }

        [Required]
        [EmailAddress]
        public string correo { get; set; }

        [Required]
        [MinLength(8)]
        public string contraseña { get; set; }

        public string rol { get; set; }
    }

}
