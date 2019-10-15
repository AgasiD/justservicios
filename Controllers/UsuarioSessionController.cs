using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace JustServicios
{
    [RoutePrefix("api/UsuarioSession")]
    public class UsuarioSessionController : ApiController
    {
       
        [HttpPost]
        [Route("validarUsuario")]
        public bool validarUsuario(JObject usupass)
        {
            try
            {
                string usuario = usupass["user"].ToString(),
                    pass = usupass["pass"].ToString(),
                    aux;
                passwd aux1;
                using (GestionEntities bd = new GestionEntities())
                {
                    aux1 = bd.passwd.Single(a => a.usuario == usuario && pass == a.passwd1);
                }
                if (String.Compare(string.Format("{0,-30}", usuario), aux1.usuario, false) == 0)
                {
                    if (String.Compare(string.Format("{0,-30}", pass), aux1.passwd1, false) == 0)
                        return true;
                    else
                        return false;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }


        [HttpGet]
        [Route("getDatos")]
        public Usuario getDatos(int empresaid, string usu, int punto)
        {
            try
            {
                var usuario = new Usuario();
                usuario.usuario = usu;
                usuario.empresaid = empresaid;
                usuario.punto = punto;
                using (GestionEntities bd = new GestionEntities())
                {
                    usuario.empresa = this.getEmpresa(usuario.empresaid).empresa1;
                    usuario.usuarioid = this.getUsuarioNombre(usuario.usuario);
                    usuario.token = bd.token.Single().token1;
                    usuario.vendeasoc = bd.passwd.Single(a => a.idusuario == usuario.usuarioid).vendeasoc;
                    usuario.veTodos = bd.passwd.Single(a => a.idusuario == usuario.usuarioid).vetodosvend;
                }
                return usuario;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public class Usuario
        {
            public int punto { get; set; }
            public int empresaid { get; set; }
            public int usuarioid { get; set; }
            public string usuario { get; set; }
            public string empresa { get; set; }
            public string token { get; set; }
            public decimal vendeasoc { get; set; }
            public bool veTodos { get; set; }
        }

        [HttpGet]
        [Route("getEmpresa")]
        public empresa getEmpresa(int empresaid)
        {
            try {
                using (GestionEntities bd = new GestionEntities())
                {
                return bd.empresa.Single(a => a.id == empresaid);
            }
            }
            catch (Exception)
            {
                return null;
            }
        }
        [HttpGet]
        [Route("getConfigen")]
        public configen GetConfigen(int empresaid)
        {
            
            try {

                using (GestionEntities bd = new GestionEntities())
                    return bd.configen.Single(a => a.empresa == empresaid);
            }
            catch (Exception)
            {
                return null;
            }
        }
        [Route("getUsuarioNombre")]
        public int getUsuarioNombre(string nombre)
        {
            try
            {
                using (GestionEntities bd = new GestionEntities())
                {

                    return bd.passwd.Single(a => a.usuario == nombre).idusuario;
                }
            }
            catch (Exception)
            {
                return 0;
            }

        }

    }


}