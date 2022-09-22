using AppGimnasioMVC.Datos;
using AppGimnasioMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;

namespace AppGimnasioMVC.Controllers
{
    //[Authorize]
    public class ClienteController : Controller
    {
        private readonly ApplicationDbContext _contexto;

        public ClienteController(ApplicationDbContext contexto)
        {
            _contexto = contexto;
        }
        [Authorize(Roles ="Administrador, Entrenador")]
        [HttpGet]
        public async Task<IActionResult> Index(string filtroApellido, string filtroIdentificacion)
        {
            var clientes = from cliente in _contexto.Cliente select cliente;
            
            if (!String.IsNullOrEmpty(filtroIdentificacion) & !String.IsNullOrEmpty(filtroApellido))
            {
                clientes = clientes.Where(c => c.Apellidos!.Contains(filtroApellido) && c.NumeroIdentificacion!.Contains(filtroIdentificacion));
            }
            else if (!String.IsNullOrEmpty(filtroIdentificacion) & String.IsNullOrEmpty(filtroApellido))
            {
                clientes = clientes.Where(c => c.NumeroIdentificacion!.Contains(filtroIdentificacion));
            }
            else if (String.IsNullOrEmpty(filtroIdentificacion) & !String.IsNullOrEmpty(filtroApellido))
            {
                clientes = clientes.Where(c => c.Apellidos!.Contains(filtroApellido));
            }
            TempData["MIdentificacion"] = filtroIdentificacion;
            TempData["MApellido"] = filtroApellido;
            return View(await clientes.ToListAsync());
        }

        [Authorize(Roles = "Administrador")]
        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }

        [Authorize(Roles = "Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                await _contexto.Cliente.AddAsync(cliente);
                await _contexto.SaveChangesAsync();
                TempData["Mensaje"] = "El cliente se creo correctamente";
                return RedirectToAction("Index");
            }
            return View();
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

        /*
        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
        */
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}