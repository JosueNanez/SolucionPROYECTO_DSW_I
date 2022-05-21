using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using appPROYECTO_DSW_I.Models;

namespace appPROYECTO_DSW_I.Models
{
    public interface IUsuario
    {
        IEnumerable<TipoUserModel> ListTipoUser();
        string AgregarUsuario(UsuarioModel reg);

    }
}
