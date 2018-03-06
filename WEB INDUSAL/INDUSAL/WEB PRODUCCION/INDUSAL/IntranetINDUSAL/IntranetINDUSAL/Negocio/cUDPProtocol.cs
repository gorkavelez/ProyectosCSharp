using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Web;


namespace IntranetINDUSAL.Negocio
{
    public class cUDPProtocol
    {

        public int Get(int nCanal)
        {

            int local_port = 12060;
            string Local_hostname = Dns.GetHostName();
            Console.WriteLine("Localhostname = {0}", Local_hostname);
            IPHostEntry local_ip = Dns.GetHostEntry(Local_hostname);
            IPEndPoint local_ipe = new IPEndPoint(local_ip.AddressList[0], local_port);
            UdpClient MultiCast_Client = new UdpClient(local_ipe);
            
            MultiCast_Client.JoinMulticastGroup(IPAddress.Parse("224.224.0.1"));
            IPEndPoint host_ipe = new IPEndPoint(IPAddress.Parse("192.168.2.225"), local_port);

            string decodeChar = "";

            while (decodeChar=="")
            {
                byte[] data = MultiCast_Client.Receive(ref host_ipe);
                
                string a = data.ToString();                
                bool seguir = true;
                
                for (int iChar = 0; iChar < data.Length && seguir; iChar++)
                {
                    string temp = ((char)data[iChar]).ToString();
                    if (temp != "|")
                        decodeChar += temp;
                    else
                    {
                        seguir = false;
                    }
                }                
            }
            // una vez que se han recibido los datos, se separa el dato del canal solicitado
            string[] datosCanales;
            try
            {
                datosCanales = decodeChar.Split(';');
                return (int.Parse(datosCanales[nCanal - 1]));
            }
            catch
            {
                return (0);
            }
        }

    }
}
