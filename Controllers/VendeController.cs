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
    [RoutePrefix("api/vende")]
    public class VendedorController : ApiController
    {
        ControladorVende cControlador = ControladorVende.getInstancia();
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getVendedor")]
        public JsonResult<RequestHTTP> getVendedor(int id)
        {
            return Json(cControlador.getVendedor(id));
        }
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getVendedores")]
        public JsonResult<DicRequestHTTP> getVendedores()
        {
            return Json(cControlador.getVendedores());
        }
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getVendedorAcot")]
        public JsonResult<RequestHTTP> getVendedorAcot(int id)
        {
            return Json(cControlador.getVendedorAcot(id));
        }
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getVendedoresAcot")]
        public JsonResult<DicRequestHTTP> getVendedoresAcot()
        {
            return Json(cControlador.getVendedoresAcot());
        }
    }
}
