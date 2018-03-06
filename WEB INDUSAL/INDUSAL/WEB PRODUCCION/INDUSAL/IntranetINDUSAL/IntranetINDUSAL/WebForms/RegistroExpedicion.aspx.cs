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
    public partial class RegistroExpedicion : System.Web.UI.Page
    {    
        
        static cExpedicionConteo oExpedicion;

        protected void Page_Load(object sender, EventArgs e)
        {
            GenerarScriptsConfirmacion();
            this.INIKER_teclado.OKClick +=
                   new INIKER_tecladoNumerico.OKClickHandler(INIKER_teclado_OKClick);
            if (!IsPostBack)
            {
                //en la carga inicial, recupera el objeto con los datos de "cabecera" para el registro
                RecuperarObjetoSession();
                TraerCarrosSacas();
                oExpedicion.CargarMovsSinRegistrar();
            }            
        }

        private void TraerCarrosSacas()
        {
            cProductos oProductos = new cProductos();
            INIKER.Item.ItemList[] carros = oProductos.GetWagons(Session["empresaLogin"].ToString());
            //INIKER.Item.ItemList[] carros = oProductos.GetWagons("02 NAVARRA");                                
            ddlTiposCarro.DataSource = carros;
            ddlTiposCarro.DataTextField = INIKER.Item.ItemList_Fields.Search_Description.ToString();
            ddlTiposCarro.DataValueField = INIKER.Item.ItemList_Fields.No.ToString();
            ddlTiposCarro.DataBind();            
            ddlTiposCarro.Items.Add("");
            ddlTiposCarro.SelectedIndex = ddlTiposCarro.Items.Count - 1;
        }

        private void GenerarScriptsConfirmacion()
        {
            btRegistrar.Attributes.Add("OnClick", "javascript:return ConfirmAction('registrar los datos');");
            //btCancelar.Attributes.Add("OnClick", "javascript:return ConfirmAction('reiniciar el conteo');");
            btVolver.Attributes.Add("OnClick", "javascript:return DatosSinRegistrar();");            
        }


        private void RecuperarObjetoSession()
        {
            oExpedicion= (cExpedicionConteo)Session["expedicionConteo"];
            MostrarDatosClase();
        }

        private void MostrarDatosClase()
        {
            lbNomCliente.Text = oExpedicion.NomCliente;
            lbCodProd.Text = oExpedicion.CodFacturacion;
            lbDescProd.Text = oExpedicion.DescFacturacion;
            txNCarro.Text = oExpedicion.PesajeEnCurso.NCarro.ToString();
            ddlTiposCarro.SelectedIndex = ddlTiposCarro.Items.Count -1;
            ddlTiposCarro.Items.FindByValue(oExpedicion.PesajeEnCurso.CodCarro);
            txPeso.Text = oExpedicion.PesajeEnCurso.PesoCarro.ToString();            
            txCompleto.Text = oExpedicion.PesajeEnCurso.Completo ? "SI" : "NO";
            // datos de las líneas de conteo
            gridConteo.DataSource = oExpedicion.PesajeEnCurso.Lineas;
            gridConteo.DataBind();
            // se almacena en el control oculto html un valor que indique si existen
            // datos sin registrar, para poder acceder a él desde las funciones javascript
            hddDatosSinRegistrar.Value= oExpedicion.DatosSinRegistrar().ToString();            
        }

        protected void INIKER_teclado_OKClick(object sender, INIKER_tecladoNumerico.OKClickEventArgs e)
        {
            switch (hdDatoTeclado.Value)
            {
                case "peso":
                    oExpedicion.PesajeEnCurso.Peso = Single.Parse(e.Valor);
                    oExpedicion.PesajeEnCurso.PesoCarro = Single.Parse(e.Valor);
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
                case "peso":
                    //INIKER_teclado.Dato = oExpedicion.PesajeEnCurso.Peso.ToString();
                    INIKER_teclado.Dato = oExpedicion.PesajeEnCurso.PesoCarro.ToString();
                    break;                
            }
        }

        protected void btVolver_Click(object sender, EventArgs e)
        {
            Session["expedicionConteo"] = oExpedicion;
            Response.Redirect("~/Webforms/ExpedicionConteo.aspx");
        }

        protected void btAddLine_Click(object sender, EventArgs e)
        {
            oExpedicion.PesajeEnCurso.Add();
            MostrarDatosClase();
            gridConteo.SelectedIndex = -1;
        }

        protected void btRegistrar_Click(object sender, EventArgs e)
        {            
            INIKER.MovsProduccion.MovsProdSinRegINDUSAL[] lineas =oExpedicion.ProductionLinesToArray();
            if (lineas != null)
            {
                cProduccion oProduccion = new cProduccion(Session["empresaLogin"].ToString());
                oProduccion.RegisterProdMovs(lineas);
            }
        }

        protected void gridConteo_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //oPlegado.NLinea =
            //    int.Parse(gridConteo.Rows[e.RowIndex].Cells[2].Text);
            //oPlegado.Delete();
            //MostrarDatosClase();
        }

        protected void gridConteo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (oExpedicion.PesajeEnCurso.SelectByLine(int.Parse(gridConteo.Rows[gridConteo.SelectedIndex].Cells[2].Text)))
            {
                MostrarDatosClase();
            }

            //oPlegado.NLinea =
            //    int.Parse(gridConteo.Rows[gridConteo.SelectedIndex].Cells[2].Text);
            //oPlegado.Select();
            //MostrarDatosClase();
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
            //oPlegado.Clear();
        }

        protected void txNCarro_TextChanged(object sender, EventArgs e)
        {
            oExpedicion.PesajeEnCurso.NCarro = int.Parse(txNCarro.Text);
        }

        protected void ddlTiposCarro_SelectedIndexChanged(object sender, EventArgs e)
        {
            oExpedicion.PesajeEnCurso.CodCarro = ddlTiposCarro.SelectedItem.Value;
        }

        protected void txPeso_TextChanged(object sender, EventArgs e)
        {
            oExpedicion.PesajeEnCurso.Peso=Single.Parse(txPeso.Text);
        }

        protected void btCarroCompleto_Click(object sender, EventArgs e)
        {
            oExpedicion.PesajeEnCurso.Completo = !oExpedicion.PesajeEnCurso.Completo;
            MostrarDatosClase();
        }

        protected void txOperario_TextChanged(object sender, EventArgs e)
        {
        }
        
    }
}
