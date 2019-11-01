using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Bson;
using Newtonsoft.Json;
using System.Web.Mvc;
using JustServicios;
using JustServicios.Clases;
using JustServicios.Clases.Controladores;

public class ControladorProducto
    {
        private static ControladorProducto instancia;
        private List<Dictionary<string,object>> productos;
        private ControladorProducto()
        {
            
        }

    internal stock getProductoCod(string codpro)
    {
        using (GestionEntities bd = new GestionEntities())
        {
            return bd.Database.SqlQuery<stock>("select * from stock where codpro like '" + codpro + "'").Single();
        }
    }

    internal RequestHTTP getDisponibles(string codpro, int empresa)
    {
        try
        {
            var req = new RequestHTTP();
            using (GestionEntities bd = new GestionEntities())
            {
                req.objeto = bd.Database.SqlQuery<ComprometidoProducto>("select * from ComprometidoporProducto(" + empresa + ") where Producto like '" + codpro + "'").Single();
                return req;
            }
        }catch(Exception e)
        {
            return new RequestHTTP().falla(e);
        }
    }

    public static ControladorProducto getCProducto()
        {
            if (instancia == null)
                instancia = new ControladorProducto();
            return instancia;
        }



        //Pedidos

        //usado en modal Pedidos
        public DicRequestHTTP getProductos(int empid)
        { 
            inicializarProductos(empid);
            JsonResult p = new JsonResult();
            
            p.Data = JsonConvert.SerializeObject(productos);
            var req = new DicRequestHTTP();
            req.objeto = productos;
            return req;
        }
        //usado en items pedidos
        public itemProducto getProducto(int idprod, int lista, int empid)
        {
        // try
        // {
            decimal precioLista;
            List<artasoc> artaso;
            List<string> arta = new List<string>();
            var prod = inicializarProductos(empid);
            Dictionary<string,object> p = productos.FirstOrDefault(dic => Convert.ToInt32(dic.Single(elem => elem.Key == "id").Value) == idprod);
            string codpro = p.Single(a => a.Key == "codpro").Value.ToString();
            using (GestionEntities bd = new GestionEntities() )
                artaso = bd.artasoc.Where(a => a.codpro == codpro).ToList();
                foreach (artasoc q in artaso)
                arta.Add(q.componen);

            switch (lista)
            {
                case 1:
                precioLista = (decimal)p.Single(a => a.Key == "precio1").Value;
                break;
            case 2:
                precioLista = (decimal)p.Single(a => a.Key == "precio2").Value; ;
                break;
            case 3:
                precioLista = (decimal)p.Single(a => a.Key == "precio3").Value; ;
                break;
            case 4:
                precioLista = (decimal)p.Single(a => a.Key == "precio4").Value; ;
                break;
            case 5:
                precioLista = (decimal)p.Single(a => a.Key == "precio5").Value; ;
                break;
            case 6:
                precioLista = (decimal)p.Single(a => a.Key == "precio6").Value;
                break;
            default:
                precioLista = (decimal)p.Single(a => a.Key == "costo").Value;
                break;
          }
        string descri = p.Single(a => a.Key == "descri").Value.ToString();
        decimal impint = (decimal)p.Single(a => a.Key == "impint").Value;
        itemProducto ip = new itemProducto(1, descri, precioLista, 0, 0, 0,impint, codpro, arta.ToArray() );
            return ip;
         /*   }
            catch (Exception)
            {
              var p = new itemProducto();
              p.codpro = "no existe";
              return p;
            }*/
        }

    public RequestHTTP getProductos (string value, int empid, int offset)
    {
        var req = new RequestHTTP();
        try
        {
            List<Producto> lista;
            using (GestionEntities bd = new GestionEntities())
            {
                bd.Database.CommandTimeout = 30;
                if (value == "0")
                     lista = bd.Database.SqlQuery<Producto>("select pathfoto, stock.codpro, stock.descri, oferta, boniprod, incluido, ivaart.porcen1, envases.descri AS descriEnvase, envases.codigo AS codEnvase,envases.simbolo AS simboloEnvase, isnull(stk.saldo,0) saldo FROM stock LEFT OUTER JOIN ivaart ON ivaart.codigo = stock.ivagrupo LEFT OUTER JOIN envases ON envases.id = stock.envase LEFT OUTER JOIN SaldoSTKALL(" + empid + ") AS stk ON stk.codpro = stock.codpro").ToList();
                else
                    lista = bd.Database.SqlQuery<Producto>("select pathfoto, stock.id, stock.codpro, stock.descri, oferta, boniprod, incluido, ivaart.porcen1, envases.descri AS descriEnvase, envases.codigo AS codEnvase,envases.simbolo AS simboloEnvase, isnull(stk.saldo,0) saldo FROM stock LEFT OUTER JOIN ivaart ON ivaart.codigo = stock.ivagrupo LEFT OUTER JOIN envases ON envases.id = stock.envase LEFT OUTER JOIN SaldoSTKALL(" + empid + ") AS stk ON stk.codpro = stock.codpro where stock.codpro like '%" + value + "%' or stock.descri like '%" + value + "%' order by descri offset " + offset + " rows fetch next 20 row only").ToList();
            }
            foreach (var prod in lista)
            {
                if (prod.pathfoto != "")
                {
                    prod.pathfoto = acortarPath(prod.pathfoto);
                }
                if (prod.pathfoto == "")
                {
                    prod.pathfoto = "empty";
                }
            }
            req.objeto = lista;
                return req;
        } catch (Exception e)
        {
            return req.falla(e);
        }
    }

    private string acortarPath(string path)
    {
        int index;
            index = path.IndexOf(@"\");
        for(int i = 0; i < 3; i++)
        {
            if(index >= 0)
            {
                index = path.IndexOf(@"\", index+1);
            }
        }
        if (index >= 0)
            path = path.Substring(index);

        return path;
    }

    

    public bool productoUnico(string value, int empid)
    {
        try
        {
            using (GestionEntities bd = new GestionEntities())
            {
                if (bd.stocksF(empid).Where(a => a.codpro.Contains(value) || a.descri.Contains(value)).Count() == 1)
                    return true;
                else
                    return false;
            }
        }catch(Exception e)
        {
            return false;
        }

    }

    public string cambiarCodigo(JObject codigos)
    {
        string viejoCodigo = codigos["codigo"].ToString(); ;
        string nuevoCodigo = codigos["nuevoCodigo"].ToString();

        if (!existeProducto(nuevoCodigo))
        {

            try
            {
                using (GestionEntities bd = new GestionEntities())
                {
                    bd.Database.ExecuteSqlCommand("exec CambioCodStk '" + viejoCodigo + "','" + nuevoCodigo + "'");
                }
                return "true";
            }
            catch (Exception e)
            {
                return "error";
            }
        }
        else
        {
            return "Existe";
        }
    }

    public bool existeProducto(string codigo)
    {
        using(GestionEntities bd = new GestionEntities())
        {
            if (bd.stock.Where(a => a.codpro == codigo).Count() >= 1)
                return true;
            else
                return false;
        }
    }

    private List<Dictionary<string,object>> inicializarProductos(int empid)
    {
        if (productos == null)
        {
            productos = new ControladorDatos().getData(" SELECT dbo.stock.id, dbo.stock.codpro, dbo.stock.cbarras, dbo.stock.dun14, dbo.stock.codorigi, " +
                "dbo.stock.coriginal, dbo.stock.codncm, dbo.stock.descri, dbo.stock.despacho, dbo.stock.verifico, dbo.stock.color, dbo.stock.pathfoto," +
                " dbo.stock.pathetiq, dbo.stock.usuestr, dbo.stock.descrilar, dbo.stock.usoart, dbo.stock.observa, dbo.stock.rubro, dbo.stock.subrub," +
                " dbo.stock.marca, dbo.stock.unimed, dbo.stock.proveed, dbo.stock.moneda, dbo.stock.envase, dbo.stock.ivagrupo, dbo.stock.ctacon, " +
                "dbo.stock.fecha, dbo.stock.fecmodcos, dbo.stock.alta, dbo.stock.fechaact, dbo.stock.fechveri, dbo.stock.fecprom, dbo.stock.fechestr," +
                " dbo.stock.fechpped, dbo.stock.cosfverif, dbo.stock.incluido, dbo.stock.artret, dbo.stock.oferta, dbo.stock.descomp, dbo.stock.vtaxbul, " +
                "dbo.stock.discont, dbo.stock.noborrarelart, dbo.stock.msgstkptoped, dbo.stock.costo, dbo.stock.precio1, dbo.stock.precio2, dbo.stock.precio3," +
                " dbo.stock.precio4, dbo.stock.precio5, dbo.stock.precio6, dbo.stock.stomin, dbo.stock.ppedido, dbo.stock.boniprod, dbo.stock.comiprod, " +
                "dbo.stock.comipcob, dbo.stock.cantenv, dbo.stock.peso, dbo.stock.promedio, dbo.stock.meses, dbo.stock.canths, dbo.stock.uxbulto, " +
                "dbo.stock.prelis, dbo.stock.tipoart, dbo.stock.impint, dbo.stock.impasoc, dbo.stock.presug, dbo.stock.cantmin, dbo.stock.porceasoc, " +
                "dbo.stock.internos, dbo.stock.modifdescr, dbo.stock.coddisp, dbo.stock.codsku, dbo.stock.uxdisp, dbo.stock.exigelote, dbo.stock.codcot," +
                " dbo.ivaart.porcen1, dbo.envases.descri AS descriEnvase, dbo.envases.codigo AS codEnvase, dbo.envases.simbolo AS simboloEnvase," +
                " isnull(stk.saldo, 0) saldo" +
                " FROM            dbo.stock" +
                " LEFT OUTER JOIN dbo.ivaart ON dbo.ivaart.codigo = dbo.stock.ivagrupo" +
                " LEFT OUTER JOIN dbo.envases ON dbo.envases.id = dbo.stock.envase" +
                " LEFT OUTER JOIN dbo.SaldoSTKALL(" + empid + ") AS stk ON stk.codpro = dbo.stock.codpro").objeto;
            return productos;
        }
        else
        {
            return productos;
        }
    }
                 
    }
