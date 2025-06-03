using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Aerolinea.Models;
using System.Threading.Tasks;

public class AvionController : Controller
{
    private readonly AerolineaDBContext _context;

    public AvionController(AerolineaDBContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _context.Avions.ToListAsync());
    }

    public IActionResult Crear() => View();

    [HttpPost]
    public async Task<IActionResult> Crear(Avion entidad)
    {
        if (ModelState.IsValid)
        {
            _context.Avions.Add(entidad);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(entidad);
    }

    public async Task<IActionResult> Editar(int id)
    {
        var entidad = await _context.Avions.FindAsync(id);
        if (entidad == null) return NotFound();
        return View(entidad);
    }

    [HttpPost]
    public async Task<IActionResult> Editar(Avion entidad)
    {
        if (ModelState.IsValid)
        {
            _context.Avions.Update(entidad);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(entidad);
    }

    public async Task<IActionResult> Eliminar(int id)
    {
        var entidad = await _context.Avions.FindAsync(id);
        if (entidad == null) return NotFound();
        return View(entidad);
    }

    [HttpPost, ActionName("Eliminar")]
    public async Task<IActionResult> ConfirmarEliminar(int id)
    {
        var entidad = await _context.Avions.FindAsync(id);
        _context.Avions.Remove(entidad);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}