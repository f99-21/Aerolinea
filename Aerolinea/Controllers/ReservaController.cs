using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Aerolinea.Models;
using System.Linq;
using System.Threading.Tasks;

public class ReservaController : Controller
{
    private readonly AerolineaDBContext _context;

    public ReservaController(AerolineaDBContext context)
    {
        _context = context;
    }

    // GET: Reserva
    public async Task<IActionResult> Index()
    {
        var reservas = _context.reserva.Include(r => r.Pago);
        return View(await reservas.ToListAsync());
    }

    // GET: Reserva/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var reserva = await _context.reserva
            .Include(r => r.Pago)
            .FirstOrDefaultAsync(m => m.id_reserva == id);

        if (reserva == null) return NotFound();

        return View(reserva);
    }

    // GET: Reserva/Create
    public IActionResult Create()
    {
        ViewData["id_pago"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(_context.pago_vuelo, "id_pago", "id_pago");
        return View();
    }

    // POST: Reserva/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Reserva reserva)
    {
        if (ModelState.IsValid)
        {
            _context.Add(reserva);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewData["id_pago"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(_context.pago_vuelo, "id_pago", "id_pago", reserva.id_pago);
        return View(reserva);
    }

    // GET: Reserva/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var reserva = await _context.reserva.FindAsync(id);
        if (reserva == null) return NotFound();

        ViewData["id_pago"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(_context.pago_vuelo, "id_pago", "id_pago", reserva.id_pago);
        return View(reserva);
    }

    // POST: Reserva/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Reserva reserva)
    {
        if (id != reserva.id_reserva) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(reserva);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.reserva.Any(e => e.id_reserva == id)) return NotFound();
                else throw;
            }

            return RedirectToAction(nameof(Index));
        }

        ViewData["id_pago"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(_context.pago_vuelo, "id_pago", "id_pago", reserva.id_pago);
        return View(reserva);
    }

    // GET: Reserva/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var reserva = await _context.reserva
            .Include(r => r.Pago)
            .FirstOrDefaultAsync(m => m.id_reserva == id);

        if (reserva == null) return NotFound();

        return View(reserva);
    }

    // POST: Reserva/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var reserva = await _context.reserva.FindAsync(id);
        _context.reserva.Remove(reserva);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
