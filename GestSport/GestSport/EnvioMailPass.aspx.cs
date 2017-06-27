using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using System.Data.SqlClient;
using BDConexion.GestSport;
using GestSportEntidades.GestSport;

namespace GestSport
{
    public partial class EnvioMailPass : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            if (txtEmail.Text != string.Empty)
            {
                try
                {
                    lblError.Text = String.Empty;
                    comprobarMail();
                    enviarMail();                    
                }
                catch (Exception ex)
                {
                    txtEmail.Text = string.Empty;
                    lblError.Text = ex.Message;                                      
                }
            }
        }

        protected void txtEmail_TextChanged(object sender, EventArgs e)
        {         
            if (!Regex.IsMatch(txtEmail.Text, RexpValidator.ValidationExpression))
            {
                txtEmail.Text = string.Empty;                
            }
        }

        public void comprobarMail()
        {    
            //comprobar que existe el email en la base de datos
            TextosEstandar TextoLiteral = new TextosEstandar();
            string MensageError = string.Empty;
            SqlConnection SqlConexion = new SqlConnection();
            Conexion BDconect = new Conexion();
            if (BDconect.conectar(ref MensageError,ref SqlConexion))
            {
                SqlCommand ConsultaLogin = new SqlCommand(@"select * from [Usuarios] where [eMail] ='" +txtEmail.Text.ToString() + "'", SqlConexion);
                SqlDataReader lector = ConsultaLogin.ExecuteReader();
                if (!lector.HasRows)
                    throw new Exception(TextoLiteral.textoNoExisteEmail);
            }
            else            
                throw new Exception(MensageError);            
        }

        public void enviarMail()
        {
            //conectar servidor smpt y envair correo.
            lblEnvioCorreo.Text = "Correo enviado";
            txtEmail.Text = string.Empty;            
        }

        protected void btnInicio_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("Login.aspx");
        }
    }
    

}
