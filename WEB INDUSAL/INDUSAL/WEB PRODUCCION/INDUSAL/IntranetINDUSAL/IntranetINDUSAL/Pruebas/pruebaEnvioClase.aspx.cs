using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IntranetINDUSAL.Pruebas
{
    public partial class pruebaEnvioClase : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            IntranetINDUSAL.Negocio.cPlegado oPlegado = new IntranetINDUSAL.Negocio.cPlegado();
            oPlegado.CodCliente = "CLI001";
            oPlegado.NomCliente = "CLIENTE 001";
            oPlegado.CodProducto = "PRO001";
            oPlegado.DescProducto = "PRODUCTO 001";
            oPlegado.UdsPorPaquete = 10;
            oPlegado.CodMaquina = "MAQ001";
            oPlegado.DescMaquina = "MAQUINA 001";
            Session["objetoPlegado"] = oPlegado;
            Response.Redirect("~/Pruebas/pruebaRecepcionClase.aspx");
        }
    }
}
