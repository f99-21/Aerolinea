using Aerolinea.Models;
using Microsoft.AspNetCore.Mvc;

namespace Aerolinea.Controllers
{
    public class pasajerosController1 : Controller
    {
        private readonly AerolineaDBContext _context;

        public pasajerosController1(AerolineaDBContext context)
        {
            _context = context;
        }
        
           public IActionResult DetallesVuelos()
           {
            var datos = (from pa in _context.pasajero
                         join l in _context.login on pa.id_login equals l.id_login
                         join p in _context.pago_vuelo on pa.id_pasajero equals p.id_usuario
                         join v in _context.Vuelo on p.id_vuelo equals v.id_vuelo
                         join t in _context.tarifas on p.id_tarifas equals t.Id_Tarifas
                         select new PasajeroVueloViewModel
                         {
                             Nombre = l.nombre,
                             Apellido = l.apellido,
                             Documento = pa.documento,
                             NumeroVuelo = v.Numero_vuelo,
                             Origen = v.Origen,
                             Destino = v.Destino,
                             Precio = (decimal)v.Precio,
                             Horario = v.Horario,
                             TipoTarifa = t.Tipo,
                             DescripcionTarifa = t.Descripcion,
                             Total = (decimal)p.total
                         }).ToList();

            return View(datos);
            }
    }
}
