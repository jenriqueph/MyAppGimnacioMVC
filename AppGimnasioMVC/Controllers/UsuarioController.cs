using AppGimnasioMVC.Datos;
using AppGimnasioMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace AppGimnasioMVC.Controllers
{
    [Authorize]
    public class UsuarioController : Controller
    {
        private readonly ApplicationDbContext _contexto;

        public UsuarioController(ApplicationDbContext contexto)
        {
            _contexto = contexto;
        }

        [Authorize(Roles ="Administrador")]
        [HttpGet]
        public async Task<IActionResult> Index(string filtroApellido, string filtroIdentificacion)
        {
            var personas = from Persona in await _contexto.Persona.ToListAsync() select Persona;

            if (!String.IsNullOrEmpty(filtroIdentificacion) & !String.IsNullOrEmpty(filtroApellido))
            {
                personas = personas.Where(p => p.Apellidos!.Contains(filtroApellido) && p.NumeroIdentificacion!.Contains(filtroIdentificacion));
            }
            else if (!String.IsNullOrEmpty(filtroIdentificacion))
            {
                personas = personas.Where(p => p.NumeroIdentificacion!.Contains(filtroIdentificacion));
            }
            else if(!String.IsNullOrEmpty(filtroApellido))
            {
                personas = personas.Where(p => p.Apellidos!.Contains(filtroApellido));
            }

            TempData["MIdentificacion"] = filtroApellido;
            TempData["MApellido"] = filtroApellido;

            return View(personas);
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
        public async Task<IActionResult> Crear(Persona persona)
        {
            if (ModelState.IsValid)
            {
                await _contexto.Persona.AddAsync(persona);
                await _contexto.SaveChangesAsync();
                TempData["Mensaje"] = "El usuario se creo correctamente";
                return RedirectToAction("Index");
            }

            return View(persona);
        }

        [Authorize(Roles = "Administrador")]
        [HttpGet]
        public IActionResult Detalle(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var persona = _contexto.Persona.Find(id);
            if (persona == null)
            {
                return NotFound();
            }

            return View(persona);
        }

        [Authorize(Roles = "Administrador")]
        [HttpGet]
        public IActionResult Editar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var persona = _contexto.Persona.Find(id);
            if (persona == null)
            {
                return NotFound();
            }

            return View(persona);
        }

        [Authorize(Roles = "Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(Persona persona)
        {
            if (ModelState.IsValid)
            {
                _contexto.Persona.Update(persona);
                await _contexto.SaveChangesAsync();
                TempData["Mensaje"] = "El usuario se edito correctamente";
                return RedirectToAction("Index");
            }
            return View(persona);
        }

        [Authorize(Roles = "Administrador")]
        [HttpGet]
        public IActionResult Borrar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var persona = _contexto.Persona.Find(id);
            if (persona == null)
            {
                return NotFound();
            }

            return View(persona);
        }

        [Authorize(Roles = "Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BorrarRegistro(int? id)
        {
            var persona = await _contexto.Persona.FindAsync(id);
            if (persona == null)
            {
                return NotFound();
            }
            _contexto.Persona.Remove(persona);
            await _contexto.SaveChangesAsync();
            TempData["Mensaje"] = "El usuario se borró correctamente";
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
