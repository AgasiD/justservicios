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
    public class ControladorSubRubro
    {
 
        private string table { get; } = "subrub";
        private static ControladorSubRubro instancia;
        private ControladorDatos cData { get; set; }
        private ControladorSubRubro()
        {
        }

        public static ControladorSubRubro getInstancia()
        {
            if (instancia == null)
                instancia = new ControladorSubRubro();
            return instancia;
        }
        public DicRequestHTTP getSubRubrosAcot()
        {
            return new ControladorDatos().getData("select codigo, descri from " + table);
        }
        public RequestHTTP getSubRubroAcot(int id)
        {
            return new ControladorDatos().getElement("select codigo, descri from " + table + " where codigo = " + id);
        }

        public DicRequestHTTP getSubRubros()
        {
            return new ControladorDatos().getData("select * from " + table);
        }
        public RequestHTTP getSubRubro(int id)
        {
            return new ControladorDatos().getElement("select * from " + table + " where codigo = " + id);
        }
    }
}
