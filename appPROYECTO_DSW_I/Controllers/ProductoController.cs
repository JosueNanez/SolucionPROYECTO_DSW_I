using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Data.SqlClient;
using System.Data;
using appPROYECTO_DSW_I.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace appPROYECTO_DSW_I.Controllers
{
    public class ProductoController : Controller
    {
        private readonly IConfiguration _Iconfig;
        public ProductoController(IConfiguration Iconfig)
        {
            _Iconfig = Iconfig;
        }

        IEnumerable<ProductoModel> productos()
        {
            List<ProductoModel> lista = new List<ProductoModel>();
            using (SqlConnection cn = new SqlConnection(_Iconfig["ConnectionStrings:cn"]))
            {
                SqlCommand cmd = new SqlCommand("usp_ListarProductos", cn);
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lista.Add(new ProductoModel()
                    {
                        idProducto = dr[0].ToString(),
                        nomProducto = dr[1].ToString(),
                        fechVencimiento = dr[2].ToString(),
                        idProveedor = dr[3].ToString(),
                        precio = decimal.Parse(dr[4].ToString()),
                        stock = int.Parse(dr[0].ToString())
                    });
                }
            }
            return lista;
        }

        public async Task<ActionResult> ListadoProductos()
        {
            return View(await Task.Run(() => productos()));
        }



        //ienumerable para listar los proveedorres

        public IEnumerable<ProveedorModel> proveedor()
        {
            List<ProveedorModel> lstProveedor = new List<ProveedorModel>();
            using (SqlConnection cn = new SqlConnection(_Iconfig["ConnectionStrings:cn"]))
            {
                SqlCommand cmd = new SqlCommand("usp_ListarProveedor", cn);
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lstProveedor.Add(new ProveedorModel()
                    {
                        idProveedor = dr[0].ToString(),
                        nomProveedor = dr[1].ToString(),
                        dirProveedor = dr[2].ToString(),
                        telefono = dr[3].ToString()
                    });
                }
            }
            return lstProveedor;
        }
        //buscar producto
        ProductoModel BuscarProducto(String idproducto)
        {
            ProductoModel reg = productos().Where(c => c.idProducto == idproducto).FirstOrDefault();
            return reg;
        }




        /************************************ parte de Jezrel**********************************/

        //para registrar
        public async Task<IActionResult> Create()
        {
            ViewBag.proveedor = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(await Task.Run(() => proveedor()), "idProveedor", "nomProveedor");
            return View(new ProductoModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductoModel reg)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.proveedor = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(await Task.Run(() => proveedor()), "idProveedor", "nomProveedor");
                return View(new ProductoModel());
            }
            string mensaje = string.Empty;

            using (SqlConnection cn = new SqlConnection(_Iconfig["ConnectionStrings:cn"]))
            {
                try
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("usp_Merge_InsertUpd", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idproducto", reg.idProducto);
                    cmd.Parameters.AddWithValue("@nomproducto", reg.nomProducto);
                    cmd.Parameters.AddWithValue("@fechvencimiento", reg.fechVencimiento);
                    cmd.Parameters.AddWithValue("@idproveedor", reg.idProveedor);
                    cmd.Parameters.AddWithValue("@precio", reg.precio);
                    cmd.Parameters.AddWithValue("@stock", reg.stock);
                    int num = cmd.ExecuteNonQuery();
                    mensaje = $"Se ha insertado {num} Producto(s)";
                }
                catch (SqlException ex)
                {
                    mensaje = ex.Message.ToString();
                }
                ViewBag.mensaje = mensaje;
                ViewBag.proveedor = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(await Task.Run(() => proveedor()), "idProveedor", "nomProveedor", reg.idProveedor);
                return View(reg);
            }
        }

        //para actualizar
        public async Task<IActionResult> Edit(string id)
        {
            ProductoModel reg = BuscarProducto(id);
            if (reg == null)
            {
                RedirectToAction("ListadoProductos");
            }

            ViewBag.proveedor = new SelectList(await Task.Run(() => proveedor()), "idProveedor", "nomProveedor", reg.nomProducto);
            return View(reg);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(ProductoModel reg)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.proveedor = new SelectList(await Task.Run(() => proveedor()), "idProveedor", "nomProveedor", reg.idProveedor);
                return View(reg);
            }
            string mensaje = string.Empty;

            using (SqlConnection cn = new SqlConnection(_Iconfig["ConnectionStrings:cn"]))
            {
                try
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("usp_Merge_InsertUpd", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idproducto", reg.idProducto);
                    cmd.Parameters.AddWithValue("@nomproducto", reg.nomProducto);
                    cmd.Parameters.AddWithValue("@fechvencimiento", reg.fechVencimiento);
                    cmd.Parameters.AddWithValue("@idproveedor", reg.idProveedor);
                    cmd.Parameters.AddWithValue("@precio", reg.precio);
                    cmd.Parameters.AddWithValue("@stock", reg.stock);
                    int num = cmd.ExecuteNonQuery();
                    mensaje = $"Se ha actualizado {num} Producto(s)";
                }
                catch (SqlException ex)
                {
                    mensaje = ex.Message.ToString();
                }
                ViewBag.mensaje = mensaje;
                ViewBag.proveedor = new SelectList(await Task.Run(() => proveedor()), "idProveedor", "nomProveedor", reg.idProveedor);
                return View(reg);
            }
        }


        public IActionResult Index()
        {
            return View();
        }
    }
}
