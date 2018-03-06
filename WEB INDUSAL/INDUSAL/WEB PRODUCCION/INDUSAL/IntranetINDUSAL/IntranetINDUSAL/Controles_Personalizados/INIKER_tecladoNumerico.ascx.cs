using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IntranetINDUSAL.Controles_Personalizados
{
    public partial class INIKER_tecladoNumerico : System.Web.UI.UserControl
    {
        #region Evento OKClick
            // delegado
            public delegate void OKClickHandler(object sender, OKClickEventArgs e);
            //evento
            public event OKClickHandler OKClick;

            // Clase que encapsula los datos que se pasan en el segundo parámetro del evento
            public class OKClickEventArgs : EventArgs
            {
                private string _valor;

                public string Valor
                {
                  get { return _valor==""?"0":_valor; }
                  set { _valor = value; }
                }                

                public OKClickEventArgs(string valor)
                {
                    this._valor = valor;                    
                }

            }

            protected virtual void OnOKClick(OKClickEventArgs e)
            {
                if (OKClick != null)
                {
                    OKClick(this, e);
                    txValorTeclado.Text = "";
                    hdnValue.Value = "";
                }
            }

            public void OK_Click(object sender, EventArgs e)
            {
                IButtonControl btn = (IButtonControl)sender;
                // se deja el valor en la propiedad, antes de lanzar el evento
                //_dato = hdnValue.Value;
                // se lanza el evento para recogerlo en el objeto contenedor del control
                OKClickEventArgs pcea = new OKClickEventArgs(FormatearNumero(hdnValue.Value));
                OnOKClick(pcea);
            }

            private string FormatearNumero(string numero)
            {
                string formatNum = numero;
                string sepDec = RecuperarSeparadorDecimal();

                switch (sepDec)
                {
                    case ",":
                        formatNum=numero.Replace(".", ",");
                        break;
                    case ".":
                        formatNum=numero.Replace(",", ".");
                        break;
                }
                return (formatNum);
            }
        #endregion

        #region Propiedades

            private string _tituloDato;
            private string _dato;  

            public string TituloDato
            {
                get { return _tituloDato; }
                set { _tituloDato = value; }
            }

            public string Dato
            {
                get { return _dato; }
                set { _dato = value; }
            }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {       
        }

        private string RecuperarSeparadorDecimal()
        {
            System.Globalization.CultureInfo CI = System.Globalization.CultureInfo.CurrentCulture;
            return(CI.NumberFormat.NumberDecimalSeparator);
        }

        protected void lbDatoIntro_PreRender(object sender, EventArgs e)
        {
            lbDatoIntro.Text = _tituloDato;            
        }

        protected void txValorTeclado_PreRender(object sender, EventArgs e)
        {
            txValorTeclado.Text = _dato;
            hdnValue.Value = _dato;
        }

        
    }
}