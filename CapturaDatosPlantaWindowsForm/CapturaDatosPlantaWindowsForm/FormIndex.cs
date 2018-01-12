using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CapturaDatosPlantaWindowsForm
{
    public partial class FormIndex : Form
    {
        public FormIndex()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {                
                lblError.Text = string.Empty;
                string CodGrupo = string.Empty;
                SK.FuncionesCapturaDatos.FuncionesCapturaWeb funciones = new SK.FuncionesCapturaDatos.FuncionesCapturaWeb();
                if (funciones.Login(txtUsuario.Text, txtPassword.Text, ref CodGrupo))
                {                 
                    //string Pag = "SabanaGrupos.aspx?grupo=" + CodGrupo;
                    SabanasGrupo vSabGrupo = new SabanasGrupo();
                    vSabGrupo.usuario = txtUsuario.Text;
                    vSabGrupo.Grupo = CodGrupo;
                    vSabGrupo.Show();
                }
                else
                    throw new Exception("Acceso denegado");
            }
            catch (Exception ex)
            {
                lblError.Visible = true;                
                lblError.Text = ex.Message;
            }
        }

        private void FormIndex_Load(object sender, EventArgs e)
        {
            txtUsuario.Focus();            
            //txtUsuario.Text = "gvelez";
            //txtPassword.Text = "111";
        }

        private void FormIndex_SizeChanged(object sender, EventArgs e)
        {
            CenterControlInParent(panel1);
        }
        
        private void CenterControlInParent(Control ctrlToCenter)
        {
            ctrlToCenter.Left = (ctrlToCenter.Parent.Width - ctrlToCenter.Width) / 2;
            ctrlToCenter.Top = (ctrlToCenter.Parent.Height - ctrlToCenter.Height) / 2;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void txtUsuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void txtUsuario_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblError_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
