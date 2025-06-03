using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Aerolinea.Models;
using System.Threading.Tasks;

public class TarifasController : Controller
{
    private readonly AerolineaDBContext _context;

    public TarifasController(AerolineaDBContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _context.Tarifas.ToListAsync());
    }

    public IActionResult Crear()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Crear(Tarifas tarifa)
    {
        if (ModelState.IsValid)
        {
            _context.Tarifas.Add(tarifa);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(tarifa);
    }

    public async Task<IActionResult> Editar(int id)
    {
        var tarifa = await _context.Tarifas.FindAsync(id);
        if (tarifa == null) return NotFound();
        return View(tarifa);
    }

    [HttpPost]
    public async Task<IActionResult> Editar(Tarifas tarifa)
    {
        if (ModelState.IsValid)
        {
            _context.Tarifas.Update(tarifa);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(tarifa);
    }

    public async Task<IActionResult> Eliminar(int id)
    {
        var tarifa = await _context.Tarifas.FindAsync(id);
        if (tarifa == null) return NotFound();
        return View(tarifa);
    }

    [HttpPost, ActionName("Eliminar")]
    public async Task<IActionResult> ConfirmarEliminar(int id)
    {
        var tarifa = await _context.Tarifas.FindAsync(id);
        _context.Tarifas.Remove(tarifa);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}