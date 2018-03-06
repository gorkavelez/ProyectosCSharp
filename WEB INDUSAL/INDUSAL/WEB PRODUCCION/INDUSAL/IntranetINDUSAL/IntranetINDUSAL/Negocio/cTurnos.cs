using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace IntranetINDUSAL.Negocio
{
    public class cTurnos
    {
        #region VARIABLES

            private Panel _panel;
            private DataTable _datosOrigen;        

        #endregion

        #region PROPIEDADES

            public Panel Panel
            {
                get { return _panel; }
                set { _panel = value; }
            }

            public DataTable DatosOrigen
            {
                get { return _datosOrigen; }
                set { _datosOrigen = value; }
            }

        #endregion
    }
}
