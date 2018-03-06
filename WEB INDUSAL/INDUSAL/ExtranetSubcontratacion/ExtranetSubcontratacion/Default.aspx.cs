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
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    lbProveedor.Text = Session["vendorCode"].ToString();
                    lbNombreProv.Text = Session["vendorName"].ToString();
                    string lavanderia = Session["lavanderia"].ToString();
                    string cliente = Session["customerCode"].ToString();
                    string pedido = Session["orderNo"].ToString();

                    CargarEmpresas(lavanderia);

                    if (lavanderia != "")
                    {
                        CargarClientes(lavanderia, cliente);
                        if (cliente != "")
                        {
                            CargarPedidosProveedor(lavanderia, lbProveedor.Text, cliente);
                            if (pedido != "")
                            {
                                CargarDatosPedido();
                                panelLineasPedido.Visible = true;
                            }
                        }
                    }

                    lkbtDatos.Visible = (lavanderia != "");
                    btNuevoPedido.Visible = (cliente != "");
                    btBuscarAlbaranes.Visible = (cliente != "");
                }
                catch(NullReferenceException)
                {
                    CargarEmpresas("");
                }
            }            
        }      

        #region LAVANDERIAS

            private void CargarEmpresas(string _lavanderiaSelec)
            {
                proxyEntidades proxy = new proxyEntidades("02 NAVARRA");
                ddlLavanderias.DataSource = proxy.GetCompanyList();
                ddlLavanderias.DataTextField = "nombre";
                ddlLavanderias.DataValueField = "nombre";
                ddlLavanderias.DataBind();
                RadComboBoxItem newItem = new RadComboBoxItem("");
                ddlLavanderias.Items.Add(newItem);

                if (_lavanderiaSelec =="")
                    ddlLavanderias.SelectedIndex = ddlLavanderias.Items.Count - 1;
                else
                {
                    bool itemFound=false;

                    for (Int32 iItem = 0; (iItem < ddlLavanderias.Items.Count) && !itemFound; iItem++)
                    {
                        if (ddlLavanderias.Items[iItem].Value == _lavanderiaSelec)
                        {
                            ddlLavanderias.SelectedIndex = iItem;
                            itemFound = true;
                        }
                    }
                }
            }

            protected void ddlLavanderias_SelectedIndexChanged1(object o, RadComboBoxSelectedIndexChangedEventArgs e)
            {
                Session["lavanderia"] = ddlLavanderias.SelectedItem.Value;
                CargarClientes(ddlLavanderias.SelectedItem.Value, "");
                lkbtDatos.Visible = (ddlLavanderias.SelectedItem.Value != "");
            }

        #endregion

        #region CLIENTES

            private void CargarClientes(string _empresaSelec, string _clienteSelec)
            {
                try
                {
                    proxyEntidades proxy = new proxyEntidades(_empresaSelec);
                    ddlClientes.DataSource = proxy.GetCustomerList(Session["vendorCode"].ToString());
                    ddlClientes.DataTextField = "nombre";
                    ddlClientes.DataValueField = "codigo";
                    ddlClientes.DataBind();
                    RadComboBoxItem newItem = new RadComboBoxItem("");
                    ddlClientes.Items.Add(newItem);

                    if (_clienteSelec == "")
                        ddlClientes.SelectedIndex = ddlClientes.Items.Count - 1;
                    else
                    {
                        bool itemFound = false;

                        for (Int32 iItem = 0; (iItem < ddlClientes.Items.Count) && !itemFound; iItem++)
                        {
                            if (ddlClientes.Items[iItem].Value == _clienteSelec)
                            {
                                ddlClientes.SelectedIndex = iItem;
                                itemFound = true;
                            }
                        }
                    }

                    
                }
                catch (Exception ex)
                {
                    MostrarMensaje(ex.Message);
                }
            }

            protected void ddlClientes_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
            {
                Session["customerCode"] = ddlClientes.SelectedItem.Value;
                Session["customerName"] = ddlClientes.SelectedItem.Text;
                btNuevoPedido.Visible = (ddlClientes.SelectedItem.Value != "");
                btBuscarAlbaranes.Visible = (ddlClientes.SelectedItem.Value != "");
                panelSeleccionPedido.Visible = false;
                panelPedidoSeleccionado.Visible = false;
                panelLineasPedido.Visible = false;
                panelSeleccionAlbaran.Visible = false;
                panelLineasAlbaran.Visible = false;
            }

        #endregion

        #region PEDIDOS

            protected void lkbtDatos_Click(object sender, EventArgs e)
            {
                CargarPedidosProveedor(ddlLavanderias.SelectedItem.Value, Session["vendorCode"].ToString(), ddlClientes.SelectedItem.Value);

                panelSeleccionPedido.Visible = true;
                panelPedidoSeleccionado.Visible = false;
                panelLineasPedido.Visible = false;

                panelSeleccionAlbaran.Visible = false;
                panelLineasAlbaran.Visible = false;
            }

            protected void gridPedidosAbiertos_SelectedIndexChanged(object sender, EventArgs e)
            {
                Session["orderNo"] = gridPedidosAbiertos.Items[gridPedidosAbiertos.SelectedIndexes[0]].Cells[3].Text;
                Session["orderDate"] = DateTime.Parse(gridPedidosAbiertos.Items[gridPedidosAbiertos.SelectedIndexes[0]].Cells[4].Text).ToShortDateString();
                Session["postingDate"] = DateTime.Parse(gridPedidosAbiertos.Items[gridPedidosAbiertos.SelectedIndexes[0]].Cells[5].Text).ToShortDateString();
                
                CargarDatosPedido();
                panelLineasPedido.Visible = true;
            }

            protected void gridPedidosAbiertos_RowDataBound(object sender, GridViewRowEventArgs e)
            {
                panelSeleccionPedido.Visible = true;
                panelPedidoSeleccionado.Visible = false;
                gridPedidosAbiertos.SelectedIndexes.Clear();
            }

            protected void btNuevoPedido_Click(object sender, EventArgs e)
            {
                SolicitarFechaRegistro();
                //CrearNuevoPedido();
            }

            protected void btEliminarPedido_Click(object sender, EventArgs e)
            {
                EliminarPedido();
            }

            protected void btConfirmarPedido_Click(object sender, EventArgs e)
            {
                string pedido = lbNumPedido.Text;
                if (ConfirmarPedido(pedido))
                    CargarPedidosProveedor(ddlLavanderias.SelectedItem.Value,
                        Session["vendorCode"].ToString(), ddlClientes.SelectedItem.Value);
            }

            protected void gridLineasPedido_DeleteCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
            {
                string pedido = lbNumPedido.Text;
                int linea = int.Parse(e.Item.Cells[3].Text);
                EliminarLineaPedido(pedido, linea);
                CargarDatosPedido();
            }

            protected void btNuevaLinea_Click(object sender, EventArgs e)
            {
                panelSeleccionSurtido.Visible = true;
            }

            protected void btAceptar_Click(object sender, EventArgs e)
            {
                InsertarLineaPedido();
                InicializarSeleccionSurtido();
                CargarDatosPedido();
            }

            protected void btCancelar_Click(object sender, EventArgs e)
            {
                InicializarSeleccionSurtido();
                panelSeleccionSurtido.Visible = false;
            }

            private void CargarPedidosProveedor(string empresa, string proveedor, string cliente)
            {
                try
                {
                    proxyEntidades proxy = new proxyEntidades(empresa);
                    if (cliente == "")
                        gridPedidosAbiertos.DataSource = proxy.GetOpenPurchaseOrder(proveedor);
                    else
                        gridPedidosAbiertos.DataSource = proxy.GetOpenPurchaseOrder(proveedor, cliente);

                    gridPedidosAbiertos.DataBind();
                    // se elimina cualquier contenido anterior del datagrid de líneas
                    gridLineasPedido.DataSource = null;
                    gridLineasPedido.DataBind();
                    panelLineasPedido.Visible = false;
                    panelPedidoSeleccionado.Visible = false;
                }
                catch (Exception ex)
                {
                    MostrarMensaje(ex.Message);
                }

            }

            private void CargarDatosPedido()
            {
                string orderNumber = "";
                string customerCode = "";
                string customerName = "";
                string orderDate = "";
                string postingDate = "";

                try
                {
                    //orderNumber = gridPedidosAbiertos.Items[gridPedidosAbiertos.SelectedIndexes[0]].Cells[3].Text;
                    //customerCode = gridPedidosAbiertos.Items[gridPedidosAbiertos.SelectedIndexes[0]].Cells[6].Text;
                    //customerName = gridPedidosAbiertos.Items[gridPedidosAbiertos.SelectedIndexes[0]].Cells[7].Text;
                    //orderDate = DateTime.Parse(gridPedidosAbiertos.Items[gridPedidosAbiertos.SelectedIndexes[0]].Cells[4].Text).ToShortDateString();
                    //postingDate = DateTime.Parse(gridPedidosAbiertos.Items[gridPedidosAbiertos.SelectedIndexes[0]].Cells[5].Text).ToShortDateString();

                    orderNumber = Session["orderNo"].ToString();
                    customerCode = Session["customerCode"].ToString();
                    customerName = Session["customerName"].ToString();
                    orderDate = Session["orderDate"].ToString();
                    postingDate=Session["postingDate"].ToString();

                    CargarSurtidoCliente(customerCode, customerName);

                    proxyEntidades proxy = new proxyEntidades(ddlLavanderias.SelectedItem.Value);
                    DataTable dtLineasPedido = proxy.GetPurchOrderLines(orderNumber);

                    ActualizarCantidadesSurtido(dtLineasPedido);

                    gridLineasPedido.DataSource = dtLineasPedido;
                    gridLineasPedido.DataBind();
                    
                    btConfirmarPedido.Enabled=(gridLineasPedido.Items.Count>0);

                    lbNumPedido.Text = orderNumber;
                    hfCodCliente.Value = customerCode;
                    lbCteFinal.Text = customerName;
                    lbFechaPedido.Text = orderDate;
                    lbFechaRegistro.Text = postingDate;
                    
                    panelSeleccionPedido.Visible = false;
                    panelPedidoSeleccionado.Visible = true;
                }
                catch (Exception ex)
                {
                    MostrarMensaje(ex.Message);
                }
            }

            private void ActualizarCantidadesSurtido(DataTable _lineasPedido)
            {
                surtidoCliente surtido = (surtidoCliente)Session["surtidoCliente"];

                foreach (DataRow lineaPedido in _lineasPedido.Rows)
                {
                    surtido.SetItemQty(lineaPedido["PRODUCTO"].ToString(), decimal.Parse(lineaPedido["CANTIDAD"].ToString()));
                }
            }

            private void SolicitarFechaRegistro()
            {
                Calendario.SelectedDate = DateTime.Today;
                Calendario.RangeMaxDate = DateTime.Today;
                panelCalendar.Visible = true;
            }

            private void CrearNuevoPedido()
            {
                try
                {                    
                    proxyEntidades proxy = new proxyEntidades(ddlLavanderias.SelectedItem.Value);
                    string numero = proxy.CreatePurchOrder(Session["vendorCode"].ToString(), ddlClientes.SelectedItem.Value,hfFechaRegistro.Value);
                    CargarPedidosProveedor(ddlLavanderias.SelectedItem.Value, Session["vendorCode"].ToString(), ddlClientes.SelectedItem.Value);

                    gridPedidosAbiertos.SelectedIndexes.Clear();
                    gridPedidosAbiertos.SelectedIndexes.Add(gridPedidosAbiertos.Items.Count - 1);

                    Session["orderNo"] = gridPedidosAbiertos.Items[gridPedidosAbiertos.SelectedIndexes[0]].Cells[3].Text;
                    Session["orderDate"] = DateTime.Parse(gridPedidosAbiertos.Items[gridPedidosAbiertos.SelectedIndexes[0]].Cells[4].Text).ToShortDateString();
                    Session["postingDate"] = DateTime.Parse(gridPedidosAbiertos.Items[gridPedidosAbiertos.SelectedIndexes[0]].Cells[5].Text).ToShortDateString();

                    CargarDatosPedido();

                    panelSeleccionPedido.Visible = false;
                    panelPedidoSeleccionado.Visible = true;
                    panelLineasPedido.Visible = true;

                    panelSeleccionAlbaran.Visible = false;
                    panelLineasAlbaran.Visible = false;
                }
                catch (Exception ex)
                {
                    MostrarMensaje(ex.Message);
                }
            }

            private void EliminarPedido()
            {
                string pedido = "";

                try
                {
                    pedido = gridPedidosAbiertos.Items[gridPedidosAbiertos.SelectedIndexes[0]].Cells[3].Text;
                    proxyEntidades proxy = new proxyEntidades(ddlLavanderias.SelectedItem.Value);
                    proxy.DeletePurchOrder(pedido);

                    CargarPedidosProveedor(ddlLavanderias.SelectedItem.Value, Session["vendorCode"].ToString(), ddlClientes.SelectedItem.Value);

                    panelSeleccionPedido.Visible = true;
                    panelPedidoSeleccionado.Visible = false;
                    panelLineasPedido.Visible = false;

                    panelSeleccionAlbaran.Visible = false;
                    panelLineasAlbaran.Visible = false;
                }
                catch (Exception ex)
                {
                    MostrarMensaje(ex.Message);
                }
            }

            private bool ConfirmarPedido(string pedido)
            {
                bool res = false;

                try
                {
                    proxyEntidades proxy = new proxyEntidades(ddlLavanderias.SelectedItem.Value);
                    if (!proxy.ExistKgLine(pedido))
                        throw new Exception("No se puede confirmar el pedido sin indicar los Kilos procesados");
                    Session["albaran"]= proxy.LaunchPurchOrder(pedido);
                    ImprimirAlbaran();
                    res = true;
                }
                catch (Exception ex)
                {
                    MostrarMensaje(ex.Message);
                    res = false;
                }

                return (res);
            }

            private void InsertarLineaPedido()
            {
                //try
                //{
                //    proxyEntidades proxy = new proxyEntidades(ddlLavanderias.SelectedItem.Value);
                //    proxy.CreatePurchOrderLine(
                //        lbNumPedido.Text,
                //        Session["vendorCode"].ToString(),
                //        ddlProductos.SelectedItem.Value,
                //        decimal.Parse(txCantidad.Text));
                //    panelSeleccionSurtido.Visible = false;
                //}
                //catch (Exception ex)
                //{
                //    MostrarMensaje(ex.Message);
                //}
            }

            private void EliminarLineaPedido(string pedido, int linea)
            {
                try
                {
                    proxyEntidades proxy = new proxyEntidades(ddlLavanderias.SelectedItem.Value);
                    proxy.DeletePurchOrderLine(pedido, linea);
                }
                catch (Exception ex)
                {
                    MostrarMensaje(ex.Message);
                }
            }

        #endregion

        #region ALBARANES

            protected void btBuscarAlbaranes_Click(object sender, EventArgs e)
            {
                CargarAlbaranesCliente(ddlLavanderias.SelectedItem.Value, ddlClientes.SelectedItem.Value);
                panelSeleccionAlbaran.Visible = true;
                panelSeleccionPedido.Visible = false;
                panelPedidoSeleccionado.Visible = false;
            }

            protected void gridAlbaranes_DeleteCommand(object source, GridCommandEventArgs e)
            {
                string albaran = e.Item.Cells[4].Text;
                DeshacerAlbaran(albaran);
                CargarAlbaranesCliente(ddlLavanderias.SelectedItem.Value, ddlClientes.SelectedItem.Value);
            }

            private void DeshacerAlbaran(string numero)
            {
                try
                {
                    proxyEntidades proxy = new proxyEntidades(ddlLavanderias.SelectedItem.Value);
                    proxy.UndoSalesShipment(numero);
                }
                catch (Exception ex)
                {
                    MostrarMensaje(ex.Message);
                }
            }

            protected void gridAlbaranes_SelectedIndexChanged(object sender, EventArgs e)
            {
                string albaran = gridAlbaranes.Items[gridAlbaranes.SelectedIndexes[0]].Cells[4].Text;
                CargarLineasAlbaran(albaran);
                lbNumAlbaran.Text = albaran;
                Session["albaran"] = albaran;
                panelLineasAlbaran.Visible = true;
            }

            private void CargarAlbaranesCliente(string empresa, string cliente)
            {
                try
                {
                    proxyEntidades proxy = new proxyEntidades(empresa);

                    gridAlbaranes.DataSource = proxy.GetSalesShipments(cliente);

                    gridAlbaranes.DataBind();
                    // se elimina cualquier contenido anterior del datagrid de líneas
                    gridLineasPedido.DataSource = null;
                    gridLineasPedido.DataBind();
                    panelLineasPedido.Visible = false;
                }
                catch (Exception ex)
                {
                    MostrarMensaje(ex.Message);
                }

            }

            private void CargarLineasAlbaran(string albaran)
            {
                proxyEntidades proxy = new proxyEntidades(ddlLavanderias.SelectedItem.Value);
                gridLineasAlbaran.DataSource = proxy.GetSalesShptLines(albaran);
                gridLineasAlbaran.DataBind();
            }

        #endregion

        #region SURTIDO

            protected void ddlFamilias_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
            {
                surtidoCliente surtido = (surtidoCliente)Session["surtidoCliente"];
                ddlSubfamilias.DataSource = surtido.GetSubfamilias(ddlFamilias.SelectedItem.Value);
                ddlSubfamilias.DataValueField = "codigo";
                ddlSubfamilias.DataTextField = "descripcion";
                ddlSubfamilias.DataBind();
                RadComboBoxItem newItem = new RadComboBoxItem("");
                ddlSubfamilias.Items.Add(newItem);
                ddlSubfamilias.SelectedIndex = ddlSubfamilias.Items.Count - 1;
            }

            protected void ddlSubfamilias_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
            {
                //surtidoCliente surtido = (surtidoCliente)Session["surtidoCliente"];
                //ddlProductos.DataSource = surtido.GetProductos(ddlFamilias.SelectedItem.Value, ddlSubfamilias.SelectedItem.Value);
                //ddlProductos.DataValueField = "codigo";
                //ddlProductos.DataTextField = "descripcion";
                //ddlProductos.DataBind();
                //RadComboBoxItem newItem = new RadComboBoxItem("");
                //ddlProductos.Items.Add(newItem);
                //ddlProductos.SelectedIndex = ddlProductos.Items.Count - 1;
            }           

            private void CargarSurtidoCliente(string customerCode, string customerName)
            {
                bool cargar = false;
                surtidoCliente surtido = null;

                try
                {
                    surtido = (surtidoCliente)Session["surtidoCliente"];
                    if (surtido.CodCliente != customerCode)
                        cargar = true;
                    else
                        surtido.ResetItemQtys();
                }
                catch
                {
                    cargar = true;
                }

                if (cargar)
                {
                    surtido = new surtidoCliente();
                    proxyEntidades proxy = new proxyEntidades(ddlLavanderias.SelectedItem.Value);

                    surtido.CodCliente = customerCode;
                    surtido.NomCliente = customerName;
                    surtido.TablaSurtido = proxy.GetCustCrossRefs(customerCode);
                    Session["surtidoCliente"] = surtido;
                }               

                ddlFamilias.DataSource = surtido.GetFamilias();
                ddlFamilias.DataValueField = "codigo";
                ddlFamilias.DataTextField = "descripcion";
                ddlFamilias.DataBind();
                RadComboBoxItem newItem = new RadComboBoxItem("");
                ddlFamilias.Items.Add(newItem);
                ddlFamilias.SelectedIndex = ddlFamilias.Items.Count - 1;
            }

            private void InicializarSeleccionSurtido()
            {
                ddlFamilias.SelectedIndex = ddlFamilias.Items.Count - 1;
                ddlSubfamilias.SelectedIndex = ddlSubfamilias.Items.Count - 1;
                //ddlProductos.SelectedIndex = ddlProductos.Items.Count - 1;
                //txCantidad.Text = null;
            }

            
        
        #endregion

        #region SCRIPTS

            private void MostrarMensaje(string mensaje)
            {
                string key = "message";
                string javascript = "MessageBox('" + mensaje + "');";

                if (!Page.ClientScript.IsStartupScriptRegistered(key))
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), key, javascript, true);
                }
            }

            private void MostrarStatus(string mensaje)
            {
                string key = "status";
                string javascript = "StatusMsj('" + mensaje + "');";

                if (!Page.ClientScript.IsStartupScriptRegistered(key))
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), key, javascript, true);
                }
            }

            private void EjecutarScript(string argumentos)
            {
                string key = "status";
                string vbscript = "EjecutarApp('" + argumentos + "');";

                if (!Page.ClientScript.IsStartupScriptRegistered(key))
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), key, vbscript, true);
                }
            }
            
            private void ImprimirAlbaran()
            {
                string key = "status";
                string vbscript = "ventanaSecundaria('" + "Albaran.aspx" + "');";

                if (!Page.ClientScript.IsStartupScriptRegistered(key))
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), key, vbscript, true);
                }
            }

            

        #endregion

            protected void btDesconectar_Click(object sender, EventArgs e)
            {
                Session["vendorCode"] = null;
                Session["vendorName"] = null;
                Session["lavanderia"] = null;
                Session["customerCode"] = null;
                Session["orderNo"] = null;

                Response.Redirect("Login.aspx");
            }

            protected void Calendario_SelectionChanged(object sender, Telerik.Web.UI.Calendar.SelectedDatesEventArgs e)
            {
                hfFechaRegistro.Value = Calendario.SelectedDate.ToShortDateString();
                panelCalendar.Visible = false;
                CrearNuevoPedido();
            }

            protected void btSurtidoCte_Click(object sender, EventArgs e)
            {
                Response.Redirect("surtidoTLK.aspx");
            }

            
                    
    }
}
