using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IntranetINDUSAL.Negocio
{
    public class cCostura
    {

        #region CONSTRUCTORES

        public cCostura(string _empresaLogin)
        {
            this._empresa = _empresaLogin;
            InitAll();
        }

        #endregion

        #region VARIABLES PRIVADAS Y PROPIEDADES

        private string _datoTeclado;        
        private string _empresa;

        private string _codTurno;
        private string _nomTurno;
        private string _codOperario;
        private string _nomOperario;
        private string _codOperacion;
        private string _nomOperacion;
        private bool _ajusteInventario;
        private bool _traspasoASucio;        
        private string _codCliente;
        private string _nomCliente;        
        private string _codProducto;
        private string _nomProducto;
        private string _nSerie;
        private decimal _cantidad;

        private DataTable _dtTurnos;
        private DataTable _dtTrapos;
        
        public string DatoTeclado
        {
            get { return _datoTeclado; }
            set { _datoTeclado = value; }
        }

        public string Empresa
        {
            get { return _empresa; }
            set { _empresa = value; }
        }

        public string CodTurno
        {
            get { return _codTurno; }
            set { _codTurno = value; }
        }
        
        public string NomTurno
        {
            get { return _nomTurno; }
            set { _nomTurno = value; }
        }
        
        public string CodOperario
        {
            get { return _codOperario; }
            set { _codOperario = value; }
        }
        
        public string NomOperario
        {
            get { return _nomOperario; }
            set { _nomOperario = value; }
        }
        
        public string CodOperacion
        {
            get { return _codOperacion; }
            set { _codOperacion = value; }
        }

        public string NomOperacion
        {
            get { return _nomOperacion; }
            set { _nomOperacion = value; }
        }
        
        public bool AjusteInventario
        {
            get { return _ajusteInventario; }
            set { _ajusteInventario = value; }
        }

        public bool TraspasoASucio
        {
            get { return _traspasoASucio; }
            set { _traspasoASucio = value; }
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

        public string NSerie
        {
            get { return _nSerie; }
            set { _nSerie = value; }
        }

        public decimal Cantidad
        {
            get { return _cantidad; }
            set { _cantidad = value; }
        }
        
        public DataTable dtTurnos
        {
            get { return _dtTurnos; }
            set { _dtTurnos = value; }
        }

        public DataTable dtTrapos
        {
            get { return _dtTrapos; }
            set { _dtTrapos = value; }
        }

        #endregion

        #region METODOS PUBLICOS

        public void Registrar()
        {
            //if (_codOperacion == "baja")
            //    _cantidad = _cantidad * (-1);

            cProduccion oProduccion = new cProduccion(_empresa);
            oProduccion.RegistrarSalidaCostura(_codOperario, _codTurno, _codCliente, CodProducto, 
                _nSerie, _cantidad, _codOperacion);
            //Init();
            _cantidad = 0;
        }

        public void Inicializar()
        {
            Init();
        }

        #endregion

        #region METODOS PRIVADOS

        private void Init()
        {            
            _codOperacion = "";
            _nomOperacion = "";
            _ajusteInventario = false;
            _traspasoASucio = true;
            _codCliente = "";
            _nomCliente = "";
            _codProducto = "";
            _nomProducto = "";
            _nSerie = "";
            _cantidad = 0;
        }

        private void InitAll()
        {
            _codTurno = "";
            _nomTurno = "";
            _codOperario = "";
            _nomOperario = "";
            Init();
        }

        #endregion



    }
}
