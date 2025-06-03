using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Aerolinea.Models;
using System.Linq;
using System.Threading.Tasks;

public class LoginController : Controller
{
    private readonly AerolineaDBContext _context;

    public LoginController(AerolineaDBContext context)
    {
        _context = context;
    }

    // Mostrar formulario de login
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    // Validar login
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Index(string correo, string contraseña)
    {
        if (string.IsNullOrEmpty(correo) || string.IsNullOrEmpty(contraseña))
        {
            ViewBag.Error = "Debe ingresar correo y contraseña.";
            return View();
        }

        correo = correo?.Trim().ToLower();
        contraseña = contraseña?.Trim();

        var usuario = await _context.login
            .FirstOrDefaultAsync(u =>
                u.correo.ToLower() == correo &&
                u.contraseña == contraseña);

        if (usuario == null)
        {
            ViewBag.Error = "Usuario o contraseña incorrectos.";
            return View();
        }

        HttpContext.Session.SetString("UserEmail", usuario.correo);
        HttpContext.Session.SetString("UserRole", usuario.rol);

        if (!string.IsNullOrEmpty(usuario.rol))
        {
            if (usuario.rol.ToLower() == "admi")
            {
                return RedirectToAction("Dashboard", "Admi");
            }
            else if (usuario.rol.ToLower() == "pasajero")
            {
                return RedirectToAction("BuscarVuelo", "Usuario");
            }
            else
            {
                ViewBag.Error = "Rol desconocido.";
                return View();
            }
        }
        else
        {
            ViewBag.Error = "El rol del usuario no está definido.";
            return View();
        }

        //return View();
    }

    // Cerrar sesión
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index");
    }

    // CRUD: Lista
    public async Task<IActionResult> Lista()
    {
        return View(await _context.login.ToListAsync());
    }

    // CRUD: Detalles
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();
        var item = await _context.login.FindAsync(id);
        if (item == null) return NotFound();
        return View(item);
    }

    // CRUD: Crear
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Login obj)
    {
        if (ModelState.IsValid)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Lista));
        }
        return View(obj);
    }

    // CRUD: Editar
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();
        var item = await _context.login.FindAsync(id);
        if (item == null) return NotFound();
        return View(item);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Login obj)
    {
        if (id != obj.id_login) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.login.Any(e => e.id_login == id)) return NotFound();
                else throw;
            }
            return RedirectToAction(nameof(Lista));
        }
        return View(obj);
    }

    // CRUD: Eliminar
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();
        var item = await _context.login.FindAsync(id);
        if (item == null) return NotFound();
        return View(item);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var item = await _context.login.FindAsync(id);
        _context.login.Remove(item);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Lista));
    }
}
