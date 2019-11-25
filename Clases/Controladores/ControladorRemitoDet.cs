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
using Newtonsoft.Json;

namespace JustServicios
{
    public class ControladorRemitoDet
    {
        private string table { get; } = "Remitodet";
        private static ControladorRemitoDet instancia;
        private ControladorDatos cData { get; set; }
        private ControladorRemitoDet()
        {
        }

        public static ControladorRemitoDet getInstancia()
        {
            if (instancia == null)
                instancia = new ControladorRemitoDet();
            return instancia;
        }
       
        public DicRequestHTTP getRemitoDets()
        {
            return new ControladorDatos().getData("select * from " + table);
        }
        public RequestHTTP getRemitoDet(int idDetalle)
        {
            return new ControladorDatos().getElement("select * from " + table + " where id = " + idDetalle);
        }
        public RequestHTTP getRemitoDetOfRemitoCab(int idCabecera)
        {
            return new ControladorDatos().getElement("select * from " + table + " where cabeceraid = " + idCabecera);
        }

        public List<RemitoDet> nuevoRemitoDet(JToken jDetalle, JToken jCab)
        {
            try
            {
                int empresa = Convert.ToInt32(jCab["empresaid"]);
                stocks item;
                object p;
                var detalle = new List<RemitoDet>();
                RemitoDet itemDetalle;
                for (int i = 0; i < jDetalle.ToArray().Count(); i++)
                {
                    if (jDetalle[i]["asociado"].ToString().Contains("["))
                        jDetalle[i]["asociado"] = "";
                     p = JsonConvert.DeserializeObject<object>(jDetalle[i].ToString());
                  //  itemDetalle = new RemitoDet(p);
                }
                return detalle;

            }
            catch (Exception e)
            {
                return null;
            }

        }


    }
}
