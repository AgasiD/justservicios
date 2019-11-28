using JustServicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JustServicios
{
    public partial class stocks
    {
        public decimal cantidad { get; set; }
        public decimal precioVenta { get; set; }
        public decimal subTotal { get; set; }
        public decimal exentoVenta { get; set; }
        public decimal bonif { get; set; }
        public decimal bonif1 { get; set; }
        public decimal iva { get; set; }
        public decimal neto { get; set; }
        public string asociado { get; set; }
        public DateTime parafecha { get; set; }
        public string detalle { get; set; }

        public stocks(decimal cantidad, decimal precioVenta, decimal bonif, decimal bonif1, decimal impInt, string codpro,
            string artasoc, decimal bonifcli, DateTime parafecha, string detalle, int empresa)
        {
          
            stocksF_Result p;
            this.parafecha = parafecha; 
            this.asociado = artasoc;
            this.codpro = codpro;
            this.cantidad = cantidad;
            this.precioVenta = precioVenta;
            this.bonif = bonif;
            this.bonif1 = bonif1;
            this.impint = impInt * cantidad;
            this.detalle = detalle;
           
            this.subTotal = (cantidad * precioVenta) - (cantidad * precioVenta * bonif / 100) - (cantidad * precioVenta * bonif1 / 100);
            using (GestionEntities bd = new GestionEntities())
            {
                p = bd.stocksF(empresa).Single(a => a.codpro == this.codpro);
                iva = bd.ivaart.Single(l => l.codigo == p.ivagrupo).porcen1;
            }
            this.moneda = p.moneda;
            this.iva = iva;
            this.descri = p.descri;
            this.marca = p.marca;
            this.rubro = p.rubro;
            this.envase = p.envase;
            this.ivagrupo = p.ivagrupo;
            this.uxbulto = p.uxbulto;
            this.cantenv = p.cantenv;
            this.unimed = p.unimed;
            this.simboloEnvase = p.simboloEnvase;
            this.costo = p.costo;
            this.precio1 = p.precio1;
            this.precio2 = p.precio2;
            this.precio3 = p.precio3;
            this.precio4 = p.precio4;
            this.precio5 = p.precio5;
            this.precio6 = p.precio6;
            calcularTotales(bonifcli);
        }

        private void calcularTotales(decimal bonifcli)
        {
           this.subTotal = getSubtotal();
            this.exentoVenta = getExento();
            this.neto = getNeto(bonifcli);
        }

        public stocks()
        {

        }

        public decimal getNeto(decimal bonifCliente)
        {
            neto = subTotal - (subTotal * bonifCliente / 100);
            return neto;
        }
        public decimal getSubtotal()
        {
            return subTotal = (cantidad * precioVenta) - (cantidad * precioVenta * bonif / 100) - (cantidad * precioVenta * bonif1 / 100);
        }

        public decimal getBonifTotal()
        {
            decimal boni1 = this.subTotal * bonif / 100;
            decimal boni2 = (this.subTotal - boni1) * bonif1 / 100;
            return boni1 + boni2;
        }

        public decimal getExento()
        {
            decimal exento = 0;
            if (this.iva == 0)
                exento = precioVenta;
            return exento;// Convert.ToDecimal(this.iva);
        }


        public decimal getTotal(decimal ivaGenera)
        {
            return this.neto + this.exentoVenta + Convert.ToDecimal(impint) + getIvaIDif(ivaGenera) + getIvaIGeneral(ivaGenera);
        }

        public decimal getIvaIGeneral(decimal ivaGeneral)
        {//Iva Tasa General se toma de configen
            if (this.iva == ivaGeneral)
            {
                decimal porcen;
                decimal neto;
                porcen = Convert.ToDecimal(this.iva / 100);
                neto = this.neto;
                return neto * porcen;
            }
            return 0;
        }
        public decimal getIvaIDif(decimal ivaGeneral)
        {//Iva Tasa General se toma de configen
            if (this.iva != ivaGeneral)
            {
                decimal porcen;
                decimal neto;
                porcen = Convert.ToDecimal(this.iva / 100);
                neto = this.neto;

                return neto * porcen;
            }
            return 0;
        }
        public decimal calcularExento()
        {
            decimal exento = Convert.ToDecimal(0.00);
            if (this.porcen1 == 0)
                exento += this.subTotal;
            return exento;
        }

        public decimal getIvaDif(decimal ivaGeneral)
        {//Iva Tasa General se toma de configen
            if (this.iva != ivaGeneral)
            {
                return iva;
            }
            return 0;
        }

        public decimal getIvaGral(decimal ivaGeneral)
        {//Iva Tasa General se toma de configen
            if (this.iva == ivaGeneral)
            {
                return iva;
            }
            return 0;
        }


    }
}