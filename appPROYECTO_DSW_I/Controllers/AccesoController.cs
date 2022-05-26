using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore-Authentication.Cookies;
using Microsoft.AspNetCore-Authentication;
using appPROYECTO_DSW_I.DataLogica;
using appPROYECTO_DSW_I.Models;

namespace appPROYECTO_DSW_I.Controllers
{
    public class AccesoController : Controller
    {
      

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(Usuario _usuario)
        {
            DA_Logica _da_usuario = new DA_Logica();
            var usuario = _da_usuario.validarUsuario(_usuario.CorreoUsuario, _usuario.ClaveUsuario);

            if (usuario != null)
            {
                var _claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, usuario.CorreoUsuario),
                    new Claim("CorreoUsuario", usuario.ClaveUsuario)
                };

                foreach(string rol in usuario.Roles)
                {
                    _claims.Add(new Claim(ClaimTypes.Role, rol))
                }

                var _claimsIdentity = new CaimsIdentity(_claims, CookieAuthenticationDefaults.AuthenticationScheme);

                HttpContext.SignInAsync(CookieAuthenticationDefaults.AutheticationScheme,
                    new ClaimsPrincipal(_claimsIdentity));

                return redirectToAction("Index", "Home");
            }
            else { return View(); }

            public async Task<IActionResult> Salir()
            {

                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return RedirecToAction("Index", "Acceso");
            }
        }
