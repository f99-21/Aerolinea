using System;
using System.ComponentModel.DataAnnotations;

namespace Aerolinea.Models
{
    public class Vuelo
    {
        [Key]
        public int id_vuelo { get; set; }
        public int Numero_vuelo { get; set; }
        public string Origen { get; set; }
        public string Destino { get; set; }
        public string Escala { get; set; }
        public decimal Precio { get; set; }
        public DateTime Horario { get; set; }
    }
}
