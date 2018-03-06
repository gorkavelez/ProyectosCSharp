using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IntranetINDUSAL.Negocio;
using System.Text;

namespace IntranetINDUSAL.WebForms
{
    public partial class Plegado : System.Web.UI.Page
    {
        static cPlegado oPlegado;
        static cSurtidoCliente oSurtidoSAL;
        static cClientes oClientes;
        static cPedidosVenta oPedidos;

        protected void Page_Load(object sender, EventArgs e)
        {
            //txCodCliente.Attributes.Add("OnChange", "javascrit:return DatosObligatorios();");            
            if (!IsPostBack)
            {
                // elimino las instancias anteriores de los objetos
                oSurtidoSAL = null;
                oPlegado = null;

                GetClientes();

                // se crea la instancia del objeto para la gestión del conteo
                InicializarClasePlanchado();
                if (oPlegado.CodCliente!=null)
                    MostrarSurtidoClienteSAL(oPlegado.CodCliente);

                MostrarEmpleados();
                MostrarDatosClase();
                MostrarSubfamilia(oPlegado.FamiliaSel);
                MostrarProductos(oPlegado.SubfamiliaSel);
                
            }

            WS_CreateButtons();

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

        #region EVENTOS CONTROLES
        
            protected void ddlMaquinas_SelectedIndexChanged(object sender, EventArgs e)
            {
                //lbCalandraSel.Text = ddlMaquinas.SelectedItem.Text;
                // se pasan los datos a la clase
                oPlegado.CodMaquina = ddlMaquinas.SelectedItem.Value;
                oPlegado.DescMaquina = ddlMaquinas.SelectedItem.Text;
                // se guarda el dato en el control oculto para la comprobación
                // desde javascript
                hdCodMaquina.Value = ddlMaquinas.SelectedItem.Value;
            }                

            protected void ddlEmpleados_SelectedIndexChanged(object sender, EventArgs e)
            {
                oPlegado.CodOperario = ddlEmpleados.SelectedItem.Value;
                oPlegado.NomOperario = ddlEmpleados.SelectedItem.Text;                
                hdCodOperario.Value = oPlegado.CodOperario;
                MostrarDatosClase();
            }

            protected void ddlMaquinas_DataBound(object sender, EventArgs e)
            {
                // al cargarse los datos, se reinicia el índice de elemento seleccionado
                ddlMaquinas.Text = "";
            }

        #endregion

        #region SURTIDO CLIENTE

            private void SeleccionarProducto(string codigo, string descripcion)
            {

                oPlegado.CodProducto = codigo;
                oPlegado.DescProducto = descripcion;
                oPlegado.UdsPorPaquete = ObtenerCantPorPaquete(codigo);
                // se lanza el webform de registro de unidades plegadas
                Session["objetoPlegado"] = oPlegado;
                Response.Redirect("~/Webforms/RegistroPlegado.aspx");
            }

            protected void MostrarSurtidoClienteSAL(string cliente)
            {
                LimpiarSurtidoCliente();

                if (oSurtidoSAL == null)
                    oSurtidoSAL = new cSurtidoCliente(cliente, Session["empresaLogin"].ToString(), oPlegado.TipoPlanchado);

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
                        pnlFamilias.Controls.Add(CreateButton(oFam, "f_" + oFam, ""));
                    }
                }
            }

            private void MostrarSubfamilia(string familia)
            {
                if (familia != null)
                {
                    lbFamSel.Text = familia;
                    oSurtidoSAL.famSel = familia;
                    oPlegado.FamiliaSel = familia;
                    // se muestran las subfamilias de la familia seleccionada
                    MostrarBotonesSubfamilia();
                    oSurtidoSAL.subfamSel = null;
                    lbSubfamSel.Text = "";
                }
            }

            private void MostrarBotonesSubfamilia()
            {
                pnlSubfamilias.Controls.Clear();

                if ((oSurtidoSAL != null) && (lbFamSel.Text != ""))
                {
                    string[] subfamilias = oSurtidoSAL.ArraySubfamilias();
                    foreach (string oSubfam in subfamilias)
                    {
                        pnlSubfamilias.Controls.Add(CreateButton(oSubfam, "s_" + oSubfam, ""));
                    }
                }
            }

            private void MostrarProductos(string subfamilia)
            {
                if (subfamilia != null)
                {
                    lbSubfamSel.Text = subfamilia;
                    oSurtidoSAL.subfamSel = subfamilia;
                    oPlegado.SubfamiliaSel = subfamilia;
                    MostrarBotonesProductos();
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

            private Button CreateButton(string pText, string pID, string code)
            {
                // instancia de objeto
                Button newButton = new Button();
                // establecimiento de propiedades
                newButton.ID = pID;
                //newButton.Text = pText;
                newButton.Text = PrepararTexto(pText, 15, 2);
                newButton.ToolTip = pText;
                newButton.CssClass = "textoBotonSurtido";

                if (code != "")
                    newButton.CommandName = code;

                newButton.Click += new EventHandler(FamilyButton_Click);
                //newButton.Attributes.Add("OnClick", "javascript:return DatosObligatorios();");

                return (newButton);
            }

            protected void FamilyButton_Click(object sender, EventArgs e)
            {
                Button buttonSender = (Button)sender;
                switch (buttonSender.ID.Substring(0, 2))
                {
                    case "f_":
                        MostrarSubfamilia(buttonSender.ToolTip);
                        break;
                    case "s_":
                        MostrarProductos(buttonSender.ToolTip);
                        break;
                    case "p_":
                        SeleccionarProducto(buttonSender.CommandName, buttonSender.ToolTip);
                        break;
                }

                VisualizarPanelesSAL();

            }

            private string PrepararTexto(string initText, int maxLength, int maxLineas)
            {
                // Método para dividir un texto en líneas de longitud determinada
                string[] words;
                string endText = "";
                string linea = "";
                int nLineas = 0;

                if (initText.Length > maxLength)
                {
                    // se separa la cadena original en palabras, utilizando los espacios como separador
                    words = initText.Split(' ');

                    for (int iWord = 0; iWord < words.Length; iWord++)
                    {
                        if ((linea.Length + words[iWord].Length) > maxLength)
                        {
                            endText += (endText.Length == 0) ? "" : "&#10;";
                            endText += linea;
                            nLineas++;
                            linea = "";
                        }

                        linea += (linea.Length == 0) ? "" : " ";
                        linea += words[iWord];
                    }
                    //hay que añadir la última línea generada
                    if (linea.Length > 0)
                    {
                        endText += (endText.Length == 0) ? "" : "&#10;";
                        endText += linea;
                        nLineas++;
                    }
                }
                else
                {
                    endText = initText;
                    nLineas++;
                }

                for (int iLinea = nLineas; iLinea <= maxLineas; iLinea++)
                {
                    endText += "&#10;";
                }

                return (HttpUtility.HtmlDecode(endText));
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

            private void LimpiarSurtidoCliente()
            {
                lbFamSel.Text = "";
                lbSubfamSel.Text = "";

                pnlFamilias.Controls.Clear();
                pnlSubfamilias.Controls.Clear();
                pnlProductos.Controls.Clear();
            }

        #endregion
        
        private void InicializarClasePlanchado()
        {            
            oPlegado = (cPlegado)Session["objetoPlegado"];
            string tipo = Request.QueryString["Tipo"];

            if (oPlegado == null)
                oPlegado = new cPlegado(Session["empresaLogin"].ToString(), int.Parse(tipo));
            else
            {
                if (int.Parse(tipo) != oPlegado.TipoPlanchadoToInt(oPlegado.TipoPlanchado))
                {
                    oPlegado = new cPlegado(Session["empresaLogin"].ToString(), int.Parse(tipo));
                }
            }
            MostrarCalandras();
        }

        private void MostrarCalandras()
        {
            if (oPlegado.TipoPlanchado == INIKER.CrossReferences.Tipo_Planchado.Calandra)
            {
                ddlMaquinas.DataSource = oPlegado.Calandras;
                ddlMaquinas.DataValueField = oPlegado.Calandras.Columns["codigo"].ToString();
                ddlMaquinas.DataTextField = oPlegado.Calandras.Columns["descripcion"].ToString();
                ddlMaquinas.DataBind();
                ddlMaquinas.Items.Add("");
                ddlMaquinas.SelectedIndex = ddlMaquinas.Items.Count - 1;
            }
        }

        private void MostrarTitulo()
        {
            switch (oPlegado.TipoPlanchadoToInt(oPlegado.TipoPlanchado))
            {
                case 4:
                    this.Title = "PLEGADO: CALANDRAS";
                    this.lbCalandra.Visible = true;
                    this.ddlMaquinas.Visible = true;
                    break;
                case 5:
                    this.Title = "PLEGADO: FELPA";
                    this.lbCalandra.Visible = false;
                    this.ddlMaquinas.Visible = false;
                    break;
                case 6:
                    this.Title = "PLEGADO: FORMA";
                    this.lbCalandra.Visible = false;
                    this.ddlMaquinas.Visible = false;
                    break;
            }
        }

        private void MostrarEmpleados()
        {
            ddlEmpleados.DataSource = oPlegado.Empleados;
            ddlEmpleados.DataValueField = oPlegado.Empleados.Columns["codigo"].ToString();
            ddlEmpleados.DataTextField = oPlegado.Empleados.Columns["nombre"].ToString();
            ddlEmpleados.DataBind();
            ddlEmpleados.Items.Add("");
            ddlEmpleados.SelectedIndex = ddlEmpleados.Items.Count - 1;
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

        private void MostrarDatosClase()
        {
            // se eliminan los datos actuales
            InicializarControles();
            // si el objeto está instanciado, se muestran los datos que tenga
            if (oPlegado != null)
                {
                // datos de operario
                lbTurnoSel.Text = oPlegado.DesTurno;
                ddlEmpleados.SelectedIndex = ddlEmpleados.Items.Count - 1;
                ddlEmpleados.SelectedIndex = ddlEmpleados.Items.IndexOf(ddlEmpleados.Items.FindByValue(oPlegado.CodOperario));
                hdCodOperario.Value = oPlegado.CodOperario;
                // datos de máquina
                ddlMaquinas.SelectedIndex=ddlMaquinas.Items.IndexOf(ddlMaquinas.Items.FindByValue(oPlegado.CodMaquina));
                //lbCalandraSel.Text = oPlegado.DescMaquina;
                hdCodMaquina.Value = oPlegado.CodMaquina;
                //datos de cliente
                txCodCliente.Text = oPlegado.CodCliente;                
                hdCodCliente.Value = oPlegado.CodCliente;
                //pedidos del cliente
                txPedido.Text = oPlegado.NumPedido;
                if (oPedidos != null)
                {
                    grdPedidos.DataSource = oPedidos.DtPedidos;
                }                
                grdPedidos.DataBind();
            }
        }

        private void InicializarControles()
        {            
            // datos de operario
            ddlEmpleados.SelectedIndex = ddlEmpleados.Items.Count - 1;
            // datos de máquina
            ddlMaquinas.SelectedIndex = ddlMaquinas.Items.Count-1;
            //lbCalandraSel.Text = "";
            //datos de cliente
            txCodCliente.Text = "";            
        }

        #region TURNOS

            private void WS_CreateButtons()
            {
                WS_ClearPanel();

                foreach (DataRow turno in oPlegado.Turnos.Rows)
                {
                    Button WSButton = WS_CreateButton(turno["descripcion"].ToString(),
                                                    turno["codigo"].ToString(),
                                                    turno["codigo"].ToString());

                    WS_AddButtonToPanel(WSButton);
                }
            }

            private void WS_ClearPanel()
            {
                panelTurnos.Controls.Clear();
            }

            private void WS_AddButtonToPanel(Button oBt)
            {
                panelTurnos.Controls.Add(oBt);
            }

            private Button WS_CreateButton(string pText, string pID, string code)
            {
                // instancia de objeto
                Button newButton = new Button();
                // establecimiento de propiedades
                newButton.ID = pID;
                //newButton.Text = pText;
                newButton.Text = WS_PrepararTexto(pText, 8, 2);
                newButton.ToolTip = pText;
                newButton.CssClass = "textoBotonTurno";
                newButton.Font.Bold = true;

                if (code != "")
                    newButton.CommandName = code;

                newButton.Click += new EventHandler(WSButton_Click);

                return (newButton);
            }

            protected void WSButton_Click(object sender, EventArgs e)
            {
                Button buttonSender = (Button)sender;
                oPlegado.CodTurno = buttonSender.CommandName;
                oPlegado.DesTurno = buttonSender.Text;

                MostrarDatosClase();
            }

            private string WS_PrepararTexto(string initText, int maxLength, int maxLineas)
            {
                // Método para dividir un texto en líneas de longitud determinada
                string[] words;
                string endText = "";
                string linea = "";
                int nLineas = 0;

                if (initText.Length > maxLength)
                {
                    // se separa la cadena original en palabras, utilizando los espacios como separador
                    words = initText.Split(' ');

                    for (int iWord = 0; iWord < words.Length; iWord++)
                    {
                        if ((linea.Length + words[iWord].Length) > maxLength)
                        {
                            endText += (endText.Length == 0) ? "" : "&#10;";
                            endText += linea;
                            nLineas++;
                            linea = "";
                        }

                        linea += (linea.Length == 0) ? "" : " ";
                        linea += words[iWord];
                    }
                    //hay que añadir la última línea generada
                    if (linea.Length > 0)
                    {
                        endText += (endText.Length == 0) ? "" : "&#10;";
                        endText += linea;
                        nLineas++;
                    }
                }
                else
                {
                    endText = initText;
                    nLineas++;
                }

                for (int iLinea = nLineas; iLinea <= maxLineas; iLinea++)
                {
                    endText += "&#10;";
                }

                return (HttpUtility.HtmlDecode(endText));
            }

        #endregion

        #region CLIENTES

            protected void txCodCliente_TextChanged(object sender, EventArgs e)
            {
                oSurtidoSAL = null;
                lbFamSel.Text = "";

                ObtenerNombreCliente(txCodCliente.Text);
                MostrarSurtidoClienteSAL(txCodCliente.Text);
                pnlClteSAL.Visible = true;
                VisualizarPanelesSAL();

            }

            protected void GetClientes()
            {
                oClientes = null;
                oClientes = new cClientes(Session["empresaLogin"].ToString());
                oClientes.LoadDropDownList(ref ddlClientes);
            }

            protected void ObtenerNombreCliente(string cliente)
            {
                if (oClientes.Get(cliente))
                {
                    GetOrders(cliente);
                    oClientes.SelectDropDownList(ref ddlClientes);
                    oPlegado.CodCliente = cliente;
                    oPlegado.NomCliente = oClientes.Alias;
                    MostrarDatosClase();
                }
            }

            protected void ddlClientes_SelectedIndexChanged(object sender, EventArgs e)
            {
                ObtenerNombreCliente(ddlClientes.SelectedItem.Value);
            }

            private void GetOrders(string cliente)
            {
                oPedidos = new cPedidosVenta(Session["empresaLogin"].ToString(), cliente);
            }

        #endregion

            protected void grdPedidos_SelectedIndexChanged(object sender, EventArgs e)
            {
                oPlegado.NumPedido = grdPedidos.Rows[grdPedidos.SelectedIndex].Cells[2].Text;
                MostrarDatosClase();
            }

            protected void txPedido_TextChanged(object sender, EventArgs e)
            {
                oPedidos.SelectGridViewRow(ref grdPedidos, txPedido.Text);
                if (oPedidos.SelectIndex != -1)
                {
                    oPlegado.NumPedido = txPedido.Text;
                }
                else
                {
                    oPlegado.NumPedido = "";
                }

                MostrarDatosClase();
            }

    }
}
