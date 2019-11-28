using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace JustServicios.Controllers
{
    [RoutePrefix("api/formularios")]
    public class FormulariosController : ApiController
    {
        public ControladorProducto cProducto = ControladorProducto.getCProducto();

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("cambiarCodigo")]
        public string cambiarCodigo(JObject codigos)
        {
            return cProducto.cambiarCodigo(codigos);
        }

        
    }
}
