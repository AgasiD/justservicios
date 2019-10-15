using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de presupd
/// </summary>

namespace JustServicios
{
    public partial class presupd
    {
        public presupd() { }

        public presupd(stocks stock, presupc cabecera, int numerador)
        {
            try
            {
                int id;
                List<artasoc> artasoc;
            decimal cotInicio;
                using (GestionEntities bd = new GestionEntities())
                {
                    cotInicio = bd.monedas.Single(a => a.codigo == stock.moneda).ncotiza;
                    this.cotizacion = bd.monedas.Single(a => a.codigo == cabecera.monfac).ncotiza;//configen.GetConfigen().GnCotizaCl;//arregalar
                    artasoc = bd.artasoc.Where(a => a.codpro == stock.codpro && a.componen == stock.asociado).ToList();
                    if (bd.presupd.Count() > 0)
                        id = bd.presupd.ToArray().Last().id;
                    else
                        id = 0;

                    if (artasoc.Count == 0)
                    {
                        this.canasoc = 0;
                    }
                    else
                        this.canasoc = artasoc.First().cantid * stock.cantidad;
                }

                this.id = id;
                this.cabeceraid = cabecera.id;
                this.fecha = DateTime.Now;
                this.hora = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                this.tipodoc = cabecera.tipodoc;
                this.letra = cabecera.letra;
                this.punto = cabecera.punto;
                this.numero = cabecera.numero;
                this.remito = cabecera.remito;
                this.codpro = stock.codpro;
                this.descri = stock.descri;
                this.cant = stock.cantidad;
                this.precio = stock.precioVenta;
                this.bonif = stock.bonif;
                this.bonif1 = stock.bonif1;
                this.prexcant = (this.precio * this.cant) - this.precio * this.cant * stock.bonif / 100 - this.precio * this.cant * stock.bonif1 / 100;
                this.ivartins = stock.getIvaIGeneral(ControladorTotales.getCTotales().getIvaTasaGral());
                this.ivartinoi = stock.getIvaIDif(ControladorTotales.getCTotales().getIvaTasaGral());
                this.bonito = stock.getBonifTotal();
                this.nrocli = cabecera.nrocli;
                this.asociado = stock.asociado;
                this.cantenv = stock.cantenv;
                this.pedido = 0;//arregalar
                this.despacho = "";//arregalar
                this.envase = stock.simboloEnvase; //arregalar;
                
                this.unimed = stock.unimed.ToString();//arregalar 
                this.pins = 21;//arregalar
                this.pnoi = 0;//arregalar
                this.deta = "";//arregalar
                this.aprobados = 0;//arregalar
                this.detalle = stock.detalle;
                this.impint = stock.impint;
                this.precorig = 0;//arregalar
                this.ivaporc1 = stock.getIvaGral(ControladorTotales.getCTotales().getIvaTasaGral());
                this.ivaporc2 = stock.getIvaDif(ControladorTotales.getCTotales().getIvaTasaGral());
                this.moneda = cabecera.monfac;
                this.costo = stock.costo * stock.cantidad * (cotInicio/this.cotizacion);
                this.simbolo = cabecera.simbolo.Substring(0, 2);
            }
            catch (Exception)
            {
                
            }

            }
        }
    }
