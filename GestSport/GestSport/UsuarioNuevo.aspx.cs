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
using BDConexion.GestSport;
using GestSportEntidades.GestSport;

namespace GestSport
{
    public partial class UsuarioNuevo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }        

        protected void btnCrearUsuario_Click(object sender, EventArgs e)
        {
            try
            {
                string textoErrorValidacion = string.Empty;
                if (!validarFomulario(ref textoErrorValidacion))
                    throw new Exception(textoErrorValidacion);

                TextosEstandar vTextoEstandar = new TextosEstandar();

                string MensageError = string.Empty;              
                SqlConnection vConectar = new SqlConnection();
                Conexion bdConect = new Conexion();
                if (bdConect.conectar(ref MensageError, ref vConectar))
                {
                    SqlCommand ComprobarUser = new SqlCommand(@"SELECT * FROM [Usuarios]where idUsuario= '" + txtUsuario.Text + "'", vConectar);
                    SqlDataReader lectorUsuario = ComprobarUser.ExecuteReader();
                    if (lectorUsuario.HasRows == true)
                        throw new Exception(vTextoEstandar.textoErrorUsuario);
                    vConectar.Close();

                    vConectar.Open();
                    SqlCommand InsertarUser = new SqlCommand(@"Insert into Usuarios(idUsuario,eMail,Nombre,[Apellido 1],[Apellido 2],Direccion) values('" + txtUsuario.Text + "','" + txtEmail.Text + "','" + txtNombre.Text + "','" + txtApellido.Text + "','" + txtApellido2.Text + "','" + txtDireccion.Text + "')", vConectar);
                    SqlDataReader Lector = InsertarUser.ExecuteReader();
                    lblError.ForeColor = System.Drawing.Color.Green;
                    vConectar.Close();

                    vConectar.Open();
                    SqlCommand CrearLogin = new SqlCommand(@"Insert into [Acceso Login] ([Usuario Login],Password,[Id login]) values ('" + txtUsuario.Text + "','" + txtPassWord.Text + "','3')", vConectar);
                    SqlDataReader LectorLogin = CrearLogin.ExecuteReader();

                    limpiarVar();
                    lblError.Text = "Usuario creado";
                }
                else
                    throw new Exception(MensageError);
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }        

        public void limpiarVar()
        {
            txtNombre.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtUsuario.Text = string.Empty;
            txtApellido.Text = string.Empty;
            txtApellido2.Text = string.Empty;
            txtDireccion.Text = string.Empty;
            txtPassWord.Text = string.Empty;
            txtConfirmPass.Text = string.Empty;
        }

        public bool validarFomulario(ref string txtError)
        {
            bool correcto = true;

            TextosEstandar vTextoEstandar = new TextosEstandar();

            if (txtUsuario.Text == string.Empty)
            {
                correcto = false;
                txtError = vTextoEstandar.textoUsuario;
            }

            if (txtNombre.Text == string.Empty)
            {
                correcto = false;
                txtError = vTextoEstandar.textoFaltaNombre;
            }

            if (txtApellido.Text == string.Empty & correcto == true)
            {
                correcto = false;
                txtError = vTextoEstandar.textoFaltaApellido;
            }

            //comprobar password
            ExpresionesReg expReg = new ExpresionesReg();
            if (!expReg.ValidarPasswordUser(txtPassWord.Text) & correcto == true)
            {
                correcto = false;
                txtError = vTextoEstandar.textoContraseñaNoEsCorrecta;
            }

            if (txtPassWord.Text != txtConfirmPass.Text & correcto == true)
            {
                correcto = false;
                txtError = vTextoEstandar.textoEmailNoCoincide;
            }

            if (!expReg.ValidarEmail(txtEmail.Text) & correcto == true)
            {
                correcto = false;
                txtError = vTextoEstandar.textoEmailErroneo;
            }
            return correcto;
        }

        protected void txtEmail_TextChanged(object sender, EventArgs e)
        {
        }

        protected void txtNombre_TextChanged(object sender, EventArgs e)
        {
        }

        protected void btnInicio_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("Login.aspx");            
        }        
    }
}
