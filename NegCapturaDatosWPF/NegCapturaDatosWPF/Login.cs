using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFIntegration;

namespace NegCapturaDatosWPF
{
    public class Login : WPFIntegration.ILogin
    {
        bool ILogin.Login(string UserName, string password, out Dictionary<string, object> UserProperties)
        {            
            UserProperties = new Dictionary<string, object>();

            string CodGrupo = string.Empty;
            NegCapturaDatosWPF.FuncionesCapturaDatos.FuncionesCapturaWeb funciones = new NegCapturaDatosWPF.FuncionesCapturaDatos.FuncionesCapturaWeb();
            if (funciones.Login(UserName, password, ref CodGrupo))
            {
                UserProperties.Add("CodGrupo", CodGrupo);
                return true;
            }
            return false;
        }
    }
}
