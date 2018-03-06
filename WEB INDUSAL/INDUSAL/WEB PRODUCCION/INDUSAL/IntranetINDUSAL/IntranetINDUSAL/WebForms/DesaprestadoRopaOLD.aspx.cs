using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IntranetINDUSAL.Controles_Personalizados;

namespace IntranetINDUSAL.WebForms
{
    public partial class DesaprestadoRopa1 : System.Web.UI.Page
    {
        static DataTable _productos;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.INIKER_teclado.OKClick +=
                   new INIKER_tecladoNumerico.OKClickHandler(INIKER_teclado_OKClick);

            if (!IsPostBack)
            {
                GetItems();
                MostrarItems();
            }
        }

        private void GetItems()
        {
            cProduccion oProduccion = new cProduccion(Session["empresaLogin"].ToString());
            _productos = oProduccion.GetItemList();
        }

        private void MostrarItems()
        {
            ddlItems.DataSource=_productos;
            ddlItems.DataValueField=_productos.Columns["codigo"].ToString();
            ddlItems.DataTextField=_productos.Columns["descripcion"].ToString();
            ddlItems.DataBind();
            ddlItems.Items.Add("");
            ddlItems.SelectedIndex=ddlItems.Items.Count-1;
        }

        protected void INIKER_teclado_OKClick(object sender, INIKER_tecladoNumerico.OKClickEventArgs e)
        {
            txCantidad.Text = e.Valor;
            PanelTeclado.Enabled = false;
            //ControlBotones();
            Desaprestar();
        }

        protected void ControlBotones()
        {
            // Se controla que para que el botón de Desaprestado esté activo, tiene que haber una descripción
            // de producto (obtenida a partir de una introducción de código de producto o de nº de serie) y
            // cantidad a desaprestar
            bool datosIntro;
            datosIntro = ((ddlItems.SelectedItem.Text != "") && (txCantidad.Text != ""));
            btDesaprestar.Enabled = datosIntro;
        }

        protected void btCancelar_Click(object sender, EventArgs e)
        {
            LimpiarControles();
        }

        protected void btDesaprestar_Click(object sender, EventArgs e)
        {
            Desaprestar();           
        }

        private void Desaprestar()
        {
            cProduccion oProduccion = new cProduccion(Session["empresaLogin"].ToString());
            oProduccion.Desaprestar(txProducto.Text, decimal.Parse(txCantidad.Text), "");
            LimpiarControles();
        }

        private void LimpiarControles()
        {
            txProducto.Text = "";
            ddlItems.SelectedIndex = ddlItems.Items.Count - 1;
            txCantidad.Text = "";
        }

        protected void txProducto_TextChanged(object sender, EventArgs e)
        {
            ddlItems.SelectedIndex=ddlItems.Items.IndexOf(ddlItems.Items.FindByValue(txProducto.Text));

            PanelTeclado.Enabled = (txProducto.Text != "");
            ControlBotones();
        }

        protected void txCantidad_TextChanged(object sender, EventArgs e)
        {
            ControlBotones();
        }

        protected void ddlItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            txProducto.Text=ddlItems.SelectedItem.Value;
        }
    }
}
