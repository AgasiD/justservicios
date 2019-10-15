using JustServicios.Clases;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JustServicios
{
    public partial class VistaPedido : System.Web.UI.Page
    {
        List<presupc> cabecera;
        List<vistacomprabantepresupuesto> detalle;
        List<empresa> empresa;
        protected void Page_Load(object sender, EventArgs e)
        { }
    }
}
            /*
               int idPresup = Convert.ToInt32(Request.QueryString["id"]),
                     empresaid = Convert.ToInt32(Request.QueryString["empresa"]);
               bool conFoto = Convert.ToBoolean(Request.QueryString["foto"]);
               string descriLocali;
               List<presupc> cabecera;
               List<vistacomprabantepresupuesto> detalle;
               List<empresa> empresa;
               List<stock> articulos = new List<stock>();
               string repo = "";
            int tipoComp = 1;// Convert.ToInt32(Request.QueryString["tipo"]);

               if (!IsPostBack)
               {
                  /* switch (tipoComp)
                   {
                       case 1: //si es presupuesto
                     
                           if (conFoto)
                           {
                               repo = "VistaPresupuestoccFoto.rdlc";
                           }
                           else
                           {
                               repo = "VistaPresupuesto.rdlc";
                           }
                           using (GestionEntities bd = new GestionEntities())
                           {
                               cabecera = bd.presupc.Where(a => a.id == idPresup).ToList();
                               var aux = cabecera.First();
                               detalle = bd.vistacomprabantepresupuesto.Where(a => a.cabeceraid == aux.id).ToList();
                               empresa = bd.empresa.Where(a => a.id == empresaid).ToList();
                               var aux2 = empresa.First();
                               descriLocali = bd.localidades.Single(a => a.id == aux2.localidad).nombre;
                               foreach (var art in detalle)
                               {
                                   articulos.Add(bd.stock.Single(a => a.codpro == art.codpro));
                               }
                           }  */
            /*   break;

       }


       reporte.LocalReport.EnableExternalImages = true;
       reporte.LocalReport.DataSources.Clear();
       reporte.LocalReport.DataSources.Add(new ReportDataSource("empresa", empresa));
       reporte.LocalReport.DataSources.Add(new ReportDataSource("presupc", cabecera));
       reporte.LocalReport.DataSources.Add(new ReportDataSource("presupd", detalle));
       reporte.LocalReport.DataSources.Add(new ReportDataSource("stock", articulos));
       reporte.LocalReport.ReportPath = repo;
       ReportParameter localidad = new ReportParameter("localidad", descriLocali);
       ReportParameter fecha = new ReportParameter("fecha", cabecera.First().fecha.ToString("dd/MM/yyyy"));
       reporte.LocalReport.SetParameters(localidad);
       reporte.LocalReport.SetParameters(fecha);
       reporte.LocalReport.Refresh();
   }
}
public void generarReporte(string rutaVista)
{
   reporte.LocalReport.DataSources.Clear();
   reporte.LocalReport.DataSources.Add(new ReportDataSource("empresa", empresa));
   reporte.LocalReport.DataSources.Add(new ReportDataSource("presupc", cabecera));
   reporte.LocalReport.DataSources.Add(new ReportDataSource("presupd", detalle));
   reporte.LocalReport.ReportPath = rutaVista;
   reporte.LocalReport.Refresh();
}




}
}*/

 