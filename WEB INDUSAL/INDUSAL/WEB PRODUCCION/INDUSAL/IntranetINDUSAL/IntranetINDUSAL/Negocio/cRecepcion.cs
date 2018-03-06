using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IntranetINDUSAL.Negocio
{
    public class cRecepcion
    {

        #region variables privadas
            // datos de login
            private string _empresa;

            // dato que se está introduciendo con el teclado numérico
            private string _tipoDatoTeclado;

        // errores durante el proceso de registro
            private string _errorMessage;

            private DateTime _fecha;
            private string _codTransportista;
            private string _transportista;
            private string _codRuta;
            private string _ruta;

            private string _codCliente;
            private string _nomCliente;
            private int _recibido;
            private int _entregado;
            private decimal _peso;
            private string _codIncidencia;
            private string _desIncidencia;
            private int _carrosVacios;
            private int _nEtiquetas;

  
            // datos maestros
            DataTable _transportistas;
            DataTable _rutas;
            DataTable _clientes;
            DataTable _incidencias;
              
        #endregion

        #region propiedades
        
            public string Empresa
            {
                get { return _empresa; }
                set { _empresa = value; }
            }

            public string TipoDatoTeclado
            {
                get { return _tipoDatoTeclado; }
                set { _tipoDatoTeclado = value; }
            }

            public string ErrorMessage
            {
                get { return _errorMessage; }
                set { _errorMessage = value; }
            }

            public DateTime Fecha
            {
                get { return _fecha; }
                set { _fecha = value; }
            }

            public string CodTransportista
            {
                get { return _codTransportista; }
                set { _codTransportista = value; }
            }

            public string Transportista
            {
                get { return _transportista; }
                set { _transportista = value; }
            }

            public string CodRuta
            {
                get { return _codRuta; }
                set { _codRuta = value; }
            }

            public string Ruta
            {
                get { return _ruta; }
                set { _ruta = value; }
            }

            public string CodCliente
            {
                get { return _codCliente; }
                set 
                {
                    _codCliente = value;
                    _recibido = 0;
                    _entregado = 0;
                    _carrosVacios = 0;
                    _peso = 0;
                }
            }

            public string NomCliente
            {
                get { return _nomCliente; }
                set { _nomCliente = value; }
            }

            public int Recibido
            {
                get { return _recibido; }
                set { _recibido = value; }
            }

            public int Entregado
            {
                get { return _entregado; }
                set { _entregado = value; }
            }

            public decimal Peso
            {
                get { return _peso; }
                set { _peso = value; }
            }

            public string CodIncidencia
            {
                get { return _codIncidencia; }
                set { _codIncidencia = value; }
            }

            public string DesIncidencia
            {
                get { return _desIncidencia; }
                set { _desIncidencia = value; }
            }

            public int CarrosVacios
            {
                get { return _carrosVacios; }
                set { _carrosVacios = value; }
            }

            public int NEtiquetas
            {
                get { return _nEtiquetas; }
                set { _nEtiquetas = value; }
            }


            public DataTable Transportistas
            {
                get { return _transportistas; }                
            }

            public DataTable Rutas
            {
                get { return _rutas; }                
            }

            public DataTable Clientes
            {
                get { return _clientes; }
            }

            public DataTable Incidencias
            {
                get { return _incidencias; }
                set { _incidencias = value; }
            }



        #endregion

        #region metodos publicos

            public cRecepcion(string empresa)
            {
                // Constructor por defecto de la clase
                this._empresa = empresa;
                // Inicialización de los valores de las propiedades
                InitProperties();
                // carga de datos maestros de transportistas e incidencias
                GetTransportistas();
                GetIncidencias();
            }
           
            
            public void GetRutasTransportista()
            {
                // se inicializa el datatable
                if (_rutas != null)
                    _rutas.Clear();

                if (_codTransportista!="")
                {
                    GetRutas(_codTransportista);
                }
            }

            public void GetClientesRuta()
            {
                // se inicializa el datatable
                if (_clientes != null)
                    _clientes.Clear();

                if (_codRuta != "")
                {
                    GetClientes(_codRuta);
                }
            }

            public void Register(bool sinConteo,bool tercerCircuito)
            {                
                RegisterRecepcion(sinConteo, tercerCircuito);
                InitProperties();
            }

            public void Init()
            {
                InitProperties();
            }

        #endregion

        #region métodos privados

            private void RegisterRecepcion(bool sinConteo, bool tercerCircuito)
            {                
                cProduccion oProduccion = new cProduccion(_empresa);
                oProduccion.RegistrarRecepcion(_codTransportista,_codCliente,_recibido,_entregado,_peso,
                                                        _codRuta,_codIncidencia,sinConteo,_carrosVacios,tercerCircuito);
                                
            }

            private void InitProperties()
            {
                _fecha = System.DateTime.Today;
                //_codTransportista="";
                //_transportista="";
                //_codRuta = "";
                //_ruta = "";

                _codCliente = "";
                _nomCliente = "";
                _recibido = 0;
                _entregado = 0;
                _carrosVacios = 0;
                _nEtiquetas = 0;
                _codIncidencia = "";
                _desIncidencia = "";
                _peso = 0;

                _errorMessage = "";                
            }
        
        #endregion

        #region DATOS AUXILIARES

            private void GetTransportistas()
            {
                cProduccion oProduccion = new cProduccion(this._empresa);
                _transportistas = oProduccion.GetTransportistas();
            }

            private void GetRutas(string shppAgent)
            {
                cProduccion oProduccion = new cProduccion(this._empresa);
                _rutas= oProduccion.GetRutasTta(shppAgent);
            }

            private void GetClientes(string routCode)
            {
                cProduccion oProduccion = new cProduccion(this._empresa);
                _clientes = oProduccion.GetClientes(routCode);
            }

            private void GetIncidencias()
            {
                cProduccion oProduccion = new cProduccion(this._empresa);
                _incidencias = oProduccion.GetListaIncidencias();
            }
        
        #endregion
    }
}
