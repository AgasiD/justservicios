using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Web;

namespace JustServicios
{
    public partial class PedidoCab
    {

        public PedidoCab()
        {

        }
        public PedidoCab(int idEmpresa, int tipoDocId, int punto, string tipoDoc, string letra, DateTime fecha, int nrocli, string razsoc, int vendedor,
       int condicion, string via, int viaid, int sucursalid, string sucursal, string usuario, int usuarioid, decimal neto, decimal exento,
       decimal bonifto, decimal impiva1, decimal impiva2, decimal timpint, decimal percib, decimal total, int transpo, double vencedias,
       int kilos, int bultos, string direcc, string locali, string codpos, int provin, int localiid, string observa, DateTime parafecha, int cond_vta,
       string puestoTrabajo, decimal nroped, string monfac, int id, decimal bonifcli, string presup
       )
        {
            try
            {
                this.id = id;
                this.nroped = nroped;
                this.facturado = "N";
                this.condicion = condicion;
                this.codven = vendedor;
                this.PercIB = percib;
                this.Total = total;
                this.observa = observa;
                this.kilos = kilos;
                this.bultos = bultos;
                this.direcc = direcc.Substring(0, 39);
                this.locali = locali;
                this.codpos = codpos;
                this.provin = provin;
                this.sucursalid = sucursalid;
                this.localiid = localiid;
                this.monfac = monfac;
                this.transpo = transpo;
                this.vence = fecha.AddDays(vencedias);
                this.cumplido = false;
                this.ocompra = "0";//arreglar
                this.aprobado = false;
                this.presup = presup;
                this.lisfac = 1;//arreglar
                this.notavta = "";//arreglar
                this.TotalExento = exento;
                this.bonifto = bonifto;
                this.bonitot = bonifcli;
                this.impiva1 = impiva1;
                this.impiva2 = impiva2;
                this.Timpint = timpint;
                this.Timpint = timpint;
                this.TotalNet = neto;
                this.preparo = "";
                this.completo = false;
                this.listo = false;
                this.puestrotra = "Web";//dejar asi
                this.enhora = fecha.Date; // verificar;
                this.usuario = usuario;
                this.usuarioid = usuarioid;
                this.nrotra = 0;
                this.sucursal = sucursal;
                this.sucursalid = sucursalid;
                this.viaid = viaid;
                this.via = via;
                this.cond_vta = cond_vta;
                this.asignadoa = "";
                this.punto = punto;
                this.empresaid = idEmpresa;
                this.tipodocid = tipoDocId;
                this.tipodoc = tipoDoc;
                this.letra = letra.Substring(0,1);
                this.tipo = "";
                this.fechaing = fecha;
                this.parafecha = parafecha;
                this.nrocli = nrocli;
                this.razsoc = razsoc;
                this.reimp = false;

            

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
            catch (Exception)
            {

            }
        }

    }
}