using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de Presupc
/// </summary>

namespace JustServicios
{
    public partial class presupc
    {
        public presupc() { }

        public presupc(int empresa, DateTime fecha, int tipodocid, string tipodoc, string letra,
            short punto, int numero, int hasta, int nrocli, string razsoc, string direcc, string locali,
            string codpos, decimal provin, string cuit, decimal subtot, decimal bonitot, decimal bonifto,
            decimal exento, decimal neto, decimal ivai, decimal ivanoi, decimal ivaidif, decimal ivanoidif,
            decimal total, decimal bonif, int condicion, string desccondic, int codven, string nomven,
            int respon, int tipcom, decimal comis, decimal retiva, decimal retgan, decimal retinb,
            string observa, decimal comiprod, decimal comicli, bool liquidada, decimal vence, int nrorepa,
            string nombreusu, decimal internos, int cond_vta, int lista, string condicionN, string usuario, string monfac,
            decimal percib, string simbolo, int id, decimal recargo, decimal cotizaMoneda, string atencion, string referencia,
            string validez, string plentrega, string lugentre, decimal costoest, int localiid, int usuarioid)
        {
            try
            {
                this.usuarioid = usuarioid;
                this.localidadid = localiid;
                this.id = id;
                this.nombreusu = usuario;
                this.empresa = empresa;
                this.fecha = DateTime.Now;
                this.tipodocid = tipodocid;
                this.tipodoc = tipodoc;
                this.letra = letra;
                this.punto = punto;
                this.numero = numero;
                this.hasta = hasta;
                this.nrocli = nrocli;
                this.razsoc = razsoc.Substring(0,40);
                this.direcc = direcc.Substring(0, 40);
                this.locali = locali;
                this.codpos = codpos.Substring(0, 10);
                this.provin = provin;
                this.cuit = cuit;
                this.subtot = subtot;
                this.bonitot = bonitot;
                this.bonifto = bonifto;
                this.total = exento;
                this.neto = neto;
                this.ivai = ivai;
                this.ivanoi = ivanoi;
                this.ivaidif = ivaidif;
                this.ivanoidif = ivanoidif;
                this.total = total;
                this.bonif = bonif;
                this.condicion = condicion;
                this.desccondic = desccondic;
                this.codven = codven;
                this.nomven = nomven;
                this.respon = respon;
                this.tipcom = tipcom;
                this.comis = comis;
                this.retiva = retiva;
                this.retgan = retgan;
                this.retinb = retinb;
                this.observa = observa;
                this.comiprod = comiprod;
                this.comicli = comicli;
                this.liquidada = liquidada;
                this.vence = DateTime.Now.AddDays(Convert.ToDouble(vence));
                this.nrorepa = nrorepa;
                this.nombreusu = nombreusu;
                this.internos = internos;
                this.cond_vta = cond_vta;
                this.monfac = monfac;
                this.percib = percib;
                this.recargo = recargo;
                ocompra = "";
                this.simbolo = simbolo.Substring(0, 3);
                this.atencion = atencion;
                this.referencia = referencia ;
                this.validez = validez;
                this.plentrega = plentrega;
                this.lugentre = lugentre;
                this.obsint = "";
                this.costoest = costoest;
                nuepresup = "";
                viejpresu = "";
                planilla = "";
                this.lisfac = lista;
                this.desccondic = condicionN;
                this.cotizacion = cotizaMoneda;
            }
            catch (DbEntityValidationException dbEx)
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


            }
        }
    }
}