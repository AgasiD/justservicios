using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JustServicios
{
    public class ControladorTotales
    {
        private static ControladorTotales instancia;

        private ControladorTotales()
        {

        }

        public static ControladorTotales getCTotales()
        {
            if (instancia == null)
                instancia = new ControladorTotales();
            return instancia;
        }

        private int empresaid = 1;



        public Totales getTotales(JObject lista)
        {
            bool cuitValido = Convert.ToBoolean(lista["valido"]),
                convenio = Convert.ToBoolean(lista["convenio"]),
                enDomicilio = Convert.ToBoolean(lista["enDomicilio"].ToString()),
                esExento = Convert.ToBoolean(lista["esExento"]);
            int empresaid = Convert.ToInt32(lista["empresaid"]);
            string provincia = lista["provincia"].ToString(),
                cuit = lista["cuit"].ToString(), codpro,
                monfac = lista["monfac"].ToString();

            prueba p = new prueba();
            var lis = lista["lista"].ToArray();
            List<stocks> listaItems;
            stocks item, aux;
            Totales totales = new Totales();
            decimal bonicli = Convert.ToDecimal(lista["bonifcli"].ToString());



            if (lis.Count() > 0)
            {
                for (int i = 0; i < lis.Count(); i++)
                {
                    if (lis[i]["asociado"].ToString().Contains("["))
                        lis[i]["asociado"] = "";
                    item = JsonConvert.DeserializeObject<stocks>(lis[i].ToString());
                    if (item.codpro == "")
                        codpro = lis[i]["articulo"].ToString();
                    else
                        codpro = item.codpro;

                    aux = new stocks(item.cantidad, item.precioVenta, item.bonif, item.bonif1, item.impint, codpro, item.asociado, bonicli, item.parafecha, item.detalle, empresaid);
                    p.lista.Add(aux);
                }
                listaItems = p.lista;
                totales.subtotal = this.getSubTotal(listaItems);
                totales.bonif = this.getBonifTotal(listaItems, bonicli);
                totales.neto = this.getNeto(listaItems, bonicli);
                totales.exento = this.getExento(listaItems);
                totales.impint = this.getImpInt(listaItems);
                totales.ivagral = this.getIvaGral(listaItems, bonicli);
                totales.ivadif = this.getIvaIDif(listaItems, bonicli);
                totales.percep = this.getPercep(listaItems, bonicli, empresaid, cuit, provincia, totales.neto, convenio, esExento, enDomicilio, cuitValido, monfac);
                totales.total = this.getTotal(listaItems);
                totales.total = totales.total + totales.percep;
                return totales;// totales;
            }
            else
            {
                totales.subtotal = 0;
                totales.bonif = 0;
                totales.neto = 0;
                totales.exento = 0;
                totales.impint = 0;
                totales.ivagral = 0;
                totales.ivadif = 0;
                totales.percep = 0;
                totales.total = 0;
                return totales;
            }
        }




        public decimal getSubTotal(List<stocks> itemsDePedido)
        {
            decimal total = 0;
            foreach (stocks item in itemsDePedido)
                total += item.getSubtotal();

            return total;
        }
        public decimal getExento(List<stocks> itemsDePedido)
        {
            decimal total = 0;
            foreach (stocks item in itemsDePedido)
            {
                total += item.getExento();
            }
            return total;
        }
        public decimal getNeto(List<stocks> itemsDePedido, decimal bonifCliente)
        {

            decimal total = 0;
            foreach (stocks item in itemsDePedido)
            {
                total += item.getNeto(bonifCliente);
            }
            return total;
        }

        public decimal getBonifTotal(List<stocks> itemsDePedido, decimal bonicli)
        {
            decimal total = 0;
            foreach (stocks item in itemsDePedido)
            {
                total += item.getSubtotal() * bonicli / 100 ;
            }
            return total;
        }

        public decimal getIvaGral(List<stocks> itemsDePedido, decimal bonifCliente)
        {
            decimal total = 0;
            foreach (stocks item in itemsDePedido)
            {
                total += item.getIvaIGeneral(getIvaTasaGral());
            }
            return total;
        }

        public decimal getIvaIDif(List<stocks> itemsDePedido, decimal bonifCliente)
        {
            decimal total = 0;
            foreach (stocks item in itemsDePedido)
            {
                total += item.getIvaIDif(getIvaTasaGral());
            }
            return total;
        }

        public decimal getImpInt(List<stocks> itemsDePedido)
        {
            decimal total = 0;
            foreach (stocks item in itemsDePedido)
            {
                total += item.impint;
            }
            return total;
        }
        public decimal getTotal(List<stocks> itemsDePedido)
        {
            decimal total = 0;
            decimal tot = 0;
            foreach (stocks item in itemsDePedido)
            {
                total += item.getTotal(getIvaTasaGral());
                tot++;
            }
            return total;
        }

        public decimal getIvaTasaGral()
        {
            decimal tasa;

            using (GestionEntities bd = new GestionEntities())
            {
                tasa = bd.configen.Single(a => a.empresa == empresaid).ivatasagral;
            }
            return tasa;
        }


        /*percept impint ivagral iva adic total
        neto = suma de precios sin iva 
        subtotal neto + exento
        total
        */

        public decimal getPercep(List<stocks> listaItems, decimal bonicli, int empresa, string cuit, string provincia, decimal neto, bool convenio, bool esExento, bool enDomicilio, bool valido, string monfac)
        {
            decimal cotizMoneda;
            char[] valores = { '-', '/' };
            cuit = cuit.Trim(valores);
            decimal netoaux = 0;
            foreach (var item in listaItems)
                netoaux += item.getNeto(bonicli);

            decimal percibido = 0;
            Percepciones percep = null;
            using (GestionEntities bd = new GestionEntities())
            {
                cotizMoneda = bd.monedas.Single(a => a.codigo == monfac).ncotiza;
                netoaux = netoaux * cotizMoneda;
                if (bd.PadronIIBBxCuityProv(cuit).Count() != 0)//si esta en padron
                {
                    if (!esExento)
                    {
                        percep = new Percepciones();
                        percep.monto = bd.PadronIIBBxCuityProv(cuit).Single().Percep;
                        percep.montoMinimo = Convert.ToDecimal(bd.PadronIIBBxCuityProv(cuit).Single().MontoPib);
                        percibido = cuentaPercep(netoaux, percep.monto, percep.montoMinimo);
                    }
                }
                else //si no esta en padron
                {
                    if (!esExento)
                    {
                        if (!enDomicilio) //checkEntrega En domicilio
                        {
                            percibido = calcularPercepLocal(netoaux, empresa);
                        }
                        else
                        {
                            percibido = percepProvincia(provincia, cuit, convenio, netoaux); //busca percepciones de la provincia
                        }
                    }
                }
            }
            percibido = percibido / cotizMoneda;
            return percibido;
        }









            /*



                if (!esExento) //!checkExento.Checked
            {
                if (!enDomicilio) //checkEntrega.Checked
                {
                    percibido = calcularPercepLocal(netoaux, empresa);
                }
                else
                {
                    Percepciones percep = null;
                  

                        if (bd.PadronIIBBxCuityProv(cuit).Count() > 0)//si esta en padron
                        {
                            percep = new Percepciones();
                            percep.monto = bd.PadronIIBBxCuityProv(cuit).Single().Percep;
                            percep.montoMinimo = Convert.ToDecimal(bd.PadronIIBBxCuityProv(cuit).Single().MontoPib);
                            percibido = cuentaPercep(netoaux, percep.monto, percep.montoMinimo);

                        }
                        else // si no esta en padron
                            percibido = percepProvincia(provincia, cuit, convenio, netoaux); //busca percepciones de la provincia
                    }
                }
                return percibido;
            }
            return 0;*/
        

        private decimal calcularPercepLocal(decimal neto, decimal empresa = 1)
        {
            int idEmpresa = Convert.ToInt32(empresa);
            decimal percep, montomin;
            decimal cotizMoneda;
            using (GestionEntities bd = new GestionEntities())
            {
                int provin = bd.empresa.Single(a => a.id == idEmpresa).juridi;
                percep = bd.provincias.Single(a => a.juridi == provin).Percepib;
                montomin = bd.provincias.Single(a => a.juridi == provin).MontoPib;
                
            }
            if (neto >= montomin)
            {
                neto = neto * percep / 100;
                return neto;
            }
            else
                return 0;
        }

        private decimal cuentaPercep(decimal neto, decimal monto, decimal montoMinimo)
        {
            
            if (neto >= montoMinimo)
                return neto * monto / 100;
            return 0;
        }

        private decimal percepProvincia(string provincia, string cuit, bool convenio, decimal neto)
        {
            provincias prov;
            Percepciones percep = new Percepciones();
            decimal percibido = 0;
            if (convenio)
                percibido = setPercepProvincias(neto);
            else
            {
                using (GestionEntities bd = new GestionEntities())
                {
                    prov = bd.provincias.Single(a => a.nombre == provincia);
                    percep.monto = prov.Percepib;
                    percep.montoMinimo = prov.MontoPib;
                }
                percibido = cuentaPercep(neto, percep.monto, percep.montoMinimo);
            }
            return percibido;
        }

        private decimal setPercepProvincias(decimal net)
        //obtiene el porcentaje de las provincias que perciban y el neto sea >= al minimo de la provincia
        {
            List<provincias> listaProvincias;
            decimal neto = net;
            Percepciones percep = new Percepciones();
            decimal percibido = 0;
            using (GestionEntities bd = new GestionEntities())
                listaProvincias = bd.provincias.Where(a => a.Percib == true).ToList();

            foreach (provincias p in listaProvincias)
            {
                if (neto >= p.MontoPib)
                    percep.monto = percep.monto + p.Percepib;
            }
            percibido = cuentaPercep(neto, percep.monto, percep.montoMinimo);
            return percibido;
        }

        public class Percepciones
        {
            public decimal monto { get; set; }
            public decimal montoMinimo { get; set; }

            public Percepciones()
            {
                montoMinimo = -1;
            }
        }
    }

    public class Totales
    {
        public decimal total { get; set; }
        public decimal subtotal { get; set; }
        public decimal bonif { get; set; }
        public decimal neto { get; set; }
        public decimal exento { get; set; }
        public decimal percep { get; set; }
        public decimal impint { get; set; }
        public decimal ivagral { get; set; }
        public decimal ivadif { get; set; }
    }

}

/*
using (GestionEntities bd = new GestionEntities())
{
    bd.PedidoCab.Add(cabecera);
    factura fac = bd.factura.Single(a => a.PUNTO == punto && a.EmpresaId == empresa);
    bd.factura.Attach(fac);
    fac.PEDIDO += 1;
    bd.SaveChanges();
}




}catch (DbEntityValidationException dbEx)
{
 StreamWriter sw = new StreamWriter("C:/Users/Usuario/Desktop/errores.txt");

 foreach (var validationErrors in dbEx.EntityValidationErrors)
 {
     foreach (var validationError in validationErrors.ValidationErrors)
     {
         sw.WriteLine("Property:" + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
     }
 }
    sw.Close();

return false;
}
}/*/
