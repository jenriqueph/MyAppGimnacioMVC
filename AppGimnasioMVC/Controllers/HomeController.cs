using AppGimnasioMVC.Datos;
using AppGimnasioMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace AppGimnasioMVC.Controllers
{
    //[Authorize]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _contexto;

        public HomeController(ApplicationDbContext contexto)
        {
            _contexto = contexto;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        public Persona ValidarUsuario(string correo, string clave) => _contexto.Persona.FirstOrDefault(u => u.Email == correo && u.Contrasenia == clave);

        [HttpPost]
        public async Task<IActionResult> Login(Persona _usuario)
        {
            var usuario = ValidarUsuario(_usuario.Email, _usuario.Contrasenia);
            

            if (usuario == null)
            {
                return View();
            }
            else
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, usuario.Nombres),
                    new Claim("Correo", usuario.Email),
                    new Claim(ClaimTypes.Role, usuario.Rol.ToString())
                };

                var claimIdenttity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme); 

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimIdenttity));

                return RedirectToAction("MenuAdministrador", "Home");
            }

        }

        public IActionResult MenuAdministrador()
        {
            return View();
        }

        public IActionResult Denegado()
        {
            return View();
        }

        public async Task<IActionResult> Salir()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}