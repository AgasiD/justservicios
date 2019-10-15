using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Web;
using JustServicios.Clases;
using JustServicios.Clases.Controladores;

namespace JustServicios
{
    public class ControladorPedido
    {
        private static ControladorPedido instancia;
        private static ControladorComprobante cComprobante = ControladorComprobante.getCComprobante();
        private ControladorPedido()
        {

        }

        public static ControladorPedido getCPedido()
        {
            if (instancia == null)
                instancia = new ControladorPedido();
            return instancia;
        }


        public RequestHTTP getPedido(string filtro, int offset)
        {
            var req = new RequestHTTP();
            try
            {
                using (GestionEntities bd = new GestionEntities())
                {

                    req.objeto = bd.Database.SqlQuery<PedidoCab>("select * from PedidoCab where " + filtro + " order by fechaing desc offset " + offset + " rows fetch next 20 row only").ToList();
                    return req;
                }
            }
            catch (Exception e)
            {
                return req.falla(e);
            }
        }

        public List<PedidoCab> getPedidoFecha(string date)
        {
            int dia, mes, anio;
            anio = Convert.ToInt32(date.Substring(0, 4));
            mes = Convert.ToInt32(date.Substring(5, 2));
            dia = Convert.ToInt32(date.Substring(8, 2));
            DateTime desde = new DateTime(anio, mes, dia).Date;
            try
            {
                using (GestionEntities bd = new GestionEntities())
                {
                    return bd.PedidoCab.Where(a => a.fechaing >= desde ).ToList();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }




        public List<PedidoDet> getItemPedido(int id)
        {
            try
            {
                using (GestionEntities bd = new GestionEntities())
                {
                    return bd.PedidoDet.Where(a => a.cabeceraid == id).ToList();
                }
            }
            catch (Exception)
            {
                return null;
            }

        }


        public List<PedidoCab> getPedidos()
        {
            try
            {
                using (GestionEntities bd = new GestionEntities())
                {
                    return bd.PedidoCab.Where(d => d.id > 126000).ToList();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        internal List<PedPendientes> getPedidosPendientes(int nrocli, string codpro, string token)
        {
            
            using(GestionEntities bd = new GestionEntities())
            {
                if (bd.token.Single().token1 == token)
                    return bd.Database.SqlQuery<PedPendientes>("select pedidocab.id, punto, nroped numero, fechaing fecha, articulo, descri, pendientes cantidad, ocompra from pedidocab left join pedidodet on pedidodet.cabeceraid = pedidocab.id where nrocli = " + nrocli + " and articulo like '" + codpro + "' and pendientes > 0").ToList();
                else
                    throw new Exception();
            }
        }

        internal List<pedPendientes> getPedidosPendientesxVende(int codven, bool veTodos, string consulta, int empresa)
        {
            if (consulta == "nula")
            {
                if (!veTodos)
                {
                    using (GestionEntities bd = new GestionEntities())
                    {
                        return bd.Database.SqlQuery<pedPendientes>("select max(pedidocab.id) id, max(PedidoCab.fechaing) fecha, max(pedidocab.punto) punto, max(pedidocab.nroped) numero, max(pedidocab.nrocli) nrocli, max(pedidocab.razsoc) razsoc, max(pedidocab.direcc) direcc, max(pedidocab.parafecha) parafecha from pedidocab left join pedidodet on pedidodet.cabeceraid = pedidocab.id where pedidocab.codven = " + codven + " and empresaid = "+ empresa + " group by pedidocab.id having sum(pendientes) > 0 order by fecha desc").ToList();
                    }
                }
                else
                {
                    using (GestionEntities bd = new GestionEntities())
                    {
                        return bd.Database.SqlQuery<pedPendientes>("select max(pedidocab.id) id,max(PedidoCab.fechaing) fecha, max(pedidocab.punto) punto, max(pedidocab.nroped) numero, max(pedidocab.nrocli) nrocli, max(pedidocab.razsoc) razsoc, max(pedidocab.direcc) direcc, max(pedidocab.parafecha) parafecha from pedidocab left join pedidodet on pedidodet.cabeceraid = pedidocab.id where empresaid = "+ empresa + " group by pedidocab.id having sum(pendientes) > 0 order by fecha desc").ToList();
                    }
                }
            }
            else
            {
                if (!veTodos)
                {
                    using (GestionEntities bd = new GestionEntities())
                    {
                        return bd.Database.SqlQuery<pedPendientes>("select max(pedidocab.id) id, max(PedidoCab.fechaing) fecha, max(pedidocab.punto) punto, max(pedidocab.nroped) numero, max(pedidocab.nrocli) nrocli, max(pedidocab.razsoc) razsoc, max(pedidocab.direcc) direcc, max(pedidocab.parafecha) parafecha from pedidocab left join pedidodet on pedidodet.cabeceraid = pedidocab.id where pedidocab.codven = " + codven + " and "+ consulta + " and empresaid = " + empresa + " group by pedidocab.id having sum(pendientes) > 0 order by fecha desc").ToList();
                    }
                }
                else
                {
                    using (GestionEntities bd = new GestionEntities())
                    {
                        return bd.Database.SqlQuery<pedPendientes>("select max(pedidocab.id) id,max(PedidoCab.fechaing) fecha, max(pedidocab.punto) punto, max(pedidocab.nroped) numero, max(pedidocab.nrocli) nrocli, max(pedidocab.razsoc) razsoc, max(pedidocab.direcc) direcc, max(pedidocab.parafecha) parafecha from pedidocab left join pedidodet on pedidodet.cabeceraid = pedidocab.id where " + consulta + " and empresaid = " + empresa + " group by pedidocab.id having sum(pendientes) > 0 order by fecha desc").ToList();
                    }
                }
            }
        }

        internal List<UltimoPrecio> getUltimosPrecios(int nrocli, string codpro, string token)
        {
           using(GestionEntities bd = new GestionEntities())
            {
                return bd.Database.SqlQuery<UltimoPrecio>("select c.fecha, c.tipodoc, c.letra, c.punto, c.numero, c.neto, detmovim.bonif, detmovim.bonif1, detmovim.bonito from ivaven c left join detmovim on detmovim.ivavenid = c.id where c.nrocli = " + nrocli + " and codpro like '" + codpro + "' order by fecha desc").ToList();
            }
        }

        public RequestHTTP finalizarPedido(JObject jCabYDet)
        {
            var req = new RequestHTTP();
            try
            {
                var jCabecera = (JObject)jCabYDet["cabecera"];
                int punto = (int)jCabecera["punto"],
                empresa = (int)jCabecera["empresa"];
                decimal bonifcli = (decimal)jCabYDet["bonifcli"];
                PedidoCab cabecera = armaCabecera(jCabecera, bonifcli);
                List<PedidoDet> detalles = armaDetalle(cabecera, jCabYDet["lista"], bonifcli);
                using (GestionEntities bd = new GestionEntities())
                {
                    bd.PedidoCab.Add(cabecera);
                    foreach (var p in detalles)
                        bd.PedidoDet.Add(p);
                    factura fac = bd.factura.Single(a => a.PUNTO == punto && a.EmpresaId == empresa);
                    bd.factura.Attach(fac);
                    fac.PEDIDO++;
                    bd.SaveChanges();
                    req.objeto = new compi(cabecera.nroped, cabecera.punto, cabecera.tipodoc, cabecera.letra, cabecera.id);

                    return req;
                }
            }
            catch (Exception e)
            {
                return req.falla(e);
            }
        }



        private List<PedidoDet> armaDetalle(PedidoCab cabecera, JToken lista, decimal bonifcli)
        {
            stocks p, item;
            DateTime parafecha = new DateTime();
            List<PedidoDet> detalles = new List<PedidoDet>();
            int empresa = cabecera.empresaid;
            for (int i = 0; i < lista.ToArray().Count(); i++)
            {
                if (lista[i]["asociado"].ToString().Contains("["))
                    lista[i]["asociado"] = "";
                p = JsonConvert.DeserializeObject<stocks>(lista[i].ToString());
                parafecha = Convert.ToDateTime(lista[i]["parafecha"].ToString());
                string itemDetalle = lista[i]["detalle"].ToString();
                DateTime pfe = Convert.ToDateTime(parafecha.ToString("dd/MM/yyyy"));
                item = new stocks(p.cantidad, p.precioVenta, p.bonif, p.bonif1, p.impint, p.codpro, p.asociado, bonifcli, pfe, itemDetalle, empresa);
                detalles.Add(new PedidoDet(item, cabecera, i + 1));

            }
            return detalles;
        }

        private PedidoCab armaCabecera(JObject jCabecera, decimal bonifcli)
        {
            decimal nroped;
            double vence;
            int provin, jPunto, jEmpresa, jSucursal, jViaid, jLocali, jNrocli, jTipoDocId, jCondPago, id,  jUsuarioId;
            string sucursal, via, jProvin, localidad, codpos, monfac, usuario, jNotaVenta, presup = "";
            jPunto = (int)jCabecera["punto"];
            jEmpresa = (int)jCabecera["empresa"];
            jSucursal = (int)jCabecera["sucursal"];
            jViaid = (int)jCabecera["viaid"];
            jProvin = (string)jCabecera["provin"];
            usuario = jCabecera["usuario"].ToString();
            jNrocli = (int)jCabecera["nrocli"];
            jTipoDocId = 1007;//(int)jCabecera["tipodocid"];
            jCondPago = (int)jCabecera["condicion"];
            jUsuarioId = (int)jCabecera["usuarioid"];
            jNotaVenta = jCabecera["notavta"].ToString();
            monfac = jCabecera["monfac"].ToString();
            
            using (GestionEntities bd = new GestionEntities())
            {//recibe parametros y busca el nombre o id 
                jLocali = bd.cliente.Single(a => a.nrocli == jNrocli).locali;
                nroped = bd.factura.Single(a => a.PUNTO == jPunto && a.EmpresaId == jEmpresa).PEDIDO;
                provin = bd.provincias.Single(a => a.nombre == jProvin).id;
                sucursal = bd.sucursal.Single(a => a.id == jSucursal).sucursal1;
                via = bd.via.Single(a => a.id == jViaid).via1;
                localidad = bd.localidades.Single(a => a.id == jLocali).nombre;
                codpos = bd.cliente.Single(a => a.nrocli == jNrocli).codpos;
                vence = Convert.ToDouble(bd.condpago.Single(a => a.codigo == jCondPago).dias);
                if (bd.PedidoCab.Count() > 0)
                    id = bd.PedidoCab.ToList().Last().id + 1;
                else
                    id = 1;
            }
            DateTime parafecha = Convert.ToDateTime(jCabecera["parafecha"].ToString()); //transformacion de DateTime 2 a Datetime porque no reconoce
            DateTime pfe = Convert.ToDateTime(parafecha.ToString("dd/MM/yyyy"));
            var cabeceraPedido = new PedidoCab((int)jCabecera["empresa"], jTipoDocId, (int)jCabecera["punto"], "PD",
                                   "X", Convert.ToDateTime(jCabecera["fechai"]), (int)jCabecera["nrocli"], (string)jCabecera["razsoc"],
                                   (int)jCabecera["codven"], (int)jCabecera["condicion"], via, (int)jCabecera["viaid"], (int)jCabecera["sucursal"],
                                   sucursal, usuario, jUsuarioId, (decimal)jCabecera["neto"],
                                   (decimal)jCabecera["exento"], (decimal)jCabecera["bonitot"], (decimal)jCabecera["ivai"], (decimal)jCabecera["ivaidif"],
                                   (decimal)jCabecera["impint"], (decimal)jCabecera["percib"], (decimal)jCabecera["total"], (int)jCabecera["transpo"],
                                   vence, 0, 0, (string)jCabecera["direcc"], localidad, codpos, provin,
                                   jLocali, jCabecera["observa"].ToString(), pfe, (int)jCabecera["cond_vta"], "PAG.WEB", nroped, monfac, id, bonifcli, presup);
            if (presup != "") //boniftot bultos codpos facturado id kilos letra nroped nrotran puestotra tipodoc ntavta
            {
                //cComprobante.getControladorPresup().actualizarNroPed(,cabeceraPedido.id);
            }
            return cabeceraPedido;

        }

    }

   
}
