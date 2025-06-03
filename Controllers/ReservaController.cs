using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Aerolinea.Models;
using System.Threading.Tasks;

public class ReservaController : Controller
{
    private readonly AerolineaDBContext _context;

    public ReservaController(AerolineaDBContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _context.Reservas.ToListAsync());
    }

    public IActionResult Crear() => View();

    [HttpPost]
    public async Task<IActionResult> Crear(Reserva entidad)
    {
        if (ModelState.IsValid)
        {
            _context.Reservas.Add(entidad);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(entidad);
    }

    public async Task<IActionResult> Editar(int id)
    {
        var entidad = await _context.Reservas.FindAsync(id);
        if (entidad == null) return NotFound();
        return View(entidad);
    }

    [HttpPost]
    public async Task<IActionResult> Editar(Reserva entidad)
    {
        if (ModelState.IsValid)
        {
            _context.Reservas.Update(entidad);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(entidad);
    }

    public async Task<IActionResult> Eliminar(int id)
    {
        var entidad = await _context.Reservas.FindAsync(id);
        if (entidad == null) return NotFound();
        return View(entidad);
    }

    [HttpPost, ActionName("Eliminar")]
    public async Task<IActionResult> ConfirmarEliminar(int id)
    {
        var entidad = await _context.Reservas.FindAsync(id);
        _context.Reservas.Remove(entidad);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}