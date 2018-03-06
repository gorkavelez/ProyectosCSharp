using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

namespace IntranetINDUSAL.Reports
{
    public class cPrintDocument
    {
        public enum eTipoDocumento
        {
            carroTransporte,
            carroLavado,
            carroIncompleto,
            carroOxido,
            paqueteProducto
        }

        private eTipoDocumento _tipoDocumento;

        //private string _empresaLogin;

        //private string _idEquipo;

        private const string separadorArgumentos = " ";
        private const string keyEspacioBlanco = "SP;";
        private const string EspacioBlanco = "#";

        private const string keyTipoEtiqueta = "TE;";
        
        private const string keyCodCliente = "CC;";
        private const string keyNomCliente = "NC;";

        private const string keyNumPedido = "NP;";

        private const string keyCodProducto = "CP;";
        private const string keyNomProducto = "DP;";

        private const string keyNumCarro = "NR;";

        private const string keyLiteral = "LT;";

        private const string keyCopias = "NN;";

        private const string keyDimensiones = "TM;";
        
        private string _codCliente;
        private string _nomCliente;
        private string _numPedido;
        private string _codProducto;
        private string _nomProducto;
        private int _nCarro;

        private string _argumentString;

        public string ArgumentString
        {
            get { return _argumentString; }
            set { _argumentString = value; }
        }

        public eTipoDocumento TipoDocumento
        {
            get { return _tipoDocumento; }
            set { _tipoDocumento = value; }
        }

        public string CodCliente
        {
            get { return _codCliente; }
            set { _codCliente = value; }
        }

        public string NomCliente
        {
            get { return _nomCliente; }
            set { _nomCliente = value; }
        }

        public string NumPedido
        {
            get { return _numPedido; }
            set { _numPedido = value; }
        }

        public string CodProducto
        {
            get { return _codProducto; }
            set { _codProducto = value; }
        }

        public string NomProducto
        {
            get { return _nomProducto; }
            set { _nomProducto = value; }
        }

        public int NCarro
        {
            get { return _nCarro; }
            set { _nCarro = value; }
        }

        public cPrintDocument(string empresa)
        {
            //this._empresaLogin = empresa;
        }

        public cPrintDocument()
        {  
        }

        public Exception Print(int nCopias)
        {
            Exception ex = null;
            StringBuilder commandArgument = new StringBuilder();
            switch (_tipoDocumento)
            {
                case eTipoDocumento.carroIncompleto:
                    //ex = oProduccion.RegistrarEtiquetaCarroIncompleto(_idEquipo, _codCliente, _nomCliente, _numPedido, nCopias, _nCarro, _tipoDocumento.ToString());
                    commandArgument.Append(keyEspacioBlanco + EspacioBlanco + separadorArgumentos);
                    commandArgument.Append(keyTipoEtiqueta + "1" + separadorArgumentos);
                    commandArgument.Append(keyCodCliente + _codCliente + separadorArgumentos);
                    commandArgument.Append(keyNomCliente + _nomCliente.Replace(" ",EspacioBlanco) + separadorArgumentos);
                    commandArgument.Append(keyNumPedido + _numPedido + separadorArgumentos);
                    commandArgument.Append(keyNumCarro + _nCarro + separadorArgumentos);                    
                    break;
                case eTipoDocumento.carroLavado:
                    //ex = oProduccion.RegistrarEtiquetaCarroLavado(_idEquipo, _codCliente, _nomCliente, _numPedido, nCopias, _tipoDocumento.ToString());
                    commandArgument.Append(keyEspacioBlanco + EspacioBlanco + separadorArgumentos);
                    commandArgument.Append(keyTipoEtiqueta + "2" + separadorArgumentos);
                    commandArgument.Append(keyCodCliente + _codCliente + separadorArgumentos);
                    commandArgument.Append(keyNomCliente + _nomCliente.Replace(" ", EspacioBlanco) + separadorArgumentos);
                    commandArgument.Append(keyNumPedido + _numPedido + separadorArgumentos);                                        
                    break;
                case eTipoDocumento.carroOxido:
                    //ex = oProduccion.RegistrarEtiquetaCarroOxido(_idEquipo, _codCliente, nCopias, _tipoDocumento.ToString());
                    commandArgument.Append(keyEspacioBlanco + EspacioBlanco + separadorArgumentos);
                    commandArgument.Append(keyTipoEtiqueta + "3" + separadorArgumentos);
                    commandArgument.Append(keyCodCliente + _codCliente + separadorArgumentos);
                    commandArgument.Append(keyNomCliente + _nomCliente.Replace(" ", EspacioBlanco) + separadorArgumentos);                    
                    break;
                case eTipoDocumento.carroTransporte:
                    //ex = oProduccion.RegistrarEtiquetaCarroTransportista(_idEquipo, _codCliente, _nomCliente, nCopias, _tipoDocumento.ToString());
                    commandArgument.Append(keyEspacioBlanco + EspacioBlanco + separadorArgumentos);
                    commandArgument.Append(keyTipoEtiqueta + "4" + separadorArgumentos);
                    commandArgument.Append(keyCodCliente + _codCliente + separadorArgumentos);
                    commandArgument.Append(keyNomCliente + _nomCliente.Replace(" ", EspacioBlanco) + separadorArgumentos);
                    break;
                case eTipoDocumento.paqueteProducto:
                    //ex = oProduccion.RegistrarEtiquetaPaqueteProducto(_idEquipo, _codCliente, _nomCliente, _codProducto, _nomProducto, _numPedido,
                     //           nCopias, _tipoDocumento.ToString());
                    commandArgument.Append(keyEspacioBlanco + EspacioBlanco + separadorArgumentos);
                    commandArgument.Append(keyTipoEtiqueta + "5" + separadorArgumentos);
                    commandArgument.Append(keyCodCliente + _codCliente + separadorArgumentos);
                    commandArgument.Append(keyNomCliente + _nomCliente.Replace(" ", EspacioBlanco) + separadorArgumentos);
                    commandArgument.Append(keyNumPedido + _numPedido + separadorArgumentos);
                    commandArgument.Append(keyCodProducto + _codProducto + separadorArgumentos);
                    commandArgument.Append(keyNomProducto + _nomProducto.Replace(" ", EspacioBlanco) + separadorArgumentos);
                    break;
            }

            commandArgument.Append(keyCopias + nCopias + separadorArgumentos);
            commandArgument.Append(keyDimensiones + HttpContext.Current.Session["dimensionesEtiqueta"].ToString());
            ArgumentString = commandArgument.ToString();

            return (ex);
        }

        


    }
}
