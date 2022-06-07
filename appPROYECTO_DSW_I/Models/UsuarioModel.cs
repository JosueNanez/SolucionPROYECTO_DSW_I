using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace appPROYECTO_DSW_I.Models
{
    public class UsuarioModel
    {
        [Display(Name ="Codigo:")]
        public string idUser { get; set; }
        [Display(Name ="Nombre:")]
        public string nombreUser { get; set; }
        [Display(Name ="Correo:")]
        public string correoUser { get; set; }
        [Display(Name ="Contraseña:")]
        public string claveUser { get; set; }
        [Display(Name ="Dirección:")]
        public string direcUser { get; set; }
        [Display(Name ="Tipo:")]
        public string idTipo { get; set; }

    }
}
