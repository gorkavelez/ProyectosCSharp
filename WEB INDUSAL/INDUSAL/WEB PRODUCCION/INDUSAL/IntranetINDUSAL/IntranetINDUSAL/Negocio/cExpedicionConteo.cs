using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using INIKER.MovsProduccion;

namespace IntranetINDUSAL.Negocio
{
    public class cExpedicionConteo
    {
        #region Variables PRIVADAS
            private string _empresaLogin;
        // datos de cabecera
            private string _codRuta;
            private string _nomRuta;
            private string _codCliente;
            private string _nomCliente;
        // líneas del pedido de venta
            private cLineasPedidoVenta _pedido;
            private string _nPedido;
            private int _nLinea;
            private string _codProducto;
            private string _descProducto;
            private string _descProducto2;
            private int _cantPendiente;
            private bool _aumento;
            private bool _retirada;

        // pesajes
            private string _codFacturacion;
            private string _descFacturacion;
            private cPesajes _pesajeEnCurso;
 
        // líneas del albarán de venta
            private cLineasAlbaranVenta _albaran;
        #endregion

        #region Metodos PRIVADOS

            private void InitProperties()
            {
                _codRuta= "";
                _nomRuta= "";
                _codCliente= "";
                _nomCliente= "";           
                
                _nPedido= "";
                _nLinea= 0;
                _codProducto= "";
                _descProducto= "";
                _descProducto2= "";
                _cantPendiente= 0;
                _aumento= false;
                _retirada= false;

           
                _codFacturacion = "";
                _descFacturacion = "";

                _pedido = null;
                _albaran=null;

            }

            private void DelOrderLine()
            {
                // elimina una línea del pedido de venta
                _pedido.Pedido = _nPedido;
                _pedido.Linea = _nLinea;
                _pedido.Delete();
            }

            private void SelOrderLine()
            {
                // selecciona una línea del pedido de venta                
                _pedido.Get(_nPedido,_nLinea);
                // se extraen los datos de la línea seleccionada
                // para que sean accesibles desde fuera de la clase
                GetOrderLineData();
            }

            private void UpdOrderLine()
            {
                SetOrderLineData();
                _pedido.Update();
                
            }

            private void GetOrderLineData()
            {
                this._codProducto = _pedido.CodProducto;
                this._descProducto = _pedido.DescProducto;
                this._descProducto2 = _pedido.DescProducto2;
                this._cantPendiente = _pedido.CantPendiente;
                this._aumento = _pedido.Aumento;
                this._retirada = _pedido.Retirada;
            }

            private void SetOrderLineData()
            {
                _pedido.CodProducto = this._codProducto;
                _pedido.DescProducto = this._descProducto;
                _pedido.DescProducto2 = this._descProducto2;
                _pedido.CantPendiente = this._cantPendiente;
                _pedido.Aumento = this._aumento;
                _pedido.Retirada = this._retirada;
            }

            private void GetUnregisterMovs(string empresaLogin)
            {
                cProduccion oProduccion = new cProduccion(empresaLogin);
                MovsProdSinRegINDUSAL[] unregMovs= oProduccion.GetUnregisterMovs(this._codCliente);
                foreach (MovsProdSinRegINDUSAL oMov in unregMovs)
                {
                    _pesajeEnCurso.NMov=oMov.Num_movimiento;
                    _pesajeEnCurso.Fecha=oMov.Fecha;
                    _pesajeEnCurso.Hora=oMov.Hora;
                    _pesajeEnCurso.Peso=Single.Parse(oMov.Cantidad_enviar.ToString());
                    _pesajeEnCurso.CodCarro=oMov.Cod_carro;
                    _pesajeEnCurso.NCarro=oMov.Num_carro;
                    _pesajeEnCurso.Completo=oMov.Carro_completo;
                    _pesajeEnCurso.Add();
                }
            }

            private void RegisterMovs()
            { 

            }
        
        #endregion

        #region Propiedades

            public string CodRuta
            {
                get { return _codRuta; }
                set { _codRuta = value; }
            }

            public string NomRuta
            {
                get { return _nomRuta; }
                set { _nomRuta = value; }
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

            public string NPedido
            {
                get { return _nPedido; }
                set { _nPedido = value; }
            }

            public int NLinea
            {
                get { return _nLinea; }
                set { _nLinea = value; }
            }            

            public DataTable Pedido
            {
                // propiedad de sólo lectura
                get { return _pedido.Datos; }
            }

            public string PedidoCodProducto
            {
                get { return _codProducto; }
                set { _codProducto = value; }
            }

            public string PedidoDescProducto
            {
                get { return _descProducto; }
                set { _descProducto = value; }
            }

            public string PedidoDescProducto2
            {
                get { return _descProducto2; }
                set { _descProducto2 = value; }
            }

            public int PedidoCantPendiente
            {
                get { return _cantPendiente; }
                set { _cantPendiente = value; }
            }

            public bool PedidoAumento
            {
                get { return _aumento; }
                set { _aumento = value; }
            }

            public bool PedidoRetirada
            {
                get { return _retirada; }
                set { _retirada = value; }
            }

            public string CodFacturacion
            {
                get { return _codFacturacion; }
                set { _codFacturacion = value; }
            }

            public string DescFacturacion
            {
                get { return _descFacturacion; }
                set { _descFacturacion = value; }
            }

            public cPesajes PesajeEnCurso
            {
                get { return _pesajeEnCurso; }
                set { _pesajeEnCurso = value; }
            }   
            
        #endregion

        #region Metodos
        // Constructores de clase
            public cExpedicionConteo()
            {
                // Constructor por defecto
                _pesajeEnCurso = new cPesajes();
                InitProperties();
            }

            public cExpedicionConteo(string empresa)
            {
                _empresaLogin = empresa;
                _pesajeEnCurso = new cPesajes();
                InitProperties();
            }

            public DataTable ArrayToDataTable(INIKER.LineasVenta.ListaLineasVentaINDUSAL[] arrayLineas)
            {
                if (_pedido == null)
                    _pedido = new cLineasPedidoVenta();

                foreach (INIKER.LineasVenta.ListaLineasVentaINDUSAL itLinea in arrayLineas)
                {
                    // para cada item del array de líneas de pedido de venta del cliente,
                    // se genera un DataRow con los datos necesarios
                    _pedido.StrKey = itLinea.Key;
                    _pedido.Pedido = itLinea.Document_No;
                    _pedido.Linea = itLinea.Line_No;
                    _pedido.CodProducto = itLinea.No;
                    _pedido.DescProducto = itLinea.Description;
                    _pedido.DescProducto2 = itLinea.Description_2;
                    _pedido.CantPendiente = int.Parse(itLinea.Outstanding_Quantity.ToString());
                    _pedido.Aumento= itLinea.Aumento;
                    _pedido.Retirada = itLinea.Retirada;                    
                    _pedido.Add();
                }
                return (_pedido.Datos);
            }

            public INIKER.LineasVenta.ListaLineasVentaINDUSAL[] OrderLinesToArray()
            {
                INIKER.LineasVenta.ListaLineasVentaINDUSAL[] arrayLineas=null;

                if (_pedido != null)
                {
                    arrayLineas = new INIKER.LineasVenta.ListaLineasVentaINDUSAL[_pedido.Datos.Rows.Count];
                    int iLinea = 0;
                    foreach (DataRow oRow in _pedido.Datos.Rows)
                    {
                        _pedido.Get(oRow["Pedido"].ToString(), int.Parse(oRow["Línea"].ToString()));
                        INIKER.LineasVenta.ListaLineasVentaINDUSAL lineaPedido =
                            new INIKER.LineasVenta.ListaLineasVentaINDUSAL();
                        // para cada item del array de líneas de pedido de venta del cliente,
                        // se genera un DataRow con los datos necesarios
                        lineaPedido.Key = _pedido.StrKey;
                        lineaPedido.Document_Type = INIKER.LineasVenta.Document_Type.Order;
                        lineaPedido.Document_No = _pedido.Pedido;
                        lineaPedido.Line_No = _pedido.Linea;
                        lineaPedido.No = _pedido.CodProducto;
                        lineaPedido.Description = _pedido.DescProducto;
                        lineaPedido.Description_2 = _pedido.DescProducto2;
                        lineaPedido.Quantity = _pedido.CantPendiente;
                        lineaPedido.Aumento = _pedido.Aumento;
                        lineaPedido.Retirada = _pedido.Retirada;
                        arrayLineas[iLinea++] = lineaPedido;                        
                    }
                }
                return (arrayLineas);
            }

            public void DeleteOrderLine()
            {
                this.DelOrderLine();
            }

            public void SelectOrderLine()
            {
                SelOrderLine();
            }

            public void UpdateOrderLine()
            {
                UpdOrderLine();
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

            public void CargarMovsSinRegistrar()
            {
                GetUnregisterMovs(_empresaLogin);
            }

            public INIKER.MovsProduccion.MovsProdSinRegINDUSAL[] ProductionLinesToArray()
            {
                INIKER.MovsProduccion.MovsProdSinRegINDUSAL[] arrayLineas = null;

                if (_pesajeEnCurso != null)
                {
                    arrayLineas = new INIKER.MovsProduccion.MovsProdSinRegINDUSAL[_pesajeEnCurso.Lineas.Rows.Count];
                    int iLinea = 0;
                    foreach (DataRow oRow in _pesajeEnCurso.Lineas.Rows)
                    {
                        _pesajeEnCurso.SelectByLine(int.Parse(oRow["nLinea"].ToString()));

                        INIKER.MovsProduccion.MovsProdSinRegINDUSAL lineaMov =
                            new INIKER.MovsProduccion.MovsProdSinRegINDUSAL();
                        // para cada item del array de líneas de pedido de venta del cliente,
                        // se genera un DataRow con los datos necesarios
                        //lineaMov.Key = _pedido.StrKey;
                        lineaMov.Num_movimiento = _pesajeEnCurso.NMov;
                        lineaMov.Fecha=_pesajeEnCurso.Fecha;
                        lineaMov.Hora= _pesajeEnCurso.Hora;
                        lineaMov.Cod_Cliente = _codCliente;
                        lineaMov.Producto = _codFacturacion;
                        lineaMov.UDM = "KG";
                        lineaMov.Cantidad_enviar= decimal.Parse(_pesajeEnCurso.Peso.ToString());
                        lineaMov.Cod_carro = _pesajeEnCurso.CodCarro;
                        lineaMov.Num_carro = _pesajeEnCurso.NCarro;
                        lineaMov.Carro_completo = _pesajeEnCurso.Completo;
                        arrayLineas[iLinea++] = lineaMov;                        
                    }
                }
                return (arrayLineas);
            }

        #endregion

        #region Pesajes

            public class cPesajes
            {
                // VARIABLES LOCALES

                private int _nLinea;
                private string _nMov;                
                private DateTime _fecha;
                private DateTime _hora;                                                
                private int _nCarro;
                private string _codCarro;
                private Single _peso;
                private bool _completo;
                private Single _pesoCarro;

                private DataTable _pesajes;

                // PROPIEDADES

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

                public string CodCarro
                {
                    get { return _codCarro; }
                    set { _codCarro = value; }
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

                public DataTable Lineas
                {
                    get { return _pesajes; }
                    set { _pesajes = value; }
                }

                // CONSTRUCTOR
                public cPesajes()
                {
                    CreateDataTable(ref _pesajes);
                }

                // MIEMBROS PRIVADOS
                private void CreateDataTable(ref DataTable _dt)
                {
                    _dt = new DataTable("pesajes");
                    DataColumn newColumn;

                    newColumn = _dt.Columns.Add("nLinea");
                    newColumn.DataType = System.Type.GetType("System.Int16");
                    newColumn.AutoIncrement = true;
                    newColumn.AutoIncrementSeed = 1;
                    newColumn.AutoIncrementStep = 1;
                    
                    newColumn = _dt.Columns.Add("nMovimientoProd");
                    newColumn.DataType = System.Type.GetType("System.String");
                    newColumn.DefaultValue = "";
                    newColumn.AllowDBNull = true;

                    newColumn = _dt.Columns.Add("fecha");
                    newColumn.DataType = System.Type.GetType("System.DateTime");
                    newColumn.DefaultValue=DateTime.Today.ToShortDateString();
                    newColumn.AllowDBNull = false;

                    newColumn = _dt.Columns.Add("hora");
                    newColumn.DataType = System.Type.GetType("System.DateTime");
                    newColumn.DefaultValue = DateTime.Now.ToShortTimeString();
                    newColumn.AllowDBNull = false;

                    newColumn = _dt.Columns.Add("numCarro");
                    newColumn.DataType = System.Type.GetType("System.Int16");
                    newColumn.DefaultValue = 0;
                    newColumn.AllowDBNull = false;

                    newColumn = _dt.Columns.Add("codCarro");
                    newColumn.DataType = System.Type.GetType("System.String");
                    newColumn.DefaultValue = "";
                    newColumn.AllowDBNull = false;

                    newColumn = _dt.Columns.Add("peso");
                    newColumn.DataType = System.Type.GetType("System.Decimal");
                    newColumn.DefaultValue = 0;
                    newColumn.AllowDBNull = false;

                    newColumn = _dt.Columns.Add("completo");
                    newColumn.DataType = System.Type.GetType("System.Boolean");
                    newColumn.DefaultValue = true;
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
                    _nMov = "";
                    _nCarro = 0;
                    _codCarro = "";
                    _peso = 0;
                    _completo = false;
                    _pesoCarro = 0;
                }

                private void AsignarVariables(DataRow oRow)
                {
                    _nLinea=int.Parse(oRow["nLinea"].ToString());
                    _nMov = oRow["nMovimientoProd"].ToString();
                    _nCarro = int.Parse(oRow["numCarro"].ToString());
                    _codCarro = oRow["codCarro"].ToString();
                    _peso = Single.Parse(oRow["peso"].ToString());
                    _completo = bool.Parse(oRow["completo"].ToString());
                    _pesoCarro = PesoTotalCarro(_nCarro);
                }

                private void AñadirLinea()
                {
                    DataRow newRow = _pesajes.NewRow();                    
                    newRow["nMovimientoProd"] = _nMov;

                    if (_nCarro == 0)
                        newRow["numCarro"] = newRow["nLinea"];
                    else
                        newRow["numCarro"] = _nCarro;

                    newRow["codCarro"] = _codCarro;
                    newRow["peso"] = _peso;
                    newRow["completo"] = _completo;

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
                    return(_pesajes.Select("nLinea=" + numLinea));
                }

                private void CompletarCarro()
                {
                    DataRow[] filterRows = _pesajes.Select("numCarro="+_nCarro);
                    foreach (DataRow oRow in filterRows)
                    {
                        oRow["completo"] = _completo;
                        oRow.AcceptChanges();
                    }
                }

                private Single PesoTotalCarro(int numCarro)
                {
                    Single pesoTotal = 0;
                    DataRow[] filterRows = _pesajes.Select("numCarro="+numCarro);
                    foreach (DataRow oRow in filterRows)
                    {
                        pesoTotal += Single.Parse(oRow["peso"].ToString());
                    }
                    return (pesoTotal);
                }
            }

        #endregion

            private class cLineasPedidoVenta
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

            private class cLineasAlbaranVenta
            { }

            
    }
}
