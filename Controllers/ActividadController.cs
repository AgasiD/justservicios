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
    [RoutePrefix("api/actividad")]
    public class ActividadController : ApiController
    {
        ControladorActividad cControlador = ControladorActividad.getInstancia();
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getActividad")]
        public JsonResult<RequestHTTP> getActividad(int id)
        {
            return Json(cControlador.getActividad(id));
        }
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getActividades")]
        public JsonResult<DicRequestHTTP> getActividades()
        {
            return Json(cControlador.getActividades());
        }
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getActividadAcot")]
        public JsonResult<RequestHTTP> getActividadAcot(int id)
        {
            return Json(cControlador.getActividadAcot(id));
        }
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getActividadesAcot")]
        public JsonResult<DicRequestHTTP> getActividadesAcot()
        {
            return Json(cControlador.getActividadesAcot());
        }
    }
}
