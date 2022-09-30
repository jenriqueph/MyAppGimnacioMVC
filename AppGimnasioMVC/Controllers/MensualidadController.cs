using AppGimnasioMVC.Datos;
using AppGimnasioMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
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
            return View(mensualidades);
        }


        [Authorize(Roles = "Administrador")]
        [HttpGet]
        public IActionResult Crear()
        {
            ViewData["Fecha"] = (String.Format("{0:yyyy-MM-dd}", DateTime.Now));
            ViewData["FechaI"] = (String.Format("{0:yyyy-MM-dd}", DateTime.Now.AddDays(1)));
            ViewData["FechaF"] = (String.Format("{0:yyyy-MM-dd}", DateTime.Now.AddMonths(1).AddDays(1)));            
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


        [Authorize(Roles = "Administrador")]
        [HttpGet]
        public IActionResult Detalle(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mensualidad = _contexto.Mensualidad.Where(m => m.Id == id)
                                            .Include(c => c.Cliente)
                                            .FirstOrDefault();

            if (mensualidad == null)
            {
                return NotFound();
            }

            return View(mensualidad);
        }

        [Authorize(Roles = "Administrador")]
        [HttpGet]
        public IActionResult Editar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mensualidad = _contexto.Mensualidad.Where(m => m.Id == id)
                                            .Include(c => c.Cliente)
                                            .FirstOrDefault();

            if (mensualidad == null)
            {
                return NotFound();
            }

            return View(mensualidad);
        }

        [Authorize(Roles = "Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(Mensualidad mensualidad)
        {
            if (ModelState.IsValid)
            {
                _contexto.Mensualidad.Update(mensualidad);
                await _contexto.SaveChangesAsync();
                TempData["Mensaje"] = "La Mensualiadad se edito correctamente";
                return RedirectToAction("Index");
            }
            return View(mensualidad);
        }

        [Authorize(Roles = "Administrador")]
        [HttpGet]
        public IActionResult Borrar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mensualidad = _contexto.Mensualidad.Where(m => m.Id == id)
                                            .Include(c => c.Cliente)
                                            .FirstOrDefault();

            if (mensualidad == null)
            {
                return NotFound();
            }

            return View(mensualidad);
        }

        [Authorize(Roles = "Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BorrarRegistro(int? id)
        {
            var mensualidad = _contexto.Mensualidad.Where(m => m.Id == id)
                                            .Include(c => c.Cliente)
                                            .FirstOrDefault();

            //var cliente = await _contexto.Cliente.FindAsync(id);
            if (mensualidad == null)
            {
                return NotFound();
            }
            _contexto.Mensualidad.Remove(mensualidad);
            await _contexto.SaveChangesAsync();
            TempData["Mensaje"] = "La Mensualidad se borró correctamente";
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}