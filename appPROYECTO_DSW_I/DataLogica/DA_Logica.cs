using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using appPROYECTO_DSW_I.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace appPROYECTO_DSW_I.DataLogica
{
    public class DA_Logica
    {
        public List<LoginUsuario> ListaUsuario
        {
            return new List<LoginUsuario>{

            new Usuario { NombreUsuario = "Albert Tello",
            Correousuario = "albert01@hotmail.com", Claveusuario ="albert01" ,Roles = new string[]{"Supervisor"} }

            new Usuario { NombreUsuario = "Admin1",
            Correousuario = "adm123@hotmail.com",Claveusuario = "adm123", Roles = new string[]{"Administrador"} }

            new Usuario { NombreUsuario = "Erick Chavez",
            Correousuario = "erick03@hotmail.com",Claveusuario = "erick03", Roles = new string[]{"Supervisor", "Empleado"} }

            new Usuario { NombreUsuario = "Gonzalo Cachuy",
            Correousuario = "gonzalo04@hotmail.com",Claveusuario = "gonzalo04", Roles = new string[]{"Supervisor"} }

            new Usuario { NombreUsuario = "Brenda Vargas",
            Correousuario = "brenda05@hotmail.com",Claveusuario = "brenda05", Roles = new string[]{"Supervisor"} }



        public Usuario ValidaUsuario(string _correo, string _clave)
        {
    return ListaUsuario()
    .Where(item => item.CorreoUsuario == _correo && item.ClaveUsuario == _clave).FirstOrDefault(); 
        }
                
                

    }
}