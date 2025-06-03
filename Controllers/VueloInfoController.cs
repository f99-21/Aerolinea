using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Aerolinea.Models;
using System.Threading.Tasks;

public class VueloInfoController : Controller
{
    private readonly AerolineaDBContext _context;

    public VueloInfoController(AerolineaDBContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _context.VueloInfos.ToListAsync());
    }

    public IActionResult Crear() => View();

    [HttpPost]
    public async Task<IActionResult> Crear(VueloInfo entidad)
    {
        if (ModelState.IsValid)
        {
            _context.VueloInfos.Add(entidad);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(entidad);
    }

    public async Task<IActionResult> Editar(int id)
    {
        var entidad = await _context.VueloInfos.FindAsync(id);
        if (entidad == null) return NotFound();
        return View(entidad);
    }

    [HttpPost]
    public async Task<IActionResult> Editar(VueloInfo entidad)
    {
        if (ModelState.IsValid)
        {
            _context.VueloInfos.Update(entidad);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(entidad);
    }

    public async Task<IActionResult> Eliminar(int id)
    {
        var entidad = await _context.VueloInfos.FindAsync(id);
        if (entidad == null) return NotFound();
        return View(entidad);
    }

    [HttpPost, ActionName("Eliminar")]
    public async Task<IActionResult> ConfirmarEliminar(int id)
    {
        var entidad = await _context.VueloInfos.FindAsync(id);
        _context.VueloInfos.Remove(entidad);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}