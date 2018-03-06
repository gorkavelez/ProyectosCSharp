using System;
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
    public partial class Surtido : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarSurtidoCliente("000270", "HOTEL TXIMISTA");
                VisualizarFamilias();
                VisualizarSurtido();
            }
        }

        #region SURTIDO

        private void CargarSurtidoCliente(string customerCode, string customerName)
        {
            bool cargar = false;
            surtidoCliente surtido = null;

            try
            {
                surtido = (surtidoCliente)Session["surtidoCliente"];
                if (surtido.CodCliente != customerCode)
                    cargar = true;
            }
            catch
            {
                cargar = true;
            }

            if (cargar)
            {
                surtido = new surtidoCliente();
                proxyEntidades proxy = new proxyEntidades("02 NAVARRA");

                surtido.CodCliente = customerCode;
                surtido.NomCliente = customerName;
                surtido.TablaSurtido = proxy.GetCustCrossRefs(customerCode);
                Session["surtidoCliente"] = surtido;
            }


        }

        private void InicializarSeleccionSurtido()
        {
            ddlFamilias.SelectedIndex = ddlFamilias.Items.Count - 1;
            ddlSubfamilias.SelectedIndex = ddlSubfamilias.Items.Count - 1;
        }

        private void VisualizarFamilias()
        {
            try
            {
                surtidoCliente surtido = (surtidoCliente)Session["surtidoCliente"];
                ddlFamilias.DataSource = surtido.GetFamilias();
                ddlFamilias.DataValueField = "codigo";
                ddlFamilias.DataTextField = "descripcion";
                ddlFamilias.DataBind();
                ListItem newItem = new ListItem("");
                ddlFamilias.Items.Add(newItem);
                ddlFamilias.SelectedIndex = ddlFamilias.Items.Count - 1;
            }
            catch(Exception)
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
                ListItem newItem = new ListItem("");
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
                gridSurtido.DataSource = surtido.GetProductos();
                gridSurtido.DataBind();                
            }
            catch (Exception)
            {

            }
        }

        private void VisualizarSurtido(string _codFamilia)
        {
            try
            {
                surtidoCliente surtido = (surtidoCliente)Session["surtidoCliente"];
                gridSurtido.DataSource = surtido.GetProductos(_codFamilia);
                gridSurtido.DataBind();
            }
            catch (Exception)
            {

            }
        }

        private void VisualizarSurtido(string _codFamilia, string _codSubfamilia)
        {
            try
            {
                surtidoCliente surtido = (surtidoCliente)Session["surtidoCliente"];
                gridSurtido.DataSource = surtido.GetProductos(_codFamilia, _codSubfamilia);
                gridSurtido.DataBind();
            }
            catch (Exception)
            {

            }
        }

        private void GenerarTextBoxCantidad()
        {
            try
            {
                
            }
            catch
            {
            }
        }

        protected void ddlFamilias_SelectedIndexChanged(object o, EventArgs e)
        {
            if (ddlFamilias.SelectedIndex != ddlFamilias.Items.Count - 1)
            {
                VisualizarSubfamilias(ddlFamilias.SelectedItem.Value);
                VisualizarSurtido(ddlFamilias.SelectedItem.Value);
            }
            else
            {
                VisualizarSubfamilias();
                VisualizarSurtido();
            }
        }

        protected void ddlSubfamilias_SelectedIndexChanged(object o, EventArgs e)
        {
            if (ddlSubfamilias.SelectedIndex != ddlSubfamilias.Items.Count - 1)
            {
                VisualizarSurtido(ddlFamilias.SelectedItem.Value, ddlSubfamilias.SelectedItem.Value);
            }
            else
            {
                VisualizarSurtido(ddlFamilias.SelectedItem.Value);
            }
        }        

        protected void btAceptar_Click(object sender, EventArgs e)
        {
            //InsertarLineaPedido();
            InicializarSeleccionSurtido();
            //CargarDatosPedido();
        }

        protected void btCancelar_Click(object sender, EventArgs e)
        {
            InicializarSeleccionSurtido();
            //panelSeleccionSurtido.Visible = false;
        }       
        
        
        #endregion

        

        
    }
}
