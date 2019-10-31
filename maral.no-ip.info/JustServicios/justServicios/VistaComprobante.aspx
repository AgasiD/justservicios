<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VistaComprobante.aspx.cs" Inherits="JustServicios.VistaPedido" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
       <form id="form1" runat="server">
    <asp:ScriptManager runat="server"></asp:ScriptManager>        
   <rsweb:ReportViewer ID="reporte" runat="server" Height="750" Width="850">
                </rsweb:ReportViewer>
           
    </form>

</body>
</html>
