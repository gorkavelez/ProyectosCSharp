using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ExtranetSubcontratacion.Negocio;

namespace ExtranetSubcontratacion
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InicializarControles();
                txUsuario.Focus();
            }


        }

        private void InicializarControles()
        {
            txUsuario.Text = "";
            txPassword.Text = "";
            lbErrorLogin.Text = "";
            lbErrorLogin.Visible = false;
        }

        protected void btLogin_Click(object sender, EventArgs e)
        {
            TryLogin();
        }

        private void TryLogin()
        {
            try
            {
                proxyEntidades proxy = new proxyEntidades("02 NAVARRA");
                string vendorName = proxy.VendorLogin(txUsuario.Text, txPassword.Text);
                if (vendorName != "")
                {
                    Session["vendorCode"] = txUsuario.Text;
                    Session["vendorName"] = vendorName;
                    Response.Redirect("Default.aspx");
                }
                else
                {
                    lbErrorLogin.Text = "El usuario y la contraseña indicados no son válidos";
                    lbErrorLogin.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lbErrorLogin.Text = ex.Message;
                lbErrorLogin.Visible = true;
            }
        }

        protected void txLogin_TextChanged(object sender, EventArgs e)
        {
            lbErrorLogin.Text = "";
            lbErrorLogin.Visible = false;
        }
        
    }
}
