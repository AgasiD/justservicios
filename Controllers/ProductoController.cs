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
    
    [RoutePrefix("api/Stock")]
    public class ProductoController : ApiController
    {
        public ControladorProducto cProducto = ControladorProducto.getCProducto();

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getArticulo")]
        public stock getProducto(string codpro)
        {
            return cProducto.getProductoCod(codpro);
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getDisponibles")]
        public JsonResult<RequestHTTP>  getDisponibles(string codpro, int empresa)
        {
            return Json<RequestHTTP>(cProducto.getDisponibles(codpro, empresa));
        }
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("verificarCodigo")]
        public bool verificarCodigo(string codpro)
        {
            return cProducto.existeProducto(codpro);
        }



    }
}
