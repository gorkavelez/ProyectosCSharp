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
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using BDConexion.GestSport;
using GestSportEntidades.GestSport;


namespace GestSport
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void validarUsuario(string User, string Pass)
        {           
            string MensageError = string.Empty;
            TextosEstandar TextoLiteral = new TextosEstandar();
            //Acceso usuariuo y password
            SqlConnection SqlConexion = new SqlConnection();
            Conexion BDconect = new Conexion();
            if (BDconect.conectar(ref MensageError, ref SqlConexion))
            {
                //Crear consulta sql
                lblEstado.Text = MensageError;
                SqlCommand ConsultaLogin = new SqlCommand("select * from [Acceso Login] where [Usuario login] ='" +
                                                          txtUsuario.Text + "' and [Password] ='" + txtPassword.Text + "'", SqlConexion);
                SqlDataReader lector = ConsultaLogin.ExecuteReader();
                string usuario = txtUsuario.Text;
                limpiarVars();
                if (lector.HasRows)                            
                    Response.Redirect(@"\WebForms\Principal.aspx");
                else
                    lblEstado.Text = TextoLiteral.textoErrorLogin;               
            }
        }

        public void limpiarVars()
        {
            lblEstado.Text = string.Empty;
            txtUsuario.Text = string.Empty;
            txtPassword.Text = string.Empty;
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            validarUsuario(txtUsuario.Text,txtPassword.Text);
        }
    }
}
