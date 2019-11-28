using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JustServicios.Views.Reports
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int report = Convert.ToInt32(Request.QueryString["nReporte"]);
            txt.Text = report.ToString();
            switch (report)
            {
                case 1:
                    lDeudores();
                    break;
            }
        }


        private void lDeudores()
        {
            string query = Request.QueryString["query"].ToString();
            int oCuenta = Convert.ToInt32(Request.QueryString["sortCount"]),
                oComprobante = Convert.ToInt32(Request.QueryString["sortDoc"]),
                reporte = Convert.ToInt32(Request.QueryString["report"]);
        }
    }
}