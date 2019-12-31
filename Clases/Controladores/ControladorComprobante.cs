using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JustServicios.Clases.Controladores
{
    public class ControladorComprobante
    {
        private static ControladorComprobante instancia;
        private static ControladorPresupuesto cPresup = ControladorPresupuesto.getCPresupuesto();
        private static ControladorPedido cPedido = ControladorPedido.getCPedido();
        private ControladorDatos cDatos = new ControladorDatos();

        private ControladorComprobante()
        {
        }

        public ControladorPresupuesto getControladorPresup()
        {
            return cPresup;
        }

        public static ControladorComprobante getCComprobante()
        {
            if (instancia == null)
                instancia = new ControladorComprobante();
            return instancia;
        }
        public List<ComprobanteAdeudado> getComprobantesAdeudados(int nrocli, int empresa)
        {
            using(GestionEntities bd = new GestionEntities())
            {
                return bd.Database.SqlQuery<ComprobanteAdeudado>(
                    "select fecha, tipodoc, letra, punto, numero,simdoc mon,importe1 importe, importe2 adeudado, vence, simbolo, cotizacion " +
                    " from clictad " +
                    "where empresa = " + empresa + " and nrocli = " + nrocli).ToList();
            }
        }
        public RequestHTTP getComprobantesFacturados(string query, int offset, int codven, bool veTodos)
        {
            var res = new RequestHTTP();
            try
            {
                using (GestionEntities bd = new GestionEntities())
                {
                    string queryVende = " and codven = " + codven;
                    if (veTodos)
                        queryVende = "";
                        res.objeto = bd.Database.SqlQuery<ivavenComprobante>(
                            "select fecha, tipodoc, letra, punto, numero,hasta ,nrocli, razsoc, total, cae, vencecae, numerofe,idpetic,ctacon,ctaconex,empresaid, id, provin, " +
                            "remito " +
                            "from ivaven " +
                            "where 1=1  " + query + " " + queryVende + " order by id desc offset " + offset + " rows fetch next 20 row only"
                            ).ToList();
                }
            }catch(Exception ex)
            {
                res = res.falla(ex);
            }
            return res;
        }

        internal List<ComprobanteSaldo> getComposicionSaldo(int nrocli, string desde, string hasta, int empresa)
        {
            using(GestionEntities bd  = new GestionEntities())
            {
                return bd.Database.SqlQuery<ComprobanteSaldo>("exec movcli " + nrocli + ",'" + desde + "', '" + hasta + "', " + empresa).ToList();
            }
        }

        internal bool verificarComprobante(int idClicta, string usuario)
        {
            try
            {
                using (GestionEntities bd = new GestionEntities())
                {
                    bd.Database.ExecuteSqlCommand("update clicta set verifi = 1, verifico = '" + usuario + "'  where id = "+ idClicta);
                    return true;
                }
            }
            catch(Exception e)
            {
                return false;
            }
        }

        internal List<presupCabecera> getPresupuestos(int empresa, int offset)
        {
            return cPresup.getPresupFecha(empresa, "1900/01/01", offset);
        }

        internal List<PedidoDet> getPresupDLikePedido(int idPresup)
        {
            presupc cabPresup; //cabecera presup
            List<presupd> dPresup; //detalle de presup
            List<PedidoDet> dPed = new List<PedidoDet>(); // detalle de pedidod
            cliente clien;
            decimal bonifcliTotal = 0;
            using (GestionEntities bd = new GestionEntities())
            {
                cabPresup = bd.presupc.Single(cab => cab.id == idPresup); //busca la presupC para la moenda de factura
                dPresup = bd.presupd.Where(det => det.cabeceraid == idPresup).ToList(); // busca la presupd
                clien = bd.cliente.Single(cliente => cliente.nrocli == cabPresup.nrocli);
            }
            int i = 0;
            bonifcliTotal = 100 - ((clien.bonif * 100) / 100);
            bonifcliTotal = bonifcliTotal - ((clien.bonif1 * bonifcliTotal) / 100);
            bonifcliTotal = bonifcliTotal - ((clien.bonif2 * bonifcliTotal) / 100);
            bonifcliTotal = bonifcliTotal - ((clien.bonif3 * bonifcliTotal) / 100);
            bonifcliTotal = bonifcliTotal - ((clien.bonif4 * bonifcliTotal) / 100);
            bonifcliTotal = 100 - bonifcliTotal;
            stocks stock;
            var pCab = new PedidoCab();
            pCab.monfac = cabPresup.monfac;
            pCab.id = 1111111111;
            foreach (var pre in dPresup)// por cada articulo de presup agrega uno al pedido
            {
                i++;
                stock = new stocks(pre.cant, pre.precio, pre.bonif, pre.bonif1, pre.impint, pre.codpro,
                                    pre.asociado, bonifcliTotal, pre.fecha, pre.deta, cabPresup.empresa);
                dPed.Add(new PedidoDet(stock, pCab, i));
            }
            return dPed;
        }

        internal List<pedPendientes> getPedPendientes(int codven, bool veTodos, string consulta, int empresa)
        {
            return cPedido.getPedidosPendientesxVende(codven, veTodos, consulta, empresa);
        
        }

        internal PedidoCab getPresupCLikePedido(int idPresup)
        {
            presupc cabInicio;
            PedidoCab cabFin;
            int expreso;
            cliente clien;
            using(GestionEntities bd = new GestionEntities())
            {
                cabInicio = bd.presupc.Single(a => a.id == idPresup);
                clien = bd.cliente.Single(cli => cli.nrocli == cabInicio.nrocli);   
            }
            decimal bonifcliTotal = 0;
            bonifcliTotal = 100 - ((clien.bonif * 100) / 100);
            bonifcliTotal = bonifcliTotal - ((clien.bonif1 * bonifcliTotal) / 100);
            bonifcliTotal = bonifcliTotal - ((clien.bonif2 * bonifcliTotal) / 100);
            bonifcliTotal = bonifcliTotal - ((clien.bonif3 * bonifcliTotal) / 100);
            bonifcliTotal = bonifcliTotal - ((clien.bonif4 * bonifcliTotal) / 100);
            bonifcliTotal = 100 - bonifcliTotal;



            cabFin = new PedidoCab(cabInicio.empresa, cabInicio.tipodocid, cabInicio.punto, cabInicio.tipodoc, cabInicio.letra, cabInicio.fecha, cabInicio.nrocli,
                cabInicio.razsoc, Convert.ToInt32(cabInicio.codven), cabInicio.condicion, "WEB", 6, 1, "a", "a", 1, cabInicio.neto, cabInicio.exento, cabInicio.bonifto, cabInicio.ivai,
                cabInicio.ivaidif, cabInicio.internos, cabInicio.percib, cabInicio.total, Convert.ToInt32(clien.expreso), 0, 0, 0, cabInicio.direcc, cabInicio.locali, cabInicio.codpos, Convert.ToInt32(cabInicio.provin),
                cabInicio.localidadid, cabInicio.observa, cabInicio.fecha, cabInicio.cond_vta, "WEB", cabInicio.numero, cabInicio.monfac, cabInicio.id, bonifcliTotal, "aaa");
            return cabFin;

        }


        public DicRequestHTTP getItemsFactura(string tipo, string letra, int punto, int numero, int empresa)
        {
            return cDatos.getData("SELECT * FROM detmovim WHERE empresa = " + empresa + " and tipodoc = '" + tipo + "' and letra = '" + letra + "' and punto = " + punto + " and numero = " + numero);
        }
    }
}