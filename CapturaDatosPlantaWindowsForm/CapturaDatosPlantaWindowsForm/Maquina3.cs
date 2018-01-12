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
    public partial class Maquina3 : Form
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
        

        public Maquina3()
        {
            InitializeComponent();
        }       

        private void Maquina3_Load(object sender, EventArgs e)
        {
            Rectangle resolution = Screen.PrimaryScreen.Bounds;
            Funciones.ResolucionControles Resolucion = new Funciones.ResolucionControles();
            if ((Screen.PrimaryScreen.Bounds.Height <= 648) & (Screen.PrimaryScreen.Bounds.Width <= 1152))
                Resolucion.ResizeForm(this, 900, 1440);
            else if ((Screen.PrimaryScreen.Bounds.Height >= 936) & (Screen.PrimaryScreen.Bounds.Width >= 1664))
                Resolucion.ResizeForm(this, 600, 800);
            else
                //vg.ResizeForm(this, Screen.PrimaryScreen.Bounds.Height, Screen.PrimaryScreen.Bounds.Width);
                Resolucion.ResizeForm(this, 864, 1536);

            txtDia.Text = DateTime.Today.ToString("dd/MM/yy");
            TxtHora.Text = DateTime.Now.ToString("HH:00");
            modificadoDatos = false;
            this.Text = sabana;
            lblSabana.Text = this.Text;
            lblUsuario.Text = usuario;
            CargarTodoFormularioDatosNav();            
        }

        

        #region BotonesClick

        private void btnSelecionFechas_Click(object sender, EventArgs e)
        {            
            SeleccionFechas vPopupFecha = new SeleccionFechas();
            vPopupFecha.fecha = txtDia.Text;
            vPopupFecha.hora = TxtHora.Text;
            vPopupFecha.formulario = this;
            vPopupFecha.StartPosition = FormStartPosition.CenterScreen;            
            vPopupFecha.Show();
            vPopupFecha.FormClosed += new FormClosedEventHandler(vPopupFecha_FormClosed);
        }

        void vPopupFecha_FormClosed(object sender, FormClosedEventArgs e)
        {
            CargarTodoFormularioDatosNav();
        }

        private void btnAtras_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (guardarDatosHistorico())
            {
                modificadoDatos = false;
                //MessageBox.Show("Ok: Prcoesado correctamente");
                CargarTodoFormularioDatosNav();
            }
        }

        private void btnUpDia_Click(object sender, EventArgs e)
        {
            DateTime dia = Convert.ToDateTime(txtDia.Text);
            if (modificadoDatos)
            {
                if (guardarDatosHistorico())
                {
                    txtDia.Text = dia.AddDays(1).ToString("dd/MM/yy");
                    CargarTodoFormularioDatosNav();
                    modificadoDatos = false;
                }
            }
            else
            {
                txtDia.Text = dia.AddDays(1).ToString("dd/MM/yy");
                CargarTodoFormularioDatosNav();
            }
            txt_sk_140.Focus();
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
            txt_sk_140.Focus();
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
            txt_sk_140.Focus();
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
            txt_sk_140.Focus();
        }

        #endregion

        #region funciones

        public bool guardarDatosHistorico()
        {
            bool procesadoOk = false;
            string[] valores = new string[200];
            SK.FuncionesCapturaDatos.FuncionesCapturaWeb cFunWeb = new SK.FuncionesCapturaDatos.FuncionesCapturaWeb();
            try
            {
                //aqui van la lectura de todos los valores de los campos
                for (int indCtrlTxt = 1; indCtrlTxt <= 196; indCtrlTxt++)
                {
                    if (indCtrlTxt != 194)//si es el campo de observaciones no lo paso.
                    {
                        valores[indCtrlTxt] = string.Empty;
                        valores[indCtrlTxt] = traerValorCampo(indCtrlTxt);
                    }
                }

                //guardar registro de observaciones
                int procesoObservaciones = 0;
                string[] obserTot = new string[100];
                if (txt_sk_194.Text != string.Empty)
                {
                    int NumLinea = 0, contador = 0;
                    for (int index = 0; index < txt_sk_194.Text.Length; index++)
                    {
                        if (contador == 250)
                        {
                            contador = 0;
                            NumLinea = NumLinea + 1;                            
                        }
                        else
                        {                            
                            obserTot[NumLinea] = obserTot[NumLinea] + txt_sk_194.Text[index].ToString();                            
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
                                          ref procesoObservaciones,
                                          obserTot);            //id proceso para relaciones observaciones

                procesadoOk = true;

            }
            catch (Exception ex)
            {
                procesadoOk = false;
                MessageBox.Show("Error: " + ex.Message);
            }
            return procesadoOk;
        }

        public void CargarTodoFormularioDatosNav()
        {
            try
            {
                limpiarDatos();
                FuncionesWs.FuncionesWs funcionesProxy = new FuncionesWs.FuncionesWs();
                string filtrofecha = string.Empty;
                CapturaDatosPlantaWindowsForm.Properties.Settings miConfig = new Properties.Settings();
                //MessageBox.Show(System.Globalization.CultureInfo.CurrentCulture.ToString());
                if (miConfig.Produccion == false)
                    filtrofecha = txtDia.Text.Split('/')[1] + "/" + txtDia.Text.Split('/')[0] + "/" + txtDia.Text.Split('/')[2];
                else
                {
                    /*como tiene el guion como caracter para fecha en produccion pero visualmente lo tiene como barra hay que hacer la conversion
                     es una ñapa.*/
                    string txtDiaStr = txtDia.Text;
                    if (txtDiaStr.Contains('/'))
                    {
                        filtrofecha = txtDia.Text.Split('/')[1] + "/" + txtDia.Text.Split('/')[0] + "/" + txtDia.Text.Split('/')[2];
                        filtrofecha = filtrofecha.Replace('/', '-');
                    }
                    else
                        filtrofecha = txtDia.Text.Split('-')[1] + "-" + txtDia.Text.Split('-')[0] + "-" + txtDia.Text.Split('-')[2];
                }
                //MessageBox.Show("Comprobacion filtro fecha:" +filtrofecha);                
                DateTime hora = Convert.ToDateTime(TxtHora.Text);
                SK.historicoAdquisicionConceptos.CapturaDatos_HistoricoAdquisicionConceptos[] datoshistorico = null;
                string filtrohoraJusta = TxtHora.Text;
                funcionesProxy.traerRegistrosUltimaHora(_sabana, filtrofecha, filtrohoraJusta, ref datoshistorico);
                int idProceso = datoshistorico[0].Id_Registro;
                if (datoshistorico.Count() == 0)
                {
                    //si no hay registros con la misma hora a la que nos movemos sacará los datos de uno en uno.
                    datoshistorico = null;
                    string filtrohoraAnt = hora.AddHours(-1).ToString("HH:00") + "|" + TxtHora.Text;
                    funcionesProxy.traerRegistrosUltimaHora(sabana, filtrofecha, filtrohoraAnt, ref datoshistorico);
                    if (datoshistorico.Count() != 0)
                        CargarControlesHidden(datoshistorico);                 
                }
                else
                {                    
                    for (int indCtrlTxt = 140; indCtrlTxt <= 196; indCtrlTxt++)
                    {
                        if (this.Controls.Find("txt_sk_" + indCtrlTxt,true)[0] != null)
                        {
                            Control[] ctrFoco = this.Controls.Find("txt_sk_" + indCtrlTxt,true);
                            TextBox Foco = ctrFoco[0] as TextBox;

                            var datoHistoricoRecuperado = datoshistorico.Where(datoHist => datoHist.Codigo == indCtrlTxt.ToString());
                            if (datoHistoricoRecuperado.Count() != 0)
                            {
                                if (datoHistoricoRecuperado.Last().Texto != null)
                                    Foco.Text = datoHistoricoRecuperado.Last().Texto;
                                else
                                    Foco.Text = datoHistoricoRecuperado.Last().Valor.ToString();
                                Foco.Text = Foco.Text.Replace(',', '.');
                            }
                            if (Foco.Text == "0")
                                Foco.Text = string.Empty;
                        }
                    }
                }
                //traer y pintar observaciones
                txt_sk_194.Text = string.Empty;
                SK.Observaciones.CapturaDatos_Observaciones[] arrObservaciones = null;
                funcionesProxy.TraerObservaciones(idProceso.ToString(), ref arrObservaciones);
                for (int vIndex = 0; vIndex < arrObservaciones.Length; vIndex++)
                {
                    txt_sk_194.Text = txt_sk_194.Text + arrObservaciones[vIndex].Observaciones;
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
            string control = "txt_sk_" + indice;                 
            Control[] v1 = this.Controls.Find(control,true);
            if (v1.Length != 0)            
                valor = v1[0].Text;            
            return valor;
        }

        public void LimpiarValorCampo(int indice)
        {
            string control = "txt_SK_" + indice;
            Control[] ctrl = this.Controls.Find(control,true);
            if (this.Controls.Find(control,true)[0] != null)
            {                
                TextBox txtCtrl = ctrl[0] as TextBox;
                txtCtrl.Text = string.Empty;
            }
        }

        public void LimpiarValorHidden(int indice)
        {
            string control = "hdn_" + indice;
            Control[] ctr = Controls.Find(control, true);
            if (Controls.Find(control, true)[0] != null)
            {
                TextBox txtCtrol = ctr[0] as TextBox;
                txtCtrol.Text = string.Empty;
            }
        }

        public void limpiarDatos()
        {
            for (int indCtrlTxt = 140; indCtrlTxt <= 196; indCtrlTxt++)
            {
                LimpiarValorCampo(indCtrlTxt);
                LimpiarValorHidden(indCtrlTxt);
            }
        }

        public void CargarControlesHidden(SK.historicoAdquisicionConceptos.CapturaDatos_HistoricoAdquisicionConceptos[] datoshistorico)
        {
            for (int indCtrlTxt = 140; indCtrlTxt <= 196; indCtrlTxt++)
            {
                var datoHistoricoRecuperado = datoshistorico.Where(dh => dh.Codigo == indCtrlTxt.ToString());
                if (datoHistoricoRecuperado.Count() != 0)
                {
                    Control hiddenFoco = this.Controls["hdn_" + indCtrlTxt];
                    TextBox datoOculto = hiddenFoco as TextBox;
                    datoOculto.Text = string.Empty;
                    if (datoHistoricoRecuperado.Last().Texto != null)
                        datoOculto.Text = datoHistoricoRecuperado.Last().Texto;
                    else
                        datoOculto.Text = datoHistoricoRecuperado.Last().Valor.ToString();
                }
            }
        }

        #endregion
                     
        #region focosTxt

        private void txt_sk_140_Enter(object sender, EventArgs e)
        {
            if (txt_sk_140.Text == string.Empty)
                txt_sk_140.Text = hdn_140.Text;
        }
        
        private void txt_sk_141_Enter(object sender, EventArgs e)
            {
            if (txt_sk_141.Text == string.Empty)
                txt_sk_141.Text = hdn_141.Text;
        }

        private void txt_sk_142_Enter(object sender, EventArgs e)
        {
            if (txt_sk_142.Text == string.Empty)
                txt_sk_142.Text = hdn_142.Text;
        }

        private void txt_sk_143_Enter(object sender, EventArgs e)
        {
            if (txt_sk_143.Text == string.Empty)
                txt_sk_143.Text = hdn_143.Text;
        }

        private void txt_sk_144_Enter(object sender, EventArgs e)
        {
            if (txt_sk_144.Text == string.Empty)
                txt_sk_144.Text = hdn_144.Text;
        }

        private void txt_sk_145_Enter(object sender, EventArgs e)
        {
            if (txt_sk_145.Text == string.Empty)
                txt_sk_145.Text = hdn_145.Text;
        }

        private void txt_sk_146_Enter(object sender, EventArgs e)
        {
            if (txt_sk_146.Text == string.Empty)
                txt_sk_146.Text = hdn_146.Text;
        }

        private void txt_sk_147_Enter(object sender, EventArgs e)
        {
            if (txt_sk_147.Text == string.Empty)
                txt_sk_147.Text = hdn_147.Text;
        }

        private void txt_sk_148_Enter(object sender, EventArgs e)
        {
            if (txt_sk_148.Text == string.Empty)
                txt_sk_148.Text = hdn_148.Text;
        }

        private void txt_sk_149_Enter(object sender, EventArgs e)
        {        
            if (txt_sk_149.Text == string.Empty)
                txt_sk_149.Text = hdn_149.Text;        
        }

        private void txt_sk_150_Enter(object sender, EventArgs e)
        {
            if (txt_sk_150.Text == string.Empty)
                txt_sk_150.Text = hdn_150.Text; 
        }

        private void txt_sk_151_Enter(object sender, EventArgs e)
        {
            if (txt_sk_151.Text == string.Empty)
                txt_sk_151.Text = hdn_151.Text; 
        }

        private void txt_sk_152_Enter(object sender, EventArgs e)
        {
            if (txt_sk_152.Text == string.Empty)
                txt_sk_152.Text = hdn_152.Text; 
        }

        private void txt_sk_153_Enter(object sender, EventArgs e)
        {
            if (txt_sk_153.Text == string.Empty)
                txt_sk_153.Text = hdn_153.Text; 
        }

        private void txt_sk_154_Enter(object sender, EventArgs e)
        {
            if (txt_sk_154.Text == string.Empty)
                txt_sk_154.Text = hdn_154.Text; 
        }

        private void txt_sk_155_Enter(object sender, EventArgs e)
        {
            if (txt_sk_155.Text == string.Empty)
                txt_sk_155.Text = hdn_155.Text;
        }

        private void txt_sk_156_Enter(object sender, EventArgs e)
        {
            if (txt_sk_156.Text == string.Empty)
                txt_sk_156.Text = hdn_156.Text; 
        }

        private void txt_sk_157_Enter(object sender, EventArgs e)
        {
            if (txt_sk_157.Text == string.Empty)
                txt_sk_157.Text = hdn_157.Text; 
        }

        private void txt_sk_158_Enter(object sender, EventArgs e)
        {
            if (txt_sk_158.Text == string.Empty)
                txt_sk_158.Text = hdn_158.Text; 
        }

        private void txt_sk_159_Enter(object sender, EventArgs e)
        {
            if (txt_sk_159.Text == string.Empty)
                txt_sk_159.Text = hdn_159.Text; 
        }

        private void txt_sk_160_Enter(object sender, EventArgs e)
        {
            if (txt_sk_160.Text == string.Empty)
                txt_sk_160.Text = hdn_160.Text; 
        }

        private void txt_sk_161_Enter(object sender, EventArgs e)
        {
            if (txt_sk_161.Text == string.Empty)
                txt_sk_161.Text = hdn_161.Text; 
        }

        private void txt_sk_162_Enter(object sender, EventArgs e)
        {
            if (txt_sk_162.Text == string.Empty)
                txt_sk_162.Text = hdn_162.Text; 
        }

        private void txt_sk_163_Enter(object sender, EventArgs e)
        {
            if (txt_sk_163.Text == string.Empty)
                txt_sk_163.Text = hdn_163.Text; 
        }

        private void txt_sk_164_Enter(object sender, EventArgs e)
        {
            if (txt_sk_164.Text == string.Empty)
                txt_sk_164.Text = hdn_164.Text; 
        }

        private void txt_sk_165_Enter(object sender, EventArgs e)
        {
            if (txt_sk_165.Text == string.Empty)
                txt_sk_165.Text = hdn_165.Text; 
        }

        private void txt_sk_166_Enter(object sender, EventArgs e)
        {
            if (txt_sk_166.Text == string.Empty)
                txt_sk_166.Text = hdn_166.Text; 
        }

        private void txt_sk_167_Enter(object sender, EventArgs e)
        {
            if (txt_sk_167.Text == string.Empty)
                txt_sk_167.Text = hdn_167.Text; 
        }

        private void txt_sk_168_Enter(object sender, EventArgs e)
        {
            if (txt_sk_168.Text == string.Empty)
                txt_sk_168.Text = hdn_168.Text; 
        }

        private void txt_sk_169_Enter(object sender, EventArgs e)
        {
            if (txt_sk_169.Text == string.Empty)
                txt_sk_169.Text = hdn_169.Text; 
        }

        private void txt_sk_170_Enter(object sender, EventArgs e)
        {
            if (txt_sk_170.Text == string.Empty)
                txt_sk_170.Text = hdn_170.Text; 
        }

        private void txt_sk_171_Enter(object sender, EventArgs e)
        {
            if (txt_sk_171.Text == string.Empty)
                txt_sk_171.Text = hdn_171.Text; 
        }

        private void txt_sk_172_Enter(object sender, EventArgs e)
        {
            if (txt_sk_172.Text == string.Empty)
                txt_sk_172.Text = hdn_172.Text; 
        }

        private void txt_sk_173_Enter(object sender, EventArgs e)
        {
            if (txt_sk_173.Text == string.Empty)
                txt_sk_173.Text = hdn_173.Text; 
        }

        private void txt_sk_174_Enter(object sender, EventArgs e)
        {
            if (txt_sk_174.Text == string.Empty)
                txt_sk_174.Text = hdn_174.Text; 
        }

        private void txt_sk_175_Enter(object sender, EventArgs e)
        {
            if (txt_sk_175.Text == string.Empty)
                txt_sk_175.Text = hdn_175.Text; 
        }

        private void txt_sk_176_Enter(object sender, EventArgs e)
        {
            if (txt_sk_176.Text == string.Empty)
                txt_sk_176.Text = hdn_176.Text; 
        }

        private void txt_sk_177_Enter(object sender, EventArgs e)
        {
            if (txt_sk_177.Text == string.Empty)
                txt_sk_177.Text = hdn_177.Text; 
        }

        private void txt_sk_178_Enter(object sender, EventArgs e)
        {
            if (txt_sk_178.Text == string.Empty)
                txt_sk_178.Text = hdn_178.Text; 
        }

        private void txt_sk_179_Enter(object sender, EventArgs e)
        {
            if (txt_sk_179.Text == string.Empty)
                txt_sk_179.Text = hdn_179.Text; 
        }

        private void txt_sk_180_Enter(object sender, EventArgs e)
        {
            if (txt_sk_180.Text == string.Empty)
                txt_sk_180.Text = hdn_180.Text; 
        }

        private void txt_sk_181_Enter(object sender, EventArgs e)
        {
            if (txt_sk_181.Text == string.Empty)
                txt_sk_181.Text = hdn_181.Text; 
        }

        private void txt_sk_182_Enter(object sender, EventArgs e)
        {
            if (txt_sk_182.Text == string.Empty)
                txt_sk_182.Text = hdn_182.Text; 
        }

        private void txt_sk_183_Enter(object sender, EventArgs e)
        {
            if (txt_sk_183.Text == string.Empty)
                txt_sk_183.Text = hdn_183.Text; 
        }

        private void txt_sk_184_Enter(object sender, EventArgs e)
        {
            if (txt_sk_184.Text == string.Empty)
                txt_sk_184.Text = hdn_184.Text; 
        }

        private void txt_sk_185_Enter(object sender, EventArgs e)
        {
            if (txt_sk_185.Text == string.Empty)
                txt_sk_185.Text = hdn_185.Text; 
        }

        private void txt_sk_186_Enter(object sender, EventArgs e)
        {
            if (txt_sk_186.Text == string.Empty)
                txt_sk_186.Text = hdn_186.Text; 
        }

        private void txt_sk_187_Enter(object sender, EventArgs e)
        {
            if (txt_sk_187.Text == string.Empty)
                txt_sk_187.Text = hdn_187.Text; 
        }

        private void txt_sk_188_Enter(object sender, EventArgs e)
        {
            if (txt_sk_188.Text == string.Empty)
                txt_sk_188.Text = hdn_188.Text; 
        }

        private void txt_sk_189_Enter(object sender, EventArgs e)
        {
            if (txt_sk_189.Text == string.Empty)
                txt_sk_189.Text = hdn_189.Text; 
        }

        private void txt_sk_190_Enter(object sender, EventArgs e)
        {
            if (txt_sk_190.Text == string.Empty)
                txt_sk_190.Text = hdn_190.Text; 
        }

        private void txt_sk_191_Enter(object sender, EventArgs e)
        {
            if (txt_sk_191.Text == string.Empty)
                txt_sk_191.Text = hdn_191.Text; 
        }

        private void txt_sk_192_Enter(object sender, EventArgs e)
        {
            if (txt_sk_192.Text == string.Empty)
                txt_sk_192.Text = hdn_192.Text; 
        }

        private void txt_sk_193_Enter(object sender, EventArgs e)
        {
            if (txt_sk_193.Text == string.Empty)
                txt_sk_193.Text = hdn_193.Text;
        }

        private void txt_sk_196_Enter(object sender, EventArgs e)
        {
            if (txt_sk_196.Text == string.Empty)
                txt_sk_196.Text = hdn_196.Text;
        }

        #endregion

        #region pulsaciones

        private void txt_sk_140_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }             
        }

        private void txt_sk_141_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }             
        }

        private void txt_sk_142_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }             
        }

        private void txt_sk_143_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void txt_sk_144_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }             
        }

        private void txt_sk_146_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }             
        }

        private void txt_sk_148_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }             
        }

        private void txt_sk_145_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }             
        }

        private void txt_sk_147_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }             
        }

        private void txt_sk_149_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }             
        }

        private void txt_sk_150_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }             
        }

        private void txt_sk_151_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }             
        }

        private void txt_sk_152_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }             
        }

        private void txt_sk_153_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }             
        }

        private void txt_sk_154_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }             
        }

        

        private void txt_sk_156_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }             
        }

        private void txt_sk_155_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }             
        }

        private void txt_sk_157_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }             
        }

        private void txt_sk_158_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }             
        }

        private void txt_sk_159_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }             
        }

        private void txt_sk_160_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }             
        }

        private void txt_sk_161_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }             
        }

        private void txt_sk_162_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }             
        }

        private void txt_sk_163_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }             
        }

        private void txt_sk_164_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }             
        }

        private void txt_sk_165_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }             
        }

        private void txt_sk_166_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }             
        }

        private void txt_sk_167_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }             
        }

        private void txt_sk_168_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }             
        }

        private void txt_sk_169_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }             
        }

        private void txt_sk_170_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }             
        }

        private void txt_sk_171_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }             
        }

        private void txt_sk_172_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }             
        }

        private void txt_sk_173_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }             
        }

        private void txt_sk_174_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }             
        }

        private void txt_sk_175_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }             
        }

        private void txt_sk_176_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }             
        }

        private void txt_sk_177_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void txt_sk_178_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }             
        }

        private void txt_sk_179_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }             
        }

        private void txt_sk_180_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }             
        }

        private void txt_sk_181_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }             
        }

        private void txt_sk_182_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }             
        }

        private void txt_sk_183_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }             
        }

        private void txt_sk_184_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }     

        }

        private void txt_sk_185_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }   
        }

        private void txt_sk_186_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }   
        }

        private void txt_sk_187_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }   
        }

        private void txt_sk_188_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }   
        }

        private void txt_sk_189_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }   
        }

        private void txt_sk_190_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }   
        }

        private void txt_sk_191_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }   
        }

        private void txt_sk_192_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }   
        }

        private void txt_sk_193_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }   
        }

        private void txt_sk_194_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void txt_sk_196_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }
        }
               

        #endregion

        #region cambioTexto

        private void txt_sk_140_TextChanged(object sender, EventArgs e)
        {
            modificadoDatos = true;
        }

        private void txt_sk_141_TextChanged(object sender, EventArgs e)
        {
            modificadoDatos = true;
        }

        private void txt_sk_142_TextChanged(object sender, EventArgs e)
        {
            modificadoDatos = true;
        }

        private void txt_sk_143_TextChanged(object sender, EventArgs e)
        {
            modificadoDatos = true;
        }

        private void txt_sk_144_TextChanged(object sender, EventArgs e)
        {
            modificadoDatos = true;
        }

        private void txt_sk_146_TextChanged(object sender, EventArgs e)
        {
            modificadoDatos = true;
        }

        private void txt_sk_148_TextChanged(object sender, EventArgs e)
        {
            modificadoDatos = true;
        }

        private void txt_sk_145_TextChanged(object sender, EventArgs e)
        {
            modificadoDatos = true;
        }

        private void txt_sk_147_TextChanged(object sender, EventArgs e)
        {
            modificadoDatos = true;
        }

        private void txt_sk_149_TextChanged(object sender, EventArgs e)
        {
            modificadoDatos = true;
        }

        private void txt_sk_150_TextChanged(object sender, EventArgs e)
        {
            modificadoDatos = true;
        }

        private void txt_sk_151_TextChanged(object sender, EventArgs e)
        {
            modificadoDatos = true;
        }

        private void txt_sk_152_TextChanged(object sender, EventArgs e)
        {
            modificadoDatos = true;
        }

        private void txt_sk_153_TextChanged(object sender, EventArgs e)
        {
            modificadoDatos = true;
        }

        private void txt_sk_154_TextChanged(object sender, EventArgs e)
        {
            modificadoDatos = true;
        }

        private void txt_sk_155_TextChanged(object sender, EventArgs e)
        {
            modificadoDatos = true;
        }

        private void txt_sk_156_TextChanged(object sender, EventArgs e)
        {
            modificadoDatos = true;
        }

        private void txt_sk_157_TextChanged(object sender, EventArgs e)
        {
            modificadoDatos = true;
        }

        private void txt_sk_158_TextChanged(object sender, EventArgs e)
        {
            modificadoDatos = true;
        }

        private void txt_sk_159_TextChanged(object sender, EventArgs e)
        {
            modificadoDatos = true;
        }

        private void txt_sk_160_TextChanged(object sender, EventArgs e)
        {
            modificadoDatos = true;
        }

        private void txt_sk_161_TextChanged(object sender, EventArgs e)
        {
            modificadoDatos = true;
        }

        private void txt_sk_162_TextChanged(object sender, EventArgs e)
        {
            modificadoDatos = true;
        }

        private void txt_sk_163_TextChanged(object sender, EventArgs e)
        {
            modificadoDatos = true;
        }

        private void txt_sk_164_TextChanged(object sender, EventArgs e)
        {
            modificadoDatos = true;
        }

        private void txt_sk_165_TextChanged(object sender, EventArgs e)
        {
            modificadoDatos = true;
        }

        private void txt_sk_166_TextChanged(object sender, EventArgs e)
        {
            modificadoDatos = true;
        }

        private void txt_sk_167_TextChanged(object sender, EventArgs e)
        {
            modificadoDatos = true;
        }

        private void txt_sk_168_TextChanged(object sender, EventArgs e)
        {
            modificadoDatos = true;
        }

        private void txt_sk_169_TextChanged(object sender, EventArgs e)
        {
            modificadoDatos = true;
        }

        private void txt_sk_170_TextChanged(object sender, EventArgs e)
        {
            modificadoDatos = true;
        }

        private void txt_sk_171_TextChanged(object sender, EventArgs e)
        {
            modificadoDatos = true;
        }

        private void txt_sk_172_TextChanged(object sender, EventArgs e)
        {
            modificadoDatos = true;
        }

        private void txt_sk_173_TextChanged(object sender, EventArgs e)
        {
            modificadoDatos = true;
        }

        private void txt_sk_174_TextChanged(object sender, EventArgs e)
        {
            modificadoDatos = true;
        }

        private void txt_sk_175_TextChanged(object sender, EventArgs e)
        {
            modificadoDatos = true;
        }

        private void txt_sk_176_TextChanged(object sender, EventArgs e)
        {
            modificadoDatos = true;
        }

        private void txt_sk_177_TextChanged(object sender, EventArgs e)
        {
            modificadoDatos = true;
        }

        private void txt_sk_178_TextChanged(object sender, EventArgs e)
        {
            modificadoDatos = true;
        }

        private void txt_sk_179_TextChanged(object sender, EventArgs e)
        {
            modificadoDatos = true;
        }

        private void txt_sk_180_TextChanged(object sender, EventArgs e)
        {
            modificadoDatos = true;
        }

        private void txt_sk_181_TextChanged(object sender, EventArgs e)
        {
            modificadoDatos = true;
        }

        private void txt_sk_182_TextChanged(object sender, EventArgs e)
        {
            modificadoDatos = true;
        }

        private void txt_sk_183_TextChanged(object sender, EventArgs e)
        {
            modificadoDatos = true;
        }

        private void txt_sk_184_TextChanged(object sender, EventArgs e)
        {
            modificadoDatos = true;
        }

        private void txt_sk_185_TextChanged(object sender, EventArgs e)
        {
            modificadoDatos = true;
        }

        private void txt_sk_186_TextChanged(object sender, EventArgs e)
        {
            modificadoDatos = true;
        }

        private void txt_sk_187_TextChanged(object sender, EventArgs e)
        {
            modificadoDatos = true;
        }

        private void txt_sk_188_TextChanged(object sender, EventArgs e)
        {
            modificadoDatos = true;
        }

        private void txt_sk_189_TextChanged(object sender, EventArgs e)
        {
            modificadoDatos = true;
        }

        private void txt_sk_190_TextChanged(object sender, EventArgs e)
        {
            modificadoDatos = true;
        }

        private void txt_sk_191_TextChanged(object sender, EventArgs e)
        {
            modificadoDatos = true;
        }

        private void txt_sk_192_TextChanged(object sender, EventArgs e)
        {
            modificadoDatos = true;
        }

        private void txt_sk_193_TextChanged(object sender, EventArgs e)
        {
            modificadoDatos = true;
        }

        private void txt_sk_194_TextChanged(object sender, EventArgs e)
        {
            modificadoDatos = true;
        }

        private void txt_sk_195_TextChanged(object sender, EventArgs e)
        {
            modificadoDatos = true;
        }

        private void txt_sk_196_TextChanged(object sender, EventArgs e)
        {
            modificadoDatos = true;
        }

        #endregion

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox9_Enter(object sender, EventArgs e)
        {

        }

       
     

    }
}
