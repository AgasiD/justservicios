using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Web;

namespace JustServicios
{
    public partial class RemitoCab
    {

        public RemitoCab()
        {

        }
        public RemitoCab(JObject cabecera)
        {
            try
            {
                this.cumplido   = false;
                this.tipodoc    = cabecera["tipodoc"].ToString(); 
                this.letra      = cabecera["letra"].ToString();
                this.cotnro     = cabecera["cotnro"].ToString();
                this.direcc     = cabecera["direcc"].ToString();
                this.facturado  = cabecera["facturado"].ToString();
                this.locali     = cabecera["locali"].ToString();
                this.codpos     = cabecera["codpos"].ToString();
                this.nombreusu  = cabecera["nombreusu"].ToString();
                this.notavta    = cabecera["notavta"].ToString();
                this.detped     = cabecera["detped"].ToString();
                this.nrolibe    = cabecera["nrolibe"].ToString();
                this.contac     = cabecera["contac"].ToString();
                this.guia       = cabecera["guia"].ToString();
                this.sucursal   = cabecera["sucursal"].ToString();
                this.direntrega = cabecera["direntrega"].ToString();
                this.obsrepa    = cabecera["obsrepa"].ToString();
                this.observa    = cabecera["observa"].ToString();
                this.razsoc     = cabecera["razsoc"].ToString();
                this.monfac     = cabecera["monfac"].ToString();
                this.puestotra  = cabecera["puestotra"].ToString();
                this.listanro   = Convert.ToInt16(cabecera["listanro"]);
                this.id         = Convert.ToInt32(cabecera["id"]);
                this.empresaid  = Convert.ToInt32(cabecera["empresaid"]);
                this.tipodocid  = Convert.ToInt32(cabecera["tipodocid"]);
                this.nrocli     = Convert.ToInt32(cabecera["nrocli"]);
                this.codven     = Convert.ToInt32(cabecera["codven"]);
                this.nrotra     = Convert.ToInt32(cabecera["nrotra"]);
                this.provin     = Convert.ToInt32(cabecera["provin"]);
                this.respon     = Convert.ToInt32(cabecera["respon"]);
                this.localiid   = Convert.ToInt32(cabecera["localiid"]);
                this.transrep   = Convert.ToInt32(cabecera["transrep"]);
                this.ivavenid   = Convert.ToInt32(cabecera["ivavenid"]);
                this.usuarioid  = Convert.ToInt32(cabecera["usuarioid"]);
                this.lisfac     = Convert.ToInt32(cabecera["lisfac"]);
                this.condicion  = Convert.ToInt32(cabecera["condicion"]);
                this.cond_vta   = Convert.ToInt32(cabecera["cond_vta"]);
                this.nroped     = Convert.ToInt32(cabecera["nroped"]);
                this.bultos     = Convert.ToInt32(cabecera["bultos"]);
                this.transpo    = Convert.ToInt32(cabecera["transpo"]);
                this.kilos      = Convert.ToDecimal(cabecera["kilos"]);
                this.punto      = Convert.ToDecimal(cabecera["punto"]);
                this.numero     = Convert.ToDecimal(cabecera["numero"]);
                this.hasta      = Convert.ToDecimal(cabecera["numero"]);
                this.subtot     = Convert.ToDecimal(cabecera["subtot"]);
                this.bonifto    = Convert.ToDecimal(cabecera["bonifto"]);
                this.neto       = Convert.ToDecimal(cabecera["neto"]);
                this.bonitot    = Convert.ToDecimal(cabecera["bonitot"]);
                this.valdec     = Convert.ToDecimal(cabecera["valdec"]);
                this.impint     = Convert.ToDecimal(cabecera["impint"]);
                this.acopionro  = Convert.ToDecimal(cabecera["acopionro"]);
                this.nrorep     = Convert.ToDecimal(cabecera["nrorep"]);
                this.exento     = Convert.ToDecimal(cabecera["exento"]);
                this.ivai       = Convert.ToDecimal(cabecera["ivai"]);
                this.ivaidif    = Convert.ToDecimal(cabecera["ivaidif"]);
                this.ivanoi     = Convert.ToDecimal(cabecera["ivanoi"]);
                this.ivanoidif  = Convert.ToDecimal(cabecera["ivanoidif"]);
                this.percib     = Convert.ToDecimal(cabecera["percib"]);
                this.total      = Convert.ToDecimal(cabecera["total"]);
                this.nrotran    = Convert.ToDecimal(cabecera["nrotran"]);
                this.recargo    = Convert.ToDecimal(cabecera["recargo"]);
            }
            catch (Exception)
            {
            }
        }

    }
}