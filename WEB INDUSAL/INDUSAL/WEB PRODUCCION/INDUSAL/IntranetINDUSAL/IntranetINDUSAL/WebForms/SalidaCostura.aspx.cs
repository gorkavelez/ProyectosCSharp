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
    public partial class SalidaCostura : System.Web.UI.Page
    {
        private cCostura oCostura;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.INIKER_teclado.OKClick +=
                   new INIKER_tecladoNumerico.OKClickHandler(INIKER_teclado_OKClick);

            this.INIKER_surtidoCliente.OKClick +=
                   new INIKER_surtido.OKClickHandler(INIKER_surtido_OKClick);

            txCodOperario.Attributes.Add("onfocus", "FocusScript('" + txCodOperario.ClientID + "')");
            txCodCliente.Attributes.Add("onfocus", "FocusScript('" + txCodCliente.ClientID + "')");
            txCodProducto.Attributes.Add("onfocus", "FocusScript('" + txCodProducto.ClientID + "')");

            if (!IsPostBack)
            {
                GenerarNuevoObjeto();
                GetTurnos();
                GetTrapos();
                //GUION
                SetKeyBData("operario", "OPERARIO");
                txCodOperario.Focus();    
            }
            else
            {
                RecuperarVariableSesion();
            }

            CrearControlesDinamicos();

                    
        }
                

        #region TECLADO NUMERICO
        
            protected void btDatoTeclado_Click(object sender, EventArgs e)
            {
                Button btSender = (Button)sender;
                SetKeyBData(btSender.CommandName, btSender.Text);
            }

            protected void INIKER_teclado_OKClick(object sender, INIKER_tecladoNumerico.OKClickEventArgs e)
            {
                switch (oCostura.DatoTeclado)
                {
                    case "operario":
                        SeleccionarOperario(e.Valor);
                        break;
                    case "cliente":
                        SeleccionarCliente(e.Valor);
                        break;
                    case "cantidad":
                        oCostura.Cantidad = decimal.Parse(e.Valor);
                        if (oCostura.CodOperacion == "trapos")
                            MostrarPanelResumen();
                        break;
                }            
                panelTeclado.Enabled = false;
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

                    oCostura.CodOperario = codigo;
                    oCostura.NomOperario = nombre;
                    MostrarDatosClase();
                    //GUION
                    panelSeleccionTurno.Visible = true;
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
                SeleccionarOperario(txCodOperario.Text.Replace("*",""));
            }
        
        #endregion

        #region SURTIDO

            protected void INIKER_surtido_OKClick(object sender, INIKER_surtido.OKClickEventArgs e)
            {
                oCostura.CodProducto = e.Codigo;
                oCostura.NomProducto = e.Descripcion;
                MostrarDatosClase();
                panelSurtido.Visible = false;
                SetKeyBData("cantidad", "CANTIDAD");
                txCantidad.Focus();
            }

            private void MostrarSurtidoCliente(string cliente)
                {
                    try
                    {
                        INIKER_surtidoCliente.Reset();
                        INIKER_surtidoCliente.EmpresaLogin = Session["empresaLogin"].ToString();
                        INIKER_surtidoCliente.CodCliente = cliente;
                        INIKER_surtidoCliente.DesFamilia = "";
                        INIKER_surtidoCliente.DesSubfamilia = "";
                        INIKER_surtidoCliente.Nivel = 2;

                        INIKER_surtidoCliente.Load();
                        panelSurtido.Visible = true;
                    }
                    catch
                    {
                        panelSurtido.Visible = false;
                    }

                }

            protected void btProducto_Click(object sender, EventArgs e)
            {
                panelSurtido.Visible = true;
            }

            protected void txCodProducto_TextChanged(object sender, EventArgs e)
            {
                SeleccionarProducto(txCodProducto.Text.Replace("*", ""));
            }

            private void SeleccionarProducto(string codigo)
            {
                try
                {
                    cProduccion oProduccion = new cProduccion(Session["empresaLogin"].ToString());
                    string data = oProduccion.GetItemDescription(codigo);
                    if (data == "")
                        throw new Exception("No existe el producto " + codigo);

                    oCostura.CodProducto = codigo;
                    oCostura.NomProducto = data;
                    MostrarDatosClase();
                }
                catch (IndexOutOfRangeException)
                {
                    MostrarMensaje("No existe el producto " + codigo);
                    txCodProducto.Focus();
                }
                catch (Exception ex)
                {
                    MostrarMensaje(ex.Message.Replace("'", ""));
                }
            }

        #endregion

        #region CLIENTES

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
                    oCostura.CodCliente = codigo;
                    oCostura.NomCliente = data[0];
                    MostrarSurtidoCliente(codigo);
                    panelSurtido.Visible = true;
                    MostrarDatosClase();
                    //GUION
                    txCodProducto.Focus();
                    
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

            protected void txCodCliente_TextChanged(object sender, EventArgs e)
            {
                SeleccionarCliente(txCodCliente.Text.Replace("*", ""));
            }

        #endregion

        #region METODOS

            private void MostrarDatosClase()
            {
                GuardarVariableSesion();

                lbTurnoSel.Text = oCostura.NomTurno;
                txCodOperario.Text = oCostura.CodOperario;
                lbNomOperario.Text = oCostura.NomOperario;
                lbOperacionSel.Text = oCostura.NomOperacion;
                txCodCliente.Text = oCostura.CodCliente;
                lbNomCliente.Text = oCostura.NomCliente;
                txCodProducto.Text = oCostura.CodProducto;
                lbNomProducto.Text = oCostura.NomProducto;                
                txCantidad.Text = oCostura.Cantidad.ToString();                

                MostrarDatosResumen();

                ControlActivacionControles();
            }

            private void MostrarDatosResumen()
            {
                lbTurnoResumen.Text = oCostura.NomTurno;
                lbOperarioResumen.Text = oCostura.NomOperario;
                lbOperacionResumen.Text = oCostura.NomOperacion;
                lbClienteResumen.Text = oCostura.NomCliente;
                lbProductoResumen.Text = oCostura.NomProducto;
                lbCantidadResumen.Text = oCostura.Cantidad.ToString();
            }

            private void ControlActivacionControles()
            {
                btTrapos.Enabled = (oCostura.CodOperario != "" && oCostura.CodTurno != "");
                btCostura.Enabled = btTrapos.Enabled &&
                    (oCostura.CodCliente != "" && oCostura.CodProducto != "" && oCostura.Cantidad > 0);
                btEntradaOxido.Enabled = btCostura.Enabled;
                btTraspasoCliente.Enabled = btCostura.Enabled;
                btTraspasoSucia.Enabled = btCostura.Enabled;
                btAumentoCostura.Enabled = btCostura.Enabled;
                btConfeccion.Enabled = btCostura.Enabled;
                btBaja.Enabled = btCostura.Enabled;

            }

            private int ObtenerInventProdAlm()
            {
                cProduccion oProduccion = new cProduccion(Session["empresaLogin"].ToString());
                return (oProduccion.GetItemLocationInventory(oCostura.CodProducto, "COSTURA"));
            }

            private void GenerarNuevoObjeto()
            {
                oCostura = null;
                oCostura = new cCostura(Session["empresaLogin"].ToString());
                Session["cCostura"] = oCostura;
            }

            private void ActivarTeclado(string dato, string titulo)
            {
                INIKER_teclado.TituloDato = titulo;
                switch (dato)
                {
                    case "operario":
                        INIKER_teclado.Dato = oCostura.CodOperario;
                        break;
                    case "cliente":
                        INIKER_teclado.Dato = oCostura.CodCliente;
                        break;
                    case "cantidad":
                        INIKER_teclado.Dato = oCostura.Cantidad.ToString();
                        break;
                }
                panelTeclado.Enabled = true;
            }

            private void SetKeyBData(string commandName, string keybTitle)
            {
                oCostura.DatoTeclado = commandName;
                GuardarVariableSesion();
                ActivarTeclado(commandName, keybTitle);
            }
            
            private void CrearControlesDinamicos()
            {
                try
                {
                    CreateWorkShiftButtons();
                    CreateItemButtons();
                }
                catch
                { }
            }

            private void Rechazar(int cantidad, bool desdeOxido)
            {
                try
                {
                    cRechazoDesdePlegado oRechazo = new cRechazoDesdePlegado(Session["empresaLogin"].ToString());
                    oRechazo.Cliente = oCostura.CodCliente;
                    oRechazo.Pedido = "";
                    oRechazo.Producto = oCostura.CodProducto;
                    oRechazo.NSerie = oCostura.NSerie;
                    oRechazo.Operario = oCostura.CodOperario;
                    oRechazo.Turno = oCostura.CodTurno;
                    oRechazo.Cantidad = cantidad;

                    if (desdeOxido)
                        oRechazo.DeOxidoACostura();
                    else
                        oRechazo.ACostura();
                }
                catch (Exception ex)
                {
                    MostrarMensaje(ex.Message.Replace("'", ""));
                }
            }

            private void Volver()
            {
                panelAcciones.Visible = false;
                panelIntroduccion.Visible = true;
            }

        #endregion

        #region ACCIONES

            protected void btOperacion_Click(object sender, EventArgs e)
            {
                Button btSender = (Button)sender;
                oCostura.CodOperacion = btSender.CommandName;
                oCostura.NomOperacion = btSender.Text;
                
                if (btSender.CommandName=="trapos")
                {
                    oCostura.AjusteInventario = true;
                    oCostura.CodCliente = "";
                    oCostura.NomCliente = "";
                    oCostura.CodProducto = "";
                    oCostura.NomProducto = "";
                    oCostura.Cantidad = 0;

                    panelSeleccionTrapo.Visible = true;
                    panelSurtido.Visible = false;
                }
                else
                    MostrarPanelResumen();

                MostrarDatosClase();
                
            }

            private void MostrarPanelResumen()
            {
                panelIntroduccion.Visible = false;
                panelAcciones.Visible = true;
            }

            protected void btRegistrar_Click(object sender, EventArgs e)
            {
                try
                {
                    switch (oCostura.CodOperacion)
                    {
                        case "entrada_costura":
                            Rechazar(int.Parse(oCostura.Cantidad.ToString()), false);
                            break;
                        case "oxido_costura":
                            Rechazar(int.Parse(oCostura.Cantidad.ToString()), true);
                            break;
                        default:
                            oCostura.Registrar();
                            break;
                    }

                    //if (oCostura.CodOperacion == "entrada_costura")
                    //{
                    //    Rechazar(int.Parse(oCostura.Cantidad.ToString()));
                    //    oCostura.Cantidad = 0;
                    //}
                    //else
                    //    oCostura.Registrar();

                    Volver();
                    SetKeyBData("cantidad", "CANTIDAD");
                }
                catch (Exception ex)
                {
                    MostrarMensaje(ex.Message.Replace("'", ""));
                }

                MostrarDatosClase();
            }

            protected void btCancelar_Click(object sender, EventArgs e)
            {
                Volver();
            }
        
        #endregion

        #region TURNOS

            protected void btTurnos_Click(object sender, EventArgs e)
            {
                panelSeleccionTurno.Visible = true;
            }

            private void GetTurnos()
            {
                cProduccion oProduccion = new cProduccion(Session["empresaLogin"].ToString());
                oCostura.dtTurnos = oProduccion.GetTurnos();
            }

            private void CreateWorkShiftButtons()
            {
                ClearPanel(panelSeleccionTurno);

                foreach (DataRow turno in oCostura.dtTurnos.Rows)
                {
                    Button WSButton = CreateButton(turno["descripcion"].ToString(),
                                                    turno["codigo"].ToString(),
                                                    turno["codigo"].ToString());

                    WSButton.Click += new EventHandler(WorkShiftButton_Click);
                    
                    AddButtonToPanel(WSButton,panelSeleccionTurno);
                }
            }

            protected void WorkShiftButton_Click(object sender, EventArgs e)
            {
                Button buttonSender = (Button)sender;

                oCostura.CodTurno = buttonSender.CommandName;
                oCostura.NomTurno = buttonSender.Text;

                MostrarDatosClase();
                panelSeleccionTurno.Visible = false;
                // GUION
                SetKeyBData("cliente", "CLIENTE");
                txCodCliente.Focus();
            }

        #endregion

        #region TRAPOS

            private void GetTrapos()
            {
                cProduccion oProduccion = new cProduccion(Session["empresaLogin"].ToString());
                oCostura.dtTrapos = oProduccion.GetTrapos("K", "TP");
            }

            private void CreateItemButtons()
            {
                ClearPanel(panelSeleccionTrapo);

                foreach (DataRow turno in oCostura.dtTrapos.Rows)
                {
                    Button itemButton = CreateButton(turno["descripcion"].ToString(),
                                                    turno["codigo"].ToString(),
                                                    turno["codigo"].ToString());

                    itemButton.Click += new EventHandler(ItemButton_Click);

                    AddButtonToPanel(itemButton, panelSeleccionTrapo);
                }
            }

            protected void ItemButton_Click(object sender, EventArgs e)
            {
                Button buttonSender = (Button)sender;

                oCostura.CodProducto = buttonSender.CommandName;
                oCostura.NomProducto = buttonSender.Text;

                MostrarDatosClase();
                panelSeleccionTrapo.Visible = false;
                // GUION
                SetKeyBData("cantidad", "CANTIDAD");                
            }

        #endregion

        #region GENERACION BOTONES DINAMICOS

            private void ClearPanel(Panel oPnl)
            {
                oPnl.Controls.Clear();
            }

            private void AddButtonToPanel(Button oBt, Panel oPnl)
            {
                oPnl.Controls.Add(oBt);
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

                return (newButton);
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

        #region SESSION

        private void GuardarVariableSesion()
        {
            Session["cCostura"] = oCostura;
        }

        private void RecuperarVariableSesion()
        {
            oCostura = (cCostura)Session["cCostura"];
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



        

        

        


        
    }
}
