using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IntranetINDUSAL.Controles_Personalizados;
using IntranetINDUSAL.Negocio;
using IntranetINDUSAL.Automatizacion;
using System.Data;
using IntranetINDUSAL.Reports;

namespace IntranetINDUSAL.WebForms
{
    public partial class RecepcionRopa : System.Web.UI.Page
    {
        private cRecepcion oRecepcion;   //objeto que contiene los datos de la recepción en curso        

        protected void Page_Load(object sender, EventArgs e)
        {
            this.INIKER_teclado.OKClick +=
                   new INIKER_tecladoNumerico.OKClickHandler(INIKER_teclado_OKClick);  
            btPeso.OnClientClick="javascript:ConectarBascula('"+ hdfPeso.ClientID +"','" + txPeso.ClientID+"');";  
            
            if (!IsPostBack)
            {
                // en la primera carga, se instancia el objeto recepcion 
                // y se establece el origen de datos del Grid
                oRecepcion = new cRecepcion(Session["empresaLogin"].ToString());
                Session["cRecepcion"] = oRecepcion;
                MostrarTransportistas();
                MostrarIncidencias();
            }
            else
            {
                oRecepcion = (cRecepcion)Session["cRecepcion"];
            }
        }

        #region Eventos de Controles
        
            protected void INIKER_teclado_OKClick(object sender, INIKER_tecladoNumerico.OKClickEventArgs e)
            {
                PanelTeclado.Enabled = false;

                switch (oRecepcion.TipoDatoTeclado)
                {
                    case "entregado":
                        oRecepcion.Entregado = int.Parse(e.Valor);
                        // GUION
                        // Se modifica por petición del cliente el 12/03/10
                        //SetKeyBData("vacios","VACIOS");
                        SetKeyBData("etiquetas", "ETIQUETAS");
                        break;
                    case "recogido":
                        oRecepcion.Recibido = int.Parse(e.Valor);
                        // GUION
                        SetKeyBData("entregado", "ENTREGADO");
                        break;
                    case "peso":
                        oRecepcion.Peso = decimal.Parse(e.Valor);
                        hdfPeso.Value = e.Valor;
                        // Se anula por petición del cliente el 12/03/10
                        //SetKeyBData("etiquetas", "ETIQUETAS");
                        break;
                    case "vacios":
                        oRecepcion.CarrosVacios = int.Parse(e.Valor);
                        // GUION
                        // Se anula por petición del cliente el 12/03/10
                        //SetKeyBData("peso", "PESO");
                        break;
                    case "etiquetas":
                        oRecepcion.NEtiquetas = int.Parse(e.Valor);
                        break;
                }                
                // una vez actualizadas las propiedades de la clase, se muestran los datos
                MostrarDatosClase();
            }

            protected void btDato_Click(object sender, EventArgs e)
            {
                Button oSender = (Button)sender;
                
                // se parametriza el control teclado
                SetKeyBData(oSender.CommandName,oSender.CommandArgument);                
            }

            protected void btCancelRecep_Click(object sender, EventArgs e)
            {
                oRecepcion.Init();
                MostrarDatosClase();
            }

            protected void ddlTransportistas_SelectedIndexChanged(object sender, EventArgs e)
            {
                oRecepcion.CodTransportista = ddlTransportistas.SelectedItem.Value;
                oRecepcion.Transportista = ddlTransportistas.SelectedItem.Text;
                oRecepcion.GetRutasTransportista();
                MostrarRutasTransporte();
            }

            protected void ddlRutasTransportista_SelectedIndexChanged(object sender, EventArgs e)
            {
                oRecepcion.CodRuta = ddlRutasTransportista.SelectedItem.Value;
                oRecepcion.Ruta = ddlRutasTransportista.SelectedItem.Text;
                oRecepcion.GetClientesRuta();
                MostrarClientesRuta();
                MostrarDatosRegistradosRuta();
            }

            protected void ddlClientesRuta_SelectedIndexChanged(object sender, EventArgs e)
            {
                oRecepcion.CodCliente = ddlClientesRuta.SelectedItem.Value;
                oRecepcion.NomCliente = ddlClientesRuta.SelectedItem.Text;
                // GUION
                if (oRecepcion.CodCliente!="")
                    SetKeyBData("recogido", "RECOGIDO");
                MostrarDatosClase();
            }
        
            protected void ddlIncidencias_SelectedIndexChanged(object sender, EventArgs e)
            {
                oRecepcion.CodIncidencia = ddlIncidencias.SelectedItem.Value;
                oRecepcion.DesIncidencia = ddlIncidencias.SelectedItem.Text;
            }

            protected void Registrar_Click(object sender, EventArgs e)
            {
                try
                {
                    Button btSender = (Button)sender;
                    ImprimirEtiquetas(oRecepcion.NEtiquetas);
                    oRecepcion.Register(btSender.CommandArgument == "SinConteo", btSender.CommandArgument=="TercerCircuito");
                }
                catch (Exception ex)
                {
                    MostrarMensaje(ex.Message.Replace("'",""));
                }
                MostrarDatosClase();
                MostrarDatosRegistradosRuta();
            }

        #endregion

        #region Metodos Privados

            private void MostrarDatosClase()
            {
                Session["cRecepcion"] = oRecepcion;
                ddlTransportistas.SelectedIndex = ddlTransportistas.Items.IndexOf(ddlTransportistas.Items.FindByValue(oRecepcion.CodTransportista));
                ddlRutasTransportista.SelectedIndex = ddlRutasTransportista.Items.IndexOf(ddlRutasTransportista.Items.FindByValue(oRecepcion.CodRuta));
                ddlClientesRuta.SelectedIndex = ddlClientesRuta.Items.IndexOf(ddlClientesRuta.Items.FindByValue(oRecepcion.CodCliente));
                txRecogido.Text = oRecepcion.Recibido.ToString();
                txEntregado.Text = oRecepcion.Entregado.ToString();
                txVacios.Text = oRecepcion.CarrosVacios.ToString();
                txEtiquetas.Text = oRecepcion.NEtiquetas.ToString();
                ddlIncidencias.SelectedIndex = ddlIncidencias.Items.IndexOf(ddlIncidencias.Items.FindByValue(oRecepcion.CodIncidencia));
                txPeso.Text = oRecepcion.Peso.ToString();

                panelAcciones.Enabled=(oRecepcion.CodCliente!="");

                //lbError.Text = oRecepcion.ErrorMessage;                
            }

            private void MostrarTransportistas()
            {
                ddlTransportistas.DataSource = oRecepcion.Transportistas;
                ddlTransportistas.DataValueField = oRecepcion.Transportistas.Columns["codigo"].ToString();
                ddlTransportistas.DataTextField = oRecepcion.Transportistas.Columns["nombre"].ToString();
                ddlTransportistas.DataBind();
                ddlTransportistas.Items.Add("");
                ddlTransportistas.SelectedIndex = ddlTransportistas.Items.Count - 1;
            }

            private void MostrarRutasTransporte()
            {
                ddlRutasTransportista.DataSource = oRecepcion.Rutas;
                ddlRutasTransportista.DataValueField = oRecepcion.Rutas.Columns["codigo"].ToString();
                ddlRutasTransportista.DataTextField = oRecepcion.Rutas.Columns["nombre"].ToString();
                ddlRutasTransportista.DataBind();
                ddlRutasTransportista.Items.Add("");
                ddlRutasTransportista.SelectedIndex = ddlRutasTransportista.Items.Count - 1;
            }

            private void MostrarClientesRuta()
            {
                ddlClientesRuta.DataSource = oRecepcion.Clientes;
                ddlClientesRuta.DataValueField = oRecepcion.Clientes.Columns["codigo"].ToString();
                ddlClientesRuta.DataTextField = oRecepcion.Clientes.Columns["alias"].ToString();
                ddlClientesRuta.DataBind();
                ddlClientesRuta.Items.Add("");
                ddlClientesRuta.SelectedIndex = ddlClientesRuta.Items.Count - 1;
            }

            private void MostrarIncidencias()
            {
                ddlIncidencias.DataSource = oRecepcion.Incidencias;
                ddlIncidencias.DataValueField = oRecepcion.Incidencias.Columns["codigo"].ToString();
                ddlIncidencias.DataTextField = oRecepcion.Incidencias.Columns["descripcion"].ToString();
                ddlIncidencias.DataBind();
                ddlIncidencias.Items.Add("");
                ddlIncidencias.SelectedIndex = ddlIncidencias.Items.Count - 1;
            }

            private void MostrarDatosRegistradosRuta()
            {
                cProduccion oProduccion=new cProduccion(Session["empresaLogin"].ToString());
                DataTable datosReg = oProduccion.GetMovsRecepcion(oRecepcion.CodTransportista, oRecepcion.CodRuta);
                grdDatosReg.DataSource = datosReg;
                grdDatosReg.DataBind();
            }
       
            private bool ComprobarDatos()
            {
                if (ddlTransportistas.SelectedItem == null)
                    return (false);
                if (ddlRutasTransportista.SelectedItem == null)
                    return (false);
                if (ddlClientesRuta.SelectedItem == null)
                    return (false);

                if (txEntregado.Text == "" && txRecogido.Text == "")
                    return (false);

                return (true);
            }

            private void SetKeyBData(string commandName, string keybTitle)
            {
                INIKER_teclado.TituloDato = keybTitle;
                oRecepcion.TipoDatoTeclado = commandName;
                switch (commandName)
                {
                    case "entregado":
                        INIKER_teclado.Dato = oRecepcion.Entregado.ToString();
                        break;
                    case "recogido":
                        INIKER_teclado.Dato = oRecepcion.Recibido.ToString();
                        break;
                    case "peso":
                        INIKER_teclado.Dato = oRecepcion.Peso.ToString();
                        break;
                    case "vacios":
                        INIKER_teclado.Dato = oRecepcion.CarrosVacios.ToString();
                        break;
                    case "etiquetas":
                        INIKER_teclado.Dato = oRecepcion.NEtiquetas.ToString();
                        break;
                }
                PanelTeclado.Enabled = true;
            }

            private void ImprimirEtiquetas(int nEtiq)
            {
                if (nEtiq != 0)
                {
                    cPrintDocument oPrinter = new cPrintDocument(Session["empresaLogin"].ToString());

                    oPrinter.TipoDocumento = cPrintDocument.eTipoDocumento.carroTransporte;
                    oPrinter.CodCliente = oRecepcion.CodCliente;
                    oPrinter.NomCliente = oRecepcion.NomCliente;

                    oPrinter.Print(nEtiq);
                    EjecutarScript(oPrinter.ArgumentString);
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

            //protected void btPeso_Click(object sender, EventArgs e)
            //{
            //    CapturarPeso();
            //    MostrarDatosClase();
            //}

            //private void CapturarPeso()
            //{
            //    string key = "peso";
            //    string javascript = "ConectarBascula('" + hdfPeso.ClientID + "','" + txPeso.ClientID + "');";

            //    if (!Page.ClientScript.IsStartupScriptRegistered(key))
            //    {
            //        Page.ClientScript.RegisterStartupScript(Page.GetType(), key, javascript, true);
            //    }
            //}

            protected void hdfPeso_ValueChanged(object sender, EventArgs e)
            {
                if ((hdfPeso.Value != "-1") && (hdfPeso.Value != ""))
                {
                    try
                    {
                        oRecepcion.Peso = decimal.Parse(hdfPeso.Value);
                        MostrarDatosClase();
                    }
                    catch (Exception)
                    {
                        oRecepcion.Peso = 0;
                        SetKeyBData("peso", "PESO");
                    }
                }
                else
                {
                    oRecepcion.Peso = 0;
                    SetKeyBData("peso", "PESO");
                }
            }        
 
    }
}
