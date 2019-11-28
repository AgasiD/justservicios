using JustServicios.Clases.Controladores;
using MercadoLibre.SDK;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;

namespace JustServicios.Controllers
{
    [System.Web.Http.RoutePrefix("api/Meli")]
    public class MELIController : ApiController
    {
        public static ControladorMELI cMELI = ControladorMELI.getInstancia();
        public string sdkVersion { get; set; }
        public string apiUrl { get; set; }
        public RestClient client { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public long ExperiIn { get; set; }
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("notificaciones")]
        public ActionResult notificaciones(JObject cabydet)
        {
            //string text = JsonConvert.SerializeObject(cabydet);
            string type = cabydet["topic"].ToString();
            string data = cabydet["resource"].ToString();
            // System.IO.File.WriteAllText(@"C:\Users\Administrator\Desktop\noticicacionesML.txt", text);
            var topics = new string[] { "questions", "orders", "created_orders", "payments", "messaging", "claims" };
            // string topic = topics.Where(top => top == text).First();
            // if (topic != null)
            // {
            switch (type)
            {
                case "questions":
                    ControladorMELI.questions(data, type);
                    break;
                case "orders":
                    ControladorMELI.questions(data, type);
                    break;
                case "created_orders":
                    ControladorMELI.questions(data, type);
                    break;
                case "payments":
                    ControladorMELI.questions(data, type);
                    break;
                case "messaging":
                    ControladorMELI.questions(data, type);
                    break;
                case "claims":
                    ControladorMELI.questions(data, type);
                    break;
                default:
                    ControladorMELI.questions(data, type);
                    break;
            }
            //   }
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("getToken")]
        public JsonResult<TokenObject> getToken(string code, long clientid, string secretKey, string redirect)
        {
            sdkVersion = "MELI-NET-SDK-1.0.2";
            apiUrl = "https://api.mercadolibre.com";
            client = new RestClient(apiUrl);
            var request = new RestRequest("/oauth/token?grant_type=authorization_code&client_id={client_id}&client_secret={client_secret}&code={code}&redirect_uri={redirect_uri}", Method.POST);
            request.AddParameter("client_id", clientid, ParameterType.UrlSegment);
            request.AddParameter("client_secret", secretKey, ParameterType.UrlSegment);
            request.AddParameter("code", code, ParameterType.UrlSegment);
            request.AddParameter("redirect_uri", redirect, ParameterType.UrlSegment);
            request.AddHeader("Accept", "application/json");
            var response = ExecuteRequest(request);
            if (response.StatusCode.Equals(HttpStatusCode.OK))
            {
                var token = JsonConvert.DeserializeObject<TokenObject>(response.Content);
                return Json(token);
            }
            return null;
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("renovarToken")]
        public JsonResult<TokenObject> renovarToken(string refreshToken, string clientSecret, long userID)
        {
            sdkVersion = "MELI-NET-SDK-1.0.2";
            apiUrl = "https://api.mercadolibre.com";
            client = new RestClient(apiUrl);
            var request = new RestRequest("/oauth/token?grant_type=refresh_token&client_id={client_id}&client_secret={client_secret}&refresh_token={refresh_token}", Method.POST);
            request.AddParameter("client_id", userID, ParameterType.UrlSegment);
            request.AddParameter("client_secret", clientSecret, ParameterType.UrlSegment);
            request.AddParameter("refresh_token", refreshToken, ParameterType.UrlSegment);
            request.AddHeader("Accept", "application/json");
            var response = ExecuteRequest(request);
            if (response.StatusCode.Equals(HttpStatusCode.OK))
            {
                var token = JsonConvert.DeserializeObject<TokenObject>(response.Content);
                return Json(token);
            }
            return null;
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("insertPic")]
        async public Task<JsonResult<string>> insertPic(string codArt,string id, string token)
        {
            HttpClient client = new HttpClient();
            foto obj = new foto();
            obj.id = id;
            string resp;
            var json = JsonConvert.SerializeObject(obj);
            var apiUrl = "https://api.mercadolibre.com/items/" + codArt + "/pictures?access_token=" + token;
            using (HttpResponseMessage response = await client.PostAsync(
                apiUrl, new StringContent(json,Encoding.UTF8, "application/json")).ConfigureAwait(false)) { 
                resp = await response.Content.ReadAsStringAsync();
            }

            return Json(resp);
            /*
            sdkVersion = "MELI-NET-SDK-1.0.2";
            apiUrl = "https://api.mercadolibre.com";
            client = new RestClient(apiUrl);
            var json = JsonConvert.SerializeObject(id);
            var request = new RestRequest("/items/"+codArt+"/pictures?access_token="+ token, Method.POST);
            request.AddHeader("Accept", "application/json");
            request.AddBody(json,"id");

            var response = ExecuteRequest(request);
           /* if (response.StatusCode.Equals(HttpStatusCode.OK))
            {*/

            /*}
            return Json(response.StatusCode.ToString());*/
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("leerNotificaciones")]
        public JsonResult<RequestHTTP> leerNotificaciones()
        {
            string query = "select * from notificacionesML order by id desc offset 0 rows fetch next 20 row only";
            return Json(ControladorMELI.getInstancia().leerNotificaciones(query));
        }

        private IRestResponse ExecuteRequest(RestRequest request)
        {
            client.UserAgent = sdkVersion;
            return client.Execute(request);
        }
    }
}