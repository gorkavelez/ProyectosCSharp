using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace INIKER.ConfProcAlmacenados
{
    public partial class AT_Conf_Proc_Almacenados_Service
    {
        public AT_Conf_Proc_Almacenados_Service(string empresa)
        {
            EntIntNavLocalViscoStock.Properties.Settings miConfig = new EntIntNavLocalViscoStock.Properties.Settings();
            this.Url = miConfig.urlInicioWS + empresa + "/Page/AT_Conf_Proc_Almacenados?WSDL";
            this.UseDefaultCredentials = true;
            miConfig = null;
        }
    }
}
