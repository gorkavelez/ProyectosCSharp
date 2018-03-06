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
    public partial class ConteoRopaAuto : System.Web.UI.Page
    {
        private cConteo oConteo;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.INIKER_teclado.OKClick +=
                   new INIKER_tecladoNumerico.OKClickHandler(INIKER_teclado_OKClick);

            this.INIKER_surtidoCliente.OKClick +=
                new INIKER_surtido.OKClickHandler(INIKER_surtido_OKClick);

            txCodCliente.Attributes.Add("onfocus", "FocusScript('" + txCodCliente.ClientID + "')");
            btCaptura.OnClientClick = "javascript:ConectarCanalConteo('" + hdfConteo.ClientID + "')";
            btCapturaTodos.OnClientClick = "javascript:ConectarCanalConteo('" + hdfConteoTodos.ClientID + "')";
            // código para realizar pruebas de captura de datos, sin estar conectados al autómata
            // el control txtDatosPruebaCanales, se deja oculto
            //btCapturaTodos.OnClientClick = "javascript:ConectarCanalConteoPrueba('" + hdfConteoTodos.ClientID + "','" + txtDatosPruebaCanales.Text + "')";

            if (!IsPostBack)
            {
                //GetClientes();
                GenerarNuevoConteo();
                // GUION
                txCodCliente.Focus();
            }
            else
            {
                oConteo = (cConteo)Session["cConteo"];
            }
        }

        #region EVENTOS CONTROLES

            protected void INIKER_teclado_OKClick(object sender, INIKER_tecladoNumerico.OKClickEventArgs e)
            {
                switch (oConteo.DatoTeclado)
                {
                    case cConteo.eDatoTeclado.cliente:
                        SeleccionarCliente(e.Valor);
                        break;
                    case cConteo.eDatoTeclado.operario:
                        SeleccionarOperario(e.Valor);
                        break;
                }
                
                panelDatos.Enabled = true;
                panelTeclado.Enabled = false;
                //MostrarDatosClase();
            }

            protected void INIKER_surtido_OKClick(object sender, INIKER_surtido.OKClickEventArgs e)
            {
                oConteo.CanalesConteoAuto.CodProducto = e.Codigo;
                oConteo.CanalesConteoAuto.NomProducto = e.Descripcion;
                oConteo.CanalesConteoAuto.Update();
                panelSurtido.Visible = false;
                MostrarCanalesConteo();
            }

            protected void gridConteo_RowDeleting(object sender, GridViewDeleteEventArgs e)
            {
                oConteo.LineaEnCurso.NLinea = int.Parse(gridConteo.Rows[e.RowIndex].Cells[2].Text.ToString());
                oConteo.DelLinea();
                MostrarDatosClase();
            }

            protected void btRegistrar_Click(object sender, EventArgs e)
            {
                try
                {
                    oConteo.Register();
                }
                catch (Exception ex)
                {
                    MostrarMensaje(ex.Message.Replace("'", ""));
                }

                MostrarDatosClase();
            }

            protected void gridConteo_SelectedIndexChanged(object sender, EventArgs e)
            {
                oConteo.LineaEnCurso.NLinea = int.Parse(gridConteo.Rows[gridConteo.SelectedIndex].Cells[2].ToString());
                oConteo.SelectLinea();
                MostrarDatosClase();
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

            protected void btConteoManual_Click(object sender, EventArgs e)
            {
                IrAConteoManual();
            }

            protected void btDato_Click(object sender, EventArgs e)
            {
                Button oSender = (Button)sender;
                //INIKER_teclado.TituloDato = oSender.Text;                
                //panelTeclado.Enabled = true;
                //panelDatos.Enabled = false;
                SetKeyBData(oSender.CommandName, oSender.Text);
            }

        #endregion

        #region SURTIDO

            private void MostrarSurtidoCliente(string cliente)
            {
                INIKER_surtidoCliente.EmpresaLogin = oConteo.Empresa;
                INIKER_surtidoCliente.CodCliente = cliente;
                INIKER_surtidoCliente.DesFamilia = "";
                INIKER_surtidoCliente.DesSubfamilia = "";
                INIKER_surtidoCliente.Nivel = 2;
                INIKER_surtidoCliente.Load();
            }

        #endregion


        #region CLIENTES

            protected void txCodCliente_TextChanged(object sender, EventArgs e)
            {
                txCodCliente.Text = txCodCliente.Text.Replace("*", "");
                SeleccionarCliente(txCodCliente.Text);
            }
        
            protected void ddlClientes_SelectedIndexChanged(object sender, EventArgs e)
            {
                SeleccionarCliente(ddlClientes.SelectedItem.Value);
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
                                        
                    oConteo.CodCliente = codigo;
                    oConteo.NomCliente = data[0];
                                        
                    MostrarSurtidoCliente(codigo);
                    MostrarDatosClase();
                    MostrarCanalesConteo();                    

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
 

        #endregion        


        #region CANALES DE CONTEO

            private bool GetCanalesAutomatizado()
            {                
                //carga los posibles canales de conteo y si no tiene ninguno redirige al conteo sin canales
                cMaquinas oCanalesConteo = new cMaquinas(Session["empresaLogin"].ToString(), "CANAL");
                // si se recupera información de canales, se carga en el objeto de conteo                
                return (oCanalesConteo.NMaquinas != 0);                
            }

            private void AddCanalesToConteo()
            {
                cMaquinas oCanalesConteo = new cMaquinas(Session["empresaLogin"].ToString(), "CANAL");

                foreach (DataRow drCanal in oCanalesConteo.tablaMaquinas.Rows)
                {
                    oConteo.CanalesConteoAuto.CodCanal = drCanal["codigo"].ToString();
                    oConteo.CanalesConteoAuto.NomCanal = drCanal["descripcion"].ToString();
                    oConteo.CanalesConteoAuto.Add();
                }
            }

            private void MostrarCanalesConteo()
            {
                //WCenter_CreateButtons();
                gridCanales.DataSource = oConteo.CanalesConteoAuto.Datos;
                gridCanales.DataBind();
                gridCanales.SelectedIndex = -1;
            }

            protected void gridCanales_SelectedIndexChanged(object sender, EventArgs e)
            {
                oConteo.CanalesConteoAuto.NLinea = int.Parse(gridCanales.Rows[gridCanales.SelectedIndex].Cells[3].Text);
                oConteo.CanalesConteoAuto.Select();                
                panelSurtido.Visible = true;
            }

            protected void gridCanales_RowDataBound(object sender, GridViewRowEventArgs e)
            {
                Button rowButton;

                e.Row.Cells[3].Visible = false; // nLinea
                e.Row.Cells[4].Visible = false; // codCanal
                e.Row.Cells[6].Visible = false; // codProducto

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    // Configurar - sólo cuando se ha especificado cliente
                    rowButton = (Button)e.Row.Cells[0].Controls[0];
                    rowButton.Enabled = ((oConteo.CodCliente != "")&&(oConteo.CodCliente != null));

                    // Seleccionar conteo - sólo cuando se ha configurado el canal con un producto
                    rowButton = (Button)e.Row.Cells[1].Controls[0];
                    rowButton.CommandArgument = e.Row.Cells[3].Text;
                    rowButton.Enabled = (e.Row.Cells[6].Text != "&nbsp;");

                    // Añadir conteo - cuando se ha configurado el canal y la cantidad es mayor que cero
                    rowButton = (Button)e.Row.Cells[2].Controls[0];
                    rowButton.CommandArgument = e.Row.Cells[3].Text;
                    rowButton.Enabled = ((e.Row.Cells[6].Text != "&nbsp;")&& (Int32.Parse(e.Row.Cells[8].Text) != 0));

                    //e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Right;
                    // si el canal no está configurado, pero al hacer una captura general de datos
                    // se recibe cantidad para ese canal, la cantidad se muestra en rojo y en negrita, 
                    // para avisar al usuario de que debe reiniciar el conteo de ese canal
                    if ((e.Row.Cells[6].Text == "&nbsp;") && (Int32.Parse(e.Row.Cells[8].Text) != 0))
                    {
                        e.Row.Cells[8].ForeColor = System.Drawing.Color.Red;
                        e.Row.Cells[8].Font.Bold = true;
                    }

                }
            }

            protected void gridCanales_RowCommand(object sender, GridViewCommandEventArgs e)
            {
                GridView grid = (GridView)sender;

                switch (e.CommandName)
                {
                    case "catch":
                        oConteo.CanalesConteoAuto.NLinea = int.Parse(e.CommandArgument.ToString());
                        oConteo.CanalesConteoAuto.Select();
                        break;
                    case "add":                        
                        AñadirCapturaAConteo(e.CommandArgument.ToString());
                        break;
                }

            }

            private int CapturarConteoCanal(int nCanal)
            {
                //cUDPProtocol oUDPProtocol = new cUDPProtocol();
                //return(oUDPProtocol.Get(nCanal));
                //return (5);
                try
                {
                    hdfConteo.Value = "";
                    CapturarConteo();
                    string[] datosCanales = hdfConteo.Value.Split(';');
                    //return (int.Parse(datosCanales[nCanal - 1]));
                    return (int.Parse(datosCanales[5]));
                }
                catch (Exception)
                {
                    return (-2);
                }                    
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

                oConteo.CodOperario = codigo;
                oConteo.NomOperario = nombre;
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

        private void MostrarDatosClase()
        {
            Session["cConteo"] = oConteo;

            txCodOperario.Text = oConteo.CodOperario;
            lbNomOperario.Text = oConteo.NomOperario;

            panelClienteConteo.Enabled = (oConteo.CodOperario != "");
            txCodCliente.Text = oConteo.CodCliente;
            lbNomCliente.Text = oConteo.NomCliente;
            //ddlClientes.SelectedIndex = ddlClientes.Items.IndexOf(ddlClientes.Items.FindByValue(oConteo.CodCliente));
            
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
            if (GetCanalesAutomatizado())
            {
                oConteo = null;
                oConteo = new cConteo(Session["empresaLogin"].ToString(), 0);
                Session["cConteo"] = oConteo;
                AddCanalesToConteo();
                MostrarCanalesConteo();
            }
            else
                IrAConteoManual();            
        }

        private void AñadirCapturaAConteo(string _nLinea)
        {
            oConteo.CanalesConteoAuto.NLinea = int.Parse(_nLinea);
            oConteo.CanalesConteoAuto.Select();
            if (oConteo.CanalesConteoAuto.Cantidad != 0)
            {
                oConteo.AddLineFromCanal(int.Parse(_nLinea));
                MostrarDatosClase();
                MostrarCanalesConteo();
            }
            else
            {
                MostrarMensaje("No se puede añadir un conteo de cero unidades");
            }
        }

        private void IrAConteoManual()
        {
            Response.Redirect("~/WebForms/ConteoRopaNuevo.aspx?Tipo=0");
        }

        private void MostrarMensaje(string mensaje)
        {
            string key = "status";
            string javascript = "MessageBox('" + mensaje + "');";

            if (!Page.ClientScript.IsStartupScriptRegistered(key))
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), key, javascript, true);
            }
        }

        private void CapturarConteo()
        {
            string key = "conteo";
            string javascript = "ConectarCanalConteo('" + hdfConteo.ClientID + "');";

            if (!Page.ClientScript.IsStartupScriptRegistered(key))
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), key, javascript, true);
            }
        }

        protected void hdfConteo_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                string[] valoresConteo = hdfConteo.Value.Split(';');
                string valorCaptura = valoresConteo[oConteo.CanalesConteoAuto.NLinea-1];                
                oConteo.CanalesConteoAuto.Cantidad = int.Parse(valorCaptura);
                oConteo.CanalesConteoAuto.Update();
            }
            catch(Exception)
            { }
            hdfConteo.Value = "#";
            MostrarCanalesConteo();
        }

        private void SetKeyBData(string commandName, string keybTitle)
        {
            switch (commandName)
            {
                case "operario":
                    oConteo.DatoTeclado = cConteo.eDatoTeclado.operario;                    
                    break;
                case "cliente":
                    oConteo.DatoTeclado = cConteo.eDatoTeclado.cliente;
                    break;
            }

            Session["cConteo"] = oConteo;

            INIKER_teclado.TituloDato = oConteo.DatoTeclado.ToString().ToUpper();            
            panelTeclado.Enabled = true;
            panelDatos.Enabled = false;
        }

        protected void hdfConteoTodos_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                string valorCaptura = "";
                string[] valoresConteo = hdfConteoTodos.Value.Split(';');
                foreach (DataRow canal in oConteo.CanalesConteoAuto.Datos.Rows)
                {
                    oConteo.CanalesConteoAuto.NLinea = int.Parse(canal["nLinea"].ToString());
                    oConteo.CanalesConteoAuto.Select();                    
                    valorCaptura = valoresConteo[oConteo.CanalesConteoAuto.NLinea - 1];
                    oConteo.CanalesConteoAuto.Cantidad = int.Parse(valorCaptura);                    
                    oConteo.CanalesConteoAuto.Update();                    
                }
            }
            catch (Exception)
            { }

            hdfConteo.Value = "#";
            MostrarCanalesConteo();
        }

    }
}
