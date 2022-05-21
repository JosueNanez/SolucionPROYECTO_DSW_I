using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace appPROYECTO_DSW_I.Models
{
    public class UsuarioModel
    {
        [Display(Name ="CODIGO")]
        public string idUser { get; set; }
        [Display(Name ="NOMBRE")]
        public string nombreUser { get; set; }
        [Display(Name ="CORREO")]
        public string correoUser { get; set; }
        [Display(Name ="CONTRASEÑA")]
        public string claveUser { get; set; }
        [Display(Name ="DIRECCION")]
        public string direcUser { get; set; }
        [Display(Name ="TIPO")]
        public string idTipo { get; set; }

    }
}
