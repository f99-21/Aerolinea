using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Aerolinea.Models;
using System.Linq;
using System.Threading.Tasks;

public class TarifaController : Controller
{
    private readonly AerolineaDBContext _context;

    public TarifaController(AerolineaDBContext context)
    {
        _context = context;
    }

    // GET: Tarifa
    public async Task<IActionResult> Index()
    {
        return View(await _context.tarifas.ToListAsync());
    }

    // GET: Tarifa/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var tarifa = await _context.tarifas
            .FirstOrDefaultAsync(t => t.Id_Tarifas == id);

        if (tarifa == null) return NotFound();

        return View(tarifa);
    }

    // GET: Tarifa/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Tarifa/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Tarifa tarifa)
    {
        if (ModelState.IsValid)
        {
            _context.Add(tarifa);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(tarifa);
    }

    // GET: Tarifa/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var tarifa = await _context.tarifas.FindAsync(id);
        if (tarifa == null) return NotFound();

        return View(tarifa);
    }

    // POST: Tarifa/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Tarifa tarifa)
    {
        if (id != tarifa.Id_Tarifas) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(tarifa);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.tarifas.Any(t => t.Id_Tarifas == id)) return NotFound();
                else throw;
            }
            return RedirectToAction(nameof(Index));
        }
        return View(tarifa);
    }

    // GET: Tarifa/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var tarifa = await _context.tarifas
            .FirstOrDefaultAsync(t => t.Id_Tarifas == id);

        if (tarifa == null) return NotFound();

        return View(tarifa);
    }

    // POST: Tarifa/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var tarifa = await _context.tarifas.FindAsync(id);
        _context.tarifas.Remove(tarifa);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
