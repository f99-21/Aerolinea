using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aerolinea.Models
{
    public class Admi
    {
        [Key]
        public int id_admi { get; set; }
        
        [ForeignKey("Login")]
        public int id_login { get; set; }

        public string Contraseña { get; set; }

        // Si deseas agregar una relación de navegación con la tabla Login
         public Login Login { get; set; }
    }
}
