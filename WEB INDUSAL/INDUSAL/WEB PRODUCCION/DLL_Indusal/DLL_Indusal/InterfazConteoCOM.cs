using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Ports;

namespace DLL_Indusal
{
    public class InterfazConteoCOM
    {
        private SerialPort automata;         // objeto para realizar la conexión a través del puerto serie

        // parámetros para la conexión

        // variables
        private string _portName = "COM1";
        private int _baudRate = 57600;
        private int _parityValue=2;
        private int _retardo = 250;
        private string _marcaEstable = "A";
        //private string _marcaCeroOlimpia = "0\r\nI";
        //private string _marcaCeroMarcilla = "0\rI";
        private string _marcaCero = "I";
        private int _iteraciones = 10;

        // propiedades
        public string PortName
        {
            get { return _portName; }
            set { _portName = value; }
        }

        public int BaudRate
        {
            get { return _baudRate; }
            set { _baudRate = value; }
        }

        public int ParityValue
        {
            get { return _parityValue; }
            set { _parityValue = value; }
        }

        public int Retardo
        {
            get { return _retardo; }
            set { _retardo = value; }
        }
        
        public string MarcaEstable
        {
            get { return _marcaEstable; }
            set { _marcaEstable = value; }
        }

        public string MarcaCero
        {
            get { return _marcaCero; }
            set { _marcaCero = value; }
        }

        public int Iteraciones
        {
            get { return _iteraciones; }
            set { _iteraciones = value; }
        }

        public InterfazConteoCOM()
        {
            
            automata = new SerialPort(_portName);
            automata.BaudRate = _baudRate;
            automata.Parity = GetParity(_parityValue);

            //bascula.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(bascula_DataReceived);
            try
            {
                if (!automata.IsOpen)
                    automata.Open();
            }
            catch (Exception)
            { }
        }

        private Parity GetParity(int nOption)
        {
            Parity parValue= Parity.None;
            switch (nOption)
            {
                case 0:
                    parValue= Parity.None;
                    break;
                case 1:
                    parValue = Parity.Odd;
                    break;
                case 2:
                    parValue = Parity.Even;
                    break;
                case 3:
                    parValue = Parity.Mark;
                    break;
                case 4:
                    parValue = Parity.Space;
                    break;
            }
            return parValue;
        }
 
        public string Get()
        {
            string stream = "";
            string data = "";

            int posIni = -1;
            int posFin = -1;
            int ciclo = 0;

            try
            {
                while ((posIni >= posFin)&&(ciclo<10))
                {
                    stream = automata.ReadExisting();
                    posIni = stream.IndexOf('#');
                    if (posIni >= 0)
                    {
                        posFin = stream.IndexOf('|', posIni);
                    }
                    System.Threading.Thread.Sleep(_retardo);
                    ciclo++;
                }

                data=stream.Substring(posIni + 1, posFin - posIni - 1);
            }
            catch (Exception)
            { }
            finally
            {
                automata.Close();                
            }

            return (data);
            
        }

        private bool ExisteMarca(string recibido, string marca)
        {
            try
            {
                int posMarca = recibido.IndexOf(marca,8);
                //int posBarra = posMarca - 4;
                return (posMarca > 0);
            }
            catch (Exception)
            {
                return (false);
            }
        }

        private string GetDecSeparator()
        {
            System.Globalization.CultureInfo CI = System.Globalization.CultureInfo.CurrentCulture;
            return (CI.NumberFormat.NumberDecimalSeparator);
        }

    }
}
