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
    public class ControladorActividad
    {
        private string table { get; } = "activida";
        private static ControladorActividad instancia;
        private ControladorDatos cData { get; set; }
        private ControladorActividad()
        {
        }

        public static ControladorActividad getInstancia()
        {
            if (instancia == null)
                instancia = new ControladorActividad();
            return instancia;
        }
       
        public DicRequestHTTP getActividadesAcot()
        {
            return new ControladorDatos().getData("select codigo, descri from " + table);
        }
        public RequestHTTP getActividadAcot(int id)
        {
            return new ControladorDatos().getElement("select codigo, descri from " + table + " where codigo = " + id);
        }

        public DicRequestHTTP getActividades()
        {
            return new ControladorDatos().getData("select * from " + table);
        }
        public RequestHTTP getActividad(int id)
        {
            return new ControladorDatos().getElement("select * from " + table + " where codigo = " + id);
        }

    }
}
