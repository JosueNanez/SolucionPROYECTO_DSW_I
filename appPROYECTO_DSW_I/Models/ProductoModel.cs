using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace appPROYECTO_DSW_I.Models
{
    public class ProductoModel
    {
        [Display(Name ="Código")]
        public string idProducto { get; set; }
        [Display(Name ="Producto")]
        public string nomProducto { get; set; }
        [Display(Name ="Fecha de Vencimiento")]
        public String fechVencimiento { get; set; }
        [Display(Name ="Proveedor")]
        public String idProveedor { get; set; }
        [Display(Name ="Precio")]
        public decimal precio { get; set; }
        [Display(Name ="Cantidad")]
        public int stock { get; set; }

    }
}
