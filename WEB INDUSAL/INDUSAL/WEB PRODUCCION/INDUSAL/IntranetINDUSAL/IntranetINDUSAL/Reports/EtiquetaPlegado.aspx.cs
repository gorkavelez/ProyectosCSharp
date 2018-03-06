using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IntranetINDUSAL.Reports
{
    public partial class EtiquetaPlegado : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                cEtiqueta oEtiqueta = (cEtiqueta)Session["Etiquetas"];
                lbClienteLinea1.Text = oEtiqueta.NomCliente;
                lbCodClienteBarras.Text = oEtiqueta.CodCliente;
                lbPedido.Text = oEtiqueta.NumPedido;
                lbNumPedidoBarras.Text = oEtiqueta.NumPedido;
                lbNomProducto.Text = oEtiqueta.NomProducto;
                lbcodProductoBarras.Text = oEtiqueta.CodProducto;
                hdUrlPrevia.Value = oEtiqueta.Url;
            }
            catch
            { }
        }
    }
}
