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
    public partial class SeleccionFechas : Form
    {
        public string _fecha;
        public string _hora;
        public Form _formulario;

        public string fecha
        {
            get { return _fecha; }
            set { _fecha = value; }
        }

        public string hora
        {
            get { return _hora; }
            set { _hora = value; }
        }

        public Form formulario
        {
            get { return _formulario; }
            set { _formulario = value; }
        }
        
        public SeleccionFechas()
        {
            InitializeComponent();
        }

        private void SeleccionFechas_Load(object sender, EventArgs e)
        {
            this.Text = "Seleccion de fechas";
            txtFechaAno.Text = DateTime.Parse(fecha).Year.ToString().Substring(2, 2);
            txtFechaMes.Text = DateTime.Parse(fecha).Month.ToString("00");
            txtFechaDia.Text = DateTime.Parse(fecha).Day.ToString("00");
            cargarHora();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void cargarHora()
        {
            for (int index = 6; index <= 23; index++)
            {
                cboHoras.Items.Add(index.ToString("00") + ":00");
            }
            for (int index = 0; index <= 5; index++)
            {
                cboHoras.Items.Add(index.ToString("00") + ":00");
            }
            cboHoras.Text = hora.ToString();
        }

        private void SeleccionFechas_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Escape))
            {
                this.Close();
            } 
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {            
            string ArgsDia = txtFechaDia.Text.PadLeft(2,'0') + "/" + 
                             txtFechaMes.Text.PadLeft(2,'0') + "/" + 
                             txtFechaAno.Text.PadLeft(2,'0');
            string ArgsHora = cboHoras.Text;

            if (exsisteFecha(ArgsDia))
            {
                Form vFormOrigen = formulario;                                    
                Control ctrDia = vFormOrigen.Controls.Find("txtDia", true).FirstOrDefault();
                ctrDia.Text = ArgsDia;
                Control ctrHora = vFormOrigen.Controls.Find("TxtHora", true).FirstOrDefault();                
                ctrHora.Text = ArgsHora;               
                this.Close();
            }
            else
                MessageBox.Show("la fecha no es correcta", "Error: Formato Fecha", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public bool exsisteFecha(string fecha)
        {
            DateTime valorFecha;
            if (DateTime.TryParse(fecha, out valorFecha))
                return true;
            else
                return false;
        }
    }
}
