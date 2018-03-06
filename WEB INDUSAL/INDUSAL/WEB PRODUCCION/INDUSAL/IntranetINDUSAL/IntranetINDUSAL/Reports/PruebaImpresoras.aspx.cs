using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IntranetINDUSAL.Reports;

namespace IntranetINDUSAL.Reports
{
    public partial class PruebaImpresoras : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {            
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            cPrintDocument oPrinter = new cPrintDocument();
            //DLL_PRINTER.Printer oPrinter = new Printer();
            oPrinter.CodCliente = "000270";
            oPrinter.NomCliente = "HOTEL TXIMISTA";
            oPrinter.NumPedido = "PV09-02-00109";
            oPrinter.CodProducto = "MSV000056";
            oPrinter.NomProducto = "SERVILLETA COMEDOR BLANCA 25x25";
            oPrinter.NCarro = "15";
            oPrinter.TipoDocumento = cPrintDocument.eTipoDocumento.paqueteProducto;
            oPrinter.Print(2);
        }
    }
}
