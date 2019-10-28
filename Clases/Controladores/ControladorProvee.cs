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
    public class ControladorProvee
    {
        private string table { get; } = "provee";
        private static ControladorProvee instancia;
        private ControladorDatos cData { get; set; }
        private ControladorProvee()
        {
        }

        public static ControladorProvee getInstancia()
        {
            if (instancia == null)
                instancia = new ControladorProvee();
            return instancia;
        }
        public DicRequestHTTP getProveedoresAcot()
        {
            return new ControladorDatos().getData("select nropro, razsoc from " + table);
        }
        public RequestHTTP getProveedoreAcot(int id)
        {
            return new ControladorDatos().getElement("select nropro, razsoc  from " + table + " where nropro = " + id);
        }

        public DicRequestHTTP getProveedores()
        {
            return new ControladorDatos().getData("select * from " + table);
        }
        public RequestHTTP getProveedor(int id)
        {
            return new ControladorDatos().getElement("select * from " + table + " where nropro = " + id);
        }


    }
}
