using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using WPFIntegration;

namespace NegCapturaDatosWPF
{
    public class SeleccionSabana : WPFIntegration.ISabanaSeleccion
    {
        public string[] NombresSabanasDisponibles(WPFIntegration.User UsuarioActual)
        {   
            NegCapturaDatosWPF.SabanasGrupos.CapturaDatos_AdquisicionSabanasGrupos[] ArraySabanas = null;
            NegCapturaDatosWPF.FuncionesWs.FuncionesWs funWebService = new NegCapturaDatosWPF.FuncionesWs.FuncionesWs();                        
            
            UsuarioActual.Properties.TryGetValue("CodGrupo", out object codGrupo);
            funWebService.traerSabanasGruposNav(ref ArraySabanas, codGrupo.ToString());            

            List<string> StrListSabanas = new List<string>();
            string SabanaNombre = string.Empty;

            foreach (NegCapturaDatosWPF.SabanasGrupos.CapturaDatos_AdquisicionSabanasGrupos Sabana in ArraySabanas)
            {                
                SabanaNombre = Sabana.Sabana;
                StrListSabanas.Add(SabanaNombre);
            }            
            string[] StrArrSabanas = StrListSabanas.ToArray();
            return StrArrSabanas;
           // return new string[] { "Máquina 1", "Máquina 2", "Máquina 3" };
        }
    }
}
