using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IntranetINDUSAL.Negocio;

namespace IntranetINDUSAL.WebForms
{
    public partial class PlanchadoCalandra : System.Web.UI.Page
    {
        static cPlanchado oPlanchado;
        static bool filaEliminada = false;
        static bool editandoFila = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            // se añade un controlador de evento para poder recoger las pulsaciones sobre los botones del
            // datagrid de surtido de cliente.
            this.INIKER_gridSurtido.ProductClick += new INIKER_ButtonDG.ProductClickHandler(INIKER_gridSurtido_ProductClick);

            if (!IsPostBack)
            {
                // se crea la instancia del objeto para la gestión del conteo
                InicializarClasePlanchado();
                MostrarTitulo();
            }
        }

        private void InicializarClasePlanchado()
        {
            string tipo = Request.QueryString["Tipo"];
            oPlanchado = new cPlanchado(Session["empresaLogin"].ToString(),int.Parse(tipo));

        }

        private void MostrarTitulo()
        {
            switch ((int)oPlanchado.TipoPlanchado)
            {
                case 0:
                    this.Title = "PLANCHADO: CALANDRAS";
                    break;
                case 1:
                    this.Title = "PLANCHADO: FELPA";
                    break;
                case 2:
                    this.Title = "PLANCHADO: FORMA";
                    break;
            }
        }

        protected void txCodCliente_TextChanged(object sender, EventArgs e)
        {
            ObtenerNombreCliente(txCodCliente.Text);
            MostrarSurtidoCliente(txCodCliente.Text);
        }

        protected void INIKER_gridSurtido_ProductClick(object sender, INIKER_ButtonDG.ProductClickEventArgs e)
        {
            lbDescProdSelec.Text = e.ProdDesc;
            hfCodProducto.Value = e.ProdCode;
            lbQtyAlmacen.Text = ObtenerInventProdAlm(e.ProdCode, hfCodAlmacen.Value).ToString();
            lbUdsPaq.Text = ObtenerCantPorPaquete(e.ProdCode).ToString();
            // Se activan los controles de introducción de cantidades
            txPaq.Enabled = true;
            txEtiqPaq.Enabled = true;
            txUnidades.Enabled = true;
            txEtiqUds.Enabled = true;
            txPaq.Focus();
        }

        protected void MostrarSurtidoCliente(string cliente)
        {
            cProductos surtido = new cProductos();
            DataTable tablaSurtido;
            tablaSurtido = surtido.ObtenerSurtidoClienteClasificado(cliente, Session["empresaLogin"].ToString(),oPlanchado.TipoPlanchado);
            INIKER_gridSurtido.Datos = tablaSurtido;
            INIKER_gridSurtido.GridDataBind();
        }

        protected void ObtenerNombreCliente(string cliente)
        {
            cProduccion clientes = new cProduccion(Session["empresaLogin"].ToString());
            string strCliente = clientes.GetCustomerData(cliente);
            string[] datosCliente = strCliente.Split(';');
            lbNomCliente.Text = datosCliente[0];
            hfCodAlmacen.Value = datosCliente[1];
        }

        protected void btAddCount_Click(object sender, EventArgs e)
        {            
            if (ValidarCantidad())
            {
                oPlanchado.UpdateCount(hfCodProducto.Value, lbDescProdSelec.Text, "", int.Parse(txPaq.Text), int.Parse(txEtiqPaq.Text),
                    int.Parse(txUnidades.Text), int.Parse(txEtiqUds.Text), int.Parse(lbUdsPaq.Text), ddlCalandras.SelectedItem.Value ,editandoFila);
                LimpiarControles();
                gridConteo.DataSource = oPlanchado.DatosConteo;
                gridConteo.DataBind();
                editandoFila = false;
                gridConteo.SelectedIndex = -1;
            }
            else
            {
                txPaq.Text = "";
            }
        }

        private void LimpiarControles()
        {
            lbDescProdSelec.Text = "";
            hfCodProducto.Value = "";
            txPaq.Text = "";
            txPaq.Enabled = false;
            txEtiqPaq.Text = "";
            txEtiqPaq.Enabled = false;
            txUnidades.Text = "";
            txUnidades.Enabled = false;
            txEtiqUds.Text = "";
            txEtiqUds.Enabled = false;
            lbUdsPaq.Text = "";
            lbQtyAlmacen.Text = "";
        }

        protected void gridConteo_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (!filaEliminada)
                oPlanchado.DeleteCountLine(e.RowIndex);

            filaEliminada = !filaEliminada;
            gridConteo.DataSource = oPlanchado.DatosConteo;
            gridConteo.DataBind();
        }

        protected void btCancel_Click(object sender, EventArgs e)
        {
            LimpiarControles();
            gridConteo.SelectedIndex = -1;
        }

        protected void btRegistrar_Click(object sender, EventArgs e)
        {
            oPlanchado.RegisterCount(txCodCliente.Text);
            gridConteo.DataSource = oPlanchado.DatosConteo;
            gridConteo.DataBind();
        }

        protected void gridConteo_SelectedIndexChanged(object sender, EventArgs e)
        {
            int iRow = gridConteo.SelectedIndex;
            hfCodProducto.Value = gridConteo.Rows[iRow].Cells[2].Text;
            lbDescProdSelec.Text = gridConteo.Rows[iRow].Cells[3].Text;
            lbQtyAlmacen.Text = ObtenerInventProdAlm(hfCodProducto.Value, hfCodAlmacen.Value).ToString();
            txPaq.Text = gridConteo.Rows[iRow].Cells[5].Text;
            txPaq.Enabled = true;
            txEtiqPaq.Text = gridConteo.Rows[iRow].Cells[6].Text;
            txEtiqPaq.Enabled = true;
            txUnidades.Text = gridConteo.Rows[iRow].Cells[7].Text;
            txUnidades.Enabled = true;
            txEtiqUds.Text = gridConteo.Rows[iRow].Cells[8].Text;
            txEtiqUds.Enabled = true;
            lbUdsPaq.Text = gridConteo.Rows[iRow].Cells[9].Text;            
            txPaq.Focus();
            editandoFila = true;
        }

        private int ObtenerInventProdAlm(string producto, string almacen)
        {
            cProduccion oProduccion = new cProduccion(Session["empresaLogin"].ToString());
            return (oProduccion.GetItemLocationInventory(producto, almacen));
        }

        private int ObtenerCantPorPaquete(string producto)
        {
            cProduccion oProduccion = new cProduccion(Session["empresaLogin"].ToString());
            return (oProduccion.GetItemPaqQty(producto));
        }

        
        private bool ValidarCantidad()
        {
            return((txPaq.Text != "")||(txUnidades.Text != ""));            
        }
        

        protected void ddlCalandras_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbCalandraSel.Text = ddlCalandras.SelectedItem.Text;
        }

        protected void txPaq_TextChanged(object sender, EventArgs e)
        {
            txEtiqPaq.Text = txPaq.Text;
            txUnidades.Focus();
        }

        protected void txUnidades_TextChanged(object sender, EventArgs e)
        {
            txEtiqUds.Text = (txUnidades.Text != "0") ? "1" : "0";
        }

    }
}
