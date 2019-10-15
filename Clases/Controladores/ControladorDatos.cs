﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Web.Http.Results;

namespace JustServicios.Clases.Controladores
{
    public class ControladorDatos
    {
        private ControladorDatos instancia { get; set; }
        private SqlConnection connection;
        private  SqlCommand command;
        private  SqlDataReader dataReader;
        private string connetionString = "Data Source=maral.no-ip.info\\SQLEXPRESS;Initial Catalog=Gestion;User ID=just;Password=just";

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
            var diccionario = new List<Dictionary<string, object>>();
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
                    diccionario.Add(d);
                }
                dataReader.Close();
                command.Dispose();
                connection.Close();
                req.objeto = diccionario;
                return req;

            }
            catch (Exception e)
            {
                return req.falla(e);
            }
        }
    }
}