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
    public class ControladorGrupo
    {
        private ControladorDatos cData { get; set; } = new ControladorDatos();
        private static ControladorGrupo instancia;
        private string table { get; } = "Grupocon";
        private ControladorGrupo()
        {
        }

        public static ControladorGrupo getCGrupo()
        {
            if (instancia == null)
                instancia = new ControladorGrupo();
            return instancia;
        }

       public DicRequestHTTP getGrupos()
        {
            return cData.getData("select * from " + table);
        }
        public DicRequestHTTP getGrupos(string value)//Obtiene todos que cumplan la condicion
        {
            return cData.getData("select * from " + table + " where grupocon like '%" + value + "%' or id  like '" + value + "'");
        }
        public RequestHTTP getGrupo(int id)
        {
            return cData.getElement("select * from " + table + " where id = " + id);
        }


    }
}
