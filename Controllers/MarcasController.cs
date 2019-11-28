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
    [RoutePrefix("api/marca")]
    public class MarcaController : ApiController
    {
        ControladorMarca cControlador = ControladorMarca.getInstancia();
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getMarca")]
        public JsonResult<RequestHTTP> getMarca(int id)
        {
            return Json(cControlador.getMarca(id));
        }
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getMarcas")]
        public JsonResult<DicRequestHTTP> getMarcas()
        {
            return Json(cControlador.getMarcas());
        }
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getMarcaAcot")]
        public JsonResult<RequestHTTP> getMarcaAcot(int id)
        {
            return Json(cControlador.getMarcaAcot(id));
        }
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getMarcasAcot")]
        public JsonResult<DicRequestHTTP> getMarcasAcot()
        {
            return Json(cControlador.getMarcasAcot());
        }
    }
}
