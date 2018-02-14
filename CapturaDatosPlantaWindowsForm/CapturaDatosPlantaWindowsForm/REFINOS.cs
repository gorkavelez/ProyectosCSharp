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
    public partial class REFINOS : Form
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
        
        public REFINOS()
        {
            InitializeComponent();
        }

        private void REFINOS_Load(object sender, EventArgs e)
        {
            txtDia.Text = DateTime.Today.ToString("dd/MM/yy");
            TxtHora.Text = DateTime.Now.ToString("HH:00");
            modificadoDatos = false;
            this.Text = sabana;
            lblSabana.Text = this.Text;
            //txt_sk_1.Focus();

            //DíaPapelero
            int horaNow = int.Parse(DateTime.Now.ToString("HH:00").Split(':')[0]);
            if (horaNow >= 0 & horaNow <= 05)
            {
                txtDia.Text = DateTime.Now.Date.AddDays(-1).ToString("dd/MM/yy");
            }
            lblUsuario.Text = "Usuario: " + usuario;
            CargarTodoFormularioDatosNav();
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
                int procesoObsercaciones = 0;

                //guardar registro de observaciones
                int procesoObservaciones = 0;
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
                                          ref procesoObsercaciones,
                                          obserTot);
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
                //string filtrofecha = txtDia.Text;
                string filtrofecha = txtDia.Text.Split('/')[1] + "/" + txtDia.Text.Split('/')[0] + "/" + txtDia.Text.Split('/')[2];
                //string filtrofecha = txtDia.Text.Split('-')[1] + "-" + txtDia.Text.Split('-')[0] + "-" + txtDia.Text.Split('-')[2];
                DateTime hora = Convert.ToDateTime(TxtHora.Text);

                SK.historicoAdquisicionConceptos.CapturaDatos_HistoricoAdquisicionConceptos[] datoshistorico = null;
                string filtrohoraJusta = TxtHora.Text;
                funcionesProxy.traerRegistrosUltimaHora(_sabana, filtrofecha, filtrohoraJusta, ref datoshistorico);                
                if (datoshistorico.Count() == 0)
                {
                    //si no hay registros con la misma hora a la que nos movemos sacará los datos de uno en uno.
                    datoshistorico = null;
                    string filtrohoraAnt = hora.AddHours(-1).ToString("HH:00") + ".." + TxtHora.Text;
                    funcionesProxy.traerRegistrosUltimaHora(sabana, filtrofecha, filtrohoraAnt, ref datoshistorico);
                    if (datoshistorico.Count() != 0)
                        CargarControlesHidden(datoshistorico);                    
                }
                else
                {                    
                    for (int indCtrlTxt = 1; indCtrlTxt <= 5; indCtrlTxt++)
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
        
    }
}
