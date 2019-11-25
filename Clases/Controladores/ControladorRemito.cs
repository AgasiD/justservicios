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
    public class ControladorRemito
    {
        private static ControladorRemitoCab cRemitoCab = ControladorRemitoCab.getInstancia();
        private static ControladorRemitoDet cRemitoDet = ControladorRemitoDet.getInstancia();
        private static ControladorRemito instancia;
        private ControladorDatos cData { get; set; }
        private ControladorRemito()
        {
        }

        public static ControladorRemito getInstancia()
        {
            if (instancia == null)
                instancia = new ControladorRemito();
            return instancia;
        }


        public RequestHTTP finalizarRemito(JObject cabydet)
        {
            var cabecera = cRemitoCab.nuevoRemitoCab((JObject)cabydet["cabecera"]);
            var detalle = cRemitoDet.nuevoRemitoDet((JObject)cabydet["detalle"], (JObject)cabydet["cabecera"]);
            return new RequestHTTP();
        }
       
      

    }
}
