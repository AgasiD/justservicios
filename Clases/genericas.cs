using JustServicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Results;

    public class genericas
    {
    }
[Serializable]




public class DeudaProvee
{
    public string razsoc { get; set; }
    public DateTime fecha { get; set; }
    public string tipodoc { get; set; }
    public string letra { get; set; }
    public string condicion { get; set; }
    public decimal punto { get; set; }
    public decimal numero { get; set; }
    public decimal importe1 { get; set; }
    public decimal importeOriginal { get; set; }
    public DateTime vence { get; set; }
    public decimal saldo { get; set; }
    public decimal importe2 { get; set; }
    public int nropro { get; set; }
}
public partial class FlistPuntoPedido { 
    public string codpro { get; set; }
    public string descri { get; set; }
    public decimal stomin { get; set; }
    public decimal ppedido { get; set; }
    public System.DateTime fechaact { get; set; }
    public decimal saldoActual { get; set; }
    public Nullable<decimal> diferencia { get; set; }
    public decimal cantmin { get; set; }
    public decimal consProm { get; set; }
    public bool discont { get; set; }
    public decimal marcaCod { get; set; }
    public string marcaDescr { get; set; }
    public int proveedCod { get; set; }
    public string proveeRazs { get; set; }
    public int codRub { get; set; }
    public string rubroDescri { get; set; }
    public decimal codSubRub { get; set; }
    public string SubRuDescri { get; set; }
    public int tipoart { get; set; }
    public decimal cantenv { get; set; }
    public decimal precio1 { get; set; }
    public decimal precio2 { get; set; }
    public decimal precio3 { get; set; }
    public decimal precio4 { get; set; }
    public decimal precio5 { get; set; }
    public decimal precio6 { get; set; }
    public decimal costo { get; set; }
    public bool incluido { get; set; }
    public decimal pedido { get; set; }
}
public class MiUtilidades
{
    public Nullable<decimal> codMarca { get; set; }
    public string codpro { get; set; }
    public string descriMarca { get; set; }
    public string descri { get; set; }
    public Nullable<decimal> codSubrub { get; set; }
    public string descriSubrub { get; set; }
    public int? codRubro { get; set; }
    public decimal? utilidad { get; set; }
    public string descriRubro { get; set; }
    public Nullable<decimal> cant { get; set; }
    public Nullable<decimal> venta { get; set; }
    public Nullable<decimal> costo { get; set; }
    public Nullable<decimal> diferencia { get; set; }
}

class MiLStockCodigo
{
    public string codpro { get; set; }
    public string descri { get; set; }
    public string color { get; set; }
    public DateTime fechveri { get; set; }
    public decimal? saldo { get; set; }

}


public class prueba2
{
    public List<stocks> lista { get; set; }
    public decimal bonifcli { get; set; }

    public prueba2()
    {
        lista = new List<stocks>();
    }
}
public class prueba
{
    public string codpro { get; set; }
    public string descri { get; set; }
    public decimal cant { get; set; }
    public decimal cantenv { get; set; }
    public decimal total { get; set; }
    public decimal bonif { get; set; }
    public decimal bonif1 { get; set; }
    public decimal bonito { get; set; }
    public int cantVentas { get; set; }
    public decimal precioPromedio { get; set; }
}
public class MiPorcenVentas
{
    public decimal? nrocli { get; set; }
    public string razsoc { get; set; }
    public decimal? cant{ get; set; }
    public decimal? porcencant { get; set; }
    public decimal? cantenv { get; set; }
    public decimal? porcencantenv { get; set; }
    public decimal? neto { get; set; }
    public decimal? porcenneto { get; set; }
}
public class MiProvee: MiPorcenVentas
{

    public string razsoc { get; set; }
    public int nropro { get; set; }
}
/*
public class MiTranspo
{
    public partial class transpo
    {
        public int id { get; set; }
        public decimal codigo { get; set; }
        public string razsoc { get; set; }
        public string direcc { get; set; }
        public decimal locali { get; set; }
        public string codpos { get; set; }
        public string telefo { get; set; }
        public string cuit { get; set; }
        public string ingbru { get; set; }
        public string observa { get; set; }
        public string horario { get; set; }
        public string contacto { get; set; }
    }
}*/
public class MiLPrecios
{
    public string codpro { get; set; }
    public string descri { get; set; }
    public decimal unimed { get; set; }
    public decimal precio { get; set; }
    public decimal costo{ get; set; }
    public int codRubro { get; set; }
    public string descriRubro { get; set; }
    public decimal codSubrub { get; set; }
    public string descriSub { get; set; }
    public decimal marca { get; set; }
    public string descrimarcas { get; set; }
    public decimal codprovee { get; set; }
    public string descriProveed { get; set; }
    public bool oferta { get; set; }
    public decimal lista { get; set; }
    public System.DateTime fechaact { get; set; }
    public string moneda { get; set; }
    public decimal ivagrupo { get; set; }
    public decimal porcen1 { get; set; }
    public string simbolo { get; set; }
    public bool incluido { get; set; }
    public string pathfoto { get; set; }
}

public class foto
{
    public string id { get; set; }
}
public class ivavenComprobante
{
    public DateTime fecha { get; set; }
    public string tipodoc { get; set; }
    public string letra { get; set; }
    public decimal punto { get; set; }
    public decimal numero { get; set; }
    public decimal hasta { get; set; }
    public decimal nrocli { get; set; }
    public string razsoc { get; set; }
    public decimal total { get; set; }
    public string cae { get; set; }
    public DateTime vencecae { get; set; }
    public string numerofe { get; set; }
    public string idpetic { get; set; }
    public int ctacon { get; set; }
    public int ctaconex { get; set; }
    public int empresaid { get; set; }
    public int id{ get; set; }
    public decimal provin { get; set; }
    public decimal remito { get; set; }


}

public class ivavenVisualizarComprobante
{
   public DateTime fecha {get;set;}
   public string tipodoc {get;set;}
        public string letra {get;set;}
        public decimal punto{get;set;}
        public decimal numero {get;set;}
        public decimal hasta { get;set;}
        public decimal nrocli { get;set;}
        public string razsoc {get;set;} 
        public string direcc {get;set;}
        public string locali {get;set;}
        public string codpos { get;set;}
        public int provin{get;set;}
        public string cuit {get;set;}
        public decimal subtot { get;set;}
        public decimal bonitot { get;set;}
        public decimal bonifto { get;set;}
        public decimal exento { get;set;}
        public decimal neto { get;set;}
        public decimal ivai { get;set;}
        public decimal ivanoi {get;set;}
        public decimal ivaidif { get;set;}
        public decimal ivanoidif {get;set;}
        public decimal total {get;set;}
        public decimal bonif { get;set;}
        public decimal codcondic{get;set;}
        public string condicion {get;set;}
        public decimal codven {get;set;}
        public string nomven { get;set;}
        public decimal respon { get;set;}
        public decimal comis { get;set;}
        public decimal retiva {get;set;}
        public decimal retgan {get;set;}
        public decimal retinb {get;set;}
        public decimal remito {get;set;}
        public string observa{get;set;}
        public decimal comiprod {get;set;}
        public decimal comicli { get;set;}
        public bool liquidada{get;set;}
        public decimal porins { get;set;}
        public decimal pornoi { get;set;}
        public decimal porinsdif { get;set;}
        public decimal pornoidif { get;set;}
        public DateTime vence{get;set;}
        public string ocompra { get;set;}
        public string simbolo { get;set;}
        public decimal cotizacion { get;set;}
        public decimal nrorepa { get;set;}
        public decimal nrollevo { get;set;}
        public bool cumplido{get;set;}
        public bool bajostk{get;set;}
        public decimal porcenib {get;set;}
        public bool cerrado{get;set;}
        public string cai { get;set;}
        public DateTime vencecai{get;set;}
        public string obsrepa {get;set;}
        public decimal cothis {get;set;}
        public bool pasoacont{get;set;}
        public int ctacon{get;set;}
        public int ctaconex{get;set;}
        public string sucursal {get;set;}
        public string nombreusu {get;set;}
        public decimal internos { get;set;}
        public decimal lisfac {get;set;} 
        public string notavta {get;set;}
        public bool esadeta{get;set;}
        public int tipocuit{get;set;}
        public string cae { get;set;}
        public string idpetic {get;set;}
        public DateTime  vencecae{get;set;}
        public string numerofe { get;set;}
        public string ctapase {get;set;}
        public string nrolibe { get;set;}
        public string contac {get;set;}
        public decimal estcue{get;set;}
        public DateTime exfecsalida{get;set;}
        public string exnroembar {get;set;}
        public string expuertosale { get;set;}
        public string expuertollega { get;set;}
        public string exnrobl {get;set;}
        public string exdestinofinal {get;set;}
        public string exincoterms {get;set;}
        public DateTime exfechafab {get;set;}
        public string exorigen {get;set;}
        public string expackag { get;set;}
        public string exmarca {get;set;}
        public string exmaterial {get;set;}
        public string exhscode {get;set;}
        public string obsrepgen {get;set;}
        public string exforwarder {get;set;}
        public decimal kilos {get;set;}
}
public class MiSubrub
{
    public string descri { get; set; }
    public decimal codigo { get; set; }
}
public class ArtVendidos
{
    public string codpro { get; set; }
    public string tipodoc { get; set; }
    public string letra { get; set; }
    public decimal punto { get; set; }
    public decimal numero { get; set; }
    public decimal cant { get; set; }
    public decimal precio { get; set; }
    public decimal prexcant { get; set; }
    public decimal bonitot { get; set; }
    public string razsoc { get; set; }
    public string descri { get; set; }
    public string nombre { get; set; }
    public decimal codven { get; set; }
    public decimal nrocli { get; set; }
    public DateTime fecha { get; set; }

}


public class TokenObject
{

        public string refresh_token { get; set; }
        public string access_token { get; set; }
        public int expires_in { get; set; }
        public string user_id { get; set; }
        public string scope { get; set; }
        public string token_type { get; set; }
        public TokenObject()
        {
            refresh_token = "";
            access_token = "";
            expires_in = 0;
            user_id = "";
            scope = "";
            token_type = "";
        }
    }

public class VtaxArtImporte
{
    public string codpro { get; set; }
    public string tipodoc { get; set; }
    public string letra { get; set; }
    public decimal punto { get; set; }
    public decimal numero { get; set; }
    public decimal cant { get; set; }
    public decimal precio { get; set; }
    public decimal prexcant { get; set; }
    public decimal bonito { get; set; }
    public string razsoc { get; set; }
    public string descri { get; set; }
    public decimal nrocli { get; set; }
}
public class RankingCompra
{
    public string codpro { get; set; }
    public string tipodoc { get; set; }
    public string letra{ get; set; }
    public decimal punto { get; set; }
    public decimal numero{ get; set; }
    public decimal cant{ get; set; }
    public decimal precio { get; set; }
    public decimal prexcant { get; set; }
    public decimal bonito{ get; set; }
    public string razsoc{ get; set; }
    public string descri { get; set; }
    public decimal nropro { get; set; }
}

public partial class Cotimesxmes
{
    public string codpro { get; set; }
    public string descri { get; set; }
    public string razsoc { get; set; }
    public string tipodoc { get; set; }
    public int rubro { get; set; }
    public decimal subrub { get; set; }
    public decimal marca { get; set; }
    public Nullable<int> nrocli { get; set; }
    public Nullable<decimal> zona { get; set; }
    public Nullable<decimal> codven { get; set; }
    public string tipo { get; set; }
    public string facturado { get; set; }
    public decimal pendientes { get; set; }
    public decimal saldo { get; set; }
    public decimal uno { get; set; }
    public decimal dos { get; set; }
    public decimal tres { get; set; }
    public decimal cuatro { get; set; }
    public decimal cinco { get; set; }
    public decimal seis { get; set; }
    public decimal siete { get; set; }
    public decimal ocho { get; set; }
    public decimal nueve { get; set; }
    public decimal diez { get; set; }
    public decimal once { get; set; }
    public decimal doce { get; set; }
}
public class MiTranspo
{
    public string direcc { get; set; }
    public string telefo { get; set; }
    public string razsoc { get; set; }
    public decimal codigo { get; set; }
}
public class MiMarca
{
    public string descripcion { get; set; }
    public decimal codigo { get; set; }
}
    public class MiRubro
{
    public string descri { get; set; }
    public int codigo { get; set; }
}

public partial class MiStock
{
    public int id { get; set; }
    public string codpro { get; set; }
    public string descri { get; set; }
    public int rubro { get; set; }
    public decimal subrub { get; set; }
    public decimal marca { get; set; }
    public int unimed { get; set; }
    public int proveed { get; set; }
    public string moneda { get; set; }
    public decimal ivagrupo { get; set; }
    public bool incluido { get; set; }
    public bool oferta { get; set; }
    public decimal costo { get; set; }
    public decimal precio1 { get; set; }
    public decimal precio2 { get; set; }
    public decimal precio3 { get; set; }
    public decimal precio4 { get; set; }
    public decimal precio5 { get; set; }
    public decimal precio6 { get; set; }
    public decimal stomin { get; set; }
    public decimal ppedido { get; set; }
    public decimal boniprod { get; set; }
    public decimal comiprod { get; set; }
    public decimal comipcob { get; set; }
    public decimal cantenv { get; set; }
    public decimal peso { get; set; }
    public decimal impint { get; set; }
    public Nullable<decimal> porcen1 { get; set; }
    public string descriEnvase { get; set; }
    public Nullable<int> codEnvase { get; set; }
    public string simboloEnvase { get; set; }
    public decimal saldo { get; set; }
    public decimal codcot { get;set;}
    public decimal internos { get; set; }

    //envase fechaact, fechveri, discont, costo, uxbulto, tipoart, cantmin, internos, dbo.envases.simbolo AS simboloEnvase, saldo


}
    public class empr
    {
        public string empresa { get; set; }
        public int id { get; set; }
    }
    
    public class RequestHTTP
    {
        public RequestHTTP()
        {
            error = "Sin errores";
            stackError = "Sin errores";
            fallo = false;
        }
        public RequestHTTP falla(Exception e)
        {
            this.error = e.Message;
            this.stackError = e.StackTrace;
            fallo = true;
            objeto = null;
            return this;
        }
        public object objeto { get; set; }
        public string error { get; set; }
        public bool fallo { get; set; }
        public string stackError { get; set; }
    }

    public class DicRequestHTTP
    {        
        public List<Dictionary<string, object>> objeto { get; set; }
        public string error { get; set; }
        public bool fallo { get; set; }
        public string stackError { get; set; }
        public DicRequestHTTP()
        {
            error = "Sin errores";
            stackError = "Sin errores";
            fallo = false;
        }
        public DicRequestHTTP falla(Exception e)
        {
            this.error = e.Message;
            this.stackError = e.StackTrace;
            fallo = true;
            objeto = null;
            return this;
        }

    } 

public class ListRequestHTTP
    {
        public ListRequestHTTP()
        {
            error = "Sin errores";
            stackError = "Sin errores";
            fallo = false;
        }
        public ListRequestHTTP falla(Exception e)
        {
            this.error = e.Message;
            this.stackError = e.StackTrace;
            fallo = true;
            objeto = null;
            return this;
        }

        public object objeto { get; set; }
        public string error { get; set; }
        public bool fallo { get; set; }
        public string stackError { get; set; }
    }


    public class presupCabecera
    {
        public int id { get; set; }
        public DateTime fecha { get; set; }
        public string tipodoc { get; set; }
        public string letra{ get; set; }
        public Int16 punto{ get; set; }
        public int numero{ get; set; }
        public int nrocli{ get; set; }
        public int hasta{ get; set; }
        public string razsoc{ get; set; }
    }
    public class Producto
    {
        public int id { get; set; }
        public string pathfoto { get; set; }
        public string codpro {get;set;}
        public string descri {get;set;}
        public decimal saldo {get;set;} 
        public bool oferta {get;set;} 
        public decimal boniprod {get;set;}
        public bool incluido {get;set;}
        public decimal porcen1 {get;set;}
        public string descriEnvase {get;set;}
        public int codEnvase {get;set;}
        public string simboloEnvase {get;set;}
        
    }
    public class ClienteBuscador
    {
        public int nrocli { get; set; }
        public string razsoc { get; set; }
        public string cuit { get; set; }
        public string fantasia { get; set; }
        public string direcc { get; set; }
        public string telef1 { get; set; }
        public string telef2 { get; set; }
        public string nombre { get; set; }
        public decimal codven { get; set; }
    }
    public class NotaPedidoPie{
        public string razsoc { get; set; }
        public string direccEntrega { get; set; }
        public string direcc { get; set; }
        public string cuit { get; set; }
        public decimal codigo { get; set; }
        public string horario { get; set; }
    }

    public class NotaPedidoCab
    {
        public int idCab{get;set;}
        public decimal punto{get;set;}
        public decimal nroped {get;set;}
        public int nrocli{get;set;}
        public string razsoc{get;set;}
        public int codven{get;set;}
        public string nombre{get;set;}
        public DateTime fechaing{get;set;}
        public DateTime parafecha{get;set;}
        public string direcc{get;set;}
        public int localiid{get;set;}
        public string locali { get; set; }
        public string codpos{get;set;}

}
    public class pedPendientes
    {
        public int id { get; set; }
        public DateTime fecha {get;set;}
        public decimal punto{get;set;}
        public decimal numero {get;set;}
        public int nrocli {get;set;}
        public string razsoc {get;set;}
        public string direcc {get;set;}
        public DateTime parafecha {get;set;}
        
    }
public class ventasAnuales
    {
        public int mes { get; set; }
        public int anio { get; set; }
        public decimal total { get; set; }
    }

public class MiConcepto
{
    public int codigo { get; set; }
    public string descri { get; set; }
    public int ctacon { get; set; }
    public bool inhabil { get; set; }
    public string grupo { get; set; }
    public int id { get; set; }
}

    public class ventasVendedores
    {
        public string nombre { get; set; }
        public decimal codven { get; set; }
        public decimal total { get; set; }
    }
public class ventasVendedor
{
   public string label { get; set; }
    public string nombre { get; set; }
    public decimal codven { get; set; }
    public decimal total { get; set; }
}

public class comprasProveedores
    {
        public string razsoc { get; set; }
        public decimal nropro { get; set; }
        public decimal total { get; set; }
    }

    public class concepto
    {
        public string descri { get; set; }
        public int codigo { get; set; }

    }
    public class PedPendientes
    {
        public int id { get; set; }
        public string descri { get; set; }
        public string articulo { get; set; }
        public decimal punto { get; set; }
        public decimal numero { get; set; }
        public DateTime fecha { get; set; }
        public decimal cantidad { get; set; }
        public string ocompra { get; set; }
    }

    public class UltimoPrecio
    {
        public DateTime fecha { get; set; }
        public string tipodoc { get; set; }
        public string letra { get; set; }
        public decimal punto { get; set; }
        public decimal numero { get; set; }
        public decimal neto { get; set; }
        public decimal bonif { get; set; }
        public decimal bonif1 { get; set; }
        public decimal bonito { get; set; }

    }

    public class ComprobanteAdeudado
    {
        public DateTime fecha { get; set; }
        public string tipodoc { get; set; }
        public string letra { get; set; }
        public decimal punto { get; set; }
        public decimal numero { get; set; }
        public string mon { get; set; }
        public decimal importe { get; set; }
        public decimal adeudado { get; set; }
        public DateTime vence { get; set; }
        public string simbolo { get; set; }
        public decimal cotizacion { get; set; }

    }


    public class ComprobanteSaldo
    {
        public string fecha { get; set; }
        public string tipodoc { get; set; }
        public string letra { get; set; }
        public decimal punto { get; set; }
        public decimal numero { get; set; }
        public decimal debe { get; set; }
        public decimal haber { get; set; }
        public decimal saldo { get; set; }
        public string tipdoco { get; set; }
        public string letrao { get; set; }
        public decimal puntoo { get; set; }
        public decimal numeroo { get; set; }
        public int id { get; set; }
        public int regi { get; set; }
        public int nrocli { get; set; }
        public string razsoc { get; set; }
        public bool verifi { get; set; }
        public string verifico { get; set; }
        public string simbolo { get; set; }
        public decimal recimanu { get; set; }
    }
    public class ComprometidoProducto
    {
        public string Producto { get; set; }
        public string Descripcion { get; set; }
        public decimal Comprometido { get; set; }
        public decimal Stock { get; set; }
    }

    public class CabeceraFactura
    {
        public string telef1 { get; set; }
        public int tipodocid { get; set; }
        public string numerofe { get; set; }
        public decimal cotizacion{ get; set; }
        public decimal cabSubt { get; set; }
        public decimal cabBonifto { get; set; }
        public decimal cabBonitot { get; set; }
        public decimal cabNeto { get; set; }
        public decimal cabExento { get; set; }
        public decimal cabIvai { get; set; }
        public decimal cabIvaidif { get; set; }
        public decimal cabIvanoi { get; set; }
        public decimal cabIvanoidif { get; set; }
        public decimal cabPorcenib { get; set; }
        public decimal cabRetinIB { get; set; }
        public string cabSimbolo { get; set; }
        public decimal cabTotal { get; set; }
        public string cae { get; set; }
        public decimal porins { get; set; }
        public decimal porinsdif { get; set; }
        public decimal pornoi { get; set; }
        public decimal pornoidif { get; set; }
        public DateTime vencecae { get; set; }
        public bool discrimina { get; set; }
        public string cabObserva { get; set; }
        public int id { get; set; }
        public string tipodoc { get; set; }
        public string letra { get; set; }
        public decimal punto { get; set; }
        public decimal numero { get; set; }
        public string empDireccion { get; set; }
        public string localiEmp { get; set; }
        public string empTelefono { get; set; }
        public string empMail { get; set; }
        public string empIva { get; set; }
        public DateTime facFecha{ get; set; }
        public string empCuit { get; set; }
        public DateTime empInicio{ get; set; }
        public string razsoc { get; set; }
        public string cliDirecc { get; set; }
        public string cliCuit { get; set; }
        public decimal cliRemito { get; set; }
        public string cliCondic { get; set; }
        public decimal nrocli{ get; set; }
        public decimal codven { get; set; }
    }

    public class DetalleFactura
    {
        public decimal ivartinoi { get; set; }
        public decimal ivartins { get; set; }
        public decimal bonif1 { get; set; }
        public decimal bonif { get; set; }
        public decimal bonifTotalArt { get; set; }
        public string codpro { get; set; }
        public string descri { get; set; }
        public decimal cant { get; set; }
        public string unimed { get; set; }
        public decimal precio { get; set; }
        public decimal bonito { get; set; }
        public decimal pins { get; set; }
        public decimal pivai { get; set; }
        public decimal pnoi { get; set; }
        public decimal pivanoi { get; set; }
        public decimal importe { get; set; }
    }
    public class MiProvincia
    {
        public int id { get; set; }
        public string nombre { get; set; }
    }
public class MiZona
{
    public decimal codigo { get; set; }
    public string descri { get; set; }
}

public class Generico
{
    public int id { get; set; }
    public string descri { get; set; }
}
    public class cabeceraRecibo
    {
        public string tipodoc { get; set; }
        public string letra { get; set; }
        public decimal punto { get; set; }
        public decimal numero { get; set; }
        public DateTime fecha { get; set; }
        public string telefono { get; set; }
        public string email { get; set; }
        public string cuit { get; set; }
        public string brutos { get; set; }
        public string nomEmpresa { get; set; }
        public string direccion { get; set; }
        public string cliRazsoc { get; set; }
        public string nomLocaliCli { get; set; }
        public int cliLocali{ get; set; }
        public string nombreProvin { get; set; }
        public string ivaDescri { get; set; }
        public string cliDirecc{ get; set; }
    public string nombre{ get; set; }
}
public class RankCompra
{
    public decimal total {get;set; }
    public decimal neto { get; set; }
    public decimal exento { get; set; }
    public decimal nropro { get; set; }
    public string razsoc { get; set; }
}

public partial class LArtVendxVende
{
    public string codpro { get; set; }
    public string descri { get; set; }
    public Nullable<int> unidades { get; set; }
    public Nullable<int> cantidad { get; set; }
    public Nullable<decimal> precio { get; set; }
    public Nullable<decimal> costo { get; set; }
    public decimal nrofac { get; set; }
    public decimal pendientes { get; set; }
    public Nullable<System.DateTime> fecha { get; set; }
    public Nullable<int> codRub { get; set; }
    public string descriRub { get; set; }
    public Nullable<decimal> codsubrub { get; set; }
    public string descriSubRu { get; set; }
    public Nullable<decimal> codven { get; set; }
    public Nullable<decimal> nrocli { get; set; }
    public string tipodoc { get; set; }
    public Nullable<decimal> cotizacion { get; set; }
    public string nomvende { get; set; }
    public string razsoc { get; set; }
    public Nullable<decimal> prexcant { get; set; }
}

public class DetalleRecibo
    {
        public string tipodoc { get; set; }
        public string letra { get; set; }
        public decimal punto { get; set; }
        public decimal numero { get; set; }
        public DateTime fecha { get; set; }
        public decimal cobrado { get; set;}
        public decimal bonificacion { get; set; }
        public decimal totalAplicado { get; set; }
    }

