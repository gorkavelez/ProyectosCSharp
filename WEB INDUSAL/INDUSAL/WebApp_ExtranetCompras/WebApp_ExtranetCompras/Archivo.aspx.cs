using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApp_ExtranetCompras.Negocio;
using Telerik.Web.UI;

namespace WebApp_ExtranetCompras
{
    public partial class _Archivo : System.Web.UI.Page
    {
        private cQuote oQuote;
        private cArchiveQuote oArchQuote;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadVersions();               
            }
            
        }

        #region CARGA DE DATOS

            private void LoadVersions()
            {
                RecuperarVariableSesion();                                
                proxyEntidades proxy = new proxyEntidades("999 INDUSAL GRUPO CONSOLIDADO");
                oQuote.Versiones = proxy.GetQuoteVersions(oQuote.NDocument);
                rcbVersiones.DataSource = oQuote.Versiones;
                rcbVersiones.DataTextField = "FechaVersion";
                rcbVersiones.DataValueField = "Version";
                rcbVersiones.DataBind();
                RadComboBoxItem newItem = new RadComboBoxItem("");
                rcbVersiones.Items.Add(newItem);
                rcbVersiones.SelectedIndex = rcbVersiones.Items.Count - 1;
                GuardarVariableSesion();
            }

        #endregion

        #region EVENTOS
        
        #endregion

        #region ACCIONES

            private void MostrarDatosOferta()
            {
                lbPortes.Text = oArchQuote.GetDescription(oQuote.ShipmentMethods, oArchQuote.ShipmentMethodCode);
                lbPorcenRappel.Text = oArchQuote.RappelPercent.ToString();
                lbTipoRappel.Text = oArchQuote.RappelConcept;
                lbFPago.Text = oArchQuote.GetDescription(oQuote.PaymentMethods, oArchQuote.PaymentMethodCode);
                
                lbComentario.Text = oArchQuote.Comment;
                lbComentario.ToolTip = lbComentario.Text;

                rg_QuoteLines.DataSource = oArchQuote.Lineas;
                rg_QuoteLines.DataBind();                
            }

            private void GetQuoteArchiveData(string quoteNo_, int versionNo_)
            {
                proxyEntidades proxy = new proxyEntidades("999 INDUSAL GRUPO CONSOLIDADO");
                oArchQuote.Lineas = proxy.GetQuoteArchiveLines(quoteNo_,versionNo_);
                oArchQuote.Comment = proxy.GetQuoteArchiveComment(quoteNo_, versionNo_);
            }

        #endregion            

            protected void rcbVersiones_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
            {
                RecuperarVariableSesion();
                oArchQuote = oQuote.SelectVersion(int.Parse(e.Value));
                GetQuoteArchiveData(oArchQuote.NDocument, oArchQuote.Version);
                GuardarVariableSesionArchivo();
                MostrarDatosOferta();
            }

        #region SESSION

            private void GuardarVariableSesion()
            {
                Session["cQuote"] = oQuote;
            }

            private void RecuperarVariableSesion()
            {
                oQuote = (cQuote)Session["cQuote"];
            }

            private void GuardarVariableSesionArchivo()
            {
                Session["cArchQuote"] = oArchQuote;
            }

            private void RecuperarVariableSesionArchivo()
            {
                oArchQuote = (cArchiveQuote)Session["cArchQuote"];
            }

        #endregion

            protected void RadToolBar1_ButtonClick(object sender, RadToolBarEventArgs e)
            {
                proxyEntidades proxy;

                switch (e.Item.Text)
                {
                    case "Restaurar ...":
                        proxy = new proxyEntidades("999 INDUSAL GRUPO CONSOLIDADO");
                        RecuperarVariableSesionArchivo();
                        proxy.RestoreDocument(oArchQuote.NDocument, oArchQuote.Version);
                        Response.Redirect("Default.aspx");
                        break;
                    case "Volver":
                        Response.Redirect("Default.aspx");
                        break;
                }
            }
            
        
    }
}
