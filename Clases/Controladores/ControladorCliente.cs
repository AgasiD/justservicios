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


namespace JustServicios
{
    public class ControladorCliente
    {

        private static List<ClientePesupuesto> clientesPresupuesto;
        private static List<clienteModal> clienteModal;
        private static List<cliente> clientes { get; set; }
        private static ControladorCliente instancia;

        private ControladorCliente()
        {
        }

        public static ControladorCliente getCCliente()
        {
            if (instancia == null)
                instancia = new ControladorCliente();
            return instancia;
        }

        internal bool actualizarTopeCredito(string query, string credito, int dias, bool masche, bool bloquear)
        {
            try
            {
                int mascheques = Convert.ToInt32(masche)
                    , bloq = Convert.ToInt32(bloquear);
                using (GestionEntities bd = new GestionEntities())
                {
                    return Convert.ToBoolean(bd.Database.ExecuteSqlCommand("update cliente set credito = " + credito + ", credias = " + dias + ",bloqxcre = " + bloq + ",  masche = " + mascheques + " where " + query));
                }
            }catch(Exception e)
            {
                return false; // e.Message.ToString();
            }
        }

        internal string getConcepto(int condicion)
        {
            using(GestionEntities bd = new GestionEntities())
            {
                return bd.Database.SqlQuery<string>("select descripcion from condpago where codigo = " + condicion).Single();
            }
        }

        public RequestHTTP getCliente(int nrocli)
        {
            try
            {
                using (GestionEntities bd = new GestionEntities())
                {
                    var req = new RequestHTTP();
                    req.objeto = bd.cliente.Single(cliente => cliente.nrocli == nrocli);
                    
                    return req;
                }
            }catch(Exception e)
            {
                return new RequestHTTP().falla(e);
            }
        }

        

        //Pedido-Presupuesto

        //usado en el modal
        public List<clienteModal> getClientesPresupuesto()
        {
           
                if (clienteModal == null)
                {
                using (GestionEntities bd = new GestionEntities())
                    clienteModal = bd.clienteModal.ToList();
                }
                return clienteModal;
          
        }

        public List<ClienteBuscador> getClientesPresupuesto(string cadena, int codven, bool veTodos, int offset)
        {
            int id = 0;
            List<ClienteBuscador> lista = null;
            if (int.TryParse(cadena, out id))
                id = Convert.ToInt32(cadena);
            using (GestionEntities bd = new GestionEntities())
            {
                if (cadena != "0")
                {
                    if (veTodos)
                        lista = bd.Database.SqlQuery<ClienteBuscador>("SELECT cliente.nrocli, cliente.razsoc, cliente.fantasia, cliente.direcc, cliente.cuit, cliente.telef1, cliente.telef2, localidades.nombre, cliente.codven FROM cliente LEFT OUTER JOIN localidades ON localidades.id = cliente.locali where (razsoc like '%" + cadena + "%' or nrocli = " + id + ") order by nrocli  offset  " + offset + " rows fetch next 20 row only").ToList();
                    else
                        lista = bd.Database.SqlQuery<ClienteBuscador>("SELECT cliente.nrocli, cliente.razsoc, cliente.fantasia, cliente.direcc, cliente.cuit, cliente.telef1, cliente.telef2, localidades.nombre, cliente.codven FROM cliente LEFT OUTER JOIN localidades ON localidades.id = cliente.locali where (razsoc like '%" + cadena + "%' or nrocli = " + id + ") and codven = " + codven + " order by nrocli  offset  " + offset + " rows fetch next 20 row only").ToList();
                }
                else if (cadena == "0")
                {
                    if (veTodos)
                        lista = bd.Database.SqlQuery<ClienteBuscador>("SELECT cliente.nrocli, cliente.razsoc, cliente.fantasia, cliente.direcc, cliente.cuit, cliente.telef1, cliente.telef2, localidades.nombre, cliente.codven FROM cliente LEFT OUTER JOIN localidades ON localidades.id = cliente.locali order by nrocli  offset " + offset + " rows fetch next 20 row only").ToList();
                    else
                        lista = bd.Database.SqlQuery<ClienteBuscador>("SELECT cliente.nrocli, cliente.razsoc, cliente.fantasia, cliente.direcc, cliente.cuit, cliente.telef1, cliente.telef2, localidades.nombre, cliente.codven FROM cliente LEFT OUTER JOIN localidades ON localidades.id = cliente.locali where codven = " + codven + " order by nrocli  offset "+offset+" rows fetch next 20 row only").ToList();
                }
            }
                  
            return lista;
        }

        //devuelve true si se encuentra un solo cliente con parametro pasado
        public bool esClienteUnico(string cadena)
        {
            int id = 0;
            if (int.TryParse(cadena, out id))
                id = Convert.ToInt32(cadena);
            using (GestionEntities bd = new GestionEntities())
            {
                if (bd.clienteModal.Where(a => a.razsoc.Contains(cadena) || a.nrocli == id).Count() <= 1)
                    return true;
                else
                    return false;
            }
        }


        //usado mostrar en la cabecera
        public RequestHTTP getClientePresupuesto(int nrocli)
        {
            var req = new RequestHTTP();
              try
              {
                using (GestionEntities bd = new GestionEntities())
                    req.objeto = bd.Database.SqlQuery<ClientePesupuesto>("SELECT dbo.cliente.nrocli, " +
                        " dbo.cliente.razsoc, dbo.cliente.direcc, dbo.cliente.locali AS localicod, " +
                        " dbo.localidades.nombre AS nombreLocali, depto.nombre AS deptoNombre, depto.id," +
                        " depto.provincia_id, dbo.provincias.nombre AS nombreProv, dbo.pais.pais, " +
                        " dbo.cliente.cuit, dbo.cliente.respon AS Iva, dbo.iva.descripcion AS descriIva, " +
                        " dbo.cliente.tipodoc, dbo.cliente.condicion, dbo.cliente.lista, dbo.vende.codven," +
                        " dbo.vende.nombre AS nombreVende, dbo.cliente.respon, dbo.cliente.telef1, " +
                        " dbo.cliente.bonif, dbo.cliente.bonif1, dbo.cliente.bonif2, dbo.cliente.bonif3," +
                        " dbo.cliente.bonif4, dbo.cliente.convenio, dbo.cliente.nopercib, dbo.cliente.expreso," +
                        " dbo.cliente.cond_vta" +
                        " FROM dbo.cliente" +
                        " LEFT OUTER JOIN dbo.localidades ON dbo.localidades.id = dbo.cliente.locali " +
                        " LEFT OUTER JOIN dbo.departamentos AS depto ON depto.id = dbo.localidades.departamento_id" +
                        " LEFT OUTER JOIN dbo.provincias ON dbo.provincias.id = depto.provincia_id " +
                        " LEFT OUTER JOIN dbo.pais ON dbo.provincias.pais_id = dbo.pais.id " +
                        " LEFT OUTER JOIN dbo.iva ON dbo.iva.id = dbo.cliente.respon " +
                        " LEFT OUTER JOIN dbo.vende ON dbo.vende.codven = dbo.cliente.codven where nrocli = " + nrocli).Single();
                return req;
            
            }
            catch (Exception e)
            {
                return req.falla(e);
            }
        }





    }
}
