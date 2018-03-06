using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using INIKER.CrossReferences;

namespace IntranetINDUSAL.Negocio
{
    public class cPlegadoNuevo
    {
        #region Variables privadas

            private Tipo_Planchado _tipoPlanchado;              // identifica la zona de planchado   
            private string _empresaLogin;

            private string _datoTeclado;

            private string _codCliente;            
            private string _nomCliente;
            private string _numPedido;

            private string _codMaquina;
            private string _descMaquina;

            private string _codOperario;
            private string _nomOperario;
            private string _codTurno;
            private string _desTurno;

            private string _familiaSel;
            private string _subfamiliaSel;
            private string _codProducto;
            private string _descProducto;
            private int _udsPorPaquete;
            private string _nSerie;

            private int _nPaquetes;
            private int _nUnidades;
            private int _unidadesTotal;

            private int _nEtiquetas;
                    
            private DataTable _calandras;
            private DataTable _turnos;
            private DataTable _empleados;
            private DataTable _pedidos;          

        #endregion

        #region Propiedades

            public Tipo_Planchado TipoPlanchado
            {
                get { return _tipoPlanchado; }
                set { _tipoPlanchado = value; }
            }

            public string Empresa
            {
                get { return _empresaLogin; }
                set { _empresaLogin = value; }
            }

            public string DatoTeclado
            {
                get { return _datoTeclado; }
                set { _datoTeclado = value; }
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

            public string DescProducto
            {
                get { return _descProducto; }
                set { _descProducto = value; }
            }

            public int UdsPorPaquete
            {
                get { return _udsPorPaquete; }
                set { _udsPorPaquete = value; }
            }

            public string CodMaquina
            {
                get { return _codMaquina; }
                set { _codMaquina = value; }
            }

            public string DescMaquina
            {
                get { return _descMaquina; }
                set { _descMaquina = value; }
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

            public string FamiliaSel
            {
                get { return _familiaSel; }
                set { _familiaSel = value; }
            }

            public string SubfamiliaSel
            {
                get { return _subfamiliaSel; }
                set { _subfamiliaSel = value; }
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

            public string NumPedido
            {
                get { return _numPedido; }
                set { _numPedido = value; }
            }



            public int Paquetes
            {
                get { return _nPaquetes; }
                set 
                {
                    _nEtiquetas = (value - _nPaquetes > 0) ? (value - _nPaquetes) : 0;
                    _nPaquetes = value;                 
                    CalcularUnidadesTotales();
                }
            }

        
            public int Unidades
            {
                get { return _nUnidades; }
                set 
                {
                    int paqAntes = _nPaquetes;
                    SumarPaquetesCompletos(value);
                    _nEtiquetas = (_nPaquetes - paqAntes);
                    _nEtiquetas += (_nUnidades > 0) ? 1 : 0;
                    CalcularUnidadesTotales();
                }
            }          

            public int UnidadesTotal
            {
                // propiedad de sólo lectura
                get { return _unidadesTotal; }             
            }

            public int NEtiquetas
            {
                get { return _nEtiquetas; }
                set { _nEtiquetas = value; }
            }

            public string NSerie
            {
                get { return _nSerie; }
                set { _nSerie = value; }
            }

            public DataTable Calandras
            {
                get { return _calandras; }
                set { _calandras = value; }
            }

            public DataTable Turnos
            {
                get { return _turnos; }
                set { _turnos = value; }
            }
        
            public DataTable Empleados
            {
                get { return _empleados; }
                set { _empleados = value; }
            }

            public DataTable Pedidos
            {
                get { return _pedidos; }
                set { _pedidos = value; }
            }

        #endregion

        #region Constructores

            public cPlegadoNuevo()
            {
                // Constructor por defecto
            }

            public cPlegadoNuevo(string empresaLogin, int tipo)
            {
                this._empresaLogin = empresaLogin;
                GetTurnos();
                GetEmpleados();

                switch (tipo)
                {
                    case 4:
                        this._tipoPlanchado = Tipo_Planchado.Calandra;
                        GetCalandras();
                        break;
                    case 5:
                        this._tipoPlanchado = Tipo_Planchado.Felpa;
                        break;
                    case 6:
                        this._tipoPlanchado = Tipo_Planchado.Forma;
                        break;
                }

                InitAllProperties();
            }

        #endregion

        #region Metodos
            
            public int TipoPlanchadoToInt(Tipo_Planchado tipo)
            {
                int valor = 0;
                switch (tipo)
                {
                    case Tipo_Planchado.Calandra:
                        valor = 4;
                        break;
                    case Tipo_Planchado.Felpa:
                        valor = 5;
                        break;
                    case Tipo_Planchado.Forma:
                        valor = 6;
                        break;
                }
                return (valor);

            }

            public void Clear()
            {
                InitProdProperties();
                InitQtyProperties();
            }

            public void ClearAll()
            {
                InitAllProperties();
            }

            public void Register()
            {
                RegisterLines();
                InitQtyProperties();
            }

        #endregion

        #region Metodos Privados

            private void CalcularUnidadesTotales()
            {
                // calcula las unidades de la línea en función de
                // los valores de las propiedades
                _unidadesTotal = (_nPaquetes * _udsPorPaquete) + _nUnidades;
            }

            private void SumarPaquetesCompletos(int value)
            {
                // controla que en el número de unidades sueltas indicadas
                // en la línea, no hay las suficientes como para completar+
                // nuevos paquetes
                if (_udsPorPaquete > 0)
                {
                    this.Paquetes += value / _udsPorPaquete;
                    _nUnidades = value % _udsPorPaquete;
                }
                else
                    _nUnidades = value;
            }

            private void InitAllProperties()
            {
                _codCliente = "";
                _nomCliente = "";
                _numPedido = "";
                InitProperties();
            }

            private void InitProperties()
            {                
                _codMaquina = "";
                _descMaquina = "";
                _codOperario = "";
                _nomOperario = "";
                _codTurno = "";
                _desTurno = "";
                InitProdProperties();                
                InitQtyProperties();
            }

            private void InitProdProperties()
            {
                _codProducto = "";
                _descProducto = "";
                _udsPorPaquete = 0;
            }

            private void InitQtyProperties()
            {
                _nSerie = "";
                _unidadesTotal = 0;                                
                _nPaquetes = 0;
                _nUnidades = 0;
            }

            private void Init()
            {
                InitProperties();
            }

            private void RegisterLines()
            {
                cProduccion oProduccion = new cProduccion(_empresaLogin);                
                oProduccion.RegistrarPlanchado(
                            _codCliente,_codProducto,_unidadesTotal,"",
                            TipoPlanchadoToInt(_tipoPlanchado),_codMaquina,
                            _codTurno,_numPedido,_codOperario);
  
            }

        #endregion

        #region DATOS AUXILIARES

            private void GetCalandras()
            {
                cProduccion oProduccion = new cProduccion(this._empresaLogin);
                _calandras = oProduccion.GetWorkCenters("CALAN");
            }

            private void GetTurnos()
            {
                cProduccion oProduccion = new cProduccion(this._empresaLogin);
                _turnos = oProduccion.GetTurnos();
            }

            private void GetEmpleados()
            {
                cProduccion oProduccion = new cProduccion(this._empresaLogin);
                _empleados = oProduccion.GetEmployees();
            }
        
        #endregion
    }
}
