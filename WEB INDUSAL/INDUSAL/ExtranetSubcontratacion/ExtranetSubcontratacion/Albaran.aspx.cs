using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ExtranetSubcontratacion.Negocio;

namespace ExtranetSubcontratacion
{
    public partial class Albaran : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Session["lavanderia"] = "02 NAVARRA";
            //Session["albaran"] = "AV10-02-01333";
            CargarDatosEmisor();
            CargarDatosEncabezadoAlbaran();
            CargarLineasAlbaran();
        }

        private void CargarDatosEmisor()
        {
            proxyEntidades proxy = new proxyEntidades(Session["lavanderia"].ToString());
            DataTable datos = proxy.GetCompanyInformation();
            lbDatosIndusal1.Text = datos.Rows[0]["name"].ToString();
            lbDatosIndusal2.Text = datos.Rows[0]["address"].ToString();
            lbDatosIndusal3.Text = datos.Rows[0]["post code"].ToString() + " - " + datos.Rows[0]["city"].ToString();
            lbDatosIndusal4.Text = datos.Rows[0]["county"].ToString();
            lbDatosIndusal5.Text = "Tfno: " + datos.Rows[0]["phone no."].ToString() + " - Fax: " + datos.Rows[0]["fax no."].ToString();
            lbDatosIndusal6.Text = datos.Rows[0]["home page"].ToString();
            lbDatosIndusal7.Text = "e-mail: " +datos.Rows[0]["e-mail"].ToString();
            lbDatosIndusal8.Text = "CIF/NIF: " + datos.Rows[0]["VAT registration no."].ToString();
        }

        private void CargarDatosEncabezadoAlbaran()
        {
            proxyEntidades proxy = new proxyEntidades(Session["lavanderia"].ToString());
            DataTable datos = proxy.GetSalesShipmentHeader(Session["albaran"].ToString());            
            lbDatosAlbaran1.Text = datos.Rows[0]["numero"].ToString();
            lbDatosAlbaran2.Text = datos.Rows[0]["fecha"].ToString();
            lbDatosAlbaran3.Text = datos.Rows[0]["pedido"].ToString();
            lbDatosAlbaran4.Text = datos.Rows[0]["cliente"].ToString();
            lbDatosAlbaran5.Text = datos.Rows[0]["direccion"].ToString();
            lbDatosAlbaran6.Text = datos.Rows[0]["direccion2"].ToString();
            lbDatosAlbaran7.Text = datos.Rows[0]["direccion3"].ToString();
            lbDatosAlbaran8.Text = "Att. de: " + datos.Rows[0]["contacto"].ToString();
        }

        private void CargarLineasAlbaran()
        {
            proxyEntidades proxy = new proxyEntidades(Session["lavanderia"].ToString());
            gridLineas.DataSource = proxy.GetSalesShptLines(Session["albaran"].ToString());            
            gridLineas.DataBind();            
        }

        protected void gridLineas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Height = 20;

            e.Row.Cells[0].Visible = false;

            e.Row.Cells[1].Width = 100;
            e.Row.Cells[2].Width = 420;
            e.Row.Cells[3].Width = 70;
            e.Row.Cells[4].Width = 100;

            e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Left;
            e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Center;    
            
        }       

    }
}
