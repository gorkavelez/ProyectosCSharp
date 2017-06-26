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

namespace SesionPruebasBTI
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
                WS_BTI_Almacen.WS_BTI_ALMACEN vServ = new WS_BTI_Almacen.WS_BTI_ALMACEN();
                vServ.Credentials = new NetworkCredential(txtUsuario.Text, txtPassWord.Text, txtDomain.Text);
                string iDioma = string.Empty;
                string nBD = string.Empty;
                string nEmpresa = string.Empty;
                vServ.AccPistola(txtUsuario.Text, txtPassWord.Text, ref iDioma, ref nBD, ref nEmpresa);
                MessageBox.Show("idioma: " + iDioma + ", nBD: " + nBD + ", Empresa: " + nEmpresa);
                //vServ.IngenetRegistrarPicking("");                          
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
