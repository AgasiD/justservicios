using JustServicios.Clases;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Web;

namespace JustServicios
{
    public class ControladorPresupuesto
    {
        private static ControladorPresupuesto instancia;
        private static ControladorProducto cProducto = ControladorProducto.getCProducto();
        private ControladorPresupuesto()
        {

        }

        public static ControladorPresupuesto getCPresupuesto()
        {
            if (instancia == null)
                instancia = new ControladorPresupuesto();
            return instancia;
        }

        public bool actualizarNroPed(int idPresup, int idPedido)
        {

            return true;
        }


        public RequestHTTP finalizarPresupuesto(JObject cabydet)
        {
            var req = new RequestHTTP();
            try
            {
                var jCabecera = (JObject)cabydet["cabecera"];
                int punto = (int)jCabecera["punto"], empresaid = (int)jCabecera["empresa"];
                var jDetalle = cabydet["lista"];
                decimal bonifcli = (decimal)cabydet["bonifcli"];

                presupc cabecera = armarCabecera(jCabecera, bonifcli);
                List<presupd> detalle = armarDetalle(jDetalle, cabecera, bonifcli);
                using (GestionEntities bd = new GestionEntities())
                {

                    bd.presupc.Add(cabecera);
                    foreach (presupd p in detalle)
                        bd.presupd.Add(p);
                    var factu = bd.factura.Single(a => a.PUNTO == punto && a.EmpresaId == empresaid);
                    bd.factura.Attach(factu);
                    factu.PRESUP++;
                    if (!Convert.ToBoolean(bd.SaveChanges()))
                    {
                        throw new Exception();
                    }
                    req.objeto = new compi(cabecera.numero, cabecera.punto, cabecera.tipodoc, cabecera.letra, cabecera.id);
                    return req;
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                StreamWriter sw = new StreamWriter("C:/Users/usuario/Desktop/error.txt");

                //Close the file

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        sw.WriteLine("Property:" + validationError.PropertyName + " Error:" + validationError.ErrorMessage);
                    }
                }

                sw.Close();
            return req.falla(new Exception());
            }
        }
            
      //  }
             //   
           // }
        


        public List<presupd> getItemPresup(int id)
        {
            using (GestionEntities bd = new GestionEntities())
            {
                return bd.presupd.Where(a => a.cabeceraid == id).ToList();
            }
        }

        public List<presupc> getPresupNrocli(int nrocli)
        {
            try
            {
                using (GestionEntities bd = new GestionEntities())
                {
                    return bd.presupc.Where(a => a.nrocli == nrocli ).ToList();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<presupc> getPresupRazon(string razon)
        {
            try
            {
                using (GestionEntities bd = new GestionEntities())
                {
                    return bd.presupc.Where(a => a.razsoc.Contains(razon)).ToList();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<presupCabecera> getPresupFecha(int empresa, string fecha, int offset)
        {
            try
            {
                //le cambia el formato de fecha (inglesa) a desde(francesa)
                DateTime desde;
                int dia, mes, anio;
                anio = Convert.ToInt32(fecha.Substring(0, 4));
                mes = Convert.ToInt32(fecha.Substring(5, 2));
                dia = Convert.ToInt32(fecha.Substring(8, 2));
                desde = new DateTime(anio, mes, dia).Date;
                using (GestionEntities bd = new GestionEntities())
                {
                    return bd.Database.SqlQuery<presupCabecera>("select id, fecha, tipodoc, letra, punto, numero, nrocli, hasta, razsoc from presupc where fecha >= '" + desde + "' and empresa = " + empresa + " order by fecha desc offset " + offset + " rows fetch next 20 row only").ToList();
//                    return bd.presupc.Where(a => a.fecha >= desde && a.empresa == empresa).ToList();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public RequestHTTP getPresup(string filtro, int offset)
        {
            var req = new RequestHTTP();
            try
            {
                using (GestionEntities bd = new GestionEntities())
                {
                    req.objeto = bd.Database.SqlQuery<presupc>("select * from presupc where " + filtro + "order by fecha desc offset " + offset + " rows fetch next 20 row only").ToList();
                    return req;
                }
            }
            catch (Exception e)
            {
                return req.falla(e) ;
            }
        }

        //finalizar presupuesto
        private List<presupd> armarDetalle(JToken lista, presupc cabecera, decimal bonifcli)
        {
            stocks p, item;
            List<presupd> detalles = new List<presupd>();
            int empresaid = cabecera.empresa;
            for (int i = 0; i < lista.ToArray().Count(); i++)
            {
                if (lista[i]["asociado"].ToString().Contains("["))
                    lista[i]["asociado"] = "";
                p = JsonConvert.DeserializeObject<stocks>(lista[i].ToString());
                item = new stocks(p.cantidad, p.precioVenta, p.bonif, p.bonif1, p.impint, p.codpro, p.asociado, bonifcli, new DateTime(2019,01,01), p.detalle, empresaid);
                detalles.Add(new presupd(item, cabecera, i + 1));
            }
            return detalles;
        }

        private presupc armarCabecera(JObject JCabecera, decimal bonifcli)
        {

            int hasta = 1;// (int)JCabecera["hasta"];
            string descricondic = "a", codpos, provincia = JCabecera["provin"].ToString();// = (int)JCabecera["condicion"]
            int provin, condicion = (int)JCabecera["condicion"], codven = (int)JCabecera["codven"], idIva = (int)JCabecera["respon"];
            decimal comis = (decimal)JCabecera["comis"], comicli = (decimal)JCabecera["comicli"], diasVence, cotizaMon, numero, recargo = (decimal)JCabecera["recargo"];// JCabecera["provin"].toString();
            int tipodocid = (int)JCabecera["tipodocid"];
            string tipodocstr, nomven, monfac = JCabecera["monfac"].ToString(), simbolo, usuario = JCabecera["usuario"].ToString();
            short punto = (short)JCabecera["punto"];
            int usuarioid = (int)JCabecera["usuarioid"], localiid, nrocli = (int)JCabecera["nrocli"],empresaid = (int)JCabecera["empresa"], respon,id, vence = (int)JCabecera["vence"];
            
            using (GestionEntities bd = new GestionEntities())
            {
                tipodocid = (int)JCabecera["tipodocid"];
                tipodocstr = bd.tipocomprobante.Single(a => a.codigo == tipodocid).abreviado;
                numero = bd.factura.Single(a => a.PUNTO == punto && a.EmpresaId == empresaid ).PRESUP;
                codpos = bd.cliente.Single(a => a.nrocli == nrocli).codpos;
                provin = bd.provincias.Single(a => a.nombre == provincia).id;
                descricondic = bd.condpago.Single(a => a.codigo == condicion).descripcion;
                diasVence = bd.condpago.Single(a => a.codigo == condicion).dias;
                nomven = bd.vende.Single(a => a.codven == codven).nombre;
                respon = bd.iva.Single(a => a.id== idIva).id;

                if (bd.presupc.Count() > 0)
                    id = bd.presupc.ToArray().Last().id + 1;
                else
                    id = 1;
                simbolo = bd.monedas.Single(a => a.codigo == monfac).simbolo;
                cotizaMon = bd.monedas.Single(a => a.codigo == monfac).ncotiza;
                localiid = bd.cliente.Single(a => a.nrocli == nrocli).locali;

            }
            
            //(DateTime)JCabecera["fechai"];
            //buscar: tipodoc con (int)JCabecera["tipodocid"], numero, nomven (decimal)JCabecera["codven"]
           return new presupc((int)JCabecera["empresa"],DateTime.Now, (int)JCabecera["tipodocid"], tipodocstr, "X", (short)JCabecera["punto"], Convert.ToInt32(numero) + 1,
                hasta, (int)JCabecera["nrocli"], JCabecera["razsoc"].ToString(), JCabecera["direcc"].ToString(), JCabecera["locali"].ToString(),
                codpos, provin, JCabecera["cuit"].ToString(), (decimal)JCabecera["subtot"], bonifcli,
                (decimal)JCabecera["bonitot"], (decimal)JCabecera["exento"], (decimal)JCabecera["neto"], (decimal)JCabecera["ivai"], 0, (decimal)JCabecera["ivaidif"],
                0, (decimal)JCabecera["total"], 0, condicion, descricondic, codven , nomven, respon, 0 , comis //tipcom y comis
                ,0,0,0, JCabecera["observa"].ToString(), 0, comicli,false, diasVence, 0, usuario, (decimal)JCabecera["impint"], 0, (int)JCabecera["lista"], descricondic,
                usuario, monfac, (decimal)JCabecera["percib"],simbolo, id, recargo, cotizaMon, JCabecera["atencion"].ToString(), JCabecera["referencia"].ToString(), JCabecera["validez"].ToString(),
                JCabecera["plentrega"].ToString(), JCabecera["lugentre"].ToString(), Convert.ToDecimal(JCabecera["costoest"].ToString())
                ,localiid, usuarioid);
        }
        
        ////tipcom, ,retiva, reggan, retinb, comiprod, , nrorepa,




    }
}