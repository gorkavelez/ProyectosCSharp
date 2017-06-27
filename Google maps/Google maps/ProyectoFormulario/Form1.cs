using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ClasesLogicas;
using System.IO;
using System.Xml;
using System.Text.RegularExpressions;


namespace ProyectoFormulario
{
    public partial class btnSelectFile : Form
    {
        int contadorFicheros = 0;
        int contadorDirectorios = 0;
        ClaseConexion vcon = new ClaseConexion();        

        public btnSelectFile()
        {
            InitializeComponent();                        
        }

        private void btnAccion_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            string mode = string.Empty;

            if (rdbBicileta.Checked)
                mode="bicycling";
            if (rdbCoche.Checked)
                mode = "driving";
            if (rdbPaseo.Checked)
                mode = "walking";

            RellenarRuta(txtOrigen.Text, txtDestino.Text,mode);
        }


        private void txtDestino_MouseClick(object sender, MouseEventArgs e)
        {
            FolderBrowserDialog directorio = new FolderBrowserDialog();
            directorio.ShowDialog();
            txtDestino.Text = directorio.SelectedPath;            
        }
                

        private void btnSelectFile_Load(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            rdbCoche.Checked = true;
        }


        public void RellenarRuta(string origen, string destino, string mode)
        {
            try
            {
                if (mode == string.Empty)
                    mode = "driving";
                string UrlServicio = @"http://maps.googleapis.com/maps/api/directions/xml?origin=" + origen + "&destination=" + destino +
                                      "&mode=" + mode + "&region=ES&language=es";
                XmlTextReader vReader = new XmlTextReader(UrlServicio);
                XmlDocument doc = new XmlDocument();
                doc.Load(vReader);
                XmlNodeList nodos = doc.GetElementsByTagName("html_instructions");
                int vIndex = 1;
                foreach (XmlElement volcado in nodos)
                {
                    listBox1.Items.Add(vIndex + " - " + Regex.Replace(volcado.InnerText, "<.*?>", String.Empty));
                    vIndex++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
