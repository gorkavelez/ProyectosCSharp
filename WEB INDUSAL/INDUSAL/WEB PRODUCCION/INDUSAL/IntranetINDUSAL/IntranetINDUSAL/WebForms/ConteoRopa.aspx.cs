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
    public partial class ConteoRopa : System.Web.UI.Page
    {
        static cConteo oConteo;
        static cSurtidoCliente oSurtidoSAL;
        static cClientes oClientes;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.INIKER_teclado.OKClick +=
                   new INIKER_tecladoNumerico.OKClickHandler(INIKER_teclado_OKClick);

            if (!IsPostBack)
            {
                GenerarNuevoConteo();
                GetClientes();                
            }
            
            CargarPaneles();
        }

        #region EVENTOS CONTROLES

            protected void INIKER_teclado_OKClick(object sender, INIKER_tecladoNumerico.OKClickEventArgs e)
            {
                oConteo.LineaEnCurso.Cantidad = int.Parse(e.Valor);
                PanelTeclado.Enabled = false;
                AgregarLinea();
                MostrarDatosClase();
            }

            protected void FamilyButton_Click(object sender, EventArgs e)
            {
                Button buttonSender = (Button)sender;
                switch (buttonSender.ID.Substring(0, 2))
                {
                    case "f_":
                        oConteo.CodFamilia = buttonSender.CommandName;
                        oConteo.Familia = buttonSender.ToolTip;
                        MostrarSubfamilia(buttonSender.ToolTip);
                        pnlProductos.Controls.Clear();
                        PanelTeclado.Enabled = false;
                        oConteo.LineaEnCurso.Cantidad = 0;
                        break;
                    case "s_":
                        oConteo.CodSubfamilia = buttonSender.CommandName;
                        oConteo.Subfamilia = buttonSender.ToolTip;
                        MostrarProductos(buttonSender.ToolTip);
                        oConteo.LineaEnCurso.Cantidad = 0;
                        PanelTeclado.Enabled = false;
                        break;
                    case "p_":
                        oConteo.LineaEnCurso.CodProducto = buttonSender.CommandName;
                        oConteo.LineaEnCurso.NomProducto = buttonSender.ToolTip;
                        oConteo.LineaEnCurso.Cantidad = 0;
                        PanelTeclado.Enabled = true;
                        PanelTeclado.Focus();
                        break;
                }

                MostrarDatosClase();
            }

            protected void gridConteo_RowDeleting(object sender, GridViewDeleteEventArgs e)
            {
                oConteo.LineaEnCurso.NLinea = int.Parse(gridConteo.Rows[e.RowIndex].Cells[2].Text.ToString());
                oConteo.DelLinea();
                MostrarDatosClase();
            }

            protected void btRegistrar_Click(object sender, EventArgs e)
            {
                oConteo.Register();
                MostrarDatosClase();
                LimpiarSurtidoCliente();
            }

            protected void gridConteo_SelectedIndexChanged(object sender, EventArgs e)
            {
                oConteo.LineaEnCurso.NLinea = int.Parse(gridConteo.Rows[gridConteo.SelectedIndex].Cells[2].ToString());
                oConteo.SelectLinea();
                MostrarDatosClase();
            }

            protected void btAceptar_Click(object sender, EventArgs e)
            {
                AgregarLinea();                
            }

            protected void btCancelRecep_Click(object sender, EventArgs e)
            {
                oConteo.ClearLineas();
                MostrarDatosClase();
            }

            protected void gridConteo_RowDataBound(object sender, GridViewRowEventArgs e)
            {
                e.Row.Cells[2].Visible = false;
                e.Row.Cells[3].Visible = false;
                e.Row.Cells[5].Visible = false;

                if (e.Row.RowType == DataControlRowType.Header)
                {
                    e.Row.Cells[4].Text = "PRODUCTO";
                    e.Row.Cells[6].Text = "CANTIDAD";
                }

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Right;
                }
            }

        #endregion

        #region SURTIDO

            private void CargarPaneles()
            {
                MostrarBotonesFamilia();
                MostrarBotonesSubfamilia();
                try
                {
                    MostrarBotonesProductos();
                }
                catch (Exception ex)
                { }
            }

            protected void MostrarSurtidoClienteSAL(string cliente)
            {
                LimpiarSurtidoCliente();

                if (oSurtidoSAL == null)
                    oSurtidoSAL = new cSurtidoCliente(cliente, Session["empresaLogin"].ToString());

                MostrarBotonesFamilia();
                MostrarBotonesSubfamilia();
            }

            private void MostrarBotonesFamilia()
            {
                pnlFamilias.Controls.Clear();

                if (oSurtidoSAL != null)
                {
                    //string[] familias = oSurtidoSAL.ArrayFamilias();
                    string[] familias = oSurtidoSAL.ArrayFamiliasInclCodigo();
                    foreach (string oFam in familias)
                    {
                        string[] datosFamilia = oFam.Split(';');
                        //pnlFamilias.Controls.Add(CreateButton(oFam, "f_" + oFam, ""));
                        pnlFamilias.Controls.Add(CreateButton(datosFamilia[0], "f_" + datosFamilia[0], datosFamilia[1]));
                    }
                }
            }

            private void MostrarSubfamilia(string familia)
            {
                if (familia != null)
                {
                    lbFamSel.Text = familia;
                    oSurtidoSAL.famSel = familia;
                    //oPlegado.FamiliaSel = familia;
                    // se muestran las subfamilias de la familia seleccionada
                    MostrarBotonesSubfamilia();
                    oSurtidoSAL.subfamSel = null;
                    lbSubfamSel.Text = "";
                }
            }

            private void MostrarBotonesSubfamilia()
            {
                pnlSubfamilias.Controls.Clear();

                if ((oSurtidoSAL != null) && (oConteo.CodFamilia != ""))
                {
                    try
                    {
                        string[] subfamilias = oSurtidoSAL.ArraySubfamiliasInclCodigo();
                        foreach (string oSubfam in subfamilias)
                        {
                            string[] datosSubfamilia = oSubfam.Split(';');
                            //pnlSubfamilias.Controls.Add(CreateButton(oSubfam, "s_" + oSubfam, ""));
                            pnlSubfamilias.Controls.Add(CreateButton(datosSubfamilia[0], "s_" + datosSubfamilia[0], datosSubfamilia[1]));
                        }
                    }
                    catch (Exception ex) { }
                }
            }

            private void MostrarProductos(string subfamilia)
            {
                if (subfamilia != null)
                {
                    lbSubfamSel.Text = subfamilia;
                    oSurtidoSAL.subfamSel = subfamilia;
                    //oPlegado.SubfamiliaSel = subfamilia;
                    MostrarBotonesProductos();
                }
            }

            private void MostrarBotonesProductos()
            {
                pnlProductos.Controls.Clear();

                if ((oSurtidoSAL != null) && (lbSubfamSel.Text != ""))
                {
                    string productos = oSurtidoSAL.ArrayProductos("|");
                    string[] arrayProductos = productos.Split('|');

                    foreach (string oProd in arrayProductos)
                    {
                        string[] datosProducto = oProd.Split(';');
                        pnlProductos.Controls.Add(CreateButton(datosProducto[1], "p_" + datosProducto[0], datosProducto[0]));
                    }
                }
            }

            private Button CreateButton(string pText, string pID, string code)
            {
                // instancia de objeto
                Button newButton = new Button();
                // establecimiento de propiedades
                newButton.ID = pID;
                //newButton.Text = pText;
                newButton.Text = PrepararTexto(pText, 15, 2);
                newButton.ToolTip = pText;
                newButton.CssClass = "textoBotonSurtido";

                if (code != "")
                    newButton.CommandName = code;

                newButton.Click += new EventHandler(FamilyButton_Click);
                //newButton.Attributes.Add("OnClick", "javascript:return DatosObligatorios();");

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

            private void LimpiarSurtidoCliente()
            {
                lbFamSel.Text = "";
                lbSubfamSel.Text = "";

                pnlFamilias.Controls.Clear();
                pnlSubfamilias.Controls.Clear();
                pnlProductos.Controls.Clear();
            }

        #endregion

        #region CLIENTES
         
            protected void txCodCliente_TextChanged(object sender, EventArgs e)
            {
                GenerarNuevoConteo();

                ObtenerNombreCliente(txCliente.Text);
                MostrarDatosClase();

                if (oConteo.CodCliente != "")
                    MostrarSurtidoClienteSAL(txCliente.Text);

            }

            protected void GetClientes()
            {
                oClientes = null;
                oClientes = new cClientes(Session["empresaLogin"].ToString());
                oClientes.LoadDropDownList(ref ddlClientes);
            }

            protected void ObtenerNombreCliente(string cliente)
            {
                if (oClientes.Get(cliente))
                {
                    oClientes.SelectDropDownList(ref ddlClientes);
                    oConteo.CodCliente = cliente;
                    oConteo.NomCliente = oClientes.Alias;
                    oConteo.CodAlmacen = oClientes.Almacen;
                    MostrarDatosClase();
                }
            }

            protected void ddlClientes_SelectedIndexChanged(object sender, EventArgs e)
            {
                ObtenerNombreCliente(ddlClientes.SelectedItem.Value);
            }

        #endregion

        private void MostrarDatosClase()
        {
            txCliente.Text = oConteo.CodCliente;
            ddlClientes.SelectedIndex = ddlClientes.Items.Count - 1;
            ddlClientes.SelectedIndex = ddlClientes.Items.IndexOf(ddlClientes.Items.FindByValue(oConteo.CodCliente));
            //lbClienteSel.Text = oConteo.NomCliente;
            txCantidad.Text = oConteo.LineaEnCurso.Cantidad.ToString();
            lbFamSel.Text = oConteo.Familia;
            lbSubfamSel.Text = oConteo.Subfamilia;
            lbDescProdSelec.Text = oConteo.LineaEnCurso.NomProducto;

            gridConteo.DataSource = oConteo.LineaEnCurso.Datos;
            gridConteo.DataBind();
        }
                
        private int ObtenerInventProdAlm()
        {
            cProduccion oProduccion = new cProduccion(Session["empresaLogin"].ToString());
            return (oProduccion.GetItemLocationInventory(oConteo.CodProducto, oConteo.CodAlmacen));
        }
        
        private void GenerarNuevoConteo()
        {
            int tipoConteo=int.Parse(Request.QueryString["Tipo"].ToString());
            oConteo = null;
            oConteo = new cConteo(Session["empresaLogin"].ToString(),tipoConteo);

            Page.Title = oConteo.PageTitle;

            oSurtidoSAL = null;
            LimpiarSurtidoCliente();           
        }

        private void AgregarLinea()
        {
            oConteo.AddLinea();
            MostrarDatosClase();
        }

    }
}
