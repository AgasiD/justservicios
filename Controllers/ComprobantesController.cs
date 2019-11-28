using JustServicios.Clases;
using JustServicios.Clases.Controladores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace JustServicios.Controllers
{
    [RoutePrefix("api/Comprobantes")]
    public class ComprobantesController : ApiController
    {
        private ControladorComprobante CComprobante = ControladorComprobante.getCComprobante();

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getComprobantesAdeudados")]
        public List<ComprobanteAdeudado> getComprobantesAdeudados(int nrocli, int empresa)
        {
            return CComprobante.getComprobantesAdeudados(nrocli, empresa);
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getComposicionSaldo")]
        public List<ComprobanteSaldo> getComposicionSaldo(int nrocli, string desde, string hasta, int empresa)
        {
            return CComprobante.getComposicionSaldo(nrocli, desde, hasta, empresa);
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("verificarComprobante")]
        public bool verificarComprobante(int idClicta, string usuario)
        {
            return CComprobante.verificarComprobante(idClicta, usuario);
        }

        //----------------------------------------------------PRESUPUESTOS

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getPresupuestos")]
        public List<presupCabecera> getPresupuestos(int empresa, int offset)
        {
            return CComprobante.getPresupuestos(empresa, offset);
        }

        
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getPresupCLikePedido")]
        public PedidoCab getPresupCLikePedido(int idPresup)
        {
            return CComprobante.getPresupCLikePedido(idPresup);
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getPresupDLikePedido")]
        public List<PedidoDet> getPresupDLikePedido(int idPresup)
        {
            return CComprobante.getPresupDLikePedido(idPresup );
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getPedPendientes")]
        public List<pedPendientes> getPedPendientes(int codven, bool veTodos, string consulta, int empresa)
        {
            return CComprobante.getPedPendientes(codven, veTodos, consulta, empresa);
        }

        //-----------------------------------------------------------------------FACTURAS
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getComprobantesFacturados")]
        public JsonResult<RequestHTTP> getComprobantesFacturados(string query, int offset, int codven, bool veTodos)
        {
            return Json(CComprobante.getComprobantesFacturados(query, offset, codven, veTodos));
        }
    }
}
