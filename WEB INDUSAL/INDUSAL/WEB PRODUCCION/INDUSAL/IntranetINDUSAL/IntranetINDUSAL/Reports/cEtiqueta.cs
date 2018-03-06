using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IntranetINDUSAL.Reports
{
    public class cEtiqueta
    {
        private string _codCliente;
        private string _nomCliente;
        private string _numPedido;
        private string _codProducto;
        private string _nomProducto;
        private string _nCarro;
        private int _copias;        


        public string CodCliente
        {
            get { return _codCliente; }
            set { _codCliente = "*" + value + "*"; }
        }
        
        public string NomCliente
        {
            get { return _nomCliente; }
            set { _nomCliente = value; }
        }
        
        public string NumPedido
        {
            get { return _numPedido; }
            set { _numPedido = "*" + value + "*"; }
        }
        
        public string CodProducto
        {
            get { return _codProducto; }
            set { _codProducto = "*" + value + "*"; }
        }
        
        public string NomProducto
        {
            get { return _nomProducto; }
            set { _nomProducto = value; }
        }
        
        public string NCarro
        {
            get { return _nCarro; }
            set { _nCarro = value; }
        }

        public int Copias
        {
            get { return _copias; }
            set { _copias = value; }
        }        

        public cEtiqueta()
        {
            Init();
        }

        private void Init()
        {
            _codCliente = "";
            _nomCliente = "";
            _numPedido = "";
            _codProducto = "";
            _nomProducto = "";            
            _copias = 0;
        }

    }
}
