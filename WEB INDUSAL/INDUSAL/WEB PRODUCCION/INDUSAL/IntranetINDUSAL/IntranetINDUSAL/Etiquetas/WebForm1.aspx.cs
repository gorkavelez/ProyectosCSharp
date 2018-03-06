using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IntranetINDUSAL.Etiquetas
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LanzarReport();            
        }

        private void LanzarReport()
        {
            dsEtiquetas _dsEtiquetas = new dsEtiquetas();
            DataTable _dtEtiquetas = _dsEtiquetas.Tables["dtEtiquetas"];
            DataRow newRow = _dtEtiquetas.NewRow();
            newRow["etiqueta"] = "clienteProducto";
            newRow["registro"] = 1;
            newRow["codCliente"] = "000007";
            newRow["nomCliente"] = "RESIDENCIA AMMA HELLIN";
            newRow["codProducto"] = "FEP000001";
            newRow["nomProducto"] = "EMPAPADOR";
            _dtEtiquetas.Rows.Add(newRow);            
        }
    }
}
