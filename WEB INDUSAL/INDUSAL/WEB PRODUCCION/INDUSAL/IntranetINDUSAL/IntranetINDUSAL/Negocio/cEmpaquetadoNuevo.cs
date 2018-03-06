using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using INIKER.MovsProduccion;

namespace IntranetINDUSAL.Negocio
{
    public class cEmpaquetadoNuevo
    {
        #region TIPOS DE DATOS

            public enum eTipoEmpaquetado
        {
            empaquetado,
            expedicion
        }
        
            public enum eDatoTeclado
            {
                peso,
                cliente,
                operario
            }

        #endregion

        #region Variables PRIVADAS

            private string _empresaLogin;
            private eDatoTeclado _datoTeclado;
            private eTipoEmpaquetado _tipo;

        // datos de cabecera
            private string _codTurno;
            private string _desTurno;
            private string _codEmpleado;
            private string _nomEmpleado;
            private string _codCliente;
            private string _nomCliente;
            private string _numPedido;

        // pedidos abiertos del cliente
            private DataTable _pedidos;
            private DataTable _turnos;
            private DataTable _carros;            
            private DataTable _empleados;
            //private DataTable _movsRegistrados;

        // líneas del pedido de venta
            private cLineasPedidoVenta _pedido;        

        // pesajes
            private cPesajes _pesajeEnCurso;

        // carros pesajes
            private cCarrosEmpaquetado _carrosPesaje;
            private bool _bloquearCarroCompleto;

            private bool _imprimirEtiqueta;
            private int _nCarroEtiqueta;
            
 
        #endregion

        #region Metodos PRIVADOS

            protected void PesajeEnCurso_DataUpdate(object sender, cPesajes.DataUpdateEventArgs e)
            {
                cPesajes oSender = (cPesajes)sender;                
                ActualizarCarro(e.Carro);               

                switch (e.Accion)
                {
                    case cPesajes.DataUpdateEventArgs.eAction.insert:
                        _imprimirEtiqueta = oSender.ImprimirEtiqueta;
                        _nCarroEtiqueta = oSender.NCarroEtiqueta;
                        _pesajeEnCurso.SelectByLine(e.Linea);
                        RegistrarLineaPesaje();                        
                        break;
                    case cPesajes.DataUpdateEventArgs.eAction.update:
                        _pesajeEnCurso.SelectByLine(e.Linea);
                        ActualizarLineaPesaje();
                        break;
                    case cPesajes.DataUpdateEventArgs.eAction.delete:
                        EliminarLineaPesaje(e.MovProduccion);
                        break;
                }

            }

            private void InitProperties()
            {
                _codTurno = "";
                _desTurno = "";
                _codEmpleado = "";
                _nomEmpleado = "";
                _codCliente= "";
                _nomCliente= "";
                _numPedido = "";
            }

            private void RegistrarLineaPesaje()
            {
                string nuevoMov="";
                cProduccion oProduccion = new cProduccion(_empresaLogin);
                nuevoMov=oProduccion.RegistrarPesaje(_codEmpleado, _codTurno, _codCliente, _numPedido, _pesajeEnCurso.CodProducto,
                    "", decimal.Parse(_pesajeEnCurso.Peso.ToString()), _pesajeEnCurso.NCarro, _pesajeEnCurso.CodTipoCarro, _pesajeEnCurso.Completo, 
                    _pesajeEnCurso.Consolidado, (_tipo== eTipoEmpaquetado.expedicion),_pesajeEnCurso.UdsConsolidacion);
                if (nuevoMov != "")
                {
                    _pesajeEnCurso.NMov = nuevoMov;
                    _pesajeEnCurso.Add();
                }
            }

            private void ActualizarLineaPesaje()
            {                
                cProduccion oProduccion = new cProduccion(_empresaLogin);
                oProduccion.ActualizarPesaje(_pesajeEnCurso.NMov, _pesajeEnCurso.Completo);
            }

            private void EliminarLineaPesaje(string numMov)
            {
                cProduccion oProduccion = new cProduccion(_empresaLogin);
                oProduccion.EliminarPesaje(numMov);
            }

            private void LoadMovsEmpaquetado()
            {
                _bloquearCarroCompleto = true;

                DataTable _movsEmpaquetado = GetMovsEmpaquetado();
                foreach (DataRow oRow in _movsEmpaquetado.Rows)
                {
                    _pesajeEnCurso.NMov = oRow["movimiento"].ToString();
                    _pesajeEnCurso.CodProducto = oRow["producto"].ToString();
                    _pesajeEnCurso.DesProducto = DescripcionProducto(_pesajeEnCurso.CodProducto);
                    _pesajeEnCurso.NCarro = int.Parse(oRow["nCarro"].ToString());
                    _pesajeEnCurso.CodTipoCarro = oRow["tipoCarro"].ToString();
                    _pesajeEnCurso.DesTipoCarro = DescripcionCarro(_pesajeEnCurso.CodTipoCarro);
                    _pesajeEnCurso.Peso=Single.Parse(oRow["peso"].ToString());
                    _pesajeEnCurso.Completo=bool.Parse(oRow["completo"].ToString());
                    _pesajeEnCurso.PesoTipoCarro = PesoCarro(_pesajeEnCurso.CodTipoCarro);
                    _pesajeEnCurso.MovExpedicion = bool.Parse(oRow["movExpedicion"].ToString());
                    _pesajeEnCurso.AddNew();
                }

                _bloquearCarroCompleto = false;
                // se genera la información a nivel de carro, bloqueando los carros completos,
                // para controlar su modificación.
                //GenerarCarros(true);
            }

            private void ActualizarCarro(int nCarro)
            {
                // se captura la línea que se está procesando para tener acceso 
                // a los datos calculados de las propiedades
                if (_pesajeEnCurso.SelectByCarro(nCarro))
                {
                    _carrosPesaje.Numero = _pesajeEnCurso.NCarro;
                    _carrosPesaje.CodTipo = _pesajeEnCurso.CodTipoCarro;
                    _carrosPesaje.Tipo = _pesajeEnCurso.DesTipoCarro;
                    _carrosPesaje.Peso = _pesajeEnCurso.PesoBrutoCarro;
                    _carrosPesaje.Completo = _pesajeEnCurso.Completo;
                    _carrosPesaje.CarroExpedicion = _pesajeEnCurso.MovExpedicion; // 13/12/12

                    //_carrosPesaje.Bloqueado = (_pesajeEnCurso.Completo && _bloquearCarroCompleto);

                    _carrosPesaje.Update();
                }
                else
                {
                    _carrosPesaje.Numero = nCarro;
                    _carrosPesaje.Delete();
                }
            }

            private void CompletarCarros()
            {
                DataRow[] carrosIncompletos = _carrosPesaje.DTCarros.Select("Completo=false");
                foreach (DataRow carroIncompleto in carrosIncompletos)
                {
                    _pesajeEnCurso.SelectByCarro(int.Parse(carroIncompleto["Numero"].ToString()));
                    _pesajeEnCurso.Completo = true;
                    _pesajeEnCurso.Add();
                }
            }

            private bool ExisteCarroIncompleto()
            {
                DataRow[] carrosIncompletos = _carrosPesaje.DTCarros.Select("Completo=false");
                try
                {
                    return (carrosIncompletos.Count() > 0);
                }
                catch
                {
                    return (false);
                }                
            }

            private void RegistrarLineasConsolidacion()
            {
                DataRow[] linConsolidacion = _pesajeEnCurso.Lineas.Select("LineaConsolidacion=true");
                foreach (DataRow lineaPesaje in linConsolidacion)
                {
                    _pesajeEnCurso.SelectByLine(int.Parse(lineaPesaje["Linea"].ToString()));
                    RegistrarLineaPesaje();
                }
            }

            private void EliminarLineasPesoActuales()
            {
                cProduccion oProduccion = new cProduccion(_empresaLogin);
                oProduccion.EliminarLineasPesoPedVenta(_numPedido);
            }

            private void CrearLineasPeso()
            {
                cProduccion oProduccion = new cProduccion(_empresaLogin);
                oProduccion.GenerarLineasPesoPedVenta(_numPedido);
            }

            private void RegistrarPedidoVenta()
            {
                cProduccion oProduccion = new cProduccion(_empresaLogin);
                oProduccion.RegistrarPedidoVenta(_numPedido);
            }

            private void ClearData()
            {
                _codCliente = "";
                _nomCliente = "";
                _numPedido = "";
                _pesajeEnCurso.Lineas.Clear();
                _carrosPesaje.DTCarros.Clear();
                _pesajeEnCurso.Clear();
            }

        #endregion

        #region PROPIEDADES

            public string EmpresaLogin
            {
                get { return _empresaLogin; }
                set { _empresaLogin = value; }
            }

            public eDatoTeclado DatoTeclado
            {
                get { return _datoTeclado; }
                set { _datoTeclado = value; }
            }

            public eTipoEmpaquetado Tipo
            {
                get { return _tipo; }
                set { _tipo = value; }
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

            public string CodEmpleado
            {
                get { return _codEmpleado; }
                set { _codEmpleado = value; }
            }

            public string NomEmpleado
            {
                get { return _nomEmpleado; }
                set { _nomEmpleado = value; }
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
                set 
                { 
                    _numPedido = value;
                    if (_numPedido != "")
                        LoadMovsEmpaquetado();
                }
            }

        
            public DataTable Pedidos
            {
                get { return _pedidos; }
                set { _pedidos = value; }
            }
        
            public DataTable Turnos
            {
                get { return _turnos; }
                set { _turnos = value; }
            }

            public DataTable Carros
            {
                get { return _carros; }
            } 

            public DataTable Empleados
            {
                get { return _empleados; }
                set { _empleados = value; }
            }
            
            public cPesajes PesajeEnCurso
            {
                get { return _pesajeEnCurso; }
                set { _pesajeEnCurso = value; }
            }

            public cCarrosEmpaquetado CarrosPesaje
            {
                get { return _carrosPesaje; }
                set { _carrosPesaje = value; }
            }

            public bool ImprimirEtiqueta
            {
                get { return _imprimirEtiqueta; }
                set { _imprimirEtiqueta = value; }
            }

            public int NCarroEtiqueta
            {
                get { return _nCarroEtiqueta; }
                set { _nCarroEtiqueta = value; }
            }

        #endregion

        public cEmpaquetadoNuevo(string empresa,eTipoEmpaquetado tipoEmpaq)
        {
            this._empresaLogin= empresa;
            this._tipo = tipoEmpaq;

            GetTurnos();
            GetCarrosSacas();
            GetEmpleados();

            _pesajeEnCurso = new cPesajes();
            this._pesajeEnCurso.DataUpdate += new cPesajes.DataUpdateHandler(PesajeEnCurso_DataUpdate);


            _carrosPesaje = new cCarrosEmpaquetado();

            InitProperties();
        }

        #region METODOS PUBLICOS
 
            public bool ItemInOrder(string itemCode)
            {
                try
                {
                    return (_pedido.ItemExists(itemCode));
                }
                catch
                {
                    return (false);
                }
            }

            public bool DatosSinRegistrar()
            {
                return (_pesajeEnCurso.CountLines() != 0);
            }

            public void CerrarPedido()
            {
                if (!ExisteCarroIncompleto())
                {
                    if (_codCliente != "000000")
                    {
                        //CompletarCarros();
                        EliminarLineasPesoActuales();
                        //_pesajeEnCurso.ConsolidarLineas(_carrosPesaje.DTCarros);
                        //RegistrarLineasConsolidacion();
                        CrearLineasPeso();
                        RegistrarPedidoVenta();
                    }
                }
                else
                    throw new Exception("Para poder cerrar el pedido todos los carros deben estar completos");

                ClearData();                
            }

            public void Back()
            {
                ClearData();
            }

        #endregion

        #region LINEAS DE PESAJE

            public class cPesajes
            {
                #region EVENTOS
                    // delegados
                    public delegate void DataUpdateHandler(object sender, DataUpdateEventArgs e);

                    //eventos
                    public event DataUpdateHandler DataUpdate;

                    // Clase que encapsula los datos que se pasan en el segundo parámetro del evento
                    public class DataUpdateEventArgs : EventArgs
                    {
                        public enum eAction
                        {
                            load,
                            insert,
                            update,
                            delete,
                            none
                        }

                        private int _nLinea;
                        private int _nCarro;
                        private string _nMovProduccion;
                        private eAction _accion;
                        
                        public int Linea
                        {
                            get { return _nLinea; }
                            set { _nLinea= value; }
                        }

                        public int Carro
                        {
                            get { return _nCarro; }
                            set { _nCarro = value; }
                        }

                        public string MovProduccion
                        {
                            get { return _nMovProduccion; }
                            set { _nMovProduccion = value; }
                        }

                        public eAction Accion
                        {
                            get { return _accion; }
                            set { _accion = value; }
                        }
                        
                        public DataUpdateEventArgs(int linea, int carro, string mov, eAction acc)
                        {
                            this._nLinea = linea;
                            this._nCarro = carro;                            
                            this._nMovProduccion = mov;
                            this._accion = acc;                            
                        }

                    }

                    protected virtual void OnDataUpdate(DataUpdateEventArgs e)
                    {
                        if (DataUpdate != null)
                        {
                            DataUpdate(this, e);
                        }
                    }

                #endregion

                #region VARIABLES PRIVADAS

                    private int _nLinea;
                    private string _nMov;
                    private bool _movExpedicion;    // 13/12/12
                    private DateTime _fecha;
                    private DateTime _hora;
                    private string _codProducto;
                    private string _desProducto;                               
                    private int _nCarro;
                    private string _codTipoCarro;
                    private string _desTipoCarro;                
                    private Single _peso;
                    private bool _completo;
                    private Single _pesoNetoCarro;
                    private Single _pesoBrutoCarro;                  
                    private bool _consolidado;
                    private Single _pesoTipoCarro;
                    private int _udsConsolidacion;
                                                                   
                    private DataTable _pesajes;

                    private bool _imprimirEtiqueta;
                    private int _nCarroEtiqueta;

                #endregion

                #region PROPIEDADES

                    public string NMov
                    {
                        get { return _nMov; }
                        set { _nMov = value; }
                    }

                    public bool MovExpedicion       // 13/12/12
                    {
                        get { return _movExpedicion; }
                        set { _movExpedicion = value; }
                    }

                    public DateTime Fecha
                    {
                        get { return _fecha; }
                        set { _fecha = value; }                    
                    }

                    public DateTime Hora
                    {
                        get { return _hora; }
                        set { _hora = value; }                    
                    }

                    public string CodProducto
                    {
                        get { return _codProducto; }
                        set { _codProducto = value; }
                    }

                    public string DesProducto
                    {
                        get { return _desProducto; }
                        set { _desProducto = value; }
                    }

                    public int NCarro
                    {
                        get { return _nCarro; }
                        set { _nCarro = value; }
                    }

                    public string CodTipoCarro
                    {
                        get { return _codTipoCarro; }
                        set { _codTipoCarro = value; }
                    }

                    public string DesTipoCarro
                    {
                        get { return _desTipoCarro; }
                        set { _desTipoCarro = value; }
                    }

                    public Single Peso
                    {
                        get { return _peso; }
                        set { _peso = value; }
                    }

                    public bool Completo
                    {
                        get { return _completo; }
                        set { _completo = value; }
                    }

                    public Single PesoNetoCarro
                    {
                        get { return _pesoNetoCarro; }
                        set { _pesoNetoCarro = value; }
                    }

                    public Single PesoBrutoCarro
                    {
                        get { return _pesoBrutoCarro; }
                        set { _pesoBrutoCarro = value; }
                    }  

                    public bool Consolidado
                    {
                        get { return _consolidado; }                    
                    }
                
                    public Single PesoTipoCarro
                    {
                        get { return _pesoTipoCarro; }
                        set { _pesoTipoCarro = value; }
                    }
                    
                    public int UdsConsolidacion
                    {
                        get { return _udsConsolidacion; }
                        set { _udsConsolidacion = value; }
                    }

                    public DataTable Lineas
                    {
                        get { return _pesajes; }
                        set { _pesajes = value; }
                    }

                    public bool ImprimirEtiqueta
                    {
                        get { return _imprimirEtiqueta; }
                        set { _imprimirEtiqueta = value; }
                    }

                    public int NCarroEtiqueta
                    {
                        get { return _nCarroEtiqueta; }
                        set { _nCarroEtiqueta = value; }
                    }

                #endregion
                
                public cPesajes()
                {
                    CreateDataTable(ref _pesajes);
                    InicializarVariables();                    
                }

                #region MIEMBROS PUBLICOS

                    public int CountLines()
                    {
                        return (_pesajes.Rows.Count);
                    }

                    public void Add()
                    {
                        DataUpdateEventArgs.eAction accion;
                        ImprimirEtiqueta = false;

                        if (_nCarro == 0)
                        {
                            // quitamos el peso del carro para almacenar el neto de ropa
                            _peso -= _pesoTipoCarro;
                            AñadirLinea();
                            accion = DataUpdateEventArgs.eAction.insert;
                        }
                        else
                        {
                            Single pesoTotalActual = CalcularPesoBrutoCarro(_nCarro);
                            //if (_peso < pesoTotalActual)
                            //{
                            //    ActualizarLinea();
                            //    loading = true;
                            //}
                            if (_peso > pesoTotalActual)
                            {
                                _peso -= pesoTotalActual;
                                AñadirLinea();
                                accion = DataUpdateEventArgs.eAction.insert;
                            }
                            else
                            {
                                ActualizarLinea();
                                accion = DataUpdateEventArgs.eAction.update;
                            }
                            // sólo modifica lo que se ve. No toca los datos registrados
                            CompletarCarro();                        

                        }
                        ThrowDataUpdateEvent(_nLinea, _nCarro, "", accion);
                        InicializarVariables();
                    }

                    public void AddNew()
                    {
                        AñadirLinea();
                        ThrowDataUpdateEvent(_nLinea, _nCarro, "", DataUpdateEventArgs.eAction.none);
                        InicializarVariables();
                    }

                    public void Delete(int linea)
                    {                        
                        EliminarLinea(linea);
                        InicializarVariables();
                    }

                    public bool SelectByLine(int nLinea)
                    {
                        string filtro = "Linea=" + nLinea;
                        return (Select(Seleccionar(filtro,"")));
                    }

                    public bool SelectByCarro(int nCarro)
                    {
                        string filtro = "Carro=" + nCarro;
                        return(Select(Seleccionar(filtro,"Linea DESC")));                        
                    }

                    public Single CalcPesoNetoProdCarro(string producto, int carro)
                    {
                        Single netWeight=0;
                        DataRow[] filtro = _pesajes.Select("Cod. Producto='" + producto + "',Carro=" + carro);
                        foreach (DataRow fila in filtro)
                        {
                            netWeight+= Single.Parse(fila["peso"].ToString());
                        }
                        return (netWeight);
                    }

                    public void Clear()
                    {
                        InicializarVariables();
                    }

                #endregion

                #region MIEMBROS PRIVADOS

                    private bool Select(DataRow[] lineasFiltro)
                    {
                        if (lineasFiltro.Count() != 0)
                        {
                            AsignarVariables(lineasFiltro[0]);
                            return (true);
                        }
                        else
                        {
                            InicializarVariables();
                            return (false);
                        }
                    }

                    private void CreateDataTable(ref DataTable _dt)
                    {
                        _dt = new DataTable("pesajes");
                        DataColumn newColumn;

                        newColumn = _dt.Columns.Add("Linea");
                        newColumn.DataType = System.Type.GetType("System.Int16");
                        newColumn.AutoIncrement = true;
                        newColumn.AutoIncrementSeed = 1;
                        newColumn.AutoIncrementStep = 1;

                        newColumn = _dt.Columns.Add("Mov. Produccion");
                        newColumn.DataType = System.Type.GetType("System.String");
                        newColumn.DefaultValue = "";
                        newColumn.AllowDBNull = true;

                        newColumn = _dt.Columns.Add("Cod. Producto");
                        newColumn.DataType = System.Type.GetType("System.String");
                        newColumn.DefaultValue = "";
                        newColumn.AllowDBNull = true;

                        newColumn = _dt.Columns.Add("Producto");
                        newColumn.DataType = System.Type.GetType("System.String");
                        newColumn.DefaultValue = "";
                        newColumn.AllowDBNull = true;

                        newColumn = _dt.Columns.Add("Carro");
                        newColumn.DataType = System.Type.GetType("System.Int16");
                        newColumn.DefaultValue = 0;
                        newColumn.AllowDBNull = true;

                        newColumn = _dt.Columns.Add("Cod. Carro");
                        newColumn.DataType = System.Type.GetType("System.String");
                        newColumn.DefaultValue = "";
                        newColumn.AllowDBNull = true;

                        newColumn = _dt.Columns.Add("Tipo Carro");
                        newColumn.DataType = System.Type.GetType("System.String");
                        newColumn.DefaultValue = "";
                        newColumn.AllowDBNull = true;

                        newColumn = _dt.Columns.Add("Peso");
                        newColumn.DataType = System.Type.GetType("System.Decimal");
                        newColumn.DefaultValue = 0;
                        newColumn.AllowDBNull = true;

                        newColumn = _dt.Columns.Add("Completo");
                        newColumn.DataType = System.Type.GetType("System.Boolean");
                        newColumn.DefaultValue = true;
                        newColumn.AllowDBNull = true;

                        newColumn = _dt.Columns.Add("LineaConsolidacion");
                        newColumn.DataType = System.Type.GetType("System.Boolean");
                        newColumn.DefaultValue = 0;
                        newColumn.AllowDBNull = true;

                        newColumn = _dt.Columns.Add("UdsConsolidacion");
                        newColumn.DataType = System.Type.GetType("System.Int16");
                        newColumn.DefaultValue = false;
                        newColumn.AllowDBNull = true;

                        newColumn = _dt.Columns.Add("Peso Tipo Carro");
                        newColumn.DataType = System.Type.GetType("System.Decimal");
                        newColumn.DefaultValue = 0;
                        newColumn.AllowDBNull = true;

                        newColumn = _dt.Columns.Add("Mov. Expedicion");         // 13/12/12
                        newColumn.DataType = System.Type.GetType("System.Boolean");
                        newColumn.DefaultValue = false;
                        newColumn.AllowDBNull = true;
                    }
                    
                    private void InicializarVariables()
                    {
                        _nLinea =0;
                        _nMov = "";
                        _movExpedicion = false; // 13/12/12
                        _codProducto = "";
                        _desProducto = "";
                        _nCarro = 0;
                        _codTipoCarro = "";
                        _desTipoCarro = "";
                        _peso = 0;
                        _completo = false;
                        _pesoNetoCarro = 0;
                        _pesoBrutoCarro = 0;
                        _pesoTipoCarro = 0;
                    }

                    private void AsignarVariables(DataRow oRow)
                    {
                        _nLinea=int.Parse(oRow["Linea"].ToString());
                        _nMov = oRow["Mov. Produccion"].ToString();
                        _movExpedicion = bool.Parse(oRow["Mov. Expedicion"].ToString());    // 13/12/12
                        _codProducto = oRow["Cod. Producto"].ToString();
                        _desProducto = oRow["Producto"].ToString();
                        _nCarro = int.Parse(oRow["Carro"].ToString());
                        _codTipoCarro = oRow["Cod. Carro"].ToString();
                        _desTipoCarro = oRow["Tipo Carro"].ToString();
                        _peso = Single.Parse(oRow["Peso"].ToString());
                        _completo = bool.Parse(oRow["Completo"].ToString());
                        _pesoNetoCarro = CalcularPesoNetoCarro(_nCarro);
                        _pesoBrutoCarro = CalcularPesoBrutoCarro(_nCarro);
                        _consolidado = bool.Parse(oRow["LineaConsolidacion"].ToString());
                        _pesoTipoCarro = Single.Parse(oRow["Peso Tipo Carro"].ToString());
                        _udsConsolidacion = int.Parse(oRow["UdsConsolidacion"].ToString());
                    }

                    private void AñadirLinea()
                    {
                        int numCarro=NuevoCarro();

                        DataRow newRow = _pesajes.NewRow();                         
                        newRow["Mov. Produccion"] = _nMov;
                        newRow["Mov. Expedicion"] = _movExpedicion; // 13/12/12
                        if (_nCarro == 0)
                            newRow["Carro"] = numCarro;                            
                        else
                            newRow["Carro"] = _nCarro;

                        newRow["Cod. Producto"] = _codProducto;
                        newRow["Producto"] = _desProducto;

                        newRow["Cod. Carro"] = _codTipoCarro;
                        newRow["Tipo Carro"] = _desTipoCarro;

                        newRow["Peso"] = _peso;
                        newRow["Completo"] = _completo;

                        newRow["Peso Tipo Carro"] = _pesoTipoCarro;

                        _pesajes.Rows.Add(newRow);
                        
                        // Etiqueta para el carro
                        if (_nCarro == 0)
                        {
                            _imprimirEtiqueta = true;                            
                            if (_completo)
                                _nCarroEtiqueta = 0;
                            else
                                _nCarroEtiqueta = numCarro;
                        }
                        else
                            if (_completo)
                            {
                                _imprimirEtiqueta = true;
                                _nCarroEtiqueta = 0;
                            }
                                 

                        // actualizo las variables globales de clase para lanzar el evento DataUpdate
                        _nLinea=int.Parse(newRow["Linea"].ToString());
                        _nCarro = int.Parse(newRow["Carro"].ToString());
                    }

                    private void ActualizarLinea()
                    {
                        _imprimirEtiqueta = false;

                        DataRow[] filterRows=Seleccionar("Linea="+_nLinea,"");
                        try
                        {
                            //filterRows[0]["peso"]=_peso;
                            filterRows[0]["completo"]=_completo;
                            filterRows[0].AcceptChanges();

                            _imprimirEtiqueta = _completo;                                                        
                            _nCarroEtiqueta = 0;                            
                        }
                        catch
                        {}
                    }

                    private void EliminarLinea(int linea)
                    {
                        DataRow[] filterRows = Seleccionar("Linea=" + linea,"");
                        if(filterRows.Count()!=0)
                        {                            
                            int carro = int.Parse(filterRows[0]["Carro"].ToString());
                            string mov = filterRows[0]["Mov. Produccion"].ToString();
                            filterRows[0].Delete();

                            ThrowDataUpdateEvent(linea, carro, mov, DataUpdateEventArgs.eAction.delete);
                        }                        
                    }

                    private void ThrowDataUpdateEvent(int linea, int carro, string mov, DataUpdateEventArgs.eAction acc)
                    {
                        DataUpdateEventArgs eArgs = new DataUpdateEventArgs(linea, carro, mov, acc);
                        OnDataUpdate(eArgs);
                    }

                    private DataRow[] Seleccionar(string filtro, string strSort)
                    {
                        return(_pesajes.Select(filtro,strSort));
                    }

                    private void CompletarCarro()
                    {
                        DataRow[] filterRows = _pesajes.Select("Carro="+_nCarro);
                        foreach (DataRow oRow in filterRows)
                        {
                            oRow["Completo"] = _completo;
                            oRow.AcceptChanges();
                        }
                    }

                    private Single CalcularPesoNetoCarro(int numCarro)
                    {
                        Single pesoTotal = 0;
                        DataRow[] filterRows = _pesajes.Select("Carro="+numCarro);
                        foreach (DataRow oRow in filterRows)
                        {
                            pesoTotal += Single.Parse(oRow["Peso"].ToString());
                        }
                        return (pesoTotal);
                    }

                    private Single CalcularPesoBrutoCarro(int numCarro)
                    {
                        Single pesoTotal = 0;
                        Single pesoContenedor = 0;
                        DataRow[] filterRows = _pesajes.Select("Carro=" + numCarro);
                        if (filterRows.Count() != 0)
                        {
                            pesoContenedor = Single.Parse(filterRows[0]["Peso Tipo Carro"].ToString());
                            foreach (DataRow oRow in filterRows)
                            {
                                pesoTotal += Single.Parse(oRow["Peso"].ToString());
                            }
                        }
                        return (pesoTotal+pesoContenedor);
                    }
                    
                    // CONSOLIDACION DE LINEAS

                    private DataTable CrearDTConsolidacion()
                    {
                        DataTable newDT= new DataTable("consolidacion");
                        DataColumn newColumn;

                        newColumn = newDT.Columns.Add("Carro");
                        newColumn.DataType = System.Type.GetType("System.Int16");
                        newColumn.DefaultValue = 0;
                        newColumn.AllowDBNull = false;

                        return(newDT);
                    }

                    public void ConsolidarLineas(DataTable dtCarros)
                    {       
                        int numCarro=0;

                        DataTable dtConsolidados=CrearDTConsolidacion();                                            

                        foreach (DataRow lineaCarro in dtCarros.Rows)
                        {
                            numCarro=int.Parse(lineaCarro["Numero"].ToString());
                            ConsolidarCarro(numCarro, dtConsolidados);
                        }
                    }

                    private void ConsolidarCarro(int numeroCarro, DataTable dtConsolidados)
                    {
                        string tipoCarro = "";
                        string codProducto = "";
                        Single pesoProd = 0;
                        int udsCons=0;

                        DataRow[] pesajesCarro = _pesajes.Select("Carro=" + numeroCarro);
                        
                        foreach (DataRow lineaPesaje in pesajesCarro)
                        {
                            tipoCarro = lineaPesaje["Cod. Carro"].ToString();
                            codProducto = lineaPesaje["Cod. Producto"].ToString();
                            pesoProd = Single.Parse(lineaPesaje["Peso"].ToString());

                            udsCons=CarroConsolidado(dtConsolidados,numeroCarro)?0:1;
                            // se busca la línea de consolidación
                            Consolidar(tipoCarro, codProducto, pesoProd,udsCons);
                            if (udsCons == 1)
                            {
                                DataRow newRow = dtConsolidados.NewRow();
                                newRow["Carro"] = numeroCarro;
                                dtConsolidados.Rows.Add(newRow);
                            }
                        }
                    }
                    
                    private bool CarroConsolidado(DataTable dtConsolidados, int carro)
                    {                
                        DataRow[] lineasConsolidadas = dtConsolidados.Select("Carro=" + carro);
                        return (lineasConsolidadas.Count() != 0);
                    }

                    private void Consolidar(string tipoCarro, string codProducto, Single pesoProducto,int uds)
                    {
                        DataRow[] filtro=
                        _pesajes.Select("[Cod. Carro]='" + tipoCarro + "' AND [Cod. Producto]='" +
                                            codProducto + "' AND LineaConsolidacion=TRUE");

                        try
                        {                            
                            filtro[0]["UdsConsolidacion"] = int.Parse(filtro[0]["UdsConsolidacion"].ToString()) + uds;
                            filtro[0]["Peso"] = Single.Parse(filtro[0]["Peso"].ToString()) + pesoProducto;
                            filtro[0].AcceptChanges();
                        }
                        catch 
                        {
                            DataRow newRow = _pesajes.NewRow();
                            newRow["LineaConsolidacion"] = true;
                            newRow["UdsConsolidacion"] = uds;
                            newRow["Peso"] = pesoProducto;
                            newRow["Cod. Carro"] = tipoCarro;
                            newRow["Cod. Producto"] = codProducto;
                            _pesajes.Rows.Add(newRow);
                        }
                    }

                    private int NuevoCarro()
                    {
                        DataRow[] filtro = _pesajes.Select("","Carro DESC");
                        int numero = 0;
                        try
                        {
                            numero = int.Parse(filtro[0]["Carro"].ToString());
                            return (numero+1);
                        }
                        catch
                        {
                            numero = 1;
                            return (numero);
                        }
                    }

                #endregion

            }

        #endregion

        #region CARROS
        
        public class cCarrosEmpaquetado
        {
            #region VARIABLES

                private int _numero;
                private string _codTipo;
                private string _tipo;
                private Single _peso;
                private bool _completo;
                private bool _carroExpedicion; // 13/12/12
                //private bool _bloqueado;

                private DataTable _dtCarros;

            #endregion

            #region PROPIEDADES

                public int Numero
                {
                    get { return _numero; }
                    set { _numero = value; }
                }

                public string CodTipo
                {
                    get { return _codTipo; }
                    set { _codTipo = value; }
                }

                public string Tipo
                {
                    get { return _tipo; }
                    set { _tipo = value; }
                }

                public Single Peso
                {
                    get { return _peso; }
                    set { _peso = value; }
                }


                public bool Completo
                {
                    get { return _completo; }
                    set { _completo = value; }
                }

                public bool CarroExpedicion // 13/12/12
                {
                    get { return _carroExpedicion; }
                    set { _carroExpedicion = value; }
                }

                //public bool Bloqueado
                //{
                //    get { return _bloqueado; }
                //    set { _bloqueado = value; }
                //}

                public DataTable DTCarros
                {
                    get { return _dtCarros; }
                    set { _dtCarros = value; }
                }

            #endregion

            public cCarrosEmpaquetado()
            {
                _dtCarros= CreateDataTable();
            }

            #region METODOS

                public void Update()
                {                    
                    UpdateRow();
                }

                public void Delete()
                {
                    DeleteRow();
                }

                public void Clear()
                {
                    DTCarros.Clear();
                }

            #endregion

            #region METODOS PRIVADOS

                private DataTable CreateDataTable()
                {
                    DataTable newDT = new DataTable("carros");
                    DataColumn newColumn;

                    newColumn = newDT.Columns.Add("Numero");
                    newColumn.DataType = System.Type.GetType("System.Int16");
                    newColumn.DefaultValue = 0;
                    newColumn.AllowDBNull = false;

                    newColumn = newDT.Columns.Add("CodTipo");
                    newColumn.DataType = System.Type.GetType("System.String");
                    newColumn.DefaultValue = "";
                    newColumn.AllowDBNull = false;

                    newColumn = newDT.Columns.Add("Tipo");
                    newColumn.DataType = System.Type.GetType("System.String");
                    newColumn.DefaultValue = "";
                    newColumn.AllowDBNull = true;

                    newColumn = newDT.Columns.Add("Peso");
                    newColumn.DataType = System.Type.GetType("System.Single");
                    newColumn.DefaultValue = 0;
                    newColumn.AllowDBNull = false;

                    newColumn = newDT.Columns.Add("Completo");
                    newColumn.DataType = System.Type.GetType("System.Boolean");
                    newColumn.DefaultValue = false;
                    newColumn.AllowDBNull = false;

                    newColumn = newDT.Columns.Add("CarroExpedicion");           // 13/12/12
                    newColumn.DataType = System.Type.GetType("System.Boolean");
                    newColumn.DefaultValue = false;
                    newColumn.AllowDBNull = false;

                    //newColumn = newDT.Columns.Add("Bloqueado");
                    //newColumn.DataType = System.Type.GetType("System.Boolean");
                    //newColumn.DefaultValue = false;
                    //newColumn.AllowDBNull = false;

                    return (newDT);

                }

                private void AddRow()
                {
                    DataRow newRow = _dtCarros.NewRow();
                    SetProperties(ref newRow);
                    _dtCarros.Rows.Add(newRow);
                }

                private void UpdateRow()
                {
                    DataRow[] existRow = _dtCarros.Select("Numero=" + _numero);
                    if(existRow.Count()!=0)
                    {
                        SetProperties(ref existRow[0]);
                        existRow[0].AcceptChanges();
                    }
                    else
                    {
                        AddRow();
                    }
                }

                private void DeleteRow()
                {
                    DataRow[] existRow = _dtCarros.Select("Numero=" + _numero);
                    try
                    {
                        existRow[0].Delete();
                    }
                    catch 
                    {
                        
                    }
                }

                private void GetProperties(DataRow oRow)
                {
                    _numero = int.Parse(oRow["Numero"].ToString());
                    _codTipo = oRow["CodTipo"].ToString();
                    _tipo = oRow["Tipo"].ToString();
                    _peso = Single.Parse(oRow["Peso"].ToString());
                    _completo = bool.Parse(oRow["Completo"].ToString());
                    _carroExpedicion = bool.Parse(oRow["CarroExpedicion"].ToString()); // 13/12/12
                    //_bloqueado= bool.Parse(oRow["Bloqueado"].ToString());
                }

                private void SetProperties(ref DataRow oRow)
                {
                    oRow["Numero"]=_numero;
                    oRow["CodTipo"]=_codTipo;
                    oRow["Tipo"]=_tipo;
                    oRow["Peso"]=_peso;
                    oRow["Completo"]=_completo;
                    oRow["CarroExpedicion"] = _carroExpedicion; // 13/12/12
                    //oRow["Bloqueado"] = _bloqueado;
                }

                private void InitProperties()
                {
                    _numero = 0;
                    _codTipo = "";
                    _tipo = "";
                    _peso = 0;
                    _completo = false;
                    _carroExpedicion = false; // 13/12/12
                    //_bloqueado = false;
                }
                
            #endregion
        }
        #endregion

        #region LINEAS PEDIDO VENTA

            public class cLineasPedidoVenta
            {
                #region Variables privadas
                    private string _strKey;
                    private string _pedido;
                    private int _linea;
                    private string _cliente;
                    private string _codProducto;
                    private string _descProducto;
                    private string _descProducto2;
                    private int _cantPendiente;
                    private bool _aumento;
                    private bool _retirada;
                    // lineas de pedido
                    private DataTable _dtLineas;
                    private DataTable _dtKeys;
                #endregion

                #region Propiedades
                
                    public string StrKey
                    {
                        get { return _strKey; }
                        set { _strKey = value; }
                    }
                    public string Pedido
                    {
                        get { return _pedido; }
                        set { _pedido = value; }
                    }

                    public int Linea
                    {
                        get { return _linea; }
                        set { _linea = value; }
                    }

                    public string Cliente
                    {
                        get { return _cliente; }
                        set { _cliente = value; }
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

                    public string DescProducto2
                    {
                        get { return _descProducto2; }
                        set { _descProducto2 = value; }
                    }

                    public int CantPendiente
                    {
                        get { return _cantPendiente; }
                        set { _cantPendiente = value; }
                    }

                    public bool Aumento
                    {
                        get { return _aumento; }
                        set { _aumento = value; }
                    }

                    public bool Retirada
                    {
                        get { return _retirada; }
                        set { _retirada = value; }
                    }

                    public DataTable Datos
                    {
                        get { return _dtLineas; }
                        set { _dtLineas = value; }
                    }

                #endregion

                #region Metodos Privados

                private void GenerarDataTable()
                {
                    _dtLineas = new DataTable("datos");
                    DataColumn newColumn;

                    // Se crean las columnas comunes a todos los tipos de conteo                        
                    newColumn = _dtLineas.Columns.Add("Pedido");
                    newColumn.DataType = System.Type.GetType("System.String");
                    newColumn.AllowDBNull = false;                    

                    newColumn = _dtLineas.Columns.Add("Línea");
                    newColumn.DataType = System.Type.GetType("System.Int32");
                    newColumn.AllowDBNull = true;

                    newColumn = _dtLineas.Columns.Add("Producto");
                    newColumn.DataType = System.Type.GetType("System.String");
                    newColumn.DefaultValue = "";
                    newColumn.AllowDBNull = false;                    

                    newColumn = _dtLineas.Columns.Add("Descripción");
                    newColumn.DataType = System.Type.GetType("System.String");
                    newColumn.DefaultValue = "";
                    newColumn.AllowDBNull = false;

                    newColumn = _dtLineas.Columns.Add("Descripción 2");
                    newColumn.DataType = System.Type.GetType("System.String");
                    newColumn.DefaultValue = "";
                    newColumn.AllowDBNull = true;

                    newColumn = _dtLineas.Columns.Add("Cantidad");
                    newColumn.DataType = System.Type.GetType("System.Int16");
                    newColumn.DefaultValue = 0;
                    newColumn.AllowDBNull = false;

                    newColumn = _dtLineas.Columns.Add("Aumento");
                    newColumn.DataType = System.Type.GetType("System.Boolean");
                    newColumn.DefaultValue = 0;
                    newColumn.AllowDBNull = false;                    

                    newColumn = _dtLineas.Columns.Add("Retirada");
                    newColumn.DataType = System.Type.GetType("System.Boolean");
                    newColumn.DefaultValue = 0;
                    newColumn.AllowDBNull = false;

                    _dtKeys = new DataTable("keys");

                    newColumn = _dtKeys.Columns.Add("Pedido");
                    newColumn.DataType = System.Type.GetType("System.String");
                    newColumn.AllowDBNull = false;

                    newColumn = _dtKeys.Columns.Add("Línea");
                    newColumn.DataType = System.Type.GetType("System.Int32");
                    newColumn.AllowDBNull = true;

                    newColumn = _dtKeys.Columns.Add("Key");
                    newColumn.DataType = System.Type.GetType("System.String");
                    newColumn.DefaultValue = 0;
                    newColumn.AllowDBNull = false;                    
                }

                private void AddLine()
                {
                    DataRow newRow = _dtLineas.NewRow();
                    newRow["Pedido"] = _pedido;                    
                    newRow["Línea"] = _linea;
                    newRow["Producto"] = _codProducto;
                    newRow["Descripción"] = _descProducto;
                    newRow["Descripción 2"] = _descProducto2;
                    newRow["Cantidad"] = _cantPendiente;
                    newRow["Aumento"] = _aumento;
                    newRow["Retirada"] = _retirada;                    
                    _dtLineas.Rows.Add(newRow);

                    newRow = _dtKeys.NewRow();
                    newRow["Pedido"] = _pedido;
                    newRow["Línea"] = _linea;
                    newRow["Key"] = _strKey;
                    _dtKeys.Rows.Add(newRow);
                }

                private void DelLine()
                {
                    DataRow[] filterRows = _dtLineas.Select("Pedido='" + _pedido + "' AND Línea=" + _linea);
                    filterRows[0].Delete();

                    filterRows = _dtKeys.Select("Pedido='" + _pedido + "' AND Línea=" + _linea);
                    filterRows[0].Delete();
                }

                private void UpdateLine()
                {
                    DataRow[] filterRows = _dtLineas.Select("Pedido='" + _pedido + "' AND Línea=" + _linea);
                    filterRows[0]["Producto"] = _codProducto;
                    filterRows[0]["Descripción"] = _descProducto;
                    filterRows[0]["Descripción 2"] = _descProducto2;
                    filterRows[0]["Cantidad"] = _cantPendiente;
                    filterRows[0]["Aumento"] = _aumento;
                    filterRows[0]["Retirada"] = _retirada;                    
                    _dtLineas.AcceptChanges();
                }

                private void SelLine(string pPedido, int pLinea)
                {
                    DataRow[] filterRows = _dtLineas.Select("Pedido='" + pPedido + "' AND Línea=" + pLinea);
                    _pedido = filterRows[0]["Pedido"].ToString();
                    _linea = int.Parse(filterRows[0]["Línea"].ToString());
                    _codProducto = filterRows[0]["Producto"].ToString();
                    _descProducto = filterRows[0]["Descripción"].ToString();
                    _descProducto2 = filterRows[0]["Descripción 2"].ToString();
                    _cantPendiente = int.Parse(filterRows[0]["Cantidad"].ToString());
                    _aumento = bool.Parse(filterRows[0]["Aumento"].ToString());
                    _retirada = bool.Parse(filterRows[0]["Retirada"].ToString());
                    
                    filterRows = _dtKeys.Select("Pedido='" + pPedido + "' AND Línea=" + pLinea);
                    _strKey = filterRows[0]["Key"].ToString();
                }

                private bool SelLine(string producto)
                {
                    DataRow[] filterRows = _dtLineas.Select("Producto='" + producto + "'");
                    return (filterRows.Count() > 0);
                }

                private void DelAllLines()
                {
                    // se eliminan todos los datos del DataTable
                    _dtLineas.Clear();
                    _dtKeys.Clear();
                }

                private void InitPtoperties()
                {
                    _pedido = "";
                    _linea = 0;
                    _codProducto = "";
                    _descProducto = "";
                    _descProducto2 = "";
                    _cantPendiente = 0;
                    _aumento = false;
                    _retirada = false;
                    _strKey = "";
                }

                #endregion

                #region Metodos

                public cLineasPedidoVenta()
                {
                    GenerarDataTable();
                }

                public void Add()
                {
                    AddLine();
                }

                public void Update()
                {
                    UpdateLine();
                }

                public void Delete()
                {
                    DelLine();
                    InitPtoperties();
                }

                public void DeleteAll()
                {
                    DelAllLines();
                    InitPtoperties();
                }

                public void Get(string pPedido, int pLinea)
                {
                    SelLine(pPedido,pLinea);
                }

                public bool ItemExists(string itemCode)
                {
                    return SelLine(itemCode);
                }

                public int CountLines()
                {
                    if (_dtLineas != null)
                    {
                        return (_dtLineas.Rows.Count);
                    }
                    else
                    {
                        return (0);
                    }
                }

                #endregion
            }

        #endregion

        #region DATOS AUXILIARES

            private void GetTurnos()
            {
                cProduccion oProduccion = new cProduccion(this._empresaLogin);
                _turnos = oProduccion.GetTurnos();
            }

            private string DescripcionTurno(string codigo)
            {
                DataRow[] filtro = _turnos.Select("codigo='" + codigo + "'");
                try
                {
                    string descripcion = filtro[0]["descripcion"].ToString();
                    return (descripcion);
                }
                catch 
                {
                    return ("");
                }
            }

            private void GetCarrosSacas()
            {
                cProduccion oProduccion = new cProduccion(this._empresaLogin);
                _carros = oProduccion.GetCarrosSacas();
            }

            private string DescripcionCarro(string codigo)
            {
                DataRow[] filtro = _carros.Select("codigo='" + codigo + "'");
                try
                {
                    string descripcion=filtro[0]["descripcion"].ToString();
                    return (descripcion);
                }
                catch
                {
                    return("");
                }
            }

            public Single PesoCarro(string codigo)
            {
                DataRow[] filtro = _carros.Select("codigo='" + codigo + "'");
                try
                {
                    Single peso =Single.Parse(filtro[0]["peso"].ToString());
                    return (peso);
                }
                catch 
                {
                    return (0);
                }
            }

            private void GetEmpleados()
            {
                cProduccion oProduccion = new cProduccion(this._empresaLogin);
                _empleados = oProduccion.GetEmployees();
            }

            private string NombreEmpleado(string codigo)
            {
                DataRow[] filtro = _empleados.Select("codigo='" + codigo + "'");
                try
                {
                    string descripcion = filtro[0]["nombre"].ToString();
                    return (descripcion);
                }
                catch 
                {
                    return ("");
                }
            }

            private DataTable GetMovsEmpaquetado()
            {
                cProduccion oProduccion = new cProduccion(this._empresaLogin);
                return(oProduccion.GetMovsEmpaquetado(_tipo.ToString().ToUpper(),_codCliente, _numPedido));
            }

            private string DescripcionProducto(string codProducto)
            {
                cProduccion oProduccion = new cProduccion(this._empresaLogin);
                return(oProduccion.GetItemDescription(codProducto));
            }

        #endregion

    }
}
