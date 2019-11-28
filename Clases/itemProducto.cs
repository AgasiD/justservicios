using JustServicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


    public class itemProducto
    {
        public decimal cant { get; set; }
        public string descri { get; set; }
        public decimal precioVenta { get; set; }
        public decimal total { get; set; }
        public decimal bonif { get; set; }
        public decimal bonif1 { get; set; }
        public decimal impint { get; set; }
        public string codpro { get; set; }
        public string[] asociado { get; set; }
        public string codigo { get; set; }

        public itemProducto() { }
        public itemProducto(decimal cant, string descri, decimal precioVenta, decimal total, decimal bonif, decimal bonif1, decimal impint, string codpro, string[] artasoc)
        {
            this.cant = cant;
            this.descri = descri;
            this.precioVenta = precioVenta;
            this.total = total;
            this.bonif = bonif;
            this.bonif1 = bonif1;
            this.impint = impint;
            this.codpro = codpro;
            this.asociado = artasoc;
         using (GestionEntities bd = new GestionEntities())
            this.codigo = bd.Database.SqlQuery<string>("select moneda from stock where codpro like '"+this.codpro+"'").First();
        }
    }
