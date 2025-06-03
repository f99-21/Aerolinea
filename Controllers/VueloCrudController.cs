using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Aerolinea.Models;
using System.Threading.Tasks;

public class VueloCrudController : Controller
{
    private readonly AerolineaDBContext _context;

    public VueloCrudController(AerolineaDBContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _context.Vuelos.ToListAsync());
    }

    public IActionResult Crear()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Crear(Vuelo vuelo)
    {
        if (ModelState.IsValid)
        {
            _context.Vuelos.Add(vuelo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(vuelo);
    }

    public async Task<IActionResult> Editar(int id)
    {
        var vuelo = await _context.Vuelos.FindAsync(id);
        if (vuelo == null) return NotFound();
        return View(vuelo);
    }

    [HttpPost]
    public async Task<IActionResult> Editar(Vuelo vuelo)
    {
        if (ModelState.IsValid)
        {
            _context.Vuelos.Update(vuelo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(vuelo);
    }

    public async Task<IActionResult> Eliminar(int id)
    {
        var vuelo = await _context.Vuelos.FindAsync(id);
        if (vuelo == null) return NotFound();
        return View(vuelo);
    }

    [HttpPost, ActionName("Eliminar")]
    public async Task<IActionResult> ConfirmarEliminar(int id)
    {
        var vuelo = await _context.Vuelos.FindAsync(id);
        _context.Vuelos.Remove(vuelo);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}