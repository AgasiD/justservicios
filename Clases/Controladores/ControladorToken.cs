using JustServicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


    public class ControladorToken
    {
        public static bool comprobarToken(string miToken)
        {
        using(GestionEntities bd = new GestionEntities())
        {
            string token = bd.token.Single().token1;
            if(miToken == token)
            {
                return true;
            } else {
                return false;
            }
        }

        }


    }
