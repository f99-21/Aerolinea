using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class AdminController : Controller
{
    private readonly ApplicationDbContext _context;

    public AdminController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Panel() => View();

    public async Task<IActionResult> Vuelos()
    {
        var vuelos = await _context.Vuelos.ToListAsync();
        return View(vuelos);
    }

    public async Task<IActionResult> Pagos()
    {
        var pagos = await _context.Pagos.ToListAsync();
        return View(pagos);
    }
}