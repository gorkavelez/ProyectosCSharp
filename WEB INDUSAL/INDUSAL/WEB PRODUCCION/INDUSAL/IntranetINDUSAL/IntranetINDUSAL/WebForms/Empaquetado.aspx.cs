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
    public partial class Empaquetado : System.Web.UI.Page
    {
        static cEmpaquetado oEmpaquetado;        
        static cClientes oClientes;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.INIKER_teclado.OKClick +=
                   new INIKER_tecladoNumerico.OKClickHandler(INIKER_teclado_OKClick);

            if (!IsPostBack)
            {                
                GetClientes();                
            }

            MostrarBotonesFacturacion();
        }

        protected void INIKER_teclado_OKClick(object sender, INIKER_tecladoNumerico.OKClickEventArgs e)
        {
        }

        private void MostrarBotonesFacturacion()
        {
            //pnlCodsFact.Controls.Clear();

            //if (oExpedicion.CodCliente != "")
            //{
            //    cProductos oSurtido = new cProductos();
            //    DataTable dtSurtFact = oSurtido.GetCustomerReferences(oExpedicion.CodCliente, Session["empresaLogin"].ToString());

            //    DataRow[] surtidoFiltrado = dtSurtFact.Select("codFactProducto is not null");

            //    foreach (DataRow producto in surtidoFiltrado)
            //    {
            //        if (oExpedicion.ItemInOrder(producto["codProducto"].ToString()))
            //        {
            //            if (!ButtonExists(producto["codFactProducto"].ToString()))
            //            {
            //                pnlCodsFact.Controls.Add(
            //                    CreateButton(producto["desFactProducto"].ToString(),
            //                                    producto["codFactProducto"].ToString(),
            //                                    producto["codProducto"].ToString()));
            //            }
            //        }
            //    }
            //}
        }

        private bool ButtonExists(string pID)
        {
            Control oCtrl = pnlCodsFact.FindControl(pID);
            return (oCtrl != null);
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

        protected void FamilyButton_Click(object sender, EventArgs e)
        {
            // se captura el objeto que lanza el evento
            Button buttonSender = (Button)sender;
            // se almacena la información del producto que se va a pesar con los datos de la expedición
            // y se guarda el objeto en la sesión de usuario para recuperarlo desde el formulario
            // de registro de las pesadas de báscula
            //oExpedicion.CodFacturacion = buttonSender.ID;
            //oExpedicion.DescFacturacion = buttonSender.ToolTip;
            //Session["expedicionConteo"] = oExpedicion;
            // se carga la página de registro de pesajes
            Response.Redirect("~/Webforms/RegistroExpedicion.aspx");
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


        #region CLIENTES

            protected void txCodCliente_TextChanged(object sender, EventArgs e)
            {
                ObtenerNombreCliente(txCodCliente.Text);
                //MostrarDatosClase();
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

                    //oLavado.PesajeEnCurso.CodCliente = cliente;
                    //oLavado.PesajeEnCurso.NomCliente = oClientes.Alias;
                    //se obtienen los pedidos abiertos del cliente en el sistema
                    GetOrders(cliente);
                    //MostrarDatosClase();
                }
            }

            protected void ddlClientes_SelectedIndexChanged(object sender, EventArgs e)
            {
                ObtenerNombreCliente(ddlClientes.SelectedItem.Value);
            }

            private void GetOrders(string cliente)
            {
                cProduccion oProduccion = new cProduccion(Session["empresaLogin"].ToString());
                //oLavado.Pedidos = oProduccion.GetCustomerOrders(cliente);
            }

        #endregion



    }
}
