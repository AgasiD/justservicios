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
    public class ControladorTipoArt
    {
        private string table { get; } = " tipoArt ";
        private static ControladorTipoArt instancia;
        private ControladorDatos cData { get; set; }
        private ControladorTipoArt()
        {
        }

        public static ControladorTipoArt getInstancia()
        {
            if (instancia == null)
                instancia = new ControladorTipoArt();
            return instancia;
        }
        public DicRequestHTTP getTipoArtsAcot()
        {
            return new ControladorDatos().getData("select codigo, descri from " + table);
        }
        public RequestHTTP getTipoArtAcot(int id)
        {
            return new ControladorDatos().getElement("select codigo, descri from " + table + " where codigo = " + id);
        }

        public DicRequestHTTP getTipoArts()
        {
            return new ControladorDatos().getData("select * from " + table);
        }
        public RequestHTTP getTipoArt(int id)
        {
            return new ControladorDatos().getElement("select * from " + table + " where codigo = " + id);
        }
        public DicRequestHTTP getSomeAcot(string param)
        {
            return new ControladorDatos().getData("select * from " + table + " where codigo like '%" + param + "%' or descri like '%" + param + "%'");
        }
        
    }
}
