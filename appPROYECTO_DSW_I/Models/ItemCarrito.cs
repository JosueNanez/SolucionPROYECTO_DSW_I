using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace appPROYECTO_DSW_I.Models
{
    public class ItemCarrito
    {
        [Display(Name = "CODIGO")]
        public string idProducto { get; set; }
        [Display(Name = "PRODUCTO")]
        public string nomProducto { get; set; }
        [Display(Name = "FEC VENCIMIENTO")]
        public String fechVencimiento { get; set; }
        [Display(Name = "PRECIO")]
        public decimal precio { get; set; }
        [Display(Name = "CANTIDAD")]
        public int unidades { get; set; }
        [Display(Name = "MONTO")]
        public decimal monto { get { return precio * unidades; } }


    }
}
