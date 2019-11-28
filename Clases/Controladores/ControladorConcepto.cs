using JustServicios.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Results;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using JustServicios.Clases.Controladores;

namespace JustServicios
{
    public class ControladorConcepto
    {
        private string table { get; } = "concepto";
        private static ControladorConcepto instancia;
        private ControladorDatos cData { get; set; } = new ControladorDatos();
        private ControladorConcepto()
        {
        }
        public static ControladorConcepto getInstancia()
        {
            if (instancia == null)
                instancia = new ControladorConcepto();
            return instancia;
        }
        public DicRequestHTTP getConceptos()
        {
            return cData.getData("select codigo, descri from " + table);
        }
        public DicRequestHTTP getConceptos(string value)//Obtiene todos que cumplan la condicion
        {
            return cData.getData("select codigo, descri from " + table + " where descri like '%" + value + "%' or codigo  like '" + value + "'");
        }
        public RequestHTTP getConcepto(int id)
        {
            return cData.getElement("select codigo, descri from " + table + " where codigo = " + id);
        }
    }
}
