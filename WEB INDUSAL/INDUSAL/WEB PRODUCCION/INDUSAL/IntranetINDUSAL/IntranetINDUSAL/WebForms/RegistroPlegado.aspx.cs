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
    public partial class RegistroPlegado : System.Web.UI.Page
    {    
        
        static cPlegado oPlegado;        

        protected void Page_Load(object sender, EventArgs e)
        {
            GenerarScriptsConfirmacion();
            this.INIKER_teclado.OKClick +=
                   new INIKER_tecladoNumerico.OKClickHandler(INIKER_teclado_OKClick);
            if (!IsPostBack)
            {
                //en la carga inicial, recupera el objeto con los datos de "cabecera" para el registro
                RecuperarObjetoSession();        
            }            
        }

        private void GenerarScriptsConfirmacion()
        {
            btRegistrar.Attributes.Add("OnClick", "javascript:return ConfirmAction('registrar los datos');");
            btCancelar.Attributes.Add("OnClick", "javascript:return ConfirmAction('reiniciar el conteo');");
            btVolver.Attributes.Add("OnClick", "javascript:return DatosSinRegistrar();");
            
        }
        
        private void RecuperarObjetoSession()
        {
            oPlegado = (cPlegado)Session["objetoPlegado"];
            MostrarDatosClase();
        }

        private void MostrarDatosClase()
        {
            lbNomCliente.Text = oPlegado.NomCliente;
            lbCalandraSel.Text = oPlegado.DescMaquina;
            lbCodProd.Text = oPlegado.CodProducto;
            lbDescProd.Text = oPlegado.DescProducto;
            lbCantPaq.Text = oPlegado.UdsPorPaquete.ToString();
            txPaq.Text = oPlegado.Paquetes.ToString();
            txEtiqPaq.Text = oPlegado.Etiquetas.ToString();
            txUnidades.Text = oPlegado.Unidades.ToString();
            txUdsTotal.Text = oPlegado.UnidadesTotal.ToString();
            TxNSerie.Text = oPlegado.NSerie;
            // datos de las líneas de conteo
            gridConteo.DataSource = oPlegado.Lineas;
            gridConteo.DataBind();
            // se almacena en el control oculto html un valor que indique si existen
            // datos sin registrar, para poder acceder a él desde las funciones javascript
            hddDatosSinRegistrar.Value= oPlegado.DatosSinRegistrar().ToString();
        }

        protected void INIKER_teclado_OKClick(object sender, INIKER_tecladoNumerico.OKClickEventArgs e)
        {
            switch (hdDatoTeclado.Value)
            {
                case "paq":
                    oPlegado.Paquetes = int.Parse(e.Valor);
                    break;
                case "eti":
                    oPlegado.Etiquetas = int.Parse(e.Valor);
                    break;
                case "uds":
                    oPlegado.Unidades = int.Parse(e.Valor);
                    break;
            }
            // una vez actualizadas las propiedades de la clase, se muestran los datos
            MostrarDatosClase();
        }

        protected void btDato_Click(object sender, EventArgs e)
        {
            Button oSender = (Button)sender;
            // se captura el nombre del emisor
            hdDatoTeclado.Value = oSender.CommandName;
            // se parametriza el control teclado
            INIKER_teclado.TituloDato = oSender.Text;
            switch(oSender.CommandName)
            {
                case "paq":
                    //INIKER_teclado.Dato = oPlegado.Paquetes.ToString();
                    oPlegado.Paquetes = 1;
                    oPlegado.Etiquetas = 1;
                    MostrarDatosClase();
                    break;
                case "eti":
                    INIKER_teclado.Dato = oPlegado.Etiquetas.ToString();
                    break;
                case "uds":
                    INIKER_teclado.Dato = oPlegado.Unidades.ToString();
                    break;
            }
        }

        protected void btVolver_Click(object sender, EventArgs e)
        {
            Session["objetoPlegado"] = oPlegado;
            Response.Redirect("~/Webforms/Plegado.aspx?Tipo="+oPlegado.TipoPlanchadoToInt(oPlegado.TipoPlanchado).ToString());
        }

        protected void btAddLine_Click(object sender, EventArgs e)
        {            
            oPlegado.Add();            
            MostrarDatosClase();
        }

        protected void btRegistrar_Click(object sender, EventArgs e)
        {
            oPlegado.Register();
            MostrarDatosClase();
        }

        protected void gridConteo_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            oPlegado.NLinea =
                int.Parse(gridConteo.Rows[e.RowIndex].Cells[2].Text);
            oPlegado.Delete();
            MostrarDatosClase();
        }

        protected void gridConteo_SelectedIndexChanged(object sender, EventArgs e)
        {
            oPlegado.NLinea = 
                int.Parse(gridConteo.Rows[gridConteo.SelectedIndex].Cells[2].Text);
            oPlegado.Select();
            MostrarDatosClase();
        }

        protected void gridConteo_DataBound(object sender, EventArgs e)
        {
            //formateo la anchura de las columnas
            //gridConteo.Columns[0].ItemStyle.Width=50;
            //gridConteo.Columns[1].ItemStyle.Width = 50;
            //gridConteo.Columns[2].ItemStyle.Width = 50;
            //gridConteo.Columns[3].ItemStyle.Width = 150;
            //gridConteo.Columns[4].ItemStyle.Width = 50;
            //gridConteo.Columns[5].ItemStyle.Width = 50;
            //gridConteo.Columns[6].ItemStyle.Width = 50;

            for (int iRow = 0; iRow < gridConteo.Rows.Count; iRow++)
            {
                if (gridConteo.Rows[iRow].RowType == DataControlRowType.DataRow)
                {
                    try
                    {
                        gridConteo.Rows[iRow].Cells[0].Attributes.Add("onClick", "javascript:return ConfirmAction('eliminar el registro');");
                    }
                    catch (Exception ex)
                    { }

                    for (int iCell = 0; iCell < gridConteo.Rows[iRow].Cells.Count; iCell++)
                    {                        
                        try
                        {
                            Single valor = Single.Parse(gridConteo.Rows[iRow].Cells[iCell].Text);
                            gridConteo.Rows[iRow].Cells[iCell].HorizontalAlign = HorizontalAlign.Right;
                        }
                        catch (Exception ex)
                        { }
                    }
                }
            }
        }

        protected void btCancelar_Click(object sender, EventArgs e)
        {
            oPlegado.Clear();
        }
        
    }
}
