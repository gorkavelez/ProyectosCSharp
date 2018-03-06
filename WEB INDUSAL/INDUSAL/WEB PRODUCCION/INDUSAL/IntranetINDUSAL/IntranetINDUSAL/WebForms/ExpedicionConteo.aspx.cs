using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IntranetINDUSAL.Negocio;
using IntranetINDUSAL.Controles_Personalizados;

namespace IntranetINDUSAL.WebForms
{
    public partial class ExpedicionConteo : System.Web.UI.Page
    {
        static cExpedicionConteo oExpedicion;        

        protected void Page_Load(object sender, EventArgs e)
        {
            this.INIKER_teclado.OKClick +=
                   new INIKER_tecladoNumerico.OKClickHandler(INIKER_teclado_OKClick);

            if (!IsPostBack)
            {
                oExpedicion = null;
                oExpedicion = new cExpedicionConteo(Session["empresaLogin"].ToString());
                CargarRutasTransporte();
                GetCustomer();
                MostrarDatosExpedicion();
            }

            MostrarBotonesFacturacion();
        }

        private void CargarRutasTransporte()
        {
            cProduccion oProduccion = new cProduccion(Session["empresaLogin"].ToString());            
            ddlRutas.DataSource = oProduccion.GetTransRoutes();
            ddlRutas.DataValueField = INIKER.RutasTransporte.ListaRTransporteINDUSAL_Fields.Codigo.ToString();
            ddlRutas.DataTextField = INIKER.RutasTransporte.ListaRTransporteINDUSAL_Fields.Nombre.ToString();
            ddlRutas.DataBind();
            ddlRutas.Items.Add("");
            ddlRutas.SelectedIndex = ddlRutas.Items.Count - 1;
        }


        protected void INIKER_teclado_OKClick(object sender, INIKER_tecladoNumerico.OKClickEventArgs e)
        {
            oExpedicion.PedidoCantPendiente = int.Parse(e.Valor);
            oExpedicion.UpdateOrderLine();
            
            // una vez actualizadas las propiedades de la clase, se muestran los datos
            MostrarDatosExpedicion();
            // se deselecciona la fila del datagrid
            grdPedido.SelectedIndex = -1;
        }

        protected void ddlRutas_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList oDDL = (DropDownList)sender;
            SeleccionarRuta(oDDL.SelectedItem.Value, oDDL.SelectedItem.Text);            
        }

        protected void ddlClientesRuta_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList oDDL = (DropDownList)sender;
            SeleccionarCliente(oDDL.SelectedItem.Value, oDDL.SelectedItem.Text);
        }

        private void GetCustomer()
        {
            // Filtra los clientes con pedidos en curso que tienen asignada la ruta pasada
            // en el parámetro y los carga en el DropDownList
            cProduccion oProduccion = new cProduccion(Session["empresaLogin"].ToString());
            ddlClientesRuta.DataSource = oProduccion.GetOpenOrderCustList();
            ddlClientesRuta.DataValueField = INIKER.Cliente.ListaClientesINDUSAL_Fields.No.ToString();
            ddlClientesRuta.DataTextField= INIKER.Cliente.ListaClientesINDUSAL_Fields.Name.ToString();
            ddlClientesRuta.DataBind();
        }

        private void GetOutstandingLines(string cliente)
        { 
            // Filtra las líneas de pedido de venta del cliente, 
            // con cantidad pendiente mayor que cero
            cProduccion oProduccion = new cProduccion(Session["empresaLogin"].ToString());
            string nPedido = oProduccion.GetLastOpenOrderNo(cliente);
            if (nPedido != "")
            {
                INIKER.LineasVenta.ListaLineasVentaINDUSAL[] lineas = oProduccion.GetOutstandingLines(nPedido);
                if (lineas.Count() > 0)
                {
                    oExpedicion.NPedido = nPedido;
                    oExpedicion.ArrayToDataTable(lineas);
                }
            }

            MostrarDatosExpedicion();
        }

        private void SeleccionarRuta(string codigo, string nombre)
        {
            // se almacenan en la expedición en curso los datos de la ruta seleccionada
            oExpedicion.CodRuta = codigo;
            oExpedicion.NomRuta = nombre;
            // se muestran los datos almacenados
            MostrarDatosExpedicion();
            //GetCustomer();
        }

        private void SeleccionarCliente(string codigo, string nombre)
        {
            // se almacenan en la expedición en curso los datos de la ruta seleccionada
            oExpedicion.CodCliente = codigo;
            oExpedicion.NomCliente = nombre;
            // se muestran los datos almacenados
            MostrarDatosExpedicion();
            GetOutstandingLines(codigo);
        }

        private void MostrarDatosExpedicion()
        {
            lbDescRuta.Text = oExpedicion.NomRuta;
            txCliente.Text = oExpedicion.CodCliente;
            try
            {
                grdPedido.DataSource = oExpedicion.Pedido;
                grdPedido.DataBind();
            }
            catch (NullReferenceException ex)
            { }
        }

        protected void grdPedido_DataBound(object sender, EventArgs e)
        {
            //formateo la anchura de las columnas
            //gridConteo.Columns[0].ItemStyle.Width=50;
            //gridConteo.Columns[1].ItemStyle.Width = 50;
            //gridConteo.Columns[2].ItemStyle.Width = 50;
            //gridConteo.Columns[3].ItemStyle.Width = 150;
            //gridConteo.Columns[4].ItemStyle.Width = 50;
            //gridConteo.Columns[5].ItemStyle.Width = 50;
            //gridConteo.Columns[6].ItemStyle.Width = 50;
            // se oculta la columna de Key            
            //grdPedido.Columns[grdPedido.Columns.Count - 1].Visible = false;

            for (int iRow = 0; iRow < grdPedido.Rows.Count; iRow++)
            {
                if (grdPedido.Rows[iRow].RowType == DataControlRowType.DataRow)
                {
                    try
                    {
                        grdPedido.Rows[iRow].Cells[0].Attributes.Add("onClick", "javascript:return ConfirmAction('eliminar el registro');");
                    }
                    catch (Exception ex)
                    { }

                    for (int iCell = 0; iCell < grdPedido.Rows[iRow].Cells.Count; iCell++)
                    {
                        try
                        {
                            Single valor = Single.Parse(grdPedido.Rows[iRow].Cells[iCell].Text);
                            grdPedido.Rows[iRow].Cells[iCell].HorizontalAlign = HorizontalAlign.Right;
                        }
                        catch (Exception ex)
                        { }
                    }
                }
            }            
        }

        protected void grdPedido_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            oExpedicion.NPedido =grdPedido.Rows[e.RowIndex].Cells[2].Text;
            oExpedicion.NLinea=int.Parse(grdPedido.Rows[e.RowIndex].Cells[3].Text);
            oExpedicion.DeleteOrderLine();
            MostrarDatosExpedicion();
        }

        protected void grdPedido_SelectedIndexChanged(object sender, EventArgs e)
        {
            // se selecciona la línea de pedido en la clase
            oExpedicion.NPedido = grdPedido.Rows[grdPedido.SelectedIndex].Cells[2].Text;
            oExpedicion.NLinea = 
                int.Parse(grdPedido.Rows[grdPedido.SelectedIndex].Cells[3].Text);
            oExpedicion.SelectOrderLine();            
            // se muestra en el teclado la cantidad actual            
            INIKER_teclado.TituloDato = oExpedicion.PedidoDescProducto;
            INIKER_teclado.Dato = oExpedicion.PedidoCantPendiente.ToString();
        }

        protected void btRegistrar_Click(object sender, EventArgs e)
        {            
            INIKER.LineasVenta.ListaLineasVentaINDUSAL[] lineas =oExpedicion.OrderLinesToArray();
            cProduccion oProduccion = new cProduccion(Session["empresaLogin"].ToString());
            oProduccion.UpdateOutstandingLines(lineas);
            MostrarBotonesFacturacion();
        }

        private void MostrarBotonesFacturacion()
        {
            pnlCodsFact.Controls.Clear();

            if (oExpedicion.CodCliente != "")
            {
                cProductos oSurtido = new cProductos();
                DataTable dtSurtFact = oSurtido.GetCustomerReferences(oExpedicion.CodCliente, Session["empresaLogin"].ToString());

                DataRow[] surtidoFiltrado = dtSurtFact.Select("codFactProducto is not null");

                foreach (DataRow producto in surtidoFiltrado)
                {
                    if (oExpedicion.ItemInOrder(producto["codProducto"].ToString()))
                    {
                        if (!ButtonExists(producto["codFactProducto"].ToString()))
                        {
                            pnlCodsFact.Controls.Add(
                                CreateButton(producto["desFactProducto"].ToString(),
                                                producto["codFactProducto"].ToString(),
                                                producto["codProducto"].ToString()));
                        }
                    }
                }
            }
        }

        private bool ButtonExists(string pID)
        {
            Control oCtrl = pnlCodsFact.FindControl(pID);
            return (oCtrl != null);
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
            // se captura el objeto que lanza el evento
            Button buttonSender = (Button)sender;
            // se almacena la información del producto que se va a pesar con los datos de la expedición
            // y se guarda el objeto en la sesión de usuario para recuperarlo desde el formulario
            // de registro de las pesadas de báscula
            oExpedicion.CodFacturacion = buttonSender.ID;
            oExpedicion.DescFacturacion = buttonSender.ToolTip;
            Session["expedicionConteo"] = oExpedicion;
            // se carga la página de registro de pesajes
            Response.Redirect("~/Webforms/RegistroExpedicion.aspx");
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

        protected void txCliente_TextChanged(object sender, EventArgs e)
        {
            ddlClientesRuta.SelectedIndex = ddlClientesRuta.Items.IndexOf(ddlClientesRuta.Items.FindByValue(txCliente.Text));
            if (ddlClientesRuta.SelectedIndex != -1)
            {                
                oExpedicion.CodCliente = ddlClientesRuta.SelectedItem.Value;
                oExpedicion.NomCliente = ddlClientesRuta.SelectedItem.Text;
            }
            else
            {                
                oExpedicion.CodCliente = "";
                oExpedicion.NomCliente = "";
            }
        }
    }
}
