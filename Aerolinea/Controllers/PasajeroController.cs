using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Aerolinea.Models;
using System.Linq;
using System.Threading.Tasks;

public class PasajeroController : Controller
{
    private readonly AerolineaDBContext _context;

    public PasajeroController(AerolineaDBContext context)
    {
        _context = context;
    }

    // GET: Pasajero
    public async Task<IActionResult> Index()
    {
        var pasajeros = await _context.pasajero
            .Include(p => p.Login)
            .Where(p => p.Login != null)
            .ToListAsync();

        return View(pasajeros);
    }


    // GET: Pasajero/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var pasajero = await _context.pasajero
            .Include(p => p.Login)
            .FirstOrDefaultAsync(p => p.id_pasajero == id);

        if (pasajero == null) return NotFound();

        return View(pasajero);
    }

    // GET: Pasajero/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Pasajero/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Pasajero pasajero)
    {
        if (ModelState.IsValid)
        {
            _context.Add(pasajero);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(pasajero);
    }

    // GET: Pasajero/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var pasajero = await _context.pasajero.FindAsync(id);
        if (pasajero == null) return NotFound();

        return View(pasajero);
    }

    // POST: Pasajero/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Pasajero pasajero)
    {
        if (id != pasajero.id_pasajero) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(pasajero);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.pasajero.Any(e => e.id_pasajero == id)) return NotFound();
                else throw;
            }
            return RedirectToAction(nameof(Index));
        }
        return View(pasajero);
    }

    // GET: Pasajero/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var pasajero = await _context.pasajero
            .Include(p => p.Login)
            .FirstOrDefaultAsync(p => p.id_pasajero == id);

        if (pasajero == null) return NotFound();

        return View(pasajero);
    }

    // POST: Pasajero/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var pasajero = await _context.pasajero.FindAsync(id);
        _context.pasajero.Remove(pasajero);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
