using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JustServicios;

public class FWmasDatos
    {
        public decimal comisionVende { get; set;}
        public decimal comisionCliente { get; set; }
        public string codMoneda{ get; set;}
        public decimal cotizMoneda { get; set; }
        public string ordenCompra { get; set;}
        public string recargo { get; set;}
        public string notaVenta { get; set;}
        public string contacto { get; set;}
        public decimal nliberacion { get; set; }
        public decimal costoEstimado { get; set; }
        public decimal cuentacontable { get; set; }

    internal FWmasDatos convertirFWmasDatos(FWmasDatos_Result entrada)
    {
        this.comisionCliente = entrada.comisionCliente;
        this.comisionVende = Convert.ToDecimal(entrada.comisionVende);
        return this;
    }
}
