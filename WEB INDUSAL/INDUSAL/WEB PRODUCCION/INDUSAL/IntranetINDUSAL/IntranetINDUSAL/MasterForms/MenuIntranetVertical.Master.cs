using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IntranetINDUSAL.Controles_Personalizados;

namespace IntranetINDUSAL.MasterForms
{
    public partial class MenuIntranetVertical : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lbEmpresa.Text = Session["empresaLogin"].ToString();
            //lbEmpresa.Text = "02 NAVARRA";
            lbFecha.Text = DateTime.Today.ToShortDateString();
            lbHora.Text = DateTime.Now.ToShortTimeString();
        }
                
    }
}
