using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc.Rendering;
using appPROYECTO_DSW_I.Models;
using System.Data;
using System.Data.SqlClient;

using Microsoft.AspNetCore.Authorization;

namespace appPROYECTO_DSW_I.Controllers
{

	public class UsuarioController : Controller
	{
		private IUsuario UsuarioProceso;
		public UsuarioController()
		{
			UsuarioProceso = new UsuarioRepositorio();
		}

		///---------------------------------------NUEVO USUARIO (ADMINISTRADOR O CLIENTE)
		//[Authorize(Roles = "1,2")]
		[ValidateAntiForgeryToken]
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


		///---------------------------------------NUEVO USUARIO (SOLO CLIENTE)
        [ValidateAntiForgeryToken]
		public IActionResult CreateCliente()
		{
			return View(new UsuarioModel());
		}
		[HttpPost]
		public IActionResult CreateCliente(UsuarioModel reg)
		{
			if (!ModelState.IsValid)
			{
				return View(reg);
			}
			ViewBag.mensaje = UsuarioProceso.NuevoCliente(reg);
			return View(reg);
		}
		///------------------------------------------------------------------

		public IActionResult Index()
        {
            return View();
        }
    }
}
