using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColaProyectos.WSLanzarCola;

namespace ColaProyectos
{
    public class Entidad
    {
        public void escribirLog(string errMsg, string path, bool fechaHora = false)
        {
            try
            {
                if (fechaHora)
                {
                    //System.Globalization.CultureInfo enUS = new CultureInfo("en-US");
                    if (System.Globalization.CultureInfo.CurrentCulture.ToString() == "en-US")
                        errMsg = System.DateTime.Now.AddHours(6).ToString("dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.CurrentCulture) + " " + errMsg;
                    else
                        errMsg = System.DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.CurrentCulture) + " " + errMsg;                    
                }

                if (!System.IO.File.Exists(path))
                {
                    System.IO.File.Create(path).Dispose();
                    System.IO.TextWriter tw = new System.IO.StreamWriter(path);
                    tw.WriteLine(errMsg);                    
                    tw.Flush();
                    tw.Close();
                    tw.Dispose();
                }
                else
                {
                    System.IO.TextWriter tw = new System.IO.StreamWriter(path, true);
                    tw.WriteLine(errMsg);         
                    tw.Flush();
                    tw.Close();
                    tw.Dispose();
                }
            }
            catch (System.Exception ex)
            {
                               

            }            

        }
    }
}
