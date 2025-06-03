namespace Aerolinea.Models
{
    public class estado
    {
        public int numero_vuelo { get; set; }
        public string modelo { get; set; }
        public string capacidad { get; set; }
        public string origen { get; set; }
        public string destino { get; set; }
        public DateTime horario { get; set; }
        public int espacios_ocupados { get; set; }
        public int espacios_vacios { get; set; }
    }
}
