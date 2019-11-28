using JustServicios.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace JustServicios.Controllers
{
    [RoutePrefix("api/grupo")]
    public class GrupoController : ApiController
    {
        private ControladorGrupo cGrupo = ControladorGrupo.getCGrupo();


        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getGrupos")]
        public DicRequestHTTP getGrupos()
        {
            return cGrupo.getGrupos();
        }
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getGruposParam")]
        public DicRequestHTTP getGrupos(string param)
        {
            return cGrupo.getGrupos(param);
        }
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getGrupo")]
        public RequestHTTP getGrupo(int id)
        {
            return cGrupo.getGrupo(id);
        }

    }
}
