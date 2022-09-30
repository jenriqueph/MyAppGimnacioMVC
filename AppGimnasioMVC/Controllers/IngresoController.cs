using AppGimnasioMVC.Datos;
using AppGimnasioMVC.Migrations;
using AppGimnasioMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;

namespace AppGimnasioMVC.Controllers
{
    [Authorize]
    public class IngresoController : Controller
    {
        private readonly ApplicationDbContext _contexto;

        public IngresoController(ApplicationDbContext contexto)
        {
            _contexto = contexto;
        }

        [Authorize(Roles = "Administrador")]
        [HttpGet]
        public async Task<IActionResult> Index(DateTime filtroFechaI, string filtroIdentificacion)
        {
            var ingresos = from IngresoGimnasio in await _contexto.IngresoGimnasio.Include(c => c.Cliente).ToListAsync() select IngresoGimnasio;

            if (!String.IsNullOrEmpty(filtroIdentificacion) & filtroFechaI != DateTime.MinValue)
            {
                ingresos = ingresos.Where(m => m.Cliente.NumeroIdentificacion!.Contains(filtroIdentificacion) && m.FechaIngreso!.Equals(filtroFechaI));
            }
            else if (!String.IsNullOrEmpty(filtroIdentificacion))
            {
                ingresos = ingresos.Where(m => m.Cliente.NumeroIdentificacion!.Contains(filtroIdentificacion));
            }
            else if (filtroFechaI != DateTime.MinValue)
            {
                ingresos = ingresos.Where(m => m.FechaIngreso!.Equals(filtroFechaI));
            }

            TempData["MIdentificacion"] = filtroIdentificacion;
            TempData["MFechaI"] = filtroFechaI;
            return View(ingresos);
        }


        [Authorize(Roles = "Cliente")]
        [HttpGet]
        public IActionResult CrearCliente(IngresoGimnasio ingreso)
        {
            ViewData["Fecha"] = (String.Format("{0:yyyy-MM-dd}", DateTime.Now));
            var userClaims = User.Claims.ToList();
            var useremail = userClaims.ElementAt(1).Value;

            var clientes = _contexto.Cliente.Where(c => c.Email == useremail).FirstOrDefault();

            if (clientes != null)
            {
                ingreso.Cliente = clientes;
                ingreso.ClienteId = clientes.Id;
                return View(ingreso);
            }
            else
            {
                return NotFound();
            }
        }

        [Authorize(Roles = "Cliente")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GuardarCliente(IngresoGimnasio ingreso)
        {
            ViewData["Fecha"] = (String.Format("{0:yyyy-MM-dd}", DateTime.Now));
            TempData["Mensaje2"] = null;

            var userClaims = User.Claims.ToList();
            var useremail = userClaims.ElementAt(1).Value;

            var clientes = _contexto.Cliente.Where(c => c.Email == useremail).FirstOrDefault();

            if (clientes != null)
            {
                ingreso.Cliente = clientes;
                ingreso.ClienteId = clientes.Id;
            }
            else
            {
                TempData["Mensaje2"] = "Error";
                return View(ingreso);
            }


            if (ModelState.IsValid)
            {
                await _contexto.IngresoGimnasio.AddAsync(ingreso);
                await _contexto.SaveChangesAsync();
                TempData["Mensaje2"] = "Se creo el Ingreso al Gimansio correctamente";
                return RedirectToAction("Index", "Menu");
            }

            return View(ingreso);
        }



        [Authorize(Roles = "Administrador")]
        [HttpGet]
        public IActionResult Crear()
        {
            ViewData["Fecha"] = (String.Format("{0:yyyy-MM-dd}", DateTime.Now));
            return View();
        }


        [Authorize(Roles = "Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(string filtroIdentificacion, IngresoGimnasio ingreso)
        {
            ViewData["Fecha"] = (String.Format("{0:yyyy-MM-dd}", DateTime.Now));
            TempData["MIdentificacion"] = filtroIdentificacion;
            TempData["Mensaje"] = null;

            if (!String.IsNullOrEmpty(filtroIdentificacion))
            {
                var clientes = _contexto.Cliente.Where(c => c.NumeroIdentificacion == filtroIdentificacion).FirstOrDefault();

                if (clientes != null)
                {
                    ingreso.Cliente = clientes;
                    ingreso.ClienteId = clientes.Id;
                }
                else
                {
                    TempData["Mensaje"] = "Error";
                }
            }

            if (ModelState.IsValid)
            {
                await _contexto.IngresoGimnasio.AddAsync(ingreso);
                await _contexto.SaveChangesAsync();
                TempData["Mensaje"] = "Se creo el Ingreso al Gimansio correctamente";
                return RedirectToAction("Index");
            }

            return View(ingreso);
        }


        [Authorize(Roles = "Administrador")]
        [HttpGet]
        public IActionResult Detalle(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ingreso = _contexto.IngresoGimnasio.Where(m => m.Id == id)
                                            .Include(c => c.Cliente)
                                            .FirstOrDefault();

            if (ingreso == null)
            {
                return NotFound();
            }

            return View(ingreso);
        }

        [Authorize(Roles = "Administrador")]
        [HttpGet]
        public IActionResult Editar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ingreso = _contexto.IngresoGimnasio.Where(m => m.Id == id)
                                            .Include(c => c.Cliente)
                                            .FirstOrDefault();

            if (ingreso == null)
            {
                return NotFound();
            }

            return View(ingreso);
        }

        [Authorize(Roles = "Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(IngresoGimnasio ingreso)
        {
            if (ModelState.IsValid)
            {
                _contexto.IngresoGimnasio.Update(ingreso);
                await _contexto.SaveChangesAsync();
                TempData["Mensaje"] = "El Ingreso al Gimansio se edito correctamente";
                return RedirectToAction("Index");
            }
            return View(ingreso);
        }

        [Authorize(Roles = "Administrador")]
        [HttpGet]
        public IActionResult Borrar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ingreso = _contexto.IngresoGimnasio.Where(m => m.Id == id)
                                            .Include(c => c.Cliente)
                                            .FirstOrDefault();

            if (ingreso == null)
            {
                return NotFound();
            }

            return View(ingreso);
        }

        [Authorize(Roles = "Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BorrarRegistro(int? id)
        {
            var ingreso = _contexto.IngresoGimnasio.Where(m => m.Id == id)
                                            .Include(c => c.Cliente)
                                            .FirstOrDefault();

            if (ingreso == null)
            {
                return NotFound();
            }
            _contexto.IngresoGimnasio.Remove(ingreso);
            await _contexto.SaveChangesAsync();
            TempData["Mensaje"] = "el Ingreso al Gimnasio se borró correctamente";
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}