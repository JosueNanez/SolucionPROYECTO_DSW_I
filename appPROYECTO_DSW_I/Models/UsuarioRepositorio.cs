using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace appPROYECTO_DSW_I.Models
{
    public class UsuarioRepositorio :IUsuario
    {
        private string cadena;
        public UsuarioRepositorio()
        { 
            cadena = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetConnectionString("cn");
        }

        public IEnumerable<TipoUserModel> ListTipoUser()
        {
            List<TipoUserModel> ListadoTipos = new List<TipoUserModel>(); 
            SqlConnection cn = new SqlConnection(cadena);
            SqlCommand cmd = new SqlCommand("exec usp_listarTiposUser", cn);
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ListadoTipos.Add(new TipoUserModel()
                {
                    idTipo = dr.GetString(0),
                    descTipo = dr.GetString(1)
                });
            }
            cn.Close();
            return ListadoTipos;
        }

        public string AgregarUsuario(UsuarioModel reg)
        {
            string mensaje = string.Empty;
            SqlConnection cn = new SqlConnection(cadena);
            SqlCommand cmd = new SqlCommand("usp_Usuario_Merge", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id_Usuario", reg.idUser);
            cmd.Parameters.AddWithValue("@nom_Usuario", reg.nombreUser);
            cmd.Parameters.AddWithValue("@correo_Usuario", reg.correoUser);
            cmd.Parameters.AddWithValue("@contra", reg.claveUser);
            cmd.Parameters.AddWithValue("@dirUsuario", reg.direcUser);
            cmd.Parameters.AddWithValue("@id_tipo", reg.idTipo);  //Para el mandar el CBO
            cn.Open();
            int NUM = cmd.ExecuteNonQuery();
            mensaje = $"Se ha insertado {NUM} Usuario";
            cn.Close();
            return mensaje;
        }

        public string NuevoCliente(UsuarioModel reg)
        {
            string mensaje = string.Empty;
            SqlConnection cn = new SqlConnection(cadena);
            SqlCommand cmd = new SqlCommand("usp_Usuario_Merge", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id_Usuario", reg.idUser);
            cmd.Parameters.AddWithValue("@nom_Usuario", reg.nombreUser);
            cmd.Parameters.AddWithValue("@correo_Usuario", reg.correoUser);
            cmd.Parameters.AddWithValue("@contra", reg.claveUser);
            cmd.Parameters.AddWithValue("@dirUsuario", reg.direcUser);
            cmd.Parameters.AddWithValue("@id_tipo", "2");  //Tipo Cliente por Defecto
            cn.Open();
            int NUM = cmd.ExecuteNonQuery();
            mensaje = $"Se ha insertado {NUM} Usuario";
            cn.Close();
            return mensaje;
        }
    }
}
