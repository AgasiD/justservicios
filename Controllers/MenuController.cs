using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace JustServicios
{
    [RoutePrefix("api/Menu")]
    public class MenuController : ApiController
    {

        [HttpGet]
        [Route("getPopUp")]
        public List<string> GetArmaPopup_Result(int empresa, int usuario, string token)
        {
            if (!ControladorToken.comprobarToken(token))
            {
                return null;
            }
            try { 
            List<string> p = new List<string>();
                using (GestionEntities bd = new GestionEntities())
                {
                    var lista = bd.Database.SqlQuery<ArmaPopupWeb_Result>("exec ArmaPopupWeb " + empresa + "," + usuario).ToList();
                    foreach (ArmaPopupWeb_Result t in lista)
                {
                    p.Add(t.cPadweb);
                }
            }
            return p;
            }
            catch (Exception)
            {
                return null;
            }
        }

        [HttpGet]
        [Route("getMenu")]
        public List<accesos> GetArmaMenu(int empresa, int usuario, string token)
        {
            if (!ControladorToken.comprobarToken(token))
            {
                return null;
            }
                try
                {
                    List<accesos> p;
                using (GestionEntities bd = new GestionEntities())
                {
                    p = bd.Database.SqlQuery<accesos>("exec ArmaMenuWeb "+ empresa +","+ usuario).ToList();
                    }
                    return p;
                }
                catch (Exception)
                {
                    return null;
                }
            
        }

    }

}