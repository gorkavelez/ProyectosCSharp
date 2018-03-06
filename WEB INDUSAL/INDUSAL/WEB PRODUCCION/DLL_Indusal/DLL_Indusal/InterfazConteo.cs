using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace DLL_Indusal
{
    public class InterfazConteo
    {
        private byte firstIpByte = 192;
        private byte secondIpByte = 168;
        private byte thirdIpByte = 0;
        private byte fourthIpByte = 225;
        private int ipPort = 12060;

        public byte FirstIpByte
        {
            get { return firstIpByte; }
            set { firstIpByte = value; }
        }

        public byte SecondIpByte
        {
            get { return secondIpByte; }
            set { secondIpByte = value; }
        }
        
        public byte ThirdIpByte
        {
            get { return thirdIpByte; }
            set { thirdIpByte = value; }
        }

        public byte FourthIpByte
        {
            get { return fourthIpByte; }
            set { fourthIpByte = value; }
        }

        public int IpPort
        {
            get { return ipPort; }
            set { ipPort = value; }
        }

        public string Get()
        {

            //int local_port = 12060;
            int local_port = ipPort;
            string Local_hostname = Dns.GetHostName();            
            IPHostEntry local_ip = Dns.GetHostEntry(Local_hostname);

            bool found = false;
            int i;
            //byte[] myIP = { 192, 168, 2, 225 };
            byte[] myIP = { firstIpByte, secondIpByte, byte.Parse(getIPbyte(2)), fourthIpByte};
            System.Net.IPAddress portIP = new System.Net.IPAddress(myIP);
            for (i = 0; i < local_ip.AddressList.Length && !found; i++)
            {
                if (local_ip.AddressList[i].Address == portIP.Address)
                    found = true;
            }

            IPEndPoint local_ipe = new IPEndPoint(local_ip.AddressList[i - 1], local_port);
            UdpClient MultiCast_Client = new UdpClient(local_ipe);
            
            MultiCast_Client.JoinMulticastGroup(IPAddress.Parse("224.224.0.1"));
                        
            IPEndPoint MultiCast_ipe = new IPEndPoint(IPAddress.Any, 0);
            IPEndPoint host_ipe = new IPEndPoint(IPAddress.Parse("192.168.0.168"), 5000); // esta también funciona
            //IPEndPoint host_ipe = new IPEndPoint(IPAddress.Parse("192.168.2.225"), 12060); //esta es la que funciona

            while (true)
            {
                byte[] data = MultiCast_Client.Receive(ref MultiCast_ipe);
                string a = data.ToString();
                string decodeChar = "";
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
                //Console.WriteLine("Valores conteo: {0}", decodeChar);
                MultiCast_Client.Close();
                return (decodeChar);
            }
        }

        private string getIPbyte(int nByte)
        {
            string octeto = "";

            try
            {
                System.Net.NetworkInformation.NetworkInterface[] ni =
                    System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces();

                string strIP = ni[0].GetIPProperties().UnicastAddresses[0].Address.ToString();
                string[] bytes = strIP.Split('.');

                octeto = bytes[nByte];
            }
            catch (Exception)
            { }

            return (octeto);



            

        }

    }
}
