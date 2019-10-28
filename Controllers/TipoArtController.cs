using System.Web.Http;
using System.Web.Http.Results;
namespace JustServicios.Controllers
{
    [RoutePrefix("api/tipoArt")]
    public class TipoArtController : ApiController
    {
        ControladorTipoArt cControlador = ControladorTipoArt.getInstancia();
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getTipoArt")]
        public JsonResult<RequestHTTP> getTipoArt(int id)
        {
            return Json(cControlador.getTipoArt(id));
        }
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getTipoArts")]
        public JsonResult<DicRequestHTTP> getTipoArts()
        {
            return Json(cControlador.getTipoArts());
        }
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getTipoArtAcot")]
        public JsonResult<RequestHTTP> getTipoArtAcot(int id)
        {
            return Json(cControlador.getTipoArtAcot(id));
        }
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getTipoArtsAcot")]
        public JsonResult<DicRequestHTTP> getTipoArtsAcot()
        {
            return Json(cControlador.getTipoArtsAcot());
        }
    }
}
