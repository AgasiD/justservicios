
using System.Web.Http;
using System.Web.Http.Results;
namespace JustServicios.Controllers
{
    [RoutePrefix("api/zona")]
    public class ZonaController : ApiController
    {
        ControladorZona cControlador = ControladorZona.getInstancia();
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getZona")]
        public JsonResult<RequestHTTP> getZona(int id)
        {
            return Json(cControlador.getZona(id));
        }
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getZonas")]
        public JsonResult<DicRequestHTTP> getZonas()
        {
            return Json(cControlador.getZonas());
        }
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getZonaAcot")]
        public JsonResult<RequestHTTP> getZonaAcot(int id)
        {   
            return Json(cControlador.getZonaAcot(id));
        }
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getZonasAcot")]
        public JsonResult<DicRequestHTTP> getZonasAcot()
        {
            return Json(cControlador.getZonasAcot());
        }
    }
}
