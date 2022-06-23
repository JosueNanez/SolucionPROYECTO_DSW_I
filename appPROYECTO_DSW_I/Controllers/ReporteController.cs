using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Formatting;
using appPROYECTO_DSW_I.Models;
using Microsoft.AspNetCore.Authorization;

namespace appPROYECTO_DSW_I.Controllers
{
    public class ReporteController : Controller
    {

        //public async Task<IActionResult> ListProductosVendidos()
        //{
        //	return View(await Task.Run(() => ReporteProductosVendidos()));
        //}

        [Authorize(Roles = "1, 2")]
        public IActionResult ReporteProductosVendidos()
        {
            HttpClient cliente = new HttpClient();
            cliente.BaseAddress = new Uri("https://localhost:44351/");
            
            var request = cliente.GetAsync("api/tb_DetalleBoleta").Result;
            if (request.IsSuccessStatusCode)
            {
                var resultado = request.Content.ReadAsStringAsync().Result;
                IEnumerable<tb_DetalleBoleta> listaProductos = JsonConvert.DeserializeObject<List<tb_DetalleBoleta>>(resultado);
                return View(listaProductos);
            }
           return View();
        }


    }
}
