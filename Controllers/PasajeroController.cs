using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Aerolinea.Models;
using System.Threading.Tasks;

public class PasajeroController : Controller
{
    private readonly AerolineaDBContext _context;

    public PasajeroController(AerolineaDBContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _context.Pasajeros.ToListAsync());
    }

    public IActionResult Crear() => View();

    [HttpPost]
    public async Task<IActionResult> Crear(Pasajero entidad)
    {
        if (ModelState.IsValid)
        {
            _context.Pasajeros.Add(entidad);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(entidad);
    }

    public async Task<IActionResult> Editar(int id)
    {
        var entidad = await _context.Pasajeros.FindAsync(id);
        if (entidad == null) return NotFound();
        return View(entidad);
    }

    [HttpPost]
    public async Task<IActionResult> Editar(Pasajero entidad)
    {
        if (ModelState.IsValid)
        {
            _context.Pasajeros.Update(entidad);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(entidad);
    }

    public async Task<IActionResult> Eliminar(int id)
    {
        var entidad = await _context.Pasajeros.FindAsync(id);
        if (entidad == null) return NotFound();
        return View(entidad);
    }

    [HttpPost, ActionName("Eliminar")]
    public async Task<IActionResult> ConfirmarEliminar(int id)
    {
        var entidad = await _context.Pasajeros.FindAsync(id);
        _context.Pasajeros.Remove(entidad);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}