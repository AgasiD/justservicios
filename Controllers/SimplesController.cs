using JustServicios.Clases;
using JustServicios.Clases.Controladores;
using Newtonsoft.Json.Linq;

using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace JustServicios
{
    [RoutePrefix("api/simples")]
    public class SimplesController : ApiController
    {
        [HttpGet]
        [Route("getTranspo")]
        public JsonResult<RequestHTTP> GetTranspo()
        {
            var req = new RequestHTTP();
            try
            {
                List<MiTranspo> transportes;
                using (GestionEntities bd = new GestionEntities())
                    req.objeto = bd.Database.SqlQuery<MiTranspo>("select razsoc, codigo, direcc,telefo from transpo order by razsoc").ToList();

                
                return Json(req);
            }
            catch (Exception e)
            {
                return Json(req.falla(e));
            }
        }


        [HttpPost]
        [Route("crearArchivo")]
        public JsonResult<RequestHTTP> crearArchivo([FromBody] JObject query)
        {
            var req = new RequestHTTP();

            try
            {
                req.objeto = query["query"].ToString();
                StreamWriter sw = new StreamWriter(@"C:\inetpub\wwwroot\JustServicios\JustServicios\Reportes\query.txt");
                sw.WriteLine(req.objeto);
                sw.Close();
                return Json(req);

            }
            catch (Exception e)
            {
                return Json(req.falla(e));
            }
        }


        [HttpGet]
        [Route("getTransporte")]
        public JsonResult<RequestHTTP> GetTransporte(int id)
        {
            var req = new RequestHTTP();

            try
            {
                List<MiTranspo> transportes;
                using (GestionEntities bd = new GestionEntities())
                    req.objeto = bd.Database.SqlQuery<MiTranspo>("select razsoc, codigo, direcc,telefo  from transpo where id = " + id + "order by razsoc").ToList();


                return Json(req);

            }
            catch (Exception e)
            {
                return Json(req.falla(e));
            }
        }

        [HttpGet]
        [Route("getCondicion")]
        public DicRequestHTTP getCondicion()
        {
            /* try
             {*/
            return new ControladorDatos().getData("select * from condpago");
            }
            /*catch (Exception)
            {
                return null;
            }
        }
        */
        [HttpGet]
        [Route("getConceptos")]
        public List<conceptosincluidos> getConceptos()
        {
            try
            {
                using (GestionEntities bd = new GestionEntities())
                    return bd.conceptosincluidos.ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }
        [HttpGet]
        [Route("getVia")]
        public List<via> getVia()
        {
            try
            {
                using (GestionEntities bd = new GestionEntities())
                    return bd.via.ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getClientePresupuesto")]
        public JsonResult<RequestHTTP> getClientePresupuesto(int nrocli, string token)
        {
            if (!ControladorToken.comprobarToken(token))
               {
                var error = new RequestHTTP();
                    error.fallo = true;
                    error.error = "Token invalido";
                    return Json(error);
            }
               try {
            return Json(ControladorCliente.getCCliente().getClientePresupuesto(nrocli));
            }
            catch (Exception e)
            {
                var error = new RequestHTTP().falla(e);
                return Json(error);
            }
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getClientesModal")]
        public List<ClienteBuscador> getClientesModal(string cadena, string token, int codven, bool veTodos, int offset)
        {

            if (!ControladorToken.comprobarToken(token))
            {
                return null;
            }
            try
            {
                return ControladorCliente.getCCliente().getClientesPresupuesto(cadena, codven, veTodos, offset);

            }
            catch (Exception)
            {
                return null;
            }
        }
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getClientes")]
        public List<clienteModal> getClientePresupuesto(string token)
        {

            if (!ControladorToken.comprobarToken(token))
            {
                return null;
            }
            return ControladorCliente.getCCliente().getClientesPresupuesto();
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("esClienteUnico")]
        public bool esClienteUnico(string cadena, string token)
        {
            if (!ControladorToken.comprobarToken(token))
            {
                return false;
            }
            return ControladorCliente.getCCliente().esClienteUnico(cadena);
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getProductos")]
        public JsonResult<RequestHTTP> getProductos(string value, int empid, string token, int offset)
        {

            if (!ControladorToken.comprobarToken(token))
            {
                return null;
            }
            return Json<RequestHTTP>(ControladorProducto.getCProducto().getProductos(value, empid, offset));
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("productoUnico")]
        public bool productoUnico(string value, int empid, string token)
        {

            if (!ControladorToken.comprobarToken(token))
            {
                return false;
            }
            return ControladorProducto.getCProducto().productoUnico(value, empid);
        }

        [HttpGet]
        [Route("getSucursales")]
        public List<sucursal> getSucursal(int nrocli, string token)
        {

            try
            {

                if (!ControladorToken.comprobarToken(token))
                {
                    throw new Exception();
                }
                List<sucursal> s = new List<sucursal>();
                using (GestionEntities bd = new GestionEntities())
                {
                    if (bd.sucursal.Where(a => a.nrocli == nrocli).Count() == 0)
                    {
                        s.Add(new sucursal());
                        return s;
                    }
                    else
                        return bd.sucursal.Where(a => a.nrocli == nrocli).ToList();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        [HttpGet]
        [Route("getVende")]
        public List<vendedor> getVende(string token)
        {

           try
            {
                if (!ControladorToken.comprobarToken(token))
                {
                    throw new Exception();
                }
                using (GestionEntities bd = new GestionEntities())
                    return bd.Database.SqlQuery<vendedor>("select nombre, codven from vende").ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }
        [HttpGet]
        [Route("getAsociado")]
        public List<artasoc> getArtAsoc(string codpro, string token)
        {
            try
            {
                if (!ControladorToken.comprobarToken(token))
                {
                    throw new Exception();
                }
                using (GestionEntities bd = new GestionEntities())
                    return bd.artasoc.Where(a => a.codpro == codpro).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        [HttpGet]
        [Route("getTipoDoc")]
        public List<tipodoc> GetTipodoc(string token)
        {
            try
            {

                if (!ControladorToken.comprobarToken(token))
                {
                    throw new Exception();
                }
                using (GestionEntities bd = new GestionEntities())
                    return bd.tipodoc.ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getProductos")]
        public DicRequestHTTP getProductos(int empid, string token)
        {

            if (!ControladorToken.comprobarToken(token))
            {
                return null;
            }

            return ControladorProducto.getCProducto().getProductos(empid);
        }


        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getTasaPercep")]
        public decimal getTasaPercep(int localid, string token)
        {

            if (!ControladorToken.comprobarToken(token))
            {
                return 0;
            }
            using (GestionEntities bd = new GestionEntities())
            {//trae tasa de percepcion de la provincia segun la localidad pasada.

                int depid, provinid;

                depid = bd.localidades.Single(a => a.id == localid).departamento_id;
                provinid = bd.departamentos.Single(a => a.id == depid).provincia_id;
                return bd.provincias.Single(a => a.id == provinid).Percepib;
            }
        }


        [HttpGet]
        [Route("empresaPunto")]
        public JsonResult<RequestHTTP> empresaPunto(int empresaid)
        {
            var req = new RequestHTTP();
            try
            {
                List<WpuntoPorEmpresa_Result> lista;
                using (GestionEntities bd = new GestionEntities())
                {
                   lista = bd.WpuntoPorEmpresa(empresaid).ToList();
                }
                req.objeto = lista;
                return Json(req);
            }
            catch (Exception e)
            {
                return Json(req.falla(e));
            }

        }

        [HttpGet]
        [Route("getEmpresas")]
        public JsonResult<object> getEmpresas()
        {
            ListRequestHTTP devolucion = new ListRequestHTTP();
            try
            {
                List<empr> lista;
                using (GestionEntities bd = new GestionEntities())
                {
                    devolucion.objeto = bd.Database.SqlQuery<empr>("select id, empresa from empresa").ToList();
                }
                 
                devolucion.fallo = false;
                devolucion.error = "Sin error";
                var p = Json<object>(devolucion);
                return  p;
            }
            catch (Exception e)
            {
                devolucion.objeto = null;
                devolucion.fallo = true;
                devolucion.error = e.Message;
                return Json<object>(devolucion); ;
            }
        }
        [HttpGet]
        [Route("getEmpresa")]
        public empresa getEmpresa(int empresaid, string token)
        {
            try
            {

                if (!ControladorToken.comprobarToken(token))
                {
                    throw new Exception();
                }
                using (GestionEntities bd = new GestionEntities())
                {
                    return bd.empresa.Single(a => a.id == empresaid);
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        [Route("getEmpresaNom")]
        public int getEmpresaNom(string nombre, string token)
        {

            if (!ControladorToken.comprobarToken(token))
            {
                return -1;
            }
            using (GestionEntities bd = new GestionEntities())
            {
                return bd.empresa.Single(a => a.empresa1 == nombre).id;
            }
        }
        [Route("getUsuarioNombre")]
        public int getUsuarioNombre(string nombre, string token)
        {

            if (!ControladorToken.comprobarToken(token))
            {
                return -1;
            }
            using (GestionEntities bd = new GestionEntities())
            {
                return bd.passwd.Single(a => a.usuario == nombre).idusuario;
            }

        }

        [HttpGet]
        [Route("validarCuit")]
        public bool validarCuit(string cuit, string token)
        {
            try
            {

                if (!ControladorToken.comprobarToken(token))
                {
                    throw new Exception();
                }
                int p = 0;
                cuit = cuit.Trim(' ');
                if (cuit == string.Empty)
                    throw new Exception();
                int total, digito;
                string[] q;
                char[] quitar = { '-', '/' };
                q = cuit.Split(quitar);
                string cuit2 = string.Empty;

                for (int h = 0; h < q.Length; h++)
                {
                    if (q[h] != string.Empty)
                        cuit2 += q[h];
                }
                cuit = cuit2;
                //txtDoc.Value = cuit;
                digito = int.Parse(cuit.Substring(10));
                if (cuit.Length == 11)
                {
                    int[] mult = new[] { 5, 4, 3, 2, 7, 6, 5, 4, 3, 2 };
                    char[] nums = cuit.ToCharArray();
                    total = 0;
                    for (int i = 0; i < mult.Length; i++)
                    {
                        total += int.Parse(nums[i].ToString()) * mult[i];
                    }

                    var resto = total % 11;
                    if (resto == 0)
                        p = 0;
                    else if (resto == 1)
                        p = 9;
                    else
                        p = 11 - resto;
                }
                else
                {
                    throw new Exception();
                }

                if (p == digito)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        [HttpGet]
        [Route("getMonedas")]
        public DicRequestHTTP getMonedas(string token)
        {
          
            return new ControladorDatos().getData("select * monedas order by ncotiza desc");
        
        }

        [HttpGet]
        [Route("getCotizacion")]
        public decimal getCotizacion(string codigo)
        {
            using (GestionEntities bd = new GestionEntities())
            {
                return bd.monedas.Single(a => a.codigo == codigo).ncotiza;
            }
        }

        [HttpGet]
        [Route("getData")]
        public DicRequestHTTP getData(string query)
        {
            return new ControladorDatos().getData(query); 
        }






        [HttpGet]
        [Route("getMasDatos")]
        public FWmasDatos getMasDatos(int nrocli, string token, int empresa)
        {

            {
                FWmasDatos resul = new FWmasDatos();
                /*try
                    if (!ControladorToken.comprobarToken(token))
                        throw new Exception();
                  */
                using (GestionEntities bd = new GestionEntities())
                {
                    resul.convertirFWmasDatos(bd.FWmasDatos(nrocli).Single());
                    resul.codMoneda = bd.configen.Single(a => a.empresa == empresa).nmonedacl;
                    resul.cotizMoneda = bd.monedas.Single(a => a.codigo == resul.codMoneda).ncotiza;
                    return resul;
                }
                /*            }
                            catch (Exception)
                            {
                                return null;
                            }*/


            }
        }


        [HttpGet]
        [Route("prueba")]
        public string prueba()
        {
             string connetionString = ConfigurationManager.ConnectionStrings["GestionConnection"].ConnectionString;
            return connetionString;
        }


    }

    public class vendedor
    {
        public decimal codven { get; set; }
        public string nombre { get; set; }
    }
}