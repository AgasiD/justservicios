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
    public class ControladorMarca
    {
        private string table { get; } = "marcas";
        private static ControladorMarca instancia;
        private ControladorMarca()
        {
        }

        public static ControladorMarca getInstancia()
        {
            if (instancia == null)
                instancia = new ControladorMarca();
            return instancia;
        }
        public DicRequestHTTP getMarcasAcot()
        {
            return new ControladorDatos().getData("select codigo, descripcion descri from " + table);
        }
        public RequestHTTP getMarcaAcot(int id)
        {
            return new ControladorDatos().getElement("select codigo, descripcion descri from " + table + " where codigo = " + id);
        }

        public DicRequestHTTP getMarcas()
        {
            return new ControladorDatos().getData("select * from " + table);
        }
        public RequestHTTP getMarca(int id)
        {
            return new ControladorDatos().getElement("select * from " + table + " where codigo = " + id);
        }
    }
}
