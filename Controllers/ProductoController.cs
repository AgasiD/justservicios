using JustServicios.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace JustServicios.Controllers
{
    
    [RoutePrefix("api/Stock")]
    public class ProductoController : ApiController
    {
        public ControladorProducto cProducto = ControladorProducto.getCProducto();

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getArticulo")]
        public stock getProducto(string codpro)
        {
            return cProducto.getProductoCod(codpro);
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getDisponibles")]
        public JsonResult<RequestHTTP>  getDisponibles(string codpro, int empresa)
        {
            return Json<RequestHTTP>(cProducto.getDisponibles(codpro, empresa));
        }
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("verificarCodigo")]
        public bool verificarCodigo(string codpro)
        {
            return cProducto.existeProducto(codpro);
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getAll")]
        public JsonResult<DicRequestHTTP> getAll( )
        {
            return Json(cProducto.getAll());
        }
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getOne")]
        public JsonResult<RequestHTTP> getOne(string codpro)
        {
            return Json(cProducto.getOne(codpro));
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getSome")]
        public JsonResult<DicRequestHTTP> getSome(string param)
        {
            return Json(cProducto.getSome(param));
        }
        
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getAllAcot")]
        public JsonResult<DicRequestHTTP> getAllAcot()
        {
            return Json(cProducto.getAllAcot());
        }
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getOneAcot")]
        public JsonResult<RequestHTTP> getOneAcot(string codpro)
        {
            return Json(cProducto.getOneAcot(codpro));
        }
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getSomeAcot")]
        public JsonResult<DicRequestHTTP> getSomeAcot(string param)
        {
            return Json(cProducto.getSomeAcot(param));
        }


    }
}
