using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class recepcion : System.Web.UI.Page
{

    static bool validQtys;

    protected void Page_Load(object sender, EventArgs e)
    {
        // cada vez que se carga la página, se refresca la fecha
        rtxFecha.Text = DateTime.Now.ToShortDateString();

  }


    
}
