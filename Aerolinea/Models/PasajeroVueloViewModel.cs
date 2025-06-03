using Aerolinea.Models;


namespace Aerolinea.Models

{
    public class PasajeroVueloViewModel
    {
        // Datos del pasajero
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Documento { get; set; }

        // Información del vuelo
        public int NumeroVuelo { get; set; }
        public string Origen { get; set; }
        public string Destino { get; set; }
        public decimal Precio { get; set; }
        public DateTime Horario { get; set; }

        // Información de tarifa
        public string TipoTarifa { get; set; }
        public string DescripcionTarifa { get; set; }
        public decimal Total { get; set; }
    }
}
