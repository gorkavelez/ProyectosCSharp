using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IntranetINDUSAL.Negocio
{
    public class cRechazo
    {
        #region Estructuras Auxiliares

        public enum eTipoRechazo
        {
            SinSeleccion,
            Normal,
            Costura,
            Oxido,
            CosturaAProduccion,
            OxidoAProduccion,
            OxidoACostura
        }


        #endregion

        #region Variables Privadas

        // datos para el login
        private string _empresaLogin;
        // datos de la clase
        private eTipoRechazo _tipoRechazo;
        private string _codCliente;
        private string _cliente;
        private string _codFamilia;
        private string _familia;
        private string _codSubfamilia;
        private string _subfamilia;
        private string _codProducto;
        private string _producto;
        private decimal _cantidad;
        private string _codOperario;
        private string _operario;
        private string _codTurno;
        private string _desTurno;

        private string _nSerie;

        private DataTable _turnos;

        #endregion

        #region Propiedades

        public eTipoRechazo TipoRechazo
        {
            get { return _tipoRechazo; }
            set
            {
                _tipoRechazo = value;
                InitProperties(false);
            }
        }

        public string CodCliente
        {
            get { return _codCliente; }
            set
            {
                _codCliente = value;
                CodFamilia = "";
            }
        }

        public string Cliente
        {
            get { return _cliente; }
            set { _cliente = value; }
        }

        public string CodFamilia
        {
            get { return _codFamilia; }
            set
            {
                _codFamilia = value;
                CodSubfamilia = "";
                _subfamilia = "";
            }
        }

        public string Familia
        {
            get { return _familia; }
            set { _familia = value; }
        }

        public string CodSubfamilia
        {
            get { return _codSubfamilia; }
            set
            {
                _codSubfamilia = value;
                _codProducto = "";
                _producto = "";
            }
        }

        public string Subfamilia
        {
            get { return _subfamilia; }
            set { _subfamilia = value; }
        }

        public string CodProducto
        {
            get { return _codProducto; }
            set { _codProducto = value; }
        }

        public string Producto
        {
            get { return _producto; }
            set { _producto = value; }
        }

        public decimal Cantidad
        {
            get { return _cantidad; }
            set { _cantidad = value; }
        }

        public string CodOperario
        {
            get { return _codOperario; }
            set { _codOperario = value; }
        }

        public string Operario
        {
            get { return _operario; }
            set { _operario = value; }
        }

        public string NSerie
        {
            get { return _nSerie; }
            set { _nSerie = value; }
        }

        public string CodTurno
        {
            get { return _codTurno; }
            set { _codTurno = value; }
        }

        public string DesTurno
        {
            get { return _desTurno; }
            set { _desTurno = value; }
        }

        public DataTable Turnos
        {
            get { return _turnos; }
            set { _turnos = value; }
        }

        #endregion

        #region Métodos Públicos

        #region Constructores

        public cRechazo(string empresaLogin)
        {
            // Constructor por defecto
            this._empresaLogin = empresaLogin;
            GetTurnos();
            InitProperties(true);
        }

        public cRechazo(string empresaLogin, int origen)
        {
            // Constructor
            this._empresaLogin = empresaLogin;
            InitProperties(true);
            GetTurnos();
        }

        //public cRechazo(string empresaLogin, TipoRechazo tipo, TipoMovRechazo tipoMov)
        //{
        //    // Constructor
        //    this._empresaLogin = empresaLogin;
        //    this.Tipo = tipo;
        //    this.TipoMov = TipoMov;
        //}                

        #endregion

        public string TipoRechazoToString()
        {
            string literal = "";
            switch (_tipoRechazo)
            {
                case eTipoRechazo.Normal:
                    literal = "RECHAZAR";
                    break;
                case eTipoRechazo.Costura:
                    literal = "A COSTURA";
                    break;
                case eTipoRechazo.Oxido:
                    literal = "A OXIDO";
                    break;
                case eTipoRechazo.CosturaAProduccion:
                    literal = "COSTURA A PRODUCCION";
                    break;
                case eTipoRechazo.OxidoAProduccion:
                    literal = "OXIDO A PRODUCCION";
                    break;
                case eTipoRechazo.OxidoACostura:
                    literal = "OXIDO A COSTURA";
                    break;
            }

            return (literal);
        }

        public void Registrar()
        {
            switch (_tipoRechazo)
            {
                case eTipoRechazo.Normal:
                    RechazoNormal();
                    break;
                case eTipoRechazo.Costura:
                    //OxidoCostura(true, false);
                    break;
                case eTipoRechazo.Oxido:
                    //OxidoCostura(false, false);
                    break;
                case eTipoRechazo.CosturaAProduccion:
                    //CosturaAProduccion();
                    break;
                case eTipoRechazo.OxidoAProduccion:
                    //SalidaOxido(false);
                    break;
                case eTipoRechazo.OxidoACostura:
                    //SalidaOxido(true);
                    break;
            }

            InitProperties(false);

        }


        #endregion

        #region Métodos Privados

        private void InitProperties(bool clienteTambien)
        {
            if (clienteTambien)
            {
                _codCliente = "";
                _cliente = "";
            }
            _codFamilia = "";
            _familia = "";
            _codSubfamilia = "";
            _subfamilia = "";
            _codProducto = "";
            _producto = "";
            _cantidad = 0;
            _codOperario = "";
            _operario = "";
            _codTurno = "";
            _desTurno = "";
            _nSerie = "";
        }

        private void RechazoNormal()
        {
            // Cuando se produce rechazo, la ropa se vuelve a lavar, pero no se produce
            // un traspaso entre almacenes.
            // Unicamente es necesario guardar registro de movimiento de producción para
            // las estadísticas e informes de producción
            cProduccion oProduccion = new cProduccion(_empresaLogin);
            oProduccion.Rechazar(_codCliente,
                                    _cantidad,
                                    System.DateTime.Today,
                                    System.DateTime.Now,
                                    _codOperario,
                                    _codFamilia + _codSubfamilia,
                                    _codTurno);
        }

        //private void OxidoCostura(bool aCostura, bool desdeOxido)
        //{
        //    // Cuando se produce rechazo, la ropa se vuelve a lavar, pero no se produce
        //    // un traspaso entre almacenes.
        //    // Unicamente es necesario guardar registro de movimiento de producción para
        //    // las estadísticas e informes de producción
        //    cProduccion oProduccion = new cProduccion(_empresaLogin);
        //    oProduccion.RechazarAOxidoCostura(_codCliente,
        //                                        _codProducto,
        //                                        _nSerie,
        //                                        _cantidad,
        //                                        System.DateTime.Today,
        //                                        System.DateTime.Now,
        //                                        _codOperario,
        //                                        aCostura,
        //                                        desdeOxido,
        //                                        _codTurno);
        //}

        //private void CosturaAProduccion()
        //{
        //    cProduccion oProduccion = new cProduccion(_empresaLogin);
        //    oProduccion.SalidaRechazoCostura(_codProducto,
        //                                        _nSerie,
        //                                        _cantidad,
        //                                        _codOperario);
        //}

        //private void SalidaOxido(bool aCostura)
        //{
        //    cProduccion oProduccion = new cProduccion(_empresaLogin);
        //    oProduccion.SalidaRechazoOxido(_codProducto,
        //                                        _nSerie,
        //                                        _cantidad,
        //                                        _codOperario,
        //                                        aCostura);
        //}

        #endregion

        #region Datos Auxiliares

        private void GetTurnos()
        {
            cProduccion oProduccion = new cProduccion(this._empresaLogin);
            _turnos = oProduccion.GetTurnos();
        }

        #endregion
    }
}
