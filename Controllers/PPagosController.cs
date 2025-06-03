using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Aerolinea.Models;
using System.Threading.Tasks;

public class PPagosController : Controller
{
    private readonly AerolineaDBContext _context;

    public PPagosController(AerolineaDBContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _context.PPagoss.ToListAsync());
    }

    public IActionResult Crear() => View();

    [HttpPost]
    public async Task<IActionResult> Crear(PPagos entidad)
    {
        if (ModelState.IsValid)
        {
            _context.PPagoss.Add(entidad);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(entidad);
    }

    public async Task<IActionResult> Editar(int id)
    {
        var entidad = await _context.PPagoss.FindAsync(id);
        if (entidad == null) return NotFound();
        return View(entidad);
    }

    [HttpPost]
    public async Task<IActionResult> Editar(PPagos entidad)
    {
        if (ModelState.IsValid)
        {
            _context.PPagoss.Update(entidad);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(entidad);
    }

    public async Task<IActionResult> Eliminar(int id)
    {
        var entidad = await _context.PPagoss.FindAsync(id);
        if (entidad == null) return NotFound();
        return View(entidad);
    }

    [HttpPost, ActionName("Eliminar")]
    public async Task<IActionResult> ConfirmarEliminar(int id)
    {
        var entidad = await _context.PPagoss.FindAsync(id);
        _context.PPagoss.Remove(entidad);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}