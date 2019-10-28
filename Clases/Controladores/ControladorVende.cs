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
    public class ControladorVende
    {
        private string table { get; } = " vende ";
        private static ControladorVende instancia;
        private ControladorDatos cData { get; set; }
        private ControladorVende()
        {
        }

        public static ControladorVende getInstancia()
        {
            if (instancia == null)
                instancia = new ControladorVende();
            return instancia;
        }
        public DicRequestHTTP getVendedoresAcot()
        {
            return new ControladorDatos().getData("select codven, nombre from " + table);
        }
        public RequestHTTP getVendedorAcot(int id)
        {
            return new ControladorDatos().getElement("select codven, nombre from " + table + " where codven = " + id);
        }
        public DicRequestHTTP getVendedores()
        {
            return new ControladorDatos().getData("select * from " + table);
        }
        public RequestHTTP getVendedor(int id)
        {
            return new ControladorDatos().getElement("select * from " + table + " where codven = " + id);
        }
    }
}
