using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IntranetINDUSAL.WebForms
{
    public partial class Etiquetas : System.Web.UI.Page
    {
        static IntranetINDUSAL.Negocio.cEtiquetas oEtiquetas;    

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                oEtiquetas = new IntranetINDUSAL.Negocio.cEtiquetas("JCA", "Iniker1", "02 NAVARRA");
                oEtiquetas.ClienteProducto_120x80("000007", "RESIDENCIA AMMA HELLIN",
                    "FEP000001", "EMPAPADOR", 1);
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        { 

        }
    }
}
