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
    public partial class ResetearPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            
            lblEstado.Text = string.Empty;
            bool boolValidado = false;
            string ErrorText = string.Empty;
            boolValidado = ValidarProceso(txtPasswordNew.Text, txtPassConfirm.Text, ref ErrorText);
            if (boolValidado != true)
                lblEstado.Text = ErrorText;
            else
            {
                ResetearPass();
                lblEstado.Text = "";
            }
        }

        public void LimpiarVar()
        {
            txtUsuario.Text = string.Empty;
            txtPasswordNew.Text = string.Empty;            
        }

        public bool ValidarProceso(string passNew, string confirm, ref string Errortxt)
        {
            TextosEstandar txtLiteral = new TextosEstandar();
            bool Validado = true;
            Errortxt = string.Empty;                                   
            string MensageError = string.Empty;           
            
            if (Validado == true)
            {
                if (txtPasswordNew.Text != string.Empty)
                {
                    if (Regex.Replace(txtPasswordNew.Text, "[^0-9]", "") == string.Empty)
                    {                       
                        Errortxt = txtLiteral.textoError2;
                        Validado = false;
                        LimpiarVar();
                    }
                    else if (Regex.Replace(txtPasswordNew.Text, "[^A-Z]", "") == string.Empty)
                    {
                        Errortxt = txtLiteral.textoError3;
                        Validado = false;
                        LimpiarVar();
                    }
                }
            }          

            if (Validado == true)
            {
                if (passNew != confirm)
                {
                    Errortxt = "Las contraseñas introducidas no son iguales";
                    Validado = false;
                    LimpiarVar();
                }
            }            

            return Validado;
        }

        public void ResetearPass()
        {
            //llamada a la función para resetear el password.
            string MensageError = string.Empty;
            //Acceso usuariuo y password
            SqlConnection SqlConexion = new SqlConnection();
            Conexion BDconect = new Conexion();
            if (BDconect.conectar(ref MensageError, ref SqlConexion))
            {               
                lblEstado.Text = MensageError;
                SqlCommand ConsultaLogin = new SqlCommand("update [Acceso Login] set [Password]='" + txtPasswordNew.Text +
                                                          "'where [Usuario login] ='" + txtUsuario.Text + "'", SqlConexion);
                SqlDataReader lector = ConsultaLogin.ExecuteReader();
            }
        }

        protected void txtPasswordOld_TextChanged(object sender, EventArgs e)
        {
            
        }

        protected void txtPasswordNew_TextChanged(object sender, EventArgs e)
        {
         
        }        
        

    }
}
