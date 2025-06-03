using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class VueloController : Controller
{
    private readonly ApplicationDbContext _context;

    public VueloController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var vuelos = await _context.Vuelos.ToListAsync();
        return View(vuelos);
    }

    public async Task<IActionResult> Details(int id)
    {
        var vuelo = await _context.Vuelos.FindAsync(id);
        if (vuelo == null)
            return NotFound();

        return View(vuelo);
    }
}