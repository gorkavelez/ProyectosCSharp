
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
//using System.Linq;
using System.Text;
//using System.Threading.Tasks;

namespace Avalara
{
    class Program
    {

        static void Main(string[] args)
        {
            entAvalaraConnector con = new entAvalaraConnector();
            con.username = "Navision API"; //"g.remirez@saltosystems.com"; //PRE/PRO
            con.password = "Avalara2017!"; //PRE/PRO
            //con.CompanyCode = "SALTOTEST"; //PRE
            //con.CompanyCode = "SALTO"; //PRO
            con.produccion = false;
            con.transactionLog = false;
            con.lineLog = false;
            con.fullLog = false;
            con.console = false;
            string strLogParms = "";

            //Recogemos parámetros
            if ((args != null) && (args.Length > 0))
            {
                if (args.Length > 0) { strLogParms += args[0].ToLower(); }       // Environment: pre - pro
                if (args.Length > 1) { strLogParms += " " + args[1].ToLower(); } // Document type
                if (args.Length > 2) { strLogParms += " " + args[2].ToLower(); } // Document 
                if (args.Length > 3) { strLogParms += " " + args[3].ToLower(); } // User
                if (args.Length > 4)                                             // Write the Full sent data to log - fulllog
                {                                                    
                    strLogParms += " " + args[4].ToLower();
                    if (args[4].ToLower().StartsWith("fulllog"))
                    {
                        con.fullLog = true;
                    }
                } 

                if (args[0].ToLower().StartsWith("pro"))
                {
                    con.produccion = true;
                }
                else {
                    con.filesPath = con.filesPath + @"test\";
                }
                con.LogPath = con.filesPath + @"log_avalara.txt";

                General.CreateFileAddLine(con.Program + " (" + con.produccion + "). Parámetros: " + strLogParms, con.LogPath, true);
            }

            General.CreateFileAddLine(con.Program + " (" + con.produccion + "). Inicio", con.LogPath, true);

            try
            {
                con.Connect();
                if (con.Connected)
                    con.EnviarTransaccion(args[1].ToLower(), args[2].ToLower()); //3 "cr 17/0105"
                else
                {
                    General.CreateFileAddLine(con.Program + " (" + con.produccion + "). Sin conexión con Avalara.", con.LogPath, true);
                    General.SendEmail("Nav1@saltosystems.com", "nav.hq@saltosystems.com", con.Program + " (" + con.produccion + "). Documento no enviado a Avalara. Enviarlo manualmente: " + strLogParms,
                        "con.Connect() no ha establecido conexión con Avalara. Hay que procesarlo.");
                }
            }
            catch (Exception ex) {
                string lastStrError = "";  
                lastStrError = ex.Message.ToString();
                if ((ex.InnerException != null) && (ex.InnerException.Message != null))
                {
                    lastStrError += ". " + ex.InnerException.Message.ToString();
                }

                con.GuardarErrorEnNavision(args[1].ToLower(), args[2].ToLower(), 0, lastStrError);

                General.CreateFileAddLine(con.Program + "Error (" + con.produccion + "). Main. " + lastStrError, con.LogPath, true);
                General.SendEmail("Nav1@saltosystems.com", "nav.hq@saltosystems.com", con.Program + "Error (" + con.produccion + "). Main. Params: " + strLogParms,
                    "Error: " + lastStrError);
            }

            //throw new Exception(con.lastStrError);
            General.CreateFileAddLine(con.Program + " (" + con.produccion + "). Fin", con.LogPath, true);

        }

    }
}
