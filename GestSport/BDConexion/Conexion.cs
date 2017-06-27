using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;

namespace BDConexion.GestSport
{
    public class Conexion
    {
        public bool conectar(ref SqlConnection ConexionOrigen)
        {
            bool conectado;
            try
            {
                //String Conn = ConfigurationSettings.AppSettings["ConexionGestSport"];
                String Conn = @"Data Source=.\sqlexpress;Initial Catalog=GestSport;Integrated Security=True";
                ConexionOrigen.ConnectionString = Conn;
                ConexionOrigen.Open();
                conectado = true;
            }
            catch (Exception ex)
            {
                conectado = false;
            }             

            return conectado;
        }

        public bool conectar(ref string ErrorConexion,ref SqlConnection ConexionOrigen)
        {
            bool conectado;
            try
            {
                //String Conn = ConfigurationSettings.AppSettings["ConexionGestSport"];
                String Conn = @"Data Source=.\sqlexpress;Initial Catalog=GestSport;Integrated Security=True";              
                ConexionOrigen.ConnectionString = Conn;                
                ConexionOrigen.Open();
                conectado = true;
            }
            catch (Exception ex)
            {
                conectado = false;
                ErrorConexion = ex.Message;
            }
            return conectado;           
        }
    }
}
