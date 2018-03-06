
static public class General
{

    static public string Program = "AVALARA";

    static public bool WriteToLog(string message, System.Diagnostics.EventLogEntryType type, string source)
    {
        System.Diagnostics.EventLog eventLogConector = new System.Diagnostics.EventLog();
        eventLogConector.Source = source;
        eventLogConector.Log = "Application";

        eventLogConector.WriteEntry(message, type);
        
        return true;
    }

    static public bool SendEmail(string from, string to, string subject, string body)
    {
        //var from = "Nav1@saltosystems.com";
        System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient("217.116.0.228", 25); //, 587)
        client.Credentials = new System.Net.NetworkCredential("navision.salto.us", "Navision12!");
        //client.EnableSsl = true;
        client.Send(from, to, subject, body);

        return true;
    }

    static public bool CreateFileAddLine(string line, string path, bool fechaHora = false)
    {
        try
        {
            if (fechaHora)
            {
                //System.Globalization.CultureInfo enUS = new CultureInfo("en-US");
                if (System.Globalization.CultureInfo.CurrentCulture.ToString() == "en-US")
                {
                    line = System.DateTime.Now.AddHours(6).ToString("dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.CurrentCulture) + " " + line;
                }
                else
                {
                    line = System.DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.CurrentCulture) + " " + line;
                }
            }

            if (!System.IO.File.Exists(path))
            {
                System.IO.File.Create(path).Dispose();
                System.IO.TextWriter tw = new System.IO.StreamWriter(path);
                tw.WriteLine(line);
                //tw.Close();
                tw.Flush();
                tw.Close();
                tw.Dispose();
            }
            else
            {
                System.IO.TextWriter tw = new System.IO.StreamWriter(path, true);
                tw.WriteLine(line);
                //tw.Close();
                tw.Flush();
                tw.Close();
                tw.Dispose();
            }
        } catch (System.Exception ex) {
            
            //Igual hay que grabar en el log de windows
            SendEmail("g.remirez@saltosystems.com", "nav.hq@saltosystems.com", Program + ". Error: " + ex.Message, "");

        }

        return true;
    }

}

static public class Seguridad
{
    /// Encripta una cadena
    public static string Encriptar(string _cadenaAencriptar)
    {
        string result = string.Empty;
        byte[] encryted = System.Text.Encoding.Unicode.GetBytes(_cadenaAencriptar);
        result = System.Convert.ToBase64String(encryted);
        return result;
    }

    /// Esta función desencripta la cadena que le envíamos en el parámentro de entrada.
    public static string DesEncriptar(string _cadenaAdesencriptar)
    {
        string result = string.Empty;
        byte[] decryted = System.Convert.FromBase64String(_cadenaAdesencriptar);
        //result = System.Text.Encoding.Unicode.GetString(decryted, 0, decryted.ToArray().Length);
        result = System.Text.Encoding.Unicode.GetString(decryted);
        return result;
    }
}