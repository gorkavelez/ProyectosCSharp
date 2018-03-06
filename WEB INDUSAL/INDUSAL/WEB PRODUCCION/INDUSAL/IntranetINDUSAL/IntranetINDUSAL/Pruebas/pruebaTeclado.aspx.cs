using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IntranetINDUSAL.Controles_Personalizados;

namespace IntranetINDUSAL.Pruebas
{
    public partial class pruebaTeclado : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.INIKER_tecladoNumerico1.OKClick += 
                new INIKER_tecladoNumerico.OKClickHandler(INIKER_tecladoNumerico1_OKClick);

        }

        protected void INIKER_tecladoNumerico1_OKClick(object sender, INIKER_tecladoNumerico.OKClickEventArgs e)
        {
            TextBox1.Text = e.Valor;
            //TextBox1.Text = INIKER_tecladoNumerico1.Dato;

        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {
            //INIKER_tecladoNumerico1.Dato = TextBox1.Text;
        }
    }
}
