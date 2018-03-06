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
    public partial class ConteoRopa : System.Web.UI.Page
    {
        static cConteo oConteo;
        static bool filaEliminada=false;
        static bool editandoFila = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            // se añade un controlador de evento para poder recoger las pulsaciones sobre los botones del
            // datagrid de surtido de cliente.
            this.INIKER_gridSurtido.ProductClick += new INIKER_ButtonDG.ProductClickHandler(INIKER_gridSurtido_ProductClick);

            if (!IsPostBack)
            {
                // se crea la instancia del objeto para la gestión del conteo
                oConteo = new cConteo(Session["empresaLogin"].ToString(),cLineasConteo.TipoConteo.Normal);                
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
            hfCodProducto.Value=e.ProdCode;
            lbQtyAlmacen.Text = ObtenerInventProdAlm(e.ProdCode, hfCodAlmacen.Value).ToString();
            txCantidad.Enabled = true;
            txCantidad.Focus();            
        }

        protected void MostrarSurtidoCliente(string cliente)
        {
            cProductos surtido = new cProductos();
            DataTable tablaSurtido;
            tablaSurtido = surtido.ObtenerSurtidoClienteClasificado(cliente, Session["empresaLogin"].ToString());
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
            if ((txCantidad.Text != "")&&(int.Parse(txCantidad.Text)>0)&&(int.Parse(txCantidad.Text)<=int.Parse(lbQtyAlmacen.Text)))
            {
                oConteo.UpdateCount(hfCodProducto.Value, lbDescProdSelec.Text, "", int.Parse(txCantidad.Text),editandoFila);
                LimpiarControles();
                gridConteo.DataSource = oConteo.DatosConteo;
                gridConteo.DataBind();
                editandoFila = false;
                gridConteo.SelectedIndex = -1;
            }
            else
            {
                txCantidad.Text = "";
            }
        }

        private void LimpiarControles()
        {
            lbDescProdSelec.Text = "";
            hfCodProducto.Value = "";
            txCantidad.Text = "";
            txCantidad.Enabled = false;
            lbQtyAlmacen.Text = "";
        }

        protected void gridConteo_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (!filaEliminada)            
                oConteo.DeleteCountLine(e.RowIndex);                
            
            filaEliminada = !filaEliminada;
            gridConteo.DataSource = oConteo.DatosConteo;
            gridConteo.DataBind();
        }

        protected void btCancel_Click(object sender, EventArgs e)
        {
            LimpiarControles();
        }

        protected void btRegistrar_Click(object sender, EventArgs e)
        {
            oConteo.RegisterCount(txCodCliente.Text);
            gridConteo.DataSource = oConteo.DatosConteo;
            gridConteo.DataBind();
        }

        protected void gridConteo_SelectedIndexChanged(object sender, EventArgs e)
        {
            int iRow = gridConteo.SelectedIndex;
            hfCodProducto.Value = gridConteo.Rows[iRow].Cells[2].Text;
            lbDescProdSelec.Text = gridConteo.Rows[iRow].Cells[3].Text;
            lbQtyAlmacen.Text = ObtenerInventProdAlm(hfCodProducto.Value, hfCodAlmacen.Value).ToString();
            txCantidad.Text = gridConteo.Rows[iRow].Cells[4].Text;
            txCantidad.Enabled = true;
            txCantidad.Focus();
            editandoFila = true;
        }

        private int ObtenerInventProdAlm(string producto, string almacen)
        {
            cProduccion oProduccion = new cProduccion(Session["empresaLogin"].ToString());
            return(oProduccion.GetItemLocationInventory(producto,almacen));
        }

    }

}
