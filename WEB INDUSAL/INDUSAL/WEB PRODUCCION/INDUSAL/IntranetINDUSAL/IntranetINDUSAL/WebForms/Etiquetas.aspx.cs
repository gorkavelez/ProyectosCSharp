using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IntranetINDUSAL.Negocio;
using System.Text;
using IntranetINDUSAL.Controles_Personalizados;
using IntranetINDUSAL.Reports;

namespace IntranetINDUSAL.WebForms
{
    public partial class Etiquetas : System.Web.UI.Page
    {
        //static cClientes oClientes;
        //static cPedidosVenta oPedidos;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.INIKER_teclado.OKClick +=
                   new INIKER_tecladoNumerico.OKClickHandler(INIKER_teclado_OKClick);

            this.INIKER_surtidoCliente.OKClick +=
                   new INIKER_surtido.OKClickHandler(INIKER_surtido_OKClick);

            txCodCliente.Attributes.Add("onfocus", "FocusScript('" + txCodCliente.ClientID + "')");

            if (!IsPostBack)
            {
                GetTipoEtiqueta();
                ViewPanels();                    
            }

            MostrarTitulo();            

        }

        #region EVENTOS CONTROLES
        
            protected void INIKER_teclado_OKClick(object sender, INIKER_tecladoNumerico.OKClickEventArgs e)
            {
                switch (datoTeclado.Value)
                {
                    case "cliente":
                        SeleccionarCliente(e.Valor);
                        break;
                    case "nCarro":
                        txNCarro.Text = e.Valor;
                        break;
                    case "nCopias":
                        txNCopias.Text = e.Valor;
                        int nCopias = int.Parse(e.Valor);
                        Imprimir(nCopias);
                        LimpiarControles();
                        break;
                }
                panelTeclado.Enabled = false;
            }

            protected void INIKER_surtido_OKClick(object sender, INIKER_surtido.OKClickEventArgs e)
            {
                txCodProducto.Text = e.Codigo;
                lbDescProducto.Text = e.Descripcion;
                panelSurtido.Visible = false;
            }

            protected void btDato_Click(object sender, EventArgs e)
            {
                Button oSender = (Button)sender;
                // se captura el nombre del emisor
                datoTeclado.Value = oSender.CommandName;
                // se parametriza el control teclado
                INIKER_teclado.TituloDato = oSender.Text;
                EjecutarAccion(oSender.Text);
                panelTeclado.Enabled = true;
            }

            protected void btProducto_Click(object sender, EventArgs e)
            {
                panelSurtido.Visible = true;
            }

        #endregion

        #region METODOS

            private void EjecutarAccion(string titulo)
            {
                switch (datoTeclado.Value)
                {
                    case "cliente":
                        INIKER_teclado.Dato = txCodCliente.Text;
                        break;
                    case "nCarro":
                        INIKER_teclado.Dato = txNCarro.Text;
                        break;
                    case "nCopias":
                        INIKER_teclado.Dato = txNCopias.Text;
                        break;
                }
            }

            private void MostrarTitulo()
            {
                switch (tipoEtiqueta.Value)
                {
                    case "1":
                        this.Title = "ETIQUETA TRANSPORTE";
                        break;
                    case "2":
                        this.Title = "ETIQUETA LAVADO";
                        break;
                    case "3":
                        this.Title = "ETIQUETA OXIDO/GRASA";
                        break;
                    case "4":
                        this.Title = "ETIQUETA CARRO INCOMPLETO";
                        break;
                    case "5":
                        this.Title = "ETIQUETA PAQUETE";
                        break;
                }
            }
            
            private void ActivarTeclado(Button sender)
            {
                datoTeclado.Value = sender.CommandName;
                INIKER_teclado.TituloDato = sender.Text;
                panelTeclado.Enabled = true;
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

            private void GetTipoEtiqueta()
            {
                tipoEtiqueta.Value = Request.QueryString["Tipo"].ToString();
            }

            private void ViewPanels()
            {
                switch (tipoEtiqueta.Value)
                {
                    case "1":   //TRANSPORTE
                        panelCliente.Visible = true;
                        panelPedido.Visible = false;
                        panelProducto.Visible = false;
                        panelSurtido.Visible = false;                        
                        panelCarro.Visible = false;                        
                        break;
                    case "2":   //LAVADO
                        panelCliente.Visible = true;
                        panelPedido.Visible = true;
                        panelProducto.Visible = false;
                        panelSurtido.Visible = false;
                        panelCarro.Visible = false;                        
                        break;
                    case "3":   //CARRO INCOMPLETO
                        panelCliente.Visible = true;
                        panelPedido.Visible = true;
                        panelProducto.Visible = false;
                        panelSurtido.Visible = false;
                        panelCarro.Visible = true;
                        break;
                    case "4":   //OXIDO/GRASA
                        panelCliente.Visible = false;
                        panelPedido.Visible = false;
                        panelProducto.Visible = false;
                        panelSurtido.Visible = false;                        
                        panelCarro.Visible = false;
                        break;
                    case "5":   //PAQUETE
                        panelCliente.Visible = true;
                        panelPedido.Visible = true;
                        panelProducto.Visible = true;
                        panelSurtido.Visible = false;
                        panelCarro.Visible = false;
                        break;
                }
                        
            }

            private void Imprimir(int copias)
            {
                //cPrintDocument oPrinter = new cPrintDocument(Session["empresaLogin"].ToString());
                cPrintDocument oPrinter = new cPrintDocument();
                //oPrinter.IdEquipo = Session["computerID"].ToString();
                //DLL_PRINTER.Printer oPrinter = new DLL_PRINTER.Printer();
                
                switch (tipoEtiqueta.Value)
                {
                    case "1":   // TRANSPORTE
                        oPrinter.CodCliente = txCodCliente.Text;
                        oPrinter.NomCliente = lbNomCliente.Text;
                        oPrinter.TipoDocumento= cPrintDocument.eTipoDocumento.carroTransporte;
                        //oPrinter.TipoDocumento = DLL_PRINTER.Printer.eTipoDocumento.carroTransporte; 
                        break;
                    case "2":   // LAVADO
                        oPrinter.CodCliente = txCodCliente.Text;
                        oPrinter.NomCliente = lbNomCliente.Text;
                        oPrinter.NumPedido = txNumPedido.Text;
                        oPrinter.TipoDocumento = cPrintDocument.eTipoDocumento.carroLavado;
                        //oPrinter.TipoDocumento = DLL_PRINTER.Printer.eTipoDocumento.carroLavado;
                        break;
                    case "3":   // CARRO INCOMPLETO
                        oPrinter.CodCliente = txCodCliente.Text;
                        oPrinter.NomCliente = lbNomCliente.Text;
                        oPrinter.NumPedido = txNumPedido.Text;
                        oPrinter.NCarro = int.Parse(txNCarro.Text);
                        oPrinter.TipoDocumento = cPrintDocument.eTipoDocumento.carroIncompleto;
                        //oPrinter.TipoDocumento = DLL_PRINTER.Printer.eTipoDocumento.carroIncompleto;
                        break;
                    case "4":
                        oPrinter.CodCliente = "000000";
                        oPrinter.NomCliente = "CLIENTE INDUSAL";
                        oPrinter.TipoDocumento = cPrintDocument.eTipoDocumento.carroOxido;
                        //oPrinter.TipoDocumento = DLL_PRINTER.Printer.eTipoDocumento.carroOxido;
                        break;
                    case "5":
                        oPrinter.CodCliente = txCodCliente.Text;
                        oPrinter.NomCliente = lbNomCliente.Text;
                        oPrinter.NumPedido = txNumPedido.Text;
                        oPrinter.CodProducto = txCodProducto.Text;
                        oPrinter.NomProducto = lbDescProducto.Text;
                        oPrinter.TipoDocumento = cPrintDocument.eTipoDocumento.paqueteProducto;
                        //oPrinter.TipoDocumento = DLL_PRINTER.Printer.eTipoDocumento.paqueteProducto;
                        break;
                }

                oPrinter.Print(copias);
                EjecutarScript(oPrinter.ArgumentString);
            }

            private void LimpiarControles()
            {
                txCodCliente.Text = "";
                lbNomCliente.Text = "";
                txNumPedido.Text = "";                
                txNCarro.Text = "";
                txNCopias.Text = "";
                txCodProducto.Text = "";
                lbDescProducto.Text="";
                ddlPedidos.Items.Clear();
                ddlPedidos.Visible = false;
                txNumPedido.Visible = true;
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
         
        #region CLIENTES

            protected void txCodCliente_TextChanged(object sender, EventArgs e)
            {
                txCodCliente.Text = txCodCliente.Text.Replace("*", "");
                SeleccionarCliente(txCodCliente.Text);
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

                    txCodCliente.Text = codigo;
                    //lbNomCliente.Text = data[0];
                    if (tipoEtiqueta.Value == "1")
                    {
                        lbNomCliente.Text = PrepararNombreCliente(data[0]);
                    }
                    else
                    {
                        lbNomCliente.Text = data[0];
                    }

                    if (tipoEtiqueta.Value != "1")
                    {
                        if (tipoEtiqueta.Value == "5")
                        {
                            MostrarSurtidoCliente(codigo);
                            panelSurtido.Visible = true;
                        }
                    }

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

        #region SURTIDO

            private void MostrarSurtidoCliente(string cliente)
            {
                INIKER_surtidoCliente.Reset();
                INIKER_surtidoCliente.EmpresaLogin = Session["empresaLogin"].ToString();
                INIKER_surtidoCliente.CodCliente = cliente;                
                INIKER_surtidoCliente.DesFamilia = "";
                INIKER_surtidoCliente.DesSubfamilia = "";
                INIKER_surtidoCliente.Nivel = 2;
                INIKER_surtidoCliente.Load();
            }

        #endregion

        #region PEDIDOS VENTA

            protected void btPedidos_Click(object sender, EventArgs e)
            {
                if (txCodCliente.Text != "")
                {
                    MostrarPedidos(txCodCliente.Text);
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

            protected void ddlPedidos_SelectedIndexChanged(object sender, EventArgs e)
            {                
                txNumPedido.Text = ddlPedidos.SelectedItem.Text;                
                ddlPedidos.Visible = false;
                txNumPedido.Visible = true;
            }


            protected void txNumPedido_TextChanged(object sender, EventArgs e)
            {                
                SeleccionarPedido(txNumPedido.Text);
            }
                    

            private void SeleccionarPedido(string pedido)
            {
                pedido = pedido.Replace("'", "-");

                cPedidosVenta oPedidos = new cPedidosVenta(Session["empresaLogin"].ToString(), txCodCliente.Text);
                if (oPedidos.ExistePedido(pedido))
                {
                    txNumPedido.Text = pedido.ToUpper(); ;                    
                    panelSurtido.Visible = (tipoEtiqueta.Value=="5");
                }
                else
                {
                    txNumPedido.Text = "";
                    panelSurtido.Visible = false;
                }                
            }

        #endregion


            private void MostrarMensaje(string mensaje)
            {
                string key = "status";
                string javascript = "MessageBox('" + mensaje + "');";

                if (!Page.ClientScript.IsStartupScriptRegistered(key))
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), key, javascript, true);
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
    }
}
