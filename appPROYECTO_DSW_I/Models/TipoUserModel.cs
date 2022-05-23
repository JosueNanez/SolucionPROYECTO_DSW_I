using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace appPROYECTO_DSW_I.Models
{
    public class TipoUserModel
    {
        [Display(Name ="Código")]
        public string idTipo { get; set; }
        [Display(Name ="Descripción")]
        public string descTipo { get; set; }
    }
}
