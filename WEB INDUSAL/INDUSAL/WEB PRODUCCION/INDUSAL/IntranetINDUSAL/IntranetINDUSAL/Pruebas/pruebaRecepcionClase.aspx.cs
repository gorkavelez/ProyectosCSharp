using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IntranetINDUSAL.Pruebas
{
    public partial class pruebaRecepcionClase : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            IntranetINDUSAL.Negocio.cPlegado oPlegado = (IntranetINDUSAL.Negocio.cPlegado)Session["objetoPlegado"];

        }
    }
}
