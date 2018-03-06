using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using INIKER.CrossReferences;

namespace IntranetINDUSAL.Negocio
{
    public class cPlegado
    {
        #region Lineas de conteo

            private class cConteoPlegado
            {
                #region Variables privadas

                    private int _nLinea;
                    private string _nSerie;
                    private int _nPaquetes;
                    private int _nEtiqPaquetes;
                    private int _uds;
                    private int _udsTotal;    
                    // estructura de datos
                    private DataTable lineas;                    

                #endregion

                #region Propiedades

                    public int NLinea
                    {
                        get { return _nLinea; }
                        set { _nLinea = value; }
                    }

                    public string NSerie
                    {
                        get { return _nSerie; }
                        set { _nSerie = value; }
                    }
                    public int NPaquetes
                    {
                        get { return _nPaquetes; }
                        set { _nPaquetes = value; }
                    }
                    public int NEtiqPaquetes
                    {
                        get { return _nEtiqPaquetes; }
                        set { _nEtiqPaquetes = value; }
                    }
                    public int Uds
                    {
                        get { return _uds; }
                        set { _uds = value; }                        
                    }

                    public int UdsTotal
                    {
                        get { return _udsTotal; }
                        set { _udsTotal = value; }
                    }

                    public DataTable Datos
                    {
                        get { return lineas; }
                        set { lineas = value; }
                    }
                #endregion

                #region Constructores
                    public cConteoPlegado()
                    {
                        GenerarDataTable();
                    }
                #endregion

                #region Metodos Publicos

                    public void Add()
                    {
                        if (_nLinea == 0)
                        {
                            AddLine();
                        }
                        else
                        {
                            UpdateLine();
                        }
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

                    public void Get(int nLinea)
                    {
                        SelLine(nLinea);
                    }

                    public int CountLines()
                    {
                        if (lineas != null)
                        {
                            return (lineas.Rows.Count);
                        }
                        else
                        {
                            return (0);
                        }
                    }
                    
                #endregion

                #region Metodos Privados

                    private void GenerarDataTable()
                    {
                        lineas = new DataTable("datos");
                        DataColumn newColumn;

                        // Se crean las columnas comunes a todos los tipos de conteo                        
                        newColumn = lineas.Columns.Add("Linea");
                        newColumn.DataType = System.Type.GetType("System.Int16");
                        newColumn.AutoIncrement = true;
                        newColumn.AutoIncrementSeed = 1;
                        newColumn.AutoIncrementStep = 1;                                                

                        newColumn = lineas.Columns.Add("Num. Serie");
                        newColumn.DataType = System.Type.GetType("System.String");
                        newColumn.AllowDBNull = true;
                        
                        newColumn = lineas.Columns.Add("Paquetes");
                        newColumn.DataType = System.Type.GetType("System.Int16");
                        newColumn.DefaultValue = 0;
                        newColumn.AllowDBNull = false;

                        newColumn = lineas.Columns.Add("Etiquetas paq.");
                        newColumn.DataType = System.Type.GetType("System.Int16");
                        newColumn.DefaultValue = 0;
                        newColumn.AllowDBNull = false;

                        newColumn = lineas.Columns.Add("Uds.");
                        newColumn.DataType = System.Type.GetType("System.Int16");
                        newColumn.DefaultValue = 0;
                        newColumn.AllowDBNull = false;

                        newColumn = lineas.Columns.Add("Uds. totales");
                        newColumn.DataType = System.Type.GetType("System.Int16");
                        newColumn.DefaultValue = 0;
                        newColumn.AllowDBNull = false;
                    }

                    private void AddLine()
                    {
                        DataRow newRow = lineas.NewRow();
                        newRow["Num. Serie"] = _nSerie;
                        newRow["Paquetes"] = _nPaquetes;
                        newRow["Etiquetas paq."] = _nEtiqPaquetes;
                        newRow["Uds."] = _uds;
                        newRow["Uds. totales"] = _udsTotal;
                        lineas.Rows.Add(newRow);
                    }

                    private void DelLine()
                    {
                        DataRow[] filterRows = lineas.Select("Linea=" + _nLinea);
                        filterRows[0].Delete();
                    }

                    private void UpdateLine()
                    {
                        DataRow[] filterRows = lineas.Select("Linea=" + _nLinea);
                        filterRows[0]["Num. Serie"] = _nSerie;
                        filterRows[0]["Paquetes"] = _nPaquetes;
                        filterRows[0]["Etiquetas paq."] = _nEtiqPaquetes;
                        filterRows[0]["Uds."] = _uds;
                        filterRows[0]["Uds. totales"] = _udsTotal;
                        lineas.AcceptChanges();
                    }

                    private void SelLine(int nLinea)
                    {
                        DataRow[] filterRows = lineas.Select("Linea=" + nLinea);
                        _nLinea = nLinea;
                        _nSerie=filterRows[0]["Num. Serie"].ToString();
                        _nPaquetes=int.Parse(filterRows[0]["Paquetes"].ToString());
                        _nEtiqPaquetes=int.Parse(filterRows[0]["Etiquetas paq."].ToString());
                        _uds=int.Parse(filterRows[0]["Uds."].ToString());
                        _udsTotal=int.Parse(filterRows[0]["Uds. totales"].ToString());
                    }

                    private void DelAllLines()
                    {
                        // se eliminan todos los datos del DataTable
                        lineas.Clear();                        
                    }

                    private void InitPtoperties()
                    {
                        _nLinea = 0;
                        _nPaquetes = 0;
                        _nEtiqPaquetes = 0;
                        _uds = 0;
                        _nSerie = "";
                        _udsTotal = 0;
                    }

                #endregion
            }

        #endregion

        #region Variables privadas

            private Tipo_Planchado _tipoPlanchado;              // identifica la zona de planchado   
            private string _empresaLogin;

            private string _codCliente;            
            private string _nomCliente;
            private string _numPedido;
            private string _codMaquina;
            private string _descMaquina;
            private string _codOperario;
            private string _nomOperario;
            private string _codTurno;
            private string _desTurno;

            private string _codProducto;
            private string _descProducto;
            private int _udsPorPaquete;

            private string _familiaSel;
            private string _subfamiliaSel;
            
        // datos de conteo con los que se está trabajando
            private cConteoPlegado _lineas;
            private int _nLinea;            
            private int _paquetes;
            private int _etiquetas;
            private int _unidades;
            private int _unidadesTotal;
            private string _nSerie;

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

            public DataTable Lineas
            {
                // propiedad de sólo lectura
                get { return _lineas.Datos; }                
            }

            public int NLinea
            {
                get { return _nLinea; }
                set { _nLinea = value; }
            }

            public int Paquetes
            {
                get { return _paquetes; }
                set 
                { 
                    _paquetes = value;
                    // al asignar el número de paquetes, se establecen las etiquetas a la misma cantidad
                    _etiquetas = value;
                    CalcularUnidadesTotales();
                }
            }

            public int Etiquetas
            {
                get { return _etiquetas; }
                set { _etiquetas = value; }
            }
        
            public int Unidades
            {
                get { return _unidades; }
                set 
                {
                    SumarPaquetesCompletos(value);                    
                    CalcularUnidadesTotales();
                }
            }          

            public int UnidadesTotal
            {
                // propiedad de sólo lectura
                get { return _unidadesTotal; }             
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

            public cPlegado()
            {
                // Constructor por defecto
                _lineas = new cConteoPlegado();
            }

            public cPlegado(string empresaLogin, int tipo)
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

                _lineas = new cConteoPlegado();
                InitProperties();
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

            public void Add()
            {
                AddLine();
                InitProperties();
            }

            public void Delete()
            {
                DelLine();
                InitProperties();
            }

            public void Select()
            {
                SelectLine();
            }

            public void Clear()
            {
                Init();
            }

            public void Register()
            {
                RegisterLines();
                Init();
            }

            public bool DatosSinRegistrar()
            {
                return (_lineas.CountLines() != 0);
            }            

        #endregion

        #region Metodos Privados

            private void CalcularUnidadesTotales()
            {
                // calcula las unidades de la línea en función de
                // los valores de las propiedades
                _unidadesTotal = (_paquetes * _udsPorPaquete) + _unidades;
            }

            private void SumarPaquetesCompletos(int value)
            {
                // controla que en el número de unidades sueltas indicadas
                // en la línea, no hay las suficientes como para completar+
                // nuevos paquetes
                if (_udsPorPaquete > 0)
                {
                    this.Paquetes += value / _udsPorPaquete;
                    _unidades = value % _udsPorPaquete;
                }
                else
                    _unidades = value;
            }

            private void AddLine()
            {
                _lineas.NLinea = _nLinea;
                _lineas.NPaquetes = _paquetes;
                _lineas.NEtiqPaquetes = _etiquetas;
                _lineas.Uds = _unidades;
                _lineas.UdsTotal = _unidadesTotal;
                _lineas.NSerie = _nSerie;
                _lineas.Add();
            }
            private void DelLine()
            {
                _lineas.NLinea = _nLinea;
                _lineas.Delete();
            }

            private void SelectLine()
            {
                // primero se selecciona la línea
                _lineas.Get(_nLinea);
                // después se pasan los datos de la línea a las propiedades de esta clase
                // para que sean visibles desde fuera
                _paquetes = _lineas.NPaquetes;
                _etiquetas= _lineas.NEtiqPaquetes;
                _unidades=_lineas.Uds;
                _unidadesTotal=_lineas.UdsTotal;
                _nSerie=_lineas.NSerie;

            }            

            private void InitProperties()
            {
                _nLinea = 0;
                _paquetes = 0;
                _etiquetas = 0;
                _unidades = 0;
                _nSerie = "";
                _unidadesTotal = 0;
                _codMaquina = "";
                //_descMaquina = "";
                _codOperario = "";
                //_nomOperario = "";
                _codTurno = "";
                _codTurno = "";
            }

            private void Init()
            {
                _lineas.DeleteAll();
                InitProperties();
            }

            private void RegisterLines()
            {
                cProduccion oProduccion;
                int nLineasConteo = _lineas.CountLines();
                if (nLineasConteo > 0)
                {
                    oProduccion = new cProduccion(_empresaLogin);
                    //for (int iLinea = 1; iLinea <= nLineasConteo;iLinea++ )
                    foreach(DataRow fila in _lineas.Datos.Rows)
                    {
                        //_lineas.Get(iLinea);
                        //oProduccion.RegistrarPlanchado(
                        //    _codCliente,
                        //    _codProducto,
                        //    decimal.Parse(fila["Uds. totales"].ToString()),
                        //    "",
                        //    TipoPlanchadoToInt(_tipoPlanchado),
                        //    _codMaquina,
                        //    _codTurno,
                        //    _numPedido);
                    }
                }                
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
