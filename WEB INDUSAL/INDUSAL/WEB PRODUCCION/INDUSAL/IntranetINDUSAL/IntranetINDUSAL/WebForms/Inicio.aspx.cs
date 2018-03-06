using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace IntranetINDUSAL.WebForms
{
    public partial class Inicio : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                IdentificacionInicial();
                //ProcesarLogin();                
            }
        }

        private void ProcesarLogin()
        {
            string usuarioCompleto;

            usuarioCompleto = CapturarUsuarioLogin();
            //ObtenerEmpresa(usuarioCompleto);
        }

        private string CapturarUsuarioLogin()
        {
            IntranetINDUSAL.Properties.Settings mySettings=new IntranetINDUSAL.Properties.Settings();

            //if (bool.Parse(mySettings.useDefCredts))
                return (mySettings.usuarioPruebas);
            //else
            //{
            //    string usuarioCompleto;
            //    usuarioCompleto = User.Identity.Name.ToString();
            //    //usuarioCompleto = usuarioCompleto.Substring(usuarioCompleto.IndexOf("\\") + 1);
            //    return (usuarioCompleto);                
            //}
        }

        //private void ObtenerEmpresa(string IDusuario)
        //{            
        //    string empresa;
        //    INIKER.FuncFabricacion.FuncFabricacion oLogin;
        //    IntranetINDUSAL.Properties.Settings mySettings=new IntranetINDUSAL.Properties.Settings();
         
        //    oLogin=new INIKER.FuncFabricacion.FuncFabricacion(mySettings.usuarioPruebas,mySettings.passwordPruebas);

        //    // Código para las pruebas de desarrollo
        //    //empresa=oLogin.LoginWeb(mySettings.dominioPruebas+mySettings.usuarioPruebas);
        //    // Código para el funcionamiento en producción
        //    //empresa = oLogin.LoginWeb(IDusuario);
        //    //Session["empresaLogin"] = empresa;
        //    Session["empresaLogin"] = "02 NAVARRA";
        //    //return(empresa);
        //}

        private bool IdentificacionEmpresa()
        {
            string nombreEmpresa = ReadCookie("empresa");
            if (nombreEmpresa != "")
            {
                Session["empresaLogin"] = nombreEmpresa;
                ddlEmpresas.SelectedIndex = ddlEmpresas.Items.IndexOf(ddlEmpresas.Items.FindByValue(nombreEmpresa));
                return (true);
            }
            else
            {                
                return (false);
            }
        }

        private void IdentificacionInicial()
        {
            //panelIdentificacion.Visible = !(IdentificacionEmpresa());
            panelIdentificacion.Visible = true;
            if (panelIdentificacion.Visible)
            {
                CargarListaEmpresas();
            }
            Session["menuVisible"] = false;
            //panelIdentificacion.Visible = false;
            //Session["empresaLogin"] = "06 OLIMPIA";
            //Session["menuVisible"] = true;
        }
       
        
        #region COOKIES IDENTIFICACION

        private void WriteCookie(string _name, string _value)
        {
            HttpCookie myCookie = new HttpCookie(_name, _value);
            myCookie.Expires = DateTime.Today.AddMonths(12);
            Response.Cookies.Add(myCookie);
        }

        private string ReadCookie(string _name)
        {
            string _cookieValue="";
            try
            {
                HttpCookie myCookie = Request.Cookies[_name];
                _cookieValue = myCookie.Value;
            }
            catch
            { }
            return (_cookieValue);
        }

    #endregion

        protected void ddlEmpresas_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["empresaLogin"] = ddlEmpresas.SelectedItem.Text;
            Session["dimensionesEtiqueta"] = ddlEtiquetas.Items[ddlEmpresas.SelectedIndex].Text;
            
            panelIdentificacion.Visible = false;            
            Session["menuVisible"] = true;
        }

        private void CargarListaEmpresas()
        {
            IntranetINDUSAL.Properties.Settings mySettings = new IntranetINDUSAL.Properties.Settings();            
            cProduccion oProduccion = new cProduccion(mySettings.empLogin);
            try
            {
                DataTable listaEmpresas = oProduccion.GetCompanyList();
                ddlEmpresas.DataSource = listaEmpresas;
                ddlEmpresas.DataValueField = listaEmpresas.Columns["nombre"].ToString();
                ddlEmpresas.DataTextField = listaEmpresas.Columns["nombre"].ToString();                
                ddlEmpresas.DataBind();
                // se carga el ddl que contiene el formato de etiquetas de cada empresa
                ddlEtiquetas.DataSource = listaEmpresas;
                ddlEtiquetas.DataValueField = listaEmpresas.Columns["nombre"].ToString();
                ddlEtiquetas.DataTextField = listaEmpresas.Columns["etiquetas"].ToString();
                ddlEtiquetas.DataBind();
            }
            catch { }
            finally
            {
                if (ddlEmpresas.Items.Count == 0)
                {
                    ddlEmpresas.Items.Add("HA FALLADO LA CARGA INICIAL");
                }
                ddlEmpresas.Items.Add("");
                ddlEmpresas.SelectedIndex = ddlEmpresas.Items.Count - 1;
            }
        }
 
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
