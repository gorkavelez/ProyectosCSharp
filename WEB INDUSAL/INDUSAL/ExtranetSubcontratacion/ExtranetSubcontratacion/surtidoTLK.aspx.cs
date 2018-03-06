using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ExtranetSubcontratacion.Negocio;
using Telerik.Web.UI;
using System.Drawing;

namespace ExtranetSubcontratacion
{
    public partial class surtidoTLK : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {                
                VisualizarFamilias();
                VisualizarSurtido();                                
            }

            GenerarTextBoxCantidad();            

        }

        #region SURTIDO

        private void VisualizarFamilias()
        {
            try
            {
                surtidoCliente surtido = (surtidoCliente)Session["surtidoCliente"];
                ddlFamilias.DataSource = surtido.GetFamilias();
                ddlFamilias.DataValueField = "codigo";
                ddlFamilias.DataTextField = "descripcion";
                ddlFamilias.DataBind();
                RadComboBoxItem newItem = new RadComboBoxItem("");
                ddlFamilias.Items.Add(newItem);
                ddlFamilias.SelectedIndex = ddlFamilias.Items.Count - 1;
            }
            catch (Exception)
            {

            }
        }

        private void VisualizarSubfamilias()
        {
            try
            {
                ddlSubfamilias.Items.Clear();
            }
            catch (Exception)
            {

            }
        }

        private void VisualizarSubfamilias(string _codFamilia)
        {
            try
            {
                surtidoCliente surtido = (surtidoCliente)Session["surtidoCliente"];
                ddlSubfamilias.DataSource = surtido.GetSubfamilias(_codFamilia);
                ddlSubfamilias.DataValueField = "codigo";
                ddlSubfamilias.DataTextField = "descripcion";
                ddlSubfamilias.DataBind();
                RadComboBoxItem newItem = new RadComboBoxItem("");
                ddlSubfamilias.Items.Add(newItem);
                ddlSubfamilias.SelectedIndex = ddlSubfamilias.Items.Count - 1;
            }
            catch (Exception)
            {

            }
        }

        private void VisualizarSurtido()
        {
            try
            {
                surtidoCliente surtido = (surtidoCliente)Session["surtidoCliente"];
                if (ddlFamilias.SelectedIndex != ddlFamilias.Items.Count - 1)
                {
                    string familia = ddlFamilias.SelectedItem.Value;
                    if (ddlSubfamilias.SelectedIndex != ddlSubfamilias.Items.Count - 1)
                    {
                        string subfamilia = ddlSubfamilias.SelectedItem.Value;
                        gridSurtido.DataSource = surtido.GetProductos(familia, subfamilia);
                    }
                    else
                        gridSurtido.DataSource = surtido.GetProductos(familia);
                }
                else
                    gridSurtido.DataSource = surtido.GetProductos();

                gridSurtido.DataBind();
                //GenerarTextBoxCantidad();
            }
            catch (Exception)
            {

            }
        }

        private void GenerarTextBoxCantidad()
        {
            Int32 nLinea = 1;
            Session["LineasSurtido"] = 0;

            try
            {
                foreach (GridItem e in gridSurtido.Items)
                {
                    if ((e.ItemType == Telerik.Web.UI.GridItemType.Item) ||
                        (e.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem))
                    {
                        e.Cells[4].Controls.Add(new RadNumericTextBox());
                        RadNumericTextBox newTextBox = (RadNumericTextBox)e.Cells[4].Controls[0];                        
                        newTextBox.ID = e.Cells[2].Text;
                        newTextBox.Text = e.Cells[4].Text;
                        newTextBox.AutoPostBack = true;
                        newTextBox.MinValue = 0;
                        newTextBox.NumberFormat.DecimalDigits = 0;
                        newTextBox.TextChanged += new EventHandler(newTextBox_TextChanged);
                        newTextBox.Attributes.Add("itemNo", e.Cells[2].Text);
                        newTextBox.Attributes.Add("linea", nLinea++.ToString());
                    }
                }

                Session["LineasSurtido"] = --nLinea;
            }
            catch
            {
            }
        }

        protected void newTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                RadNumericTextBox txtSender = (RadNumericTextBox)sender;
                surtidoCliente surtido = (surtidoCliente)Session["surtidoCliente"];
                surtido.SetItemQty(txtSender.Attributes["itemNo"], decimal.Parse(txtSender.Text));
                Session["surtidoCliente"] = surtido;
                VisualizarSurtido();
                GenerarTextBoxCantidad();

                Int32 linea =Int32.Parse(txtSender.Attributes["linea"]);
                Int32 totalLineas=Int32.Parse(Session["LineasSurtido"].ToString());
                Int32 nextLine = 0;
                if (linea < totalLineas)                    
                    nextLine = linea + 1;
                else                    
                    nextLine = 1;

                FindNextItem(nextLine);
            }
            catch
            {
            }
        }

        private void FindNextItem(Int32 lineaSel)
        {
            try
            {
                Int32 linea =0;
                RadNumericTextBox newTextBox = null;

                foreach (GridItem e in gridSurtido.Items)
                {
                    newTextBox = (RadNumericTextBox)e.Cells[4].Controls[0];   
                    linea =Int32.Parse(newTextBox.Attributes["linea"]);
                    if (linea == lineaSel)
                    {
                        newTextBox.Focus();
                    }
                }
            }
            catch
            {
            }
        }

        protected void ddlFamilias_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (ddlFamilias.SelectedIndex != ddlFamilias.Items.Count - 1)
            {
                VisualizarSubfamilias(ddlFamilias.SelectedItem.Value);                
            }
            else
            {
                VisualizarSubfamilias();                
            }

            VisualizarSurtido();
            GenerarTextBoxCantidad();
        }

        protected void ddlSubfamilias_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            VisualizarSurtido();
            GenerarTextBoxCantidad();
        }

        protected void btAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                string orderNo = Session["orderNo"].ToString();
                string lavanderia = Session["lavanderia"].ToString();
                string vendorCode = Session["vendorCode"].ToString();
                EliminarLineasPedido(lavanderia,orderNo);
                CrearLineasDesdeSurtido(lavanderia, orderNo, vendorCode);
                Response.Redirect("Default.aspx");
            }
            catch
            {
            }

        }

        protected void btCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }

        private void EliminarLineasPedido(string _lavanderia, string _pedido)
        {
            proxyEntidades proxy = new proxyEntidades(_lavanderia);
            proxy.DeleteAllOrderLines(_pedido);
        }

        private void CrearLineasDesdeSurtido(string _lavanderia, string _pedido, string _proveedor)
        {
            proxyEntidades proxy = new proxyEntidades(_lavanderia);
            surtidoCliente surtido = (surtidoCliente)Session["surtidoCliente"];
            DataTable tablaSurtido = surtido.TablaSurtido;
            DataRow[] filtro = tablaSurtido.Select("Cantidad > 0");
            foreach (DataRow lineaFiltro in filtro)
            {
                proxy.CreatePurchOrderLine(
                        _pedido,
                        _proveedor,
                        lineaFiltro["CodProducto"].ToString(),
                        decimal.Parse(lineaFiltro["Cantidad"].ToString()));
            }
        }

        #endregion
        
    }
}
