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
    [RoutePrefix("api/factura")]
    public class FacturaController : ApiController
    {
        ControladorFactura cControlador = ControladorFactura.getInstancia();
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getFactura")]
        public JsonResult<RequestHTTP> getFactura(int id)
        {
            return Json(cControlador.getFactura(id));
        }
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getFacturas")]
        public JsonResult<DicRequestHTTP> getFacturas()
        {
            return Json(cControlador.getFacturas());
        }
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getFacturaAcot")]
        public JsonResult<RequestHTTP> getFacturaAcot(int id)
        {
            return Json(cControlador.getFacturaAcot(id));
        }
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getFacturasAcot")]
        public JsonResult<DicRequestHTTP> getFacturasAcot()
        {
            return Json(cControlador.getFacturasAcot());
        }
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getPuntos")]
        public JsonResult<DicRequestHTTP> getPuntos(int empresa)
        {
            return Json(cControlador.getPuntos(empresa));
        }
    }
}
