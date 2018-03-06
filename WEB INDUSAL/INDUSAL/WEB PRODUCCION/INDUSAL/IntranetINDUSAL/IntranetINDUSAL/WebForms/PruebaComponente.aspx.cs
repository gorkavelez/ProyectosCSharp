using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IntranetINDUSAL.Controles_Personalizados;

namespace IntranetINDUSAL.WebForms
{
    public partial class PruebaComponente : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.INIKER_surtido1.OKClick +=
                   new INIKER_surtido.OKClickHandler(INIKER_surtido_OKClick);

            if (!IsPostBack)
            {
                INIKER_surtido1.EmpresaLogin = "02 NAVARRA";
                INIKER_surtido1.CodCliente = "000000";
                INIKER_surtido1.Nivel = 2;
                INIKER_surtido1.CodFamilia = "F";
                INIKER_surtido1.DesFamilia = "FELPA";
                INIKER_surtido1.Load();
            }
        }

        protected void INIKER_surtido_OKClick(object sender, INIKER_surtido.OKClickEventArgs e)
        {
            Label1.Text = e.Descripcion;
        }

    }
}
