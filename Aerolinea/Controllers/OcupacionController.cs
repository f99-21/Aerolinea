using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Aerolinea.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Aerolinea.Controllers
{
    public class OcupacionController : Controller
    {
        private readonly AerolineaDBContext _context;

        public OcupacionController(AerolineaDBContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var vuelos = await (
                from vi in _context.vuelo_info
                join a in _context.avion on vi.id_avion equals a.id_avion
                join v in _context.Vuelo on vi.numeroVuelo equals v.Numero_vuelo
                select new estado
                {
                    numero_vuelo = vi.numeroVuelo,
                    modelo = a.modelo,
                    capacidad = a.capacidad,
                    origen = v.Origen,
                    destino = v.Destino,
                    horario = v.Horario,
                    espacios_ocupados = vi.espacios_ocupados,
                    espacios_vacios = vi.espacios_vacios
                }).ToListAsync();

            return View(vuelos);
        }
    }
}
