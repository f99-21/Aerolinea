using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Aerolinea.Models;
using System.Threading.Tasks;
using System.Linq;

public class AvionController : Controller
{
    private readonly AerolineaDBContext _context;

    public AvionController(AerolineaDBContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _context.avion.ToListAsync());
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var avion = await _context.avion.FirstOrDefaultAsync(a => a.id_avion == id);
        if (avion == null) return NotFound();

        return View(avion);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Avion avion)
    {
        if (ModelState.IsValid)
        {
            _context.Add(avion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(avion);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var avion = await _context.avion.FindAsync(id);
        if (avion == null) return NotFound();

        return View(avion);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Avion avion)
    {
        if (id != avion.id_avion) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(avion);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.avion.Any(a => a.id_avion == id)) return NotFound();
                else throw;
            }
            return RedirectToAction(nameof(Index));
        }
        return View(avion);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var avion = await _context.avion.FirstOrDefaultAsync(a => a.id_avion == id);
        if (avion == null) return NotFound();

        return View(avion);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var avion = await _context.avion.FindAsync(id);
        _context.avion.Remove(avion);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
