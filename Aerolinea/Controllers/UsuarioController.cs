using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Aerolinea.Models;
using System.Linq;
using System.Threading.Tasks;
using SelectPdf;

public class UsuarioController : Controller
{
    private readonly AerolineaDBContext _context;

    public UsuarioController(AerolineaDBContext context)
    {
        _context = context;
    }

   
    public IActionResult Index()
    {
        return View();
    }

 
    public async Task<IActionResult> BuscarVuelo()
    {
        var vuelos = await _context.Vuelo.ToListAsync();
        return View(vuelos); 
    }


    public async Task<IActionResult> Seleccionar(int id)
    {
        var vuelo = await _context.Vuelo.FirstOrDefaultAsync(v => v.id_vuelo == id);
        if (vuelo == null) return NotFound();
        return View(vuelo); 


    }

    [HttpPost]
    public async Task<IActionResult> Pagar(int id)
    {
        var vuelo = await _context.Vuelo.FirstOrDefaultAsync(v => v.id_vuelo == id);
        if (vuelo == null) return NotFound();

        return View(vuelo); 
    }
    [HttpPost]
    public async Task<IActionResult> ProcesarPago(int id, string numero, string cvv, string expiracion)
    {
        var vuelo = await _context.Vuelo.FirstOrDefaultAsync(v => v.id_vuelo == id);
        if (vuelo == null) return NotFound();

        // Obtener usuario desde sesión
        var correoUsuario = HttpContext.Session.GetString("UserEmail");
        if (string.IsNullOrEmpty(correoUsuario))
        {
            return RedirectToAction("Index", "Login");
        }

        // Obtener pasajero
        var pasajero = await _context.pasajero
            .Include(p => p.Login)
            .FirstOrDefaultAsync(p => p.Login.correo.ToLower() == correoUsuario.ToLower());

        if (pasajero == null)
        {
            return BadRequest("Pasajero no encontrado.");
        }

        decimal total = vuelo.Precio; 

        // Crear el pago
        var pago = new Pago_Vuelo
        {
            id_usuario = pasajero.id_pasajero,
            id_vuelo = vuelo.id_vuelo,
            id_tarifas = 1, 
            total = total
        };

        _context.pago_vuelo.Add(pago);
        await _context.SaveChangesAsync();

        // Crear la reserva
        var reserva = new Reserva
        {
            id_pago = pago.id_pago,
            fecha_hora = DateTime.Now
        };

        _context.reserva.Add(reserva);
        await _context.SaveChangesAsync();

        
        return RedirectToAction("Confirmacion", new { id = reserva.id_reserva });
    }


    /*

    [HttpPost]
    public IActionResult Confirmar(int id)
    {
        var vuelo = _context.Vuelo.FirstOrDefault(v => v.id_vuelo == id); 
        if (vuelo == null) return NotFound();

        return View("Confirmar", vuelo); 
    }
    */

    [HttpPost]
    public IActionResult ComprarBoleto(int id_reserva)
    {
        var reserva = _context.reserva
            .Include(r => r.Pago)
                .ThenInclude(p => p.Vuelo)
            .Include(r => r.Pago)
                .ThenInclude(p => p.Tarifa)
            .FirstOrDefault(r => r.id_reserva == id_reserva);

        if (reserva == null) return NotFound();

        var vuelo = reserva.Pago.Vuelo;
        var pasajeroEmail = HttpContext.Session.GetString("UserEmail") ?? "Invitado";

        string html = $@"
<div style='max-width: 600px; margin: auto; font-family: Arial, sans-serif; border: 2px solid #005288; border-radius: 10px; padding: 20px; background: linear-gradient(to right, #f0f8ff, #e6f0fa); box-shadow: 0 4px 8px rgba(0,0,0,0.1);'>
    <h1 style='color: #005288; text-align: center; border-bottom: 1px solid #ccc; padding-bottom: 10px;'>✈️ Boleto de Vuelo</h1>
    <table style='width: 100%; font-size: 16px; margin-top: 20px;'>
        <tr>
            <td><strong>Número de vuelo:</strong></td>
            <td>{vuelo.Numero_vuelo}</td>
        </tr>
        <tr>
            <td><strong>Origen:</strong></td>
            <td>{vuelo.Origen}</td>
        </tr>
        <tr>
            <td><strong>Destino:</strong></td>
            <td>{vuelo.Destino}</td>
        </tr>
        <tr>
            <td><strong>Horario:</strong></td>
            <td>{vuelo.Horario.ToString("f")}</td>
        </tr>
        <tr>
            <td><strong>Precio:</strong></td>
            <td><span style='color: green; font-weight: bold;'>${vuelo.Precio}</span></td>
        </tr>
        <tr>
            <td><strong>Pasajero:</strong></td>
            <td>{pasajeroEmail}</td>
        </tr>
    </table>
    <p style='text-align: center; margin-top: 30px; color: #666;'>¡Gracias por elegirnos!<br>Le deseamos un excelente viaje.</p>
</div>
";


        var converter = new SelectPdf.HtmlToPdf();
        SelectPdf.PdfDocument doc = converter.ConvertHtmlString(html);
        byte[] pdf = doc.Save();
        doc.Close();

        return File(pdf, "application/pdf", $"Boleto_{vuelo.Numero_vuelo}.pdf");
    }




    [HttpGet]
    public IActionResult Registro()
    {
        return View();
    }



    [HttpPost]
    public async Task<IActionResult> Registro(string nombre, string apellido, string correo, string contraseña)
    {
        if (ModelState.IsValid)
        {
            var nuevoLogin = new Login
            {
                nombre = nombre,
                apellido = apellido,
                correo = correo,
                rol = "pasajero",
                contraseña = contraseña
            };

            _context.login.Add(nuevoLogin);
            await _context.SaveChangesAsync();

            // Crear también el pasajero asociado a ese login
            var nuevoPasajero = new Pasajero
            {
                id_login = nuevoLogin.id_login,
                documento = "SinDocumento" // Puedes cambiar esto por un campo de entrada si lo necesitas
            };

            _context.pasajero.Add(nuevoPasajero);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Login");
        }

        return View();
    }


    /*
    [HttpPost]
    public async Task<IActionResult> ComprarBoleto(int id_vuelo, int id_tarifa, decimal total)
    {
        // Obtener correo del usuario desde sesión
        var correoUsuario = HttpContext.Session.GetString("UserEmail");
        if (string.IsNullOrEmpty(correoUsuario))
        {
            return RedirectToAction("Index", "Login"); // Redirige si no está logueado
        }

        // Obtener pasajero desde la base con su login (correo)
        var pasajero = await _context.pasajero
            .Include(p => p.Login)
            .FirstOrDefaultAsync(p => p.Login.correo.ToLower() == correoUsuario.ToLower());

        if (pasajero == null)
        {
            return BadRequest("Pasajero no encontrado");
        }

        // Crear el registro de pago
        var pago = new Pago_Vuelo
        {
            id_usuario = pasajero.id_pasajero,
            id_vuelo = id_vuelo,
            id_tarifas = id_tarifa,
            total = total
        };

        _context.pago_vuelo.Add(pago);
        await _context.SaveChangesAsync();

  
        var reserva = new Reserva
        {
            id_pago = pago.id_pago,
            fecha_hora = DateTime.Now
        };

        _context.reserva.Add(reserva);
        await _context.SaveChangesAsync();

   
        return RedirectToAction("Confirmacion", new { id = reserva.id_reserva });
    }*/

    public async Task<IActionResult> Confirmacion(int id)
    {
        var reserva = await _context.reserva
            .Include(r => r.Pago)
                .ThenInclude(p => p.Vuelo)
            .Include(r => r.Pago)
                .ThenInclude(p => p.Tarifa)
            .FirstOrDefaultAsync(r => r.id_reserva == id);

        if (reserva == null)
        {
            return NotFound();
        }

        return View(reserva);
    }






}


