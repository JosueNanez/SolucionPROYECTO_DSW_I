using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using appPROYECTO_DSW_I.Models;
using System.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace appPROYECTO_DSW_I.Controllers
{
    public class UsuarioController : Controller
    {
        private IUsuario UsuarioProceso;
        public UsuarioController()
        {
            UsuarioProceso = new UsuarioRepositorio();
        }


		public async Task<IActionResult> CreateUser()
		{
			ViewBag.LISTCBO = new SelectList(await Task.Run(() => UsuarioProceso.ListTipoUser()), "idTipo", "descTipo");
			return View(new UsuarioModel());
		}
		[HttpPost]
		public async Task<IActionResult> CreateUser(UsuarioModel reg)
		{
			if (!ModelState.IsValid)
			{
				ViewBag.LISTCBO = new SelectList(await Task.Run(() => UsuarioProceso.ListTipoUser()), "idTipo", "descTipo", reg.idTipo);
				return View(reg);
			}
			ViewBag.mensaje = UsuarioProceso.AgregarUsuario(reg);
			ViewBag.LISTCBO = new SelectList(await Task.Run(() => UsuarioProceso.ListTipoUser()), "idTipo", "descTipo", reg.idTipo);
			return View(reg);
		}


		//Comentario de prueba
		//comentario de prueba 2
		//comentario de prueba 3

		public IActionResult Index()
        {
            return View();
        }
    }
}
