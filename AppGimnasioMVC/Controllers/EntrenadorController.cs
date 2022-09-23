using AppGimnasioMVC.Datos;
using AppGimnasioMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace AppGimnasioMVC.Controllers
{
    [Authorize]
    public class EntrenadorController : Controller
    {
        private readonly ApplicationDbContext _contexto;

        public EntrenadorController(ApplicationDbContext contexto)
        {
            _contexto = contexto;
        }

        [Authorize(Roles = "Administrador")]
        [HttpGet]
        public async Task<IActionResult> Index(string filtroApellido, string filtroIdentificacion)
        {
            var entrenadores = from entrenador in _contexto.Entrenador select entrenador;

            if (!String.IsNullOrEmpty(filtroIdentificacion) & !String.IsNullOrEmpty(filtroApellido))
            {
                entrenadores = entrenadores.Where(e => e.Apellidos!.Contains(filtroApellido) && e.NumeroIdentificacion!.Contains(filtroIdentificacion));
            }
            else if (!String.IsNullOrEmpty(filtroIdentificacion) & String.IsNullOrEmpty(filtroApellido))
            {
                entrenadores = entrenadores.Where(e => e.NumeroIdentificacion!.Contains(filtroIdentificacion));
            }
            else if (String.IsNullOrEmpty(filtroIdentificacion) & !String.IsNullOrEmpty(filtroApellido))
            {
                entrenadores = entrenadores.Where(e => e.Apellidos!.Contains(filtroApellido));
            }
            TempData["MIdentificacion"] = filtroIdentificacion;
            TempData["MApellido"] = filtroApellido;
            return View(await entrenadores.ToListAsync());
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
        public async Task<IActionResult> Crear(Entrenador entrenador)
        {
            if (ModelState.IsValid)
            {
                await _contexto.Entrenador.AddAsync(entrenador);
                await _contexto.SaveChangesAsync();
                TempData["Mensaje"] = "El entrenador se creo correctamente";
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

            var entrenador = _contexto.Entrenador.Find(id);
            if (entrenador == null)
            {
                return NotFound();
            }

            return View(entrenador);
        }

        [Authorize(Roles = "Administrador")]
        [HttpGet]
        public IActionResult Editar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entrenador = _contexto.Entrenador.Find(id);
            if (entrenador == null)
            {
                return NotFound();
            }

            return View(entrenador);
        }

        [Authorize(Roles = "Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(Entrenador entrenador)
        {
            if (ModelState.IsValid)
            {
                _contexto.Entrenador.Update(entrenador);
                await _contexto.SaveChangesAsync();
                TempData["Mensaje"] = "El entrenador se edito correctamente";
                return RedirectToAction("Index");
            }
            return View(entrenador);
        }

        [Authorize(Roles = "Administrador")]
        [HttpGet]
        public IActionResult Borrar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entrenador = _contexto.Entrenador.Find(id);
            if (entrenador == null)
            {
                return NotFound();
            }

            return View(entrenador);
        }

        [Authorize(Roles = "Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BorrarRegistro(int? id)
        {
            var entrenador = await _contexto.Entrenador.FindAsync(id);
            if (entrenador == null)
            {
                return NotFound();
            }
            _contexto.Entrenador.Remove(entrenador);
            await _contexto.SaveChangesAsync();
            TempData["Mensaje"] = "El entrenador se borró correctamente";
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}