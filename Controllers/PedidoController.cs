using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using JustServicios;
using JustServicios.Clases;


    [System.Web.Http.RoutePrefix("api/Pedido")]
public class PedidoController : ApiController
{
    ControladorPedido cPedido = ControladorPedido.getCPedido();
    ControladorTotales cTotales = ControladorTotales.getCTotales();   

    [System.Web.Http.HttpPost]
    [System.Web.Http.Route("finalizarPedido")]
    public JsonResult<RequestHTTP> finalizarPedido(JObject cabydet)
    {
        string token = cabydet["token"].ToString();
        if (!ControladorToken.comprobarToken(token))
        {
            return null;
        }
        return Json(cPedido.finalizarPedido(cabydet));
        
       
    }
    //Item Pedido
    [System.Web.Http.HttpGet]
    [System.Web.Http.Route("getPedido")]
    public JsonResult<RequestHTTP> GetPedido(string filtro, string token, int offset)
    {
        if (!ControladorToken.comprobarToken(token))
        {
            return null;
        }
        return Json(cPedido.getPedido(filtro, offset));
    }
    [System.Web.Http.HttpGet]
    [System.Web.Http.Route("getPedidoFecha")]
    public List<PedidoCab> getPedidoFecha(string date, string token)
    {
        if (!ControladorToken.comprobarToken(token))
        {
            return null;
        }
        return cPedido.getPedidoFecha(date);
    }


    [System.Web.Http.HttpGet]
    [System.Web.Http.Route("getItemPedido")]
    public List<PedidoDet> GetItemPedido(int id, string token)
    {
        if (!ControladorToken.comprobarToken(token))
        {
            return null;
        }
        return cPedido.getItemPedido(id);
    }
    [System.Web.Http.HttpGet]
    [System.Web.Http.Route("getPedidos")]
    public List<PedidoCab> GetPedidos(string token)
    {
        if (!ControladorToken.comprobarToken(token))
        {
            return null;
        }
        return cPedido.getPedidos();
    }

    //------------------------------------------Pedidos pendientes

    [System.Web.Http.HttpGet]
    [System.Web.Http.Route("getPedidosPendientes")]
    public List<PedPendientes> getPedidosPendientes(int nrocli, string codpro, string token) 
    {
        return cPedido.getPedidosPendientes(nrocli, codpro, token);
    }

    //-----------------------------------------Ultimos precios 
    [System.Web.Http.HttpGet]
    [System.Web.Http.Route("getUltimosPrecios")]
    public List<UltimoPrecio> getUltimosPrecios(int nrocli, string codpro, string token)
    {
        return cPedido.getUltimosPrecios(nrocli, codpro, token);
    }




    // Totales
    [System.Web.Http.HttpPost]
    [System.Web.Http.Route("getTotales")]
    public Totales getTotales(JObject lista)
    {
        string token = lista["token"].ToString();
        if (!ControladorToken.comprobarToken(token))
        {
            return null;
        }
        return cTotales.getTotales(lista);
    }


        //Productos

        ControladorProducto cProducto = ControladorProducto.getCProducto();
     
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getProducto")]
        public itemProducto getProducto(int id, int lista, int empid, string token)
        {
          /*  if (!ControladorToken.comprobarToken(token))
            {
                return null;
            }
              try
            {*/
                return cProducto.getProducto(id, lista, empid);
           /* }
            catch (Exception)
            {
                return null;
            }*/
        }

   

        //Clientes

      
       



    }

