using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApp_ExtranetCompras.Negocio;
using Telerik.Web.UI;
using System.Drawing;

namespace WebApp_ExtranetCompras
{
    public partial class _Default : System.Web.UI.Page
    {
        private cQuote oQuote;

        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                oQuote = new cQuote();

                LoadComboBox();
                LoadQuote(Session["vendorCode"].ToString());
                MostrarDatosOferta();
            }
            else
            {
                RecuperarVariableSesion();
                
            }

            MostrarTextBoxDataGrid();            
        }

        #region CARGA DE DATOS

            private void LoadQuote(string vendorCode)
            {
                try
                {
                    proxyEntidades proxy = new proxyEntidades("999 INDUSAL GRUPO CONSOLIDADO");
                    cQuote newQuote = proxy.GetVendorQuote(vendorCode);
                    newQuote.ShipmentMethods = oQuote.ShipmentMethods;
                    newQuote.PaymentMethods = oQuote.PaymentMethods;
                    Session["cQuote"] = newQuote;
                }
                catch (Exception)
                { }
            }

            private void LoadComboBox()
            {
                proxyEntidades proxy = new proxyEntidades("999 INDUSAL GRUPO CONSOLIDADO");
                LoadShipmentMethods(proxy);
                LoadPaymentMethods(proxy);
                LoadRappelConcept();                
            }

            private void LoadShipmentMethods(proxyEntidades proxy)
            {
                oQuote.ShipmentMethods = proxy.GetShipmentMethods("");
                GuardarVariableSesion();
                ddlPortes.DataSource = oQuote.ShipmentMethods;
                ddlPortes.DataTextField = "descripcion";
                ddlPortes.DataValueField = "codigo";
                ddlPortes.DataBind();                
                ddlPortes.Items.Add("");
                ddlPortes.SelectedIndex = ddlPortes.Items.Count - 1;
            }

            private void LoadPaymentMethods(proxyEntidades proxy)
            {
                oQuote.PaymentMethods = proxy.GetPaymentMethods("");
                GuardarVariableSesion();
                ddlFPago.DataSource = oQuote.PaymentMethods;
                ddlFPago.DataTextField = "Descripcion";
                ddlFPago.DataValueField = "Codigo";
                ddlFPago.DataBind();
                ddlFPago.Items.Add("");
                ddlFPago.SelectedIndex = ddlFPago.Items.Count - 1;
            }

            private void LoadRappelConcept()
            {
                ListItem newItem;
                
                newItem = new ListItem("FIJO", "FIJO");
                ddlTipoRappel.Items.Add(newItem);

                newItem = new ListItem("POR VOLUMEN", "POR VOLUMEN");
                ddlTipoRappel.Items.Add(newItem);

                newItem = new ListItem("");
                ddlTipoRappel.Items.Add(newItem);

                ddlTipoRappel.SelectedIndex = ddlTipoRappel.Items.Count - 1;
            }

        #endregion

        #region EVENTOS

            protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
            {                
                switch (e.Item.Text)
                {
                    case "Guardar":
                        GuardarDatosOferta();
                        break;
                    case "Archivar":                        
                        ArchivarOferta();
                        break;
                    case "Deshacer":
                        MostrarDatosOferta();
                        break;
                    case "Versiones":                        
                        Response.Redirect("Archivo.aspx");
                        break;
                }
            }

            protected void ddlPortes_SelectedIndexChanged(object sender, EventArgs e)
            {
                oQuote.ShipmentMethodCode = ddlPortes.Items[ddlPortes.SelectedIndex].Value;
            }

            protected void ddlTipoRappel_SelectedIndexChanged(object sender, EventArgs e)
            {
                oQuote.RappelConcept = ddlTipoRappel.Items[ddlTipoRappel.SelectedIndex].Value; ;
            }

            protected void ddlFPago_SelectedIndexChanged(object sender, EventArgs e)
            {
                oQuote.PaymentMethodCode = ddlFPago.Items[ddlFPago.SelectedIndex].Value;
            }

            protected void rntxPorcenRappel_TextChanged(object sender, EventArgs e)
            {
                oQuote.RappelPercent = decimal.Parse(rntxPorcenRappel.Text);
            }

            protected void rtxComentario_TextChanged(object sender, EventArgs e)
            {
                oQuote.Comment = rtxComentario.Text;
                rtxComentario.ToolTip = rtxComentario.Text;
            }


        #endregion

        #region ACCIONES

            private void MostrarDatosOferta()
            {
                try
                {
                    RecuperarVariableSesion();
                    
                    ddlPortes.SelectedValue = ddlPortes.Items.FindByValue(oQuote.ShipmentMethodCode).Value;
                    ddlTipoRappel.SelectedValue = ddlTipoRappel.Items.FindByValue(oQuote.RappelConcept).Value;
                    rntxPorcenRappel.Text = oQuote.RappelPercent.ToString();
                    ddlFPago.SelectedValue = ddlFPago.Items.FindByValue(oQuote.PaymentMethodCode).Value;
                    rtxComentario.Text = oQuote.Comment;
                    rtxComentario.ToolTip = rtxComentario.Text;

                    rg_QuoteLines.DataSource = oQuote.Lineas;
                    rg_QuoteLines.DataBind();
                }
                catch (Exception)
                { }
            }

            private void GuardarDatosOferta()
            {
                proxyEntidades proxy = new proxyEntidades("999 INDUSAL GRUPO CONSOLIDADO");
                proxy.UpdateQuote(oQuote);
            }

            private void ArchivarOferta()
            {
                try
                {
                    GuardarDatosOferta();
                    proxyEntidades proxy = new proxyEntidades("999 INDUSAL GRUPO CONSOLIDADO");
                    proxy.ArchiveDocument(oQuote.NDocument);
                }
                catch (Exception)
                { }
            }

            private void MostrarTextBoxDataGrid()
            {
                RadTextBox editText;                

                foreach (GridItem e in rg_QuoteLines.Items)
                {                    
                    if ((e.ItemType == Telerik.Web.UI.GridItemType.Item) ||
                        (e.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem))
                    {
                        e.Cells[6].Controls.Add(new RadTextBox());
                        editText = (RadTextBox)e.Cells[6].Controls[0];
                        editText.ID=e.Cells[2].Text+";0";                        
                        editText.TextChanged += new EventHandler(editText_TextChanged);                        
                        editText.AutoPostBack = true;
                        editText.Text = e.Cells[6].Text;

                        e.Cells[7].Controls.Add(new RadTextBox());
                        editText = (RadTextBox)e.Cells[7].Controls[0];
                        editText.ID = e.Cells[2].Text + ";1";                        
                        editText.TextChanged += new EventHandler(editText_TextChanged);
                        editText.AutoPostBack = true;
                        editText.Text = e.Cells[7].Text;

                        e.Cells[8].Controls.Add(new RadTextBox());
                        editText = (RadTextBox)e.Cells[8].Controls[0];
                        editText.ID = e.Cells[2].Text + ";2";                        
                        editText.TextChanged += new EventHandler(editText_TextChanged);
                        editText.AutoPostBack = true;
                        editText.Text = e.Cells[8].Text;                                               
                    }
                }
            }

            protected void editText_TextChanged(object sender, EventArgs e)
            {
                RadTextBox editText = (RadTextBox)sender;
                string[] controlID = editText.ID.Split(';');
                oQuote.UpdateLine(int.Parse(controlID[0]),int.Parse(controlID[1]),editText.Text);
            }            
                    
        #endregion            
                    
        #region SESSION

            private void GuardarVariableSesion()
            {
                Session["cQuote"] = oQuote;
            }

            private void RecuperarVariableSesion()
            {
                oQuote = (cQuote)Session["cQuote"];
            }

        #endregion

    }
}
