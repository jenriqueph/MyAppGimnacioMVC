using AppGimnasioMVC.Datos;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppGimnasioMVC.Controllers
{
    [Authorize]
    public class MenuController : Controller
    {
        private readonly ApplicationDbContext _contexto;

        public MenuController(ApplicationDbContext contexto)
        {
            _contexto = contexto;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Salir()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Home");
        }

    }

}
