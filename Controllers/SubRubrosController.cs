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
    [RoutePrefix("api/subrub")]
    public class SubRubroController : ApiController
    {
        ControladorSubRubro cControlador = ControladorSubRubro.getInstancia();
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getSubRubro")]
        public JsonResult<RequestHTTP> getSubRubro(int id)
        {
            return Json(cControlador.getSubRubro(id));
        }
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getSubRubros")]
        public JsonResult<DicRequestHTTP> getSubRubros()
        {
            return Json(cControlador.getSubRubros());
        }
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getSubRubroAcot")]
        public JsonResult<RequestHTTP> getSubRubroAcot(int id)
        {
            return Json(cControlador.getSubRubroAcot(id));
        }
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getSubRubrosAcot")]
        public JsonResult<DicRequestHTTP> getSubRubrosAcot()
        {
            return Json(cControlador.getSubRubrosAcot());
        }
    }
}
