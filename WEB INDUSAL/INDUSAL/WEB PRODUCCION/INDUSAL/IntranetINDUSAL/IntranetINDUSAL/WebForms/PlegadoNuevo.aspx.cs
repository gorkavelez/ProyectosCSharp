using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IntranetINDUSAL.Negocio;
using System.Text;
using IntranetINDUSAL.Controles_Personalizados;
using IntranetINDUSAL.Reports;

namespace IntranetINDUSAL.WebForms
{
    public partial class PlegadoNuevo : System.Web.UI.Page
    {
        private cPlegadoNuevo oPlegado;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.INIKER_teclado.OKClick +=
                   new INIKER_tecladoNumerico.OKClickHandler(INIKER_teclado_OKClick);

            this.INIKER_surtidoCliente.OKClick +=
                   new INIKER_surtido.OKClickHandler(INIKER_surtido_OKClick);

            txCodCliente.Attributes.Add("onfocus", "FocusScript('" + txCodCliente.ClientID + "')");
            txNumPedido.Attributes.Add("onfocus", "FocusScript('" + txNumPedido.ClientID + "')");
            txCodOperario.Attributes.Add("onfocus", "FocusScript('" + txCodOperario.ClientID + "')");
            txPaq.Attributes.Add("onfocus", "FocusScriptWithAction('" + txPaq.ClientID + "','paq','PAQUETES')");
            txUdsTotal.Attributes.Add("onfocus", "ImprimirEtiquetas('" + mostrarPopUp.ClientID + "')");

            if (!IsPostBack)
            {
                GenerarNuevoPlegado();
                //GetClientes();
                MostrarCalandras();
                //MostrarEmpleados();
                MostrarDatosClase();
                // posicionamiento en la primera carga de la página
                SetKeyBData("cliente", "CLIENTE");
                txCodCliente.Focus();
            }
            else
            {
                RecuperarVariableSesion();                

                string accion = Request.Form["__EVENTTARGET"].ToString();
                string titulo = Request.Form["__EVENTARGUMENT"].ToString();
                if (accion != "")
                {
                    SetKeyBData(accion, titulo);
                }
            }            

            WS_CreateButtons();

            MostrarTitulo();            

        }

        #region EVENTOS CONTROLES
        
            protected void INIKER_teclado_OKClick(object sender, INIKER_tecladoNumerico.OKClickEventArgs e)
            {
                switch (oPlegado.DatoTeclado)
                {
                    case "cliente":
                        SeleccionarCliente(e.Valor);
                        break;
                    case "operario":
                        SeleccionarOperario(e.Valor);
                        break;
                    case "addPaq":
                        oPlegado.Paquetes = int.Parse(e.Valor);                        
                        txUdsTotal.Focus();                                                
                        break;
                    case "paq":
                        oPlegado.Paquetes = int.Parse(e.Valor);                        
                        break;
                    case "uds":
                        oPlegado.Unidades = int.Parse(e.Valor);
                        break;
                    case "costura":
                        Rechazar(int.Parse(e.Valor));
                        break;
                    case "oxido":
                        Rechazar(int.Parse(e.Valor));
                        break;
                    case "etiqueta":
                        ImprimirEtiquetas(int.Parse(e.Valor));
                        break;                    
                }

                panelTeclado.Enabled = false;
                MostrarDatosClase();

            }

            protected void INIKER_surtido_OKClick(object sender, INIKER_surtido.OKClickEventArgs e)
            {
                try
                {
                    oPlegado.CodProducto = e.Codigo;
                    oPlegado.DescProducto = e.Descripcion;
                    oPlegado.UdsPorPaquete = ObtenerCantPorPaquete(e.Codigo);
                    txUdsRegistradas.Text = ObtenerUdsContadas().ToString();

                    MostrarDatosClase();
                    SetKeyBData("uds", "UNIDADES");

                    panelSurtido.Visible = false;
                    panelRegistro.Visible = true;
                }
                catch (Exception ex)
                {
                    MostrarMensaje(ex.Message);
                }
            }
            
            protected void ddlMaquinas_SelectedIndexChanged(object sender, EventArgs e)
            {                
                oPlegado.CodMaquina = ddlMaquinas.SelectedItem.Value;
                oPlegado.DescMaquina = ddlMaquinas.SelectedItem.Text;
                MostrarDatosClase();
            }

            protected void btDato_Click(object sender, EventArgs e)
            {
                Button oSender = (Button)sender;
                // se captura el nombre del emisor
                //hdDatoTeclado.Value = oSender.CommandName;
                // se parametriza el control teclado
                SetKeyBData(oSender.CommandName, oSender.Text);

            }

        #endregion

        #region ACCIONES

            protected void btVolver_Click(object sender, EventArgs e)
            {
                Button oSender=(Button) sender;

                oPlegado.Clear();
                MostrarDatosClase();

                switch (oSender.CommandName)
                {
                    case "surtido":                        
                        panelRegistro.Visible = false;
                        panelDatosSeleccionados.Visible = true;
                        panelSurtido.Visible = true;
                        break;
                    case "inicio":
                        panelDatosSeleccionados.Visible = false;
                        panelSurtido.Visible = false;
                        txCodCliente.Focus();
                        panelSeleccion.Visible = true;
                        break;
                }
                
            }

            protected void btRegistrar_Click(object sender, EventArgs e)
            {
                try
                {
                    int nEtiqs = oPlegado.NEtiquetas;
                    int udsRegistro = oPlegado.UnidadesTotal;
                    oPlegado.Register();
                    txUdsRegistradas.Text = (int.Parse(txUdsRegistradas.Text) + udsRegistro).ToString();
                    ImprimirEtiquetas(nEtiqs);
                }
                catch (Exception ex)
                {
                    MostrarMensaje(ex.Message.Replace("'", ""));
                }

                MostrarDatosClase();                
            }

            protected void btCancelar_Click(object sender, EventArgs e)
            {
                oPlegado.ClearAll();
                MostrarDatosClase();
                txCodCliente.Focus();
                panelSeleccion.Visible = true;
                panelRegistro.Visible = false;
            }

            protected void btEtiqueta_Click(object sender, EventArgs e)
            {
                // TODO: Imprime una etiqueta de paquete del producto seleccionado, sin añadir el paquete al conteo
            }

        #endregion 

        #region METODOS

            private void ActivarTeclado(string dato, string titulo)
            {          
                switch (dato)
                {
                    case "addPaq":                        
                        INIKER_teclado.TituloDato = titulo;
                        INIKER_teclado.Dato = oPlegado.Paquetes.ToString();
                        panelTeclado.Enabled = true;
                        break;
                    case "paq":
                        INIKER_teclado.TituloDato = titulo;
                        INIKER_teclado.Dato = oPlegado.Paquetes.ToString();
                        panelTeclado.Enabled = true;
                        break;
                    case "uds":
                        INIKER_teclado.TituloDato = titulo;
                        INIKER_teclado.Dato = oPlegado.Unidades.ToString();
                        panelTeclado.Enabled = true;
                        break;
                    case "costura":
                        INIKER_teclado.TituloDato = titulo;
                        panelTeclado.Enabled = true;
                        break;
                    case "oxido":
                        INIKER_teclado.TituloDato = titulo;
                        panelTeclado.Enabled = true;
                        break;
                    case "etiqueta":
                        INIKER_teclado.TituloDato = titulo;
                        panelTeclado.Enabled = true;
                        break;
                    case "etiquetaCliente":                        
                        ImprimirEtiquetaCliente();
                        break;
                    case "cliente":
                        INIKER_teclado.TituloDato = titulo;
                        INIKER_teclado.Dato = oPlegado.CodCliente;
                        panelTeclado.Enabled = true;
                        break;
                    case "operario":
                        INIKER_teclado.TituloDato = titulo;
                        INIKER_teclado.Dato = oPlegado.CodOperario;
                        panelTeclado.Enabled = true;
                        break;
                }
            }

            private void GenerarNuevoPlegado()
            {           
                string tipo = Request.QueryString["Tipo"];               

                oPlegado =null;
                oPlegado = new cPlegadoNuevo(Session["empresaLogin"].ToString(), int.Parse(tipo));
                tipoPlegado.Value = oPlegado.TipoPlanchado.ToString().ToLower();
                GuardarVariableSesion();
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

            private int ObtenerUdsContadas()
            {
                cProduccion oProduccion = new cProduccion(Session["empresaLogin"].ToString());
                return (oProduccion.GetUdsContadasPlegado(oPlegado.TipoPlanchado.ToString(),
                    oPlegado.CodCliente, oPlegado.NumPedido, oPlegado.CodProducto));
            }

            private void MostrarDatosClase()
            {
                GuardarVariableSesion();
                panelTeclado.Enabled = false;

                // si el objeto está instanciado, se muestran los datos que tenga
                if (oPlegado != null)
                {
                    // Panel de selección de datos

                    // datos de operario
                    lbTurnoSel.Text = oPlegado.DesTurno;

                    txCodOperario.Text = oPlegado.CodOperario;
                    lbNomOperario.Text = oPlegado.NomOperario;
                    //ddlEmpleados.SelectedIndex = ddlEmpleados.Items.IndexOf(ddlEmpleados.Items.FindByValue(oPlegado.CodOperario));
                    // datos de máquina
                    ddlMaquinas.SelectedIndex=ddlMaquinas.Items.IndexOf(ddlMaquinas.Items.FindByValue(oPlegado.CodMaquina));
                    //datos de cliente
                    txCodCliente.Text = oPlegado.CodCliente;
                    lbNomCliente.Text = oPlegado.NomCliente;
                    //ddlClientes.SelectedIndex = ddlClientes.Items.IndexOf(ddlClientes.Items.FindByValue(oPlegado.CodCliente));

                    txNumPedido.Text = oPlegado.NumPedido;
                    ddlPedidos.SelectedIndex = ddlPedidos.Items.IndexOf(ddlPedidos.Items.FindByValue(oPlegado.NumPedido));

                    ActivarPanelResumen();

                    // Panel de registro
                    lbClienteSel.Text = oPlegado.NomCliente;
                    lbpedidoSel.Text = oPlegado.NumPedido;
                    lbMaquinaSel.Text = oPlegado.DescMaquina;
                    lbProductoSel.Text = oPlegado.DescProducto;
                    lbCantPaq.Text = oPlegado.UdsPorPaquete.ToString();
                    txPaq.Text = oPlegado.Paquetes.ToString();
                    txUnidades.Text = oPlegado.Unidades.ToString();
                    txUdsTotal.Text = oPlegado.UnidadesTotal.ToString();

                }
            }

            private void Rechazar(int cantidad)
            {
                try
                {
                    cRechazoDesdePlegado oRechazo = new cRechazoDesdePlegado(oPlegado.Empresa);
                    oRechazo.Cliente = oPlegado.CodCliente;
                    oRechazo.Pedido = oPlegado.NumPedido;
                    oRechazo.Producto = oPlegado.CodProducto;
                    oRechazo.NSerie = oPlegado.NSerie;
                    oRechazo.Operario = oPlegado.CodOperario;
                    oRechazo.Turno = oPlegado.CodTurno;
                    oRechazo.Cantidad = cantidad;
                    switch (oPlegado.DatoTeclado)
                    {
                        case "costura":
                            oRechazo.ACostura();
                            break;
                        case "oxido":
                            oRechazo.AOxido();
                            break;
                    }
                }
                catch (Exception ex)
                {
                    MostrarMensaje(ex.Message.Replace("'", ""));
                }
            }

            private void EscribirBarraEstado(string mensaje)
            {
                string key = "status";
                string javascript = "StatusMsj('" + mensaje + "');";

                if (!Page.ClientScript.IsStartupScriptRegistered(key))
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), key, javascript, true);
                }
            }

            private void ImprimirEtiquetas(int nEtiqs)
            {
                if (nEtiqs != 0)
                {
                    cPrintDocument oPrinter = new cPrintDocument(Session["empresaLogin"].ToString());

                    oPrinter.TipoDocumento = cPrintDocument.eTipoDocumento.paqueteProducto;
                    oPrinter.CodCliente = oPlegado.CodCliente;
                    oPrinter.NomCliente = oPlegado.NomCliente;
                    oPrinter.NumPedido = oPlegado.NumPedido;
                    oPrinter.CodProducto = oPlegado.CodProducto;
                    oPrinter.NomProducto = oPlegado.DescProducto;

                    oPrinter.Print(nEtiqs);
                    EjecutarScript(oPrinter.ArgumentString);
                }
            }

            private void ImprimirEtiquetaCliente()
            {
                
                cPrintDocument oPrinter = new cPrintDocument(Session["empresaLogin"].ToString());

                oPrinter.TipoDocumento = cPrintDocument.eTipoDocumento.carroTransporte;
                oPrinter.CodCliente = oPlegado.CodCliente;
                oPrinter.NomCliente = oPlegado.NomCliente;          
                
                oPrinter.Print(1);
                EjecutarScript(oPrinter.ArgumentString);
                
            }

            private void SetKeyBData(string commandName, string keybTitle)
            {
                oPlegado.DatoTeclado = commandName;
                GuardarVariableSesion();
                ActivarTeclado(commandName, keybTitle);
            }

            private void ActivarPanelResumen()
            {
                panelDatosSeleccionados.Visible =
                    (oPlegado.CodCliente != "") &&
                    ((oPlegado.CodCliente == "000000") || (oPlegado.CodCliente == "100000") ? true : oPlegado.NumPedido != "") &&
                    (oPlegado.CodTurno != "") && (oPlegado.CodOperario != "") &&
                    ((oPlegado.TipoPlanchado != INIKER.CrossReferences.Tipo_Planchado.Calandra) ? true : oPlegado.CodMaquina != "");
                
                panelSeleccion.Visible = !panelDatosSeleccionados.Visible;
                panelSurtido.Visible = (panelDatosSeleccionados.Visible)&&(!panelRegistro.Visible);
            }
  
        #endregion

        #region TURNOS

            protected void btTurnos_Click(object sender, EventArgs e)
            {
                panelTurnos.Visible = true;
            }

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
                panelTurnos.Visible = false;
                SetKeyBData("operario", "OPERARIO");
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
 
        #region CLIENTES

            protected void btCliente_Click(object sender, EventArgs e)
            {
                Button oButton = (Button)sender;
                SetKeyBData(oButton.CommandName, oButton.Text);
            }

            protected void txCodCliente_TextChanged(object sender, EventArgs e)
            {
                txCodCliente.Text = txCodCliente.Text.Replace("*", "");
                SeleccionarCliente(txCodCliente.Text);
            }

            //protected void GetClientes()
            //{
            //    cClientes oClientes = new cClientes(Session["empresaLogin"].ToString());
            //    oClientes.LoadDropDownList(ref ddlClientes);
            //}

            //protected void ObtenerNombreCliente(string cliente)
            //{
            //    cClientes oClientes = new cClientes(Session["empresaLogin"].ToString());

            //    oClientes.Get(cliente);
            //    oPlegado.CodCliente = oClientes.Codigo;
            //    oPlegado.NomCliente = oClientes.Alias;
            //    //oPlegado.CodAlmacen = oClientes.Almacen;
            //    oClientes.SelectDropDownList(ref ddlClientes);
            //}

            protected void ddlClientes_SelectedIndexChanged(object sender, EventArgs e)
            {
                //SeleccionarCliente(ddlClientes.SelectedItem.Value);
            }

            private void SeleccionarCliente(string codigo)
            {
                try
                {
                    // se rellena el código de cliente con ceros hasta completar el tamaño
                    if (codigo.Length < 6)
                        codigo = codigo.PadLeft(6, '0');

                    GenerarNuevoPlegado();
                    cProduccion oProduccion = new cProduccion(Session["empresaLogin"].ToString());
                    string[] data = oProduccion.GetCustomerData(codigo).Split(';'); ;
                    if (data[0] == "")
                        throw new Exception("No existe el cliente " + codigo);
                    oPlegado.CodCliente = codigo;
                    oPlegado.NomCliente = data[0];

                    InicializarPedidosCliente();
                    MostrarSurtidoCliente(codigo);

                    MostrarDatosClase();

                    if((oPlegado.CodCliente=="000000")||(oPlegado.CodCliente=="100000"))
                        panelTurnos.Visible=true;
                    else
                        txNumPedido.Focus();

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

            private void InicializarPedidosCliente()
            {
                ddlPedidos.Items.Clear();
                ddlPedidos.Visible = false;
            }

        #endregion        

        #region SURTIDO

            private void MostrarSurtidoCliente(string cliente)
            {
                INIKER_surtidoCliente.EmpresaLogin = oPlegado.Empresa;
                INIKER_surtidoCliente.CodCliente = cliente;
                INIKER_surtidoCliente.TipoPlanchado = oPlegado.TipoPlanchado;
                INIKER_surtidoCliente.DesFamilia = "";
                INIKER_surtidoCliente.DesSubfamilia = "";
                INIKER_surtidoCliente.Nivel = 2;
                INIKER_surtidoCliente.Load();

            }

        #endregion

        #region PEDIDOS VENTA

            protected void btPedidos_Click(object sender, EventArgs e)
            {
                if (oPlegado.CodCliente != "")
                {
                    MostrarPedidos(oPlegado.CodCliente);
                    // se comprueba que se ha recuperado al menos un pedido
                    // antes de mostrar el desplegable
                    if (ddlPedidos.Items.Count > 1)
                    {
                        ddlPedidos.Visible = true;
                        txNumPedido.Visible = false;
                    }
                }
            }

            private void MostrarPedidos(string cliente)
            {
                cPedidosVenta oPedidos = new cPedidosVenta(Session["empresaLogin"].ToString(), cliente,false);

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
                SeleccionarPedido(oPlegado.CodCliente, txNumPedido.Text);
            }

            protected void ddlPedidos_SelectedIndexChanged(object sender, EventArgs e)
            {
                //SeleccionarPedido(oPlegado.CodCliente, ddlPedidos.SelectedItem.Value);  
                oPlegado.NumPedido = ddlPedidos.SelectedItem.Text;
                MostrarDatosClase();
                ddlPedidos.Visible = false;
                txNumPedido.Visible = true;
                panelTurnos.Visible = true;
            }

            private void SeleccionarPedido(string cliente, string pedido)
            {
                cPedidosVenta oPedidos = new cPedidosVenta(Session["empresaLogin"].ToString(), cliente);

                if (oPedidos.ExistePedido(pedido))
                {
                    oPlegado.NumPedido = pedido.ToUpper();
                    panelTurnos.Visible = true;
                }
                else
                    oPlegado.NumPedido = "";
                
                MostrarDatosClase();
            }

        #endregion

        #region EMPLEADOS

            private void SeleccionarOperario(string codigo)
            {
                try
                {
                    // se rellena el código de empleado con ceros hasta completar el tamaño
                    if (codigo.Length < 6)
                        codigo = codigo.PadLeft(6, '0');

                    cProduccion oProduccion = new cProduccion(Session["empresaLogin"].ToString());
                    string nombre = oProduccion.GetEmployeeName(codigo);
                    if (nombre == "")
                        throw new Exception("No existe el empleado " + codigo);

                    oPlegado.CodOperario = codigo;
                    oPlegado.NomOperario = nombre;
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

        #region SESSION

            private void GuardarVariableSesion()
        {
            switch (tipoPlegado.Value)
            {
                case "calandra":
                    Session["cCalandra"] = oPlegado;
                    break;
                case "felpa":
                    Session["cFelpa"] = oPlegado;
                    break;
                case "forma":
                    Session["cForma"] = oPlegado;
                    break;
            }
        }

            private void RecuperarVariableSesion()
        {
            switch (tipoPlegado.Value)
            {
                case "calandra":
                     oPlegado=(cPlegadoNuevo)Session["cCalandra"];
                    break;
                case "felpa":
                    oPlegado = (cPlegadoNuevo)Session["cFelpa"];                    
                    break;
                case "forma":
                    oPlegado = (cPlegadoNuevo)Session["cForma"];
                    break;
            }
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

            private void EjecutarScript(string argumentos)
            {
                string key = "status";
                string vbscript = "EjecutarApp('" + argumentos + "');";

                if (!Page.ClientScript.IsStartupScriptRegistered(key))
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), key, vbscript, true);
                }
            }

        #endregion

            

    }

}
