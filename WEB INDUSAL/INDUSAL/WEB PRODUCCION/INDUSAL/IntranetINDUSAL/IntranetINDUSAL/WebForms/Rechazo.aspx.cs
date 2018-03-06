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
    public partial class Rechazo : System.Web.UI.Page
    {
        private cRechazoDesdePlegado oRechazo;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            this.INIKER_teclado.OKClick +=
                   new INIKER_tecladoNumerico.OKClickHandler(INIKER_teclado_OKClick);

            btPeso.OnClientClick = "javascript:ConectarBascula('" + hdfPeso.ClientID + "','" + txPeso.ClientID + "');";  
            txCodOperario.Attributes.Add("onfocus", "FocusScript('" + txCodOperario.ClientID + "')");

            if (!IsPostBack)
            {
                oRechazo = null;
                oRechazo = new cRechazoDesdePlegado(Session["empresaLogin"].ToString());
                GuardarVariableSesion();
                MostrarCarros();
            }
            else
            {
                RecuperarVariableSesion();
            }

            WS_CreateButtons();
        }
        
        protected void INIKER_teclado_OKClick(object sender, INIKER_tecladoNumerico.OKClickEventArgs e)
        {
            switch (oRechazo.DatoTeclado)
            {
                case "kilos":
                    GetPesaje(e.Valor);                    
                    break;
                case "operario":
                    SeleccionarOperario(e.Valor);
                    break;
            }
            // una vez actualizadas las propiedades de la clase, se muestran los datos
            MostrarDatosClase();
        }

        private void RegistrarRechazo()
        {
            try
            {
                oRechazo.Rechazar();
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message.Replace("'", ""));
            }            
        }


        #region TURNOS

            private void WS_CreateButtons()
            {
                WS_ClearPanel();

                foreach (DataRow turno in oRechazo.Turnos.Rows)
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
                oRechazo.Turno = buttonSender.CommandName;
                oRechazo.DesTurno = buttonSender.Text;
                
                GuardarVariableSesion();
                MostrarDatosClase();

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

        #region CARROS/SACAS

            private DataTable GetCarrosSacas()
            {
                cProduccion oProduccion = new cProduccion(Session["empresaLogin"].ToString());
                return(oProduccion.GetCarrosSacas());
            }

            private decimal PesoCarro(string codigo)
            {
                cProduccion oProduccion = new cProduccion(Session["empresaLogin"].ToString());
                return (oProduccion.GetPesoCarro(codigo));
            }

            private void MostrarCarros()
            {
                DataTable _dtCarros = GetCarrosSacas();
                ddlTiposCarro.DataSource = _dtCarros;
                ddlTiposCarro.DataValueField = _dtCarros.Columns["codigo"].ToString();
                ddlTiposCarro.DataTextField = _dtCarros.Columns["descripcion"].ToString();
                ddlTiposCarro.DataBind();
                ddlTiposCarro.Items.Add("");
                ddlTiposCarro.SelectedIndex = ddlTiposCarro.Items.Count - 1;
            }

            protected void ddlTiposCarro_SelectedIndexChanged(object sender, EventArgs e)
            {
                oRechazo.Producto = ddlTiposCarro.SelectedItem.Value;

                if (string.Compare(ddlTiposCarro.SelectedItem.Value, "") != 0)
                {
                    pesoCarro.Value = PesoCarro(ddlTiposCarro.SelectedItem.Value).ToString();
                    //SetKeyBData("kilos", "KILOS RECHAZO " + lbTurnoSel.Text);
                }
                else
                {
                    INIKER_teclado.TituloDato = "";
                    panelTeclado.Enabled = false;
                }

                GuardarVariableSesion();
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

            private void EscribirBarraEstado(string mensaje)
            {
                string key = "status";
                string javascript = "StatusMsj('" + mensaje + "');";

                if (!Page.ClientScript.IsStartupScriptRegistered(key))
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), key, javascript, true);
                }
            }

        #endregion



            protected void btDato_Click(object sender, EventArgs e)
            {
                Button oSender = (Button)sender;
                SetKeyBData(oSender.CommandName, oSender.CommandArgument);
            }

            private void SetKeyBData(string commandName, string keybTitle)
            {
                oRechazo.DatoTeclado = commandName;
                GuardarVariableSesion();
                ActivarTeclado(commandName, keybTitle);
            }

            private void ActivarTeclado(string dato, string titulo)
            {
                switch (dato)
                {                    
                    case "kilos":
                        INIKER_teclado.TituloDato = titulo;                        
                        panelTeclado.Enabled = true;
                        break;
                    case "operario":
                        INIKER_teclado.TituloDato = titulo;
                        INIKER_teclado.Dato = oRechazo.Operario;
                        panelTeclado.Enabled = true;
                        break;
                }
            }

        #region SESSION

            private void GuardarVariableSesion()
            {
                Session["cRechazoDesdePlegado"] = oRechazo;
            }

            private void RecuperarVariableSesion()
            {
                oRechazo = (cRechazoDesdePlegado)Session["cRechazoDesdePlegado"];
            }

        #endregion

            protected void txPeso_TextChanged(object sender, EventArgs e)
            {
                oRechazo.Kilos = decimal.Parse(txPeso.Text);
            }

            protected void txCodOperario_TextChanged(object sender, EventArgs e)
            {
                SeleccionarOperario(txCodOperario.Text.Replace("*", ""));
            }

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

                    oRechazo.Operario = codigo;
                    oRechazo.NomOperario = nombre;
                    //MostrarDatosClase();

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

            private void MostrarDatosClase()
            {
                GuardarVariableSesion();
                panelTeclado.Enabled = false;

                // si el objeto está instanciado, se muestran los datos que tenga
                if (oRechazo != null)
                {
                    // datos de operario
                    lbTurnoSel.Text = oRechazo.DesTurno;

                    txCodOperario.Text = oRechazo.Operario;
                    lbNomOperario.Text = oRechazo.NomOperario;
                }
            }
            
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

            private void GetPesaje(string valor)
            {
                if (decimal.Parse(valor) > decimal.Parse(pesoCarro.Value))
                    oRechazo.Kilos = decimal.Parse(valor) - decimal.Parse(pesoCarro.Value);
                else
                {
                    MostrarMensaje("El peso indicado es menor que el peso del carro");
                    oRechazo.Kilos = 0;
                    //ddlTiposCarro.SelectedIndex = ddlTiposCarro.Items.Count - 1;
                }

                panelTeclado.Enabled = false;
                if (oRechazo.Kilos != 0)
                {
                    RegistrarRechazo();
                    lbTurnoSel.Text = "";                    
                    ddlTiposCarro.SelectedIndex = ddlTiposCarro.Items.Count - 1;
                    INIKER_teclado.TituloDato = "";
                }
            }

        #endregion
    }
}
