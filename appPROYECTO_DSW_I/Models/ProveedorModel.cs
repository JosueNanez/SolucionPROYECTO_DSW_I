using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace appPROYECTO_DSW_I.Models
{
    public class ProveedorModel
    {
        [Display(Name ="CÓDIGO")]
        public String idProveedor { get; set; }
        [Display(Name ="NOMBRE")]
        public String nomProveedor { get; set; }
        [Display(Name ="DIRECCIÓN")]
        public String dirProveedor { get; set; }
        [Display(Name ="TELÉFONO")]
        public String telefono { get; set; }
    }
}
