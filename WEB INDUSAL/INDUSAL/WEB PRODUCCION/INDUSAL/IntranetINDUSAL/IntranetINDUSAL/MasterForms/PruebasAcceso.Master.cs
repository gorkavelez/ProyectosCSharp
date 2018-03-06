using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IntranetINDUSAL.Controles_Personalizados;

namespace IntranetINDUSAL.MasterForms
{
    public partial class PruebasAcceso : System.Web.UI.MasterPage
    {
        //public event TecladoClick(object sender,System.EventArgs e);

        protected void Page_Load(object sender, EventArgs e)
        {
            //this.INIKER_teclado.OKClick +=
            //       new INIKER_tecladoNumerico.OKClickHandler(INIKER_teclado_OKClick);
            //lbEmpresa.Text = Session["empresaLogin"].ToString();
            lbEmpresa.Text = "02 NAVARRA";
            lbFecha.Text = DateTime.Today.ToShortDateString();
            lbHora.Text = DateTime.Now.ToShortTimeString();
        }

        //protected void INIKER_tecladoNumerico1_OKClick(object sender, INIKER_tecladoNumerico.OKClickEventArgs e)
        //{
        //    TecladoClick(sender, e);
        //}
                
        //Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        //    Response.Write("Button1_Click desde página maestra<br>")
        //    RaiseEvent Button1Click(sender, e)
        //End Sub

    }
}
