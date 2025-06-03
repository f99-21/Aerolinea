
namespace Aerolinea.Models
{
    public class referenicia
    {
        public int id_reserva { get; set; }
        public DateTime fecha_hora { get; set; }
        public int id_pago { get; set; }
        public decimal total { get; set; }
        public string documento { get; set; }
        public int numero_vuelo { get; set; }
        public string origen { get; set; }
        public string destino { get; set; }
    }
}
