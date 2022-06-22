using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Session;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using appPROYECTO_DSW_I.Models;

namespace appPROYECTO_DSW_I.Controllers
{
    public class CarritoController : Controller
    {
        private readonly IConfiguration _config;

        public CarritoController(IConfiguration Iconfig)
        {
            _config = Iconfig;
        }

//---------------------------PARA EL CATALOGO
        
        public async Task<IActionResult> Catalogo()
        {
            if (HttpContext.Session.GetString("Canasta") == null)
                HttpContext.Session.SetString("Canasta", JsonConvert.SerializeObject(new List<ItemCarrito>()));
            return View(await Task.Run(() => ListaProductos()));
        }

        public IEnumerable<ProductoModel> ListaProductos()
        {
            SqlConnection cn = new SqlConnection(_config["ConnectionStrings:cn"]);
            List<ProductoModel> ListadoProd = new List<ProductoModel>();
            SqlCommand cmd = new SqlCommand("exec usp_ListarProductos", cn);
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ListadoProd.Add(new ProductoModel()
                {
                    idProducto = dr[0].ToString(),
                    nomProducto = dr[1].ToString(),
                    fechVencimiento = dr[2].ToString(),
                    nomProveedor = dr[3].ToString(),
                    precio = decimal.Parse(dr[4].ToString()),
                    stock = int.Parse(dr[5].ToString())
                });
            }
            cn.Close();
            return ListadoProd;
        }

        
//----------------------------PARA SELECCION DETALLE PRODUCTO 
        ProductoModel Buscar(string id = "") //Para obtener el id producto del CATALOGO
        {
            ProductoModel reg = ListaProductos().Where(p => p.idProducto == id).FirstOrDefault();
            if (reg == null)
                reg = new ProductoModel();
            return reg;
        }
        private int getIndiceCarrito(string id) //Para obtener el producto ya existe en el Carrito de Compras
        {
            List<ItemCarrito> compras = JsonConvert.DeserializeObject<List<ItemCarrito>>(HttpContext.Session.GetString("Canasta"));
            for (int i = 0; i < compras.Count; i++)
            {
                if (compras[i].idProducto == id)
                    return i;
            }
            return -1;
        }
        public async Task<IActionResult> Detalle(string id = "")  //Vista Razor con ProductoModel
        {
            return View(await Task.Run(() => Buscar(id)));
        }
        [HttpPost]
        public ActionResult Detalle(string codigo, int cantidad)
        {
            ProductoModel reg = Buscar(codigo);
            if (cantidad > reg.stock)
            {
                ViewBag.mensaje = string.Format("El producto solo dispone de {0} unidades", reg.stock);
                return View(reg);
            }

            ItemCarrito ic = new ItemCarrito();
            ic.idProducto = codigo; //ID a Productos Compra
            ic.nomProducto = reg.nomProducto;
            ic.fechVencimiento = reg.fechVencimiento;
            ic.precio = reg.precio;
            ic.unidades = cantidad; //Cantidad de Unidades a Productos Compra

            List<ItemCarrito> carrito = JsonConvert.DeserializeObject<List<ItemCarrito>>(HttpContext.Session.GetString("Canasta"));
            int CodProdExistente = getIndiceCarrito(codigo);
            if (CodProdExistente == -1)
            {
                carrito.Add(ic);
                ViewBag.mensaje = "Producto Agregado";
            }
            else
            {
                carrito[CodProdExistente].unidades += cantidad;
                ViewBag.mensaje = "Producto Agregado";
            }
            HttpContext.Session.SetString("Canasta", JsonConvert.SerializeObject(carrito));
            return View(reg);
        }

 //-------------------------------PARA EL CARRITO DE COMPRAS
        private decimal getSumaTotales() //Para obtener La suma Total del Carrito Compras
        {
            List<ItemCarrito> compras = JsonConvert.DeserializeObject<List<ItemCarrito>>(HttpContext.Session.GetString("Canasta"));
            decimal SumaTotal = 0;
            for (int i = 0; i < compras.Count; i++)
            {
                SumaTotal += compras[i].monto;
            }
            return SumaTotal;
        }
        public ActionResult Canasta() //Vista tipo List clase ItemCarrito
        {
            if (HttpContext.Session.GetString("Canasta") == null) return RedirectToAction("Portal");
            IEnumerable<ItemCarrito> carrito = JsonConvert.DeserializeObject<List<ItemCarrito>>(HttpContext.Session.GetString("Canasta"));
            ViewBag.SumaTotal = getSumaTotales();
            return View(carrito);
        }
        public IActionResult Delete(string id)
        {
            List<ItemCarrito> carrito = JsonConvert.DeserializeObject<List<ItemCarrito>>(HttpContext.Session.GetString("Canasta"));

            ItemCarrito reg = carrito.Where(it => it.idProducto == id).First();
            carrito.Remove(reg);

            HttpContext.Session.SetString("Canasta", JsonConvert.SerializeObject(carrito));
            return RedirectToAction("Canasta");
        }

        public IActionResult FinalizarCompra()
        {
            SqlConnection cn = new SqlConnection(_config["ConnectionStrings:cn"]);
            List<ItemCarrito> carrito = JsonConvert.DeserializeObject<List<ItemCarrito>>(HttpContext.Session.GetString("Canasta"));
            cn.Open();
            if (carrito != null && carrito.Count > 0)
            {
                SqlCommand cmdV = new SqlCommand("usp_NuevaBoleta", cn);
                cmdV.CommandType = CommandType.StoredProcedure;
                cmdV.Parameters.AddWithValue("@nom_Usuario", User.Identity.Name);
                cmdV.Parameters.AddWithValue("@fechaOrden ", DateTime.Now);
                cmdV.Parameters.AddWithValue("@Total", getSumaTotales());
                cmdV.ExecuteNonQuery();

                for (int i = 0; i < carrito.Count; i++)
                {
                    SqlCommand cmdB = new SqlCommand("usp_DetalleBoleta", cn);
                    cmdB.CommandType = CommandType.StoredProcedure;
                    cmdB.Parameters.AddWithValue("@idProducto", carrito[i].idProducto);
                    cmdB.Parameters.AddWithValue("@nomProducto", carrito[i].nomProducto);
                    cmdB.Parameters.AddWithValue("@precio", carrito[i].precio);
                    cmdB.Parameters.AddWithValue("@cantidad", carrito[i].unidades);
                    cmdB.Parameters.AddWithValue("@Monto", carrito[i].monto);
                    cmdB.ExecuteNonQuery();
                }
                HttpContext.Session.Remove("Canasta");
                ViewData.Model = "Gracias por realizar su compra Vuelva Pronto !!";
            }
            cn.Close();
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
