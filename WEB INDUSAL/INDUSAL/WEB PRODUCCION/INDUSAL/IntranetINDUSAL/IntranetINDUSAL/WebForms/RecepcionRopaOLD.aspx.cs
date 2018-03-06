using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IntranetINDUSAL
{
    public partial class RecepcionRopa : System.Web.UI.Page
    {
        #region Variables Privadas
            static cRecepcion oRecepcion;   //objeto que contiene los datos de la recepción en curso
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            // Cada vez que se carga la página, se actualiza la fecha mostrada con la del sistema
            rtxFecha.Text = DateTime.Now.ToShortDateString();
            // se crea la instancia del objeto recepcion
            if (!IsPostBack)
            {
                // en la primera carga, se instancia el objeto recepcion 
                // y se establece el origen de datos del Grid
                oRecepcion = new cRecepcion();
                RefreshGrid();
            }
        }

        #region Eventos de Controles

            protected void btAceptar_Click(object sender, EventArgs e)
        {
            if (ComprobarDatos())
            {
                AddReceptLine();
                InitQtys();
                ActivarBotonesRecepcion(true);
                RefreshGrid();
            }
        }

            protected void rgLinRecepcion_DeleteCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            //oRecepcion.DelRecepLine(rgLinRecepcion.Columns[0]);
            oRecepcion.DeleteLine(1);
            RefreshGrid();
        }

            protected void btRegRecepcion_Click(object sender, EventArgs e)
        {
            oRecepcion.Registrar(Session["empresaLogin"].ToString());
            InitRecept();
            ActivarBotonesRecepcion(false);
            RefreshGrid();
        }

            protected void btCancelar_Click(object sender, EventArgs e)
        {
            InitQtys();
        }

            protected void btCancelRecep_Click(object sender, EventArgs e)
        {
            InitRecept();
            ActivarBotonesRecepcion(false);
            RefreshGrid();
        }

        #endregion

        #region Metodos Privados

        private bool ComprobarDatos()
        {
            if (ddlTransportistas.SelectedItem == null)
                return (false);
            if (ddlRutasTransportista.SelectedItem == null)
                return (false);
            if (ddlClientesRuta.SelectedItem == null)
                return (false);

            if (rNtxEntregado.Text == "" && rNtxRecogido.Text == "")
                return (false);

            return (true);
        }

        private void AddReceptLine()
        {
            cLineasRecepcion oLinea = new cLineasRecepcion();
            oLinea.fecha = DateTime.Parse(rtxFecha.Text);
            oLinea.codTta = ddlTransportistas.SelectedItem.Value;
            oLinea.nomTta = ddlTransportistas.SelectedItem.Text;
            oLinea.codRuta = ddlRutasTransportista.SelectedItem.Value;
            oLinea.nomRuta = ddlRutasTransportista.SelectedItem.Text;
            oLinea.codCte = ddlClientesRuta.SelectedItem.Value;
            oLinea.nomCte = ddlClientesRuta.SelectedItem.Text;
            oLinea.cantEnt = rNtxEntregado.Text;
            oLinea.cantRec = rNtxRecogido.Text;
            oLinea.peso = rNtxPeso.Text;

            oRecepcion.AddLine(oLinea);
        }

        private void RefreshGrid()
        {
            rgLinRecepcion.DataSource = oRecepcion.GetLines();
            rgLinRecepcion.DataBind();
        }

        private void InitQtys()
        {
            rNtxEntregado.Text = "";
            rNtxRecogido.Text = "";
            rNtxPeso.Text = "";
        }

        private void InitRecept()
        {
            oRecepcion = null;
            oRecepcion=new cRecepcion();
        }

        private void ActivarBotonesRecepcion(Boolean valor)
        {
            btRegRecepcion.Enabled = valor;
            btCancelRecep.Enabled = valor;
        }

        #endregion
        
    }
}
