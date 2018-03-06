using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO.Ports;
using System.Globalization;

namespace IntranetINDUSAL.Automatizacion
{
    public class cBascula
    {
        #region EVENTOS
            // delegados
            public delegate void DataReceivedHandler(object sender, DataReceivedEventArgs e);

            //eventos
            public event DataReceivedHandler DataReceived;

            // Clase que encapsula los datos que se pasan en el segundo parámetro del evento
            public class DataReceivedEventArgs : EventArgs
            {            

                private string _peso;

                public string Peso
                {
                    get { return _peso; }
                    set { _peso = value; }
                }



                public DataReceivedEventArgs(string peso)
                {
                    this._peso = peso;                
                }

            }

            protected virtual void OnDataReceived(DataReceivedEventArgs e)
            {
                if (DataReceived != null)
                {
                    DataReceived(this, e);
                }
            }

            private void ThrowDataReceivedEvent(string peso)
            {
                DataReceivedEventArgs eArgs = new DataReceivedEventArgs(peso);
                OnDataReceived(eArgs);
            }
        #endregion


        private const string marcaInicio="    ";
        private const string marcaFin="\r";

        private SerialPort puertoCOM;

        private string _peso;

        public string Peso
        {
            get { return _peso; }
            set { _peso = value; }
        }


        public cBascula()
        {
            // al instanciar la clase, se crea el objeto de comunicación por el puerto COM
            
            puertoCOM = new SerialPort("COM1", 9600, Parity.None, 8, StopBits.One);
            puertoCOM.DataReceived += new SerialDataReceivedEventHandler(puertoCOM_DataReceived);
            OpenCOM();
            puertoCOM.Write("$");            
        }

        private void puertoCOM_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {

            //string temp = puertoCOM.ReadExisting();
            string temp = puertoCOM.ReadLine();
            temp = GetValue(temp);
            CloseCOM();
            ThrowDataReceivedEvent(temp);
        }

        public void OpenCOM()
        {
            if (puertoCOM.IsOpen)
                puertoCOM.Close();

            puertoCOM.Open();
        }

        public void CloseCOM()
        {
            puertoCOM.Close();
        }

        public string Read()
        {
            string temp;            

            try
            {
                if(!puertoCOM.IsOpen)
                    puertoCOM.Open();
                
                temp = puertoCOM.ReadExisting();
                temp = GetValue(temp);
            }
            catch (Exception ex)
            {                
                temp = ex.Message;
            }               
            finally
            {
                puertoCOM.Close();
            }

            return (temp);            
        }

        private string FormatNumber(string number)
        {
            NumberFormatInfo info = CultureInfo.CurrentCulture.NumberFormat;
            string decSeparator = info.NumberDecimalSeparator;
            switch(decSeparator)
            {
                case ".":
                    number=number.Replace(',','.');
                    break;
                case ",":
                    number=number.Replace('.', ',');
                    break;
            }
            return (number);
        }

        private string GetValue(string arrayOfValues)
        {
            int posicionInicio;
            int posicionFin;
            string temp;

            posicionInicio = arrayOfValues.IndexOf(marcaInicio)+marcaInicio.Length;
            posicionFin = arrayOfValues.IndexOf(marcaFin);

            if (posicionInicio >= 0 && posicionFin >= posicionInicio)
                temp = arrayOfValues.Substring(posicionInicio, posicionFin - posicionInicio);
            else
                temp = "0";

            return (temp);
        }

    }
}
