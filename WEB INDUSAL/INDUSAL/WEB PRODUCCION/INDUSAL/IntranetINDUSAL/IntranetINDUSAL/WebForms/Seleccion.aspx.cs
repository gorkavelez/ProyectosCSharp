using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IntranetINDUSAL.Negocio;

namespace IntranetINDUSAL.WebForms
{
    public partial class Seleccion : System.Web.UI.Page
    {
        static cListado oListado;
    
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                oListado = (cListado)Session["ListadoSeleccion"];
                oListado.LoadListBox(ref this.listaSeleccion);
            }
        }

        protected void listaSeleccion_SelectedIndexChanged(object sender, EventArgs e)
        {
            oListado.SelectValue = listaSeleccion.SelectedItem.Value;
            oListado.SelectText = listaSeleccion.SelectedItem.Text;
            Session["ListadoSeleccion"] = oListado;
            Response.Redirect(oListado.UrlBack);
        }

        protected void btCancelar_Click(object sender, EventArgs e)
        {
            oListado.SelectValue = "";
            oListado.SelectText = "";
            Session["ListadoSeleccion"] = oListado;
            Response.Redirect(oListado.UrlBack);
        }
    }
}
