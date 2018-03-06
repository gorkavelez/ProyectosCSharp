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
    public partial class Costura : System.Web.UI.Page
    {
        private cCostura oCostura;        

        protected void Page_Load(object sender, EventArgs e)
        {
            this.INIKER_teclado.OKClick +=
                   new INIKER_tecladoNumerico.OKClickHandler(INIKER_teclado_OKClick);

            this.INIKER_surtidoCliente.OKClick +=
                   new INIKER_surtido.OKClickHandler(INIKER_surtido_OKClick);

            txCliente.Attributes.Add("onfocus", "FocusScript('" + txCliente.ClientID + "')");

            if (!IsPostBack)
            {
                //Session["empresaLogin"] = "02 NAVARRA";
                GenerarNuevoObjeto();
                GetTurnos();
            }
            else
            {
                RecuperarVariableSesion();
            }
            
            CreateWorkShiftButtons();
                    
        }

        #region TECLADO NUMERICO

            protected void INIKER_teclado_OKClick(object sender, INIKER_tecladoNumerico.OKClickEventArgs e)
            {                
                panelTeclado.Enabled = false;                
                MostrarDatosClase();                
            }

        #endregion

        #region SURTIDO

            protected void INIKER_surtido_OKClick(object sender, INIKER_surtido.OKClickEventArgs e)
            {              
                MostrarDatosClase();                
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

        #endregion

        #region CLIENTES

            protected void btDatoTeclado_Click(object sender, EventArgs e)
            {
                Button btSender = (Button)sender;
                ActivarTeclado(btSender.CommandName, btSender.Text);                
            }  

            protected void txCodCliente_TextChanged(object sender, EventArgs e)
            {
                txCliente.Text = txCliente.Text.Replace("*", "");
                SeleccionarCliente(txCliente.Text);
            }

            protected void ObtenerNombreCliente(string cliente)
            {
                             
            }            

            private void SeleccionarCliente(string codCliente)
            {


            }            
                      
        #endregion        

        #region METODOS

            private void MostrarDatosClase()
            {
                GuardarVariableSesion();

                lbTurnoSel.Text = oCostura.NomTurno;

                ControlActivacionControles();
            }

            private void ControlActivacionControles()
            {
                panelOperario.Enabled = (oCostura.CodTurno != "");
                panelOperacion.Enabled=(oCostura.CodOperario!="");
                panelCliente.Enabled = (oCostura.CodOperacion !="");
                panelSurtido.Enabled = (oCostura.CodCliente != "");
                panelCantidad.Enabled = (oCostura.CodProducto != "");
                panelAcciones.Enabled = (oCostura.Cantidad != 0);
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
                ActivarTeclado(commandName, keybTitle);
            }

        #endregion

        #region ACCIONES

            protected void btRegistrar_Click(object sender, EventArgs e)
            {
                try
                {
                    //oConteo.Register();
                }
                catch (Exception ex)
                {
                    MostrarMensaje(ex.Message);
                }

                MostrarDatosClase();
            }                       

        #endregion

        #region TURNOS

            private void GetTurnos()
            {
                cProduccion oProduccion = new cProduccion(Session["empresaLogin"].ToString());
                oCostura.dtTurnos = oProduccion.GetTurnos();
            }

            private void CreateWorkShiftButtons()
            {
                ClearPanel();

                foreach (DataRow turno in oCostura.dtTurnos.Rows)
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

                oCostura.CodTurno = buttonSender.CommandName;
                oCostura.NomTurno = buttonSender.Text;

                MostrarDatosClase();
                // GUION
                //SetKeyBData("operario", "OPERARIO");
                //txCodOperario.Focus();
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
                oCostura= (cCostura)Session["cCostura"];
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
