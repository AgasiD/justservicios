using JustServicios.Clases;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace JustServicios.Controllers
{
    [RoutePrefix("api/reportes")]
    public class ReportController : ApiController
    {
        
        ControladorReportes reports = ControladorReportes.getCReporter();

        //------------------------------------------CLIENTES

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("cantClientes")]
        public int cantClientes(int nrocli)
        {
           return reports.cantClientes( nrocli);
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getClientes")]
        public JsonResult<RequestHTTP> getClientes(int codven, bool veTodos, int offset)
        {
            return Json(reports.getClientes(codven, veTodos, offset));
        }
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getClientesFiltro")]
        public JsonResult<RequestHTTP> getClientesFiltro(string param, int codven, bool veTodos, int offset)
        {//
            return Json(reports.getClientesFiltro(param, codven, veTodos, offset));
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getCliente")]
        public JsonResult<RequestHTTP> getClientes(int param)
        {
            return Json(reports.getCliente(param));
        }

        //------------------------------------------VENDEDORES

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getVendedores")]
        public JsonResult<RequestHTTP> getVendedores()
        {
            return Json(reports.getVendedores());
        }
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getVendedoresFiltro")]
        public JsonResult<RequestHTTP> getVendedoresFiltro(string param)
        {
            return Json(reports.getVendedoresFiltro(param));
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getVendedor")]
        public JsonResult<RequestHTTP> getVendedor(int param)
        {
            return Json(reports.GetVende(param));
        }


        //------------------------------------------ZONAS

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getZonas")]
        public JsonResult<RequestHTTP> getZonas()
        {
            return Json(reports.getZonas());
        }
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getZonasFiltro")]
        public JsonResult<RequestHTTP> getZonasFiltro(string param)
        {
            return Json(reports.getZonasFiltro(param));
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getZona")]
        public JsonResult<RequestHTTP> getZona(int param)
        {
            return Json(reports.getZona(param));
        }

        //------------------------------------------PROVINCIAS


        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getProvincias")]
        public JsonResult<RequestHTTP> getProvincias()
        {
            return Json(reports.getPrvincias());
        }
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getProvinciasFiltro")]
        public JsonResult<RequestHTTP> getProvinciasFiltro(string param)
        {
            return Json(reports.getProvinciasFiltro(param));
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getProvincia")]
        public JsonResult<RequestHTTP> getProvincias(int param)
        {
            return Json(reports.getProvincia(param));
        }


        //------------------------------------------ RUBROS
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getRubros")]
        public JsonResult<RequestHTTP> getRubros()
        {
            return Json(reports.getRubros());
        }
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getRubrosFiltro")]
        public JsonResult<RequestHTTP> getRubrosFiltro(string param)
        {
            return Json(reports.getRubrosFiltro(param));
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getRubro")]
        public JsonResult<RequestHTTP> getRubro(int param)
        {
            return Json(reports.getRubro(param));
        }

        //------------------------------------------ SUBRUBROS
        
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getSubRubros")]
        public JsonResult<RequestHTTP> getSubRubros()
        {
            return Json(reports.getSubRubros());
        }

    
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getSubRubrosFiltro")]
        public JsonResult<RequestHTTP> getSubRubrosFiltro(string param)
        {
            return Json(reports.getSubRubrosFiltro(param));
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getSubRub")]
        public JsonResult<RequestHTTP> getSubRubro(int param)
        {
            return Json(reports.getSubRubro(param));
        }
      
    //------------------------------------------ MARCAS
    [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getMarcas")]
        public JsonResult<RequestHTTP> getMarcas()
        {
            return Json(reports.getmarcas());
        }
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getMarcasFiltro")]
        public JsonResult<RequestHTTP> getMarcasFiltro(string param)
        {
            return Json(reports.getmarcasFiltro(param));
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getMarca")]
        public JsonResult<RequestHTTP> getMarca(int param)
        {
            return Json(reports.getmarca(param));
        }

        //------------------------------------------PROVEEDORES
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getProveedores")]
        public JsonResult<RequestHTTP> getProveedores()
        {
            return Json(reports.getProveedores());
        }
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getProveedoresFiltro")]
        public JsonResult<RequestHTTP> getProveedoresFiltro(string param)
        {
            return Json(reports.getProveedoresFiltro(param));
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getProveedor")]
        public JsonResult<RequestHTTP> getProveedores(int param)
        {
            return Json(reports.getProveedor(param));

        }
        //------------------------------------- ACTIVIDADES

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getActividades")]
        public JsonResult<RequestHTTP> getActividades()
        {
            return Json(reports.getActividades());
        }
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getActividadesFiltro")]
        public JsonResult<RequestHTTP> getActividadesFiltro(string param)
        {
            return Json(reports.getActividadesFiltro(param));
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getActividad")]
        public JsonResult<RequestHTTP> getActividades(int param)
        {
            return Json(reports.getActividad(param));

        }
        //------------------------------------- TRANSPORTE

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getTransportes")]
        public JsonResult<RequestHTTP> getTransportes()
        {
            return Json(reports.getTransportes());
        }
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getTransportesFiltro")]
        public JsonResult<RequestHTTP> getTransportesFiltro(string param)
        {
            return Json(reports.getTransportesFiltro(param));
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getTranspo")]
        public JsonResult<RequestHTTP> getTransportes(int param)
        {
            return Json(reports.getTransporte(param));

        }

        //---------------------------------------- CONCEPTOS


        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getConceptos")]
        public JsonResult<RequestHTTP> getConceptos()
        {
            return Json(reports.getConceptos());
        }
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getConceptosFiltro")]
        public JsonResult<RequestHTTP> getConceptosFiltro(string param)
        {
            return Json(reports.getConceptosFiltro(param));
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getConcepto")]
        public JsonResult<RequestHTTP> getConcepto(int param)
        {
            return Json(reports.getConcepto(param));

        }


        //---------------------------------------- ARTICULOS
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getArticulos")]
        public JsonResult<RequestHTTP> getArticulos()
        {
            return Json(reports.getArticulos());
        }

  
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getArticulosFiltro")]
        public JsonResult<RequestHTTP> getArticulosFiltro(string param)
        {
            return Json(reports.getArticulosFiltro(param));
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getArticulo")]
        public JsonResult<RequestHTTP> getArticulo(string param)
        {
            return Json(reports.getArticulo(param));

        }

        //--------------------------------------------------USUARIOS
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getUsuarios")]
        public List<passwd> getUsuarios()
        {

            return reports.getUsuarios();

        }
        //--------------------------------------------------PUNTOS
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getPuntos")]
        public JsonResult<RequestHTTP> getPuntos(int empresa)
        {

            return Json(reports.getPuntos(empresa));

        }

        //-------------------------------------------------VENTAS

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getVentasAnuales")]
        public JsonResult<RequestHTTP> getVentas(int empresa, bool cotizaciones, string desde, string hasta, int operacion, bool checkope)
        {
            return Json(reports.getVentasAnuales(empresa, cotizaciones, desde, hasta, operacion, checkope));

        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getVentasPorVendedores")]
        public JsonResult<RequestHTTP> getVentasPorVendedores(int empresa, bool cotizaciones, string desde, string hasta)
        {
            return Json(reports.getVentasPorVendedores(empresa, cotizaciones, desde, hasta));
        }
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getVentasPorVendedor")]
        public JsonResult<RequestHTTP> getVentasPorVendedor(int empresa, int codven, bool cotizaciones, string desde, string hasta, int group)
        {
            return Json(reports.getVentasPorVendedor(empresa, codven, cotizaciones, desde, hasta, group));
        }
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getCobranzasVendedores")]
        public JsonResult<RequestHTTP> getCobranzasVendedores(int empresa, string desde, string hasta)
        {
            return Json(reports.getCobranzasVendedores(empresa, desde, hasta));
        }
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getCobranzasVendedor")]
        public JsonResult<RequestHTTP> getCobranzasVendedor(int empresa, int codven, bool cotizaciones, string desde, string hasta, int group)
        {
            return Json(reports.getCobranzasPorVendedor(empresa,codven, cotizaciones, desde, hasta, group));
        }
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getComprasProvee")]
        public JsonResult<RequestHTTP> GetComprasProveedores(int empresa, string desde, string hasta, int concepto, bool cotizaciones)
        {
            return Json(reports.getComprasProvee(empresa, desde, hasta, concepto, cotizaciones));
        }

    }
}

/*
 * 
 * 
Elem = clabes.Elegir();
while (!claves.ConjuntoVacio()){
    Elem = claves.Elegir();
    system.out.println("CLAVE: " + Elem );
    while(!dm.Recuperar(Elem).ConjuntoVacio()){
        String valor = dm.Recuperar(Elem).Elegir()
        System.out.println("Valor: " + valor);
        dm.Recuperar(Elem).Sacar(valor);
    }
}

    }*/
