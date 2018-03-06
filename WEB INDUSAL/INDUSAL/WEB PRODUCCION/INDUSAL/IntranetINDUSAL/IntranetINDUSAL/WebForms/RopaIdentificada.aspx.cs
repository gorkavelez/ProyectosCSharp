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
    public partial class RopaIdentificada : System.Web.UI.Page
    {
        private cUniformidad oUniformidad;        

        protected void Page_Load(object sender, EventArgs e)
        {
            this.INIKER_teclado.OKClick +=
                   new INIKER_tecladoNumerico.OKClickHandler(INIKER_teclado_OKClick);

            txCodOperario.Attributes.Add("onfocus", "FocusScript('" + txCodOperario.ClientID + "')");
            txCodCliente.Attributes.Add("onfocus", "FocusScript('" + txCodCliente.ClientID + "')");
            txNumPedido.Attributes.Add("onfocus", "FocusScript('" + txNumPedido.ClientID + "')");
            txNSerie.Attributes.Add("onfocus", "FocusScript('" + txNSerie.ClientID + "')");

            if (!IsPostBack)
            {
                GenerarNuevoObjeto();
                GetTurnos();
                // Modificación hecha para introducir el pedido en los movimientos de producto
                // de las lecturas de entrada. El panel está con Visible=False y se cambia a True.
                //panelPedido.Visible = (oUniformidad.TipoLectura == cUniformidad.eTipoLectura.salida);
            }
            else
            {
                RecuperarVariableSesion();
            }

            WS_CreateButtons();
                    
        }

        #region TECLADO NUMERICO

            protected void INIKER_teclado_OKClick(object sender, INIKER_tecladoNumerico.OKClickEventArgs e)
            {
                switch (oUniformidad.DatoTeclado)
                {
                    case "cliente":
                        SeleccionarCliente(e.Valor);
                        break;
                    case "empleado":
                        SeleccionarOperario(e.Valor);                        
                        break;
                }

                panelTeclado.Enabled = false;                
                MostrarDatosClase();                
                
            }

        #endregion

        #region GRID LINEAS

            protected void gridNumerosSerie_RowDeleting(object sender, GridViewDeleteEventArgs e)
            {
                oUniformidad.Lineas.Delete(gridNumerosSerie.Rows[e.RowIndex].Cells[1].Text.ToString());
                MostrarDatosClase();
            }

            protected void gridNumerosSerie_RowDataBound(object sender, GridViewRowEventArgs e)
            {
                CheckBox rowCheck;

                e.Row.Cells[4].Visible = false; 
                e.Row.Cells[5].Visible = false; 

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    // si la línea está marcada como procesada, no se muestra el botón de borrar
                    rowCheck = (CheckBox)e.Row.Cells[4].Controls[0];
                    e.Row.Cells[0].Controls[0].Visible = !rowCheck.Checked;
                    // si la línea está marcada como errónea, se muestra con texto de color rojo
                    rowCheck = (CheckBox)e.Row.Cells[5].Controls[0];
                    if (rowCheck.Checked)
                    {
                        for (int iCell = 1; iCell < e.Row.Cells.Count; iCell++)
                        {
                            e.Row.Cells[iCell].ForeColor = System.Drawing.Color.Red;
                        }
                    }
                }
            }

        #endregion

        #region CLIENTES

            protected void btCliente_Click(object sender, EventArgs e)
            {
                SetKeybCustomer();
            }

            private void SetKeybCustomer()
            {                
                INIKER_teclado.TituloDato = "CLIENTE";
                panelTeclado.Enabled = true;
            }

            protected void txCodCliente_TextChanged(object sender, EventArgs e)
            {
                txCodCliente.Text = txCodCliente.Text.Replace("*", "");
                SeleccionarCliente(txCodCliente.Text);
            }
        
            private void SeleccionarCliente(string codigo)
            {      
                try
                {
                    // se rellena el código de cliente con ceros hasta completar el tamaño
                    if (codigo.Length < 6)
                        codigo = codigo.PadLeft(6, '0');

                    cProduccion oProduccion = new cProduccion(Session["empresaLogin"].ToString());
                    string[] data = oProduccion.GetCustomerData(codigo).Split(';'); ;
                    if (data[0] == "")
                        throw new Exception("No existe el cliente " + codigo);
                                        
                    oUniformidad.CodCliente = codigo;
                    oUniformidad.NomCliente = data[0];

                    // JCA 16/07/12
                    //if (oUniformidad.TipoLectura == cUniformidad.eTipoLectura.entrada)                    
                    //    GetNumerosSerieCliente(codigo);
                    // FIN JCA 16/07/12
                    txNumPedido.Focus();                    

                    MostrarDatosClase();

                }
                catch (IndexOutOfRangeException)
                {
                    MostrarMensaje("No existe el cliente " + codigo);
                    txCodCliente.Focus();
                }
                catch (Exception ex)
                {
                    MostrarMensaje(ex.Message.Replace("'", ""));
                }               
                
            }

            private void GetNumerosSerieCliente(string codigo)
            {
                cProduccion oProduccion = new cProduccion(Session["empresaLogin"].ToString());
                string almacen="";
                if (oUniformidad.TipoLectura == cUniformidad.eTipoLectura.entrada)
                    almacen = codigo;
                else
                    almacen = "INDUSAL";

                oUniformidad.NumerosSerieCliente = oProduccion.GetCustomerSerialNumbers(codigo,almacen);
            }

            private DataTable GetInfoNumeroSerieCliente(string codCliente_, string serialNo_)
            {
                cProduccion oProduccion = new cProduccion(Session["empresaLogin"].ToString());
                string almacen = "";
                if (oUniformidad.TipoLectura == cUniformidad.eTipoLectura.entrada)
                    almacen = codCliente_;
                else
                    almacen = "INDUSAL";

                return(oProduccion.GetCustomerSerialNumberInfo(codCliente_, almacen, serialNo_));
            }
                      
        #endregion        

        #region METODOS

            private void MostrarDatosClase()
            {
                GuardarVariableSesion();

                lbTurnoSel.Text = oUniformidad.DescTurno;

                txCodOperario.Text = oUniformidad.CodEmpleado;
                lbNomOperario.Text = oUniformidad.NomEmpleado;

                txCodCliente.Text = oUniformidad.CodCliente;
                lbNomCliente.Text = oUniformidad.NomCliente;

                txNumPedido.Text = oUniformidad.NumPedido;

                gridNumerosSerie.DataSource = oUniformidad.Lineas.Lecturas;
                gridNumerosSerie.DataBind();

                gridProductos.DataSource = oUniformidad.Lineas.ResumenProductos;
                gridProductos.DataBind();

                //btRegistrar.Enabled=(oUniformidad.Lineas.Lecturas.Rows.Count!=0);
                btRegistrar.Enabled = oUniformidad.AllowRegister();
                btCancelar.Enabled = btRegistrar.Enabled;                
            }

            private void GenerarNuevoObjeto()
            {
                int iTipoLectura = int.Parse(Request.QueryString["Tipo"].ToString());
                oUniformidad = null;
                oUniformidad = new cUniformidad(Session["empresaLogin"].ToString(), iTipoLectura);
                tipoConteo.Value = oUniformidad.TipoLectura.ToString().ToLower();
                GuardarVariableSesion();

                Page.Title = oUniformidad.PageTitle;
            }

            private void ActivarTeclado(string dato, string titulo)
            {
                INIKER_teclado.TituloDato = titulo;
                switch (dato)
                {                    
                    case "cliente":
                        INIKER_teclado.Dato = oUniformidad.CodCliente;
                        break;
                    case "empleado":
                        INIKER_teclado.Dato = oUniformidad.CodEmpleado;
                        break;                    
                }
                panelTeclado.Enabled = true;
            }

            private void SetKeyBData(string commandName, string keybTitle)
            {
                oUniformidad.DatoTeclado = commandName;
                ActivarTeclado(commandName, keybTitle);
            }
        
        #endregion

        #region ACCIONES

            protected void btRegistrar_Click(object sender, EventArgs e)
            {
                cProduccion oProduccion = new cProduccion(Session["empresaLogin"].ToString());
                int iFila;
                DataRow lectura=null;

                try
                {
                    //foreach (DataRow lectura in oUniformidad.Lineas.Lecturas.Rows)
                    for(iFila=0;iFila<oUniformidad.Lineas.Lecturas.Rows.Count;iFila++)
                    {
                        lectura = oUniformidad.Lineas.Lecturas.Rows[iFila];
                        if (lectura.RowState != DataRowState.Deleted)
                        {
                            if (!bool.Parse(lectura["Procesada"].ToString()))
                            {
                                oProduccion.RegistrarMovUniformidad(
                                    oUniformidad.CodEmpleado,
                                    oUniformidad.CodTurno,
                                    oUniformidad.CodCliente,
                                    oUniformidad.NumPedido,
                                    lectura["Cod. Producto"].ToString(),
                                    lectura["Num. Serie"].ToString(),
                                    (oUniformidad.TipoLectura == cUniformidad.eTipoLectura.entrada ? 1 : -1));
                                lectura["Procesada"] = true;
                                lectura.AcceptChanges();
                            }
                        }
                    }
                    oUniformidad.ClearData();
                    btLimpiar.Enabled = false;
                }
                catch (Exception ex)
                {
                    lectura["Error"] = true;
                    lectura.AcceptChanges();
                    MostrarMensaje(ex.Message.Replace("'", ""));
                    oUniformidad.Lineas.ClearProcesadas();
                    btLimpiar.Enabled = true;
                }

                MostrarDatosClase();
            }
        
            protected void btCancelar_Click(object sender, EventArgs e)
            {
                oUniformidad.ClearData();
                oUniformidad.ClearLineas();
                txCodCliente.Focus();
                MostrarDatosClase();
            }

            protected void btLimpiar_Click(object sender, EventArgs e)
            {
                oUniformidad.ClearLineas();
                btLimpiar.Enabled = false;
                MostrarDatosClase();
            }

            protected void btDato_Click(object sender, EventArgs e)
            {
                Button oSender = (Button)sender;
                // se captura el nombre del emisor
                oUniformidad.DatoTeclado = oSender.CommandName;
                ActivarTeclado(oSender.CommandName, oSender.Text);
            }

        #endregion

        #region TURNOS

            private void GetTurnos()
            {
                cProduccion oProduccion = new cProduccion(Session["empresaLogin"].ToString());
                oUniformidad.Turnos = oProduccion.GetTurnos();
            }

            protected void btTurnos_Click(object sender, EventArgs e)
            {
                panelTurnos.Visible = true;
            }

            private void WS_CreateButtons()
            {
                WS_ClearPanel();

                foreach (DataRow turno in oUniformidad.Turnos.Rows)
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
                oUniformidad.CodTurno = buttonSender.CommandName;
                oUniformidad.DescTurno = buttonSender.Text;

                MostrarDatosClase();
                panelTurnos.Visible = false;
                SetKeyBData("empleado", "OPERARIO");
                txCodOperario.Focus();
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

        #region EMPLEADOS

            private void SeleccionarOperario(string codigo)
            {
                try
                {
                    if (codigo.Length < 6)
                        codigo = codigo.PadLeft(6, '0');

                    cProduccion oProduccion = new cProduccion(Session["empresaLogin"].ToString());
                    string nombre = oProduccion.GetEmployeeName(codigo);
                    if (nombre == "")
                        throw new Exception("No existe el empleado " + codigo);

                    oUniformidad.CodEmpleado= codigo;
                    oUniformidad.NomEmpleado = nombre;

                    SetKeyBData("cliente", "CLIENTE");                        
                    txCodCliente.Focus();
                    
                    MostrarDatosClase();
                }
                catch (IndexOutOfRangeException)
                {
                    MostrarMensaje("No existe el empleado " + codigo);
                    txCodOperario.Focus();
                }
                catch (Exception ex)
                {
                    MostrarMensaje(ex.Message.Replace("'", ""));
                }
            }

            protected void txCodOperario_TextChanged(object sender, EventArgs e)
            {
                SeleccionarOperario(txCodOperario.Text.Replace("*", ""));
            }

        #endregion

        #region PEDIDOS VENTA

            protected void btPedidos_Click(object sender, EventArgs e)
            {
                if (oUniformidad.CodCliente != "")
                {
                    MostrarPedidos(oUniformidad.CodCliente);
                    if (ddlPedidos.Items.Count > 1)
                    {
                        ddlPedidos.Visible = true;
                        txNumPedido.Visible = false;
                    }
                    else
                        MostrarMensaje("El cliente no tiene pedidos abiertos");

                }
            }

            private void MostrarPedidos(string cliente)
            {
                cPedidosVenta oPedidos = new cPedidosVenta(Session["empresaLogin"].ToString(), cliente, false);

                ddlPedidos.DataSource = oPedidos.DtPedidos;
                ddlPedidos.DataValueField = oPedidos.DtPedidos.Columns["numero"].ToString();
                ddlPedidos.DataTextField = oPedidos.DtPedidos.Columns["numero"].ToString();
                ddlPedidos.DataBind();
                ddlPedidos.Items.Add("");
                ddlPedidos.SelectedIndex = ddlPedidos.Items.Count - 1;
            }

            protected void txNumPedido_TextChanged(object sender, EventArgs e)
            {
                txNumPedido.Text = txNumPedido.Text.Replace("*", "");
                txNumPedido.Text = txNumPedido.Text.Replace("'", "-");
                SeleccionarPedido(oUniformidad.CodCliente, txNumPedido.Text);
            }

            protected void ddlPedidos_SelectedIndexChanged(object sender, EventArgs e)
            {                
                oUniformidad.NumPedido = ddlPedidos.SelectedItem.Text;
                if (oUniformidad.TipoLectura== cUniformidad.eTipoLectura.salida)
                    GetNumerosSeriePedido(oUniformidad.NumPedido);
                txNSerie.Focus();
                MostrarDatosClase();
                ddlPedidos.Visible = false;
                txNumPedido.Visible = true;
            }

            private void SeleccionarPedido(string cliente, string pedido)
            {
                cPedidosVenta oPedidos = new cPedidosVenta(Session["empresaLogin"].ToString(), cliente);

                if (oPedidos.ExistePedido(pedido))
                {
                    oUniformidad.NumPedido = pedido.ToUpper();
                    if (oUniformidad.TipoLectura == cUniformidad.eTipoLectura.salida)
                        GetNumerosSeriePedido(oUniformidad.NumPedido);
                }
                else
                    oUniformidad.NumPedido = "";

                txNSerie.Focus();
                MostrarDatosClase();
            }

            private void GetNumerosSeriePedido(string numero)
            {
                cProduccion oProduccion = new cProduccion(Session["empresaLogin"].ToString());
                oUniformidad.NumerosSerieCliente = oProduccion.GetCustomerSerialNumbers(numero);
            }

        #endregion
                
        #region SESSION

            private void GuardarVariableSesion()
            {
                Session["cUniformidad"] = oUniformidad;
            }

            private void RecuperarVariableSesion()
            {
                oUniformidad= (cUniformidad)Session["cUniformidad"];
            }

        #endregion

        #region SCRIPTS

            private void MostrarMensaje(string mensaje)
            {
                string key = "status";
                string javascript = "MessageBox('" + mensaje + "');";

                if (!Page.ClientScript.IsStartupScriptRegistered(key))
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), key, javascript, true);
                }
            }
        
        #endregion

        #region NUMEROS DE SERIE

            protected void txNSerie_TextChanged(object sender, EventArgs e)
            {
                txNSerie.Text = txNSerie.Text.Replace("*", "");
                txNSerie.Text = txNSerie.Text.Replace("'", "-");
                RecuperarInfoNSerie(txNSerie.Text);
                MostrarDatosClase();
            }

            private void RecuperarInfoNSerie(string nSerie)
            {
                if ((oUniformidad.NumPedido != "")&&(oUniformidad.TipoLectura == cUniformidad.eTipoLectura.salida))
                {
                    string res=oUniformidad.SearchSerialNumber(nSerie);

                    if (res!="")                
                        MostrarMensaje(res);
                    else
                    {
                        txNSerie.Text = "";
                        txNSerie.Focus();
                    }
                }
                else
                {
                    try
                    {
                        DataTable datos = GetInfoNumeroSerieCliente(oUniformidad.CodCliente, nSerie);
                        if (datos.Rows.Count >= 1)
                        {
                            string res = oUniformidad.AddSerialNumber(datos.Rows[0]["Serial_No"].ToString(),
                                            datos.Rows[0]["Item_No"].ToString(), datos.Rows[0]["Descripcion_Planta"].ToString());
                            if (res != "")
                                MostrarMensaje(res);
                            else
                            {
                                txNSerie.Text = "";
                                txNSerie.Focus();
                            }
                        }
                        else
                        {
                            throw new Exception("Nº de serie no encontrado");
                        }
                    }
                    catch (Exception ex)
                    {
                        MostrarMensaje(ex.Message);
                    }
                }
            }
        
        #endregion
                        
    }
}
