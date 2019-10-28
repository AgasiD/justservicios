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
    public class ControladorFactura
    {
        private string table { get; } = " factura ";
        private static ControladorFactura instancia;
        private ControladorDatos cData { get; set; }
        private ControladorFactura
()
        {
        }

        public static ControladorFactura getInstancia()
        {
            if (instancia == null)
                instancia = new ControladorFactura();
            return instancia;
        }

        public DicRequestHTTP getFacturasAcot()
        {
            return new ControladorDatos().getData("select codigo, descri from " + table);
        }
        public RequestHTTP getFacturaAcot(int id)
        {
            return new ControladorDatos().getElement("select codigo, descri from " + table + " where codigo = " + id);
        }

        public DicRequestHTTP getFacturas()
        {
            return new ControladorDatos().getData("select * from " + table);
        }
        public RequestHTTP getFactura(int id)
        {
            return new ControladorDatos().getElement("select * from " + table + " where codigo = " + id);
        }

        public DicRequestHTTP getPuntos(int empresa)
        {
            return new ControladorDatos().getData("select PUNTO from " + table + " where empresaid = " + empresa);
        }

    }
}
