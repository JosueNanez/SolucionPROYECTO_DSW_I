using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace appPROYECTO_DSW_I.Models
{
    public class ItemCarrito
    {
        [Display(Name = "Codigo")]
        public string idProducto { get; set; }
        [Display(Name = "Producto")]
        public string nomProducto { get; set; }
        [Display(Name = "Fec. Vencimiento")]
        public String fechVencimiento { get; set; }
        [Display(Name = "Precio")]
        public decimal precio { get; set; }
        [Display(Name = "Cantidad")]
        public int unidades { get; set; }
        [Display(Name = "Total")]
        public decimal monto { get { return precio * unidades; } }


    }
}
