using AppGimnasioMVC.Datos;
using AppGimnasioMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace AppGimnasioMVC.Controllers
{
    [Authorize]
    public class MensualidadController : Controller
    {
        private readonly ApplicationDbContext _contexto;

        public MensualidadController(ApplicationDbContext contexto)
        {
            _contexto = contexto;
        }

        [Authorize(Roles = "Administrador")]
        [HttpGet]
        public async Task<IActionResult> Index(DateTime filtroFechaR, string filtroIdentificacion)
        {
            //var clientes = from cliente in await _contexto.Cliente.Include(r => r.Rutina).ToListAsync() select cliente;
            //var clientes = await _contexto.Cliente.Include(r => r.Rutina).ToListAsync();
            //var clientes = from Cliente in await _contexto.Cliente.Include(r => r.Rutina).ToListAsync() select Cliente;
            var mensualidades = from Mensualidad in await _contexto.Mensualidad.Include(c => c.Cliente).ToListAsync() select Mensualidad;

            if (!String.IsNullOrEmpty(filtroIdentificacion) & filtroFechaR != DateTime.MinValue)
            {
                mensualidades = mensualidades.Where(m => m.Cliente.NumeroIdentificacion!.Contains(filtroIdentificacion) && m.Fecha!.Equals(filtroFechaR));
            }
            else if (!String.IsNullOrEmpty(filtroIdentificacion))
            {
                mensualidades = mensualidades.Where(m => m.Cliente.NumeroIdentificacion!.Contains(filtroIdentificacion));
            }
            else if (filtroFechaR != DateTime.MinValue)
            {
                mensualidades = mensualidades.Where(m => m.Fecha!.Equals(filtroFechaR));
            }

            TempData["MIdentificacion"] = filtroIdentificacion;
            TempData["MFechaR"] = filtroFechaR;
            //return View(await clientes.ToListAsync());
            return View(mensualidades);
        }

        [Authorize(Roles = "Administrador")]
        [HttpGet]
        public IActionResult Crear()
        {
            ViewData["Fecha"] = (String.Format("{0:yyyy-MM-dd}", DateTime.Now));
            ViewData["FechaI"] = (String.Format("{0:yyyy-MM-dd}", DateTime.Now.AddDays(1)));
            ViewData["FechaF"] = (String.Format("{0:yyyy-MM-dd}", DateTime.Now.AddMonths(1).AddDays(1)));
            ViewData["Clientes"] = new SelectList(_contexto.Cliente, "Id", "NumeroIdentificacion");
            
            return View();
        }

        [Authorize(Roles = "Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(string filtroIdentificacion, Mensualidad mensualidad)
        {
            ViewData["Fecha"] = (String.Format("{0:yyyy-MM-dd}", DateTime.Now));
            ViewData["FechaI"] = (String.Format("{0:yyyy-MM-dd}", DateTime.Now.AddDays(1)));
            ViewData["FechaF"] = (String.Format("{0:yyyy-MM-dd}", DateTime.Now.AddMonths(1).AddDays(1)));
            //ViewData["Clientes"] = new SelectList(_contexto.Cliente, "Id", "NumeroIdentificacion", mensualidad.ClienteId);
            TempData["MIdentificacion"] = filtroIdentificacion;
            TempData["Mensaje"] = null;

            if (!String.IsNullOrEmpty(filtroIdentificacion))
            {
                var clientes = _contexto.Cliente.Where(c => c.NumeroIdentificacion == filtroIdentificacion).FirstOrDefault();

                if (clientes != null)
                {
                    mensualidad.Cliente = clientes;
                    mensualidad.ClienteId = clientes.Id;
                }
                else 
                {
                    TempData["Mensaje"] = "Error";
                }
            }

            if (ModelState.IsValid)
            {
                await _contexto.Mensualidad.AddAsync(mensualidad);
                await _contexto.SaveChangesAsync();
                TempData["Mensaje"] = "Se creo la Mensualidad correctamente";
                return RedirectToAction("Index");
            }
            
            return View(mensualidad);
        }


        [Authorize(Roles = "Administrador, Entrenador")]
        [HttpGet]
        public IActionResult Rutina(int? id)
        {
            ViewData["Rutinas"] = new SelectList(_contexto.Rutina, "Id", "Codigo");
            if (id == null)
            {
                return NotFound();
            }

            var cliente = _contexto.Cliente.Find(id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        [Authorize(Roles = "Administrador, Entrenador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Rutina(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                _contexto.Cliente.Update(cliente);
                await _contexto.SaveChangesAsync();
                TempData["Mensaje"] = "Se Asigno la Rutina correctamente";
                return RedirectToAction("Index");
            }
            ViewData["Rutinas"] = new SelectList(_contexto.Rutina, "Id", "Codigo", cliente.RutinaId);
            return View(cliente);
        }

        [Authorize(Roles = "Administrador")]
        [HttpGet]
        public IActionResult Detalle(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = _contexto.Cliente.Find(id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        [Authorize(Roles = "Administrador")]
        [HttpGet]
        public IActionResult Editar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = _contexto.Cliente.Find(id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        [Authorize(Roles = "Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                _contexto.Cliente.Update(cliente);
                await _contexto.SaveChangesAsync();
                TempData["Mensaje"] = "El cliente se edito correctamente";
                return RedirectToAction("Index");
            }
            return View(cliente);
        }

        [Authorize(Roles = "Administrador")]
        [HttpGet]
        public IActionResult Borrar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = _contexto.Cliente.Find(id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        [Authorize(Roles = "Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BorrarRegistro(int? id)
        {
            var cliente = await _contexto.Cliente.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            _contexto.Cliente.Remove(cliente);
            await _contexto.SaveChangesAsync();
            TempData["Mensaje"] = "El cliente se borró correctamente";
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}