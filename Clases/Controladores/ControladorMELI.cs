using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Results;

namespace JustServicios.Clases.Controladores
{
    public class ControladorMELI
    {
        static SqlDataReader reader;
        static SqlCommand command;
        static string connetionString = "Data Source=SERVERSISTEMAS\\JUSTSOLUTION;Initial Catalog=gestion;User ID=just;Password=just";
        static SqlConnection cnn;
        static HttpClient client;
        public static ControladorMELI controlador { get; set; }
        string[] topics = new string[] { "questions", "orders", "created_orders", "payments", "messaging", "claims" };

        public ControladorMELI()
        {
            cnn = new SqlConnection(connetionString);
            client = new HttpClient();

        }
        public static ControladorMELI getInstancia()
        {
            if (controlador == null)
                controlador = new ControladorMELI();
            return controlador;
        }

        async public static void questions(string resource,string type)
        {
            string query = "insert into notificacionesML (data, tipo) values (@data, @tipo)";
            string token = "APP_USR-5633633498561215-092411-f7908bc7b93b62b571c61abff4dd43e4-144214079";
            string urlRequest = "https://api.mercadolibre.com"+resource+"?access_token="+token;
            HttpResponseMessage response = await client.GetAsync(urlRequest);
            string respuesta = JsonConvert.SerializeObject(response.Content.ReadAsStringAsync());
            try
            {
                cnn.Open();
                command = new SqlCommand(query, cnn);
                command.Parameters.Add("@data", SqlDbType.VarChar);
                command.Parameters["@data"].Value = respuesta;
                command.Parameters.Add("@tipo", SqlDbType.VarChar);
                command.Parameters["@tipo"].Value = type;
                command.ExecuteNonQuery();
                System.IO.File.WriteAllText(@"C:\Users\Administrator\Desktop\noticicacionesML.txt", respuesta);
                command.Dispose();
                cnn.Close();
            }
            catch (Exception ex)
            {
                System.IO.File.WriteAllText(@"C:\Users\Administrator\Desktop\noticicacionesML.txt", ex.Message);
            }


        }
        public static void createOrders()
        {
            //payment_required --> paid: Cuando el pago en el momento
            //confirmed : cuando el pago es al momento de retirar
            //order_items muestra el item comprado y sus caracteristicas
            //transaction_amount precio de la venta
            //status: estado de la compra
            //date_created: "2019-09-05T09:59:48.000-04:00",
            //date_last_modified: "2019-09-05T09:59:48.000-04:00"

            var estados = new string[] { "payment_required", "paid", "confirmed" };
            string urlRequest = "https://api.mercadolibre.com/orders/{ORDER}?access_token=token";
        }
 


        public RequestHTTP leerNotificaciones(string query)
        {
            cnn = new SqlConnection(connetionString);
            cnn.Open();
            command = new SqlCommand(query, cnn);
            command.Connection = cnn;
            reader = command.ExecuteReader();
            List<Notificacion> p = new List<Notificacion>();
            while (reader.Read())
                p.Add(new Notificacion(reader["data"].ToString(), reader["tipo"].ToString()));
            command.Dispose();
            cnn.Close();
            var e = new RequestHTTP();
            e.objeto = p;
            return e;
        }

       

        private static void generarMovimiento()
        {

        }



    }

    public class Notificacion
    {
        public string data { get; set; }
        public string tipo { get; set; }
        public Notificacion(string dat, string tip) {
            data = dat;
            tipo = tip;
        }
    
    }
}