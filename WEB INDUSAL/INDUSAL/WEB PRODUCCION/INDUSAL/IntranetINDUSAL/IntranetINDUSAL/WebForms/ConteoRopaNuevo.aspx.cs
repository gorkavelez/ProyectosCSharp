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
    public partial class ConteoRopaNuevo : System.Web.UI.Page
    {
        private cConteo oConteo;        

        protected void Page_Load(object sender, EventArgs e)
        {
            this.INIKER_teclado.OKClick +=
                   new INIKER_tecladoNumerico.OKClickHandler(INIKER_teclado_OKClick);

            this.INIKER_surtidoCliente.OKClick +=
                   new INIKER_surtido.OKClickHandler(INIKER_surtido_OKClick);

            txCodCliente.Attributes.Add("onfocus", "FocusScript('" + txCodCliente.ClientID + "')");

            if (!IsPostBack)
            {
                GenerarNuevoConteo();

                switch (oConteo.TipoConteo)
                {
                    case cConteo.eTipoConteo.conteo:
                        panelClienteConteo.Visible = true;
                        panelClienteDesaprestado.Visible = false;
                        //SetKeybCustomer();
                        //txCodCliente.Focus();
                        txCodOperario.Focus();
                        break;
                    case cConteo.eTipoConteo.desaprestado:
                        panelClienteConteo.Visible = false;
                        panelClienteDesaprestado.Visible = true;
                        if (GetClienteDesaprestado())
                        {
                            MostrarSurtidoCliente(oConteo.CodCliente);
                            //panelSurtido.Visible = true;
                        }
                        MostrarDatosClase();
                        break;
                }
            }
            else
            {
                RecuperarVariableSesion();
            }

                    
        }

        protected void btDato_Click(object sender, EventArgs e)
        {
            Button oSender = (Button)sender;            
            SetKeyBData(oSender.CommandName, oSender.Text);
        }

        #region TECLADO NUMERICO

            protected void INIKER_teclado_OKClick(object sender, INIKER_tecladoNumerico.OKClickEventArgs e)
            {
                switch (oConteo.DatoTeclado)
                {
                    case cConteo.eDatoTeclado.cantidad:
                        oConteo.LineaEnCurso.Cantidad = int.Parse(e.Valor);
                        AgregarLinea();
                        gridConteo.SelectedIndex = -1;
                        btRegistrar.Enabled = true;
                        btReiniciar.Enabled = true;
                        btCancelar.Enabled = false;
                        txCodCliente.Focus();
                        break;
                    case cConteo.eDatoTeclado.cliente:
                        SeleccionarCliente(e.Valor);
                        break;
                    case cConteo.eDatoTeclado.operario:
                        SeleccionarOperario(e.Valor);
                        break;
                }

                PanelTeclado.Enabled = false;
                panelDatos.Enabled = true;
                MostrarDatosClase();                
                
            }

        #endregion

        #region SURTIDO

            protected void INIKER_surtido_OKClick(object sender, INIKER_surtido.OKClickEventArgs e)
            {
                oConteo.LineaEnCurso.CodProducto = e.Codigo;
                oConteo.LineaEnCurso.NomProducto = e.Descripcion;

                //if(oConteo.TipoConteo== cConteo.eTipoConteo.desaprestado)
                //    panelSurtido.Visible = false;

                MostrarDatosClase();
                SetQty("0");
            }

        #endregion

        #region GRID LINEAS

            protected void gridConteo_RowDeleting(object sender, GridViewDeleteEventArgs e)
            {
                oConteo.LineaEnCurso.NLinea = int.Parse(gridConteo.Rows[e.RowIndex].Cells[2].Text.ToString());
                oConteo.DelLinea();
                btRegistrar.Enabled = (oConteo.LineaEnCurso.Datos.Rows.Count != 0);
                btReiniciar.Enabled = btRegistrar.Enabled;
                MostrarDatosClase();
            }

            protected void gridConteo_SelectedIndexChanged(object sender, EventArgs e)
            {
                oConteo.LineaEnCurso.NLinea = int.Parse(gridConteo.Rows[gridConteo.SelectedIndex].Cells[2].Text.ToString());
                oConteo.SelectLinea();
                SetQty(gridConteo.Rows[gridConteo.SelectedIndex].Cells[6].Text);
                btCancelar.Enabled = true;
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

            private void MostrarSurtidoCliente(string cliente)
            {
                try
                {
                    INIKER_surtidoCliente.Reset();
                    INIKER_surtidoCliente.EmpresaLogin = oConteo.Empresa;
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

            protected void btCliente_Click(object sender, EventArgs e)
            {
                SetKeybCustomer();                
            }

            private void SetKeybCustomer()
            {
                oConteo.DatoTeclado = cConteo.eDatoTeclado.cliente;
                INIKER_teclado.TituloDato = "CLIENTE";
                PanelTeclado.Enabled = true;
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
            //    oConteo.CodCliente = oClientes.Codigo;
            //    oConteo.NomCliente = oClientes.Alias;
            //    oConteo.CodAlmacen = oClientes.Almacen;
            //    oClientes.SelectDropDownList(ref ddlClientes);
            //}

            //protected void ddlClientes_SelectedIndexChanged(object sender, EventArgs e)
            //{
            //    SeleccionarCliente(ddlClientes.SelectedItem.Value);                
            //}

            private void SeleccionarCliente(string codigo)
            {
                //GenerarNuevoConteo();
                                
                try
                {
                    // se rellena el código de cliente con ceros hasta completar el tamaño
                    if (codigo.Length < 6)
                        codigo = codigo.PadLeft(6, '0');

                    cProduccion oProduccion = new cProduccion(Session["empresaLogin"].ToString());
                    string[] data = oProduccion.GetCustomerData(codigo).Split(';'); ;
                    if (data[0] == "")
                        throw new Exception("No existe el cliente " + codigo);

                    //GenerarNuevoConteo();
                    oConteo.CodCliente = codigo;
                    oConteo.NomCliente = data[0];

                    MostrarSurtidoCliente(codigo);

                    MostrarDatosClase();

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

            private bool GetClienteDesaprestado()
            {
                cProduccion oProduccion = new cProduccion(Session["empresaLogin"].ToString());
                string custCode = oProduccion.GetInternalCustomer(cProduccion.eTipoCliente.DESAPRESTADO);
                if (custCode != "")
                {
                    oConteo.CodCliente = custCode;
                    string custData = oProduccion.GetCustomerData(custCode);
                    try
                    {
                        string[] custDataArray = custData.Split(';');
                        oConteo.NomCliente = custDataArray[0];
                        oConteo.CodAlmacen = custDataArray[1];
                        return (true);
                    }
                    catch 
                    {
                        return (false);
                    }
                }
                else
                    return (false);
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
        
        #region METODOS

            private void SetQty(string qty)
            {
                INIKER_teclado.TituloDato = "CANTIDAD";
                INIKER_teclado.Dato = qty;
                PanelTeclado.Enabled = true;
                oConteo.DatoTeclado = cConteo.eDatoTeclado.cantidad;
            }

            private void MostrarDatosClase()
            {
                GuardarVariableSesion();

                txCodOperario.Text = oConteo.CodOperario;
                lbNomOperario.Text = oConteo.NomOperario;

                panelClienteConteo.Enabled = (oConteo.CodOperario != "");

                txCodCliente.Text = oConteo.CodCliente;
                //ddlClientes.SelectedIndex = ddlClientes.Items.IndexOf(ddlClientes.Items.FindByValue(oConteo.CodCliente));
                lbNomCliente.Text = oConteo.NomCliente;
                lbCodCliente.Text = oConteo.CodCliente;
                lbDescCliente.Text = oConteo.NomCliente;

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
                int iTipoConteo = int.Parse(Request.QueryString["Tipo"].ToString());
                oConteo = null;
                oConteo = new cConteo(Session["empresaLogin"].ToString(), iTipoConteo);
                tipoConteo.Value = oConteo.TipoConteo.ToString().ToLower();
                GuardarVariableSesion();

                Page.Title = oConteo.PageTitle;
            }

            private void AgregarLinea()
            {
                oConteo.AddLinea();
                MostrarDatosClase();
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
                PanelTeclado.Enabled = true;
                panelDatos.Enabled = false;
            }

        #endregion

        #region ACCIONES

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

            protected void btAceptar_Click(object sender, EventArgs e)
            {
                AgregarLinea();
            }

            protected void btCancelRecep_Click(object sender, EventArgs e)
            {
                oConteo.ClearLineas();
                MostrarDatosClase();
            }

            protected void btCancelar_Click(object sender, EventArgs e)
            {
                gridConteo.SelectedIndex = -1;
                lbDescProdSelec.Text = "";
                PanelTeclado.Enabled = false;
                btCancelar.Enabled = false;
            }

        #endregion

        #region SESSION

            private void GuardarVariableSesion()
        {
            switch (tipoConteo.Value)
            {
                case "conteo":
                    Session["cConteo"] = oConteo;
                    break;
                case "desaprestado":
                    Session["cDesaprestado"] = oConteo;
                    break;
            }
        }

            private void RecuperarVariableSesion()
        {
            switch (tipoConteo.Value)
            {
                case "conteo":
                    oConteo = (cConteo)Session["cConteo"];
                    break;
                case "desaprestado":
                    oConteo = (cConteo)Session["cDesaprestado"];
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
        
        #endregion
    }
}
