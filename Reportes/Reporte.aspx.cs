﻿using JustServicios;
using JustServicios.Clases.Controladores;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Dynamic;

public partial class Reporte : System.Web.UI.Page
{

    string query, dsd, hst, rutaReporte;
    int empresa, report;
    Dictionary<string, string> parametros = new Dictionary<string, string>();
    /*
        List<presupc> cabecera;
        List<vistaComprobantePresupuesto> detalle;
        List<empresa> empresa;
    */
    RequestHTTP oConfigen;
    protected void Page_Load(object sender, EventArgs e)
    {

        txt.Visible = false;
        report = Convert.ToInt32(Request.QueryString["nReporte"]);
        empresa = Convert.ToInt32(Request.QueryString["empresa"]);
        oConfigen = ControladorConfigen.getInstancia().getConfigen(empresa);
            dsd = Request.QueryString["dsd"].ToString();
        hst = Request.QueryString["hst"].ToString();
        reporte.KeepSessionAlive = false;
        reporte.AsyncRendering = false;
        if (!IsPostBack)
        {
            alertaElementos.Visible = false;
            reporte.Visible = false;
            switch (report)
            {
                case 1:
                    lDeudores();
                    break;
                case 2:
                    lPreciosDC();
                    break;
                case 3:
                    lArtNoVendidos();
                    break;
                case 4:
                    lArtPorProveedor();
                    break;
                case 5:
                    LArtVendAgrupando();
                    break;
                case 6:
                    LArtVendCantPorArt();
                    break;
                case 7:
                    LClientes();
                    break;
                case 8:
                    LCobranzas();
                    break;
                case 9:
                    LComisiones();
                    break;
                case 10:
                    LCotizacionesEmitidas();
                    break;
                case 11:
                    LConceptosCaja();
                    break;
                case 12:
                    LMarcas();
                    break;
                case 13:
                    LOrdenesdeCompraPend();
                    break;
                case 14:
                    LOperacionesUsuario();
                    break;
                case 15:
                    LPedPendientes();
                    break;
                case 16:
                    LOperacionesPorRubro();
                    break;
                case 17:
                    LArtVendidosPorVende();
                    break;
                case 18:
                    LPresup();
                    break;
                case 19:
                    LSaldoCliente();
                    break;
                case 20:
                    LSaldoProveedores();
                    break;
                case 21:
                    LRemitos();
                    break;
                case 22:
                    LVendedores();
                    break;
                case 23:
                    LSubrubros();
                    break;
                case 24:
                    LRubros();
                    break;
                case 25:
                    LTransportistas();
                    break;
                case 26:
                    LProveedores();
                    break;
                case 27:
                    LZonas();
                    break;
                case 28:
                    LStockValorizado();
                    break;
                case 29:
                    LStockActual();
                    break;
                case 31:
                    LRefCliente();
                    break;
                case 32:
                    LPuntosdePedido();
                    break;
                case 33:
                    LCotizacionesMesAMes();
                    break;
                case 34:
                    viewComprobanteFactura();
                    break;
                case 35:
                    verPagos();
                    break;
                case 36:
                    viewNotaPedido();
                    break;
                case 37:
                    LVentasMesAMes();
                    break;
                case 38:
                    rankingArtComprados();
                    break;
                case 39:
                    VtaxArtImporte();
                    break;
                case 40:
                    LArtVendidos();
                    break;
                case 41:
                    LMovCaja();
                    break;
                case 42:
                    LPorcenCliFactu();
                    break;
                case 43:
                    LUtilidades();
                    break;
                case 44:
                    LStockCodigo();
                    break;
                case 45:
                    LRankingComp();
                    break;
                case 46:
                    LDeudaProvee();
                    break;
            }
        }
    }

    /*
    public void LconceptosCaja()
    {
        List<concepto> lista;
        using (var bd = new GestionEntities())
            bd.Database.SqlQuery<concepto>("select codigo, descri from concepto").ToList();
        string rutaReporte = "Reportes/ConceptosCaja/lconcepto.rdlc";
        generarReporte(rutaReporte, parametros, new ReportDataSource("lista", lista), dsd, hst);
        
        

    }
    */


    public void LDeudaProvee()
    {
        List<DeudaProvee> lista;
        int nropro = Convert.ToInt32(Request.QueryString["nropro"].ToString()),
            concepto = Convert.ToInt32(Request.QueryString["concepto"].ToString()),
            ordenId = Convert.ToInt32(Request.QueryString["orden"].ToString());
        bool anotacion = Convert.ToBoolean(Request.QueryString["anotacion"].ToString()),
            suspendida = Convert.ToBoolean(Request.QueryString["suspendida"].ToString()),
            saldo = Convert.ToBoolean(Request.QueryString["saldo"].ToString()),
            resumido = Convert.ToBoolean(Request.QueryString["resumido"].ToString());

        DateTime dsd = Convert.ToDateTime(Request.QueryString["dsd"].ToString()),
                 hst = Convert.ToDateTime(Request.QueryString["hst"].ToString());

        int comp = Convert.ToInt32(Request.QueryString["comp"].ToString());
        string  orden = " order by ",
                tipoDoc = " and ";

        string where = "";
        if (saldo)
            where += " and sal.importe > 0 ";
        if (!suspendida)
            where += " and p.incobrable = 0";
        if (nropro > 0)
            where += " and p.nropro = " + nropro;
        if (concepto > 0)
            where += " and p.concepto = " + concepto;
        switch(comp) // tipos de comprobantes
        {
            case 1:
                tipoDoc = "";
                break;
            case 2:
                tipoDoc = " and pd.tipodoc in ('FC', 'TK', 'LE', 'CT')";
                break;
            case 3:
                tipoDoc = " and  pd.tipodoc in ('NC')";
                break;
            case 4:
                tipoDoc = "  and pd.tipodoc in ('ND')";
                break;
            default:
                tipoDoc = "";
                break;
        }
        switch (ordenId) //ordena los datos
        {
            case 1:
                orden += " p.nropro, pd.vence";
                break;
            case 2:
                orden += " p.razsoc, pd.vence ";
                break;
            case 3:
                orden += " pd.vence, p.nropro";
                break;
            case 4:
                orden += " pd.vence, p.nropro "; //no es este orden, falta campo "vencedias" 
                break;
            default:
                orden = "";
                break;
        }
        where += " and pd.empresaid = " + empresa + " and pd.vence >= '" + dsd.ToString("dd/MM/yyyy") + "' and pd.vence <= '" + hst.ToString("dd/MM/yyyy") + "' " + tipoDoc;
        string query =
            "select p.nropro, p.razsoc, pd.fecha, pd.tipodoc, pd.letra, pd.punto, pd.numero, pd.importe1 importeOriginal," +
            " pd.importe2 importe2, sal.importe saldo, pd.vence, condpago.descripcion condicion " +
            "from proctad pd " +
            "left join provee p on pd.nropro = p.nropro " +
            "left join SaldosProveedoresAll(" + empresa + ") sal on pd.tipodoc = sal.tipodoc and pd.letra = sal.letra and pd.punto = sal.punto and pd.numero = sal.numero "+
            "left join condpago on condpago.codigo = p.condicion " +
            "where 1 = 1  " + where + orden;
        if (resumido)
            rutaReporte = "Reportes/DeudaProveedores/ldeudapResum.rdlc";
        else
            rutaReporte = "Reportes/DeudaProveedores/ldeudap.rdlc";

        using (var bd = new GestionEntities())
            lista = bd.Database.SqlQuery<DeudaProvee>(query).ToList();

        generarReporte(rutaReporte, parametros, new ReportDataSource("lista", lista), dsd.ToString("dd/MM/yyyy"), hst.ToString("dd/MM/yyyy"));
        
    }
    public void LRankingComp()
    {
        DateTime dsd = Convert.ToDateTime(Request.QueryString["dsd"].ToString())
               , hst = Convert.ToDateTime(Request.QueryString["hst"].ToString());
        List<RankCompra> lista; 
        string rutaReporte, consulta =
            "select nropro, max(razsoc) razsoc, sum(neto + neto1) neto , sum(exento) exento, sum(total) total from ivacom " +
            "where fecha >= '" + dsd.ToString("dd/MM/yyyy") + "' and fecha <= '" + hst.ToString("dd/MM/yyyy") + "' and nropro > 0 and empresaid = " + empresa +
            "group by nropro " +
            "order by total desc";
        using (var bd = new GestionEntities())
            lista = bd.Database.SqlQuery<RankCompra>(consulta).ToList();
        decimal totalTotal = 0;
        foreach(var det in lista ){
            totalTotal += det.total;
        }
        parametros.Add("TotalTotal", totalTotal.ToString());
        rutaReporte = "Reportes/RankingComprasProvee/LComprasProvee.rdlc";
        generarReporte(rutaReporte, parametros, new ReportDataSource("lista", lista), dsd.ToString("dd/MM/yyyy"), hst.ToString("dd/MM/yyyy"));
    }
    public void LStockCodigo()
    {
        string orden = Request.QueryString["orden"].ToString();
        string codigo = Request.QueryString["codigo"].ToString();
        string ordenado = "";
        if (orden == "1")
            ordenado = " codpro";
        else
            ordenado = " descri";
        
        List<MiLStockCodigo> lista;
        using(GestionEntities bd = new GestionEntities()){
            lista = bd.Database.SqlQuery<MiLStockCodigo>(
                "select stock.codpro, descri, fechveri, saldo, color "                        +
                "from stock "                                                                 +
                "left join SaldoSTKAll(" + empresa + ") as stk on stk.codpro = stock.codpro " +
                "where stock.codpro like '" + codigo + "%' "                                 +
                "order by " + ordenado).ToList();
        }
        string ruta = "reportes/StockCodigo/LStockCodigo.rdlc";
        generarReporte(ruta, new Dictionary<string, string>(), new ReportDataSource("LStockCodigo", lista), dsd, hst);
    }

    
    public void LUtilidades()
    {
        
        List<MiUtilidades> lista;
        bool incluCoti = Convert.ToBoolean(Request.QueryString["cotizaciones"].ToString());
        DateTime dsd = Convert.ToDateTime(Request.QueryString["dsd"].ToString()),
            hst = Convert.ToDateTime(Request.QueryString["hst"].ToString());
        string whereRequest = getQueryFromFile(),
        cotizaciones = "";
        if (whereRequest != "")
            whereRequest = " and " + whereRequest;
        if (!incluCoti)
            cotizaciones = " and cab.tipodoc not in ('CT', 'AJD', 'AJC') ";
        string where = " where cab.empresaid = " + empresa + " and det.fecha >= '" + dsd + "' and det.fecha <= '" + hst + "' " + whereRequest + cotizaciones;
        string consulta = "select max(det.codpro) codpro, max(det.descri) descri , " +
            "sum(det.cant) cant, sum(det.prexcant * det.cotizacion) venta, sum(det.costo * abs(cant) * det.cotizacion) costo, " +
            "sum(det.prexcant - (prexcant * det.bonito / 100) * det.cotizacion) - sum(det.costo * abs(cant) * det.cotizacion) diferencia," +
			"CASE WHEN max(det.costo * det.cotizacion) <> 0 THEN" +
            " (sum( (det.prexcant - (prexcant * det.bonito / 100) ) * det.cotizacion) / Sum(det.costo * det.cotizacion * cant) * 100) - 100 " +
            "ELSE 0 END utilidad,"                                                   +
            "max(rubros.codigo) codRubro, max(rubros.descri) descriRubro, "          +
            "max(subrub.codigo) codSubrub, max(subrub.descri) descriSubrub ,"        +
            "max(marcas.codigo) codMarca, max(marcas.descripcion) descriMarca"       +
            " from detmovim det"                                                     +
            " left join ivaven cab on cab.id = det.ivavenid"                         +
            " left join stock st on st.codpro = det.codpro"                          +
            " left join rubros on st.rubro = rubros.codigo"                          +
            " left join subrub on st.subrub = subrub.codigo"                         +
            " left join marcas on marcas.codigo = st.marca"                          +
            " left join provee on provee.nropro = st.proveed"                        +
            " left join tipoart on st.tipoart = tipoart.codigo"                      +
            " left join cliente on cliente.nrocli = cab.nrocli"                      +
            " left join activida on activida.codigo = cliente.activi"                +
            " left join vende on vende.codven = cliente.codven"                      +
            " left join zonas on cliente.zona = zonas.codigo "                       +
              where                                                                  +           
            " group by det.codpro";

        using (GestionEntities bd = new GestionEntities())
            lista = bd.Database.SqlQuery<MiUtilidades>(consulta).ToList();

        string ruta = "reportes/UtilidadesTeoricas/lutilidades.rdlc";
        generarReporte(ruta, parametros, new ReportDataSource("Teoricas", lista), dsd.ToString("dd/MM/yyyy"), hst.ToString("dd/MM/yyyy"));
    }

    private string getQueryFromFile()
    {
        string line;
        System.IO.StreamReader file = new System.IO.StreamReader(@"C:\inetpub\wwwroot\JustServicios\JustServicios\Reportes\query.txt");
        while ((line = file.ReadLine()) != null)
            query += line;
        file.Close();
        return query;
    }

   
public void LPorcenCliFactu()
    {
        bool pedidos = Convert.ToBoolean(Request.QueryString["ped"]),
             cotizacion = Convert.ToBoolean(Request.QueryString["coti"]),
             venta = Convert.ToBoolean(Request.QueryString["venta"]),
             remito = Convert.ToBoolean(Request.QueryString["remito"]);
        int  codven = Convert.ToInt32(Request.QueryString["codven"]),
             nrocli = Convert.ToInt32(Request.QueryString["nrocli"]);
        DateTime ddsd = Convert.ToDateTime(Request.QueryString["dsd"].ToString()),
          dhst = Convert.ToDateTime(Request.QueryString["hst"].ToString());
        string query = "";
        string where = "";
      
        if (venta)
        {
            where = getWhere("ivaven.", ddsd, dhst, nrocli, codven);
            string coti = "";
            if (!cotizacion)
                coti = " and ivaven.tipodoc not in ('CT', 'AJD', 'AJC') ";
            query = " select convert(numeric(5,0),max(ivaven.nrocli)) nrocli, max(razsoc) razsoc, sum(cant) cant, (sum(cant) / (select sum(cant) from detmovim)) porcencant," +
                " (sum(cantenv) / (select sum(cantenv) from detmovim) )  porcencantenv, sum(cant * cantenv) cantenv, sum(prexcant) neto, " +
                "(sum(neto) / (select sum(neto) from ivaven)) porcenneto" +
                " from ivaven " +
                "left join detmovim det on det.ivavenid = ivaven.id" +
                " where 1 = 1 " + where + coti + " " +
                "group by ivaven.nrocli ";
        }
        if(cotizacion && !venta)
        {
             where = getWhere("ivaven.", ddsd, dhst, nrocli, codven);
            string coti = "";
                       coti = " and ivaven.tipodoc in ('CT', 'AJD', 'AJC') ";
            query = " select convert(numeric(5,0),max(ivaven.nrocli)) nrocli, max(razsoc) razsoc, sum(cant) cant, (sum(cant) / (select sum(cant) from detmovim)) *100 porcencant," +
                " (sum(cantenv) / (select sum(cantenv) from detmovim) ) porcencantenv, sum(cant * cantenv) cantenv, sum(prexcant) neto, " +
                "(sum(neto) / (select sum(neto) from ivaven)) *100 porcenneto" +
                " from ivaven " +
                "left join detmovim det on det.ivavenid = ivaven.id" +
                " where 1=1  " + where + coti + " " +
                "group by ivaven.nrocli ";
        }
        if (pedidos)
        {
            where = getWhere("pedidocab.", ddsd, dhst, nrocli, codven);
            where = where.Replace("fecha", "fechaing");
            if (query != "")
                query += " union ";
            query += " select convert(numeric(5,0),max(PedidoCab.nrocli)) nrocli, max(razsoc) razsoc, sum(cantidad) cant, " +
                " (sum(cantidad) / (select sum(cantidad) from pedidodet)) *100 porcencant, (sum(cantidad) / (select sum(1) from pedidodet) ) *100 porcencantenv," +
                " sum(cantidad * 1) cantenv, sum(cantidad * precio - (precio * cantidad * bonif/100) - (cantidad * precio - (precio * cantidad * bonif1/100))) neto, (sum(pedidocab.total) / (select sum(total) from PedidoCab)) *100 porcenneto " +
                " from PedidoCab " +
                " left join PedidoDet det on det.cabeceraid = PedidoCab.id" +
                " where pedidocab.listo = 0 " + where + " group by PedidoCab.nrocli ";
        }
        if (remito)
        {
            where = getWhere("remitocab.", ddsd, dhst,nrocli,codven);
            if (query != "")
                query += " union ";
            query += "select convert(numeric(5,0),max(RemitoCab.nrocli)) nrocli, max(razsoc) razsoc, sum(cant) cant,  (sum(cant) / (select sum(cant) from remitodet)) *100 porcencant," +
                " (sum(cantenv) / (select sum(cantenv) from remitodet) ) *100 porcencantenv, sum(cant * cantenv) cantenv, sum(prexcant) neto," +
                " (sum(neto) / (select sum(neto) from Remitocab)) *100 porcenneto" +
                " from RemitoCab " +
                " left join remitodet det on det.cabeceraid = RemitoCab.id " +
                " where facturado not like 'S' " + where + "" +
                " group by RemitoCab.nrocli";
        }
        List<MiPorcenVentas> lista;
        using (GestionEntities bd = new GestionEntities())
            lista = bd.Database.SqlQuery<MiPorcenVentas>(query).ToList();
        lista = lista.OrderByDescending(a => a.neto).ToList();
        string ruta = "Reportes/Proveedores/LPorcenVentas.rdlc";
        generarReporte(ruta, parametros, new ReportDataSource("PorcenVentas", lista), dsd, hst);
    }

    private string getWhere(string table, DateTime ddsd, DateTime dhst, int nrocli, int codven)
    {
        string query = " and " + table + "fecha >= '" + ddsd.ToString("dd/MM/yyyy") + "' and " + table + "fecha <= '" + dhst.ToString("dd/MM/yyyy") + "' ";
        if (codven > 0)
            query += " and " + table + "codven = " + codven;
        if (nrocli > 0)
            query += " and  " + table + "nrocli = " + nrocli;

        return query;
    }

    public void LMovCaja()
    {
        string query = Request.QueryString["query"].ToString();

    }

    public void LArtVendidos()
    {
        string query = Request.QueryString["query"].ToString();
        string ruta;
        DateTime ddsd, dhst;
        ddsd = Convert.ToDateTime(Request.QueryString["dsd"].ToString());
        dhst = Convert.ToDateTime(Request.QueryString["hst"].ToString());
        List<ArtVendidos> lista;
        using (GestionEntities bd = new GestionEntities())
        {
            lista = bd.Database.SqlQuery<ArtVendidos>(
                 "SELECT ivaven.tipodoc,  ivaven.letra,  detmovim.empresa,  ivaven.numero,  detmovim.codpro, detmovim.descri, "                     +
                 "detmovim.cant, detmovim.precio, detmovim.prexcant, ivaven.bonitot, vende.nombre, vende.codven, ivaven.nrocli, "                   +
                 "ivaven.razsoc, ivaven.fecha "                                                                                                     +
                 "FROM detmovim "                                                                                                                   +
                 "left JOIN ivaven ON  ivaven.id = detmovim.ivavenid left JOIN vende ON  ivaven.codven = vende.codven "                             +
                 "left JOIN stock ON  stock.codpro = detmovim.codpro "                                                                              +
                 "where ivaven.empresaid = " + empresa + " and " + query
                ).ToList();
        }

        ruta = "Reportes/ArtVendidos/LArtVendidos.rdlc";

        generarReporte(ruta, parametros,
            new ReportDataSource("LArtVendidos", lista), ddsd.ToString("dd/MM/yyyy")
            , dhst.ToString("dd/MM/yyyy"));
    }
    public void VtaxArtImporte()
    {

        string query = Request.QueryString["query"].ToString();
        string ruta;
        DateTime ddsd, dhst;
        ddsd = Convert.ToDateTime(Request.QueryString["dsd"].ToString());
        dhst = Convert.ToDateTime(Request.QueryString["hst"].ToString());
        List<VtaxArtImporte> lista;
        using (GestionEntities bd = new GestionEntities())
        {
            lista = bd.Database.SqlQuery<VtaxArtImporte>(
                "select cab.tipodoc, cab.letra, cab.punto, cab.numero, cab.fecha, det.codpro, det.cant, det.precio, det.prexcant, det.bonito, cliente.razsoc, stock.descri, cab.nrocli " +
                "from detmovim det "                                +
                "left join ivaven cab on cab.id = det.ivavenid "    +
                "left join stock on det.codpro = stock.codpro "     +
                "left join cliente on cliente.nrocli = cab.nrocli " +
                "where empresa = " + empresa + " and " + query).ToList();
        }

        ruta = "Reportes/VtaArtImportes/VtaArtXImportes.rdlc";

        generarReporte(ruta, parametros,
            new ReportDataSource("VtaxImporta", lista), ddsd.ToString("dd/MM/yyyy")
            , dhst.ToString("dd/MM/yyyy"));
    }
    public void rankingArtComprados()
    {
        bool detallado = Convert.ToBoolean(Request.QueryString["detallado"].ToString());
        string query = Request.QueryString["query"].ToString();
        string ruta;
        DateTime ddsd, dhst;
        ddsd = Convert.ToDateTime(Request.QueryString["dsd"].ToString());
        dhst = Convert.ToDateTime(Request.QueryString["hst"].ToString());
        List<RankingCompra> lista;
        using (GestionEntities bd = new GestionEntities())
        {
            lista = bd.Database.SqlQuery<RankingCompra>(
                "select cab.tipodoc, cab.letra, cab.punto, cab.numero, cab.fecha, det.codpro, det.cant, det.precio, det.prexcant, det.bonito, provee.razsoc, stock.descri, cab.nropro " +
                "from detcompr det "                                                                 +
                "left join ivacom cab on cab.id = det.ivacomid "                                     +
                "join stock on det.codpro = stock.codpro join provee on provee.nropro = cab.nropro " +
                "where empresa = " + empresa + " and " + query).ToList();
        }
        if (detallado)
        {
            ruta = "Reportes/RankingCompras/RankingArtCompradosDet.rdlc";
        }
        else
        {
            ruta = "Reportes/RankingCompras/RankingArtCompradosAcum.rdlc";
        }
        generarReporte(ruta, parametros,
            new ReportDataSource("RankingComprasArt", lista), ddsd.ToString("dd/MM/yyyy")
            , dhst.ToString("dd/MM/yyyy"));
    }

    private void viewNotaPedido()
    {
        int idCab = Convert.ToInt32(Request.QueryString["cabeceraId"].ToString());
        var dataSources = new List<ReportDataSource>();
        List<NotaPedidoCab> cabecera;
        List<PedidoDet> detalle;
        List<NotaPedidoPie> pie;
        using (GestionEntities bd = new GestionEntities())
        {
            cabecera = bd.Database.SqlQuery<NotaPedidoCab>(
                "select  pedidocab.id idCab ,punto, nroped, nrocli, razsoc, pedidocab.codven, vende.nombre, fechaing, parafecha, pedidocab.direcc, pedidocab.locali, localiid, " +
                "pedidocab.codpos " +
                "from pedidocab " +
                "left join vende on vende.codven = pedidocab.codven" +
                " where pedidocab.id = " + idCab
                ).ToList();
            detalle = bd.Database.SqlQuery<PedidoDet>("select * from PedidoDet where cabeceraid = " + cabecera.First().idCab).ToList();
            pie = bd.Database.SqlQuery<NotaPedidoPie>(
                "select  transpo.razsoc, codigo, transpo.direcc, transpo.cuit, pedidocab.direcc direccEntrega, horario " +
                "from transpo left join pedidoCab on pedidocab.transpo = transpo.codigo where pedidocab.id = " + cabecera.First().idCab
                ).ToList();
        }
        dataSources.Add(new ReportDataSource("cabecera", cabecera));
        dataSources.Add(new ReportDataSource("detalle", detalle));
        dataSources.Add(new ReportDataSource("pie", pie));
        generarReporteComprobante("reportes/comprobantes/notapedido.rdlc", dataSources);
    }

    private void verPagos()
    {
        int numero = Convert.ToInt32(Request.QueryString["numero"].ToString()),
            punto = Convert.ToInt32(Request.QueryString["punto"].ToString());
        string tipodoc = Request.QueryString["tipodoc"].ToString(),
                letra = Request.QueryString["letra"].ToString();
        List<ComprobanteSaldo> ultSaldo = new List<ComprobanteSaldo>();
        List<ComprobanteSaldo> lista;
        using (GestionEntities bd = new GestionEntities())
        {
            lista = bd.Database.SqlQuery<ComprobanteSaldo>(
                " select fecha, clicta.tipodoc, letra, punto, numero, debe, haber, saldo, tipdoco, letrao, puntoo, numeroo, clicta.id, clicta.nrocli, razsoc, verifi, verifico, " +
                "simbolo, recimanu from clicta left join cliente on cliente.nrocli = clicta.nrocli where ").ToList();
            ultSaldo.Add(lista.First());
            lista.Remove(lista.First());
        }

    }

    private void viewComprobanteFactura()
    {
        var config = (Dictionary<string, object>)oConfigen.objeto;
        int numero = Convert.ToInt32(Request.QueryString["numero"].ToString()),
            punto = Convert.ToInt32(Request.QueryString["punto"].ToString()),
            candec = Convert.ToInt32(config["candec"].ToString()),
            impdec = Convert.ToInt32(config["impdec"].ToString());

        string tipodoc = Request.QueryString["tipodoc"].ToString(),
                letra = Request.QueryString["letra"].ToString();

        if (tipodoc.Contains("FC") || tipodoc.Contains("NC") || tipodoc.Contains("CT"))
        {
            List<CabeceraFactura> cab;
            List<DetalleFactura> detalle;
            using (GestionEntities bd = new GestionEntities())
            {
                cab = bd.Database.SqlQuery<CabeceraFactura>(
                    " select ivaven.codven, cliente.telef1, cotizacion, numerofe, ivaven.tipodocid, ivaven.subtot cabSubt, " +
                    " ivaven.bonifto cabBonifto, ivaven.bonitot cabBonitot, ivaven.neto cabNeto, ivaven.exento cabExento ,ivaven.ivai cabIvai, ivaven.ivanoi cabIvanoi," +
                    " ivaven.ivaidif cabIvaidif, ivaven.ivanoidif cabIvanoidif, ivaven.porcenib cabPorcenib, ivaven.retinb cabRetinIB, ivaven.simbolo cabSimbolo, ivaven.total cabTotal," +
                    " ivaven.cae, ivaven.vencecae, tipocomprobante.discrimina, ivaven.observa cabObserva,  ivaven.id, ivaven.tipodoc, ivaven.letra, punto, numero, emp.id, ivaven.porins," +
                    " ivaven.porinsdif, ivaven.pornoi, ivaven.pornoidif, emp.direccion empDireccion, locali.nombre localiEmp, emp.telefono empTelefono, emp.email empEmail," +
                    " iva.descripcion empIva, ivaven.fecha facFecha, emp.cuit empCuit, emp.finiact empInicio, ivaven.razsoc, ivaven.direcc cliDirecc, ivaven.locali cliLocali," +
                    " ivaven.cuit cliCuit, ivaven.remito cliRemito, ivaven.condicion cliCondic, ivaven.nrocli, vende.nombre" +
                    " from ivaven" +
                    " left join empresa emp on ivaven.empresaid = emp.id" +
                    " left join localidades locali on locali.id = emp.localidad" +
                    " left join iva on iva.id = emp.condfiva" +
                    " left join tipocomprobante on tipodocid = tipocomprobante.codigo" +
                    " left join cliente on cliente.nrocli  = ivaven.nrocli" +
                    " left join vende on vende.codven = ivaven.codven" +
                    " where ivaven.tipodoc = '" + tipodoc + "' and ivaven.letra = '" + letra + "' and punto = " + punto + " and numero = " + numero + " and emp.id = " + empresa)
                    .ToList();
                detalle = bd.Database.SqlQuery<DetalleFactura>(
                    "select codpro, descri, cant, unimed, precio, bonif1, bonif, ((bonif/100 +1) * (bonif1/100 + 1))-1 bonifTotalArt, bonito, pins pivai, " +
                    "pnoi pivanoi, prexcant + ivartinoi + ivartins importe, ivartinoi, ivartins  from detmovim where ivavenid = " + cab.First().id)
                    .ToList();
            }
            //Si incluye iva en precios
            bool incluyeIvaEnCotizaciones = (bool)config["presinclu"];
            if (!cab.First().discrimina || (tipodoc.Contains("CT") && incluyeIvaEnCotizaciones))
            {
                if (!tipodoc.Contains("CT") || tipodoc.Contains("CT") && incluyeIvaEnCotizaciones)
                {
                    decimal porcenBonif = (100 - cab.First().cabBonitot) / 100;
                    var iva = (decimal)config["ivatasagral"] / 100;
                    cab.First().cabNeto = 0;
                    foreach (var det in detalle)
                    {
                        decimal ivaNuevo = ((det.ivartins + det.ivartinoi) / det.cant);
                        if ((det.bonif1 < 100 && ivaNuevo != 0) || (det.bonif < 100 && ivaNuevo != 0))
                        {
                            ivaNuevo = ivaNuevo / ((100 - det.bonif) / 100); //bonificacion  del art
                            ivaNuevo = ivaNuevo / ((100 - det.bonif1) / 100); //bonificacion 1 del art
                            ivaNuevo = Math.Round(ivaNuevo / porcenBonif, 4);  //bonificacion total de factura
                        }
                        else
                        {
                            ivaNuevo = 0;
                        }
                      
                        det.precio += ivaNuevo; //precio con iva y sin bonificacion
                        det.importe = det.precio * det.cant - (det.precio * det.cant * det.bonifTotalArt) ;
                        det.cant = Math.Round(det.cant, impdec);
                        det.importe = Math.Round(det.importe, impdec);
                        det.precio = Math.Round(det.precio, impdec);
                        cab.First().cabNeto += det.importe;
                    }
                    
                    cab.First().cabSubt = cab.First().cabNeto + cab.First().cabExento;
                    cab.First().cabBonifto = (cab.First().cabNeto * cab.First().cabBonitot) / 100;
                    cab.First().cabTotal = cab.First().cabSubt - cab.First().cabBonifto;
                    cab.First().cabSubt = Math.Round(cab.First().cabSubt, impdec);
                    cab.First().cabTotal = Math.Round(cab.First().cabTotal, impdec);
                   

                    // calcula el nuevo neto con iva incluido
                }
            }
            List<ReportDataSource> cuerpo = new List<ReportDataSource>();
            cuerpo.Add(new ReportDataSource("cabecera", cab));
            cuerpo.Add(new ReportDataSource("detalle", detalle));
            string ruta = "Reportes/Comprobantes/Factura.rdlc";
            if (tipodoc.Contains("CT"))
            {
                ruta = "Reportes/Comprobantes/Cotizacion.rdlc";
            }
            generarReporteComprobante(ruta, cuerpo);
        }
        else if (tipodoc.Contains("RC"))
        {
            List<cabeceraRecibo> cab = new List<cabeceraRecibo>();
            List<DetalleRecibo> detalleRec;
            using (GestionEntities bd = new GestionEntities())
            {
                cab.Add(bd.Database.SqlQuery<cabeceraRecibo>(
                    "select clicta.tipodoc, letra, punto, numero, fecha, empresa.telefono, empresa.email, empresa.cuit, empresa.brutos, empresa.empresa nomEmpresa, empresa.direccion," +
                    " cliente.razsoc cliRazsoc, cliente.direcc cliDirecc, loc.nombre nomLocaliCli, cliente.locali cliLocali, provincias.nombre nombreProvin, iva.descripcion ivaDescri " +
                    " from clicta " +
                    "left join empresa on empresa.id = clicta.empresa " +
                    "left join cliente on cliente.nrocli = clicta.nrocli " +
                    "left join iva on iva.id = cliente.condicion " +
                    "left join localidades loc on loc.id = cliente.locali  " +
                    "left join departamentos on departamentos.id = loc.departamento_id " +
                    "left join provincias on provincias.id = departamentos.provincia_id " +
                    "where clicta.tipodoc = '" + tipodoc + "' and clicta.letra = '" + letra + "' and clicta.punto = " + punto + " and clicta.numero = " + numero + " and clicta.empresa = " + empresa
                    ).First());
                var cabe = cab.First();
                detalleRec = bd.Database.SqlQuery<DetalleRecibo>(
                    "select puntoo punto, tipdoco tipodoc, letrao letra, numeroo numero ,fechafac fecha, haber cobrado, bonif bonificacion, impbonif bonificacion1, " +
                    "cobro totalAplicado " +
                    "from clicta " +
                    "where tipodoc = '" + cabe.tipodoc + "' and letra = '" + cabe.letra + "' and punto = " + cabe.punto + " and numero = " + cabe.numero
                    ).ToList();
            }
            List<ReportDataSource> cuerpo = new List<ReportDataSource>();
            cuerpo.Add(new ReportDataSource("cabecera", cab));
            cuerpo.Add(new ReportDataSource("detalleRecibo", detalleRec));
            string ruta = "Reportes/Comprobantes/Recibo.rdlc";
            generarReporteComprobante(ruta, cuerpo);
            //  List<DetallePago> detallePago;
        }
    }

    private void LStockActual()
    {
        string query = Request.QueryString["query"].ToString();
        DateTime dsd = Convert.ToDateTime(Request.QueryString["dsd"].ToString()),
                hst = Convert.ToDateTime(Request.QueryString["hst"].ToString());
        int precio = Convert.ToInt32(Request.QueryString["precio"].ToString()),
            orden = Convert.ToInt32(Request.QueryString["orden"].ToString());
        bool conIVA = Convert.ToBoolean(Request.QueryString["iva"].ToString());
        decimal tasaIVA;
        List<LStockActua1_Result> lista;
        using (GestionEntities bd = new GestionEntities())
        {
            if (query != "")
                lista = bd.Database.SqlQuery<LStockActua1_Result>("select * from LStockActua(" + empresa + "," + precio + ") where " + query).ToList();
            else
                lista = bd.Database.SqlQuery<LStockActua1_Result>("select * from LStockActua(" + empresa + "," + precio + ")").ToList();
        }
        if (conIVA)
        {
            using (GestionEntities bd = new GestionEntities())
                tasaIVA = bd.configen.Single(e => e.empresa == empresa).ivatasagral;
            foreach (var item in lista)
            {
                item.precio = item.precio + (item.precio * tasaIVA / 100);
            }
        }
        switch (orden)
        {

            case 1:
                lista = lista.OrderBy(a => a.codpro).ToList();
                break;
            case 2:
                lista = lista.OrderBy(a => a.descri).ToList();
                break;
            case 3:
                lista = lista.OrderBy(a => a.saldoActual).ToList();
                break;
            case 4:
                lista = lista.OrderBy(a => a.cantenv).ToList();
                break;
        }
        parametros.Add("tipoPrecio", precio.ToString());
        parametros.Add("dsd", dsd.ToString("dd/MM/yyyy"));
        parametros.Add("hst", hst.ToString("dd/MM/yyyy"));
        generarReporte("Reportes/StockActual/LStockActual.rdlc", parametros, new ReportDataSource("LStockAct", lista), dsd.ToString("dd/MM/yyyy"), hst.ToString("dd/MM/yyyy"));

    }

    private void LStockValorizado()
    {
        string query = Request.QueryString["query"].ToString();
        DateTime dsd = Convert.ToDateTime(Request.QueryString["dsd"].ToString()),
                hst = Convert.ToDateTime(Request.QueryString["hst"].ToString());
        int precio = Convert.ToInt32(Request.QueryString["precio"].ToString()),
            orden = Convert.ToInt32(Request.QueryString["orden"].ToString());
        bool conIVA = Convert.ToBoolean(Request.QueryString["iva"].ToString());
        decimal tasaIVA;
        List<StockValorizado_Result> lista;
        using (GestionEntities bd = new GestionEntities())
        {
            if (query != "")
                lista = bd.Database.SqlQuery<StockValorizado_Result>("select * from StockValorizado(" + empresa + "," + precio + ") where " + query).ToList();
            else
                lista = bd.Database.SqlQuery<StockValorizado_Result>("select * from StockValorizado(" + empresa + "," + precio + ")").ToList();
        }
        if (conIVA)
        {
            using (GestionEntities bd = new GestionEntities())
                tasaIVA = bd.configen.Single(e => e.empresa == empresa).ivatasagral;
            foreach (var item in lista)
            {
                item.precio = item.precio + (item.precio * tasaIVA / 100);
            }
        }// agrega tasaGral de iva a cada item 

        if (orden == 1)
            lista = lista.OrderBy(a => a.codpro).ToList();
        else
            lista = lista.OrderBy(a => a.descri).ToList();

        parametros.Add("precio", precio.ToString());
        generarReporte("Reportes/StockValorizado/LStockValorizado.rdlc", parametros, new ReportDataSource("StockValorizado", lista), dsd.ToString("dd/MM/yyyy"), hst.ToString("dd/MM/yyyy"));
    }
    private void LCotizacionesMesAMes()
    {
        string query = Request.QueryString["query"].ToString(),
            agrupar = Request.QueryString["agrupar"].ToString();
        int mes = Convert.ToInt32(Request.QueryString["mes"].ToString());
        List<Cotimesxmes> lista;
        DateTime pdsd = new DateTime(DateTime.Now.Year - 1, mes, DateTime.Now.Day);
        DateTime phst = new DateTime(DateTime.Now.Year, mes, DateTime.Now.Day);
        string mes1 = "", mes2 = "", mes3 = "", mes4 = "", mes5 = "", mes6 = "",
            mes7 = "", mes8 = "", mes9 = "", mes10 = "", mes11 = "", mes12 = "";
        switch (mes)
        {
            case 1:
                mes2 = "Febrero";
                mes1 = "Enero";
                mes3 = "Marzo";
                mes4 = "Abril";
                mes5 = "Mayo";
                mes6 = "Junio";
                mes7 = "Julio";
                mes8 = "Agosto";
                mes9 = "Septiembre";
                mes10 = "Octubre";
                mes11 = "Noviembre";
                mes12 = "Diciembre";
                break;
            case 2:
                mes12 = "Enero";
                mes1 = "Febrero";
                mes2 = "Marzo";
                mes3 = "Abril";
                mes4 = "Mayo";
                mes5 = "Junio";
                mes6 = "Julio";
                mes7 = "Agosto";
                mes8 = "Septiembre";
                mes9 = "Octubre";
                mes10 = "Noviembre";
                mes11 = "Diciembre";
                break;
            case 3:
                mes11 = "Enero";
                mes12 = "Febrero";
                mes1 = "Marzo";
                mes2 = "Abril";
                mes3 = "Mayo";
                mes4 = "Junio";
                mes5 = "Julio";
                mes6 = "Agosto";
                mes7 = "Septiembre";
                mes8 = "Octubre";
                mes9 = "Noviembre";
                mes10 = "Diciembre";
                break;
            case 4:
                mes10 = "Enero";
                mes11 = "Febrero";
                mes12 = "Marzo";
                mes1 = "Abril";
                mes2 = "Mayo";
                mes3 = "Junio";
                mes4 = "Julio";
                mes5 = "Agosto";
                mes6 = "Septiembre";
                mes7 = "Octubre";
                mes8 = "Noviembre";
                mes9 = "Diciembre";
                break;
            case 5:
                mes9 = "Enero";
                mes10 = "Febrero";
                mes11 = "Marzo";
                mes12 = "Abril";
                mes1 = "Mayo";
                mes2 = "Junio";
                mes3 = "Julio";
                mes4 = "Agosto";
                mes5 = "Septiembre";
                mes6 = "Octubre";
                mes7 = "Noviembre";
                mes8 = "Diciembre";
                break;
            case 6:
                mes8 = "Enero";
                mes9 = "Febrero";
                mes10 = "Marzo";
                mes11 = "Abril";
                mes12 = "Mayo";
                mes1 = "Junio";
                mes2 = "Julio";
                mes3 = "Agosto";
                mes4 = "Septiembre";
                mes5 = "Octubre";
                mes6 = "Noviembre";
                mes7 = "Diciembre";
                break;
            case 7:
                mes7 = "Enero";
                mes8 = "Febrero";
                mes9 = "Marzo";
                mes10 = "Abril";
                mes11 = "Mayo";
                mes12 = "Junio";
                mes1 = "Julio";
                mes2 = "Agosto";
                mes3 = "Septiembre";
                mes4 = "Octubre";
                mes5 = "Noviembre";
                mes6 = "Diciembre";
                break;
            case 8:
                mes6 = "Enero";
                mes7 = "Febrero";
                mes8 = "Marzo";
                mes9 = "Abril";
                mes10 = "Mayo";
                mes11 = "Junio";
                mes12 = "Julio";
                mes1 = "Agosto";
                mes2 = "Septiembre";
                mes3 = "Octubre";
                mes4 = "Noviembre";
                mes5 = "Diciembre";
                break;
            case 9:
                mes5 = "Enero";
                mes6 = "Febrero";
                mes7 = "Marzo";
                mes8 = "Abril";
                mes9 = "Mayo";
                mes10 = "Junio";
                mes11 = "Julio";
                mes12 = "Agosto";
                mes1 = "Septiembre";
                mes2 = "Octubre";
                mes3 = "Noviembre";
                mes4 = "Diciembre";
                break;
            case 10:
                mes4 = "Enero";
                mes5 = "Febrero";
                mes6 = "Marzo";
                mes7 = "Abril";
                mes8 = "Mayo";
                mes9 = "Junio";
                mes10 = "Julio";
                mes11 = "Agosto";
                mes12 = "Septiembre";
                mes1 = "Octubre";
                mes2 = "Noviembre";
                mes3 = "Diciembre";
                break;
            case 11:
                mes3 = "Enero";
                mes4 = "Febrero";
                mes5 = "Marzo";
                mes6 = "Abril";
                mes7 = "Mayo";
                mes8 = "Junio";
                mes9 = "Julio";
                mes10 = "Agosto";
                mes11 = "Septiembre";
                mes12 = "Octubre";
                mes1 = "Noviembre";
                mes2 = "Diciembre";
                break;
            case 12:
                mes2 = "Enero";
                mes3 = "Febrero";
                mes4 = "Marzo";
                mes5 = "Abril";
                mes6 = "Mayo";
                mes7 = "Junio";
                mes8 = "Julio";
                mes9 = "Agosto";
                mes10 = "Septiembre";
                mes11 = "Octubre";
                mes12 = "Noviembre";
                mes1 = "Diciembre";
                break;


        }
        using (GestionEntities bd = new GestionEntities())
            lista = bd.Database.SqlQuery<Cotimesxmes>("exec sp_cotimesamesmodificado " + mes + "," + empresa + ", '" + pdsd.ToString("dd/MM/yyyy") + "', '" + phst.ToString("dd/MM/yyyy") + "','" + query + "'").ToList();
        if (agrupar == "1")
        {
            rutaReporte = "Reportes/CotizacionesMesAMes/LCotizacionesMesAMesArticulos.rdlc";
        }
        else
        {
            rutaReporte = "Reportes/CotizacionesMesAMes/LCotizacionesMesAMesClientes.rdlc";
        }
        parametros.Add("mes1", mes1);
        parametros.Add("mes2", mes2);
        parametros.Add("mes3", mes3);
        parametros.Add("mes4", mes4);
        parametros.Add("mes5", mes5);
        parametros.Add("mes6", mes6);
        parametros.Add("mes7", mes7);
        parametros.Add("mes8", mes8);
        parametros.Add("mes9", mes9);
        parametros.Add("mes10", mes10);
        parametros.Add("mes11", mes11);
        parametros.Add("mes12", mes12);
        parametros.Add("mesInicio", mes.ToString());
        generarReporte(rutaReporte, parametros, new ReportDataSource("lCotiMesxMes", lista), pdsd.ToString("dd/MM/yyyy"), phst.ToString("dd/MM/yyyy"));
    }
    private void LVentasMesAMes()//Ventas mes a mes en importes
    {
        string query = Request.QueryString["query"].ToString(),
            agrupar = Request.QueryString["agrupar"].ToString();
        int mes = Convert.ToInt32(Request.QueryString["mes"].ToString());
        List<Cotimesxmes> lista;
        DateTime pdsd = new DateTime(DateTime.Now.Year - 1, mes, DateTime.Now.Day);
        DateTime phst = new DateTime(DateTime.Now.Year, mes, DateTime.Now.Day);
        string mes1 = "", mes2 = "", mes3 = "", mes4 = "", mes5 = "", mes6 = "",
           mes7 = "", mes8 = "", mes9 = "", mes10 = "", mes11 = "", mes12 = "";
        switch (mes)
        {
            case 1:
                mes2 = "Febrero";
                mes1 = "Enero";
                mes3 = "Marzo";
                mes4 = "Abril";
                mes5 = "Mayo";
                mes6 = "Junio";
                mes7 = "Julio";
                mes8 = "Agosto";
                mes9 = "Septiembre";
                mes10 = "Octubre";
                mes11 = "Noviembre";
                mes12 = "Diciembre";
                break;
            case 2:
                mes12 = "Enero";
                mes1 = "Febrero";
                mes2 = "Marzo";
                mes3 = "Abril";
                mes4 = "Mayo";
                mes5 = "Junio";
                mes6 = "Julio";
                mes7 = "Agosto";
                mes8 = "Septiembre";
                mes9 = "Octubre";
                mes10 = "Noviembre";
                mes11 = "Diciembre";
                break;
            case 3:
                mes11 = "Enero";
                mes12 = "Febrero";
                mes1 = "Marzo";
                mes2 = "Abril";
                mes3 = "Mayo";
                mes4 = "Junio";
                mes5 = "Julio";
                mes6 = "Agosto";
                mes7 = "Septiembre";
                mes8 = "Octubre";
                mes9 = "Noviembre";
                mes10 = "Diciembre";
                break;
            case 4:
                mes10 = "Enero";
                mes11 = "Febrero";
                mes12 = "Marzo";
                mes1 = "Abril";
                mes2 = "Mayo";
                mes3 = "Junio";
                mes4 = "Julio";
                mes5 = "Agosto";
                mes6 = "Septiembre";
                mes7 = "Octubre";
                mes8 = "Noviembre";
                mes9 = "Diciembre";
                break;
            case 5:
                mes9 = "Enero";
                mes10 = "Febrero";
                mes11 = "Marzo";
                mes12 = "Abril";
                mes1 = "Mayo";
                mes2 = "Junio";
                mes3 = "Julio";
                mes4 = "Agosto";
                mes5 = "Septiembre";
                mes6 = "Octubre";
                mes7 = "Noviembre";
                mes8 = "Diciembre";
                break;
            case 6:
                mes8 = "Enero";
                mes9 = "Febrero";
                mes10 = "Marzo";
                mes11 = "Abril";
                mes12 = "Mayo";
                mes1 = "Junio";
                mes2 = "Julio";
                mes3 = "Agosto";
                mes4 = "Septiembre";
                mes5 = "Octubre";
                mes6 = "Noviembre";
                mes7 = "Diciembre";
                break;
            case 7:
                mes7 = "Enero";
                mes8 = "Febrero";
                mes9 = "Marzo";
                mes10 = "Abril";
                mes11 = "Mayo";
                mes12 = "Junio";
                mes1 = "Julio";
                mes2 = "Agosto";
                mes3 = "Septiembre";
                mes4 = "Octubre";
                mes5 = "Noviembre";
                mes6 = "Diciembre";
                break;
            case 8:
                mes6 = "Enero";
                mes7 = "Febrero";
                mes8 = "Marzo";
                mes9 = "Abril";
                mes10 = "Mayo";
                mes11 = "Junio";
                mes12 = "Julio";
                mes1 = "Agosto";
                mes2 = "Septiembre";
                mes3 = "Octubre";
                mes4 = "Noviembre";
                mes5 = "Diciembre";
                break;
            case 9:
                mes5 = "Enero";
                mes6 = "Febrero";
                mes7 = "Marzo";
                mes8 = "Abril";
                mes9 = "Mayo";
                mes10 = "Junio";
                mes11 = "Julio";
                mes12 = "Agosto";
                mes1 = "Septiembre";
                mes2 = "Octubre";
                mes3 = "Noviembre";
                mes4 = "Diciembre";
                break;
            case 10:
                mes4 = "Enero";
                mes5 = "Febrero";
                mes6 = "Marzo";
                mes7 = "Abril";
                mes8 = "Mayo";
                mes9 = "Junio";
                mes10 = "Julio";
                mes11 = "Agosto";
                mes12 = "Septiembre";
                mes1 = "Octubre";
                mes2 = "Noviembre";
                mes3 = "Diciembre";
                break;
            case 11:
                mes3 = "Enero";
                mes4 = "Febrero";
                mes5 = "Marzo";
                mes6 = "Abril";
                mes7 = "Mayo";
                mes8 = "Junio";
                mes9 = "Julio";
                mes10 = "Agosto";
                mes11 = "Septiembre";
                mes12 = "Octubre";
                mes1 = "Noviembre";
                mes2 = "Diciembre";
                break;
            case 12:
                mes2 = "Enero";
                mes3 = "Febrero";
                mes4 = "Marzo";
                mes5 = "Abril";
                mes6 = "Mayo";
                mes7 = "Junio";
                mes8 = "Julio";
                mes9 = "Agosto";
                mes10 = "Septiembre";
                mes11 = "Octubre";
                mes12 = "Noviembre";
                mes1 = "Diciembre";
                break;


        }
        using (GestionEntities bd = new GestionEntities())
            lista = bd.Database.SqlQuery<Cotimesxmes>("exec sp_cotimesamesmodificado " + mes + "," + empresa + ", '" + pdsd.ToString("dd/MM/yyyy") + "', '" + phst.ToString("dd/MM/yyyy") + "'," + query).ToList();
        if (agrupar == "1")
        {
            rutaReporte = "Reportes/CotizacionesMesAMes/LCotizacionesMesAMesArticulos.rdlc";
        }
        else
        {
            rutaReporte = "Reportes/CotizacionesMesAMes/LCotizacionesMesAMesCliente.rdlc";
        }
        using (GestionEntities bd = new GestionEntities())
            lista = bd.Database.SqlQuery<Cotimesxmes>("exec SP_VentasMesAMes " + mes + "," + empresa + ", '" + pdsd.ToString("dd/MM/yyyy") + "', '" + phst.ToString("dd/MM/yyyy") + "'," + query).ToList();
        if (agrupar == "1")
        {
            rutaReporte = "Reportes/CotizacionesMesAMes/LCotizacionesMesAMesArticulos.rdlc";
        }
        else
        {
            rutaReporte = "Reportes/CotizacionesMesAMes/LCotizacionesMesAMesClientes.rdlc";
        }
        parametros.Add("mes1", mes1);
        parametros.Add("mes2", mes2);
        parametros.Add("mes3", mes3);
        parametros.Add("mes4", mes4);
        parametros.Add("mes5", mes5);
        parametros.Add("mes6", mes6);
        parametros.Add("mes7", mes7);
        parametros.Add("mes8", mes8);
        parametros.Add("mes9", mes9);
        parametros.Add("mes10", mes10);
        parametros.Add("mes11", mes11);
        parametros.Add("mes12", mes12);
        parametros.Add("mesInicio", mes.ToString());
        generarReporte(rutaReporte, parametros, new ReportDataSource("lCotiMesxMes", lista), pdsd.ToString("dd/MM/yyyy"), phst.ToString("dd/MM/yyyy"));
    }

    private void LPuntosdePedido()
    {
        string orden = Request.QueryString["orden"].ToString(),
                query = Request.QueryString["query"].ToString();
        bool incluirPedP = Convert.ToBoolean(Request.QueryString["incluirPedP"].ToString());
        DateTime dsd = Convert.ToDateTime(Request.QueryString["dsd"].ToString()),
            hst = Convert.ToDateTime(Request.QueryString["hst"].ToString());
        int incluir = 0;
        if (incluirPedP)
            incluir = 1;
        List<FlistPuntoPedido_Result> lista;
        using (GestionEntities bd = new GestionEntities())
            lista = bd.Database.SqlQuery<FlistPuntoPedido_Result>("exec sp_PuntoPedido " + incluir + ", " + empresa + ", '" + dsd.ToString("dd/MM/yyyy") + "', '" + hst.ToString("dd/MM/yyyy") + "'").ToList();
        lista = lista.Where(query).ToList();
        if (orden == "1")
            lista = lista.OrderBy(a => a.codpro).ToList();
        else
            lista = lista.OrderBy(a => a.descri).ToList();
        
        generarReporte("Reportes/PuntosDePedido/LPuntoPedido.rdlc", parametros, new ReportDataSource("PuntosPedido", lista), dsd.ToString("dd/MM/yyyy"), hst.ToString("dd/MM/yyyy"));
    }

    private void LRefCliente()
    {
        string orden = Request.QueryString["orden"].ToString();
        bool desc = Convert.ToBoolean(Request.QueryString["desc"].ToString());
        DateTime dsd = Convert.ToDateTime(Request.QueryString["dsd"].ToString()),
            hst = Convert.ToDateTime(Request.QueryString["hst"].ToString());
        List<RWRefCliente> lista;
        using (GestionEntities bd = new GestionEntities())
        {
            lista = bd.RWRefCliente.ToList();
        }

        if (orden == "1")
        {
            if (desc)
                lista = lista.OrderByDescending(a => a.nrocli).ToList();
            else
                lista = lista.OrderBy(a => a.nrocli).ToList();
        }
        else
        {
            if (desc)
                lista = lista.OrderByDescending(a => a.razsoc).ToList();
            else
                lista = lista.OrderBy(a => a.razsoc).ToList();
        }
        generarReporte("reportes/RefClientes/lRefCliente.rdlc", parametros, new ReportDataSource("RWRefCliente", lista), dsd.ToString("dd/MM/yyyy"), hst.ToString("dd/MM/yyyy"));
    }
    private void LProveedores()
    {
        string orden = Request.QueryString["orden"].ToString();
        bool desc = Convert.ToBoolean(Request.QueryString["desc"].ToString());
        DateTime dsd = Convert.ToDateTime(Request.QueryString["dsd"].ToString()),
            hst = Convert.ToDateTime(Request.QueryString["hst"].ToString());
        List<provee> lista;
        using (GestionEntities bd = new GestionEntities())
        {
            lista = bd.provee.ToList();
        }
        if (!desc)
        {
            if (orden == "1")
            {
                lista = lista.OrderBy(a => a.nropro).ToList();
            }
            else if (orden == "2")
            {
                lista = lista.OrderBy(a => a.razsoc).ToList();
            }
        }
        else
        {
            if (orden == "1")
            {
                lista = lista.OrderByDescending(a => a.nropro).ToList();
            }
            else if (orden == "2")
            {
                lista = lista.OrderByDescending(a => a.razsoc).ToList();
            }
        }
        generarReporte("Reportes/Proveedores/LProveedores.rdlc", parametros, new ReportDataSource("LProveedores", lista),
            dsd.ToString("dd/MM/yyyy"), hst.ToString("dd/MM/yyyy"));
    }
    private void LTransportistas()
    {

        string orden = Request.QueryString["orden"].ToString();
        bool desc = Convert.ToBoolean(Request.QueryString["desc"].ToString());
        DateTime dsd = Convert.ToDateTime(Request.QueryString["dsd"].ToString()),
            hst = Convert.ToDateTime(Request.QueryString["hst"].ToString());
        List<transpo> lista;
        using (GestionEntities bd = new GestionEntities())
        {
            lista = bd.transpo.ToList();
        }
        if (!desc)
        {
            if (orden == "1")
            {
                lista = lista.OrderBy(a => a.codigo).ToList();
            }
            else if (orden == "2")
            {
                lista = lista.OrderBy(a => a.razsoc).ToList();
            }
        }
        else
        {
            if (orden == "1")
            {
                lista = lista.OrderByDescending(a => a.codigo).ToList();
            }
            else if (orden == "2")
            {
                lista = lista.OrderByDescending(a => a.razsoc).ToList();
            }
        }
        generarReporte("Reportes/Transportistas/LTransportistas.rdlc", parametros, new ReportDataSource("LTransportistas", lista), dsd.ToString("dd/MM/yyyy"), hst.ToString("dd/MM/yyyy"));
    }
    private void LZonas()
    {

        string orden = Request.QueryString["orden"].ToString();
        bool desc = Convert.ToBoolean(Request.QueryString["desc"].ToString());
        DateTime dsd = Convert.ToDateTime(Request.QueryString["dsd"].ToString()),
            hst = Convert.ToDateTime(Request.QueryString["hst"].ToString());
        List<zonas> lista;
        using (GestionEntities bd = new GestionEntities())
        {
            lista = bd.zonas.ToList();
        }
        if (!desc)
        {
            if (orden == "1")
            {
                lista = lista.OrderBy(a => a.codigo).ToList();
            }
            else if (orden == "2")
            {
                lista = lista.OrderBy(a => a.descri).ToList();
            }
        }
        else
        {
            if (orden == "1")
            {
                lista = lista.OrderByDescending(a => a.codigo).ToList();
            }
            else if (orden == "2")
            {
                lista = lista.OrderByDescending(a => a.descri).ToList();
            }
        }
        generarReporte("Reportes/Zonas/LZonas.rdlc", parametros, new ReportDataSource("Lzonas", lista), dsd.ToString("dd/MM/yyyy"), hst.ToString("dd/MM/yyyy"));
    }

    private void LRubros()
    {

        string orden = Request.QueryString["orden"].ToString();
        bool desc = Convert.ToBoolean(Request.QueryString["desc"].ToString());
        DateTime dsd = Convert.ToDateTime(Request.QueryString["dsd"].ToString()),
            hst = Convert.ToDateTime(Request.QueryString["hst"].ToString());
        List<rubros> lista;
        using (GestionEntities bd = new GestionEntities())
        {
            lista = bd.rubros.ToList();
        }
        if (!desc)
        {
            if (orden == "1")
            {
                lista = lista.OrderBy(a => a.codigo).ToList();
            }
            else if (orden == "2")
            {
                lista = lista.OrderBy(a => a.descri).ToList();
            }
        }
        else
        {
            if (orden == "1")
            {
                lista = lista.OrderByDescending(a => a.codigo).ToList();
            }
            else if (orden == "2")
            {
                lista = lista.OrderByDescending(a => a.descri).ToList();
            }
        }
        generarReporte("Reportes/Rubros/LRubros.rdlc", parametros, new ReportDataSource("LRubros", lista), dsd.ToString("dd/MM/yyyy"), hst.ToString("dd/MM/yyyy"));
    }
    private void LSubrubros() {

        string orden = Request.QueryString["orden"].ToString();
        bool desc = Convert.ToBoolean(Request.QueryString["desc"].ToString());
        DateTime dsd = Convert.ToDateTime(Request.QueryString["dsd"].ToString()),
            hst = Convert.ToDateTime(Request.QueryString["hst"].ToString());
        List<subrub> lista;
        using (GestionEntities bd = new GestionEntities())
        {
            lista = bd.subrub.ToList();
        }
        if (!desc)
        {
            if (orden == "1")
            {
                lista = lista.OrderBy(a => a.codigo).ToList();
            }
            else if (orden == "2")
            {
                lista = lista.OrderBy(a => a.descri).ToList();
            }
        }
        else
        {
            if (orden == "1")
            {
                lista = lista.OrderByDescending(a => a.codigo).ToList();
            }
            else if (orden == "2")
            {
                lista = lista.OrderByDescending(a => a.descri).ToList();
            }
        }
        generarReporte("Reportes/SubRubros/LSubRubros.rdlc", parametros, new ReportDataSource("LSubRubros", lista), dsd.ToString("dd/MM/yyyy"), hst.ToString("dd/MM/yyyy"));
    }


    private void LVendedores()
    {
        int orden = Convert.ToInt32(Request.QueryString["orden"].ToString());
        bool desc = Convert.ToBoolean(Request.QueryString["desc"].ToString()),
            soloSaldo = Convert.ToBoolean(Request.QueryString["soloSaldo"].ToString());
        List<vVendedores> lista;
        using (GestionEntities bd = new GestionEntities())
            lista = bd.vVendedores.ToList();
        if (orden == 1)
        {
            if (desc)
                lista = lista.OrderByDescending(a => a.codven).ToList();
            else
                lista = lista.OrderBy(a => a.codven).ToList();
        }
        else
        {
            if (desc)
                lista = lista.OrderByDescending(a => a.VendeNom).ToList();
            else
                lista = lista.OrderBy(a => a.VendeNom).ToList();
        }
        string rutaReporte;
        if (soloSaldo)
            rutaReporte = "Reportes/Vendedores/LVendedoresSaldo.rdlc";
        else
            rutaReporte = "Reportes/Vendedores/LVendedores.rdlc";

        generarReporte(rutaReporte, parametros, new ReportDataSource("LVendedores", lista), Request.QueryString["dsd"].ToString(), Request.QueryString["hst"].ToString());
    }

    private void LRemitos()
    {
        string rutaReporte, query = Request.QueryString["query"].ToString();
        bool detalle = Convert.ToBoolean(Request.QueryString["detalle"].ToString());
        List<VRemitos> listac;
        List<VRemDet> listad;

        if (detalle)
        {
            using (GestionEntities bd = new GestionEntities())
            {
                listad = bd.Database.SqlQuery<VRemDet>(
                    "SELECT CONVERT(date, dbo.RemitoCab.fecha) AS fecha, dbo.RemitoCab.letra, dbo.RemitoCab.empresaid, dbo.RemitoDet.cabeceraid, dbo.RemitoCab.numero," +
                    " dbo.RemitoDet.cant, dbo.RemitoDet.precio, dbo.RemitoDet.codpro, dbo.RemitoDet.descri, dbo.RemitoDet.prexcant, dbo.cliente.razsoc, " +
                    "dbo.RemitoCab.facturado "                              +
                    "from RemitoDet "                                       +
                    "left join "                                            +
                    "RemitoCab ON RemitoDet.cabeceraid = dbo.RemitoCab.id " +
                    "left join "                                            +
                    "cliente ON cliente.nrocli = RemitoCab.nrocli "         +
                    "where " + query
                    ).ToList();
            }
            rutaReporte = "ReporteS/RemitosEmitidos/LRemitosDet.rdlc";
            generarReporte(rutaReporte, parametros, new ReportDataSource("LRemitosDet", listad), Request.QueryString["dsd"].ToString(), Request.QueryString["hst"].ToString());
        }
        else
        {
            using (GestionEntities bd = new GestionEntities())
            {
                listac = bd.Database.SqlQuery<VRemitos>("" +
                    "SELECT cliente.razsoc, RemitoCab.subtot, RemitoCab.bonitot, RemitoCab.bonifto, RemitoCab.neto, CONVERT(date, dbo.RemitoCab.fecha) AS fecha," +
                    "RemitoCab.letra, RemitoCab.empresaid, RemitoCab.numero, RemitoCab.facturado " +
                    "FROM  RemitoCab " +
                    "left Join cliente ON cliente.nrocli = RemitoCab.nrocli " +
                    "where "+ query
                    ).ToList();
            }
            rutaReporte = "ReporteS/RemitosEmitidos/LRemitos.rdlc";
            generarReporte(rutaReporte, parametros, new ReportDataSource("LRemitos", listac), Request.QueryString["dsd"].ToString(), Request.QueryString["hst"].ToString());
        }
    }
    private void LSaldoProveedores()
    {
        string query = Request.QueryString["query"].ToString(),
            orden = Request.QueryString["orden"].ToString();
        DateTime dsd = Convert.ToDateTime(Request.QueryString["dsd"].ToString()),
                hst = Convert.ToDateTime(Request.QueryString["hst"].ToString());
        List<SaldoProveedoresAllGroup_Result> lista;
        using (GestionEntities bd = new GestionEntities())
        {
            if (query != "")
                lista = bd.SaldoProveedoresAllGroup(empresa).Where(query).ToList();
            else
                lista = bd.SaldoProveedoresAllGroup(empresa).ToList();
        }
        if (orden == "nropro")
        {
            lista = lista.OrderBy(a => a.nropro).ToList();
        }
        else
        {
            lista = lista.OrderBy(a => a.razsoc).ToList();
        }


        generarReporte("Reportes/SaldoProveedores/LSaldoProveedores.rdlc", parametros, new ReportDataSource("LSaldoProveedores", lista), dsd.ToString("dd/MM/yyyy"), hst.ToString("dd/MM/yyyy"));

    }
    private void LSaldoCliente()
    {
        string query = Request.QueryString["query"].ToString();
        DateTime dsd = Convert.ToDateTime(Request.QueryString["dsd"].ToString()),
                hst = Convert.ToDateTime(Request.QueryString["hst"].ToString());
        List<LsaldoCli_Result> lista;
        using (GestionEntities bd = new GestionEntities())
        {
            if (query != "")
                lista = bd.LsaldoCli(empresa, dsd, hst).Where(query).ToList();
            else
                lista = bd.LsaldoCli(empresa, dsd, hst).ToList();
        }

        generarReporte("Reportes/SaldoCliente/LSaldoCliente.rdlc", parametros, new ReportDataSource("LSaldoCliente", lista), dsd.ToString("dd/MM/yyyy"), hst.ToString("dd/MM/yyyy"));

    }
    private void LPresup()
    {
        string query = Request.QueryString["query"].ToString();
        DateTime dsd = Convert.ToDateTime(Request.QueryString["dsd"].ToString()),
        hst = Convert.ToDateTime(Request.QueryString["hst"].ToString());
        List<LWPresup_Result> lista;
        using (GestionEntities bd = new GestionEntities())
        {
            if (query != "")
                lista = bd.LWPresup(dsd, hst, empresa).Where(query).ToList();
            else
                lista = bd.LWPresup(dsd, hst, empresa).ToList();

        }
        generarReporte("Reportes/Presupuestos/LPresup.rdlc", parametros, new ReportDataSource("FWPresup", lista), dsd.ToString("dd/MM/yyyy"), hst.ToString("dd/MM/yyyy"));

    }
    private void LArtVendidosPorVende()
    {
        DateTime dsd = Convert.ToDateTime(Request.QueryString["dsd"].ToString()),
            hst = Convert.ToDateTime(Request.QueryString["hst"].ToString());
        int agrupar = Convert.ToInt32(Request.QueryString["agrupar"].ToString());
        List<LArtVendxVende> lista;
        bool cotizaciones = Convert.ToBoolean(Request.QueryString["cotizaciones"].ToString()),
            ventas = Convert.ToBoolean(Request.QueryString["ventas"].ToString()),
            remitos = Convert.ToBoolean(Request.QueryString["remitos"].ToString()),
            pedidos = Convert.ToBoolean(Request.QueryString["pedidos"].ToString()); 

        string query = Request.QueryString["query"].ToString(),
            qRemitos, qVentas, qPedidos, rutaReporte;
        string consulta = "";

        qVentas = 
            "SELECT stock.codpro, stock.descri, CONVERT(int, x.cant) AS unidades, CONVERT(int, x.cant * stock.cantenv) AS cantidad, x.prexcant*(100-x.bonito)/100 precio, " +
            "stock.costo, convert(decimal,-2)  AS nrofac, convert(numeric,- 1) AS pendientes, x.fecha AS fecha, stock.rubro codRub, r.descri descriRub, stock.subrub codsubrub, s.descri descriSubRu, i.codven," +
            " x.nrocli, x.tipodoc AS tipodoc, x.cotizacion, ven.nombre nomvende, c.razsoc, x.prexcant, stock.marca, c.zona, stock.proveed " +
            "FROM detmovim x " +
            "LEFT JOIN  stock ON x.codpro = stock.codpro " +
            "LEFT JOIN ivaven i ON i.id = x.ivavenid " +
            "LEFT JOIN rubros r ON r.codigo = stock.rubro " +
            "LEFT JOIN subrub s ON s.codigo = stock.subrub " +
            "LEFT JOIN vende ven on i.codven = ven.codven " +
            "LEFT JOIN stock p on p.codpro = x.codpro " +
            "LEFT JOIN cliente c on c.nrocli = x.nrocli " +
            "WHERE (x.fecha >= '" + dsd.ToString("dd/MM/yyyy") + "' and x.fecha <= '" + hst.ToString("dd/MM/yyyy") + "') and empresa = " + empresa + " and stock.codpro is not NULL";
        if (!cotizaciones)
            qVentas += " and x.tipodoc not in ('CT','AJD','AJC') ";

        qRemitos =
            "SELECT stock.codpro, stock.descri, CONVERT(int, x.cant) AS unidades, CONVERT(int, x.cant * stock.cantenv) AS cantidad,x.prexcant*(100-y.bonitot)/100 precio, stock.costo," +
            " x.nrofac ,convert(numeric,- 1) pendientes, y.fecha, stock.rubro codRub, r.descri descriRub, stock.subrub codsubrub, s.descri descriSubRu, y.codven, y.nrocli, 'RTO' AS tipodoc," +
            "1 cotizacion, ven.nombre nomvende, c.razsoc, x.prexcant, stock.marca, c.zona, stock.proveed " +
            "FROM remitodet x " +
            "LEFT JOIN stock ON x.codpro = stock.codpro " +
            "LEFT JOIN remitocab y ON y.id = x.cabeceraid " +
            "LEFT JOIN rubros r ON r.codigo = stock.rubro " +
            "LEFT JOIN subrub s ON s.codigo = stock.subrub " +
            "LEFT JOIN vende ven on y.codven = ven.codven " +
            "LEFT JOIN stock p on p.codpro = x.codpro " +
            "LEFT JOIN cliente c on c.nrocli = y.nrocli " +
            "WHERE (y.fecha >=  '" + dsd.ToString("dd/MM/yyyy") + "' and y.fecha <=  '" + hst.ToString("dd/MM/yyyy") + "') and empresaid = " + empresa + " and stock.codpro is not NULL and y.facturado = 'N'";

        qPedidos =
            "SELECT stock.codpro, stock.descri, CONVERT(int, x.cantidad) AS unidades, CONVERT(int, x.cantidad * stock.cantenv) AS cantidad, " +
            "(x.precio*cantidad)*(100-x.precio*x.bonif)/100*(100-x.precio*x.bonif1)/100*(100-x.precio*y.bonifto)/100 precio, stock.costo, convert(decimal,-1) as nrofac, " +
            "x.pendientes AS pendientes, y.[fechaing] AS fecha, stock.rubro codRub, r.descri descriRub, stock.subrub codsubrub, s.descri descriSubRu, y.codven, " +
            "y.nrocli, 'PE' AS tipodoc, mon.ncotiza cotizacion, ven.nombre nomvende, c.razsoc, x.precio* cantidad-bonitot prexcant, stock.marca, c.zona, stock.proveed " +
            "FROM PedidoDet x " +
            "LEFT JOIN stock ON x.articulo = stock.codpro " +
            "LEFT JOIN Pedidocab y ON y.id = x.cabeceraid " +
            "LEFT JOIN rubros r ON r.codigo = stock.rubro " +
            "LEFT JOIN subrub s ON s.codigo = stock.subrub " +
            "LEFT JOIN vende ven on y.codven = ven.codven " +
            "LEFT JOIN monedas mon on x.moneda = mon.codigo " +
            "LEFT JOIN stock p on p.codpro = x.articulo " +
            "LEFT JOIN cliente c on c.nrocli = y.nrocli " +
            "LEFT JOIN (select cabeceraid, sum(pendientes) pendientes from pedidodet group by cabeceraid) pend on y.id = pend.cabeceraid " +
            "WHERE (y.fechaing >=  '" + dsd.ToString("dd/MM/yyyy") + "' and y.fechaing <=  '" + hst.ToString("dd/MM/yyyy") + "') and empresaid = " + empresa + "" +
            " and stock.codpro is not NULL and pend.pendientes > 0";

        if (ventas)
            consulta += qVentas;

        if (remitos) {
            if (ventas)
                consulta += " UNION ";
            consulta += qRemitos;
        }

        if (pedidos)
        {
            if (remitos || ventas)
                consulta += " UNION ";
            consulta += qPedidos;
        }

        consulta = "select * from (" + consulta + " ) consulta where 1 = 1" + query;

        using (GestionEntities bd = new GestionEntities())
        {
            lista = bd.Database.SqlQuery<LArtVendxVende>(
                consulta
               ).ToList();
        }

        switch (agrupar)
        {
            case 1:
                rutaReporte = "Reportes/ArtVendPorVende/LArtVenxVendeRubSub.rdlc";
                break;
            case 2:
                rutaReporte = "Reportes/ArtVendPorVende/LArtVenxVendeRubroDet.rdlc";
                break;
            case 3:
                rutaReporte = "Reportes/ArtVendPorVende/LArtVenxVendeRubroAgrup.rdlc";
                break;
            case 4:
                rutaReporte = "Reportes/ArtVendPorVende/LArtVenxVendeCli.rdlc";
                break;
            default:
                rutaReporte = "";
                break;
        }

        generarReporte(rutaReporte, parametros, new ReportDataSource("FWArtVendidosxVende", lista), dsd.ToString("dd/MM/yyyy"), hst.ToString("dd/MM/yyyy"));
    }
    private void LOperacionesPorRubro()
    {
        bool Eventas = Convert.ToBoolean(Request.QueryString["ventas"]),
            Epedidos = Convert.ToBoolean(Request.QueryString["pedidos"]),
            Eremitos = Convert.ToBoolean(Request.QueryString["remitos"]),
            Ecotizaciones = Convert.ToBoolean(Request.QueryString["cotizaciones"]),
            esVenta = Convert.ToBoolean(Request.QueryString["esVenta"]),
            subDetalle = Convert.ToBoolean(Request.QueryString["subDetalle"]);
        string agrupar = Request.QueryString["agrupar"],
            from = "",
            selectCompleto, rutaReporte;
        query = Request.QueryString["query"];
        string sinCotizaciones = "";
        if(!Ecotizaciones)
            sinCotizaciones = " and i.tipodoc not in ('CT', 'AJC', 'AJD') ";
        string ventas =
            " SELECT stock.codpro, stock.descri,  x.cant unidades, 0 AS porsunid,  x.cant * stock.cantenv cantidad," +
            " 0 AS porscant, x.prexcant * (100-x.bonito)/100 * x.cotizacion precio, x.costo * x.cant * i.cotizacion costo, convert(numeric,- 1) AS nrofac, - 1 AS pendientes, x.fecha AS fecha, " +
            "stock.rubro codRub, r.descri descriRub, stock.subrub codSubRub, s.descri descriSubRu, stock.tipoart codTipoArt, t.descri descriTipoArt, stock.proveed," +
            " i.codven, x.nrocli, x.tipodoc AS tipodoc, x.empresa, x.cotizacion " +
            " FROM detmovim x  " +
            " LEFT JOIN stock ON x.codpro = stock.codpro " +
            " LEFT JOIN ivaven i ON i.id = x.ivavenid " +
            " LEFT JOIN rubros r ON r.codigo = stock.rubro " +
            " LEFT JOIN subrub s ON s.codigo = stock.subrub " +
            " LEFT JOIN tipoart t ON t .codigo = stock.tipoart " +
            " where(x.fecha >= '" + dsd + "' and x.fecha <= '" + hst + "') and i.empresaid = " + empresa + sinCotizaciones + "  and stock.codpro is not NULL";
        string remitos = 
            "SELECT stock.codpro, stock.descri, x.cant AS unidades, 0 AS porsunid, x.cant * stock.cantenv AS cantidad, 0 AS porscant," +
            " x.prexcant * (100-y.bonitot) / 100 * x.cotizacion precio, x.costo, x.nrofac, - 1 AS pendientes, y.fecha, stock.rubro codRub, r.descri descriRub, stock.subrub codSubRub," +
            " s.descri descriSubRu, stock.tipoart codTipoArt, t .descri descriTipoArt, stock.proveed, y.codven, y.nrocli, 'RTO' AS tipodoc, y.empresaid empresa, 1 cotizacion " +
            " FROM remitodet x "                                +
            " LEFT JOIN stock ON x.codpro = stock.codpro "      +
            " LEFT JOIN remitocab y ON y.id = x.cabeceraid "    +
            " LEFT JOIN rubros r ON r.codigo = stock.rubro "    +
            " LEFT JOIN subrub s ON s.codigo = stock.subrub "   +
            " LEFT JOIN tipoart t ON t .codigo = stock.tipoart" +
            " where(y.fecha >='" + dsd + "' and y.fecha <= '" + hst + "') and y.empresaid = " + empresa + " and stock.codpro is not NULL and y.facturado = 'N'";
        string pedidos =
            "SELECT stock.codpro, stock.descri, x.cantidadAS unidades, 0 AS porsunid,  x.cantidad * stock.cantenv AS cantidad, 0 AS porscant," +
            " (x.precio* x.cant )*(100-x.precio*bonif)/100*(100-x.precio*bonif1)/100*(100-x.precio*y.bonifto)/100 precio, x.costo, convert(numeric,- 1), pendientes AS pendientes," +
            " y.[fechaing] AS fecha, stock.rubro codRub, r.descri descriRub, stock.subrub codSubRub, s.descri descriSubRu, stock.tipoart codTipoArt, t .descri descriTipoArt," +
            " stock.proveed, y.codven,  y.nrocli, 'PE' AS tipodoc, y.empresaid empresa, mon.ncotiza cotizacion " +
            " FROM PedidoDet x " +
            " LEFT JOIN stock  ON x.articulo = stock.codpro " +
            " LEFT JOIN Pedidocab y ON y.id = x.cabeceraid " +
            " LEFT JOIN rubros r ON r.codigo = stock.rubro " +
            " LEFT JOIN subrub s ON s.codigo = stock.subrub " +
            " LEFT JOIN monedas mon on x.moneda = mon.codigo " +
            " LEFT JOIN tipoart t ON t .codigo = stock.tipoart" +
            " LEFT JOIN (select cabeceraid, sum(pendientes) pendientes from pedidodet group by cabeceraid ) pend on y.id = pend.cabeceraid " +
            " where(fecha >= '" + dsd + "' and fecha <='" + hst + "') and y.empresaid =  " + empresa + " and stock.codpro is not NULL and pend.pendientes > 0";

        if (Eventas)
            from += ventas;
        if (Epedidos)
        {
            if (Eventas)
                from += " UNION ";
            from += pedidos;
        }
        if (Eremitos)
        {
            if (Eventas || Epedidos)
                from += " UNION ";
            from += remitos;
        }

        List<OperacionxRubro> lista;

        if (esVenta) {

            selectCompleto = "select" +
                "  max(codrub) codRub"                          +
                ", max(descriRub) descriRub"                    +
                ", max(codSubRub) codSubRub"                    +
                ", max(descriSubRu) descriSubRu"                +
                ", max(codTipoArt) codTipoArt"                  +
                ", max(descriTipoArt) descriTipoArt"            +
                ", max(codpro) codpro"                          + 
                ", max(descri) descri"                          +
                ", sum(unidades) unidades"                      +
                ", sum(cantidad) cantidad"                      +
                ", sum(precio) precio"                          +
                ", sum(costo) costo"                            +
                ", case when Sum(costo) = 0 then 0 else (sum(precio) / sum(costo) * 100) end as margen" +
                "  from (" + from + ") as loperaciones" + 
                "  where 1 = 1 " + query + "" +
                "  group by " + agrupar ;
            using (GestionEntities bd = new GestionEntities())
                lista = bd.Database.SqlQuery<OperacionxRubro>(selectCompleto).ToList();
        }
        else
        {

            selectCompleto = "select max(codrub) codRub" +
                ", max(descriRub) descriRub " +
                ", max(codSubRub) codSubRub " +
                ", max(descriSubRu) descriSubRu " +
                ", max(codTipoArt) codTipoArt " +
                ", max(descriTipoArt) descriTipoArt " +
                ", max(codpro) codpro " +
                ", max(descri) descri " +
                ", sum(unidades) unidades " +
                ", sum(cantidad) cantidad " +
                ", sum(precio) precio " +
                ", sum(costo) costo " +
                ", case when Sum(costo) = 0 then 0 " +
                " else (sum(precio) / sum(costo) * 100 - 100) end as margen " +
                " from LFOpxRubroCompra('" + dsd + "','" + hst + "'," + empresa + ") " +
                " where " + query +
                " group by " + agrupar + " ";
            using (GestionEntities bd = new GestionEntities())
                lista = bd.Database.SqlQuery<OperacionxRubro>(selectCompleto).ToList();
        }
        switch (agrupar)
        {
            case "codRub":
                rutaReporte = "Reportes/OperacionesXRubro/LOperacionesPorRubroRub.rdlc";
                break;
            case "codSubRub":

                rutaReporte = "Reportes/OperacionesXRubro/LOperacionesPorRubroSub.rdlc";
                break;
            case "codrub, codsubrub":
                rutaReporte = "Reportes/OperacionesXRubro/LOperacionesPorRubroRubSub.rdlc";
                break;
            case "codpro":
                rutaReporte = "Reportes/OperacionesXRubro/LOperacionesPorRubroArt.rdlc";
                break;
            case "codTipoArt":
                rutaReporte = "Reportes/OperacionesXRubro/LOperacionesPorRubroTipo.rdlc";
                break;
            default:
                rutaReporte = "";
                break;
        }
        // txt.Visible = true;
        txt.Text = selectCompleto;
        generarReporte(rutaReporte, parametros, new ReportDataSource("Operaciones", lista), dsd, hst);
    }

    private void LPedPendientes()
    {

        List<LWPedPendientes_Result> lista;
        string query = Request.QueryString["query"].ToString(),
            codigo = "1",
            limite = Request.QueryString["limite"].ToString();
        int reporte = Convert.ToInt32(Request.QueryString["reporte"].ToString()),
            listado = Convert.ToInt32(Request.QueryString["listado"].ToString()),
            odsd, ohst;
        bool ordenDescri = Convert.ToBoolean(Request.QueryString["orden"].ToString()),
            radioArt = Convert.ToBoolean(Request.QueryString["radioArt"].ToString()),
            resumido = Convert.ToBoolean(Request.QueryString["resumido"].ToString()),
            sinStock = Convert.ToBoolean(Request.QueryString["sinStock"].ToString()),
            sinAgrupar = Convert.ToBoolean(Request.QueryString["sinAgrupar"].ToString());
        DateTime ddsd, dhst;
        if (listado == 1)
        {
            codigo = Request.QueryString["cod"].ToString();
        }

        if (limite == "fecha")
        {
            ddsd = Convert.ToDateTime(Request.QueryString["dsd"].ToString());
            dhst = Convert.ToDateTime(Request.QueryString["hst"].ToString());

            using (GestionEntities bd = new GestionEntities())
            {
                if (query == "")
                    lista = bd.LWPedPendientes(empresa, ddsd, dhst).ToList();
                else
                    lista = bd.LWPedPendientes(empresa, ddsd, dhst).Where(query).ToList();
            }
        }
        else
        {
            odsd = Convert.ToInt32(Request.QueryString["dsd"].ToString()); ;
            ohst = Convert.ToInt32(Request.QueryString["hst"].ToString()); ;
            using (GestionEntities bd = new GestionEntities())
            {
                dsd = "01/01/1900";
                hst = DateTime.Now.ToString("dd/MM/yyyy");
                DateTime desde, hasta;
                desde = Convert.ToDateTime("01/01/1900");
                hasta = DateTime.Now;
                if (query == "")
                    lista = bd.LWPedPendientes(empresa, desde, hasta).Where(a => a.idCab >= odsd && a.idCab <= ohst).ToList();
                else
                    lista = bd.LWPedPendientes(empresa, desde, hasta).Where(query).Where(a => a.idCab >= odsd && a.idCab <= ohst).ToList();
            }
        }

        if (ordenDescri)
        {
            switch (reporte)
            {
                case 1:
                    lista = lista.OrderBy(a => a.proveeRazsoc).ToList();
                    break;
                case 2:
                    lista = lista.OrderBy(a => a.stockDescri).ToList();
                    break;
                case 3:
                    lista = lista.OrderBy(a => a.razsoc).ToList();
                    break;
                case 4:
                    lista = lista.OrderBy(a => a.descriMarca).ToList();
                    break;
                case 7:
                    lista = lista.OrderBy(a => a.descriZonas).ToList();
                    break;
                case 8:
                    lista = lista.OrderBy(a => a.rubroDescri).ToList();
                    break;
                case 9:
                    lista = lista.OrderBy(a => a.transpoDescri).ToList();
                    break;
            }
        }

        if (listado == 1)
        {
            int cod;
            switch (reporte)
            {
                case 1:
                    cod = Convert.ToInt32(codigo);
                    lista = lista.Where(a => a.nropro == cod).ToList();
                    break;
                case 2:
                    lista = lista.Where(a => a.codpro.ToString() == codigo).ToList();
                    break;
                case 3:
                    cod = Convert.ToInt32(codigo);
                    lista = lista.Where(a => a.nrocli == cod).ToList();
                    break;
                case 4:
                    cod = Convert.ToInt32(codigo);
                    lista = lista.Where(a => a.marca == cod).ToList();
                    break;
                case 7:
                    cod = Convert.ToInt32(codigo);
                    lista = lista.Where(a => a.zona == cod).ToList();
                    break;
                case 8:
                    cod = Convert.ToInt32(codigo);
                    lista = lista.Where(a => a.rubro == cod).ToList();
                    break;
                case 9:
                    cod = Convert.ToInt32(codigo);
                    lista = lista.Where(a => a.transpo == cod).ToList();
                    break;
            }
        }

        switch (reporte)
        {
            case 1:
                if (radioArt)
                    generarReporte("Reportes/PedidosPendientes/LPedPendientesProvee1.rdlc", parametros, new ReportDataSource("LWPedPendientes", lista), dsd, hst);
                else
                    generarReporte("Reportes/PedidosPendientes/LPedPendientesProveeVal.rdlc", parametros, new ReportDataSource("LWPedPendientes", lista), dsd, hst);
                break;
            case 2:
                if (!sinAgrupar)
                {
                    if (radioArt)
                        generarReporte("Reportes/PedidosPendientes/LPedPendientesArts1.rdlc", parametros, new ReportDataSource("LWPedPendientes", lista), dsd, hst);
                    else
                        generarReporte("Reportes/PedidosPendientes/LPedPendientesArtsVal.rdlc", parametros, new ReportDataSource("LWPedPendientes", lista), dsd, hst);
                }
                else
                {
                    generarReporte("Reportes/PedidosPendientes/LPedPendientesArtSinAgrup.rdlc", parametros, new ReportDataSource("LWPedPendientes", lista), dsd, hst);
                }
                break;
            case 3:
                if (!resumido)
                {
                    if (radioArt)
                        generarReporte("Reportes/PedidosPendientes/LPedPendientesCliente1.rdlc", parametros, new ReportDataSource("LWPedPendientes", lista), dsd, hst);
                    else
                        generarReporte("Reportes/PedidosPendientes/LPedPendientesClienteVal.rdlc", parametros, new ReportDataSource("LWPedPendientes", lista), dsd, hst);
                }
                else
                {
                    generarReporte("Reportes/PedidosPendientes/LPedPendientesClienteResumido.rdlc", parametros, new ReportDataSource("LWPedPendientes", lista), dsd, hst);
                }

                break;
            case 4:
                if (radioArt)
                    generarReporte("Reportes/PedidosPendientes/LPedPendientesMarca1.rdlc", parametros, new ReportDataSource("LWPedPendientes", lista), dsd, hst);
                else
                    generarReporte("Reportes/PedidosPendientes/LPedPendientesMarcaVal.rdlc", parametros, new ReportDataSource("LWPedPendientes", lista), dsd, hst);
                break;

            case 5:
                if (radioArt)
                    generarReporte("Reportes/PedidosPendientes/LPedPendientesVende1.rdlc", parametros, new ReportDataSource("LWPedPendientes", lista), dsd, hst);
                else
                    generarReporte("Reportes/PedidosPendientes/LPedPendientesVendeVal.rdlc", parametros, new ReportDataSource("LWPedPendientes", lista), dsd, hst);
                break;
            case 6:
                if (sinStock)
                    lista = lista.Where(a => a.saldo <= 0).ToList();
                generarReporte("Reportes/PedidosPendientes/LPedPendientesResum.rdlc", parametros, new ReportDataSource("LWPedPendientes", lista), dsd, hst);
                break;
            case 7:
                if (radioArt)
                    generarReporte("Reportes/PedidosPendientes/LPedPendientesZona1.rdlc", parametros, new ReportDataSource("LWPedPendientes", lista), dsd, hst);
                else
                    generarReporte("Reportes/PedidosPendientes/LPedPendientesZonaVal.rdlc", parametros, new ReportDataSource("LWPedPendientes", lista), dsd, hst);
                break;
            case 8:
                if (radioArt)
                    generarReporte("Reportes/PedidosPendientes/LPedPendientesRubro1.rdlc", parametros, new ReportDataSource("LWPedPendientes", lista), dsd, hst);
                else
                    generarReporte("Reportes/PedidosPendientes/LPedPendientesRubroVal.rdlc", parametros, new ReportDataSource("LWPedPendientes", lista), dsd, hst);
                break;

            case 9:
                if (radioArt)
                    generarReporte("Reportes/PedidosPendientes/LPedPendientesTransportista1.rdlc", parametros, new ReportDataSource("LWPedPendientes", lista), dsd, hst);
                else
                    generarReporte("Reportes/PedidosPendientes/LPedPendientesTransportistaVal.rdlc", parametros, new ReportDataSource("LWPedPendientes", lista), dsd, hst);
                break;

            case 10:
                lista = lista.OrderBy(a => a.idCab).ToList();
                if (radioArt)
                    generarReporte("Reportes/PedidosPendientes/LPedPendientesPedido.rdlc", parametros, new ReportDataSource("LWPedPendientes", lista), dsd, hst);
                else
                    generarReporte("Reportes/PedidosPendientes/LPedPendientesPedidoVal.rdlc", parametros, new ReportDataSource("LWPedPendientes", lista), dsd, hst);
                break;

            default:
                break;
        }

    }

    private void LOperacionesUsuario()
    {
        int radContenido = Convert.ToInt32(Request.QueryString["radcontenido"].ToString());
        bool usuario = Convert.ToBoolean(Request.QueryString["usuario"].ToString());
        DateTime ddsd = Convert.ToDateTime(dsd),
            dhst = Convert.ToDateTime(hst);
        List<LInforme_Result> lista;
        if (radContenido == 1)
        {
            if (usuario) {
                using (GestionEntities bd = new GestionEntities())
                    lista = bd.LInforme().Where(op => op.fecha >= ddsd && op.fecha <= dhst).ToList();
            }
            else
            {
                int idUsuario = Convert.ToInt32(Request.QueryString["idUsuario"].ToString());
                using (GestionEntities bd = new GestionEntities())
                    lista = bd.LInforme().Where(a => a.idusuario == idUsuario && a.fecha >= ddsd && a.fecha <= dhst).ToList();
            }
        }
        else
        {
            string contenido = Request.QueryString["contenido"].ToString();
            if (usuario)
            {
                using (GestionEntities bd = new GestionEntities())
                    lista = bd.LInforme().Where(a => a.detalle.Contains(contenido) && a.fecha >= ddsd && a.fecha <= dhst).ToList();
            }
            else
            {
                int idUsuario = Convert.ToInt32(Request.QueryString["idUsuario"].ToString());
                using (GestionEntities bd = new GestionEntities())
                    lista = bd.LInforme().Where(a => a.idusuario == idUsuario && a.detalle.Contains(contenido) && a.fecha >= ddsd && a.fecha <= dhst).ToList();
            }
        }
        txt.Text = lista.Count.ToString();
        generarReporte("Reportes/OperacionesUsuario/LOperacionesUsuario.rdlc", parametros, new ReportDataSource("Operaciones", lista), dsd, hst);
    }

    private void LOrdenesdeCompraPend()
    {
        int listado = Convert.ToInt32(Request.QueryString["listado"].ToString()),

            seleccion = Convert.ToInt32(Request.QueryString["seleccion"].ToString());
        DateTime dsd = Convert.ToDateTime(Request.QueryString["dsd"].ToString()),
            hst = Convert.ToDateTime(Request.QueryString["dsd"].ToString());
        int odsd = Convert.ToInt32(Request.QueryString["odsd"].ToString()),
            ohst = Convert.ToInt32(Request.QueryString["ohst"].ToString());
        string codpro = Request.QueryString["codpro"].ToString(),
            limite = Request.QueryString["limite"].ToString();
        List<LOrdenesDeCompraPendientes_Result> lista = null;
        using (GestionEntities bd = new GestionEntities()) {
            if (listado == 1)
            {
                if (limite == "fecha")
                {
                    lista = bd.Database.SqlQuery<LOrdenesDeCompraPendientes_Result>("select * from LOrdenesDeCompraPendientes(" + empresa + ")").Where(a => a.fecha >= dsd && a.fecha <= hst).ToList();
                }
                else
                {
                    lista = bd.Database.SqlQuery<LOrdenesDeCompraPendientes_Result>("select * from LOrdenesDeCompraPendientes(" + empresa + ")").Where(a => a.numero >= odsd && a.numero <= ohst).ToList();
                }
            }
            else
            {
                if (limite == "fecha")
                {
                    lista = bd.Database.SqlQuery<LOrdenesDeCompraPendientes_Result>("select * from LOrdenesDeCompraPendientes(" + empresa + ")").Where(a => a.codpro == codpro && a.fecha >= dsd && a.fecha <= hst).ToList();
                }
                else
                {
                    lista = bd.Database.SqlQuery<LOrdenesDeCompraPendientes_Result>("select * from LOrdenesDeCompraPendientes(" + empresa + ")").Where(a => a.codpro == codpro && a.numero >= odsd && a.numero <= ohst).ToList();
                }
            }

        }

        string rutaReporte = "";
        switch (seleccion)
        {
            case 1:
                rutaReporte = "Reportes/OrdenesCompra/LOrdenesDeCompraPend.rdlc";
                break;
            case 2:
                rutaReporte = "Reportes/OrdenesCompra/LOrdenesDeCompraPendComp.rdlc";
                break;
            case 3:
                rutaReporte = "Reportes/OrdenesCompra/LOrdenesDeCompraPendProvee.rdlc";
                break;
        }
        generarReporte(rutaReporte, parametros, new ReportDataSource("LOrdenesDeCompraPendientes", lista), dsd.ToString("dd/MM/yyyy"), hst.ToString("dd/MM/yyyy"));


    }


    private void LMarcas()
    {
        string orden = Request.QueryString["orden"].ToString();
        bool desc = Convert.ToBoolean(Request.QueryString["desc"].ToString());
        DateTime dsd = Convert.ToDateTime(Request.QueryString["dsd"].ToString()),
            hst = Convert.ToDateTime(Request.QueryString["hst"].ToString());
        List<marcas> lista;
        using (GestionEntities bd = new GestionEntities())
        {
            lista = bd.marcas.ToList();
        }
        if (!desc)
        {
            if (orden == "1")
            {
                lista = lista.OrderBy(a => a.codigo).ToList();
            }
            else if (orden == "2")
            {
                lista = lista.OrderBy(a => a.descripcion).ToList();
            }
        }
        else
        {
            if (orden == "1")
            {
                lista = lista.OrderByDescending(a => a.codigo).ToList();
            }
            else if (orden == "2")
            {
                lista = lista.OrderByDescending(a => a.descripcion).ToList();
            }
        }
        generarReporte("Reportes/Marcas/lMarcas.rdlc", parametros, new ReportDataSource("LMarcas", lista), dsd.ToString("dd/MM/yyyy"), hst.ToString("dd/MM/yyyy"));
    }
    private void LConceptosCaja()
    {
        string orden = Request.QueryString["orden"].ToString();
        bool desc = Convert.ToBoolean(Request.QueryString["desc"].ToString());
        DateTime dsd = Convert.ToDateTime(Request.QueryString["dsd"].ToString()),
            hst = Convert.ToDateTime(Request.QueryString["hst"].ToString());
        List<MiConcepto> lista;
        using (GestionEntities bd = new GestionEntities())
            lista = bd.Database.SqlQuery<MiConcepto>("select codigo, descri from concepto").ToList();
        if (!desc) {
            if (orden == "1")
                lista = lista.OrderBy(a => a.id).ToList();
             else if (orden == "2")
                lista = lista.OrderBy(a => a.descri).ToList();
        }
        else
        {
            if (orden == "1")
                lista = lista.OrderByDescending(a => a.id).ToList();
            else if (orden == "2")
                lista = lista.OrderByDescending(a => a.descri).ToList();
        }
        generarReporte("Reportes/ConceptosCaja/lConceptos.rdlc", parametros, new ReportDataSource("LConceptos", lista), dsd.ToString("dd/MM/yyyy"), hst.ToString("dd/MM/yyyy"));
    }

    private void LCotizacionesEmitidas()
    {
        string listado = Request.QueryString["listado"].ToString(),
            query = Request.QueryString["query"].ToString();
        bool orden = Convert.ToBoolean(Request.QueryString["orden"].ToString());
        DateTime dsd = Convert.ToDateTime(Request.QueryString["dsd"].ToString()),
            hst = Convert.ToDateTime(Request.QueryString["hst"].ToString());
        List<LIvaVen_Result> lista;
        using (GestionEntities bd = new GestionEntities())
            lista = bd.LIvaVen(dsd, hst, empresa).Where(query).ToList();
        switch (listado)
        {
            case "1":
                generarReporte("Reportes/CotizacionesEmitidas/LCotizacionesCompleto.rdlc", parametros, new ReportDataSource("Vivaven", lista), dsd.ToString("dd/MM/yyyy"), hst.ToString("dd/MM/yyyy"));
                break;
            case "2":
                generarReporte("Reportes/CotizacionesEmitidas/LCotizacionesProv.rdlc", parametros, new ReportDataSource("Vivaven", lista), dsd.ToString("dd/MM/yyyy"), hst.ToString("dd/MM/yyyy"));
                break;
            case "3":
                generarReporte("Reportes/CotizacionesEmitidas/LCotizacionesTipoComp.rdlc", parametros, new ReportDataSource("Vivaven", lista), dsd.ToString("dd/MM/yyyy"), hst.ToString("dd/MM/yyyy"));
                break;
            case "4":
                generarReporte("Reportes/CotizacionesEmitidas/LCotizacionesCondIVA.rdlc", parametros, new ReportDataSource("Vivaven", lista), dsd.ToString("dd/MM/yyyy"), hst.ToString("dd/MM/yyyy"));
                break;
            case "5":
                generarReporte("Reportes/CotizacionesEmitidas/LCotizacionesCliente.rdlc", parametros, new ReportDataSource("Vivaven", lista), dsd.ToString("dd/MM/yyyy"), hst.ToString("dd/MM/yyyy"));
                break;
            case "6":
                generarReporte("Reportes/CotizacionesEmitidas/LCotizacionesVende.rdlc", parametros, new ReportDataSource("Vivaven", lista), dsd.ToString("dd/MM/yyyy"), hst.ToString("dd/MM/yyyy"));
                break;
            case "7":
                generarReporte("Reportes/CotizacionesEmitidas/LCotizacionesResumen.rdlc", parametros, new ReportDataSource("Vivaven", lista), dsd.ToString("dd/MM/yyyy"), hst.ToString("dd/MM/yyyy"));

                break;
            case "8":
                break;
        }
    }
    private void LComisiones()
    {
        bool agrupar = Convert.ToBoolean(Request.QueryString["agrupar"].ToString()),
               liquidadas = Convert.ToBoolean(Request.QueryString["liquidadas"].ToString()),
               cotizaciones = Convert.ToBoolean(Request.QueryString["cotizaciones"].ToString());
        string tipocomis = Request.QueryString["comis"].ToString(),
            queryCotis = "",
            dsd = Request.QueryString["dsd"].ToString(),
            hst = Request.QueryString["hst"].ToString();
        int codven = Convert.ToInt32(Request.QueryString["codven"].ToString());
        DateTime ddsd = Convert.ToDateTime(dsd),
                hhst = Convert.ToDateTime(hst);

        List<FComisiones_Result> lista;
        string querycodven = "";
        string liquidada = "";
        string where = "";
        if (!liquidadas)
        {
            if (where != "")
                where += " and ";
            where += " liquidada = 0 ";
        }
        if (codven > 0)
        {
            if (where != "")
                where += " and ";
            where += " codven = " + codven;
        }
        if (!cotizaciones)
        {
            if (where != "")
                where += " and ";
            where += " tipodoc not in('CT','AJD','AJC') ";
        }
        if(where != "")
        {
            where = "where " + where;
        }

        string orderby = " order by fecha ";
        using (GestionEntities bd = new GestionEntities())
        {
            lista = bd.Database.SqlQuery<FComisiones_Result>(
                "select  * from FComisiones(" + empresa + ",'" + Request.QueryString["dsd"].ToString() + "', '" + Request.QueryString["hst"].ToString() + "' , "+tipocomis +")" 
                + where
                + orderby
                ).ToList();
        }

        switch (tipocomis)
        {
            case "1":
                if (agrupar)
                    generarReporte("Reportes/Comisiones/LComisionesVendedoresAgrup.rdlc", parametros, new ReportDataSource("Vivaven", lista), dsd, hst);
                else
                    generarReporte("Reportes/Comisiones/LComisionesVendedores.rdlc", parametros, new ReportDataSource("Vivaven", lista), dsd, hst);
                break;
            case "2":
                if (agrupar)
                    generarReporte("Reportes/Comisiones/LComisionesClienteAgrup.rdlc", parametros, new ReportDataSource("Vivaven", lista), dsd, hst);
                else
                    generarReporte("Reportes/Comisiones/LComisionesCliente.rdlc", parametros, new ReportDataSource("Vivaven", lista), dsd, hst);
                break;
            case "3":
                if (agrupar)
                    generarReporte("Reportes/Comisiones/LComisionesProductoAgrup.rdlc", parametros, new ReportDataSource("Vivaven", lista), dsd, hst);
                else
                    generarReporte("Reportes/Comisiones/LComisionesProducto.rdlc", parametros, new ReportDataSource("Vivaven", lista), dsd, hst);
                break;
        }
    }

    private void LCobranzas()
    {
        bool agrupar = Convert.ToBoolean(Request.QueryString["agrupar"].ToString()),
            liquidadas = Convert.ToBoolean(Request.QueryString["liquidadas"].ToString()),
            cotizaciones = Convert.ToBoolean(Request.QueryString["cotizaciones"].ToString());
        string tipocomis = Request.QueryString["comis"].ToString(),
            queryCotis = "",
            dsd = Request.QueryString["dsd"].ToString(),
            hst = Request.QueryString["hst"].ToString();
        int codven = Convert.ToInt32(Request.QueryString["codven"].ToString());
        DateTime ddsd = Convert.ToDateTime(dsd),
                hhst = Convert.ToDateTime(hst);

        List<FCobranzas_Result> lista;
        string querycodven = "";
        string liquidada = "";
        string where = "";
        if (!liquidadas)
        {
            if (where != "")
                where += " and ";
            where += " liquidada = 0 ";
        }
        if (codven > 0)
        {
            if (where != "")
                where += " and ";
            where += " codven = " + codven;
        }
        if (!cotizaciones)
        {
            if (where != "")
                where += " and ";
            where += " tipdoco not in('CT','AJD','AJC') ";
        }
        if (where != "")
        {
            where = "where " + where;
        }
        string orderby = " order by fecha,id ";

        using (GestionEntities bd = new GestionEntities())
        {
            lista = bd.Database.SqlQuery<FCobranzas_Result>(
                "select  * from FCobranzas('" + ddsd.ToString("dd/MM/yyyy") + "', '" + hhst.ToString("dd/MM/yyyy") + "'," + empresa + ", " + tipocomis + " )" 
                + where
                + orderby).ToList();
        }
        switch (tipocomis)
        {
            case "1":
                generarReporte("Reportes/Cobranzas/LCobranzasVendedores.rdlc", parametros, new ReportDataSource("VCobranzas", lista), dsd, hst);
                break;
            case "2":
                generarReporte("Reportes/Cobranzas/LCobranzasCliente.rdlc", parametros, new ReportDataSource("VCobranzas", lista), dsd, hst);
                break;
            case "3":
                generarReporte("Reportes/Cobranzas/LCobranzasProducto.rdlc", parametros, new ReportDataSource("VCobranzas", lista), dsd, hst);
                break;
        }
    }

    private void LClientes()
    {
        string query = Request.QueryString["query"].ToString(),
            rutaReporte;
        List<LClientes_Result> lista;
        string sdsd = Request.QueryString["dsd"].ToString(),
            shst = Request.QueryString["hst"].ToString();
        DateTime dsd = Convert.ToDateTime(sdsd),
                 hst = Convert.ToDateTime(shst);
        bool resumido = Convert.ToBoolean(Request.QueryString["resumido"].ToString());
        using (GestionEntities bd = new GestionEntities())
        {
            if (query != "")
                lista = bd.LClientes(dsd, hst).Where(query).ToList();
            else
            {
                dsd = Convert.ToDateTime("1900-01-01");
                hst = DateTime.Today;
                lista = bd.LClientes(dsd, hst).ToList();
            }
        }
        string orden = Request.QueryString["orden"].ToString();
        txt.Text = lista.Count.ToString();
        if (orden == "1")
        {
            lista = lista.OrderBy(a => a.razsoc).ToList();
        }
        if (orden != "1")
        {
            lista = lista.OrderBy(a => a.nrocli).ToList();
        }
        if (resumido)
        {
            rutaReporte = "Reportes/Cliente/lClientesResumido.rdlc";
        }
        else
        {
            rutaReporte = "Reportes/Cliente/lClientes.rdlc";
        }
        generarReporte(rutaReporte, parametros, new ReportDataSource("LClientes", lista), sdsd, shst);
    }

    private void LArtVendCantPorArt()
    {
        string query = Request.QueryString["query"].ToString();
        List<prueba> lista;
        using (GestionEntities bd = new GestionEntities())
        {
            lista = bd.Database.SqlQuery<prueba>(
                "select max(codpro) as codpro, max(descri) as descri, sum(cant) as cant, max(cantenv) as cantenv, sum(prexcant) as total, sum(bonif) as bonif, " +
                "sum(bonif1) as bonif1, count(cant) cantVentas, sum(bonito) as bonito,  sum(precio - (precio*bonif/100)-(precio*bonif1/100))/count(cant) precioPromedio" +
                " from LVentasPorProducto(" + empresa + ")" +
                " where " + query + " " +
                "group by codpro").ToList();
        }
        generarReporte("Reportes/ArtVendCantPorArt/LArtVendCantPorArt.rdlc", parametros, new ReportDataSource("LArtVendCantPorArt", lista), dsd, hst);

    }

    private void LArtVendAgrupando()
    {
        string dsd = Request.QueryString["dsd"].ToString(),
            hst = Request.QueryString["hst"].ToString(),
            query = Request.QueryString["query"].ToString(),
            rutaReporte = "";
        DateTime ddsd = Convert.ToDateTime(dsd),
            hhst = Convert.ToDateTime(hst);
        //        bool resumido = Convert.ToBoolean(Request.QueryString["resumido"].ToString());

        List<LArtVendAgrupando_Result> lista;
        using (GestionEntities bd = new GestionEntities())
        {
            lista = bd.Database.SqlQuery<LArtVendAgrupando_Result>("select  * from LArtVendAgrupando('" + dsd + "', '" + hst + "', " + empresa + ")").Where(query).ToList();
        }
        rutaReporte = "Reportes/ArtVendAgrup/LArtVendAgrupando.rdlc";
        /* if (resumido)
         {

         } else
         {
             rutaReporte = "Reportes/LArtVendAgrupandoResumido.rdlc";
         }*/
        generarReporte(rutaReporte, parametros, new ReportDataSource("LArtVendAgrupando", lista), dsd, hst);
    }

    private void lArtPorProveedor()
    {
        bool orden = Convert.ToBoolean(Request.QueryString["orden"]),
            desc = Convert.ToBoolean(Request.QueryString["desc"]);
        List<lArtxProveedor_Result> lista;
        string where = Request.QueryString["where"].ToString();
        string order;
        
        if (!desc)
        {
            if (orden)
            {
                order = " codpro ";
            }
            else
            {
                order = " descri ";
            }
        }
        else
        {
            if (orden)
            {
                order = " codpro desc ";
            }
            else
            {
                order = " descri desc ";
            }
        }
        using (GestionEntities bd = new GestionEntities())
            lista = bd.Database.SqlQuery<lArtxProveedor_Result>("select * from lArtxProveedor(" + empresa + ") where 1 = 1 " + where + " order by " + order).ToList();


        generarReporte("Reportes/ArtPorProveedor/LArtPorPreveedor.rdlc", parametros, new ReportDataSource("LArtPorPreveedor", lista), dsd, hst);
    }

    private void lArtNoVendidos()
    {
        int idRubro = Convert.ToInt32(Request.QueryString["rubro"]),
            idProvee = Convert.ToInt32(Request.QueryString["provee"]),
            idMarca = Convert.ToInt32(Request.QueryString["marca"]),
            idSubrub = Convert.ToInt32(Request.QueryString["subrub"]),
            enLista = 0, incluir = 0;
        if (Convert.ToBoolean(Request.QueryString["enlista"]) == true)
            enLista = 1;
        else
            enLista = 0;

        if (Convert.ToBoolean(Request.QueryString["cotizaciones"]) == true)
        {
            incluir = 1;
        }
        else
        {
            incluir = 0;
        }

        string dsd = Request.QueryString["dsd"],
            hst = Request.QueryString["hst"];

        List<NoVendidos_Result> lista;
        using (GestionEntities bd = new GestionEntities())
        {
            lista = bd.Database.SqlQuery<NoVendidos_Result>("exec sp_NoVendidos '" + dsd + "','" + hst + "', " + empresa + ", " + enLista).ToList();
        }
        if (enLista == 1)
            lista = lista.Where(a => a.incluido == true).ToList();
        if (idRubro != 0)
            lista = lista.Where(a => a.rubro == idRubro).ToList();
        if (idSubrub != 0)
            lista = lista.Where(a => a.subrub == idSubrub).ToList();
        if (idMarca != 0)
            lista = lista.Where(a => a.marca == idMarca).ToList();
        if (idProvee != 0)
            lista = lista.Where(a => a.proveed == idProvee).ToList();

        generarReporte("Reportes/NoVendidos/LArtNoVendidos.rdlc", parametros, new ReportDataSource("LArtNoVendidos", lista), dsd, hst);
    }
    private void lPreciosDC() // falta en moneda de curso legal
    {
        /* nReporte

         moneda = ${ this.checkMoneda}



         */
        ///----------

        List<MiLPrecios> lista;
        bool encabezado = Convert.ToBoolean(Request.QueryString["encabezado"]),
            compCosto = Convert.ToBoolean(Request.QueryString["compCosto"]),
            excel = Convert.ToBoolean(Request.QueryString["excel"]),
            precioConIVA = Convert.ToBoolean(Request.QueryString["IVA"]),
            monedaCursoLegal = Convert.ToBoolean(Request.QueryString["moneda"]),
            conFoto = Convert.ToBoolean(Request.QueryString["conFoto"]);
        var precioSeleccionado = Request.QueryString["precio"].ToString();
        query = Request.QueryString["query"].ToString();
        string precio,
            oArticulo = Request.QueryString["oArticulo"].ToString(),
            oEncabezado = Request.QueryString["oEncabezado"].ToString();
        string consulta = "SELECT dbo.stock.codpro, dbo.stock.descri, dbo.stock.unimed, stock.costo, " +
            "(case when '" + precioSeleccionado + "' = 'precio1' then precio1" +
                " when '" + precioSeleccionado + "' = 'precio2' then precio2 " +
                " when '" + precioSeleccionado + "' = 'precio3' then precio3 " +
                " when '" + precioSeleccionado + "' = 'precio4' then precio4 " +
                " when '" + precioSeleccionado + "' = 'precio5' then precio5 " +
                " when '" + precioSeleccionado + "' = 'precio6' then precio6 " +
                " when '" + precioSeleccionado + "' = 'costo' then costo end ) as precio " +
            " , dbo.stock.rubro AS codRubro, stock.pathfoto , dbo.rubros.descri AS descriRubro, dbo.stock.subrub AS codSubrub, dbo.subrub.descri AS descriSub," +
            " dbo.stock.marca, dbo.marcas.descripcion AS descrimarcas, dbo.stock.proveed AS codprovee, dbo.provee.razsoc AS descriProveed, dbo.stock.oferta," +
            " dbo.provee.lista, dbo.stock.fechaact, dbo.stock.moneda, dbo.stock.ivagrupo, dbo.ivaart.porcen1, dbo.unimed.simbolo, dbo.stock.incluido " +
            "FROM dbo.stock left JOIN dbo.rubros ON dbo.rubros.codigo = dbo.stock.rubro left JOIN dbo.subrub ON dbo.stock.subrub = dbo.subrub.codigo " +
            "left JOIN dbo.provee ON dbo.provee.nropro = dbo.stock.proveed left JOIN dbo.ivaart ON dbo.ivaart.codigo = dbo.stock.ivagrupo " +
            "left JOIN dbo.marcas ON dbo.marcas.codigo = dbo.stock.marca left  JOIN dbo.unimed ON dbo.unimed.codigo = dbo.stock.unimed";
        using (GestionEntities bd = new GestionEntities())
        {
            if (query == "empty")
                lista = bd.Database.SqlQuery<MiLPrecios>(consulta).ToList();
            else
                lista = bd.Database.SqlQuery<MiLPrecios>(consulta).Where(query).ToList();
        }
            if (precioConIVA)
                lista = agregarIva(lista, precioSeleccionado);
            if (monedaCursoLegal)
                lista = enMonedaCursoLegal(lista, precioSeleccionado);
            parametros.Add("oArticulo", oArticulo);
            parametros.Add("oEncabezado", oEncabezado);

            if (conFoto)
            {
                rutaReporte = "Reportes/Precios/LPreciosFoto.rdlc";
                foreach (var p in lista)
                    p.pathfoto = "file:" + p.pathfoto;
        }
            else
            {
                if (compCosto)
                {
                    if (!encabezado)
                        rutaReporte = "Reportes/Precios/LPreciosDCComp.rdlc";
                    else
                        rutaReporte = "Reportes/Precios/LPreciosSinEncComp.rdlc";
                }
                else
                {
                    if (!encabezado)
                    {
                        rutaReporte = "Reportes/Precios/LPreciosDC.rdlc";
                    }
                    else
                    {
                        rutaReporte = "Reportes/Precios/LPreciosSinEnc.rdlc";
                    }
                }
            }

            if (lista.Count > 0)
                generarReporte(rutaReporte, parametros, new ReportDataSource("LPrecios", lista), dsd, hst);
            else
                alertaElementos.Visible = true;
            if (excel)
            {
                Response.Clear();
                Response.ContentType =
                     "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=\"Precios" + DateTime.Now.Date.ToString("ddMMyyyy") + ".xlsx");
                var workbook = excelControllers.getIntancia().ListadoPrecios(lista, empresa);
                //var worksheet = workbook.Worksheets.Add("Sheet 1");
                using (var memoryStream = new MemoryStream())
                {
                    workbook.SaveAs(memoryStream);
                    memoryStream.WriteTo(Response.OutputStream);
                }
                Response.End();
            }


        
    }

        private List<MiLPrecios> agregarIva(List<MiLPrecios> lista, string precioSeleccionado)
        {
            decimal tasaIva;
            using (GestionEntities bd = new GestionEntities())
                tasaIva = bd.configen.Single(a => a.empresa == empresa).ivatasagral;
            foreach (var item in lista)
            {
                
                        item.precio = item.precio + (item.precio * tasaIva / 100);
                

            }
            return lista;
        }


        private void lDeudores()
        {// mandar fechas, datos cliente, datos vende, datos prov, datos zona
            string rutaReporte = "Reportes/lDeudoresResum.rdlc";
            List<LSaldoDeudores2_Result> lista;

            int oCuenta = Convert.ToInt32(Request.QueryString["sortCount"]),
                oComprobante = Convert.ToInt32(Request.QueryString["sortDoc"]),
                tipoReport = Convert.ToInt32(Request.QueryString["report"]),
                idZona = Convert.ToInt32(Request.QueryString["codzona"]),
                idCliente = Convert.ToInt32(Request.QueryString["nrocli"]),
                idVende = Convert.ToInt32(Request.QueryString["codven"]),
                idProvincia = Convert.ToInt32(Request.QueryString["codProv"]);

            string vence = Request.QueryString["vence"].ToString(),
                descriZona = Request.QueryString["nomzona"].ToString(),
                descriCliente = Request.QueryString["razsoc"].ToString(),
                descriVende = Request.QueryString["nombreven"].ToString(),
                descriProvincia = Request.QueryString["nombreprov"].ToString();
            query = Request.QueryString["query"].ToString();


            using (GestionEntities bd = new GestionEntities())
                lista = bd.LSaldoDeudores2(empresa, new DateTime().Date).Where(query).ToList();//.Where(query).ToList();


            parametros.Add("vence", vence);
            parametros.Add("oComp", oComprobante.ToString());
            parametros.Add("oCuenta", oCuenta.ToString());
            parametros.Add("idCliente", idCliente.ToString());
            parametros.Add("idVende", idVende.ToString());
            parametros.Add("idProvincia", idProvincia.ToString());
            parametros.Add("idZona", idZona.ToString());
            parametros.Add("descriCliente", descriCliente);
            parametros.Add("descriVende", descriVende);
            parametros.Add("descriProvincia", descriProvincia);
            parametros.Add("descriZona", descriZona);
            switch (tipoReport)
            {
                case 1: rutaReporte = "Reportes/Deudores/lDeudoresSinDet.rdlc";
                    break;
                case 2:
                    rutaReporte = "Reportes/Deudores/lDeudoresResum.rdlc";
                    break;
                case 3:
                    rutaReporte = "Reportes/Deudores/lDeudores.rdlc";
                    break;
            }
            if (lista.Count > 0)
                generarReporte(rutaReporte, parametros, new ReportDataSource("LsaldoDeudores", lista), dsd, hst);
            else
                alertaElementos.Visible = true;

        }

        private void generarReporte(string rutaReporte, Dictionary<string, string> parametros, ReportDataSource dataSource, string dsd, string hst)
        {


            reporte.Visible = true;
            reporte.LocalReport.EnableExternalImages = true;
            reporte.LocalReport.DataSources.Clear();
            ReportParameter[] fechas = new ReportParameter[2];
            ReportParameter[] paramsReporte = new ReportParameter[parametros.Count];
            paramsReporte = addParamsToArray(paramsReporte, parametros);
            reporte.LocalReport.ReportPath = rutaReporte;
            fechas[0] = new ReportParameter("dsd", dsd);
            fechas[1] = new ReportParameter("hst", hst);
            reporte.LocalReport.SetParameters(paramsReporte);
            if (report == 1 || report == 3 || report == 5 || report == 6 || report == 8 || report == 9 ||
                report == 10 || report == 13 || report == 14 || report == 15 || report == 16 || report == 17
                || report == 32 || report == 33 || report == 37 || report == 38 || report == 39 || report == 40 || report == 45 || report == 46)
                reporte.LocalReport.SetParameters(fechas);
            reporte.LocalReport.DataSources.Add(dataSource);
            reporte.LocalReport.Refresh();
    
        }
    private void generarReporteComprobante(string rutaReporte, List<ReportDataSource> dataSource)
        {


            reporte.Visible = true;
            reporte.LocalReport.DataSources.Clear();
            reporte.LocalReport.ReportPath = rutaReporte;
            foreach (var p in dataSource)
            {
                reporte.LocalReport.DataSources.Add(p);
            }
            reporte.LocalReport.Refresh();
        }

        private ReportParameter[] addParamsToArray(ReportParameter[] paramsReporte, Dictionary<string, string> parametros)
        {
            int i = 0;
            foreach (var param in parametros)
            {
                paramsReporte[i] = new ReportParameter(param.Key, param.Value);
                i++;
            }
            ;
            return paramsReporte;
        }



        /* reporte.Visible = true;
          ReportParameter[] fechas = new ReportParameter[2];
          ReportParameter oCuenta = new ReportParameter("oCuenta", ordenCuenta);
          ReportParameter oComp = new ReportParameter("oComp", ordenComprobante);
          fechas[0] = new ReportParameter("dsd", dsd.ToString("dd/MM/yyy"));
          fechas[1] = new ReportParameter("hst", hst.ToString("dd/MM/yyy"));
          reporte.LocalReport.SetParameters(fechas);
          reporte.LocalReport.SetParameters(oCuenta);
            ReportParameter oComp = new ReportParameter("oComp", ordenComprobante);
          reporte.LocalReport.SetParameters(oComp);*/





        private List<MiLPrecios> enMonedaCursoLegal(List<MiLPrecios> lista, string precioSeleccionado)
        {
            decimal valorMonedaFinal, valorMonedaInicio;

            using (GestionEntities bd = new GestionEntities())
            {
                var moneda = bd.configen.Single(b => b.empresa == empresa).nmonedacl;
                valorMonedaInicio = bd.monedas.Single(a => a.codigo == moneda).ncotiza;
            }
            foreach (var item in lista)
            {
                using (GestionEntities bd = new GestionEntities())
                    valorMonedaFinal = bd.monedas.Single(a => a.codigo == item.moneda).ncotiza;
                item.precio = item.precio * valorMonedaInicio / valorMonedaFinal;
            }
            return lista;
        }

    }


public class OperacionxRubro
{


    public int codRub { get; set; }
    public string descriRub { get; set; }
    public decimal codSubRub { get; set; }
    public string descriSubRu { get; set; }
    public int codTipoArt { get; set; }
    public string descriTipoArt { get; set; }
    public string codpro { get; set; }
    public string descri { get; set; }
    public decimal unidades { get; set; }
    public decimal cantidad { get; set; }
    public decimal precio { get; set; }
    public decimal costo { get; set; }
    public decimal margen { get; set; }

    
}



