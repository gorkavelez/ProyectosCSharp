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
    public partial class Lavadora : System.Web.UI.Page
    {
        private cLavado oLavado; 

        protected void Page_Load(object sender, EventArgs e)
        {
            this.INIKER_teclado.OKClick +=
                   new INIKER_tecladoNumerico.OKClickHandler(INIKER_teclado_OKClick);

            btPeso.OnClientClick = "javascript:ConectarBascula('" + hdfPeso.ClientID + "','" + txPeso.ClientID + "');";
            
            txCodCliente.Attributes.Add("onfocus", "FocusScript('" + txCodCliente.ClientID + "')");
            txNumPedido.Attributes.Add("onfocus", "FocusScript('" + txNumPedido.ClientID + "')"); 

            if (!IsPostBack)
            {
                string tipo = Request.QueryString["Tipo"];
                oLavado = null;                
                oLavado = new cLavado(Session["empresaLogin"].ToString(), int.Parse(tipo));
                tipoLavado.Value = oLavado.TipoLavado.ToString().ToLower();
                GuardarVariableSesion();

                MostrarLavadoras();
                //MostrarEmpleados();                                

                if (oLavado.TipoLavado == cLavado.eTipoLavado.Lavadora)
                {
                    MostrarCarros();                    
                    //GetClientes();
                }

                MostrarPanelesPorTipo();
                MostrarDatosClase();
            }
            else
            {
                RecuperarVariableSesion();
            }

            CreateWorkShiftButtons();                        
            // se muestra el título correspondiente al tipo de lavado
            this.Title = oLavado.WebFormCaption;
            
        }

        #region EVENTOS DE CONTROLES

            protected void btDato_Click(object sender, EventArgs e)
            {
                Button oSender = (Button)sender;
                // se captura el nombre del emisor
                hdDatoTeclado.Value = oSender.CommandName;
                ActivarTeclado(oSender.CommandName, oSender.CommandArgument);
            }

            protected void INIKER_teclado_OKClick(object sender, INIKER_tecladoNumerico.OKClickEventArgs e)
            {
                panelTeclado.Enabled = false;

                switch (hdDatoTeclado.Value)
                {
                    case "peso":
                        GetPesaje(e.Valor.ToString());
                        //Single pesoCarro = oLavado.PesoCarro(oLavado.PesajeEnCurso.CodCarro);
                        //Single pesoNeto = Single.Parse(e.Valor.ToString()) - pesoCarro;
                        //if (pesoNeto > 0)
                        //{
                        //    oLavado.PesajeEnCurso.Peso = decimal.Parse(pesoNeto.ToString());
                        //}
                        //else
                        //{
                        //    MostrarMensaje("NO SE PUEDE INDICAR UN PESO NEGATIVO");
                        //}
                        ////ImprimirEtiquetas();
                        //// se guardan los datos de cliente y pedido para imprimir las etiquetas
                        //hdCliente.Value = oLavado.PesajeEnCurso.CodCliente + ";" + oLavado.PesajeEnCurso.NomCliente;
                        //hdPedido.Value = oLavado.PesajeEnCurso.NumPedido;
                        //AgregarPesaje();
                        //SetKeyBData("copias", "ETIQUETAS");
                        break;
                    case "horas":
                        oLavado.PesajeEnCurso.Tiempo = decimal.Parse(e.Valor.ToString());
                        SetKeyBData("kilos", "KG LAVADOS");
                        break;
                    case "kilos":
                        decimal tempKilos=decimal.Parse(e.Valor.ToString());
                        if(tempKilos<=oLavado.PesoMaximoMaquina)
                            oLavado.PesajeEnCurso.Peso = tempKilos;
                        else
                        {
                            MostrarMensaje("PESO MAXIMO EXCEDIDO");
                        }
                        break;
                    case "cliente":
                        oLavado.PesajeEnCurso.CodCliente = e.Valor;
                        SeleccionarCliente(e.Valor);
                        break;
                    case "operario":                        
                        SeleccionarOperario(e.Valor);
                        break;
                    case "copias":
                        ImprimirEtiquetas(int.Parse(e.Valor));
                        break;
                }
                
                // una vez actualizadas las propiedades de la clase, se muestran los datos
                MostrarDatosClase();
            }

            protected void hdfPeso_ValueChanged(object sender, EventArgs e)
            {
                if ((hdfPeso.Value != "-1") && (hdfPeso.Value != ""))
                {
                    try
                    {
                        //oLavado.PesajeEnCurso.Peso = decimal.Parse(hdfPeso.Value);
                        GetPesaje(hdfPeso.Value);
                        MostrarDatosClase();
                    }
                    catch (Exception)
                    {
                        //oLavado.PesajeEnCurso.Peso = 0;
                        GetPesaje("0");
                        SetKeyBData("peso", "PESO");
                    }
                }
                else
                {
                    //oLavado.PesajeEnCurso.Peso = 0;
                    GetPesaje("0");
                    SetKeyBData("peso", "PESO");
                }
            }     

        #endregion

        #region MAQUINAS Y PROGRAMAS

            protected void ddlMaquinas_SelectedIndexChanged(object sender, EventArgs e)
            {
                oLavado.CodMaquina = ddlMaquinas.SelectedItem.Value;
                oLavado.Maquina = ddlMaquinas.SelectedItem.Text;
                MostrarProgramas();
                MostrarDatosClase();
            }

            protected void ddlTuneles_SelectedIndexChanged(object sender, EventArgs e)
            {
                oLavado.CodMaquina = ddlTuneles.SelectedItem.Value;
                oLavado.Maquina = ddlTuneles.SelectedItem.Text;
                MostrarDatosClase();
            }

            protected void ddlProgramas_SelectedIndexChanged(object sender, EventArgs e)
            {
                oLavado.CodProgLavado = ddlProgramas.SelectedItem.Value;
                oLavado.ProgLavado = ddlProgramas.SelectedItem.Text;
                MostrarDatosClase();
                panelSeleccionTurno.Visible = true;
            }

        #endregion

        #region CARROS Y PESO

            protected void txPeso_TextChanged(object sender, EventArgs e)
            {
                oLavado.PesajeEnCurso.Peso = decimal.Parse(txPeso.Text);
            }

            protected void ddlTiposCarro_SelectedIndexChanged(object sender, EventArgs e)
            {
                oLavado.PesajeEnCurso.CodCarro = ddlTiposCarro.SelectedItem.Value;
                oLavado.PesajeEnCurso.DesCarro = ddlTiposCarro.SelectedItem.Text;
                MostrarDatosClase();
                // desactivo esta parte para que capture el peso de la báscula
                //SetKeyBData("peso", "PESO");                
                btPeso.Enabled = true;
                btPesoTeclado.Enabled = true;
            }

        #endregion

        #region LINEAS PESAJE

            protected void grdPesajes_SelectedIndexChanged(object sender, EventArgs e)
            {
                oLavado.PesajeEnCurso.NLinea = int.Parse(grdPesajes.Rows[grdPesajes.SelectedIndex].Cells[2].Text);
                oLavado.SelectPesaje();
                MostrarDatosClase();
            }

            protected void grdPesajes_RowDataBound(object sender, GridViewRowEventArgs e)
            {
                // se formatea la carga de datos en el dataGrid

                // en todas las filas, incluida la de cabecera, se ocultan ciertas columnas
                e.Row.Cells[1].Visible = false;         //nLinea
                e.Row.Cells[2].Visible = false;         //CodOperario
                e.Row.Cells[5].Visible = false;         //CodCarro
                e.Row.Cells[7].Visible = false;         //Fecha
                e.Row.Cells[9].Visible = false;         //CodTurno
                e.Row.Cells[11].Visible = false;         //CodCliente
            }

            protected void grdPesajes_RowDeleting(object sender, GridViewDeleteEventArgs e)
            {
                oLavado.PesajeEnCurso.NLinea = int.Parse(grdPesajes.Rows[e.RowIndex].Cells[1].Text);
                oLavado.PesajeEnCurso.Delete();
                MostrarDatosClase();
            }

        #endregion

        #region ACCIONES

            protected void btEtiqueta_Click(object sender, EventArgs e)
            {
                // TODO: Procedimiento para imprimir una etiqueta con la línea seleccionada           
            }

            protected void btCancelar_Click(object sender, EventArgs e)
            {
                oLavado.ClearPesajes();
                MostrarDatosClase();
            }

            protected void btRegistrar_Click(object sender, EventArgs e)
            {
                try
                {
                    oLavado.Register();
                }
                catch (Exception ex)
                {
                    MostrarMensaje(ex.Message.Replace("'", ""));
                }

                MostrarDatosClase();
            }

        #endregion

        #region METODOS

            private void MostrarPanelesPorTipo()
            {
                PanelMaquina.Visible = (oLavado.TipoLavado == cLavado.eTipoLavado.Lavadora);
                PanelTunel.Visible = (oLavado.TipoLavado == cLavado.eTipoLavado.Tunel);
                PanelDatosEmpleado.Visible = true;
                panelDatosCliente.Visible = (oLavado.TipoLavado == cLavado.eTipoLavado.Lavadora);
                PanelCarro.Visible = (oLavado.TipoLavado == cLavado.eTipoLavado.Lavadora);
                PanelHoras.Visible = (oLavado.TipoLavado == cLavado.eTipoLavado.Tunel);
                panelRegistro.Visible = true;                
            }

            private void MostrarDatosClase()
            {
                GuardarVariableSesion();

                txCodOperario.Text = oLavado.PesajeEnCurso.CodOperario;
                lbNomOperario.Text = oLavado.PesajeEnCurso.NomOperario;
                //ddlEmpleados.SelectedIndex = ddlEmpleados.Items.IndexOf(ddlEmpleados.Items.FindByValue(oLavado.PesajeEnCurso.CodOperario));

                txCodCliente.Text = oLavado.PesajeEnCurso.CodCliente;
                lbNomCliente.Text = oLavado.PesajeEnCurso.NomCliente;

                lbTurnoSel.Text = oLavado.PesajeEnCurso.DesTurno;
                
                txNumPedido.Text = oLavado.PesajeEnCurso.NumPedido;
                ddlPedidos.SelectedIndex = ddlPedidos.Items.IndexOf(ddlPedidos.Items.FindByValue(oLavado.PesajeEnCurso.NumPedido)); 
                                
                ddlMaquinas.SelectedIndex = ddlMaquinas.Items.IndexOf(ddlMaquinas.Items.FindByValue(oLavado.CodMaquina));                        

                ddlProgramas.SelectedIndex = ddlProgramas.Items.IndexOf(ddlProgramas.Items.FindByValue(oLavado.CodProgLavado));
                                        
                txPeso.Text = oLavado.PesajeEnCurso.Peso.ToString();
                ddlTiposCarro.SelectedIndex = ddlTiposCarro.Items.IndexOf(ddlTiposCarro.Items.FindByValue(oLavado.PesajeEnCurso.CodCarro));
                
                grdPesajes.DataSource = oLavado.PesajeEnCurso.Datos;
                grdPesajes.DataBind();                

                ddlTuneles.SelectedIndex = ddlTuneles.Items.IndexOf(ddlTuneles.Items.FindByValue(oLavado.CodMaquina));
                
                txHorasTunel.Text = oLavado.PesajeEnCurso.Tiempo.ToString();
                txKilosTunel.Text = oLavado.PesajeEnCurso.Peso.ToString();

                lbPesoMaximo.Text = oLavado.PesoMaximoMaquina.ToString();
                if (lbPesoMaximo.Text == "0") lbPesoMaximo.Text = "No especificado";

                // control estado activación controles
                switch (oLavado.TipoLavado)
                {
                    case cLavado.eTipoLavado.Lavadora:
                        PanelDatosEmpleado.Enabled = (oLavado.CodProgLavado != "");
                        btOperario.Enabled = (oLavado.PesajeEnCurso.CodTurno != "");
                        txCodOperario.Enabled = btOperario.Enabled;
                        //ddlEmpleados.Enabled = btOperario.Enabled;
                        btCliente.Enabled = (oLavado.PesajeEnCurso.CodOperario != "");
                        txCodCliente.Enabled = btCliente.Enabled;
                        //ddlClientes.Enabled = btCliente.Enabled;
                        btPedidos.Enabled=(oLavado.PesajeEnCurso.CodCliente!="");
                        txNumPedido.Enabled = (oLavado.PesajeEnCurso.CodCliente != "");
                        ddlPedidos.Enabled = txNumPedido.Enabled;
                        ddlTiposCarro.Enabled = (oLavado.PesajeEnCurso.CodCliente != "");
                        btPeso.Enabled = (oLavado.PesajeEnCurso.CodCarro != "");
                        btPesoTeclado.Enabled = (oLavado.PesajeEnCurso.CodCarro != "");
                        btRegistrar.Enabled = (oLavado.PesajeEnCurso.Datos.Rows.Count > 0);
                        break;
                }
            }

            private void MostrarLavadoras()
            {
                switch (oLavado.TipoLavado)
                {
                    case cLavado.eTipoLavado.Lavadora:
                        CargarDatosDDL(ref ddlMaquinas);
                        break;
                    case cLavado.eTipoLavado.Tunel:
                        CargarDatosDDL(ref ddlTuneles);
                        break;
                }
            }

            private void CargarDatosDDL(ref DropDownList oDDL)
            {
                oDDL.DataSource = oLavado.Lavadoras;
                oDDL.DataValueField = oLavado.Lavadoras.Columns["codigo"].ToString();
                oDDL.DataTextField = oLavado.Lavadoras.Columns["descripcion"].ToString();
                oDDL.DataBind();
                oDDL.Items.Add("");
                oDDL.SelectedIndex = oDDL.Items.Count - 1;
            }

            private void MostrarProgramas()
            {
                ddlProgramas.DataSource = oLavado.FiltrarProgramasMaquina();
                ddlProgramas.DataValueField = oLavado.ProgramasMaquina.Columns["programa"].ToString();
                ddlProgramas.DataTextField = oLavado.ProgramasMaquina.Columns["programa"].ToString();
                ddlProgramas.DataBind();
                ddlProgramas.Items.Add("");
                ddlProgramas.SelectedIndex = ddlProgramas.Items.Count - 1;
            }

            private void MostrarCarros()
            {
                ddlTiposCarro.DataSource = oLavado.Carros;
                ddlTiposCarro.DataValueField = oLavado.Carros.Columns["codigo"].ToString();
                ddlTiposCarro.DataTextField = oLavado.Carros.Columns["descripcion"].ToString();
                ddlTiposCarro.DataBind();
                ddlTiposCarro.Items.Add("");
                ddlTiposCarro.SelectedIndex = ddlTiposCarro.Items.Count - 1;                
            }
        
            private void AgregarPesaje()
            {
                try
                {
                    oLavado.AddPesaje();                    
                }
                catch (Exception ex)
                {
                    MostrarMensaje(ex.Message.Replace("'", ""));
                }
                grdPesajes.SelectedIndex = -1;
                MostrarDatosClase();
            }

            private void ImprimirEtiquetas(int nCopias)
            {                
                cPrintDocument oPrinter = new cPrintDocument(Session["empresaLogin"].ToString());
                try
                {
                    oPrinter.TipoDocumento = cPrintDocument.eTipoDocumento.carroLavado;
                    //oPrinter.CodCliente = oLavado.PesajeEnCurso.CodCliente;
                    //oPrinter.NomCliente = oLavado.PesajeEnCurso.NomCliente;
                    //oPrinter.NumPedido = oLavado.PesajeEnCurso.NumPedido;
                    string[] datosCliente = hdCliente.Value.Split(';');
                    oPrinter.CodCliente = datosCliente[0];
                    oPrinter.NomCliente = datosCliente[1];
                    oPrinter.NumPedido = hdPedido.Value;

                    //oPrinter.Print(1);
                    oPrinter.Print(nCopias);
                    EjecutarScript(oPrinter.ArgumentString);
                }
                catch(Exception ex)
                {
                    MostrarMensaje(ex.Message.Replace("'",""));
                }
            }

            private void ActivarTeclado(string dato, string titulo)
            {
                INIKER_teclado.TituloDato = titulo;
                switch (dato)
                {
                    case "peso":
                        INIKER_teclado.Dato = oLavado.PesajeEnCurso.Peso.ToString();
                        break;
                    case "horas":
                        INIKER_teclado.Dato = oLavado.PesajeEnCurso.Tiempo.ToString();
                        break;
                    case "kilos":
                        INIKER_teclado.Dato = oLavado.PesajeEnCurso.Peso.ToString();
                        break;
                    case "cliente":
                        INIKER_teclado.Dato = oLavado.PesajeEnCurso.CodCliente;
                        break;
                    case "operario":
                        INIKER_teclado.Dato = oLavado.PesajeEnCurso.CodOperario;
                        break;
                    case "copias":
                        INIKER_teclado.Dato = "1";
                        break;
                }
                panelTeclado.Enabled = true;
            }

            private void SetKeyBData(string commandName, string keybTitle)
            {                
                hdDatoTeclado.Value = commandName;
                ActivarTeclado(commandName, keybTitle);
            }

            private void GetPesaje(string valor)
            {
                Single pesoCarro = oLavado.PesoCarro(oLavado.PesajeEnCurso.CodCarro);
                Single pesoNeto = Single.Parse(valor) - pesoCarro;
                if (pesoNeto > 0)
                {
                    oLavado.PesajeEnCurso.Peso = decimal.Parse(pesoNeto.ToString());
                }
                else
                {
                    MostrarMensaje("NO SE PUEDE INDICAR UN PESO NEGATIVO");
                }
                //ImprimirEtiquetas();
                // se guardan los datos de cliente y pedido para imprimir las etiquetas
                hdCliente.Value = oLavado.PesajeEnCurso.CodCliente + ";" + oLavado.PesajeEnCurso.NomCliente;
                hdPedido.Value = oLavado.PesajeEnCurso.NumPedido;
                AgregarPesaje();
                SetKeyBData("copias", "ETIQUETAS");
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

                foreach (DataRow turno in oLavado.Turnos.Rows)
                {
                    Button WSButton = CreateButton(turno["descripcion"].ToString(),
                                                    turno["codigo"].ToString(),
                                                    turno["codigo"].ToString());

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
                
                oLavado.PesajeEnCurso.CodTurno = buttonSender.CommandName;
                oLavado.PesajeEnCurso.DesTurno = buttonSender.Text;

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
                                        
                    cProduccion oProduccion = new cProduccion(Session["empresaLogin"].ToString());
                    string[] data = oProduccion.GetCustomerData(codigo).Split(';'); ;
                    if (data[0] == "")
                        throw new Exception("No existe el cliente " + codigo);
                    oLavado.PesajeEnCurso.CodCliente = codigo;
                    oLavado.PesajeEnCurso.NomCliente = data[0];

                    InicializarPedidosCliente();
                    
                    MostrarDatosClase();
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
                oLavado.PesajeEnCurso.NumPedido = "";
                ddlPedidos.Items.Clear();
                ddlPedidos.Visible = false;
            }

        #endregion        

        #region PEDIDOS VENTA

            protected void btPedidos_Click(object sender, EventArgs e)
            {
                if (oLavado.PesajeEnCurso.CodCliente != "")
                {
                    MostrarPedidos(oLavado.PesajeEnCurso.CodCliente);
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
                SeleccionarPedido(oLavado.PesajeEnCurso.CodCliente, txNumPedido.Text);
            }

            protected void ddlPedidos_SelectedIndexChanged(object sender, EventArgs e)
            {
                //SeleccionarPedido(oLavado.PesajeEnCurso.CodCliente, ddlPedidos.SelectedItem.Value);
                oLavado.PesajeEnCurso.NumPedido = ddlPedidos.SelectedItem.Text;
                MostrarDatosClase();
                ddlPedidos.Visible = false;
                txNumPedido.Visible = true;
            }

            private void SeleccionarPedido(string cliente, string pedido)
            {
                cPedidosVenta oPedidos = new cPedidosVenta(Session["empresaLogin"].ToString(), cliente);

                if (oPedidos.ExistePedido(pedido))
                    oLavado.PesajeEnCurso.NumPedido = pedido.ToUpper();
                else
                    oLavado.PesajeEnCurso.NumPedido = "";

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

                    oLavado.PesajeEnCurso.CodOperario = codigo;
                    oLavado.PesajeEnCurso.NomOperario = nombre;
                    MostrarDatosClase();
                    if (oLavado.TipoLavado == cLavado.eTipoLavado.Lavadora)
                    {
                        SetKeyBData("cliente", "CLIENTE");
                        txCodCliente.Focus();
                    }
                    else
                    {
                        SetKeyBData("horas", "HR LAVADO");
                    }
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

        #region SESSION

            private void GuardarVariableSesion()
            {
                switch (tipoLavado.Value)
                {
                    case "lavadora":
                        Session["cLavadora"] = oLavado;
                        break;
                    case "tunel":
                        Session["cTunel"] = oLavado;
                        break;
                }
            }

            private void RecuperarVariableSesion()
            {
                switch (tipoLavado.Value)
                {
                    case "lavadora":
                        oLavado = (cLavado)Session["cLavadora"];
                        break;
                    case "tunel":
                        oLavado= (cLavado)Session["cTunel"];
                        break;
                }
            }

        #endregion          

            
    }
}
