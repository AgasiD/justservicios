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
    public class ControladorZona
    {
        private string table { get; } = " zonas ";
        private static ControladorZona instancia;
        private ControladorDatos cData { get; set; }
        private ControladorZona()
        {
        }

        public static ControladorZona getInstancia()
        {
            if (instancia == null)
                instancia = new ControladorZona();
            return instancia;
        }
        public DicRequestHTTP getZonasAcot()
        {
            return new ControladorDatos().getData("select codigo, descri from " + table);
        }
        public RequestHTTP getZonaAcot(int id)
        {
            return new ControladorDatos().getElement("select codigo, descri from " + table + " where codigo = " + id);
        }

        public DicRequestHTTP getZonas()
        {
            return new ControladorDatos().getData("select * from " + table);
        }
        public RequestHTTP getZona(int id)
        {
            return new ControladorDatos().getElement("select * from " + table + " where codigo = " + id);
        }
    }
}
