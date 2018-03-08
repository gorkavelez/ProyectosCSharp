using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColaProyectos.WSLanzarCola;

namespace ColaProyectos
{
    class Program
    {
        static void Main(string[] args)
        {
            string Path = @"C:\PDF\Log.txt";
            Entidad cssEntidad = new Entidad();
            WSLanzarCola.LanzarColaProyecto SrvLanzar = new LanzarColaProyecto();
            SrvLanzar.Timeout = 86400000;
            SrvLanzar.UseDefaultCredentials = true;
            string msgError = string.Empty;            
            try
            {
                if (SrvLanzar.LanzarColaTrabajo(ref msgError))
                {
                    cssEntidad.escribirLog("Cola procesada correctamente", Path, true);
                }
                else
                    throw new Exception(msgError);                
            }
            catch(Exception ex)
            {               
                cssEntidad.escribirLog(ex.Message, Path, true);
                
            }
        }

    }
}
