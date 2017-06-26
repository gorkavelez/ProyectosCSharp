using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using System.Windows.Forms;

using SK;

namespace CapturaDatosPlantaWindowsForm
{
    public partial class Maquina4 : Form
    {
        string _sabana = string.Empty;
        string _usuario = string.Empty;
        bool modificadoDatos = false;

        public string sabana
        {
            get { return _sabana; }
            set { _sabana = value; }
        }

        public string usuario
        {
            get { return _usuario; }
            set { _usuario = value; }
        }

        public Maquina4()
        {
            InitializeComponent();
        }
        

        private void Form1_Load(object sender, EventArgs e)
        {
			//Carga formulario prbgve1  
            txtDia.Text = DateTime.Today.ToString("dd/MM/yy");
            TxtHora.Text = DateTime.Now.ToString("HH:00");
            modificadoDatos = false;
            this.Text = sabana;
            lblSabana.Text = this.Text;
            txt_sk_1.Focus();

            //DíaPapelero
            int horaNow = int.Parse(DateTime.Now.ToString("HH:00").Split(':')[0]);
            if (horaNow >= 0 & horaNow <= 05)
            {
                txtDia.Text = DateTime.Now.Date.AddDays(-1).ToString("dd/MM/yy");
            }
            lblUsuario.Text = "Usuario: " + usuario;
            CargarTodoFormularioDatosNav();    
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (guardarDatosHistorico())
            {
                modificadoDatos = false;
                MessageBox.Show("Ok: Prcoesado correctamente");
                CargarTodoFormularioDatosNav();
            }            
        }
        #region funciones

        public bool guardarDatosHistorico()
        {
            bool procesadoOk = false;
            string[] valores = new string[70];
            SK.FuncionesCapturaDatos.FuncionesCapturaWeb cFunWeb = new SK.FuncionesCapturaDatos.FuncionesCapturaWeb();
            try
            {
                //aqui van la lectura de todos los valores de los campos
                for (int indCtrlTxt = 1; indCtrlTxt <= 5; indCtrlTxt++)
                {
                    valores[indCtrlTxt] = string.Empty;                    
                    valores[indCtrlTxt] = traerValorCampo(indCtrlTxt);                    
                }
                
                //guardar registro de observaciones                
                string[] obserTot = new string[100];
                if (txtObservac.Text != string.Empty)
                {
                    int NumLinea = 0, contador = 0;
                    for (int index = 0; index < txtObservac.Text.Length; index++)
                    {
                        if (contador == 250)
                        {
                            contador = 0;
                            NumLinea = NumLinea + 1;
                        }
                        else
                        {
                            obserTot[NumLinea] = obserTot[NumLinea] + txtObservac.Text[index].ToString();
                            contador++;
                        }
                    }
                }

                //añadimos los valores propios del formulario una vez recogidos los campos de lectura.                               
                cFunWeb.GuardarDatosenNav(valores,
                                          sabana,                           //sabana
                                          usuario,    //usuario
                                          DateTime.Parse(txtDia.Text),      //fecha registro
                                          DateTime.Parse(TxtHora.Text),     //hora registro
                                          DateTime.Now.Date,                //fecha sistema
                                          DateTime.Now,                     //hora sistema                                          
                                          obserTot);  
                procesadoOk = true;
                
            }
            catch (Exception ex)
            {
                procesadoOk = false;                
                MessageBox.Show("Error: "+ex.Message);                                
            }
            return procesadoOk;
        }

        public void CargarTodoFormularioDatosNav()
        {
            try
            {
                limpiarDatos();                
                FuncionesWs.FuncionesWs funcionesProxy = new FuncionesWs.FuncionesWs();
                //string filtrofecha = txtDia.Text;
                string filtrofecha = txtDia.Text.Split('/')[1] + "/" + txtDia.Text.Split('/')[0] + "/" + txtDia.Text.Split('/')[2];                
                //string filtrofecha = txtDia.Text.Split('-')[1] + "-" + txtDia.Text.Split('-')[0] + "-" + txtDia.Text.Split('-')[2];
                DateTime hora = Convert.ToDateTime(TxtHora.Text);

                SK.historicoAdquisicionConceptos.CapturaDatos_HistoricoAdquisicionConceptos[] datoshistorico = null;
                string filtrohoraJusta = TxtHora.Text;                
                funcionesProxy.traerRegistrosUltimaHora(_sabana, filtrofecha, filtrohoraJusta, ref datoshistorico);
                LblError.Text = "Entra1";
                if (datoshistorico.Count() == 0)
                {
                    //si no hay registros con la misma hora a la que nos movemos sacará los datos de uno en uno.
                    datoshistorico = null;
                    string filtrohoraAnt = hora.AddHours(-1).ToString("HH:00") + ".." + TxtHora.Text;
                    funcionesProxy.traerRegistrosUltimaHora(sabana, filtrofecha, filtrohoraAnt, ref datoshistorico);
                    if (datoshistorico.Count() != 0)
                        CargarControlesHidden(datoshistorico);
                    LblError.Text = "Entra2" + " " + sabana + " " + filtrofecha + " " + filtrohoraAnt+ " "+ datoshistorico.Count().ToString();
                }
                else
                {
                    LblError.Text = "Entra3";
                    for (int indCtrlTxt = 1; indCtrlTxt <=5; indCtrlTxt++)
                    {
                        if (this.Controls["txt_SK_" + indCtrlTxt] != null)
                        {
                            var ctrFoco = this.Controls["txt_SK_" + indCtrlTxt];
                            TextBox Foco = ctrFoco as TextBox;
                           
                            var datoHistoricoRecuperado = datoshistorico.Where(dh => dh.Codigo == indCtrlTxt.ToString());
                            if (datoHistoricoRecuperado.Count() != 0)
                            {
                                if (datoHistoricoRecuperado.Last().Texto != null)
                                    Foco.Text = datoHistoricoRecuperado.Last().Texto;
                                else
                                    Foco.Text = datoHistoricoRecuperado.Last().Valor.ToString();
                            }
                            if (Foco.Text == "0")
                                Foco.Text = string.Empty;                            
                        }
                    }                    
                }
             

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public string traerValorCampo(int indice)
        {
            string valor = string.Empty;
            string control = "txt_SK_" + indice;
            
            if (control != null)
            {
                Control v1 = this.Controls[control];
                valor = v1.Text;
            }
            return valor;
        }

        public void LimpiarValorCampo(int indice)
        {
            string control = "txt_SK_" + indice;
            if (this.Controls[control] != null)
            {
                Control ctrl = this.Controls[control];
                TextBox txtCtrl = ctrl as TextBox;
                txtCtrl.Text = string.Empty;
            }
        }

        public void limpiarDatos()
        {
            for (int indCtrlTxt = 1; indCtrlTxt <= 69; indCtrlTxt++)
            {
                LimpiarValorCampo(indCtrlTxt);
            }
        }

        public void CargarControlesHidden(SK.historicoAdquisicionConceptos.CapturaDatos_HistoricoAdquisicionConceptos[] datoshistorico)
        {
            for (int indCtrlTxt = 1; indCtrlTxt <= 5; indCtrlTxt++)
            {
                var datoHistoricoRecuperado = datoshistorico.Where(dh => dh.Codigo == indCtrlTxt.ToString());
                if (datoHistoricoRecuperado.Count() != 0)
                {
                    Control hiddenFoco = this.Controls["hdn_" + indCtrlTxt];
                    TextBox datoOculto = hiddenFoco as TextBox;
                    if (datoHistoricoRecuperado.Last().Texto != null)
                        datoOculto.Text = datoHistoricoRecuperado.Last().Texto;
                    else
                        datoOculto.Text = datoHistoricoRecuperado.Last().Valor.ToString();
                }
            }
        }

#endregion
        

        #region pulsaciones

        private void txt_sk_1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");               
            }             
        }
      
        private void txt_sk_2_KeyPress(object sender, KeyPressEventArgs e)
        {            
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");                
            }          
        }

        private void txt_sk_3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void txt_sk_4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }          
        }
        private void txt_sk_5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }     
        }

        #endregion

        #region focos

        private void txt_sk_1_Enter(object sender, EventArgs e)
        {
            if (txt_sk_1.Text == string.Empty)
                txt_sk_1.Text = hdn_1.Text;
        }

        private void txt_sk_2_Enter(object sender, EventArgs e)
        {
            if (txt_sk_2.Text == string.Empty)
                txt_sk_2.Text = hdn_2.Text;
        }

        private void txt_sk_3_Enter(object sender, EventArgs e)
        {
            if (txt_sk_3.Text == string.Empty)
                txt_sk_3.Text = hdn_3.Text;
        }

        private void txt_sk_4_Enter(object sender, EventArgs e)
        {
            if (txt_sk_4.Text == string.Empty)
                txt_sk_4.Text = hdn_4.Text;
        }

        private void txt_sk_5_Enter(object sender, EventArgs e)
        {
            if (txt_sk_5.Text == string.Empty)
                txt_sk_5.Text = hdn_5.Text;
        }

        #endregion
        
        #region botonesFechaHora

        private void btnUpDia_Click(object sender, EventArgs e)
        {
            DateTime dia = Convert.ToDateTime(txtDia.Text);
            if (modificadoDatos)
            {
                if (guardarDatosHistorico())
                {
                    txtDia.Text = dia.AddDays(1).ToString("dd/MM/yy");
                    CargarTodoFormularioDatosNav();
                    modificadoDatos=false;
                }
            }
            else
            {
                txtDia.Text = dia.AddDays(1).ToString("dd/MM/yy");
                CargarTodoFormularioDatosNav();
            }
            txt_sk_1.Focus();
        }
        
        private void btnDownDia_Click(object sender, EventArgs e)
        {
            DateTime dia = Convert.ToDateTime(txtDia.Text);
            if (modificadoDatos)
            {
                if (guardarDatosHistorico())
                {
                    txtDia.Text = dia.AddDays(-1).ToString("dd/MM/yy");
                    CargarTodoFormularioDatosNav();
                    modificadoDatos = false;
                }
            }
            else
            {
                txtDia.Text = dia.AddDays(-1).ToString("dd/MM/yy");
                CargarTodoFormularioDatosNav();
            }
            txt_sk_1.Focus();
        }


       
        private void btnUpHora_Click(object sender, EventArgs e)
        {
            DateTime hora = Convert.ToDateTime(TxtHora.Text);
            DateTime dia = Convert.ToDateTime(txtDia.Text);
            if (modificadoDatos)
            {
                if (guardarDatosHistorico())
                {
                    TxtHora.Text = hora.AddHours(1).ToString("HH:00");
                    if (TxtHora.Text == "06:00")
                        txtDia.Text = dia.AddDays(1).ToString("dd/MM/yy");
                    CargarTodoFormularioDatosNav();
                    modificadoDatos = false;
                }
            }
            else
            {
                TxtHora.Text = hora.AddHours(1).ToString("HH:00");
                if (TxtHora.Text == "06:00")
                    txtDia.Text = dia.AddDays(1).ToString("dd/MM/yy");
                CargarTodoFormularioDatosNav();
            }
            txt_sk_1.Focus();
        }

        private void btnDownHora_Click(object sender, EventArgs e)
        {
            DateTime hora = Convert.ToDateTime(TxtHora.Text);
            DateTime dia = Convert.ToDateTime(txtDia.Text);
            if (modificadoDatos)
            {
                if (guardarDatosHistorico())
                {
                    TxtHora.Text = hora.AddHours(-1).ToString("HH:00");
                    if (TxtHora.Text == "05:00")
                        txtDia.Text = dia.AddDays(-1).ToString("dd/MM/yy");
                    CargarTodoFormularioDatosNav();
                    modificadoDatos = false;
                }
            }
            else
            {
                TxtHora.Text = hora.AddHours(-1).ToString("HH:00");
                if (TxtHora.Text == "05:00")
                    txtDia.Text = dia.AddDays(-1).ToString("dd/MM/yy");
                CargarTodoFormularioDatosNav();
            }
            txt_sk_1.Focus();
        }

        #endregion

        #region textChange       
                    

        private void txt_sk_1_TextChanged(object sender, EventArgs e)
        {
            modificadoDatos = true;
        }

        private void txt_sk_2_TextChanged(object sender, EventArgs e)
        {
            modificadoDatos = true;
        }

        private void txt_sk_3_TextChanged(object sender, EventArgs e)
        {
            modificadoDatos = true;
        }

        private void txt_sk_4_TextChanged(object sender, EventArgs e)
        {
            modificadoDatos = true;
        }

        private void txt_sk_5_TextChanged(object sender, EventArgs e)
        {
            modificadoDatos = true;
        }



        #endregion

        private void btnAtras_Click(object sender, EventArgs e)
        {
            this.Close();//pruebas 
        }

        private void Maquina4_SizeChanged(object sender, EventArgs e)
        {

        }

        private void btnSelecionFechas_Click(object sender, EventArgs e)
        {
            SeleccionFechas vPopupFecha = new SeleccionFechas();
            vPopupFecha.fecha = txtDia.Text;
            vPopupFecha.hora = TxtHora.Text;
            vPopupFecha.formulario = this;
            vPopupFecha.StartPosition = FormStartPosition.CenterScreen;
            vPopupFecha.Show();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void TxtHora_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDia_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblSabana_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

               
    }
}
