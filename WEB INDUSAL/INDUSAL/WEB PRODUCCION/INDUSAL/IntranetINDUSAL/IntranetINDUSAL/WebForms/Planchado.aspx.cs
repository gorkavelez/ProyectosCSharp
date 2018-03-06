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
    public partial class Planchado : System.Web.UI.Page
    {
        private const string clienteSAL = "000000";

        static cPlanchado oPlanchado;
        static cSurtidoSAL oSurtidoSAL;
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
                oSurtidoSAL = null;               
            }

            MostrarTitulo();
            MostrarBotonesFamilia();
            MostrarBotonesSubfamilia();
            try
            {
                MostrarBotonesProductos();
            }
            catch (Exception ex)
            { }

            VisualizarPanelesSAL();
        }

        private void InicializarClasePlanchado()
        {
            string tipo = Request.QueryString["Tipo"];
            oPlanchado = new cPlanchado(Session["empresaLogin"].ToString(), int.Parse(tipo));

        }

        private void MostrarTitulo()
        {
            switch (oPlanchado.TipoPlanchadoToInt(oPlanchado.TipoPlanchado))
            {
                case 4:
                    this.Title = "PLANCHADO: CALANDRAS";
                    this.lbCalandra.Visible = true;
                    this.ddlMaquinas.Visible = true;
                    break;
                case 5:
                    this.Title = "PLANCHADO: FELPA";
                    this.lbCalandra.Visible = false;
                    this.ddlMaquinas.Visible = false;
                    break;
                case 6:
                    this.Title = "PLANCHADO: FORMA";
                    this.lbCalandra.Visible = false;
                    this.ddlMaquinas.Visible = false;
                    break;
            }
        }

        protected void txCodCliente_TextChanged(object sender, EventArgs e)
        {
            if (txCodCliente.Text != clienteSAL)
            {
                if (ObtenerNombreCliente(txCodCliente.Text))
                    MostrarSurtidoCliente(txCodCliente.Text);
                else
                {
                    INIKER_gridSurtido.Datos = null;
                    INIKER_gridSurtido.GridDataBind();
                }
                pnlClteNormal.Visible = true;
                pnlClteSAL.Visible = false;
            }
            else
            {
                if (ObtenerNombreCliente(txCodCliente.Text))
                    MostrarSurtidoClienteSAL(txCodCliente.Text);
                pnlClteNormal.Visible = false;
                pnlClteSAL.Visible = true;
            }

            VisualizarPanelesSAL();
        }

        protected void INIKER_gridSurtido_ProductClick(object sender, INIKER_ButtonDG.ProductClickEventArgs e)
        {
            SeleccionarProducto(e.ProdCode,e.ProdDesc);
            
        }

        private void SeleccionarProducto(string codigo, string descripcion)
        {
            lbDescProdSelec.Text = descripcion;
            hfCodProducto.Value = codigo;
            if (txCodCliente.Text==clienteSAL)
                lbQtyAlmacen.Text = ObtenerInventProdAlm(codigo, hfCodAlmacen.Value).ToString();

            txUdsPaq.Text = ObtenerCantPorPaquete(codigo).ToString();
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
            tablaSurtido = surtido.ObtenerSurtidoClienteClasificado(cliente, Session["empresaLogin"].ToString(), oPlanchado.TipoPlanchado);
            INIKER_gridSurtido.Datos = tablaSurtido;
            INIKER_gridSurtido.GridDataBind();
        }

        protected void MostrarSurtidoClienteSAL(string cliente)
        {
            if (oSurtidoSAL==null)
                oSurtidoSAL = new cSurtidoSAL(cliente, Session["empresaLogin"].ToString(), oPlanchado.TipoPlanchado);

            MostrarBotonesFamilia();            
        }        

        private void MostrarBotonesFamilia()
        {
            pnlFamilias.Controls.Clear();                        

            if (oSurtidoSAL != null)
            {
                string[] familias = oSurtidoSAL.ArrayFamilias();                
                foreach (string oFam in familias)
                {
                    pnlFamilias.Controls.Add(CreateButton(oFam,"f_"+oFam,""));
                }
            }
        }

        private void MostrarBotonesSubfamilia()
        {
            pnlSubfamilias.Controls.Clear();            

            if ((oSurtidoSAL != null)&&(lbFamSel.Text!=""))
            {
                string[] subfamilias = oSurtidoSAL.ArraySubfamilias();                
                foreach (string oSubfam in subfamilias)
                {
                    pnlSubfamilias.Controls.Add(CreateButton(oSubfam,"s_"+oSubfam,""));
                }
            }
        }

        private void MostrarBotonesProductos()
        {
            pnlProductos.Controls.Clear();

            if ((oSurtidoSAL != null) && (lbSubfamSel.Text != ""))
            {
                string productos = oSurtidoSAL.ArrayProductos("|");
                string[] arrayProductos = productos.Split('|');
                
                foreach (string oProd in arrayProductos)
                {
                    string[] datosProducto = oProd.Split(';');
                    pnlProductos.Controls.Add(CreateButton(datosProducto[1], "p_" + datosProducto[0], datosProducto[0]));                    
                }
            }
        }

        private Button CreateButton(string pText,string pID,string code)
        {
            // instancia de objeto
            Button newButton = new Button();
            // establecimiento de propiedades
            newButton.ID = pID;
            newButton.Text = pText;
            newButton.ToolTip = pText;
            newButton.CssClass = "textoBotonSurtido";                        

            if (code != "")
                newButton.CommandName = code;

            newButton.Click += new EventHandler(FamilyButton_Click);

            return (newButton);
        }

        protected void FamilyButton_Click(object sender, EventArgs e)
        {
            Button buttonSender=(Button) sender;
            switch (buttonSender.ID.Substring(0, 2))
            {
                case "f_":            
                    lbFamSel.Text = buttonSender.Text;
                    oSurtidoSAL.famSel = buttonSender.Text;
                    // se muestran las subfamilias de la familia seleccionada
                    MostrarBotonesSubfamilia();
                    oSurtidoSAL.subfamSel = null;
                    lbSubfamSel.Text = "";
                    break;
                case "s_":            
                    lbSubfamSel.Text = buttonSender.Text;
                    oSurtidoSAL.subfamSel = buttonSender.Text;
                    MostrarBotonesProductos();
                    break;
                case "p_":
                    SeleccionarProducto(buttonSender.CommandName, buttonSender.Text);
                    break;
            }

            VisualizarPanelesSAL();

        }

        protected bool ObtenerNombreCliente(string cliente)
        {
            cProduccion clientes = new cProduccion(Session["empresaLogin"].ToString());
            string strCliente = clientes.GetCustomerData(cliente);
            if (strCliente != "")
            {
                string[] datosCliente = strCliente.Split(';');
                lbNomCliente.Text = datosCliente[0];
                hfCodAlmacen.Value = datosCliente[1];
                return (true);
            }
            else
            {
                txCodCliente.Text = "";
                lbNomCliente.Text = "";
                hfCodAlmacen.Value = "";
                return (false);
            }
        }

        protected void btAddCount_Click(object sender, EventArgs e)
        {
            if (ValidarCantidad())
            {
                if (oPlanchado.TipoPlanchado == INIKER.CrossReferences.Tipo_Planchado.Calandra)
                {
                    oPlanchado.UpdateCount(hfCodProducto.Value, lbDescProdSelec.Text, "", int.Parse(txPaq.Text), int.Parse(txEtiqPaq.Text),
                        int.Parse(txUnidades.Text), int.Parse(txEtiqUds.Text), int.Parse(txUdsPaq.Text),
                        ddlMaquinas.SelectedItem.Value, editandoFila);
                }
                else
                {
                    oPlanchado.UpdateCount(hfCodProducto.Value, lbDescProdSelec.Text, "", int.Parse(txPaq.Text), int.Parse(txEtiqPaq.Text),
                        int.Parse(txUnidades.Text), int.Parse(txEtiqUds.Text), int.Parse(txUdsPaq.Text), "", editandoFila);
                }
                LimpiarControles();
                gridConteo.DataSource = oPlanchado.DatosConteo;
                gridConteo.DataBind();
                editandoFila = false;
                gridConteo.SelectedIndex = -1;

                btRegistrar.Visible = (oPlanchado.DatosConteo.Rows.Count > 0);
                btCancelar.Visible = (oPlanchado.DatosConteo.Rows.Count > 0);
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
            txUdsPaq.Text = "";
            lbQtyAlmacen.Text = "";
        }

        protected void gridConteo_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (!filaEliminada)
                oPlanchado.DeleteCountLine(e.RowIndex);

            filaEliminada = !filaEliminada;
            gridConteo.DataSource = oPlanchado.DatosConteo;
            gridConteo.DataBind();

            btRegistrar.Visible = (oPlanchado.DatosConteo.Rows.Count > 0);
            btCancelar.Visible = (oPlanchado.DatosConteo.Rows.Count > 0);
        }

        protected void btCancel_Click(object sender, EventArgs e)
        {
            LimpiarControles();
            gridConteo.SelectedIndex = -1;
        }

        protected void btRegistrar_Click(object sender, EventArgs e)
        {
            oPlanchado.Registrar(txCodCliente.Text);
            gridConteo.DataSource = oPlanchado.DatosConteo;
            gridConteo.DataBind();

            btRegistrar.Visible = (oPlanchado.DatosConteo.Rows.Count > 0);
            btCancelar.Visible = (oPlanchado.DatosConteo.Rows.Count > 0);

            INIKER_gridSurtido.Datos = null;
            INIKER_gridSurtido.GridDataBind();
            LimpiarControles();
            txCodCliente.Text = "";
            lbDescProdSelec.Text = "";
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
            txUdsPaq.Text = gridConteo.Rows[iRow].Cells[9].Text;
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
            return ((txPaq.Text != "") || (txUnidades.Text != ""));
        }

        protected void ddlMaquinas_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbCalandraSel.Text = ddlMaquinas.SelectedItem.Text;
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

        protected void btCancelar_Click(object sender, EventArgs e)
        {
            oPlanchado.Cancelar();
            gridConteo.DataSource = oPlanchado.DatosConteo;
            gridConteo.DataBind();

            btRegistrar.Visible = (oPlanchado.DatosConteo.Rows.Count > 0);
            btCancelar.Visible = (oPlanchado.DatosConteo.Rows.Count > 0);
        }

        protected void gridConteo_DataBound(object sender, EventArgs e)
        {
            for (int iRow = 0; iRow < gridConteo.Rows.Count; iRow++)
            {
                if (gridConteo.Rows[iRow].RowType == DataControlRowType.DataRow)
                {
                    for (int iCell = 0; iCell < gridConteo.Rows[iRow].Cells.Count; iCell++)
                    {
                        try
                        {
                            Single valor = Single.Parse(gridConteo.Rows[iRow].Cells[iCell].Text);
                            gridConteo.Rows[iRow].Cells[iCell].HorizontalAlign = HorizontalAlign.Right;
                        }
                        catch (Exception ex)
                        {}
                    }
                }
            }            
        }

        private void VisualizarPanelesSAL()
        {
            // se inicializan todos los paneles a no visibles
            pnlTitFamilias.Visible = false;
            pnlFamilias.Visible = false;
            pnlTitSubfam.Visible = false;
            pnlSubfamilias.Visible = false;
            pnlTitProductos.Visible = false;
            pnlProductos.Visible = false;

            if (oSurtidoSAL != null)
            {
                pnlTitFamilias.Visible = true;
                pnlFamilias.Visible = true;
                pnlTitSubfam.Visible = (oSurtidoSAL.famSel != null);
                pnlSubfamilias.Visible = (oSurtidoSAL.famSel != null);
                pnlTitProductos.Visible = (oSurtidoSAL.subfamSel != null);
                pnlProductos.Visible = (oSurtidoSAL.subfamSel != null);
            }                
        }
    }
}
