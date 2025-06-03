using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class LoginController : Controller
{
    private readonly ApplicationDbContext _context;

    public LoginController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index() => View();

    [HttpPost]
    public async Task<IActionResult> Login(string correo, string rol)
    {
        var user = await _context.Logins
            .FirstOrDefaultAsync(u => u.Correo == correo && u.Rol == rol);

        if (user == null)
        {
            ViewBag.Error = "Credenciales inválidas";
            return View("Index");
        }

        if (rol == "admi")
            return RedirectToAction("Panel", "Admin");

        return RedirectToAction("Index", "Vuelo");
    }
}