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
    [RoutePrefix("api/clientes")]
    public class ClienteController : ApiController
    {
        ControladorCliente cCliente = ControladorCliente.getCCliente();
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("actualizarTopeCredito")]
        public bool actualizarTopeCredito(string query, string credito, int dias, bool masche, bool bloquear)
        {
            return cCliente.actualizarTopeCredito(query, credito, dias, masche, bloquear);
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getCliente")]
        public JsonResult<object> getCliente(int nrocli)
        {
            return Json<object>(cCliente.getCliente(nrocli));
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getClientes")]
        public JsonResult<DicRequestHTTP> getClientes()
        {
            return Json(cCliente.getClientes());
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getCondicion")]
        public string getCondicion(int condicion)
        {
            return cCliente.getConcepto(condicion);
        }
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getClienteAcot")]
        public JsonResult<object> getClienteAcot(int nrocli)
        {
            return Json<object>(cCliente.getClienteAcot(nrocli));
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getClientesAcot")]
        public JsonResult<DicRequestHTTP> getClientesAcot()
        {
            return Json(cCliente.getClientesAcot());
        }

    }
}
