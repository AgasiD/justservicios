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
    public class ControladorRubros
    {
        private string table { get; } = "rubros";
        private static ControladorRubros instancia;
        private ControladorDatos cData { get; set; }
        private ControladorRubros()
        {
        }

        public static ControladorRubros getInstancia()
        {
            if (instancia == null)
                instancia = new ControladorRubros();
            return instancia;
        }
        public DicRequestHTTP getRubrosAcot ()
        {
            return new ControladorDatos().getData("select codigo, descri from " + table);
        }
        public RequestHTTP getRubroAcot(int id)
        {
            return new ControladorDatos().getElement("select * from " + table + " where codigo = " + id);
        }

        public DicRequestHTTP getRubros()
        {
            return new ControladorDatos().getData("select codigo, descri from " + table);
        }
        public RequestHTTP getRubro(int id)
        {
            return new ControladorDatos().getElement("select * from " + table + " where codigo = " + id);
        }
    }
}
