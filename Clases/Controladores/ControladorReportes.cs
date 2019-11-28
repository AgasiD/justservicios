using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Linq.Dynamic;
using JustServicios.Clases;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace JustServicios
{
    public class ControladorReportes
    {
        private static ControladorReportes instancia;
        private static ControladorProducto cProducto = ControladorProducto.getCProducto();
        private ControladorReportes()
        {

        }

        public static ControladorReportes getCReporter()
        {
            if (instancia == null)
                instancia = new ControladorReportes();
            return instancia;
        }

        //----------------------------------------------------------CLIENTES
        public RequestHTTP getClientes(int codven, bool veTodos, int offset)
        {
            var req = new RequestHTTP();
            try
            {
                List<ClienteBuscador> clientes;
                using (GestionEntities bd = new GestionEntities())
                {

                    if (veTodos)
                        req.objeto = bd.Database.SqlQuery<ClienteBuscador>("select nrocli, razsoc, cuit, fantasia, direcc, telef1, telef2,  codven from cliente order by nrocli offset " + offset + " rows fetch next 20 row only").ToList();
                    else
                        req.objeto = bd.Database.SqlQuery<ClienteBuscador>("select nrocli, razsoc, cuit, fantasia, direcc, telef1, telef2, codven from cliente where codven = " + codven + "  order by nrocli   offset " + offset + " rows fetch next 20 row only").ToList();
                }

                return req;
            }catch(Exception e)
            {
                return req.falla(e);
            }
        }

        internal RequestHTTP getClientesFiltro(string query, int codven, bool veTodos, int offset)
        {
            var req = new RequestHTTP();
            try
            {
                query = query.Replace("@", "%");
                int nrocli;
                using (GestionEntities bd = new GestionEntities())
                {
                    if (veTodos)
                        req.objeto = bd.Database.SqlQuery<ClienteBuscador>("select nrocli, razsoc, cuit, fantasia, direcc, telef1, telef2, codven from cliente where " + query + " order by nrocli  offset " + offset + " rows fetch next 20 row only").ToList();
                    else
                        req.objeto = bd.Database.SqlQuery<ClienteBuscador>("select nrocli, razsoc, cuit, fantasia, direcc, telef1, telef2, codven from cliente where " + query + " and codven = " + codven + "  order by nrocli offset " + offset + " rows fetch next 20 row only").ToList();
                    return req;
                    
                }
            }catch(Exception e)
            {
                return req.falla(e);
            }
        }

        internal int cantClientes(int nrocli)
        {

            using (GestionEntities bd = new GestionEntities())
            {
                return bd.cliente.Where(a => a.nrocli == nrocli).Count();
            }
        }

        public RequestHTTP getCliente(int paran)
        {
            cliente cli;
            var req = new RequestHTTP();
            try
            {
                using (GestionEntities bd = new GestionEntities())
                {
                    if (bd.cliente.Where(a => a.nrocli == paran).Count() == 1)
                        cli = bd.cliente.Single(a => a.nrocli == paran);
                    else
                    {
                        cli = new cliente();
                        cli.nrocli = 0;
                    }
                    req.objeto = cli;
                    return req;
                }
            }catch(Exception e)
            {
                return req.falla(e);
            }
        }

        //----------------------------------------------------------VENDEDORES

        public RequestHTTP getVendedores()
        {
            var req = new RequestHTTP();
            try
            {
                List<vende> vendedores;
                using (GestionEntities bd = new GestionEntities())
                {
                    req.objeto = bd.vende.ToList();
                }
                return req;
            }
            catch (Exception e)
            {
                return req.falla(e);
            }
        }
        internal RequestHTTP getVendedoresFiltro(string param)
        {
            var req = new RequestHTTP();
            try
            {
                int nrocli;
                using (GestionEntities bd = new GestionEntities())
                {
                    if (int.TryParse(param, out nrocli))
                    {
                        nrocli = Convert.ToInt32(param);
                        req.objeto = bd.Database.SqlQuery<vendedor>("select codven, nombre from vende where codven = " + param + " and nombre like '%"+param+"%'").ToList();
                    }
                    else
                    {
                        req.objeto = bd.Database.SqlQuery<vendedor>("select codven, nombre from vende where nombre like '%" + param + "%'").ToList();
                    }
                    return req;
                }
            }
            catch(Exception e)
            {
                return req.falla(e);
            }
}

        public RequestHTTP GetVende(int param)
        {
            var req = new RequestHTTP();
            try
            {
                
                using (GestionEntities bd = new GestionEntities())
                {
                    req.objeto = bd.Database.SqlQuery<vendedor>("select codven, nombre from vende where codven = " + param).ToList();
                }
                return req;
            }
            catch(Exception e)
            {
                return req.falla(e);
            }
        }

        //----------------------------------------------------------ZONAS


        public RequestHTTP getZonas()
        {
            var req = new RequestHTTP();
            try
            {
                using (GestionEntities bd = new GestionEntities())
                {
                    req.objeto = bd.Database.SqlQuery<MiZona>("select codigo, descri from zonas").ToList();
                }
                return req;
            }
            catch(Exception e)
            {
                return req.falla(e);
            }
        }


        internal RequestHTTP getZonasFiltro(string param)
        {

            var req = new RequestHTTP();
            try
            {
                using (GestionEntities bd = new GestionEntities())
                {
                    req.objeto = bd.Database.SqlQuery<MiZona>("select codigo, descri from zonas where codigo like '" + param + "' or descri like '%" + param +"%'").ToList();
                }
                return req;
            }
            catch (Exception e)
            {
                return req.falla(e);
            }
        }


        internal RequestHTTP getZona(int param)
        {
            var req = new RequestHTTP();
            try
            {
                using (GestionEntities bd = new GestionEntities())
                {
                    req.objeto = bd.Database.SqlQuery<MiZona>("select codigo, descri from zonas where codigo = " + param).ToList();
                }
                return req;
            }
            catch (Exception e)
            {
                return req.falla(e);
            }
        }
        //----------------------------------------------------------RUBROS
        internal RequestHTTP getRubrosFiltro(string param)
        {
            var req = new RequestHTTP();
            try
            {
                using (GestionEntities bd = new GestionEntities())
                {
                    req.objeto = bd.Database.SqlQuery<MiRubro>("select codigo, descri from rubros where codigo like '" + param + "' or descri like '%"+param+ "%'").ToList();
                }
                return req;
            }
            catch (Exception e)
            {
                return req.falla(e);
            }
        }
        internal RequestHTTP getRubro(int param)
        {
            var req = new RequestHTTP();
            try
            {
                using (GestionEntities bd = new GestionEntities())
                {
                    req.objeto = bd.Database.SqlQuery<MiRubro>("select codigo, descri from rubros where codigo = "+ param).ToList();
                }
                return req;
            }
            catch (Exception e)
            {
                return req.falla(e);
            }
        }
        internal RequestHTTP getRubros()
        {
            var req = new RequestHTTP();
            try
            {
                using (GestionEntities bd = new GestionEntities())
                {
                    req.objeto = bd.Database.SqlQuery<MiRubro>("select codigo, descri from rubros").ToList();
                }
                return req;
            }
            catch (Exception e)
            {
                return req.falla(e);
            }
        }

        //----------------------------------------------------------SUBRUBROS

        internal RequestHTTP getSubRubros()
        {
            var req = new RequestHTTP();
            try
            {
                using (GestionEntities bd = new GestionEntities())
                {
                    req.objeto = bd.Database.SqlQuery<MiSubrub>("select codigo, descri from subrub").ToList();
                }
                return req;
            }
            catch (Exception e)
            {
                return req.falla(e);
            }
        }

        internal RequestHTTP getSubRubrosFiltro(string param)
        {
            var req = new RequestHTTP();
            try
            {
                using (GestionEntities bd = new GestionEntities())
                {
                    req.objeto = bd.Database.SqlQuery<MiSubrub>("select codigo, descri from subrub where codigo like '" + param + "' or descri like '%" + param + "%'").ToList();
                }
                return req;
            }
            catch (Exception e)
            {
                return req.falla(e);
            }
        }
        internal RequestHTTP getSubRubro(int param)
        {
            var req = new RequestHTTP();
            try
            {
                using (GestionEntities bd = new GestionEntities())
                {
                    req.objeto = bd.Database.SqlQuery<MiSubrub>("select codigo, descri from subrub where codigo = " + param).ToList();
                }
                return req;
            }
            catch (Exception e)
            {
                return req.falla(e);
            }
        }

        //----------------------------------------------------------PROVINCIAS

        public RequestHTTP getPrvincias()
        {
            var req = new RequestHTTP();
            try
            {
                List<provincias> provincias;
                using (GestionEntities bd = new GestionEntities())
                {
                    req.objeto = bd.Database.SqlQuery<MiProvincia>("select nombre, id from provincias").ToList() ;
                }
                return req;
            }catch(Exception e)
            {
                return req.falla(e);
            }
        }


        internal RequestHTTP getProvincia(int param)
        {
            var req = new RequestHTTP();
            try
            {
                List<provincias> provincias;
                using (GestionEntities bd = new GestionEntities())
                {
                    req.objeto = bd.Database.SqlQuery<MiProvincia>("select nombre, id from provincias where id = " + param).ToList();
                }
                return req;
            }
            catch (Exception e)
            {
                return req.falla(e);
            }

        }


        internal RequestHTTP getProvinciasFiltro(string param)
        {
            var req = new RequestHTTP();
            try
            {
                List<provincias> provincias;
                using (GestionEntities bd = new GestionEntities())
                {
                    req.objeto = bd.Database.SqlQuery<MiProvincia>("select nombre, id from provincias where nombre like '%" + param + "%' or id like '" + param + "'").ToList();
                }
                return req;
            }
            catch (Exception e)
            {
                return req.falla(e);
            }
        }


        //----------------------------------------------------- CONCEPTOS

        internal RequestHTTP getConceptosFiltro(string param)
        {
            var req = new RequestHTTP();
            try
            {
                using (GestionEntities bd = new GestionEntities())
                {
                    req.objeto = bd.Database.SqlQuery<concepto>("select codigo, descri from concepto where codigo like '" + param + "' or descri like '%" + param + "%'").ToList();
                }
                return req;
            }
            catch (Exception e)
            {
                return req.falla(e);
            }
        }

        internal RequestHTTP getConceptos()
        {
            var req = new RequestHTTP();
            try
            {
                using (GestionEntities bd = new GestionEntities())
                {
                    req.objeto = bd.Database.SqlQuery<MiConcepto>("select codigo, descri from concepto").ToList();
                }
                return req;
            }
            catch (Exception e)
            {
                return req.falla(e);
            }
        }
        internal RequestHTTP getConcepto(int param)
        {
            var req = new RequestHTTP();
            try
            {
                using (GestionEntities bd = new GestionEntities())
                {
                    req.objeto = bd.Database.SqlQuery<MiConcepto>("select codigo, descri from concepto where codigo = " + param).ToList();
                }
                return req;
            }
            catch (Exception e)
            {
                return req.falla(e);
            }
        }

        //----------------------------------------------------------PROVEEDORES
        internal RequestHTTP getProveedores()
        {
            var req = new RequestHTTP();
            try
            {
                using (GestionEntities bd = new GestionEntities())
                {
                    req.objeto = bd.Database.SqlQuery<MiProvee>("select nropro, razsoc from provee").ToList();
                }
                return req;
            }
            catch (Exception e)
            {
                return req.falla(e);
            }
        }

      

        internal RequestHTTP getProveedoresFiltro(string param)
        {
            var req = new RequestHTTP();
            try
            {
                using (GestionEntities bd = new GestionEntities())
                {
                    req.objeto = bd.Database.SqlQuery<concepto>("select nropro, razsoc from provee where nropro like '" + param + "' or razsoc like '%" + param + "%'").ToList();
                }
                return req;
            }
            catch (Exception e)
            {
                return req.falla(e);
            }
        }
        internal RequestHTTP getProveedor(int param)
        {
            var req = new RequestHTTP();
            try
            {
                using (GestionEntities bd = new GestionEntities())
                {
                    req.objeto = bd.Database.SqlQuery<MiProvee>("select nropro, razsoc from provee where nropro = " + param).ToList();
                }
                return req;
            }
            catch (Exception e)
            {
                return req.falla(e);
            }
        }

            internal RequestHTTP getPuntos(int empresa)
        {
            var req = new RequestHTTP();
            try
            {
                using (GestionEntities bd = new GestionEntities())
                {
                    req.objeto = bd.factura.Where(a => a.EmpresaId == empresa).ToList();
                }
                return req;
            }
            catch (Exception e)
            {
                return req.falla(e);
            }
        }

        internal RequestHTTP getVentasAnuales(int empresa, bool cotizaciones, string desde, string hasta, int operacion, bool checkope)
        {
            var req = new RequestHTTP();
            try
            {
                using (GestionEntities bd = new GestionEntities())
                {
                    if (operacion == 1)
                    {
                        if (cotizaciones)
                        {
                            if (checkope)
                                req.objeto = bd.Database.SqlQuery<ventasAnuales>(" select  * from ( select MONTH(fecha) mes, YEAR(fecha) anio, sum(total) total from ivaven where empresaid = " + empresa + " and fecha >='" + desde + "' and fecha <= '" + hasta + "' group by YEAR(fecha), MONTH(fecha) union select MONTH(fecha) mes, YEAR(fecha) anio, sum(total) total  from remitoCab where empresaid= " + empresa + " and facturado = 'N' and fecha >='" + desde + "' and fecha <= '" + hasta + "' group by YEAR(fecha), MONTH(fecha)) consul order by anio, mes").ToList();// .Where(a => a.EmpresaId == empresa).ToList();           
                            else
                                req.objeto = bd.Database.SqlQuery<ventasAnuales>(" select  * from ( select MONTH(fecha) mes, YEAR(fecha) anio, sum(total) total from ivaven where empresaid = " + empresa + " and fecha >='" + desde + "' and fecha <= '" + hasta + "' group by YEAR(fecha), MONTH(fecha)) consul order by anio, mes").ToList();// .Where(a => a.EmpresaId == empresa).ToList();              
                        }
                        else
                        {
                            if (checkope)
                                req.objeto = bd.Database.SqlQuery<ventasAnuales>(" select  * from ( select MONTH(fecha) mes, YEAR(fecha) anio, sum(total) total from ivaven where empresaid = " + empresa + " and tipodoc <> 'CT' and fecha >='" + desde + "' and fecha <= '" + hasta + "' group by YEAR(fecha), MONTH(fecha) union select MONTH(fecha) mes, YEAR(fecha) anio, sum(total) total  from remitoCab where empresaid= " + empresa + " and  facturado = 'N' and fecha >='" + desde + "' and fecha <= '" + hasta + "' group by YEAR(fecha), MONTH(fecha)) consul order by anio, mes").ToList();// .Where(a => a.EmpresaId == empresa).ToList();           
                            else
                                req.objeto = bd.Database.SqlQuery<ventasAnuales>(" select  * from ( select MONTH(fecha) mes, YEAR(fecha) anio, sum(total) total from ivaven where empresaid = " + empresa + " and tipodoc <> 'CT' and fecha >='" + desde + "' and fecha <= '" + hasta + "' group by YEAR(fecha), MONTH(fecha)) consul order by anio, mes").ToList();// .Where(a => a.EmpresaId == empresa).ToList();              
                        }
                    }
                    if (operacion == 2)
                    {
                        if (cotizaciones)
                        {
                            if (checkope)
                                req.objeto = bd.Database.SqlQuery<ventasAnuales>(" select  * from ( select MONTH(fecha) mes, YEAR(fecha) anio, sum(total) total from ivacom where empresaid = " + empresa + " and  fecha >='" + desde + "' and fecha <= '" + hasta + "' group by YEAR(fecha), MONTH(fecha)) consul order by anio, mes").ToList();// .Where(a => a.EmpresaId == empresa).ToList();           
                            else
                                req.objeto = bd.Database.SqlQuery<ventasAnuales>(" select  * from ( select MONTH(fecha) mes, YEAR(fecha) anio, sum(total) total from ivacom where empresaid = " + empresa + " and fecha >='" + desde + "' and fecha <= '" + hasta + "' group by YEAR(fecha), MONTH(fecha)) consul order by anio, mes").ToList();// .Where(a => a.EmpresaId == empresa).ToList();              
                        }
                        else
                        {
                            if (checkope)
                                req.objeto = bd.Database.SqlQuery<ventasAnuales>(" select  * from ( select MONTH(fecha) mes, YEAR(fecha) anio, sum(total) total from ivacom where empresaid = " + empresa + "and tipodoc <> 'CT' and fecha >='" + desde + "' and fecha <= '" + hasta + "' group by YEAR(fecha), MONTH(fecha)) consul order by anio, mes").ToList();// .Where(a => a.EmpresaId == empresa).ToList();           
                            else
                                req.objeto = bd.Database.SqlQuery<ventasAnuales>(" select  * from ( select MONTH(fecha) mes, YEAR(fecha) anio, sum(total) total from ivacom where empresaid = " + empresa + " and tipodoc <> 'CT' and fecha >='" + desde + "' and fecha <= '" + hasta + "' group by YEAR(fecha), MONTH(fecha)) consul order by anio, mes").ToList();// .Where(a => a.EmpresaId == empresa).ToList();              
                        }
                    }
                    if (operacion == 3)
                    {
                        req.objeto = bd.Database.SqlQuery<ventasAnuales>(" select  * from ( select MONTH(fecha) mes, YEAR(fecha) anio, sum(total) total from presupc where empresa = " + empresa + " and fecha >='" + desde + "' and fecha <= '" + hasta + "' group by YEAR(fecha), MONTH(fecha)) consul order by anio, mes").ToList();// .Where(a => a.EmpresaId == empresa).ToList();              
                    }

                    return req;
                }
            }catch(Exception e)
            {
                return req.falla(e);
            }

        }

        internal RequestHTTP getVentasPorVendedores(int empresa, bool cotizaciones, string desde, string hasta)
        {
            var req = new RequestHTTP();
            try
            {
                using (GestionEntities bd = new GestionEntities())
                {
                    if (!cotizaciones)
                        req.objeto = bd.Database.SqlQuery<ventasVendedores>("select isnull(max(nombre),'') nombre, isnull(max(vende.codven),0) codven, sum(total) total from ivaven left join cliente on cliente.nrocli = ivaven.nrocli left join vende on vende.codven = cliente.codven where empresaid =" + empresa + " and fecha >= '" + desde + "' and fecha <= '" + hasta + "' and ivaven.tipodoc <> 'CT' group by vende.codven order by total desc").ToList();
                    else
                        req.objeto = bd.Database.SqlQuery<ventasVendedores>("select isnull(max(nombre),'') nombre, isnull(max(vende.codven),0) codven, sum(total) total from ivaven left join cliente on cliente.nrocli = ivaven.nrocli left join vende on vende.codven = cliente.codven where empresaid = " + empresa + " and fecha >= '" + desde + "' and fecha <= '" + hasta + "' group by vende.codven order by total desc").ToList();
                }
                return req;
            }catch(Exception e)
            {
                return req.falla(e);
            }
        }

        internal RequestHTTP getVentasPorVendedor(int empresa, int codven, bool cotizaciones, string desde, string hasta, int group)
        {
            var req = new RequestHTTP();
            try
            {
                using (GestionEntities bd = new GestionEntities())
                {

                    switch (group)
                    {
                        case 1: //agrupado por dia
                            if (!cotizaciones)
                                req.objeto = bd.Database.SqlQuery<ventasVendedor>("select sum(total) total, convert(char(10),fecha,103) label, max(vende.nombre) nombre, max(vende.codven) codven  from ivaven left join cliente on cliente.nrocli = ivaven.nrocli left join vende on vende.codven = cliente.codven where empresaid = " + empresa + " and fecha >= '" + desde + "' and fecha <= '" + hasta + "' and vende.codven = " + codven + " and ivaven.tipodoc <> 'CT' group by fecha order by fecha").ToList();
                            else
                                req.objeto = bd.Database.SqlQuery<ventasVendedor>("select sum(total) total, convert(char(10),fecha,103) label, max(vende.nombre) nombre, max(vende.codven) codven   from ivaven left join cliente on cliente.nrocli = ivaven.nrocli left join vende on vende.codven = cliente.codven where empresaid = " + empresa + " and fecha >= '" + desde + "' and fecha <= '" + hasta + "' and vende.codven = " + codven + " group by fecha order by fecha").ToList();
                            break;
                        case 2: //agrupado por semana
                            if (!cotizaciones)
                                req.objeto = bd.Database.SqlQuery<ventasVendedor>(" select sum(total) total,  convert(varchar,DATEPART(wk, fecha))+'/'+convert(varchar,year(fecha)) label, max(vende.nombre) nombre, max(vende.codven) codven  from ivaven left join cliente on cliente.nrocli = ivaven.nrocli left join vende on vende.codven = cliente.codven where empresaid = " + empresa + " and fecha >= '" + desde + "' and fecha <= '" + hasta + "' and vende.codven = " + codven + " and ivaven.tipodoc <> 'CT' group by DATEPART(wk, fecha), year(fecha)  having sum(netocob)<> 0 order by  year(fecha), DATEPART(wk, fecha) ").ToList();
                            else
                                req.objeto = bd.Database.SqlQuery<ventasVendedor>(" select sum(total) total, convert(varchar,DATEPART(wk, fecha))+'/'+convert(varchar,year(fecha)) label, max(vende.nombre) nombre, max(vende.codven) codven  from ivaven left join cliente on cliente.nrocli = ivaven.nrocli left join vende on vende.codven = cliente.codven where empresaid = " + empresa + " and fecha >= '" + desde + "' and fecha <= '" + hasta + "' and vende.codven = " + codven+ " group by DATEPART(wk, fecha), year(fecha)  having sum(netocob)<> 0 order by  year(fecha), DATEPART(wk, fecha) ").ToList();
                            break;
                        case 3: //agrupado x mes
                            if (!cotizaciones)
                                req.objeto = bd.Database.SqlQuery<ventasVendedor>(" select sum(total) total, convert(varchar,month(fecha))+'/'+ convert(varchar,year(fecha)) label,max(vende.nombre) nombre, max(vende.codven) codven  from ivaven left join cliente on cliente.nrocli = ivaven.nrocli left join vende on vende.codven = cliente.codven where empresaid = " + empresa+" and fecha >= '"+desde+"' and fecha <= '"+hasta+"' and vende.codven = "+codven+ " and ivaven.tipodoc <> 'CT' group by    month(fecha), year(fecha)  having sum(netocob)<> 0 order by  year(fecha), month(fecha) ").ToList();
                            else
                                req.objeto = bd.Database.SqlQuery<ventasVendedor>("select sum(total) total, convert(varchar,month(fecha))+'/'+ convert(varchar,year(fecha)) label, max(vende.nombre) nombre, max(vende.codven) codven  from ivaven left join cliente on cliente.nrocli = ivaven.nrocli left join vende on vende.codven = cliente.codven where empresaid = " + empresa + " and fecha >= '" + desde + "' and fecha <= '" + hasta + "' and vende.codven = " + codven + " group by  month(fecha), year(fecha)  having sum(netocob)<> 0 order by  year(fecha), month(fecha) ").ToList();
                            break;

                    }
                }
                return req;
            }
            catch (Exception e)
            {
                return req.falla(e);
            }
        }

        internal RequestHTTP getCobranzasPorVendedor(int empresa, int codven, bool cotizaciones, string desde, string hasta, int group)
        {
            var req = new RequestHTTP();
            try
            {
                using (GestionEntities bd = new GestionEntities())
                {

                    switch (group)
                    {
                        case 1: //agrupado por dia
                            if (!cotizaciones)
                                req.objeto = bd.Database.SqlQuery<ventasVendedor>("select sum(netocob) total, convert(char(10),fecha,103) label, max(vende.nombre) nombre, max(vende.codven) codven from clicta left join cliente on cliente.nrocli = clicta.nrocli left join vende on vende.codven = cliente.codven where empresa = " + empresa+" and fecha >= '"+desde+"' and fecha <= '" + hasta + "' and vende.codven = " + codven + " and clicta.tipdoco <> 'CT' group by fecha having sum(netocob)<> 0 order by fecha ").ToList();
                            else
                                req.objeto = bd.Database.SqlQuery<ventasVendedor>(" select sum(netocob) total, convert(char(10),fecha,103) label, max(vende.nombre) nombre, max(vende.codven) codven  from clicta left join cliente on cliente.nrocli = clicta.nrocli left join vende on vende.codven = cliente.codven where empresa = " + empresa + " and fecha >= '" + desde + "' and fecha <= '" + hasta + "' and vende.codven = " + codven + " group by fecha having sum(netocob)<> 0").ToList();

                            break;
                        case 2: //agrupado por semana
                            if (!cotizaciones)
                                req.objeto = bd.Database.SqlQuery<ventasVendedor>(" select sum(netocob) total,  convert(varchar,DATEPART(wk, fecha))+'/'+convert(varchar,year(fecha)) label, max(vende.nombre) nombre, max(vende.codven) codven  from clicta left join cliente on cliente.nrocli = clicta.nrocli left join vende on vende.codven = cliente.codven where empresa = " + empresa + " and fecha >= '" + desde + "' and fecha <= '" + hasta + "' and vende.codven = " + codven + " and clicta.tipdoco <> 'CT' group by DATEPART(wk, fecha), year(fecha) having sum(netocob)<> 0 order by  year(fecha), DATEPART(wk, fecha) ").ToList();
                            else
                                req.objeto = bd.Database.SqlQuery<ventasVendedor>(" select sum(netocob) total, convert(varchar,DATEPART(wk, fecha))+'/'+convert(varchar,year(fecha)) label, max(vende.nombre) nombre, max(vende.codven) codven  from clicta left join cliente on cliente.nrocli = clicta.nrocli left join vende on vende.codven = cliente.codven where empresa = " + empresa + " and fecha >= '" + desde + "' and fecha <= '" + hasta + "' and vende.codven = " + codven + " group by DATEPART(wk, fecha), year(fecha)  having sum(netocob)<> 0 order by  year(fecha), DATEPART(wk, fecha) ").ToList();
                            break;
                        case 3: //agrupado x mes
                            if (!cotizaciones)
                                req.objeto = bd.Database.SqlQuery<ventasVendedor>(" select sum(netocob) total, convert(varchar,month(fecha))+'/'+ convert(varchar,year(fecha)) label,max(vende.nombre) nombre, max(vende.codven) codven  from clicta left join cliente on cliente.nrocli = clicta.nrocli left join vende on vende.codven = cliente.codven where empresa = " + empresa + " and fecha >= '" + desde + "' and fecha <= '" + hasta + "' and vende.codven = " + codven + " and clicta.tipdoco <> 'CT' group by month(fecha), year(fecha)  having sum(netocob)<> 0 order by  year(fecha), month(fecha) ").ToList();
                            else 
                                req.objeto = bd.Database.SqlQuery<ventasVendedor>("select sum(netocob) total, convert(varchar,month(fecha))+'/'+ convert(varchar,year(fecha)) label, max(vende.nombre) nombre, max(vende.codven) codven  from clicta left join cliente on cliente.nrocli = clicta.nrocli left join vende on vende.codven = cliente.codven where empresa = " + empresa + " and fecha >= '" + desde + "' and fecha <= '" + hasta + "' and vende.codven = " + codven + " group by  month(fecha), year(fecha)  having sum(netocob)<> 0 order by  year(fecha), month(fecha)").ToList();
                            break;

                    }
                }
                return req;
            }
            catch (Exception e)
            {
                return req.falla(e);
            }
        }


        internal RequestHTTP getCobranzasVendedores(int empresa, string desde, string hasta)
        {
            var req = new RequestHTTP();
            try
            {
                using (GestionEntities bd = new GestionEntities())
                {
                    req.objeto = bd.Database.SqlQuery<ventasVendedores>("select isnull(max(clicta.codven),0) codven, isnull(max(vende.nombre),'') nombre,  sum(haber-debe) total from clicta left join vende on clicta.codven = vende.codven where empresa = " + empresa + " and tipodoc = 'RC' and fecha >= '" + desde + "' and fecha <= '" + hasta + "'  and anulado = 0 group by clicta.codven order by total desc").ToList();
                }
                return req;
            }
            catch(Exception e)
            {
                return req.falla(e);
            }
        }

        internal RequestHTTP getComprasProvee(int empresa, string desde, string hasta, int concepto, bool cotizacion)
        {
            var req = new RequestHTTP();
            try
            {
                string ccCotiza = "and tipodoc <> 'CT'";
                using (GestionEntities bd = new GestionEntities())
                {
                    if (!cotizacion)
                        ccCotiza = "";

                    req.objeto = bd.Database.SqlQuery<comprasProveedores>("select max(nropro) nropro, max(razsoc) razsoc, sum(neto + neto1 + exento) total from ivacom where fecha >= '" + desde + "' and fecha <= '" + hasta + "' and concepto = " + concepto + " " + ccCotiza + " group by nropro order by total desc").ToList();
                }
                return req;
            }catch(Exception e)
            {
                return req.falla(e);
            }
        }


        internal List<passwd> getUsuarios()
        {
            using(GestionEntities bd = new GestionEntities())
            {
                return bd.passwd.ToList();

            }
        }

       
        //----------------------------------------------------------MARCAS
        internal RequestHTTP getmarcas()
        {
            var req = new RequestHTTP();
            try
            {
                using (GestionEntities bd = new GestionEntities())
                {
                    req.objeto = bd.Database.SqlQuery<MiMarca>("select codigo, descripcion from marcas").ToList();
                }
                return req;
            }
            catch (Exception e)
            {
                return req.falla(e);
            }
        }
        internal RequestHTTP getmarca(int param)
        {
            var req = new RequestHTTP();
            try
            {
                using (GestionEntities bd = new GestionEntities())
                {
                    req.objeto = bd.Database.SqlQuery<MiMarca>("select codigo, descripcion from marcas where codigo = "+ param).ToList();
                }
                return req;
            }
            catch (Exception e)
            {
                return req.falla(e);
            }
        }

        internal RequestHTTP getmarcasFiltro(string param)
        {
            var req = new RequestHTTP();
            try
            {
                using (GestionEntities bd = new GestionEntities())
                {
                    req.objeto = bd.Database.SqlQuery<MiMarca>("select codigo, descripcion from marcas where codigo like '" + param + "' or descripcion like '%"+param+"%'").ToList();
                }
                return req;
            }
            catch (Exception e)
            {
                return req.falla(e);
            }
        }

        //----------------------------------------- TRANSPORTES

        internal RequestHTTP getTransporte(int param)
        {
            var req = new RequestHTTP();
            try
            {
                using (GestionEntities bd = new GestionEntities())
                {
                    req.objeto = bd.Database.SqlQuery<MiTranspo>("select codigo, razsoc from transpo where codigo = "+ param).ToList();
                }
                return req;
            }
            catch (Exception e)
            {
                return req.falla(e);
            }
        }
        internal RequestHTTP getTransportesFiltro(string param)
        {
            var req = new RequestHTTP();
            try
            {
                using (GestionEntities bd = new GestionEntities())
                {
                    req.objeto = bd.Database.SqlQuery<MiTranspo>("select codigo, razsoc from transpo where razsoc like '%" + param + "%' or codigo like '" + param + "'").ToList();
                }
                return req;
            }
            catch (Exception e)
            {
                return req.falla(e);
            }
        }

        internal RequestHTTP getTransportes()
        {
            var req = new RequestHTTP();
            try
            {
                using (GestionEntities bd = new GestionEntities())
                {
                    req.objeto = bd.Database.SqlQuery<MiTranspo>("select codigo, razsoc from transpo").ToList();
                }
                return req;
            }
            catch (Exception e)
            {
                return req.falla(e);
            }
        }

        //--------------------------------------------------------ACTIVIDAD

        internal RequestHTTP getActividades()
        {
            var req = new RequestHTTP();
            try
            {
                using (GestionEntities bd = new GestionEntities())
                {
                    req.objeto = bd.Database.SqlQuery<activida>("select * from activida").ToList();
                }
                return req;
            }
            catch (Exception e)
            {
                return req.falla(e);
            }
        }

        internal RequestHTTP getActividadesFiltro(string param)
        {
            var req = new RequestHTTP();
            try
            {
                using (GestionEntities bd = new GestionEntities())
                {
                    req.objeto = bd.Database.SqlQuery<activida>("select * from activida where descri like '%" + param + "%' or codigo like '" + param + "'").ToList();
                }
                return req;
            }
            catch (Exception e)
            {
                return req.falla(e);
            }
        }

        internal RequestHTTP getActividad(int param)
        {
            var req = new RequestHTTP();
            try
            {
                using (GestionEntities bd = new GestionEntities())
                {
                    req.objeto = bd.Database.SqlQuery<activida>("select * from activida where codigo =" + param).ToList();
                }
                return req;
            }
            catch (Exception e)
            {
                return req.falla(e);
            }
        }


        //---------------------------------------------------------------------ARTICULOS

        internal RequestHTTP getArticulos()
        {
            var req = new RequestHTTP();
            try
            {
                using (GestionEntities bd = new GestionEntities())
                {
                    req.objeto = bd.Database.SqlQuery<StockView>("select codpro, descri from stock").ToList();
                }
                return req;
            }
            
            catch (Exception e)
            {
                return req.falla(e);
            }
        }
                
        internal RequestHTTP getArticulosFiltro(string param)
        {
            var req = new RequestHTTP();
            try
            {
                using (GestionEntities bd = new GestionEntities())
                {
                    req.objeto = bd.Database.SqlQuery<StockView>("select codpro, descri from stock where descri like '%"+param+ "%' or codpro like '%" + param + "%'").ToList();
                }
                return req;
            }

            catch (Exception e)
            {
                return req.falla(e);
            }
          
        }
        internal RequestHTTP getArticulo(string param)
        {
            while (param.Length < 16)
                param += " ";
            var req = new RequestHTTP();
            try
            {
                using (GestionEntities bd = new GestionEntities())
                {
                    req.objeto = bd.Database.SqlQuery<StockView>("select codpro, descri from stock where codpro like '%" + param + "%'").ToList();
                }
                return req;
            }

            catch (Exception e)
            {
                return req.falla(e);
            }

        }
    }
}