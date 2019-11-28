using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JustServicios
{
    public partial class PedidoDet
    {

        private stocks s;
        private PedidoCab pedidoCab;

        public PedidoDet()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }

        public PedidoDet(stocks stock, PedidoCab cabecera, int item)
        {
            List<artasoc> artasoc;
            using (GestionEntities bd = new GestionEntities())
                artasoc = bd.artasoc.Where(a => a.codpro == stock.codpro && a.componen == stock.asociado).ToList();
            if (artasoc.Count == 0)
            {
                this.canasoc = 0;
            }
            else
                this.canasoc = Convert.ToInt32(artasoc.First().cantid * stock.cantidad);

            this.cabeceraid = cabecera.id;
            this.item = Convert.ToInt16(item);
            this.articulo = stock.codpro;
            this.descri = stock.descri;
            this.cantidad = stock.cantidad;
            this.parafecha = stock.parafecha;
            this.pendientes = stock.cantidad;
            this.preparado = 0;
            this.precio = stock.precioVenta;
            this.precorig = 0;//rrrrrrrrrrrr
            this.bonif = stock.bonif;
            this.bonif1 = stock.bonif1;
            this.total = (this.precio * this.cantidad)+ impint - this.precio * this.cantidad * stock.bonif / 100 - this.precio * this.cantidad * stock.bonif1 / 100;
            this.impint = stock.impint;
            this.orden = 0;//aaaaaaaaaaa
            this.marca = stock.marca;
            this.rubro = stock.rubro;
            this.nropro = stock.proveed;
            this.asociado = stock.asociado;
            this.detalle = stock.detalle;//aaaaa
            this.precioesp = 0;
            this.envase = stock.envase;
            this.unimed = stock.unimed;
            this.moneda = cabecera.monfac;
            this.ivaporc1 = Convert.ToDecimal(stock.iva);
            this.ivaporc2 = 0;
            this.deta = "";
            this.listo = false;
            this.codorigi = "";
            this.uxbulto = stock.uxbulto;
            this.costoest = 0;
            this.bajostk = false;
            this.impiva1 = stock.getIvaIGeneral(ControladorTotales.getCTotales().getIvaTasaGral()); 
            this.impiva2 = stock.getIvaIDif(ControladorTotales.getCTotales().getIvaTasaGral());
        }
    }
}