using AppGimnasioMVC.Datos;
using AppGimnasioMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace AppGimnasioMVC.Controllers
{
    public class RutinaController : Controller
    {
        private readonly ApplicationDbContext _contexto;

        public RutinaController(ApplicationDbContext contexto)
        {
            _contexto = contexto;
        }

        [Authorize(Roles = "Administrador, Entrenador")]
        [HttpGet]
        public async Task<IActionResult> Index(string filtroCodigo, string filtroNombre, string filtroRutina)
        {
            var rutinas = from rutina in _contexto.Rutina select rutina;

            if (!String.IsNullOrEmpty(filtroRutina))
            {
                rutinas = rutinas.Where(r => r.Ejercicio1!.Contains(filtroRutina) || r.Ejercicio2!.Contains(filtroRutina)
                        || r.Ejercicio3!.Contains(filtroRutina) || r.Ejercicio4!.Contains(filtroRutina)
                        || r.Ejercicio5!.Contains(filtroRutina) || r.Cardio!.Contains(filtroRutina));
                TempData["MRutina"] = filtroRutina;
            }
            else if (!String.IsNullOrEmpty(filtroCodigo))
            {
                rutinas = rutinas.Where(r => r.Codigo!.Contains(filtroCodigo));
                TempData["MCodigo"] = filtroCodigo;
            }
            else if (!String.IsNullOrEmpty(filtroNombre))
            {
                rutinas = rutinas.Where(r => r.NombreRutina!.Contains(filtroNombre));
                TempData["MNombre"] = filtroNombre;
            }
            return View(await rutinas.ToListAsync());
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
        public async Task<IActionResult> Crear(Rutina rutina)
        {
            if (ModelState.IsValid)
            {
                await _contexto.Rutina.AddAsync(rutina);
                await _contexto.SaveChangesAsync();
                TempData["Mensaje"] = "La rutina se creo correctamente";
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

            var rutina = _contexto.Rutina.Find(id);
            if (rutina == null)
            {
                return NotFound();
            }

            return View(rutina);
        }

        [Authorize(Roles = "Administrador")]
        [HttpGet]
        public IActionResult Editar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rutina = _contexto.Rutina.Find(id);
            if (rutina == null)
            {
                return NotFound();
            }

            return View(rutina);
        }

        [Authorize(Roles = "Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(Rutina rutina)
        {
            if (ModelState.IsValid)
            {
                _contexto.Rutina.Update(rutina);
                await _contexto.SaveChangesAsync();
                TempData["Mensaje"] = "La rutina se edito correctamente";
                return RedirectToAction("Index");
            }
            return View(rutina);
        }

        [Authorize(Roles = "Administrador")]
        [HttpGet]
        public IActionResult Borrar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rutina = _contexto.Rutina.Find(id);
            if (rutina == null)
            {
                return NotFound();
            }

            return View(rutina);
        }

        [Authorize(Roles = "Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BorrarRegistro(int? id)
        {
            var rutina = await _contexto.Rutina.FindAsync(id);
            if (rutina == null)
            {
                return NotFound();
            }
            _contexto.Rutina.Remove(rutina);
            await _contexto.SaveChangesAsync();
            TempData["Mensaje"] = "La rutina se borró correctamente";
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
