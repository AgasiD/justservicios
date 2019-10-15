<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Reporte.aspx.cs" Inherits="Reporte" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title id="titulo" runat="server"></title>
   
</head>
<body>
     <header>
       
          <div class="alert alert-info gj-text-align-center" id="alertaElementos" runat="server">
            <strong><span class="mb-0">¡No se han encontrado resultados! 
                        <button type="button" class="close" data-dismiss="alert" aria-label="Cerrar">
                            &times;
                        </button>
            </span></strong>


        </div>
       </header>
       <form id="form1" runat="server">
    <asp:TextBox ID="txt" runat="server"></asp:TextBox>
    <asp:ScriptManager runat="server"></asp:ScriptManager>        
   <rsweb:ReportViewer ID="reporte" runat="server" Height="750" Width="850">

                </rsweb:ReportViewer>
           
    </form>

</body>
</html>
