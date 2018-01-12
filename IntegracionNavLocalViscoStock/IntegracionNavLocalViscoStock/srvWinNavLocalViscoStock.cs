using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace IntegracionNavLocalViscoStock
{
    public partial class srvWinNavLocalViscoStock : ServiceBase
    {
        private System.Timers.Timer miTimer;
        private string msgError;
        public srvWinNavLocalViscoStock()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                miTimer = new System.Timers.Timer();

                //miTimer.Interval = 1000;
                miTimer.Interval = IntegracionNavLocalViscoStock.Properties.Settings.Default.svTiempo;
                miTimer.Elapsed += new System.Timers.ElapsedEventHandler(miTimer_Elapsed);
                miTimer.Enabled = true;
                miTimer.Start();
                //logSrvIntNavLocalViscoStock.WriteEntry("VISCOSTOCK: Integration service STARTED");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected override void OnStop()
        {
            try
            {
                //logSrvIntNavLocalViscoStock.WriteEntry("VISCOSTOCK: Integration service STOPPED");
                miTimer.Stop();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void miTimer_Elapsed(Object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                lanzadorIntegracion.lanzadorIntegracion lanzador = new lanzadorIntegracion.lanzadorIntegracion();
                lanzador.lanzar(ref msgError);
                //if (msgError != "")
                    //logSrvIntNavLocalViscoStock.WriteEntry("VISCOSTOCK: Integration Error: " + msgError, EventLogEntryType.Error);
                    //logSrvIntNavLocalViscoStock.WriteEntry("VISCOSTOCK: Integration Error: " + msgError);                

                //**INI ES-6411 Running stock report GVE
                lanzador.RunColaTrabajo(ref msgError);
                //**FIN ES-6411 Running stock report GVE
            }
            catch (Exception ex)
            {
                //logSrvIntNavLocalViscoStock.WriteEntry("VISCOSTOCK: Integration Error: " + ex.ToString(), EventLogEntryType.Error);
                //logSrvIntNavLocalViscoStock.WriteEntry("VISCOSTOCK: Integration Error: " + ex.ToString());
            }
        }
        private void logSrvIntNavLocalViscoStock_EntryWritten(object sender, EntryWrittenEventArgs e)
        {

        }
    }
}
