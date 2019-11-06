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
    public class ControladorConfigen
    {
        private string table { get; } = "configen";
        private static ControladorConfigen instancia;
        private ControladorDatos cData { get; set; }
        private ControladorConfigen()
        {
        }

        public static ControladorConfigen getInstancia()
        {
            if (instancia == null)
                instancia = new ControladorConfigen();
            return instancia;
        }
        public DicRequestHTTP getConfigens()
        {
            return new ControladorDatos().getData("select * from " + table);
        }
        public RequestHTTP getConfigen(int id)
        {
            return new ControladorDatos().getElement("select * from " + table + " where empresa = " + id);
        }

    }
}
