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
    public class ControladorUsuario
    {

        private static ControladorUsuario instancia;
        private DicRequestHTTP req;
        private ControladorDatos cData = new ControladorDatos();
        private string table = "passwd";
        private ControladorUsuario()
        {
        }

        public static ControladorUsuario getCUsuario()
        {
            if (instancia == null)
                instancia = new ControladorUsuario();
            return instancia;
        }

        public DicRequestHTTP getUsuarios()//Obtiene todos
        {
            return cData.getData("select idusuario, usuario from " + table);
        }
        public DicRequestHTTP getUsuarios(string value)//Obtiene todos que cumplan la condicion
        {
            return cData.getData("select idusuario, usuario from " + table + " where usuario like '%" + value + "%' or idusuario like '" + value + "'");
        }
        public RequestHTTP getUsuario(int id)//Solo uno
        {
            return cData.getElement("select idusuario, usuario from " + table + " where idusuario = " + id);
        }

    }
}
