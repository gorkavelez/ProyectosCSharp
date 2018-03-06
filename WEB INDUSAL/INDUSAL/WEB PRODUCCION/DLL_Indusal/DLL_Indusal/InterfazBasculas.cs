using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Ports;

namespace DLL_Indusal
{
    public class InterfazBasculas
    {
        private SerialPort bascula;         // objeto para realizar la conexión a través del puerto serie

        // parámetros para la conexión

        // variables
        private string _portName = "COM1";
        private int _retardo = 250;
        private string _marcaEstable = "A";
        private string _marcaCero = "I";
        private int _iteraciones = 10;

        // propiedades
        public string PortName
        {
            get { return _portName; }
            set { _portName = value; }
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

        public InterfazBasculas()
        {
            bascula = new SerialPort(_portName);

            try
            {
                if (!bascula.IsOpen)
                    bascula.Open();
            }
            catch (Exception)
            { }
        }

        public string GetPeso()
        {
            bool ok = false;
            string temp;
            decimal dValor = 0;
            int nIteracion = 0;
            string marcaEncontrada;

            try
            {
                while ((!ok) && (nIteracion++ < _iteraciones))
                {
                    temp = "";
                    marcaEncontrada = "";

                    while ((marcaEncontrada == "") && (nIteracion++ < _iteraciones))
                    {
                        // se introduce un retardo para dar tiempo a que lea desde el puerto COM
                        System.Threading.Thread.Sleep(_retardo);
                        temp = bascula.ReadExisting();
                        // se busca alguna de las dos marcas de resultado en la traza obtenida de la lectura
                        if (ExisteMarca(temp, _marcaEstable))
                            marcaEncontrada = _marcaEstable;
                        else
                        {
                            if (ExisteMarca(temp, _marcaCero))
                                marcaEncontrada = _marcaCero;
                        }
                    }

                    // si ha salido del bucle de lecturas por haber alcanzado el tope de iteraciones, se termina
                    // la ejecución, indicando el error en el valor de retorno.

                    if (marcaEncontrada == _marcaCero)
                    {
                        dValor = 0;
                        ok = true;
                    }
                    else
                    {
                        if (marcaEncontrada == _marcaEstable)
                        {
                            string[] aTemp = temp.Split(' ');
                            for (int i = 0; i < aTemp.Length && !ok; i++)
                            {
                                try
                                {
                                    if ((aTemp[i].Substring(aTemp[i].Length - 1, 1) == marcaEncontrada
                                        ))
                                    {
                                        int posBarra = aTemp[i].IndexOf('\r');
                                        if (posBarra > 1)
                                        {
                                            string strValor = aTemp[i].Substring(0, posBarra);
                                            dValor = decimal.Parse(strValor);// / 10;
                                            dValor /= 10;
                                            ok = true;

                                        }
                                    }
                                }
                                catch (Exception)
                                {
                                    dValor = 0;
                                }
                            }
                            ok = true;
                        }
                        else
                        {
                            dValor = 0;
                            ok = false;
                        }
                    }

                }
            }
            catch (Exception)
            {
                dValor = 0;
            }
            // se cierra la conexión con el puerto COM, porque si no fallará en la siguiente llamada.
            bascula.Close();
            return (dValor.ToString());
        }

        private bool ExisteMarca(string recibido, string marca)
        {
            try
            {
                int posMarca = recibido.IndexOf(marca,8);
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
