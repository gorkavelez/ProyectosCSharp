using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace INIKER.ViscoStockLabelIntegration
{
    public partial class ViscoStock_Label_Integration
    {
        public ViscoStock_Label_Integration(string empresa)
        {
            EntIntNavLocalViscoStock.Properties.Settings miConfig = new EntIntNavLocalViscoStock.Properties.Settings();
            this.Url = miConfig.urlInicioWS + empresa + "/Codeunit/ViscoStock_Label_Integration?WSDL";
            this.UseDefaultCredentials = true;
            miConfig = null;
        }
    }
}
