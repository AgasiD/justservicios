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
    [RoutePrefix("api/rubro")]
    public class RubroController : ApiController
    {
        ControladorRubros cControlador = ControladorRubros.getInstancia();
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getRubro")]
        public JsonResult<RequestHTTP> getRubro(int id)
        {
            return Json(cControlador.getRubro(id));
        }
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getRubros")]
        public JsonResult<DicRequestHTTP> getRubros()
        {
            return Json(cControlador.getRubros());
        }
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getRubroAcot")]
        public JsonResult<RequestHTTP> getRubroAcot(int id)
        {
            return Json(cControlador.getRubroAcot(id));
        }
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getRubrosAcot")]
        public JsonResult<DicRequestHTTP> getRubrosAcot()
        {
            return Json(cControlador.getRubrosAcot());
        }
    }
}
