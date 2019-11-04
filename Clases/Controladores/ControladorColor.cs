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
    public class ControladorColor
    {
        private string table { get; } = "coloress";
        private static ControladorColor instancia;
        private ControladorColor()
        {
        }

        public static ControladorColor getInstancia()
        {
            if (instancia == null)
                instancia = new ControladorColor();
            return instancia;
        }
        public DicRequestHTTP getColores()
        {
            return new ControladorDatos().getData("select * from " + table);
        }
        public RequestHTTP getColor(int id)
        {
            return new ControladorDatos().getElement("select * from " + table + " where codigo = " + id);
        }

        public DicRequestHTTP getColoresAcot()
        {
            return new ControladorDatos().getData("select codigo, descri from " + table);
        }
        public RequestHTTP getColorAcot(int id)
        {
            return new ControladorDatos().getElement("select codigo, descri from " + table + " where codigo = " + id);
        }

        public DicRequestHTTP getSomeColor(string param)
        {
            return new ControladorDatos().getData("select * from " + table + " where codigo like '%" + param + "%' or descri like '%"+ param +"%'" );
        }

        public DicRequestHTTP getSomeColorAcot(string param)
        {
            return new ControladorDatos().getData("select codigo, descri from " + table + " where codigo like '%" + param + "%' or descri like '%" + param + "%'");
        }
    }
}
