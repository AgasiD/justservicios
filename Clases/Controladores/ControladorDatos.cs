using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Web.Http.Results;
using System.Configuration;

namespace JustServicios.Clases.Controladores
{
    public class ControladorDatos
    {
        private ControladorDatos instancia { get; set; }
        private SqlConnection connection;
        private  SqlCommand command;
        private  SqlDataReader dataReader;
        private string connetionString = ConfigurationManager.ConnectionStrings["GestionConnection"].ConnectionString;

            //"Data Source=maral.no-ip.info\\sqlExpress;Initial Catalog=Gestion;User ID=just;Password=just"; //ConfigurationManager.ConnectionStrings["GestionEntities"].ConnectionString;
        public ControladorDatos()
        {
        }
        
       public ControladorDatos getInstancia()
        {
            if(instancia == null)
            {
                return new ControladorDatos();
            }
            return instancia;
        }

        public  DicRequestHTTP getData(string query)
        {
            var listDiccionario = new List<Dictionary<string, object>>();
            string sql = query;
            connection = new SqlConnection(connetionString);
            var req = new DicRequestHTTP();
            try
            {
                connection.Open();
                command = new SqlCommand(sql, connection);
                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    var d = new Dictionary<string, object>();
                    for (int i = 0; i < dataReader.FieldCount; i++)
                    {
                        d.Add(dataReader.GetName(i), dataReader.GetValue(i));
                    }
                    listDiccionario.Add(d);
                }
                dataReader.Close();
                command.Dispose();
                connection.Close();
                req.objeto = listDiccionario;
                return req;
            }
            catch (Exception e)
            {
                return req.falla(e);
            }
        }
        public RequestHTTP getElement(string query)
        {
            var diccionario = new Dictionary<string, object>();
            string sql = query;
            connection = new SqlConnection(connetionString);
            var req = new RequestHTTP();
            try
            {
                connection.Open();
                command = new SqlCommand(sql, connection);
                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    for (int i = 0; i < dataReader.FieldCount; i++)
                    {
                        diccionario.Add(dataReader.GetName(i), dataReader.GetValue(i));
                    }
                }
                dataReader.Close();
                command.Dispose();
                connection.Close();
                req.objeto = diccionario.ToList();
                return req;
            }
            catch (Exception e)
            {
                return req.falla(e);
            }
        }
    }
}