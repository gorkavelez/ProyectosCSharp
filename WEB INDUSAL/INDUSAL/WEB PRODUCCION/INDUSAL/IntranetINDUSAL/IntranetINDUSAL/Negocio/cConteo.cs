using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IntranetINDUSAL.Negocio
{
    public class cConteo
    {
        #region estructuras auxiliares de la clase 
            
            public enum eTipoConteo
            {
                conteo,
                oxido,
                desaprestado
            }

            public enum eDatoTeclado
            {
                cliente,
                cantidad,
                operario
            }

            public class cLineaConteo
            {
                // estructura para almacenar los datos de las diferentes pesadas
                // de ropa asociadas a un mismo lavado, para el caso de Lavadora

                #region VARIABLES PRIVADAS

                    // por cada pesada, son necesarios los siguientes datos
                    private int _nLinea;
                    private string _codProducto;
                    private string _nomProducto;
                    private string _nSerie;
                    private int _cantidad;
                    private DateTime _fecha;
                    private DateTime _hora;
                    private bool _procesada;
                    // INI DSC 98
                    private string _operario;
                    private string _nomOperario;                    
                    // FIN DSC 98
                    
                    private DataTable _dtLineasConteo;                    
                    
                #endregion

                #region PROPIEDADES

                    public int NLinea
                    {
                        get { return _nLinea; }
                        set { _nLinea = value; }
                    }

                    public string CodProducto
                    {
                        get { return _codProducto; }
                        set { _codProducto= value; }
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

                    public int Cantidad
                    {
                        get { return _cantidad; }
                        set { _cantidad = value; }
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

                    public bool Procesada
                    {
                        get { return _procesada; }
                        set { _procesada = value; }
                    }

                    public DataTable Datos
                    {
                        get { return _dtLineasConteo; }
                        set { _dtLineasConteo = value; }
                    }
                    // INI DSC 98
                    public string Operario
                    {
                        get { return _operario; }
                        set { _operario = value; }
                    }

                    public string NomOperario
                    {
                        get { return _nomOperario; }
                        set { _nomOperario = value; }
                    }
                    // FIN DSC 98

                #endregion

                #region METODOS PUBLICOS

                    public cLineaConteo()
                    {
                        GenerarDataTable();
                        InitProperties();
                    }

                    public void Add()
                    {
                        if (_nLinea == 0)
                            AddLine();
                        else
                            UpdateLine();

                        InitProperties();
                    }

                    public void Delete()
                    {
                        DelLine();
                        InitProperties();
                    }

                    public void Clear()
                    {
                        DelAllLines();
                    }

                    public void Update()
                    {
                        UpdateLine();
                    }

                    public void Select()
                    {
                        SelLine();
                    }

                #endregion

                #region METODOS PRIVADOS

                    private void GenerarDataTable()
                    {
                        _dtLineasConteo = new DataTable("datos");
                        DataColumn newColumn;

                        // Se crean las columnas comunes a todos los tipos de conteo                                    

                        newColumn = _dtLineasConteo.Columns.Add("nLinea");
                        newColumn.DataType = Type.GetType("System.Int16");
                        newColumn.AllowDBNull = false;
                        newColumn.AutoIncrement = true;
                        newColumn.AutoIncrementSeed = 1;
                        newColumn.AutoIncrementStep = 1;                        

                        newColumn = _dtLineasConteo.Columns.Add("codProducto");
                        newColumn.DataType = System.Type.GetType("System.String");
                        newColumn.AllowDBNull = false;

                        newColumn = _dtLineasConteo.Columns.Add("nomProducto");
                        newColumn.DataType = System.Type.GetType("System.String");
                        newColumn.AllowDBNull = false;

                        newColumn = _dtLineasConteo.Columns.Add("nSerie");
                        newColumn.DataType = System.Type.GetType("System.String");
                        newColumn.DefaultValue = "";
                        newColumn.AllowDBNull = true;

                        newColumn = _dtLineasConteo.Columns.Add("cantidad");
                        newColumn.DataType = System.Type.GetType("System.Int16");
                        newColumn.AllowDBNull = false;

                        newColumn = _dtLineasConteo.Columns.Add("Fecha");
                        newColumn.DataType = System.Type.GetType("System.DateTime");                        

                        newColumn = _dtLineasConteo.Columns.Add("Fecha/Hora");
                        newColumn.DataType = System.Type.GetType("System.DateTime");

                        newColumn = _dtLineasConteo.Columns.Add("Procesada");
                        newColumn.DataType = System.Type.GetType("System.Boolean");
                        newColumn.DefaultValue = false;
                    }

                    private void AddLine()
                    {
                        // se busca si existe alguna línea de conteo con ese código de producto
                        DataRow newRow = SelProductLine();
                        // si no existe, se genera una nueva línea
                        if (newRow == null)
                        {
                            newRow = _dtLineasConteo.NewRow();

                            newRow["codProducto"] = _codProducto;
                            newRow["nomProducto"] = _nomProducto;
                            newRow["nSerie"] = _nSerie;
                            newRow["cantidad"] = _cantidad;
                            newRow["fecha"] = DateTime.Today;
                            newRow["fecha/hora"] = DateTime.Now;

                            _dtLineasConteo.Rows.Add(newRow);
                        }
                        else 
                        {
                            newRow["cantidad"] = int.Parse(newRow["cantidad"].ToString()) + _cantidad;
                            newRow.AcceptChanges();
                        }

                    }

                    private void DelLine()
                    {
                        DataRow[] filterRows = _dtLineasConteo.Select("nLinea=" + _nLinea);
                        filterRows[0].Delete();                        
                    }

                    private void UpdateLine()
                    {
                        DataRow[] filterRows = _dtLineasConteo.Select("nLinea=" + _nLinea);
                        filterRows[0]["codProducto"] = _codProducto;
                        filterRows[0]["nomProducto"] = _nomProducto;
                        filterRows[0]["nSerie"] = _nSerie;
                        filterRows[0]["cantidad"] = _cantidad;
                        _dtLineasConteo.AcceptChanges();
                    }

                    private void SelLine()
                    {
                        DataRow[] filterRows = _dtLineasConteo.Select("nLinea=" + _nLinea);
                        _nLinea = int.Parse(filterRows[0]["nLinea"].ToString());
                        _codProducto= filterRows[0]["codProducto"].ToString();
                        _nomProducto = filterRows[0]["nomProducto"].ToString();
                        _nSerie=filterRows[0]["nSerie"].ToString();
                        _cantidad= int.Parse(filterRows[0]["cantidad"].ToString());
                        _fecha = DateTime.Parse(filterRows[0]["Fecha"].ToString());
                        _hora = DateTime.Parse(filterRows[0]["Fecha/Hora"].ToString());
                    }

                    private DataRow SelProductLine()
                    {
                        DataRow[] filterRows = _dtLineasConteo.Select("codProducto='" + _codProducto + "'");
                        try
                        {
                            return (filterRows[0]);
                        }
                        catch
                        {
                            return (null);
                        }
                    }

                    private void DelAllLines()
                    {
                        // se eliminan todos los datos del DataTable
                        _dtLineasConteo.Clear();
                    }

                    private void InitProperties()
                    {
                        _nLinea = 0;
                        _codProducto = "";
                        _nomProducto = "";
                        _nSerie = "";
                        _cantidad = 0;                        
                    }

                #endregion
            }

            public class cCanalConteo
            {
                // estructura para almacenar los datos de las diferentes pesadas
                // de ropa asociadas a un mismo lavado, para el caso de Lavadora

                #region VARIABLES PRIVADAS

                // por cada pesada, son necesarios los siguientes datos
                private int _nLinea;
                private string _codCanal;
                private string _nomCanal;
                private string _codProducto;
                private string _nomProducto;                
                private int _cantidad;

                private DataTable _dtCanalesConteo;

                #endregion

                #region PROPIEDADES

                public int NLinea
                {
                    get { return _nLinea; }
                    set { _nLinea = value; }
                }

                public string CodCanal
                {
                  get { return _codCanal; }
                  set { _codCanal = value; }
                }

                public string NomCanal
                {
                  get { return _nomCanal; }
                  set { _nomCanal = value; }
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

                public int Cantidad
                {
                    get { return _cantidad; }
                    set { _cantidad = value; }
                }

                public DataTable Datos
                {
                    get { return _dtCanalesConteo; }
                    set { _dtCanalesConteo = value; }
                }

                #endregion

                #region METODOS PUBLICOS

                public cCanalConteo()
                {
                    GenerarDataTable();
                    InitProperties();
                }

                public void Add()
                {
                    if (_nLinea == 0)
                        AddLine();
                    else
                        UpdateLine();

                    InitProperties();
                }

                public void Delete()
                {
                    DelLine();
                    InitProperties();
                }

                public void Clear()
                {
                    DelAllLines();
                }

                public void Update()
                {
                    UpdateLine();
                }

                public void Select()
                {
                    SelLine();
                }

                #endregion

                #region METODOS PRIVADOS

                private void GenerarDataTable()
                {
                    _dtCanalesConteo = new DataTable("datos");
                    DataColumn newColumn;

                    // Se crean las columnas comunes a todos los tipos de conteo                                    

                    newColumn = _dtCanalesConteo.Columns.Add("nLinea");
                    newColumn.DataType = Type.GetType("System.Int16");
                    newColumn.AllowDBNull = false;
                    newColumn.AutoIncrement = true;
                    newColumn.AutoIncrementSeed = 1;
                    newColumn.AutoIncrementStep = 1;

                    newColumn = _dtCanalesConteo.Columns.Add("codCanal");
                    newColumn.DataType = System.Type.GetType("System.String");
                    newColumn.DefaultValue = "";
                    newColumn.AllowDBNull = false;

                    newColumn = _dtCanalesConteo.Columns.Add("Canal");
                    newColumn.DataType = System.Type.GetType("System.String");
                    newColumn.DefaultValue = "";
                    newColumn.AllowDBNull = false;

                    newColumn = _dtCanalesConteo.Columns.Add("codProducto");
                    newColumn.DataType = System.Type.GetType("System.String");
                    newColumn.DefaultValue = "";
                    newColumn.AllowDBNull = true;

                    newColumn = _dtCanalesConteo.Columns.Add("Producto");
                    newColumn.DataType = System.Type.GetType("System.String");
                    newColumn.DefaultValue = "";
                    newColumn.AllowDBNull = true;

                    newColumn = _dtCanalesConteo.Columns.Add("cantidad");
                    newColumn.DataType = System.Type.GetType("System.Int16");
                    newColumn.DefaultValue = 1;
                    newColumn.AllowDBNull = true;

                }

                private void AddLine()
                {
                    DataRow newRow = _dtCanalesConteo.NewRow();
                    newRow["codCanal"] = _codCanal;
                    newRow["Canal"] = _nomCanal;                    
                    newRow["codProducto"] = _codProducto;
                    newRow["Producto"] = _nomProducto;                    
                    newRow["cantidad"] = _cantidad;

                    _dtCanalesConteo.Rows.Add(newRow);

                }

                private void DelLine()
                {
                    DataRow[] filterRows = _dtCanalesConteo.Select("nLinea=" + _nLinea);
                    filterRows[0].Delete();
                }

                private void UpdateLine()
                {
                    DataRow[] filterRows = _dtCanalesConteo.Select("nLinea=" + _nLinea);
                    filterRows[0]["codCanal"] = _codCanal;
                    filterRows[0]["Canal"] = _nomCanal;                    
                    filterRows[0]["codProducto"] = _codProducto;
                    filterRows[0]["Producto"] = _nomProducto;                    
                    filterRows[0]["cantidad"] = _cantidad;
                    _dtCanalesConteo.AcceptChanges();
                }

                private void SelLine()
                {
                    DataRow[] filterRows = _dtCanalesConteo.Select("nLinea=" + _nLinea);
                    _nLinea = int.Parse(filterRows[0]["nLinea"].ToString());
                    _codCanal= filterRows[0]["codCanal"].ToString();
                    _nomCanal= filterRows[0]["Canal"].ToString();                    
                    _codProducto = filterRows[0]["codProducto"].ToString();
                    _nomProducto = filterRows[0]["Producto"].ToString();                    
                    _cantidad = int.Parse(filterRows[0]["cantidad"].ToString());
                }

                private void DelAllLines()
                {
                    // se eliminan todos los datos del DataTable
                    _dtCanalesConteo.Clear();
                }

                private void InitProperties()
                {
                    _nLinea = 0;
                    _codCanal = "";
                    _nomCanal = "";
                    _codProducto = "";
                    _nomProducto = "";                    
                    _cantidad = 0;
                }

                #endregion
            }

        #endregion

        #region variables privadas
            // datos de login
            private string _empresa;

            private DateTime _fecha;

            private eTipoConteo _tipoConteo;            
            private string _pageTitle;

            private eDatoTeclado _datoTeclado;

            private string _codOperario;
            private string _nomOperario;

            private string _codCliente;
            private string _nomCliente;
            private string _codAlmacen;

            private string _codFamilia;
            private string _familia;
            private string _codSubfamilia;
            private string _subfamilia;
            private string _codProducto;
            private string _producto;

            private DataTable _clientes;            

            // líneas de conteo en la recepción
            private cLineaConteo _lineaEnCurso;    
            // canales de conteo automático
            private cCanalConteo _canalesConteoAuto;
              
        #endregion

        #region propiedades
        
            public string Empresa
            {
                get { return _empresa; }
                set { _empresa = value; }
            }

            public DateTime Fecha
            {
                get { return _fecha; }
                set { _fecha = value; }
            }
        
            public eTipoConteo TipoConteo
            {
                get { return _tipoConteo; }
                set { _tipoConteo = value; }
            }

            public string PageTitle
            {
                get { return _pageTitle; }
                set { _pageTitle = value; }
            }

            public eDatoTeclado DatoTeclado
            {
                get { return _datoTeclado; }
                set { _datoTeclado = value; }
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

            public string CodAlmacen
            {
                get { return _codAlmacen; }
                set { _codAlmacen = value; }
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
            
            public DataTable Clientes
            {
                get { return _clientes; }
                set { _clientes = value; }
            }

            public cLineaConteo LineaEnCurso
            {
                get { return _lineaEnCurso; }
                set { _lineaEnCurso = value; }
            }
        
            public cCanalConteo CanalesConteoAuto
            {
                get { return _canalesConteoAuto; }
                set { _canalesConteoAuto = value; }
            }

        #endregion

        #region metodos publicos

            public cConteo(string empresa, int tipoConteo)
            {
                // Constructor por defecto de la clase
                this._empresa = empresa;

                switch (tipoConteo)
                {
                    case 0:
                        _tipoConteo = eTipoConteo.conteo;
                        _pageTitle="CONTEO ROPA";
                        break;
                    case 1:
                        _tipoConteo = eTipoConteo.oxido;
                        _pageTitle="OXIDO/GRASA";
                        break;
                    case 2:
                        _tipoConteo = eTipoConteo.desaprestado;
                        _pageTitle = "DESAPRESTADO ROPA";
                        break;
                }
                
                // se crea la instancia de objeto para ir almacenando los datos de líneas
                _lineaEnCurso = new cLineaConteo();

                _canalesConteoAuto = new cCanalConteo();
                
            }

            #region LINEAS CONTEO

                public void AddLinea()
                {
                    _lineaEnCurso.Add();
                    //if (_tipoConteo != eTipoConteo.oxido)
                    //{
                    //    Register();
                    //}
                }

                public void ClearLineas()
                {
                    _lineaEnCurso.Clear();
                }

                public void DelLinea()
                {
                    _lineaEnCurso.Delete();
                }

                public void SelectLinea()
                {
                    _lineaEnCurso.Select();
                }

                public bool TieneLineas()
                {
                    return (_lineaEnCurso.Datos.Rows.Count > 0);
                }
            
            #endregion

            #region CANALES CONTEO AUTOMATIZADO

                public void AddCanal()
                {                    
                    _canalesConteoAuto.Add();
                }

                public void ClearCanales()
                {
                    _canalesConteoAuto.Clear();
                }

                public void DelCanal()
                {
                    _canalesConteoAuto.Delete();
                }

                public void SelectCanal()
                {
                    _canalesConteoAuto.Select();
                }

                public bool TieneCanales()
                {
                    return (_canalesConteoAuto.Datos.Rows.Count > 0);
                }

            #endregion

            public void Register()
            {
                RegisterConteo();
                _lineaEnCurso.Clear();                
                _fecha = System.DateTime.Today;
                //InitOperario();                
            }

            public void AddLineFromCanal(int _nCanal)
            {
                // se captura la información del canal de conteo
                this._canalesConteoAuto.NLinea = _nCanal;
                this._canalesConteoAuto.Select();
                // se traspasan los datos a las líneas de conteo
                this._lineaEnCurso.NLinea = 0;
                this._lineaEnCurso.CodProducto = this._canalesConteoAuto.CodProducto;
                this._lineaEnCurso.NomProducto = this._canalesConteoAuto.NomProducto;
                this._lineaEnCurso.Cantidad = this._canalesConteoAuto.Cantidad;
                // se añade la línea
                this._lineaEnCurso.Add();
                // se inicializa la cantidad de conteo en el canal añadido
                this._canalesConteoAuto.Cantidad = 0;
                this._canalesConteoAuto.Update();
            }
        
        #endregion

        #region métodos privados

            private void RegisterConteo()
            {             
                foreach (DataRow linea in _lineaEnCurso.Datos.Rows)
                {
                    if (!bool.Parse(linea["procesada"].ToString()))
                    {
                        RegisterLine(linea);
                        linea["procesada"] = true;
                    }
                }
                // se eliminan los datos del objeto, porque ya están procesados
                _lineaEnCurso.Clear();
            }

            private void RegisterLine(DataRow linea)
            {                 
                cProduccion oProduccion = new cProduccion(_empresa);
                switch (_tipoConteo)
                {
                    case eTipoConteo.conteo:
                        oProduccion.RegistrarConteo(_codCliente,
                                                        linea["codProducto"].ToString(),
                                                        decimal.Parse(linea["cantidad"].ToString()),
                                                        linea["nSerie"].ToString(),
                                                        DateTime.Parse(linea["fecha"].ToString()),
                                                        DateTime.Parse(linea["Fecha/Hora"].ToString()),
                                                        _codOperario);
                        break;
                    case eTipoConteo.oxido:
                        //oProduccion.SalidaRechazoOxido(linea["codProducto"].ToString(),
                        //                                linea["nSerie"].ToString(),
                        //                                decimal.Parse(linea["cantidad"].ToString()),
                        //                                "", false);
                        break;
                    case eTipoConteo.desaprestado:
                        oProduccion.Desaprestar(linea["codProducto"].ToString(),
                                                decimal.Parse(linea["cantidad"].ToString()), "",
                                                DateTime.Parse(linea["fecha"].ToString()),
                                                DateTime.Parse(linea["Fecha/Hora"].ToString()));
                        break;
                }
            }

            private void InitProperties()
            {
                _fecha = System.DateTime.Today;
                InitOperario();
                InitCliente();                
            }            

            private void InitOperario()
            {
                _codOperario = "";
                _nomOperario = "";
            }

            private void InitCliente()
            {
                _codCliente = "";
                _nomCliente = "";
            }

        #endregion
        
    }
}
