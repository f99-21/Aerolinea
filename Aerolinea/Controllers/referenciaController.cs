using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Aerolinea.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Aerolinea.Controllers
{
    public class referenciaController : Controller
    {
        private readonly AerolineaDBContext _context;

        public referenciaController(AerolineaDBContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var reservas = await (
                from r in _context.reserva
                join p in _context.pago_vuelo on r.id_pago equals p.id_pago
                join pa in _context.pasajero on p.id_usuario equals pa.id_pasajero
                join v in _context.Vuelo on p.id_vuelo equals v.id_vuelo
                select new referenicia
                {
                    id_reserva = r.id_reserva,
                    fecha_hora = r.fecha_hora,
                    id_pago = p.id_pago,
                    total = (decimal)p.total,
                    documento = pa.documento,
                    numero_vuelo = v.Numero_vuelo,
                    origen = v.Origen,
                    destino = v.Destino
                }).ToListAsync();

            return View(reservas);
        }
    }
}
