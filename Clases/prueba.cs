using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using JustServicios;


public class prueba
    {
    public List<stocks> lista { get; set; }
    public decimal bonifcli { get; set; }

    public prueba()
    {
        lista = new List<stocks>();
    }
    }
