using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IntranetINDUSAL.Controles_Personalizados;

namespace IntranetINDUSAL.MasterForms
{
    public partial class MenuIntranetVerticalBotones : System.Web.UI.MasterPage
    {        

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                lbEmpresa.Text = Session["empresaLogin"].ToString();
            }
            catch
            { }
            
            lbFecha.Text = DateTime.Today.ToShortDateString();
            lbHora.Text = DateTime.Now.ToShortTimeString();

            if (!IsPostBack)
            {
                VisualizarMenus();
                MostrarTitulo();
            }

        }

        protected void btMenu_Click(object sender, EventArgs e)
        {
            Button btSender = (Button)sender;

            switch (btSender.CommandName)
            {
                case "RECEPCION":
                    Session["TituloForm"] = "RECEPCION DE ROPA";
                    Session["menuVisible"] = false;
                    Response.Redirect("~/WebForms/RecepcionRopa.aspx");                    
                    break;
                case "DESAPRESTADO":
                    Session["TituloForm"] = "DESAPRESTADO DE ROPA";
                    Session["menuVisible"] = false;
                    Response.Redirect("~/WebForms/ConteoRopaNuevo.aspx?Tipo=2");
                    break;
                case "UNIFORMIDAD":
                    panelMenuUniformidad.Visible = true;
                    panelMenuPrincipal.Visible = false;
                    break;
                case "CONTEO":
                    Session["TituloForm"] = "CONTEO DE ROPA";
                    Session["menuVisible"] = false;
                    Response.Redirect("~/WebForms/ConteoRopaAuto.aspx");
                    break;
                case "LAVADO":
                    panelMenuLavado.Visible = true;
                    panelMenuPrincipal.Visible = false;
                    break;
                case "PLEGADO":
                    panelMenuPlegado.Visible = true;
                    panelMenuPrincipal.Visible = false;
                    break;
                case "RECHAZO":
                    panelMenuRechazo.Visible = true;
                    panelMenuPrincipal.Visible = false;
                    //Session["TituloForm"] = "RECHAZO DE ROPA";
                    //Session["menuVisible"] = false;
                    //Response.Redirect("~/WebForms/Rechazo.aspx?Tipo=0");
                    break;
                case "PESO":
                    Session["TituloForm"] = "PESO";
                    Session["menuVisible"] = false;
                    Response.Redirect("~/WebForms/RegistroEmpaquetado.aspx?Tipo=0");
                    break;
                case "EXPEDICION":
                    Session["TituloForm"] = "EXPEDICION DE ROPA";
                    Session["menuVisible"] = false;
                    Response.Redirect("~/WebForms/RegistroEmpaquetado.aspx?Tipo=1");
                    break;
                case "ENT_UNIFORMIDAD":
                    Session["TituloForm"] = "UNIFORMIDAD: ENTRADA DE ROPA";
                    Session["menuVisible"] = false;
                    Response.Redirect("~/WebForms/RopaIdentificada.aspx?Tipo=0");
                    break;
                case "SAL_UNIFORMIDAD":
                    Session["TituloForm"] = "UNIFORMIDAD: SALIDA DE ROPA";
                    Session["menuVisible"] = false;
                    Response.Redirect("~/WebForms/RopaIdentificada.aspx?Tipo=1");
                    break;
                case "LAVADORA":
                    Session["TituloForm"] = "LAVADO: LAVADORAS";
                    Session["menuVisible"] = false;
                    Response.Redirect("~/WebForms/Lavadora.aspx?Tipo=0");
                    break;
                case "TUNEL":
                    Session["TituloForm"] = "LAVADO: TUNELES DE LAVADO";
                    Session["menuVisible"] = false;
                    Response.Redirect("~/WebForms/Lavadora.aspx?Tipo=1");
                    break;
                case "CALANDRA":
                    Session["TituloForm"] = "PLEGADO: CALANDRAS";
                    Session["menuVisible"] = false;
                    Response.Redirect("~/WebForms/PlegadoNuevo.aspx?Tipo=4");
                    break;
                case "FELPA":
                    Session["TituloForm"] = "PLEGADO: FELPA";
                    Session["menuVisible"] = false;
                    Response.Redirect("~/WebForms/PlegadoNuevo.aspx?Tipo=5");
                    break;
                case "FORMA":
                    Session["TituloForm"] = "PLEGADO: ROPA DE FORMA";
                    Session["menuVisible"] = false;
                    Response.Redirect("~/WebForms/PlegadoNuevo.aspx?Tipo=6");
                    break;
                case "RECHAZAR":
                    Session["TituloForm"] = "RECHAZO DE ROPA";
                    Session["menuVisible"] = false;
                    Response.Redirect("~/WebForms/Rechazo.aspx?Tipo=0");
                    break;
                case "COSTURA":
                    Session["TituloForm"] = "SALIDA DE COSTURA";
                    Session["menuVisible"] = false;
                    Response.Redirect("~/WebForms/SalidaCostura.aspx");
                    break;
                //case "OXIDO":
                //    Session["menuVisible"] = false;
                //    Response.Redirect("~/WebForms/ConteoRopaNuevo.aspx?Tipo=1");
                //    break;
                case "ETIQUETAS":
                    panelMenuEtiquetas.Visible = true;
                    panelMenuPrincipal.Visible = false;
                    break;
                case "ETIQUETA":
                    Session["TituloForm"] = "IMPRESION MANUAL DE ETIQUETAS";
                    Session["menuVisible"] = false;
                    Response.Redirect("~/WebForms/Etiquetas.aspx?Tipo="+btSender.CommandArgument);
                    break;
                case "MENU":
                    panelMenuLavado.Visible = false;
                    panelMenuPlegado.Visible = false;
                    panelMenuRechazo.Visible = false;
                    panelMenuEtiquetas.Visible = false;
                    panelMenuPrincipal.Visible = true;
                    break;
                case "OCULTAR":
                    Session["menuVisible"] = false;
                    VisualizarMenus();
                    break;
                case "MOSTRAR":
                    try
                    {
                        Session["menuVisible"] = (Session["empresaLogin"].ToString() != "");
                    }
                    catch
                    {
                        Session["menuVisible"] = false;
                    }                    
                    VisualizarMenus();
                    break;
                case "SALIR":
                    
                    break;
            }

        }

        private void VisualizarMenus()
        {
            bool menuVisible = bool.Parse(Session["menuVisible"].ToString());
            panelMenu.Visible = menuVisible;
            panelMostrarMenu.Visible = !menuVisible;
        }

        private void MostrarTitulo()
        {
            try
            {
                lbTitulo.Text = Session["TituloForm"].ToString();
            }
            catch 
            {}
        }
    }
}
