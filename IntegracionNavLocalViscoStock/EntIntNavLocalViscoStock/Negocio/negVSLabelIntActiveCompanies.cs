using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace INIKER.VSLabelIntActiveCompanies
{
    
    public partial class VS_Label_Int_Active_Companies
    {
        public VS_Label_Int_Active_Companies(string empresa)
        {
            EntIntNavLocalViscoStock.Properties.Settings miConfig = new EntIntNavLocalViscoStock.Properties.Settings();
            this.Url = miConfig.urlInicioWS + empresa +"/Codeunit/VS_Label_Int_Active_Companies?WSDL";
            this.UseDefaultCredentials = true;
            miConfig = null;         
        }
    }
}
