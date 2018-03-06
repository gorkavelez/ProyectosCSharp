using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WF_PruebaDLL
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DLL_Indusal.InterfazBasculas iBascula = new DLL_Indusal.InterfazBasculas();
            iBascula.MarcaEstable = "A";
            iBascula.MarcaCero = "00\rI";            
            textBox1.Text = iBascula.GetPeso();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DLL_Indusal.InterfazConteo conteo = new DLL_Indusal.InterfazConteo();
            conteo.FirstIpByte = 192;
            conteo.SecondIpByte = 168;
            conteo.ThirdIpByte = 2;
            conteo.FourthIpByte = 225;
            textBox2.Text = conteo.Get();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            System.Net.NetworkInformation.NetworkInterface[] ni =
                System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces();
            
            string strIP = ni[0].GetIPProperties().UnicastAddresses[0].Address.ToString();
            string[] bytes = strIP.Split('.');
            
            //MessageBox.Show(ni[0].GetIPProperties().UnicastAddresses[0].Address.ToString());
            MessageBox.Show(strIP);
            foreach (string str in bytes)
            {
                MessageBox.Show(str);
            }
            
        }
    }
}