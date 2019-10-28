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
    public class Controlador
    {
        private string table { get; }
        private static Controlador instancia;
        private ControladorDatos cData { get; set; }
        private Controlador()
        {
        }

        public static Controlador getInstancia()
        {
            if (instancia == null)
                instancia = new Controlador();
            return instancia;
        }
        public DicRequestHTTP get ()
        {
            return cData.getData("select codigo, descri from " + table);
        }
        public RequestHTTP get (int id)
        {
            return cData.getElement("select codigo, descri from " + table + " where codigo = " + id);
        }

    }
}
