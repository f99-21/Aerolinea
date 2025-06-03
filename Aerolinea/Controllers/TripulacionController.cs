using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Aerolinea.Models;
using System.Linq;
using System.Threading.Tasks;

public class TripulacionController : Controller
{
    private readonly AerolineaDBContext _context;

    public TripulacionController(AerolineaDBContext context)
    {
        _context = context;
    }

    // GET: Tripulacion
    public async Task<IActionResult> Index()
    {
        var tripulacion = await _context.tripulacion
            .Include(t => t.vuelo_info)
            .ToListAsync();

        return View(tripulacion);
    }

    // GET: Tripulacion/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var tripulante = await _context.tripulacion
            .Include(t => t.vuelo_info)
            .FirstOrDefaultAsync(t => t.id_tripulacion == id);

        if (tripulante == null) return NotFound();

        return View(tripulante);
    }

    // GET: Tripulacion/Create
    public IActionResult Create()
    {
        ViewData["id_info"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(_context.vuelo_info, "id_info", "id_info");
        return View();
    }

    // POST: Tripulacion/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Tripulacion tripulacion)
    {
        if (ModelState.IsValid)
        {
            _context.Add(tripulacion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewData["id_info"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(_context.vuelo_info, "id_info", "id_info", tripulacion.id_info);
        return View(tripulacion);
    }

    // GET: Tripulacion/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var tripulacion = await _context.tripulacion.FindAsync(id);
        if (tripulacion == null) return NotFound();

        ViewData["id_info"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(_context.vuelo_info, "id_info", "id_info", tripulacion.id_info);
        return View(tripulacion);
    }

    // POST: Tripulacion/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Tripulacion tripulacion)
    {
        if (id != tripulacion.id_tripulacion) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(tripulacion);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.tripulacion.Any(t => t.id_tripulacion == id)) return NotFound();
                else throw;
            }

            return RedirectToAction(nameof(Index));
        }

        ViewData["id_info"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(_context.vuelo_info, "id_info", "id_info", tripulacion.id_info);
        return View(tripulacion);
    }

    // GET: Tripulacion/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var tripulacion = await _context.tripulacion
            .Include(t => t.vuelo_info)
            .FirstOrDefaultAsync(t => t.id_tripulacion == id);

        if (tripulacion == null) return NotFound();

        return View(tripulacion);
    }

    // POST: Tripulacion/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var tripulacion = await _context.tripulacion.FindAsync(id);
        _context.tripulacion.Remove(tripulacion);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
