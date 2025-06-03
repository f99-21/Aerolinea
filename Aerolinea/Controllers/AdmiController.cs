using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Aerolinea.Models;
using System.Linq;
using System.Threading.Tasks;

public class AdmiController : Controller
{
    private readonly AerolineaDBContext _context;

    public AdmiController(AerolineaDBContext context)
    {
        _context = context;
    }

    // GET: Admin/Index
    public async Task<IActionResult> Index()
    {
        // Obtener todos los administradores con su relación con Login
        var administradores = await _context.Admi
            .Include(a => a.Login)  // Incluir Login asociado a Admi
            .ToListAsync();
        return View(administradores);
    }

    // GET: Admin/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        // Buscar el administrador por su id con su relación con Login
        var admi = await _context.Admi
            .Include(a => a.Login)
            .FirstOrDefaultAsync(a => a.id_admi == id);

        if (admi == null) return NotFound();

        return View(admi);
    }

    // GET: Admin/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Admin/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Admi admi)
    {
        if (ModelState.IsValid)
        {
            _context.Add(admi);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(admi);
    }

    // GET: Admin/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        // Buscar el administrador por su id
        var admi = await _context.Admi.FindAsync(id);
        if (admi == null) return NotFound();

        return View(admi);
    }

    // POST: Admin/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Admi admi)
    {
        if (id != admi.id_admi) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(admi);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Admi.Any(e => e.id_admi == id)) return NotFound();
                else throw;
            }
            return RedirectToAction(nameof(Index));
        }
        return View(admi);
    }

    // GET: Admin/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var admi = await _context.Admi
            .FirstOrDefaultAsync(m => m.id_admi == id);

        if (admi == null) return NotFound();

        return View(admi);
    }

    // POST: Admin/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var admi = await _context.Admi.FindAsync(id);
        _context.Admi.Remove(admi);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Dashboard()
    {
        return View();
    }

}
