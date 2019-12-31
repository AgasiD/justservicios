using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Web;

namespace JustServicios
{
    public partial class RemitoDet
    {

        public RemitoDet()
        {

        }
        public RemitoDet(JToken detalle)
        {
            this.descri     = detalle["descri"].ToString();
            this.codpro     = detalle["codpro"].ToString();
            this.asociado   = detalle["asociado"].ToString();
            this.despacho   = detalle["despacho"].ToString();
            this.unimed     = detalle["unimed"].ToString();
            this.envase     = detalle["envase"].ToString();
            this.deta       = detalle["deta"].ToString();
            this.ocompra    = detalle["ocompra"].ToString();
            this.cbarras    = detalle["cbarras"].ToString();
            this.codorigi   = detalle["codorigi"].ToString();
            this.tipofac    = detalle["tipofac"].ToString();
            this.letrafac   = detalle["letrafac"].ToString();
            this.id         = Convert.ToInt32(detalle["id"]);
            this.cabeceraid = Convert.ToInt32(detalle["cabeceraid"]);
            this.nroped     = Convert.ToInt32(detalle["nroped"]);
            this.bultos     = Convert.ToInt32(detalle["bultos"]);
            this.cant       = Convert.ToDecimal(detalle["cant"]);
            this.precio     = Convert.ToDecimal(detalle["precio"]);
            this.bonif      = Convert.ToDecimal(detalle["bonif"]);
            this.bonif1     = Convert.ToDecimal(detalle["bonif1"]);
            this.prexcant   = Convert.ToDecimal(detalle["prexcant"]);
            this.canasoc    = Convert.ToDecimal(detalle["canasoc"]);
            this.pedido     = Convert.ToDecimal(detalle["pedido"]);
            this.cantenv    = Convert.ToDecimal(detalle["cantenv"]);
            this.porcen1    = Convert.ToDecimal(detalle["porcen1"]);
            this.porcen2    = Convert.ToDecimal(detalle["porcen2"]);
            this.impiva1    = Convert.ToDecimal(detalle["impiva1"]);
            this.impiva2    = Convert.ToDecimal(detalle["impiva2"]);
            this.uxbulto    = Convert.ToDecimal(detalle["uxbulto"]);
            this.kilos      = Convert.ToDecimal(detalle["kilos"]);
            this.puntofac   = Convert.ToDecimal(detalle["puntofac"]);
            this.nrofac     = Convert.ToDecimal(detalle["nrofac"]);
            this.bajostk    = Convert.ToBoolean(detalle["bajostk"]);

        }

    }
}