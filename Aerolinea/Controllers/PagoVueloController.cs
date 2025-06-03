using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Aerolinea.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;

public class PagoVueloController : Controller
{
    private readonly AerolineaDBContext _context;

    public PagoVueloController(AerolineaDBContext context)
    {
        _context = context;
    }

    // GET: PagoVuelo
    public async Task<IActionResult> Index()
    {
        var pagos = _context.pago_vuelo
            .Include(p => p.Pasajero)
            .Include(p => p.Vuelo)
            .Include(p => p.Tarifa);

        return View(await pagos.ToListAsync());
    }

    // GET: PagoVuelo/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var pago = await _context.pago_vuelo
            .Include(p => p.Pasajero)
            .Include(p => p.Vuelo)
            .Include(p => p.Tarifa)
            .FirstOrDefaultAsync(p => p.id_pago == id);

        if (pago == null) return NotFound();

        return View(pago);
    }

    // GET: PagoVuelo/Create
    public IActionResult Create()
    {
        ViewData["id_usuario"] = new SelectList(_context.pasajero, "id_pasajero", "documento");
        ViewData["id_vuelo"] = new SelectList(_context.Vuelo, "id_vuelo", "numero_vuelo");
        ViewData["id_tarifas"] = new SelectList(_context.tarifas, "id_tarifas", "tipo");
        return View();
    }

    // POST: PagoVuelo/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Pago_Vuelo obj)
    {
        if (ModelState.IsValid)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewData["id_usuario"] = new SelectList(_context.pasajero, "id_pasajero", "documento", obj.id_usuario);
        ViewData["id_vuelo"] = new SelectList(_context.Vuelo, "id_vuelo", "numero_vuelo", obj.id_vuelo);
        ViewData["id_tarifas"] = new SelectList(_context.tarifas, "id_tarifas", "tipo", obj.id_tarifas);
        return View(obj);
    }

    // GET: PagoVuelo/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var pago = await _context.pago_vuelo.FindAsync(id);
        if (pago == null) return NotFound();

        ViewData["id_usuario"] = new SelectList(_context.pasajero, "id_pasajero", "documento", pago.id_usuario);
        ViewData["id_vuelo"] = new SelectList(_context.Vuelo, "id_vuelo", "numero_vuelo", pago.id_vuelo);
        ViewData["id_tarifas"] = new SelectList(_context.tarifas, "id_tarifas", "tipo", pago.id_tarifas);
        return View(pago);
    }

    // POST: PagoVuelo/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Pago_Vuelo obj)
    {
        if (id != obj.id_pago) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.pago_vuelo.Any(e => e.id_pago == id)) return NotFound();
                else throw;
            }
            return RedirectToAction(nameof(Index));
        }

        ViewData["id_usuario"] = new SelectList(_context.pasajero, "id_pasajero", "documento", obj.id_usuario);
        ViewData["id_vuelo"] = new SelectList(_context.Vuelo, "id_vuelo", "numero_vuelo", obj.id_vuelo);
        ViewData["id_tarifas"] = new SelectList(_context.tarifas, "id_tarifas", "tipo", obj.id_tarifas);
        return View(obj);
    }

    // GET: PagoVuelo/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var pago = await _context.pago_vuelo
            .Include(p => p.Pasajero)
            .Include(p => p.Vuelo)
            .Include(p => p.Tarifa)
            .FirstOrDefaultAsync(p => p.id_pago == id);

        if (pago == null) return NotFound();

        return View(pago);
    }

    // POST: PagoVuelo/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var pago = await _context.pago_vuelo.FindAsync(id);
        _context.pago_vuelo.Remove(pago);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
