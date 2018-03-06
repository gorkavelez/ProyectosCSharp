using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IntranetINDUSAL.Automatizacion;

namespace IntranetINDUSAL.Automatizacion
{
    public partial class PruebaBascula : System.Web.UI.Page
    {
        

        protected void Page_Load(object sender, EventArgs e)
        {
            

        }

        protected void btPeso_Click(object sender, EventArgs e)
        {            
            cBascula bascula = new cBascula();            
            bascula.DataReceived+= new cBascula.DataReceivedHandler(bascula_DataReceived);
            
            try
            {
                //bascula.OpenCOM();
                txPeso.Text = bascula.Peso;
                //bascula.CloseCOM();
                //string msj = bascula.Read();    
                //txPeso.Text = msj;
            }
            catch (FormatException ex)
            {
                lbError.Text= ex.Message;
            }
                
            
        }

        protected void bascula_DataReceived(object sender, cBascula.DataReceivedEventArgs e)
        {
            txPeso.Text = e.Peso;
        }

    }
}
