using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using INIKER.MovsProduccion;

namespace IntranetINDUSAL.Negocio
{
    public class cEmpaquetado
    {
        #region Variables PRIVADAS
            private string _empresaLogin;
            private string _datoTeclado;

        // datos de cabecera
            private string _codCliente;
            private string _nomCliente;
            private string _numPedido;
            private string _codProducto;
            private string _desProducto;

        // pedidos abiertos del cliente
            private DataTable _pedidos;
            private DataTable _turnos;
            private DataTable _carros;            
            private DataTable _empleados;

        // líneas del pedido de venta
            private cLineasPedidoVenta _pedido;        

        // pesajes
            private cPesajes _pesajeEnCurso;
 
        #endregion

        #region Metodos PRIVADOS

            private void InitProperties()
            {
                _codCliente= "";
                _nomCliente= "";
                _numPedido = "";
                _codProducto = "";
                _desProducto = "";
            }

            private void RegistrarLineaPesaje()
            {
                cProduccion oProduccion = new cProduccion(_empresaLogin);
                oProduccion.RegistrarPesaje(_pesajeEnCurso.CodEmpleado, _pesajeEnCurso.CodTurno, _codCliente, _numPedido, _codProducto,
                    "", decimal.Parse(_pesajeEnCurso.Peso.ToString()), _pesajeEnCurso.NCarro, _pesajeEnCurso.CodTipoCarro, _pesajeEnCurso.Completo, _pesajeEnCurso.Consolidado, false);
            }

        #endregion

        #region PROPIEDADES

            public string EmpresaLogin
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

            public string DesProducto
            {
                get { return _desProducto; }
                set { _desProducto = value; }
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
            
        #endregion

        #region METODOS PUBLICOS

        // Constructores de clase
            public cEmpaquetado(string empresa)
            {
                _empresaLogin = empresa;

                GetTurnos();
                GetCarrosSacas();
                GetEmpleados();

                _pesajeEnCurso = new cPesajes();
                InitProperties();
            }
 
            public bool ItemInOrder(string itemCode)
            {
                try
                {
                    return (_pedido.ItemExists(itemCode));
                }
                catch (NullReferenceException ex)
                {
                    return (false);
                }
            }

            public bool DatosSinRegistrar()
            {
                return (_pesajeEnCurso.CountLines() != 0);
            }

            public void Registrar()
            {
                if(_codCliente!="000000")                
                    _pesajeEnCurso.ConsolidarLineas();

                foreach (DataRow lineaPesaje in _pesajeEnCurso.Lineas.Rows)
                {
                    _pesajeEnCurso.SelectByLine(int.Parse(lineaPesaje["Linea"].ToString()));
                    RegistrarLineaPesaje();
                }
            }

        #endregion

        #region LINEAS DE PESAJE

            public class cPesajes
            {
                #region VARIABLES PRIVADAS

                    private int _nLinea;
                    private string _nMov;                
                    private DateTime _fecha;
                    private DateTime _hora;                                                
                    private int _nCarro;
                    private string _codTipoCarro;
                    private string _desTipoCarro;                
                    private Single _peso;
                    private bool _completo;
                    private Single _pesoCarro;
                    private string _codTurno;
                    private string _desTurno;
                    private string _codEmpleado;
                    private string _nomEmpleado;
                    private bool _consolidado;
                                                    
                    private DataTable _pesajes;

                #endregion

                #region PROPIEDADES

                    public string NMov
                {
                    get { return _nMov; }
                    set { _nMov = value; }
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

                    public Single PesoCarro
                    {
                        get { return _pesoCarro; }
                        set { _pesoCarro = value; }
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

                    public bool Consolidado
                    {
                        get { return _consolidado; }                    
                    }

                    public DataTable Lineas
                    {
                        get { return _pesajes; }
                        set { _pesajes = value; }
                    }

                #endregion
                
                // CONSTRUCTOR
                public cPesajes()
                {
                    CreateDataTable(ref _pesajes);
                    InicializarVariables();
                }

                // MIEMBROS PRIVADOS
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

                    newColumn = _dt.Columns.Add("Cod. Turno");
                    newColumn.DataType = System.Type.GetType("System.String");
                    newColumn.DefaultValue = "";
                    newColumn.AllowDBNull = false;

                    newColumn = _dt.Columns.Add("Turno");
                    newColumn.DataType = System.Type.GetType("System.String");
                    newColumn.DefaultValue = "";
                    newColumn.AllowDBNull = false;

                    newColumn = _dt.Columns.Add("Cod. Empleado");
                    newColumn.DataType = System.Type.GetType("System.String");
                    newColumn.DefaultValue = "";
                    newColumn.AllowDBNull = false;

                    newColumn = _dt.Columns.Add("Empleado");
                    newColumn.DataType = System.Type.GetType("System.String");
                    newColumn.DefaultValue = "";
                    newColumn.AllowDBNull = false;

                    newColumn = _dt.Columns.Add("Carro");
                    newColumn.DataType = System.Type.GetType("System.Int16");
                    newColumn.DefaultValue = 0;
                    newColumn.AllowDBNull = false;

                    newColumn = _dt.Columns.Add("Cod. Carro");
                    newColumn.DataType = System.Type.GetType("System.String");
                    newColumn.DefaultValue = "";
                    newColumn.AllowDBNull = false;

                    newColumn = _dt.Columns.Add("Tipo Carro");
                    newColumn.DataType = System.Type.GetType("System.String");
                    newColumn.DefaultValue = "";
                    newColumn.AllowDBNull = false;

                    newColumn = _dt.Columns.Add("Peso");
                    newColumn.DataType = System.Type.GetType("System.Decimal");
                    newColumn.DefaultValue = 0;
                    newColumn.AllowDBNull = false;

                    newColumn = _dt.Columns.Add("Completo");
                    newColumn.DataType = System.Type.GetType("System.Boolean");
                    newColumn.DefaultValue = true;
                    newColumn.AllowDBNull = false;

                    newColumn = _dt.Columns.Add("UdsConsolidacion");
                    newColumn.DataType = System.Type.GetType("System.Int16");
                    newColumn.DefaultValue = 0;
                    newColumn.AllowDBNull = false;

                    newColumn = _dt.Columns.Add("LineaConsolidacion");
                    newColumn.DataType = System.Type.GetType("System.Boolean");
                    newColumn.DefaultValue = false;
                    newColumn.AllowDBNull = false;
                }
                
                // MIEMBROS PUBLICOS
                public int CountLines()
                {
                    return (_pesajes.Rows.Count);
                }

                public void Add()
                {
                    if (_nCarro == 0)
                    {
                        AñadirLinea();
                    }
                    else
                    {
                        Single pesoTotalActual = PesoTotalCarro(_nCarro);
                        if (_peso < pesoTotalActual)
                        {
                            ActualizarLinea();
                        }
                        if (_peso > pesoTotalActual)
                        {
                            _peso -= pesoTotalActual;
                            AñadirLinea();                            
                        }
                        
                        CompletarCarro();                        

                    }                   
                    InicializarVariables();
                }

                public bool SelectByLine(int nLinea)
                {
                    DataRow[] lineasFiltro= Seleccionar(nLinea);

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

                // MIEMBROS PRIVADOS

                private void InicializarVariables()
                {
                    _nLinea =0;
                    _nMov = "";
                    _nCarro = 0;
                    _codTipoCarro = "";
                    _desTipoCarro = "";
                    _peso = 0;
                    _completo = false;
                    _pesoCarro = 0;
                    _codTurno = "";
                    _desTurno = "";
                    _codEmpleado = "";
                    _nomEmpleado = "";
                }

                private void AsignarVariables(DataRow oRow)
                {
                    _nLinea=int.Parse(oRow["Linea"].ToString());
                    _nMov = oRow["Mov. Produccion"].ToString();
                    _nCarro = int.Parse(oRow["Carro"].ToString());
                    _codTipoCarro = oRow["Cod. Carro"].ToString();
                    _desTipoCarro = oRow["Tipo Carro"].ToString();
                    _codTurno = oRow["Cod. Turno"].ToString();
                    _desTurno = oRow["Turno"].ToString();
                    _codEmpleado = oRow["Cod. Empleado"].ToString();
                    _nomEmpleado = oRow["Empleado"].ToString();
                    _peso = Single.Parse(oRow["Peso"].ToString());
                    _completo = bool.Parse(oRow["Completo"].ToString());
                    _pesoCarro = PesoTotalCarro(_nCarro);
                    _consolidado = bool.Parse(oRow["LineaConsolidacion"].ToString());
                }

                private void AñadirLinea()
                {
                    int numCarro=NuevoCarro();

                    DataRow newRow = _pesajes.NewRow();                         
                    newRow["Mov. Produccion"] = _nMov;
                    if (_nCarro == 0)
                        //newRow["Carro"] = newRow["Linea"];
                        newRow["Carro"] = numCarro;
                    else
                        newRow["Carro"] = _nCarro;

                    newRow["Cod. Carro"] = _codTipoCarro;
                    newRow["Tipo Carro"] = _desTipoCarro;

                    newRow["Cod. Turno"]=_codTurno;
                    newRow["Turno"]=_desTurno;

                    newRow["Cod. Empleado"]=_codEmpleado;
                    newRow["Empleado"] = _nomEmpleado;

                    newRow["Peso"] = _peso;
                    newRow["Completo"] = _completo;

                    _pesajes.Rows.Add(newRow);
                }

                private void ActualizarLinea()
                {
                    DataRow[] filterRows=Seleccionar(_nLinea);
                    try
                    {
                        filterRows[0]["peso"]=_peso;
                        filterRows[0]["completo"]=_completo;
                        filterRows[0].AcceptChanges();
                    }
                    catch(NullReferenceException ex)
                    {}
                }

                private DataRow[] Seleccionar(int numLinea)
                {
                    return(_pesajes.Select("Linea=" + numLinea));
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

                private Single PesoTotalCarro(int numCarro)
                {
                    Single pesoTotal = 0;
                    DataRow[] filterRows = _pesajes.Select("Carro="+numCarro);
                    foreach (DataRow oRow in filterRows)
                    {
                        pesoTotal += Single.Parse(oRow["Peso"].ToString());
                    }
                    return (pesoTotal);
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

                public void ConsolidarLineas()
                {
                    //DataTable _pesajesAux=_pesajes.Clone();
                    DataRow[] lineasConsolidar = _pesajes.Select("LineaConsolidacion=false And Completo=true");
                    

                    DataTable dtConsolidados=CrearDTConsolidacion();
                    
                    string tipoCarro="";
                    Int16 nCarro=0;
                    Single pesoCarro=0;                    

                    foreach (DataRow linea in lineasConsolidar)
                    {
                        if (bool.Parse(linea["Completo"].ToString()))
                        {
                            nCarro = Int16.Parse(linea["Carro"].ToString());
                            if (!CarroConsolidado(dtConsolidados, nCarro))
                            {
                                tipoCarro = linea["Cod. Carro"].ToString();
                                pesoCarro = PesoTotalCarro(nCarro);
                                Consolidar(tipoCarro, pesoCarro);
                                DataRow newRow = dtConsolidados.NewRow();
                                newRow["Carro"] = nCarro;
                                dtConsolidados.Rows.Add(newRow);
                            }
                        }
                    }
                }
                
                private bool CarroConsolidado(DataTable dtConsolidados, Int16 carro)
                {                
                    DataRow[] lineasConsolidadas = dtConsolidados.Select("Carro=" + carro);
                    return (lineasConsolidadas.Count() != 0);
                }

                private void Consolidar(string tipoCarro, Single pesoCarro)
                {
                    DataRow[] filtro=
                    _pesajes.Select("LineaConsolidacion=true And [Cod. Carro]='" + tipoCarro + "'");

                    try
                    {
                        filtro[0]["Cod. Carro"] = tipoCarro;
                        filtro[0]["UdsConsolidacion"] = int.Parse(filtro[0]["UdsConsolidacion"].ToString()) + 1;
                        filtro[0]["Peso"] = Single.Parse(filtro[0]["Peso"].ToString()) + pesoCarro;
                        filtro[0].AcceptChanges();
                    }
                    catch (IndexOutOfRangeException ex)
                    {
                        DataRow newRow = _pesajes.NewRow();
                        newRow["LineaConsolidacion"] = true;
                        newRow["UdsConsolidacion"] = 1;
                        newRow["Peso"] = pesoCarro;
                        newRow["Cod. Carro"] = tipoCarro;
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
                    catch (IndexOutOfRangeException ex)
                    {
                        numero = 1;
                        return (numero);
                    }                    
                }
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

            private void GetTurnos()
            {
                cProduccion oProduccion = new cProduccion(this._empresaLogin);
                _turnos = oProduccion.GetTurnos();
            }

            private void GetCarrosSacas()
            {
                cProduccion oProduccion = new cProduccion(this._empresaLogin);
                _carros = oProduccion.GetCarrosSacas();
            }
        
            private void GetEmpleados()
            {
                cProduccion oProduccion = new cProduccion(this._empresaLogin);
                _empleados = oProduccion.GetEmployees();
            }
    }
}
