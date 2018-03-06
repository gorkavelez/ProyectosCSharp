using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IntranetINDUSAL.Negocio
{
    public class cLavado
    {
        #region estructuras auxiliares de la clase

            public enum eTipoLavado
            {
                // tipo enumerado para diferenciar el tipo de lavado 
                // que se está procesando
                Lavadora,
                Tunel
            }

            public class cPesajeLavado
            {
                // estructura para almacenar los datos de las diferentes pesadas
                // de ropa asociadas a un mismo lavado, para el caso de Lavadora

                #region VARIABLES PRIVADAS

                    // por cada pesada, son necesarios los siguientes datos
                    private int _nLinea;               
                    private string _codOperario;
                    private string _nomOperario;
                    private decimal _peso;
                    private string _codCarro;
                    private string _desCarro;                    
                    private DateTime _fecha;                    
                    private DateTime _hora;
                    private string _codTurno;
                    private string _desTurno;
                    private decimal _tiempo;
                    //INI, JCA, DSC 2 - GESTION MULTIPEDIDO EN FORMULARIOS WEB
                    private string _codCliente;
                    private string _nomCliente;
                    private string _numPedido;
                    //INI, JCA, DSC 2 - GESTION MULTIPEDIDO EN FORMULARIOS WEB
                                    
                    private DataTable _dtPesajes;                    
                    
                #endregion

                #region PROPIEDADES

                    public int NLinea
                    {
                        get { return _nLinea; }
                        set { _nLinea = value; }
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

                    public decimal Peso
                    {
                        get { return _peso; }
                        set { _peso = value; }
                    }

                    public string CodCarro
                    {
                        get { return _codCarro; }
                        set { _codCarro = value; }
                    }

                    public string DesCarro
                    {
                        get { return _desCarro; }
                        set { _desCarro = value; }
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

                    public decimal Tiempo
                    {
                        get { return _tiempo; }
                        set { _tiempo = value; }
                    }
                
                    public DataTable Datos
                    {
                        get { return _dtPesajes; }
                        set { _dtPesajes = value; }
                    }

                #endregion

                #region METODOS PUBLICOS

                    public cPesajeLavado()
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
                        InitProperties();
                    }

                    public void Update()
                    {
                        UpdateLine();
                    }

                    public void Select()
                    {
                        SelLine();
                    }

                    public decimal PesoTotal()
                    {
                        decimal temp = 0;
                        foreach (DataRow drPesaje in _dtPesajes.Rows)
                        {
                            temp += decimal.Parse(drPesaje["peso"].ToString());
                        }
                        return (temp);
                    }

                #endregion

                #region METODOS PRIVADOS

                    private void GenerarDataTable()
                    {
                        _dtPesajes = new DataTable("datos");
                        DataColumn newColumn;

                        // Se crean las columnas comunes a todos los tipos de conteo                        
                        newColumn = _dtPesajes.Columns.Add("nLinea");
                        newColumn.DataType = System.Type.GetType("System.Int16");
                        newColumn.AutoIncrement = true;
                        newColumn.AutoIncrementSeed = 1;
                        newColumn.AutoIncrementStep = 1;                       

                        newColumn = _dtPesajes.Columns.Add("codOperario");
                        newColumn.DataType = System.Type.GetType("System.String");
                        newColumn.DefaultValue = "";
                        newColumn.AllowDBNull = true;

                        newColumn = _dtPesajes.Columns.Add("Operario");                        
                        newColumn.DataType = System.Type.GetType("System.String");
                        newColumn.DefaultValue = "";
                        newColumn.AllowDBNull = true;

                        newColumn = _dtPesajes.Columns.Add("Peso");
                        newColumn.DataType = System.Type.GetType("System.Decimal");
                        newColumn.DefaultValue = 0;
                        newColumn.AllowDBNull = false;

                        newColumn = _dtPesajes.Columns.Add("CodCarro");
                        newColumn.DataType = System.Type.GetType("System.String");
                        newColumn.DefaultValue = "";
                        newColumn.AllowDBNull = true;

                        newColumn = _dtPesajes.Columns.Add("Carro/Saca");                        
                        newColumn.DataType = System.Type.GetType("System.String");
                        newColumn.DefaultValue = "";
                        newColumn.AllowDBNull = true;

                        newColumn = _dtPesajes.Columns.Add("Fecha");
                        newColumn.DataType = System.Type.GetType("System.DateTime");
                        //newColumn.DefaultValue = System.DateTime.Today.ToShortDateString();                        

                        newColumn = _dtPesajes.Columns.Add("Fecha/Hora");                        
                        newColumn.DataType = System.Type.GetType("System.DateTime");
                        //newColumn.DefaultValue = System.DateTime.Now.ToShortTimeString();

                        newColumn = _dtPesajes.Columns.Add("CodTurno");
                        newColumn.DataType = System.Type.GetType("System.String");                        
                        newColumn.AllowDBNull = false;

                        newColumn = _dtPesajes.Columns.Add("Turno");
                        newColumn.DataType = System.Type.GetType("System.String");                        
                        newColumn.AllowDBNull = false;

                        //INI ***** JCA, 17/12/2009; DSC 2 - GESTION MULTIPEDIDO EN FORMULARIOS WEB
                        newColumn = _dtPesajes.Columns.Add("CodCliente");
                        newColumn.DataType = System.Type.GetType("System.String");
                        newColumn.AllowDBNull = true;

                        newColumn = _dtPesajes.Columns.Add("Cliente");
                        newColumn.DataType = System.Type.GetType("System.String");
                        newColumn.AllowDBNull = true;

                        newColumn = _dtPesajes.Columns.Add("Pedido");
                        newColumn.DataType = System.Type.GetType("System.String");
                        newColumn.AllowDBNull = true;
                        //FIN ***** JCA, 17/12/2009; DSC 2 - GESTION MULTIPEDIDO EN FORMULARIOS WEB
                    }

                    private void AddLine()
                    {
                        DataRow newRow = _dtPesajes.NewRow();
                        newRow["CodOperario"] = _codOperario;
                        newRow["Operario"] = _nomOperario;
                        newRow["Peso"] = _peso;
                        newRow["CodCarro"] = _codCarro;
                        newRow["Carro/Saca"] = _desCarro;
                        newRow["CodTurno"] = _codTurno;
                        newRow["Turno"] = _desTurno;
                        //INI ***** JCA, 17/12/2009; DSC 2 - GESTION MULTIPEDIDO EN FORMULARIOS WEB
                        newRow["CodCliente"] = _codCliente;
                        newRow["Cliente"] = _nomCliente;
                        newRow["Pedido"] = _numPedido;
                        //FIN ***** JCA, 17/12/2009; DSC 2 - GESTION MULTIPEDIDO EN FORMULARIOS WEB
                        newRow["fecha"] = DateTime.Today;
                        newRow["fecha/hora"] = DateTime.Now;

                        _dtPesajes.Rows.Add(newRow);

                    }

                    private void DelLine()
                    {
                        DataRow[] filterRows = _dtPesajes.Select("nLinea=" + _nLinea);
                        filterRows[0].Delete();
                    }

                    private void UpdateLine()
                    {
                        DataRow[] filterRows = _dtPesajes.Select("nLinea=" + _nLinea);
                        filterRows[0]["CodOperario"] = _codOperario;
                        filterRows[0]["Operario"] = _nomOperario;
                        filterRows[0]["Peso"] = _peso;
                        filterRows[0]["CodCarro"] = _codCarro;
                        filterRows[0]["Carro/Saca"] = _desCarro;
                        filterRows[0]["CodTurno"] = _codTurno;
                        filterRows[0]["Turno"] = _desTurno;
                        //INI ***** JCA, 17/12/2009; DSC 2 - GESTION MULTIPEDIDO EN FORMULARIOS WEB
                        filterRows[0]["CodCliente"] = _codCliente;
                        filterRows[0]["Cliente"] = _nomCliente;
                        filterRows[0]["Pedido"] = _numPedido;
                        //FIN ***** JCA, 17/12/2009; DSC 2 - GESTION MULTIPEDIDO EN FORMULARIOS WEB

                        _dtPesajes.AcceptChanges();
                    }

                    private void SelLine()
                    {
                        DataRow[] filterRows = _dtPesajes.Select("nLinea=" + _nLinea);
                        _nLinea = int.Parse(filterRows[0]["nLinea"].ToString());
                        _codOperario = filterRows[0]["CodOperario"].ToString();
                        _nomOperario = filterRows[0]["Operario"].ToString();
                        _peso=decimal.Parse(filterRows[0]["Peso"].ToString());
                        _codCarro= filterRows[0]["CodCarro"].ToString();
                        _desCarro = filterRows[0]["Carro/Saca"].ToString();
                        _fecha=DateTime.Parse(filterRows[0]["Fecha"].ToString());
                        _hora = DateTime.Parse(filterRows[0]["Fecha/Hora"].ToString());
                        _codTurno = filterRows[0]["CodTurno"].ToString();
                        _desTurno = filterRows[0]["Turno"].ToString();
                        //INI ***** JCA, 17/12/2009; DSC 2 - GESTION MULTIPEDIDO EN FORMULARIOS WEB
                        _codCliente = filterRows[0]["CodCliente"].ToString();
                        _nomCliente = filterRows[0]["Cliente"].ToString();
                        _numPedido = filterRows[0]["Pedido"].ToString();
                        //FIN ***** JCA, 17/12/2009; DSC 2 - GESTION MULTIPEDIDO EN FORMULARIOS WEB
                    }

                    private void DelAllLines()
                    {
                        // se eliminan todos los datos del DataTable
                        _dtPesajes.Clear();
                    }

                    private void InitProperties()
                    {
                        _nLinea = 0;
                        _codOperario = "";
                        _nomOperario = "";
                        _peso = 0;
                        _tiempo=0;
                        _codCarro = "";
                        _desCarro = "";
                        _fecha = System.DateTime.Today;
                        _hora = System.DateTime.Now;
                        _codTurno = "";
                        _desTurno = "";
                        //INI ***** JCA, 17/12/2009; DSC 2 - GESTION MULTIPEDIDO EN FORMULARIOS WEB
                        _codCliente = "";
                        _nomCliente = "";
                        _numPedido = "";
                        //FIN ***** JCA, 17/12/2009; DSC 2 - GESTION MULTIPEDIDO EN FORMULARIOS WEB
                    }

                #endregion
            }

        #endregion

        #region variables privadas
            // datos de login
            private string _empresa;

            // datos comunes a todos los tipos de lavado
            private eTipoLavado _tipo;
            private string _tipoMaquina;
            private string _codMaquina;
            private string _maquina;
            private string _webFormCaption;
            private decimal _pesoMaximoMaquina;
            
            // lavadora
            private string _codProgLavado;
            private string _ProgLavado;
            private Single _tiempoLavado;
            private string _nLavado;            
            private cPesajeLavado _pesajeEnCurso;
               
            // datos maestros
            DataTable _lavadoras;
            DataTable _programas;
            DataTable _programasMaquina;
            DataTable _carros;
            DataTable _turnos;                        
               
        #endregion

        #region propiedades
        
            public string Empresa
            {
                get { return _empresa; }
                set { _empresa = value; }
            }

            public eTipoLavado TipoLavado
            {
                get { return _tipo; }
                set 
                { 
                    _tipo = value;
                    switch (_tipo)
                    {
                        case eTipoLavado.Lavadora:
                            _tipoMaquina = "LAVAD";
                            _webFormCaption = "LAVADORAS";
                            break;
                        case eTipoLavado.Tunel:
                            _tipoMaquina = "TUNEL";
                            _webFormCaption = "TUNELES DE LAVADO";
                            break;
                    }
                }
            }
        
            public string WebFormCaption
            {
                get { return _webFormCaption; }
                set { _webFormCaption = value; }
            }

            public string CodMaquina
            {
                get { return _codMaquina; }
                set 
                { 
                    _codMaquina = value;
                    _pesoMaximoMaquina = GetPesoMaximo();
                }
            }

            public string Maquina
            {
                get { return _maquina; }
                set { _maquina = value; }
            }

            public string CodProgLavado
            {
                get { return _codProgLavado; }
                set 
                { 
                    _codProgLavado = value;
                    _tiempoLavado = TiempoPrograma(_codMaquina, _codProgLavado);
                }
            }

            public string ProgLavado
            {
                get { return _ProgLavado; }
                set { _ProgLavado = value; }
            }

            public decimal PesoMaximoMaquina
            {
                get { return _pesoMaximoMaquina; }
                set { _pesoMaximoMaquina = value; }
            }

            public DataTable Lavadoras
            {
                get { return _lavadoras; }                
            }

            public DataTable Programas
            {
                get { return _programas; }                
            }

            public DataTable ProgramasMaquina
            {
                get { return _programasMaquina; }
            }

            public DataTable Carros
            {
                get { return _carros; }             
            }
        
            public DataTable Turnos
            {
                get { return _turnos; }
                set { _turnos = value; }
            }
        
            public cPesajeLavado PesajeEnCurso
            {
                get { return _pesajeEnCurso; }
                set { _pesajeEnCurso = value; }
            }


        #endregion

        #region metodos publicos

            public cLavado(string empresa)
            {
                // Constructor por defecto de la clase
                this._empresa = empresa;                
            }

            public cLavado(string empresa,int tipo)
            {
                // Constructor en el que se indica el tipo de lavado
                this._empresa = empresa;
                // instanciación del objeto para las líneas de pesaje
                _pesajeEnCurso = new cPesajeLavado();

                switch (tipo)
                {
                    case 0:
                        this.TipoLavado = eTipoLavado.Lavadora;
                        // Obtención de datos maestros                        
                        GetProgramasLavado();
                        GetCarrosSacas();                        
                        InitLavProperties();
                        break;

                    case 1:
                        this.TipoLavado = eTipoLavado.Tunel;                       
                        
                        // inicialización de variables
                        InitLavProperties();
                        break;
                }

                GetLavadoras();
                GetTurnos();                
            }

            public DataTable FiltrarProgramasMaquina()
            {
                DataRow[] filtro=_programas.Select("maquina='" + _codMaquina + "'");
                DataRow newRow;
                _programasMaquina = _programas.Clone();
                _programasMaquina.Clear();
                foreach (DataRow fila in filtro)
                {
                    newRow = _programasMaquina.NewRow();                    
                    newRow[0] = fila[0];
                    newRow[1] = fila[1];
                    newRow[2] = fila[2];
                    _programasMaquina.Rows.Add(newRow);
                }
                return (_programasMaquina);                
            }

 

            public void AddPesaje()
            {
                // Si el peso máximo es cero, lo tomo como no especificado
                if (_pesoMaximoMaquina == 0)
                    _pesajeEnCurso.Add();
                else
                {
                    if (_pesajeEnCurso.PesoTotal() + _pesajeEnCurso.Peso <= _pesoMaximoMaquina)
                        _pesajeEnCurso.Add();
                    else
                        throw new Exception("EL PESO TOTAL EXCEDE DEL PESO TOTAL ADMITIDO");
                }
                //InitLavProperties();
            }

            public void ClearPesajes()
            {
                _pesajeEnCurso.Clear();
            }

            public void DelPesaje()
            {
                _pesajeEnCurso.Delete();
            }

            public void SelectPesaje()
            {
                _pesajeEnCurso.Select();
            }



            public void Register()
            {
                // Registro de los datos de lavado en Dynamics
                // Se realiza un tipo de registro, u otro, en función del
                // tipo de lavado
                switch (_tipo)
                {
                    case eTipoLavado.Lavadora:
                        RegisterLavadora();
                        break;
                    case eTipoLavado.Tunel:
                        if (_pesoMaximoMaquina == 0)
                            RegisterTunel();
                        else
                        {
                            if ((_pesoMaximoMaquina != 0) && (_pesoMaximoMaquina >= _pesajeEnCurso.Peso))
                                RegisterTunel();
                            else
                                throw new Exception("EL PESO TOTAL EXCEDE DEL PESO TOTAL ADMITIDO");
                        }
                        break;
                }

                //InitTunelProperties();                
                InitLavProperties();
            }

            public void SetOrder(string orderNo)
            {
                _pesajeEnCurso.NumPedido= orderNo;
            }
        
        #endregion

        #region métodos privados

            private void RegisterLavadora()
            {
                // Método para realizar el registro de datos en Dynamics
                // para el caso de los lavados de tipo lavadora.
                // Por cada pesada de ropa asociada al lavado, se registra
                // una línea

                // Lo primero que hay que hacer es solicitar un número de serie para identificar todas
                // las pesadas del lavado
                _nLavado = GetNumLavado();
                // Este número de lavado se utiliza para el registro de todas las pesadas
                foreach (DataRow regPeso in _pesajeEnCurso.Datos.Rows)
                {
                    RegisterPesaje(regPeso);
                }
                // se eliminan los datos del objeto, porque ya están procesados
                _pesajeEnCurso.Clear();
            }

            private void RegisterTunel()
            {
                // registro de lavado en túnel
                //INI ***** JCA, 17/12/2009; DSC 2 - GESTION MULTIPEDIDO EN FORMULARIOS WEB
                // Se añaden los parámetros _codCliente y _numPedido en la llamada a RegistrarTunel
                
                cProduccion oProduccion = new cProduccion(_empresa);
                oProduccion.RegistrarTunel(_pesajeEnCurso.CodOperario,
                                             _codMaquina,
                                             _pesajeEnCurso.Tiempo,
                                             _pesajeEnCurso.Peso,
                                             System.DateTime.Today,
                                             System.DateTime.Now,
                                             _pesajeEnCurso.CodTurno,
                                             _pesajeEnCurso.CodCliente,
                                             _pesajeEnCurso.NumPedido);
                
                _pesajeEnCurso.Clear();
            }
            
            private void RegisterPesaje(DataRow pesajeRegistro)
            {
                // registro de lavado en lavadora
                //INI ***** JCA, 17/12/2009; DSC 2 - GESTION MULTIPEDIDO EN FORMULARIOS WEB
                // Se añaden los parámetros pesajeRegistro["CodCliente"] y pesajeRegistro["Pedido"]
                // en la llamada a RegistrarTunel
                
                cProduccion oProduccion = new cProduccion(_empresa);
                oProduccion.RegistrarLavado(pesajeRegistro["CodOperario"].ToString(),
                                             _codMaquina,
                                             _codProgLavado,
                                             decimal.Parse(pesajeRegistro["Peso"].ToString()),
                                             _nLavado,
                                             DateTime.Parse(pesajeRegistro["Fecha"].ToString()),
                                             DateTime.Parse(pesajeRegistro["Fecha/Hora"].ToString()),
                                             pesajeRegistro["CodCarro"].ToString(),
                                             pesajeRegistro["CodTurno"].ToString(),
                                             pesajeRegistro["CodCliente"].ToString(),
                                             pesajeRegistro["Pedido"].ToString(),
                                             decimal.Parse(_tiempoLavado.ToString()));
                
            }

            private string GetNumLavado()
            {
                try
                {
                    cProduccion oProduccion = new cProduccion(_empresa);
                    return (oProduccion.GetWashNumber());
                }
                catch 
                {
                    return ("");
                }
            }

            private void InitTunelProperties()
            {
                _codMaquina = "";
                _maquina = "";
            }

            private void InitLavProperties()
            {
                _codMaquina = "";
                _maquina = "";
                _codProgLavado = "";
                _ProgLavado = "";
                _pesoMaximoMaquina = 0;
                _nLavado = "";
            }
                    

        #endregion

        #region DATOS AUXILIARES

            private void GetLavadoras()
            {
                cProduccion oProduccion = new cProduccion(this._empresa);
                _lavadoras = oProduccion.GetWorkCenters(_tipoMaquina);
            }

            private void GetProgramasLavado()
            {
                cProduccion oProduccion = new cProduccion(this._empresa);
                _programas = oProduccion.GetProgramasLavado();
            }

            private decimal GetPesoMaximo()
            {
                DataRow[] fila = _lavadoras.Select("codigo='" + _codMaquina + "'");
                if (fila.Count() != 0)
                    return (decimal.Parse(fila[0]["pesoMaximo"].ToString()));
                else
                    return (0);
            }

            private Single TiempoPrograma(string maquina, string programa)
            {
                Single tiempo=0;
                DataRow[] prog = _programas.Select("maquina='" + maquina + "' And programa='" + programa + "'");
                try
                {
                    tiempo = Single.Parse(prog[0]["tiempo"].ToString());
                    return (tiempo);
                }
                catch 
                {
                    return (tiempo);
                }
            }

            private void GetCarrosSacas()
            {
                cProduccion oProduccion = new cProduccion(this._empresa);
                _carros = oProduccion.GetCarrosSacas();
            }
            
            public Single PesoCarro(string codigo)
            {
                DataRow[] filtro = _carros.Select("codigo='" + codigo + "'");
                try
                {
                    Single peso = Single.Parse(filtro[0]["peso"].ToString());
                    return (peso);
                }
                catch 
                {
                    return (0);
                }
            }
            
            private void GetTurnos()
            {
                cProduccion oProduccion = new cProduccion(this._empresa);
                _turnos = oProduccion.GetTurnos();
            }
            
        #endregion
    }
}
