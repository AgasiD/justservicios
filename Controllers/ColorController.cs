using System.Web.Http;
using System.Web.Http.Results;
namespace JustServicios.Controllers
{
    [RoutePrefix("api/color")]
    public class ColorController : ApiController
    {
        ControladorColor cControlador = ControladorColor.getInstancia();
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getColor")]
        public JsonResult<RequestHTTP> getColor(int id)
        {
            return Json(cControlador.getColor(id));
        }
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getColores")]
        public JsonResult<DicRequestHTTP> getColores()
        {
            return Json(cControlador.getColores());
        }
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getColorAcot")]
        public JsonResult<RequestHTTP> getColorAcot(int id)
        {
            return Json(cControlador.getColorAcot(id));
        }
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getColoresAcot")]
        public JsonResult<DicRequestHTTP> getColoresAcot()
        {
            return Json(cControlador.getColoresAcot());
        }
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getSomeAcot")]
        public JsonResult<DicRequestHTTP> getSome(string param)
        {
            return Json(cControlador.getSomeColorAcot(param ));
        }
    }
}
