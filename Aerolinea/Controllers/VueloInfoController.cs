using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Aerolinea.Models;
using System.Linq;
using System.Threading.Tasks;

public class VueloInfoController : Controller
{
    private readonly AerolineaDBContext _context;

    public VueloInfoController(AerolineaDBContext context)
    {
        _context = context;
    }

    // GET: VueloInfo
    public async Task<IActionResult> Index()
    {
        var info = await _context.vuelo_info.ToListAsync();
        return View(info);
    }

    // GET: VueloInfo/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var item = await _context.vuelo_info
            .FirstOrDefaultAsync(v => v.id_info == id);

        if (item == null) return NotFound();

        return View(item);
    }

    // GET: VueloInfo/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: VueloInfo/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(VueloInfo obj)
    {
        if (ModelState.IsValid)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        return View(obj);
    }

    // GET: VueloInfo/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var item = await _context.vuelo_info.FindAsync(id);
        if (item == null) return NotFound();

        return View(item);
    }

    // POST: VueloInfo/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, VueloInfo obj)
    {
        if (id != obj.id_info) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.vuelo_info.Any(e => e.id_info == id)) return NotFound();
                else throw;
            }
            return RedirectToAction(nameof(Index));
        }

        return View(obj);
    }

    // GET: VueloInfo/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var item = await _context.vuelo_info
            .FirstOrDefaultAsync(v => v.id_info == id);

        if (item == null) return NotFound();

        return View(item);
    }

    // POST: VueloInfo/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var item = await _context.vuelo_info.FindAsync(id);
        _context.vuelo_info.Remove(item);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
