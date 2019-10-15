using JustServicios.Clases;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace JustServicios
{
    [RoutePrefix("api/presupuesto")]
    public class PresupuestoController : ApiController
    {
        private ControladorTotales cTotales = ControladorTotales.getCTotales();
        private ControladorPresupuesto cPresupuesto = ControladorPresupuesto.getCPresupuesto();
        private ControladorProducto cProducto = ControladorProducto.getCProducto();

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("getTotales")]
        public Totales getTotales(JObject lista)
        {
            string token = lista["token"].ToString();
            if (!ControladorToken.comprobarToken(token))
            {
                return null;
            }
            return cTotales.getTotales(lista);
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("finalizarPresup")]
        public JsonResult<RequestHTTP> finalizarPresupuesto(JObject cabydet)
        {
            string token = cabydet["token"].ToString();
            if (!ControladorToken.comprobarToken(token))
            {
                return null;
            }
            return Json(cPresupuesto.finalizarPresupuesto(cabydet));
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getProducto")]
        public itemProducto getProducto(int id, int lista, int empid, string token)
        {
            if (!ControladorToken.comprobarToken(token))
            {
                return null;
            }
            return cProducto.getProducto(id, lista, empid);
        }
        public Totales GetTotales(JObject lista)
        {
            string token = lista["token"].ToString();
            if (!ControladorToken.comprobarToken(token))
            {
                return null;
            }

            return cTotales.getTotales(lista);
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getItemPresup")]
        public List<presupd> getItemPresup(int id, string token)
        {
            if (!ControladorToken.comprobarToken(token))
            {
                return null;
            }
            return cPresupuesto.getItemPresup(id);
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getPresup")]
        public JsonResult<RequestHTTP> getPresup(string filtro, string token, int offset)
        {

            if (!ControladorToken.comprobarToken(token))
            {
                return null;
            }
            return Json<RequestHTTP>(cPresupuesto.getPresup(filtro, offset));
        }


        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getPresupFecha")]
        public List<presupCabecera> getPresupFecha(string date, string token, int empresa, int offset)
        {

            if (!ControladorToken.comprobarToken(token))
            {
                return null;
            }
            return cPresupuesto.getPresupFecha(empresa, date, offset);
        }
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getPresupNrocli")]
        public List<presupc> getPresupNrocli(int id, string token)
        {

            if (!ControladorToken.comprobarToken(token))
            {
                return null;
            }
            return cPresupuesto.getPresupNrocli(id);
        }
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getPresupRazon")]
        public List<presupc> getPresupRazon(string razon, string token)
        {

            if (!ControladorToken.comprobarToken(token))
            {
                return null;
            }
            return cPresupuesto.getPresupRazon(razon);
        }
 
}
}