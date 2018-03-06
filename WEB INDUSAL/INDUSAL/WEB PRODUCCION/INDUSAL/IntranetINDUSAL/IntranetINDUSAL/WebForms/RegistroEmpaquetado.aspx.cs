using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IntranetINDUSAL.Negocio;
using IntranetINDUSAL.Controles_Personalizados;
using IntranetINDUSAL.Reports;

namespace IntranetINDUSAL.WebForms
{
    public partial class RegistroEmpaquetado : System.Web.UI.Page
    {    
        private cEmpaquetadoNuevo oEmpaquetado;

        protected void Page_Load(object sender, EventArgs e)
        {
            
            this.INIKER_teclado.OKClick +=
                   new INIKER_tecladoNumerico.OKClickHandler(INIKER_teclado_OKClick);
            btPeso.OnClientClick = "javascript:ConectarBascula('" + hdfPeso.ClientID + "','" + txPeso.ClientID + "');";  
            txCodCliente.Attributes.Add("onfocus", "FocusScript('" + txCodCliente.ClientID + "')");
            txNumPedido.Attributes.Add("onfocus", "FocusScript('" + txNumPedido.ClientID + "')");
            
            if (!IsPostBack)
            {
                string tipo = Request.QueryString["Tipo"].ToString();

                oEmpaquetado = null;
                oEmpaquetado = new cEmpaquetadoNuevo(Session["empresaLogin"].ToString(), SetTipoEmpaquetado(tipo));
                tipoEmpaquetado.Value = oEmpaquetado.Tipo.ToString().ToLower();
                GuardarVariableSesion();

                //GetClientes();
                //MostrarEmpleados();
                MostrarCarros();
            }
            else
            {
                RecuperarVariableSesion();
            }
            
            // generación de los elementos dinámicos de la página
            CreateWorkShiftButtons();
            MostrarBotonesFacturacion();
            ActivarControles();
        }

        #region METODOS

            private cEmpaquetadoNuevo.eTipoEmpaquetado SetTipoEmpaquetado(string queryStr)
            {
                cEmpaquetadoNuevo.eTipoEmpaquetado tipo= cEmpaquetadoNuevo.eTipoEmpaquetado.empaquetado;

                try
                {
                    int iTipo=int.Parse(queryStr);
                    switch (iTipo)
                    {
                        case 0:
                            tipo=cEmpaquetadoNuevo.eTipoEmpaquetado.empaquetado;
                            break;
                        case 1:
                            tipo= cEmpaquetadoNuevo.eTipoEmpaquetado.expedicion;
                            break;
                    }
                    return (tipo);
                }
                catch
                {
                    // en caso de error de conversión de la cadena de QueryString,
                    // se fuerza a tipo de empaquetado normal
                    return(cEmpaquetadoNuevo.eTipoEmpaquetado.empaquetado);
                }
                
            }

            private void TraerCarrosSacas()
            {
                cProductos oProductos = new cProductos();
                INIKER.Item.ItemList[] carros = oProductos.GetWagons(Session["empresaLogin"].ToString());            
                ddlTiposCarro.DataSource = carros;
                ddlTiposCarro.DataTextField = INIKER.Item.ItemList_Fields.Search_Description.ToString();
                ddlTiposCarro.DataValueField = INIKER.Item.ItemList_Fields.No.ToString();
                ddlTiposCarro.DataBind();            
                ddlTiposCarro.Items.Add("");
                ddlTiposCarro.SelectedIndex = ddlTiposCarro.Items.Count - 1;
            }

            private void MostrarDatosClase()
            {
                GuardarVariableSesion();

                //Datos del panel de selección
                txCodCliente.Text = oEmpaquetado.CodCliente;
                lbNomCliente.Text = oEmpaquetado.NomCliente;
                //ddlClientes.SelectedIndex = ddlClientes.Items.IndexOf(ddlClientes.Items.FindByValue(oEmpaquetado.CodCliente));

                txNumPedido.Text = oEmpaquetado.NumPedido;
                ddlPedidos.SelectedIndex = ddlPedidos.Items.IndexOf(ddlPedidos.Items.FindByValue(oEmpaquetado.NumPedido));

                //Datos del panel de resumen de datos seleccionados
                lbTurnoSelRes.Text = oEmpaquetado.DesTurno;
                lbEmpleadoSel.Text = oEmpaquetado.NomEmpleado;
                lbClienteSel.Text = oEmpaquetado.NomCliente;
                lbPedidoSel.Text = oEmpaquetado.NumPedido;
                lbProductoSel.Text = oEmpaquetado.PesajeEnCurso.DesProducto;

                lbTurnoSel.Text = oEmpaquetado.DesTurno;

                txCodOperario.Text = oEmpaquetado.CodEmpleado;
                lbNomOperario.Text = oEmpaquetado.NomEmpleado;
                //ddlEmpleados.SelectedIndex = ddlEmpleados.Items.IndexOf(ddlEmpleados.Items.FindByValue(oEmpaquetado.CodEmpleado));

                ddlTiposCarro.SelectedIndex = ddlTiposCarro.Items.Count - 1;
                ddlTiposCarro.SelectedIndex = ddlTiposCarro.Items.IndexOf(ddlTiposCarro.Items.FindByValue(oEmpaquetado.PesajeEnCurso.CodTipoCarro));

                txNCarro.Text = oEmpaquetado.PesajeEnCurso.NCarro.ToString();
                txPeso.Text = oEmpaquetado.PesajeEnCurso.PesoBrutoCarro.ToString();
                txCompleto.Text = oEmpaquetado.PesajeEnCurso.Completo ? "SI" : "NO";

                grdPesajes.DataSource = oEmpaquetado.PesajeEnCurso.Lineas;
                grdPesajes.DataBind();

                grdCarros.DataSource = oEmpaquetado.CarrosPesaje.DTCarros;
                grdCarros.DataBind();

                ActivarControles();
            }
            
            private Button CreateButton(string pText, string pID, string code, EventHandler oEvHndl, string css, int lngLineaTexto)
            {
                // instancia de objeto
                Button newButton = new Button();
                // establecimiento de propiedades
                newButton.ID = pID;
                //newButton.Text = pText;
                newButton.Text = PrepararTexto(pText, lngLineaTexto, 2);
                newButton.ToolTip = pText;
                newButton.CssClass = css;

                if (code != "")
                    newButton.CommandName = code;

                newButton.Click += new EventHandler(oEvHndl);
                //newButton.Attributes.Add("OnClick", "javascript:return DatosObligatorios();");

                return (newButton);
            }

            private void MostrarCarros()
            {
                ddlTiposCarro.DataSource = oEmpaquetado.Carros;
                ddlTiposCarro.DataValueField = oEmpaquetado.Carros.Columns["codigo"].ToString();
                ddlTiposCarro.DataTextField = oEmpaquetado.Carros.Columns["descripcion"].ToString();
                ddlTiposCarro.DataBind();
                ddlTiposCarro.Items.Add("");
                ddlTiposCarro.SelectedIndex = ddlTiposCarro.Items.Count - 1;
            }
            
            private void ActivarPanelDatosPesaje()
            {
                panelDatosPesaje.Enabled = (oEmpaquetado.PesajeEnCurso.CodProducto != "" &&
                                                oEmpaquetado.PesajeEnCurso.CodTipoCarro != "");

                //if (panelDatosPesaje.Enabled)
                //    SetKeyBData("peso", "PESO");
            }

            private void ActivarControles()
            {
                // Operario
                btOperario.Enabled = (oEmpaquetado.CodTurno != "");
                txCodOperario.Enabled = btOperario.Enabled;
                //ddlEmpleados.Enabled = btOperario.Enabled;
                // Cliente
                btCliente.Enabled = (oEmpaquetado.CodEmpleado != "");
                txCodCliente.Enabled = btCliente.Enabled;
                //ddlClientes.Enabled = btCliente.Enabled;
                // Pedido
                txNumPedido.Enabled = (oEmpaquetado.CodCliente != "");
                ddlPedidos.Enabled = txNumPedido.Enabled;

                panelDatosPesaje.Enabled=(oEmpaquetado.PesajeEnCurso.CodProducto!="")&&(oEmpaquetado.PesajeEnCurso.CodTipoCarro!="");
            }

            private void ActivarTeclado(string dato, string titulo)
            {
                INIKER_teclado.TituloDato = titulo;
                switch (dato)
                {
                    case "peso":
                        oEmpaquetado.DatoTeclado = cEmpaquetadoNuevo.eDatoTeclado.peso;
                        panelDatosPesaje.Enabled = false;
                        break;
                    case "cliente":
                        oEmpaquetado.DatoTeclado = cEmpaquetadoNuevo.eDatoTeclado.cliente;
                        break;
                    case "operario":
                        oEmpaquetado.DatoTeclado = cEmpaquetadoNuevo.eDatoTeclado.operario;
                        break;
                }
                panelTeclado.Enabled = true;
            }

            private void SetKeyBData(string commandName, string keybTitle)
            {
                ActivarTeclado(commandName, keybTitle);
            }

            private void GetPesaje(string valor)
            {
                Single tempPeso = Single.Parse(valor);

                // en la primera pesada del carro, el peso bruto será cero, por lo que hay
                // que validar que el peso indicado sea siempre mayor que el peso del tipo
                // de carro indicado
                if (oEmpaquetado.PesajeEnCurso.PesoBrutoCarro == 0)
                {
                    if (oEmpaquetado.PesajeEnCurso.PesoTipoCarro < tempPeso)
                    {
                        oEmpaquetado.PesajeEnCurso.Peso = Single.Parse(valor);
                        oEmpaquetado.PesajeEnCurso.PesoBrutoCarro = Single.Parse(valor);
                    }
                    else
                    {
                        MostrarMensaje("PESO INSUFICIENTE");
                    }
                }
                // en las pesadas siguientes, se comprueba que el peso indicado sea mayor que
                // el peso bruto acumulado del carro
                else
                {
                    if (oEmpaquetado.PesajeEnCurso.PesoBrutoCarro < tempPeso)
                    {
                        oEmpaquetado.PesajeEnCurso.Peso = Single.Parse(valor);
                        oEmpaquetado.PesajeEnCurso.PesoBrutoCarro = Single.Parse(valor);
                    }
                    else
                    {
                        MostrarMensaje("PESO INSUFICIENTE");
                    }
                }
                panelDatosPesaje.Enabled = true;
            }

        #endregion

        #region TURNOS

            protected void btTurnos_Click(object sender, EventArgs e)
            {
                panelSeleccionTurno.Visible = true;
            }

            private void CreateWorkShiftButtons()
            {
                ClearPanel();

                foreach (DataRow turno in oEmpaquetado.Turnos.Rows)
                {
                    Button WSButton = CreateButton(turno["descripcion"].ToString(),
                                                    turno["codigo"].ToString(),
                                                    turno["codigo"].ToString(),
                                                    WorkShiftButton_Click,
                                                    "textoBotonTurno",8);

                    AddButtonToPanel(WSButton);
                }
            }

            private void ClearPanel()
            {
                panelSeleccionTurno.Controls.Clear();
            }

            private void AddButtonToPanel(Button oBt)
            {
                panelSeleccionTurno.Controls.Add(oBt);
            }

            private Button CreateButton(string pText, string pID, string code)
            {
                // instancia de objeto
                Button newButton = new Button();
                // establecimiento de propiedades
                newButton.ID = pID;
                //newButton.Text = pText;
                newButton.Text = PrepararTexto(pText, 8, 2);
                newButton.ToolTip = pText;
                newButton.CssClass = "textoBotonTurno";
                newButton.Font.Bold = true;

                if (code != "")
                    newButton.CommandName = code;

                newButton.Click += new EventHandler(WorkShiftButton_Click);

                return (newButton);
            }

            protected void WorkShiftButton_Click(object sender, EventArgs e)
            {
                Button buttonSender = (Button)sender;

                oEmpaquetado.CodTurno = buttonSender.CommandName;
                oEmpaquetado.DesTurno = buttonSender.Text;

                MostrarDatosClase();
                panelSeleccionTurno.Visible = false;
                // GUION
                SetKeyBData("operario", "OPERARIO");
                txCodOperario.Focus();
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

        #endregion

        #region CLIENTES OLD
        
            //protected void txCodCliente_TextChanged(object sender, EventArgs e)
            //{
            //    txCodCliente.Text = txCodCliente.Text.Replace("*", "");
            //    SeleccionarCliente(txCodCliente.Text);
            //}

            //protected void GetClientes()
            //{
            //    cClientes oClientes = new cClientes(Session["empresaLogin"].ToString());
            //    oClientes.LoadDropDownList(ref ddlClientes);
            //}

            //protected void ObtenerNombreCliente(string cliente)
            //{
            //    cClientes oClientes = new cClientes(Session["empresaLogin"].ToString());

            //    oClientes.Get(cliente);
            //    oEmpaquetado.CodCliente = oClientes.Codigo;
            //    oEmpaquetado.NomCliente = oClientes.Alias;                
            //    oClientes.SelectDropDownList(ref ddlClientes);
            //}

            //protected void ddlClientes_SelectedIndexChanged(object sender, EventArgs e)
            //{
            //    SeleccionarCliente(ddlClientes.SelectedItem.Value);
            //}

            //private void SeleccionarCliente(string codCliente)
            //{
            //    ObtenerNombreCliente(codCliente);

            //    if (oEmpaquetado.CodCliente != "")
            //    {
            //        if ((oEmpaquetado.CodCliente != "000000")&&(oEmpaquetado.CodCliente != "100000"))
            //        {
            //            MostrarPedidos(oEmpaquetado.CodCliente);
            //        }
            //        else
            //        {
            //            MostrarBotonesFacturacion();
            //            PanelSeleccion.Visible = false;
            //            PanelRegistro.Visible = true;
            //        }
            //    }

            //    MostrarDatosClase();
            //    panelTeclado.Enabled = false;
            //    txNumPedido.Focus();
            //}

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

                    //GenerarNuevoPlegado();
                    cProduccion oProduccion = new cProduccion(Session["empresaLogin"].ToString());
                    string[] data = oProduccion.GetCustomerData(codigo).Split(';'); ;
                    if (data[0] == "")
                        throw new Exception("No existe el cliente " + codigo);
                    oEmpaquetado.CodCliente = codigo;
                    oEmpaquetado.NomCliente = data[0];

                    InicializarPedidosCliente();

                    if ((oEmpaquetado.CodCliente != "000000")&&(oEmpaquetado.CodCliente != "100000"))
                    {
                        MostrarPedidos(oEmpaquetado.CodCliente);
                    }
                    else
                    {
                        MostrarBotonesFacturacion();
                        PanelSeleccion.Visible = false;
                        PanelRegistro.Visible = true;
                    }
                       

                    MostrarDatosClase();
                    panelTeclado.Enabled = false;
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

        #region PRODUCTOS FACTURACION

            private void MostrarBotonesFacturacion()
            {
                panelSurtido.Controls.Clear();

                if (oEmpaquetado.CodCliente != "")
                {
                    cProductos oSurtido = new cProductos();
                    DataTable dtSurtFact = oSurtido.GetCustomerReferences(oEmpaquetado.CodCliente, oEmpaquetado.EmpresaLogin);

                    DataRow[] surtidoFiltrado = dtSurtFact.Select("codFactProducto is not null");

                    foreach (DataRow producto in surtidoFiltrado)
                    {                        
                        if (!ButtonExists(producto["codFactProducto"].ToString()))
                        {
                            panelSurtido.Controls.Add(
                                CreateButton(producto["desFactProducto"].ToString(),
                                                producto["codFactProducto"].ToString(),
                                                producto["codProducto"].ToString(),
                                                FamilyButton_Click, 
                                                "textoBotonSurtido",15));
                        }
                        
                    }
                }
            }

            private bool ButtonExists(string pID)
            {
                Control oCtrl = panelSurtido.FindControl(pID);
                return (oCtrl != null);
            }            

            protected void FamilyButton_Click(object sender, EventArgs e)
            {
                // se captura el objeto que lanza el evento
                Button buttonSender = (Button)sender;
                
                oEmpaquetado.PesajeEnCurso.CodProducto = buttonSender.ID;
                oEmpaquetado.PesajeEnCurso.DesProducto = buttonSender.ToolTip;

                MostrarDatosClase();
                ActivarPanelDatosPesaje();
            }

        #endregion

        #region LINEAS DE PESAJE

            protected void grdPesajes_RowDataBound(object sender, GridViewRowEventArgs e)
            {
                //se ocultan las columnas no necesarias
                e.Row.Cells[1].Visible = false; // nº línea
                e.Row.Cells[3].Visible = false; // cod. producto            
                e.Row.Cells[6].Visible = false; // cod. tipo carro
                e.Row.Cells[10].Visible = false; // línea consolidada
                e.Row.Cells[11].Visible = false; // unidades consolidación
                e.Row.Cells[12].Visible = false; // peso tipo carro
                e.Row.Cells[13].Visible = false; // tipo mov. expedicion // 13/12/12

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    // alineación del checkbox que indica carro completo
                    e.Row.Cells[9].HorizontalAlign = HorizontalAlign.Center;

                    // si no es un carro completo, hay que marcarlo
                    CheckBox myCheck = (CheckBox)e.Row.Cells[9].Controls[0];
                    if (!myCheck.Checked)
                    {
                        for (int iCell = 1; iCell < e.Row.Cells.Count; iCell++)
                        {
                            e.Row.Cells[iCell].ForeColor = System.Drawing.Color.Red;
                        }
                    }

                    // 13/12/12
                    // si el pesaje corresponde al otro proceso de pesaje (Empaquetado / Expedición) no se puede
                    // trabajar con él
                    RecuperarVariableSesion();
                    CheckBox checkExped = (CheckBox)e.Row.Cells[13].Controls[0];
                    bool mostrar = ((checkExped.Checked) && (oEmpaquetado.Tipo == cEmpaquetadoNuevo.eTipoEmpaquetado.expedicion)) ||
                        (!(checkExped.Checked) && (oEmpaquetado.Tipo == cEmpaquetadoNuevo.eTipoEmpaquetado.empaquetado));
                    e.Row.Cells[0].Controls[0].Visible = mostrar;                        
                                        
                }
            }

            protected void grdPesajes_RowDeleting(object sender, GridViewDeleteEventArgs e)
            {
                oEmpaquetado.PesajeEnCurso.Delete(int.Parse(grdPesajes.Rows[e.RowIndex].Cells[1].Text));
                MostrarDatosClase();
            }

            protected void grdPesajes_SelectedIndexChanged(object sender, EventArgs e)
            {
                if (oEmpaquetado.PesajeEnCurso.SelectByLine(int.Parse(grdPesajes.Rows[grdPesajes.SelectedIndex].Cells[1].Text)))
                {
                    MostrarDatosClase();
                }
            }

            protected void grdPesajes_DataBound(object sender, EventArgs e)
            {
                for (int iRow = 0; iRow < grdPesajes.Rows.Count; iRow++)
                {
                    System.Drawing.Color colorFondo = System.Drawing.Color.Black;

                    if (grdPesajes.Rows[iRow].RowType == DataControlRowType.DataRow)
                    {
                        try
                        {
                            grdPesajes.Rows[iRow].Cells[0].Attributes.Add("onClick", "javascript:return ConfirmAction('eliminar el registro');");
                        }
                        catch 
                        { }

                        for (int iCell = 0; iCell < grdPesajes.Rows[iRow].Cells.Count; iCell++)
                        {
                            try
                            {
                                Single valor = Single.Parse(grdPesajes.Rows[iRow].Cells[iCell].Text);
                                grdPesajes.Rows[iRow].Cells[iCell].HorizontalAlign = HorizontalAlign.Right;
                            }
                            catch 
                            { }
                        }
                    }
                }
            }
        
        #endregion

        #region LINEAS DE CARRO

            protected void grdCarros_RowDataBound(object sender, GridViewRowEventArgs e)
            {
                CheckBox rowCheck;                

                e.Row.Cells[2].Visible = false; //código de tipo de carro
                e.Row.Cells[6].Visible = false; // tipo mov. expedicion // 13/12/12


                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                    e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                    // alineación del checkbox que indica carro completo
                    e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Center;
                    //e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Center;

                    // si no es un carro completo, hay que marcarlo
                    rowCheck = (CheckBox)e.Row.Cells[5].Controls[0];
                    if (!rowCheck.Checked)
                    {
                        for (int iCell = 1; iCell < e.Row.Cells.Count; iCell++)
                        {
                            e.Row.Cells[iCell].ForeColor = System.Drawing.Color.Red;
                        }
                    }

                    // 13/12/12
                    // si el pesaje corresponde al otro proceso de pesaje (Empaquetado / Expedición) no se puede
                    // trabajar con él
                    RecuperarVariableSesion();
                    CheckBox checkExped = (CheckBox)e.Row.Cells[6].Controls[0];
                    bool mostrar = ((checkExped.Checked) && (oEmpaquetado.Tipo == cEmpaquetadoNuevo.eTipoEmpaquetado.expedicion)) ||
                        (!(checkExped.Checked) && (oEmpaquetado.Tipo == cEmpaquetadoNuevo.eTipoEmpaquetado.empaquetado));
                    e.Row.Cells[0].Controls[0].Visible = mostrar;
                                        
                    //si el carro está bloqueado, se desactiva el botón de selección de la fila
                    //rowCheck = (CheckBox)e.Row.Cells[6].Controls[0];
                    //selectButton = (LinkButton)e.Row.Cells[0].Controls[0];
                    //selectButton.Enabled=(!rowCheck.Checked);
                }
            }

            protected void grdCarros_SelectedIndexChanged(object sender, EventArgs e)
            {
                if (oEmpaquetado.PesajeEnCurso.SelectByCarro(int.Parse(grdCarros.Rows[grdCarros.SelectedIndex].Cells[1].Text)))
                {
                    panelDatosPesaje.Enabled = true;
                    MostrarDatosClase();
                }
            }

        #endregion

        #region ACCIONES

            protected void btAddLine_Click(object sender, EventArgs e)
            {
                oEmpaquetado.PesajeEnCurso.MovExpedicion=(oEmpaquetado.Tipo==cEmpaquetadoNuevo.eTipoEmpaquetado.expedicion); // 13/12/12
                oEmpaquetado.PesajeEnCurso.Add();
                ImprimirEtiquetas();
                
                MostrarDatosClase();
                grdPesajes.SelectedIndex = -1;
                grdCarros.SelectedIndex = -1;
                panelDatosPesaje.Enabled = false;
            }

            private void ImprimirEtiquetas()
            {                                
                if (oEmpaquetado.ImprimirEtiqueta)
                {
                    cPrintDocument oPrinter = new cPrintDocument(Session["empresaLogin"].ToString());
                    try
                    {
                        if (oEmpaquetado.NCarroEtiqueta==0)
                            oPrinter.TipoDocumento = cPrintDocument.eTipoDocumento.carroTransporte;
                        else
                            oPrinter.TipoDocumento = cPrintDocument.eTipoDocumento.carroIncompleto;          

                        oPrinter.CodCliente = oEmpaquetado.CodCliente;
                        //oPrinter.NomCliente = oEmpaquetado.NomCliente;
                        oPrinter.NomCliente = PrepararNombreCliente(oEmpaquetado.NomCliente);
                        oPrinter.NumPedido = oEmpaquetado.NumPedido;
                        oPrinter.NCarro = oEmpaquetado.NCarroEtiqueta;
                        oPrinter.Print(1);
                        EjecutarScript(oPrinter.ArgumentString);
                    }
                    catch (Exception ex)
                    {
                        MostrarMensaje(ex.Message.Replace("'", ""));
                    }
                }
            }

            private string PrepararNombreCliente(string _nomCliente)
            {
                if (HttpContext.Current.Session["dimensionesEtiqueta"].ToString() == "80x80")
                {
                    Int32 longitud = _nomCliente.Length;

                    if ((longitud > 15) && (longitud <= 18))
                    {
                        return (_nomCliente.PadRight(19, '.'));
                    }
                    else
                        return (_nomCliente);
                }
                else
                    return (_nomCliente);
            }

            protected void btRegistrar_Click(object sender, EventArgs e)
            {
                try
                {
                    oEmpaquetado.CerrarPedido();
                    PanelSeleccion.Visible = true;
                    PanelRegistro.Visible = false;
                }
                catch (Exception ex)
                {
                    MostrarMensaje(ex.Message.Replace("'", ""));
                }

                MostrarDatosClase();
                
            }

            protected void btVolver_Click(object sender, EventArgs e)
            {
                oEmpaquetado.Back();
                MostrarDatosClase();
                PanelSeleccion.Visible = true;
                PanelRegistro.Visible = false;
            }

            protected void btCambioVista_Click(object sender, EventArgs e)
            {
                grdCarros.Visible = !grdCarros.Visible;
                grdPesajes.Visible = !grdPesajes.Visible;
            }

            protected void btCancelar_Click(object sender, EventArgs e)
            {
                oEmpaquetado.PesajeEnCurso.Clear();
                MostrarDatosClase();
            }

            protected void btCarroCompleto_Click(object sender, EventArgs e)
            {
                oEmpaquetado.PesajeEnCurso.Completo = !oEmpaquetado.PesajeEnCurso.Completo;
                MostrarDatosClase();
            }

        #endregion

        #region TECLADO NUMERICO

            protected void INIKER_teclado_OKClick(object sender, INIKER_tecladoNumerico.OKClickEventArgs e)
            {
                switch (oEmpaquetado.DatoTeclado)
                {
                    case cEmpaquetadoNuevo.eDatoTeclado.peso:
                        GetPesaje(e.Valor);
                        //Single tempPeso=Single.Parse(e.Valor);

                        //// en la primera pesada del carro, el peso bruto será cero, por lo que hay
                        //// que validar que el peso indicado sea siempre mayor que el peso del tipo
                        //// de carro indicado
                        //if (oEmpaquetado.PesajeEnCurso.PesoBrutoCarro == 0)
                        //{
                        //    if (oEmpaquetado.PesajeEnCurso.PesoTipoCarro < tempPeso)
                        //    {
                        //        oEmpaquetado.PesajeEnCurso.Peso = Single.Parse(e.Valor);
                        //        oEmpaquetado.PesajeEnCurso.PesoBrutoCarro = Single.Parse(e.Valor);
                        //    }
                        //    else
                        //    {
                        //        MostrarMensaje("PESO INSUFICIENTE");
                        //    }
                        //}
                        //// en las pesadas siguientes, se comprueba que el peso indicado sea mayor que
                        //// el peso bruto acumulado del carro
                        //else
                        //{
                        //    if (oEmpaquetado.PesajeEnCurso.PesoBrutoCarro < tempPeso)
                        //    {
                        //        oEmpaquetado.PesajeEnCurso.Peso = Single.Parse(e.Valor);
                        //        oEmpaquetado.PesajeEnCurso.PesoBrutoCarro = Single.Parse(e.Valor);
                        //    }
                        //    else
                        //    {
                        //        MostrarMensaje("PESO INSUFICIENTE");
                        //    }
                        //}
                        //panelDatosPesaje.Enabled = true;
                        break;
                    case cEmpaquetadoNuevo.eDatoTeclado.cliente:
                        SeleccionarCliente(e.Valor);
                        break;
                    case cEmpaquetadoNuevo.eDatoTeclado.operario:
                        SeleccionarOperario(e.Valor);
                        break;
                }
                panelTeclado.Enabled = false;
                MostrarDatosClase();
            }
        
        #endregion

        #region EVENTOS CONTROLES

            protected void btDato_Click(object sender, EventArgs e)
            {
                Button oSender = (Button)sender;
                ActivarTeclado(oSender.CommandName, oSender.CommandArgument);
            }        
  
            protected void txNCarro_TextChanged(object sender, EventArgs e)
            {
                oEmpaquetado.PesajeEnCurso.NCarro = int.Parse(txNCarro.Text);
            }

            protected void txPeso_TextChanged(object sender, EventArgs e)
            {
                oEmpaquetado.PesajeEnCurso.Peso = Single.Parse(txPeso.Text);
            }
        
            protected void txOperario_TextChanged(object sender, EventArgs e)
            {
            }

            protected void ddlTiposCarro_SelectedIndexChanged(object sender, EventArgs e)
            {
                oEmpaquetado.PesajeEnCurso.CodTipoCarro = ddlTiposCarro.SelectedItem.Value;
                oEmpaquetado.PesajeEnCurso.DesTipoCarro = ddlTiposCarro.SelectedItem.Text;
                oEmpaquetado.PesajeEnCurso.PesoTipoCarro = oEmpaquetado.PesoCarro(ddlTiposCarro.SelectedItem.Value);
                            
                ActivarPanelDatosPesaje();                
            }
        
        #endregion

        #region PEDIDOS VENTA OLD

            //private void MostrarPedidos(string cliente)
            //{
            //    cPedidosVenta oPedidos=null;

            //    switch (oEmpaquetado.Tipo)
            //    {
            //        case cEmpaquetadoNuevo.eTipoEmpaquetado.empaquetado:
            //            oPedidos = new cPedidosVenta(Session["empresaLogin"].ToString(), cliente, false);
            //            break;
            //        case cEmpaquetadoNuevo.eTipoEmpaquetado.expedicion:
            //            oPedidos = new cPedidosVenta(Session["empresaLogin"].ToString(), cliente);
            //            break;
            //    }


            //    ddlPedidos.DataSource = oPedidos.DtPedidos;
            //    ddlPedidos.DataValueField = oPedidos.DtPedidos.Columns["numero"].ToString();
            //    ddlPedidos.DataTextField = oPedidos.DtPedidos.Columns["numero"].ToString();
            //    ddlPedidos.DataBind();
            //    ddlPedidos.Items.Add("");
            //    ddlPedidos.SelectedIndex = ddlPedidos.Items.Count - 1;
            //}

            //private void MostrarDespuesPedido()
            //{
            //    MostrarBotonesFacturacion();
            //    MostrarDatosClase();
            //    if (oEmpaquetado.NumPedido != "")
            //    {
            //        PanelSeleccion.Visible = false;
            //        PanelRegistro.Visible = true;
            //    }
            //}

            //protected void txNumPedido_TextChanged(object sender, EventArgs e)
            //{
            //    txNumPedido.Text = txNumPedido.Text.Replace("*", "");
            //    txNumPedido.Text = txNumPedido.Text.Replace("'", "-");
            //    SeleccionarPedido(oEmpaquetado.CodCliente,txNumPedido.Text);
            //}

            //protected void ddlPedidos_SelectedIndexChanged(object sender, EventArgs e)
            //{
            //    SeleccionarPedido(oEmpaquetado.CodCliente,ddlPedidos.SelectedItem.Value);
            //}

            //private void SeleccionarPedido(string cliente,string pedido)
            //{
            //    cPedidosVenta oPedidos = new cPedidosVenta(Session["empresaLogin"].ToString(), cliente);

            //    if (oPedidos.ExistePedido(pedido))
            //    {
            //        oEmpaquetado.NumPedido = pedido;
            //        MostrarDespuesPedido();                    
            //    }
            //    else
            //    {
            //        oEmpaquetado.NumPedido = "";                    
            //    }
            //    MostrarDatosClase();
            //    panelTeclado.Enabled = false;
            //}

        #endregion

        #region PEDIDOS VENTA

            protected void btPedidos_Click(object sender, EventArgs e)
            {
                if (oEmpaquetado.CodCliente != "")
                {
                    MostrarPedidos(oEmpaquetado.CodCliente);
                    ddlPedidos.Visible = true;
                    txNumPedido.Visible = false;
                }
            }

            private void MostrarPedidos(string cliente)
            {
                cPedidosVenta oPedidos = new cPedidosVenta(Session["empresaLogin"].ToString(), cliente, false);

                ddlPedidos.DataSource = oPedidos.DtPedidos;
                ddlPedidos.DataValueField = oPedidos.DtPedidos.Columns["Numero"].ToString();
                ddlPedidos.DataTextField = oPedidos.DtPedidos.Columns["Numero"].ToString();
                ddlPedidos.DataBind();
                ddlPedidos.Items.Add("");
                ddlPedidos.SelectedIndex = ddlPedidos.Items.Count - 1;
            }

            protected void txNumPedido_TextChanged(object sender, EventArgs e)
            {
                txNumPedido.Text = txNumPedido.Text.Replace("*", "");
                txNumPedido.Text = txNumPedido.Text.Replace("'", "-");
                SeleccionarPedido(oEmpaquetado.CodCliente, txNumPedido.Text);
            }

            protected void ddlPedidos_SelectedIndexChanged(object sender, EventArgs e)
            {
                SeleccionarPedido(oEmpaquetado.CodCliente, ddlPedidos.SelectedItem.Value);
                ddlPedidos.Visible = false;
                txNumPedido.Visible = true;
            }

            private void SeleccionarPedido(string cliente, string pedido)
            {
                cPedidosVenta oPedidos = new cPedidosVenta(Session["empresaLogin"].ToString(), cliente);

                if (oPedidos.ExistePedido(pedido))
                    oEmpaquetado.NumPedido = pedido.ToUpper();
                MostrarBotonesFacturacion();                
                if (oEmpaquetado.NumPedido != "")
                {
                    PanelSeleccion.Visible = false;
                    PanelRegistro.Visible = true;
                }

                else
                    oEmpaquetado.NumPedido = "";

                MostrarDatosClase();
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

                    oEmpaquetado.CodEmpleado = codigo;
                    oEmpaquetado.NomEmpleado= nombre;
                    MostrarDatosClase();
                    
                    SetKeyBData("cliente", "CLIENTE");
                    txCodCliente.Focus();                    
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
                switch (tipoEmpaquetado.Value)
                {
                    case "empaquetado":
                        Session["cEmpaquetado"] = oEmpaquetado;
                        break;
                    case "expedicion":
                        Session["cExpedicion"] = oEmpaquetado;
                        break;
                }
            }

            private void RecuperarVariableSesion()
            {
                switch (tipoEmpaquetado.Value)
                {
                    case "empaquetado":
                        oEmpaquetado = (cEmpaquetadoNuevo)Session["cEmpaquetado"];
                        break;
                    case "expedicion":
                        oEmpaquetado = (cEmpaquetadoNuevo)Session["cExpedicion"];
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

            private void PedirConfirmacion(string mensaje)
            {
                string key = "status";
                string javascript = "Confirmacion('" + mensaje + "','" + respuestaConfirm.ClientID + "');";

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

        #region BASCULA

            protected void hdfPeso_ValueChanged(object sender, EventArgs e)
            {
                if ((hdfPeso.Value != "-1") && (hdfPeso.Value != ""))
                {
                    try
                    {
                        GetPesaje(hdfPeso.Value);
                        MostrarDatosClase();
                    }
                    catch (Exception)
                    {
                        GetPesaje("0");
                        SetKeyBData("peso", "PESO");
                    }
                }
                else
                {
                    GetPesaje("0");
                    SetKeyBData("peso", "PESO");
                }
            }

        #endregion
    }
}
