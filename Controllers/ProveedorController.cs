using JustServicios.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using Newtonsoft.Json;
namespace JustServicios.Controllers
{
    [RoutePrefix("api/provee")]
    public class ProveeController : ApiController
    {
        ControladorProvee cControlador = ControladorProvee.getInstancia();
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getProveedor")]
        public JsonResult<RequestHTTP> getProveedor(int id)
        {
            return Json(cControlador.getProveedor(id));
        }
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getProveedores")]
        public JsonResult<DicRequestHTTP> getProveedores()
        {
            return Json(cControlador.getProveedores());
        }
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getProveedorAcot")]
        public JsonResult<RequestHTTP> getProveedorAcot(int id)
        {
            return Json(cControlador.getProveedoreAcot(id));
        }
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getProveedoresAcot")]
        public JsonResult<DicRequestHTTP> getProveedoresAcot()
        {
            return Json(cControlador.getProveedoresAcot());
        }
    }
}
