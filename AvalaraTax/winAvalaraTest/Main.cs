using Avalara;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

namespace winAvalaraTest
{
    public partial class Main : Form
    {

        entAvalaraConnector con = new entAvalaraConnector();

        public Main()
        {
            InitializeComponent();
        }

        void SetCon()
        {
            con.username = "g.remirez@saltosystems.com";
            con.password = "Avalara2017!";
            con.CompanyCode = "SALTOTEST";
            con.produccion = chkPRO.Checked;
            con.transactionLog = false;
            con.lineLog = false;
            con.console = false;
            //this.filesPath = this.filesPath + @"test\";
            con.LogPath = con.filesPath + @"log_avalara.txt";

            //General.CreateFileAddLine("Avalara (" + con.produccion + ") Inicio", con.LogPath, true);
            con.Connect();
        }

        //private void ShowOffset(DateTime time, TimeZoneInfo timeZone)
        //{
        //    DateTime convertedTime = time;
        //    TimeSpan offset;

        //    if (time.Kind == DateTimeKind.Local && !timeZone.Equals(TimeZoneInfo.Local))
        //        convertedTime = TimeZoneInfo.ConvertTime(time, TimeZoneInfo.Local, timeZone);
        //    else if (time.Kind == DateTimeKind.Utc && !timeZone.Equals(TimeZoneInfo.Utc))
        //        convertedTime = TimeZoneInfo.ConvertTime(time, TimeZoneInfo.Utc, timeZone);

        //    offset = timeZone.GetUtcOffset(time);
        //    if (time == convertedTime)
        //    {
        //        Console.WriteLine("{0} {1} ", time,
        //                          timeZone.IsDaylightSavingTime(time) ? timeZone.DaylightName : timeZone.StandardName);
        //        Console.WriteLine("   It differs from UTC by {0} hours, {1} minutes.",
        //                           offset.Hours,
        //                           offset.Minutes);
        //    }
        //    else
        //    {
        //        Console.WriteLine("{0} {1} ", time,
        //                          time.Kind == DateTimeKind.Utc ? "UTC" : TimeZoneInfo.Local.Id);
        //        Console.WriteLine("   converts to {0} {1}.",
        //                          convertedTime,
        //                          timeZone.Id);
        //        Console.WriteLine("   It differs from UTC by {0} hours, {1} minutes.",
        //                          offset.Hours, offset.Minutes);
        //    }
        //    Console.WriteLine();
        //}

        private void cmdGetDocNo_Click(object sender, EventArgs e)
        {
             try
             {
                SetCon();
                //var trans = con.GetDocNo(cbTransactions.Text);
                var trans = con.GetDocNo(txtDoc.Text);
                if (trans != null)
                    txtResp.Text = trans.ToString();
                else
                    txtResp.Text = "No se ha podido recuperar la transacción";
             }
             catch (Avalara.AvaTax.RestClient.AvaTaxError AvaError)
             {
                 if (AvaError.error != null)
                     MessageBox.Show("ERROR: " + AvaError.error.ToString());
                 else
                     MessageBox.Show("ERROR: " + AvaError.Data);
             }
             catch (Exception ex)
             {
                 MessageBox.Show("ERROR: " + ex.Message.ToString());
             }
        }

        private void cmdVoid_Click(object sender, EventArgs e)
        {
             try
            {
                SetCon();
                con.VoidDocNo(cbTransactions.Text);
            }
             catch (Avalara.AvaTax.RestClient.AvaTaxError AvaError)
             {
                 MessageBox.Show("ERROR: " + AvaError.error.ToString());
             }
             catch (Exception ex)
             {
                 MessageBox.Show("ERROR: " + ex.Message.ToString());
             }
        }

        private void cmdCOMMIT_Click(object sender, EventArgs e)
        {
             try
             {
                SetCon();
                var trans = con.CommitDocNo(cbTransactions.Text,true);
                txtResp.Text = trans.ToString();
             }
             catch (Avalara.AvaTax.RestClient.AvaTaxError AvaError)
             {
                 MessageBox.Show("ERROR: " + AvaError.error.ToString());
             }
             catch (Exception ex)
             {
                 MessageBox.Show("ERROR: " + ex.Message.ToString());
             }
        }

        private void cmdUNCOMMIT_Click(object sender, EventArgs e)
        {
            try
            {
                SetCon();
                var trans = con.CommitDocNo(cbTransactions.Text, false);
                txtResp.Text = trans.ToString();
            }
            catch (Avalara.AvaTax.RestClient.AvaTaxError AvaError)
            {
                MessageBox.Show("ERROR: " + AvaError.error.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.Message.ToString());
            }
        }
        
        private void cmdADJUST_Click(object sender, EventArgs e)
        {
            try
            {
                SetCon();
                con.trans = new Avalara.AvaTax.RestClient.TransactionModel();
                con.trans = con.GetDocNo(cbTransactions.Text);
                var transact = con.AdjustDocNo(cbTransactions.Text);
            }
            catch (Avalara.AvaTax.RestClient.AvaTaxError AvaError)
            {
                MessageBox.Show("ERROR: " + AvaError.error.ToString());
            } catch (Exception ex) {
                MessageBox.Show("ERROR: "+ex.Message.ToString());           
            }
            
        }

        private void cmdDELETE_Click(object sender, EventArgs e)
        {
            try
            {
                SetCon();
                var trans = con.GetDocNo(cbTransactions.Text);
                con.DeleteDocNo(cbTransactions.Text);
            } catch (Avalara.AvaTax.RestClient.AvaTaxError AvaError) {

                MessageBox.Show("ERROR: " + AvaError.error.ToString());

            } catch (Exception ex) {
                MessageBox.Show("ERROR: "+ex.Message.ToString());
            }

        }

        private void GetTransactionsFillCombo() {
    
            try
            {
                SetCon();
                var trans = con.GetAllTransactions();
                cbTransactions.Items.Clear();
                lblTotalTrans.Text = trans.count.ToString();
                
                foreach (Avalara.AvaTax.RestClient.TransactionModel item in trans.value)
                {
                    cbTransactions.Items.Add(item);
                    //cbTransactions.Items.Add(item.code + " " + item.version);
                    dgData.Rows.Add(item.code + " - v"+item.version+" - Status:"+item.status.ToString() +" - Id:"+item.id);
  
                }
                cbTransactions.DisplayMember = "code";
                //cbTransactions.ValueMember = "code";
                
            }
            catch (Avalara.AvaTax.RestClient.AvaTaxError AvaError)
            {

                MessageBox.Show("ERROR: " + AvaError.error.ToString());

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.Message.ToString());
            }
    
        }

        private void Main_Load(object sender, EventArgs e)
        {
            //GetTransactionsFillCombo();
        }

        private void cmdNEWCODE_Click(object sender, EventArgs e)
        {
            try
            {
                SetCon();
                con.ChangeDocNo(cbTransactions.Text,txtNewDocCode.Text);
            }
            catch (Avalara.AvaTax.RestClient.AvaTaxError AvaError)
            {

                MessageBox.Show("ERROR: " + AvaError.error.ToString());

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.Message.ToString());
            }
        }

        private void btnHORA_Click(object sender, EventArgs e)
        {
            //TimeZoneInfo.ConvertTimeToUtc(DateTime.Now);
            
            //if (System.TimeZone.CurrentTimeZone == System.TimeZone
            GetHora();
            
        }

        public void GetHora()
        {
            //6/7/2017 7:47:43 PM 
            //08/06/2017 9:54:13 

            CultureInfo enUS = new CultureInfo("en-US");



            string normalDate = "";
            if (CultureInfo.CurrentCulture.ToString() == "es-ES") //En server2 - "en-GB"
            {
                normalDate = System.DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            }
            if (CultureInfo.CurrentCulture.ToString() == "us-US") //En server2 - "en-GB"
            {
                normalDate = System.DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                //Fecha_tratamiento.ToString("dd/MM/yyyy");
            }
            MessageBox.Show(CultureInfo.CurrentCulture.ToString() + " - " + normalDate);

            normalDate = System.DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss", enUS);

            MessageBox.Show(enUS.ToString() + " - " + normalDate);


            
            //return strData;
        }

        public string SetDecimal(string strData)
        {
            if (System.Globalization.CultureInfo.CurrentCulture.ToString() == "es-ES") //En server2 - "en-GB"
            {
                strData = strData.Replace(".", ",");
            }
            return strData;
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            GetTransactionsFillCombo();
        }

        //public enum AdjustmentReason
        //{
        //    /// <summary>
        //    /// 
        //    /// </summary>
        //    NotAdjusted,

        //    /// <summary>
        //    /// 
        //    /// </summary>
        //    SourcingIssue,

        //    /// <summary>
        //    /// 
        //    /// </summary>
        //    ReconciledWithGeneralLedger,

        //    /// <summary>
        //    /// 
        //    /// </summary>
        //    ExemptCertApplied,

        //    /// <summary>
        //    /// 
        //    /// </summary>
        //    PriceAdjusted,

        //    /// <summary>
        //    /// 
        //    /// </summary>
        //    ProductReturned,

        //    /// <summary>
        //    /// 
        //    /// </summary>
        //    ProductExchanged,

        //    /// <summary>
        //    /// 
        //    /// </summary>
        //    BadDebt,

        //    /// <summary>
        //    /// 
        //    /// </summary>
        //    Other,

        //    /// <summary>
        //    /// 
        //    /// </summary>
        //    Offline,

        //}

    }
}

