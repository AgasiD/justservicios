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
    [RoutePrefix("api/concepto")]
    public class ConceptoController : ApiController
    {
        private ControladorConcepto cConcepto = ControladorConcepto.getInstancia();
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getConceptos")]
        public DicRequestHTTP getConceptos()
        {
            return cConcepto.getConceptos();
        }
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getConceptosParam")]
        public DicRequestHTTP getConceptos(string param)
        {
            return cConcepto.getConceptos(param);
        }
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getConcepto")]
        public RequestHTTP getConcepto(int id)
        {
            return cConcepto.getConcepto(id);
        }
    }
}
