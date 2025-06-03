using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Aerolinea.Models;
using System.Linq;
using System.Threading.Tasks;

public class PagoController : Controller
{
    private readonly AerolineaDBContext _context;

    public PagoController(AerolineaDBContext context)
    {
        _context = context;
    }

    // GET: Pago
    public async Task<IActionResult> Index()
    {
        var pagos = await _context.ppagos
            .Include(p => p.Admi)
            .ToListAsync();

        return View(pagos);
    }

    // GET: Pago/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var pago = await _context.ppagos
            .Include(p => p.Admi)
            .FirstOrDefaultAsync(p => p.id_pago == id);

        if (pago == null) return NotFound();

        return View(pago);
    }

    // GET: Pago/Create
    public IActionResult Create()
    {
        ViewData["IdAdmi"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(_context.Admi, "id_admi", "id_admi");
        return View();
    }

    // POST: Pago/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Pago pago)
    {
        if (ModelState.IsValid)
        {
            _context.Add(pago);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewData["IdAdmi"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(_context.Admi, "id_admi", "id_admi", pago.id_admi);
        return View(pago);
    }

    // GET: Pago/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var pago = await _context.ppagos.FindAsync(id);
        if (pago == null) return NotFound();

        ViewData["IdAdmi"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(_context.Admi, "id_admi", "id_admi", pago.id_admi);
        return View(pago);
    }

    // POST: Pago/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Pago pago)
    {
        if (id != pago.id_pago) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(pago);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.ppagos.Any(p => p.id_pago == id)) return NotFound();
                else throw;
            }
            return RedirectToAction(nameof(Index));
        }

        ViewData["IdAdmi"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(_context.Admi, "id_admi", "id_admi", pago.id_admi);
        return View(pago);
    }

    // GET: Pago/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var pago = await _context.ppagos
            .Include(p => p.Admi)
            .FirstOrDefaultAsync(p => p.id_pago == id);

        if (pago == null) return NotFound();

        return View(pago);
    }

    // POST: Pago/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var pago = await _context.ppagos.FindAsync(id);
        _context.ppagos.Remove(pago);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
