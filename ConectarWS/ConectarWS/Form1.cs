using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;

namespace ConectarWS
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                System.Net.NetworkCredential vCred = new System.Net.NetworkCredential("GVELEZ", @"pass@word1", "AXINI");
                CodeunitWS.PruebasWS vprb = new CodeunitWS.PruebasWS();
                vprb.UseDefaultCredentials = false;
                vprb.Credentials = vCred;                                
                CodeunitWS.prbgve vXmlPort = new CodeunitWS.prbgve();
                
                
                CodeunitWS.SD[] vSD = new CodeunitWS.SD[10];

                CodeunitWS.SD vSDSimple = new CodeunitWS.SD();
                for (int i = 1; i < 5; i++)
                {
                    vSDSimple.Name = "prbgve" + i.ToString();
                    vSDSimple.Address = "direccion" + i.ToString();
                    vSD[i] = vSDSimple;
                }
                
                vXmlPort.SD = vSD;
                vprb.LLamarWS(ref vXmlPort);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            com.w3schools.www.TempConvert vTemp = new com.w3schools.www.TempConvert();                        
            vTemp.UseDefaultCredentials = false;            
            MessageBox.Show(vTemp.CelsiusToFahrenheit(txtGrados.Text));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            HttpWebRequest vReq = HttpWebRequest.Create(@"http://www.w3schools.com/xml/tempconvert.asmx") as HttpWebRequest ;
            vReq.Method = "POST";
            vReq.KeepAlive = true;
            vReq.AllowAutoRedirect = true;
            vReq.ContentType = "text/xml;charset=utf-8";
            vReq.Timeout =10000;
            vReq.AutomaticDecompression = DecompressionMethods.GZip;
        }

        public void CreateSoapRequest()
        {
                        
        }

        public void GetWebResponse(ref HttpWebRequest LReq, ref HttpWebResponse LResp, ref string txtResp,ref  HttpStatusCode StatusCode,ref  HttpResponseHeader respHeader)
        {
            LResp = LReq.GetResponse() as HttpWebResponse;            
            StatusCode = LResp.StatusCode;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            LineasPrograma.PDA_LineasPrograma_Service vServ = new LineasPrograma.PDA_LineasPrograma_Service();
            NetworkCredential vCred = new NetworkCredential("gvelez", "pass@word1", "AXINI");
            vServ.Credentials = vCred;

            LineasPrograma.PDA_LineasPrograma_Filter[] vFiltros = new LineasPrograma.PDA_LineasPrograma_Filter[10];

            //LineasPrograma.PDA_LineasPrograma_Filter vFiltro = new LineasPrograma.PDA_LineasPrograma_Filter();
            //vFiltro.Field = LineasPrograma.PDA_LineasPrograma_Fields.Cod_programa;
            //vFiltro.Criteria = "M1120001";

            //vFiltros[0] = vFiltro;

            LineasPrograma.PDA_LineasPrograma[] listPrograma;
            listPrograma= vServ.ReadMultiple(vFiltros,"",0);
            if (listPrograma.Count()!=0)
            {
                foreach(LineasPrograma.PDA_LineasPrograma prog in listPrograma)
                {
                    dataGridView1.DataSource = listPrograma;

                }

            }
        }
    }
}
