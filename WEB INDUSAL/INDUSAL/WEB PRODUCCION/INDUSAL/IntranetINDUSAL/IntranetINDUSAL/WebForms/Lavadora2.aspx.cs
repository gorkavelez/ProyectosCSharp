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
    public partial class Lavadora2 : System.Web.UI.Page
    {
        static cLavado oLavado;
        static cClientes oClientes;
        static cEmpleados oEmpleados;
        static cPedidosVenta oPedidos;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.INIKER_teclado.OKClick +=
                   new INIKER_tecladoNumerico.OKClickHandler(INIKER_teclado_OKClick);
            
            txCodCliente.Attributes.Add("onfocus", "FocusScript('" + txCodCliente.ClientID + "')"); 

            if (!IsPostBack)
            {
                cListado oListado = (cListado)Session["ListadoSeleccion"];
                if (oListado == null)
                {
                    string tipo = Request.QueryString["Tipo"];
                    oLavado = null;
                    oLavado = new cLavado(Session["empresaLogin"].ToString(), int.Parse(tipo));
                    MostrarLavadoras();
                    MostrarEmpleados();

                    if (oLavado.TipoLavado == cLavado.eTipoLavado.Lavadora)
                    {
                        MostrarCarros();
                        GetClientes();
                    }                    
                }
                else
                {
                    MostrarLavadoras();
                    MostrarEmpleados();

                    if (oLavado.TipoLavado == cLavado.eTipoLavado.Lavadora)
                    {
                        MostrarCarros();
                        GetClientes();
                    }

                    oLavado = (cLavado)oListado.FormData;
                    Session["ListadoSeleccion"] = null;
                    SeleccionListado(oListado);
                    oListado = null;
                }

                MostrarPanelesPorTipo();
                MostrarDatosClase();

            }

            CreateWorkShiftButtons();                        
            // se muestra el título correspondiente al tipo de lavado
            this.Title = oLavado.WebFormCaption;
            
        }

        #region EVENTOS DE CONTROLES

            protected void ddlMaquinas_SelectedIndexChanged(object sender, EventArgs e)
            {
                oLavado.CodMaquina = ddlMaquinas.SelectedItem.Value;
                oLavado.Maquina = ddlMaquinas.SelectedItem.Text;
                MostrarProgramas();
                MostrarDatosClase();
            }

            protected void btDato_Click(object sender, EventArgs e)
            {
                Button oSender = (Button)sender;
                // se captura el nombre del emisor
                hdDatoTeclado.Value = oSender.CommandName;
                // se parametriza el control teclado
                INIKER_teclado.TituloDato = oSender.Text;
                switch (oSender.CommandName)
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
                }
            }

            protected void INIKER_teclado_OKClick(object sender, INIKER_tecladoNumerico.OKClickEventArgs e)
            {
                switch (hdDatoTeclado.Value)
                {
                    case "peso":
                        Single pesoCarro = oLavado.PesoCarro(oLavado.PesajeEnCurso.CodCarro);
                        Single pesoNeto = Single.Parse(e.Valor.ToString()) - pesoCarro;
                        oLavado.PesajeEnCurso.Peso = decimal.Parse(pesoNeto.ToString());
                        AgregarPesaje();
                        break;
                    case "horas":
                        oLavado.PesajeEnCurso.Tiempo = decimal.Parse(e.Valor.ToString());
                        break;
                    case "kilos":
                        oLavado.PesajeEnCurso.Peso = decimal.Parse(e.Valor.ToString());
                        break;
                    case "cliente":
                        oLavado.PesajeEnCurso.CodCliente = e.Valor;
                        SeleccionarCliente(e.Valor);
                        break;
                    case "operario":                        
                        SeleccionarOperario(e.Valor);
                        break;
                }
                // una vez actualizadas las propiedades de la clase, se muestran los datos
                MostrarDatosClase();
            }

            protected void btEtiqueta_Click(object sender, EventArgs e)
            {
                // TODO: Procedimiento para imprimir una etiqueta con la línea seleccionada           
            }

            protected void txPeso_TextChanged(object sender, EventArgs e)
            {
                oLavado.PesajeEnCurso.Peso = decimal.Parse(txPeso.Text);
            }

            protected void ddlTiposCarro_SelectedIndexChanged(object sender, EventArgs e)
            {
                //oLavado.PesajeEnCurso.CodCarro = ddlTiposCarro.SelectedItem.Value;
                //oLavado.PesajeEnCurso.DesCarro = ddlTiposCarro.SelectedItem.Text;
            }

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
                e.Row.Cells[2].Visible = false;         //nLinea
                e.Row.Cells[3].Visible = false;         //CodOperario
                e.Row.Cells[6].Visible = false;         //CodCarro
                e.Row.Cells[8].Visible = false;         //Fecha
                e.Row.Cells[10].Visible = false;         //CodTurno
                e.Row.Cells[12].Visible = false;         //CodCliente
            }

            protected void btCancelar_Click(object sender, EventArgs e)
            {
                oLavado.ClearPesajes();
                MostrarDatosClase();
            }

            protected void btRegistrar_Click(object sender, EventArgs e)
            {
                oLavado.Register();
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
                txCodOperario.Text = oLavado.PesajeEnCurso.CodOperario;
                lbNomOperario.Text = oLavado.PesajeEnCurso.NomOperario;
                //ddlEmpleados.SelectedIndex = ddlEmpleados.Items.IndexOf(ddlEmpleados.Items.FindByValue(oLavado.PesajeEnCurso.CodOperario));

                txCodCliente.Text = oLavado.PesajeEnCurso.CodCliente;                
                //ddlClientes.SelectedIndex = ddlClientes.Items.IndexOf(ddlClientes.Items.FindByValue(oLavado.PesajeEnCurso.CodCliente));

                lbTurnoSel.Text = oLavado.PesajeEnCurso.DesTurno;
                
                //txPedido.Text = oLavado.PesajeEnCurso.NumPedido;
                //grdPedidos.DataSource = oLavado.Pedidos;
                //grdPedidos.DataBind();
                txNumPedido.Text = oLavado.PesajeEnCurso.NumPedido;
                //ddlPedidos.SelectedIndex = ddlPedidos.Items.IndexOf(ddlPedidos.Items.FindByValue(oLavado.PesajeEnCurso.NumPedido)); 
                                
                ddlMaquinas.SelectedIndex = ddlMaquinas.Items.IndexOf(ddlMaquinas.Items.FindByValue(oLavado.CodMaquina));                                                        
                ddlProgramas.SelectedIndex = ddlProgramas.Items.IndexOf(ddlProgramas.Items.FindByValue(oLavado.CodProgLavado));
                                        
                txPeso.Text = oLavado.PesajeEnCurso.Peso.ToString();                
                //ddlTiposCarro.SelectedIndex = ddlTiposCarro.Items.IndexOf(ddlTiposCarro.Items.FindByValue(oLavado.PesajeEnCurso.CodCarro));
                
                grdPesajes.DataSource = oLavado.PesajeEnCurso.Datos;
                grdPesajes.DataBind();                
                                
                ddlTuneles.SelectedIndex = ddlTuneles.Items.IndexOf(ddlTuneles.Items.FindByValue(oLavado.CodMaquina));
                
                txHorasTunel.Text = oLavado.PesajeEnCurso.Tiempo.ToString();
                txKilosTunel.Text = oLavado.PesajeEnCurso.Peso.ToString();
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
                //ddlTiposCarro.DataSource = oLavado.Carros;
                //ddlTiposCarro.DataValueField = oLavado.Carros.Columns["codigo"].ToString();
                //ddlTiposCarro.DataTextField = oLavado.Carros.Columns["descripcion"].ToString();
                //ddlTiposCarro.DataBind();
                //ddlTiposCarro.Items.Add("");
                //ddlTiposCarro.SelectedIndex = ddlTiposCarro.Items.Count - 1;                
            }

        
            private void AgregarPesaje()
            {
                oLavado.AddPesaje();
                grdPesajes.SelectedIndex = -1;
                MostrarDatosClase();
            }

            private void SeleccionListado(cListado oListado)
            {
                switch (oListado.DatoListado)
                {
                    case "operario":
                        if (oListado.SelectValue != "")
                        {
                            oLavado.PesajeEnCurso.CodOperario = oListado.SelectValue;
                            oLavado.PesajeEnCurso.NomOperario = oListado.SelectText;
                        }
                        break;
                }
            }

        #endregion

        #region TURNOS
        
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

            protected void txCodCliente_TextChanged(object sender, EventArgs e)
            {
                SeleccionarCliente(txCodCliente.Text);
            }

            protected void ddlClientes_SelectedIndexChanged(object sender, EventArgs e)
            {
                //SeleccionarCliente(ddlClientes.SelectedItem.Value);
            }

            protected void GetClientes()
            {
                oClientes = null;
                oClientes = new cClientes(Session["empresaLogin"].ToString());
                //oClientes.LoadDropDownList(ref ddlClientes);
            }

            protected void ObtenerNombreCliente(string cliente)
            {
                if (oClientes.Get(cliente))
                {
                    oLavado.PesajeEnCurso.CodCliente = oClientes.Codigo;
                    oLavado.PesajeEnCurso.NomCliente = oClientes.Alias;
                    //oClientes.SelectDropDownList(ref ddlClientes);
                    GetOrders(oClientes.Codigo);
                    MostrarPedidos();
                }
                else
                {
                    oLavado.PesajeEnCurso.CodCliente = "";
                    oLavado.PesajeEnCurso.NomCliente = "";
                    oPedidos = null;
                    EscribirBarraEstado("El cliente " + cliente + " no existe.");
                }
            }

            private void SeleccionarCliente(string codCliente)
            {
                ObtenerNombreCliente(codCliente);
                MostrarDatosClase();
            }

        #endregion

        #region PEDIDOS VENTA

            private void GetOrders(string cliente)
            {
                oPedidos = new cPedidosVenta(Session["empresaLogin"].ToString(), cliente);
            }

            private void MostrarPedidos()
            {
                //ddlPedidos.DataSource = oPedidos.DtPedidos;
                //ddlPedidos.DataValueField = oPedidos.DtPedidos.Columns["numero"].ToString();
                //ddlPedidos.DataTextField = oPedidos.DtPedidos.Columns["numero"].ToString();
                //ddlPedidos.DataBind();
                //ddlPedidos.Items.Add("");
                //ddlPedidos.SelectedIndex = ddlPedidos.Items.Count - 1;
            }

            protected void txNumPedido_TextChanged(object sender, EventArgs e)
            {
                SeleccionarPedido(txNumPedido.Text);
            }

            protected void ddlPedidos_SelectedIndexChanged(object sender, EventArgs e)
            {
                //SeleccionarPedido(ddlPedidos.SelectedItem.Value);                
            }

            private void SeleccionarPedido(string pedido)
            {
                if (oPedidos.ExistePedido(pedido))
                {
                    oLavado.PesajeEnCurso.NumPedido = pedido.ToUpper();
                }
                else
                {
                    oLavado.PesajeEnCurso.NumPedido = "";
                }
                MostrarDatosClase();
            }

        #endregion

        #region EMPLEADOS

            private void MostrarEmpleados()
            {
                oEmpleados = new cEmpleados(Session["empresaLogin"].ToString());
                //oEmpleados.LoadDropDownList(ref ddlEmpleados);
            }

            private void SeleccionarOperario(string operario)
            {
                if (oEmpleados.Get(operario))
                {
                    oLavado.PesajeEnCurso.CodOperario = operario.ToUpper();
                    oLavado.PesajeEnCurso.NomOperario = oEmpleados.Nombre;
                }
                else
                {
                    oLavado.PesajeEnCurso.CodOperario = "";
                    oLavado.PesajeEnCurso.NomOperario = "";
                }

                MostrarDatosClase();
            }

            protected void txCodOperario_TextChanged(object sender, EventArgs e)
            {
                SeleccionarOperario(txCodOperario.Text);
            }

            protected void ddlEmpleados_SelectedIndexChanged(object sender, EventArgs e)
            {
                DropDownList ddlSender = (DropDownList)sender;

                oLavado.PesajeEnCurso.CodOperario = ddlSender.SelectedItem.Value;
                oLavado.PesajeEnCurso.NomOperario = ddlSender.SelectedItem.Text;
            }
        
        #endregion

            private void EscribirBarraEstado(string mensaje)
            {
                string key = "status";
                string javascript = "StatusMsj('" + mensaje + "');";

                if (!Page.ClientScript.IsStartupScriptRegistered(key))
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), key, javascript, true);
                }                
            }

            protected void btSelOperario_Click(object sender, EventArgs e)
            {
                cListado oListado = new cListado();
                oListado.Datos = oEmpleados.tablaClientes;
                oListado.ValueColumn = "codigo";
                oListado.TextColumn = "nombre";
                oListado.FormData=oLavado;
                oListado.UrlBack= Request.Url.ToString();
                oListado.DatoListado = "operario";
                Session["ListadoSeleccion"] = oListado;
                Response.Redirect("~/WebForms/Seleccion.aspx");
            }

    }
}
