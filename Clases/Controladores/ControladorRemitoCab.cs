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
    public class ControladorRemitoCab
    {
        private string table { get; } = "RemitoCab";
        private static ControladorRemitoCab instancia;
        private ControladorDatos cData { get; set; }
        private ControladorRemitoCab()
        {
        }

        public static ControladorRemitoCab getInstancia()
        {
            if (instancia == null)
                instancia = new ControladorRemitoCab();
            return instancia;
        }

        public DicRequestHTTP getRemitoCabs()
        {
            return new ControladorDatos().getData("select * from " + table);
        }
        public RequestHTTP getRemitoCabByID(int idDetalle)
        {
            return new ControladorDatos().getElement("select * from " + table + " where id = " + idDetalle);
        }
        public RequestHTTP getRemitoCab(string letra, string tipodoc, int punto, int numero)
        {
            return new ControladorDatos().getElement("select * from " + table + " where tipodoc = '" + tipodoc + "' and letra = '" + letra + "' and punto = " + punto + " and numero = " + numero);
        }

        public RequestHTTP getRemitoDetOfRemitoCab(int idCabecera)
        {
            return new ControladorDatos().getElement("select * from " + table + " where cabeceraid = " + idCabecera);
        }

        public RemitoCab nuevoRemitoCab(JObject jCabecera)
        {
            try
            {
                return new RemitoCab(jCabecera);
            }
            catch(Exception e)
            {
                return null;
            }
               }
    }
}
