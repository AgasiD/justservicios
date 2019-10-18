using JustServicios.Clases;
using JustServicios.Clases.Controladores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace JustServicios.Controllers
{
    [RoutePrefix("api/usuario")]
    public class UsuarioController : ApiController
    {
        private ControladorUsuario cUsuario = ControladorUsuario.getCUsuario();
        private DicRequestHTTP req;
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getUsuarios")]
        public DicRequestHTTP getUsuarios()
        {
            return cUsuario.getUsuarios();    
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getUsuariosParam")]
        public DicRequestHTTP getUsuarios(string param)
        {
            return cUsuario.getUsuarios(param);
        } 

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getUsuario")]
        public RequestHTTP getUsuario(int id)
        {
            return cUsuario.getUsuario(id);
        }

    }
}
