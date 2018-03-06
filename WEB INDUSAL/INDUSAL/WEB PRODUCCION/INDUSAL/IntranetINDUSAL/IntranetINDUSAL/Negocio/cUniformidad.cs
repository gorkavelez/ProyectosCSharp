using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IntranetINDUSAL.Negocio
{
    public class cUniformidad
    {
        public enum eTipoLectura
        {
            entrada,
            salida
        }

        private string _empresa="";
        private eTipoLectura _tipoLectura;
        private string _pageTitle="";
        private string _datoTeclado = "";

        private string _codTurno = "";
        private string _descTurno = "";

        private string _codEmpleado = "";
        private string _nomEmpleado = "";

        private string _codCliente = "";
        private string _nomCliente = "";

        private string _numPedido = "";

        private DataTable _numerosSerieCliente;
        private DataTable _turnos;

        private cLineasUniformidad _lineas;

        public string Empresa
        {
            get { return _empresa; }
            set { _empresa = value; }
        }

        public eTipoLectura TipoLectura
        {
            get { return _tipoLectura; }
            set { _tipoLectura = value; }
        }

        public string PageTitle
        {
            get { return _pageTitle; }
            set { _pageTitle = value; }
        }
        
        public string DatoTeclado
        {
            get { return _datoTeclado; }
            set { _datoTeclado = value; }
        }

        public string CodTurno
        {
            get { return _codTurno; }
            set { _codTurno = value; }
        }

        public string DescTurno
        {
            get { return _descTurno; }
            set { _descTurno = value; }
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
            set { _numPedido = value; }
        }

        public DataTable NumerosSerieCliente
        {
            get { return _numerosSerieCliente; }
            set 
            { 
                _numerosSerieCliente = value;
                if (_tipoLectura == eTipoLectura.salida)
                {
                    RellenarTablaProductos();
                    RellenarTablaLecturas();
                }
            }
        }

        public DataTable Turnos
        {
            get { return _turnos; }
            set { _turnos = value; }
        }

        public cLineasUniformidad Lineas
        {
            get { return _lineas; }
            set { _lineas = value; }
        }

        public cUniformidad(string empresa, int tipoLectura)
        {
                // Constructor por defecto de la clase
                this._empresa = empresa;

                switch (tipoLectura)
                {
                    case 0:
                        _tipoLectura = eTipoLectura.entrada;
                        _pageTitle="UNIFORMIDAD: RECUENTO DE ENTRADA";
                        break;
                    case 1:
                        _tipoLectura = eTipoLectura.salida;
                        _pageTitle = "UNIFORMIDAD: RECUENTO DE SALIDA";
                        break;
                }

                _lineas = new cLineasUniformidad(_tipoLectura);
        }

        public string SearchSerialNumber(string nSerie)
        {
            try
            {
                DataRow[] filtro = _numerosSerieCliente.Select("Serial_No='" + nSerie + "'");
                if (filtro.Count() == 1)
                {
                    if (_lineas.Select(nSerie) != null)
                        return ("Número de serie duplicado en la lectura");
                    else
                    {
                        _lineas.Add(filtro[0]["Serial_No"].ToString(),
                                    filtro[0]["Item_No"].ToString(),
                                    filtro[0]["Descripcion_Planta"].ToString());
                        return ("");
                    }
                }
                else
                {
                    if (filtro.Count() > 1)
                        return ("Existe más de un número de serie con ese código");
                    else
                        return ("No Existe un número de serie con ese código");
                }

            }
            catch(Exception ex)
            {
                return (ex.Message);
            }
        }

        public string AddSerialNumber(string nSerie_, string itemNo_, string descrPlanta_)
        {
            try
            {
                if (_lineas.Select(nSerie_) != null)
                    return ("Número de serie duplicado en la lectura");
                else
                {
                    _lineas.Add(nSerie_, itemNo_, descrPlanta_);
                    return ("");
                }              
            }
            catch (Exception ex)
            {
                return (ex.Message);
            }
        }

        public void ClearData()
        {
            _codCliente = "";
            _nomCliente = "";
            _numPedido = "";                        
            _lineas.ClearProcesadas();
            if (_tipoLectura == eTipoLectura.salida)
            {
                _lineas.ResumenProductos.Clear();
                _numerosSerieCliente.Clear();
            }
        }

        public void ClearLineas()
        {
            _lineas.Lecturas.Clear();            
            _lineas.ResumenProductos.Clear();
        }

        public bool AllowRegister()
        {
            try
            {
                DataRow[] filtro = _lineas.Lecturas.Select("Procesada=false and Error=false");
                return (filtro.Count() > 0);
            }
            catch (Exception)
            {
                return (false);
            }
        }

        private void RellenarTablaProductos()
        {
            foreach (DataRow fila in _numerosSerieCliente.Rows)
            {
                _lineas.AddItem(fila["Item_No"].ToString(),
                                fila["Descripcion_Planta"].ToString(),1,1);
            }
        }

        private void RellenarTablaLecturas()
        {
            foreach (DataRow fila in _numerosSerieCliente.Rows)
            {
                if (int.Parse(fila["Enviado"].ToString()) == 1)
                {
                    _lineas.Add(fila["Serial_No"].ToString(),
                                fila["Item_No"].ToString(),
                                fila["Descripcion_Planta"].ToString(),
                                true);
                }
            }
        }

        #region LINEAS

            public class cLineasUniformidad
            {
                private string _nSerie;
                private string _codProducto;
                private string _descProducto;

                private DataTable _lineas;
                private DataTable _lineasProducto;

                private cUniformidad.eTipoLectura _tipoLectura;
               
                public string NSerie
                {
                    get { return _nSerie; }
                    set { _nSerie = value; }
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
                
                public DataTable Lecturas
                {
                    get { return _lineas; }
                    set { _lineas = value; }
                }

                public DataTable ResumenProductos
                {
                    get { return _lineasProducto; }
                    set { _lineasProducto = value; }
                }

                public cLineasUniformidad(cUniformidad.eTipoLectura tipoLectura)
                {
                    _tipoLectura = tipoLectura;
                    CreateDataTable();
                    CreateItemDataTable();
                }
                
                private void CreateDataTable()
                {
                    _lineas = new DataTable("lineas");
                    DataColumn newColumn;             

                    newColumn = _lineas.Columns.Add("Num. Serie");
                    newColumn.DataType = System.Type.GetType("System.String");
                    newColumn.DefaultValue = "";
                    newColumn.AllowDBNull = false;

                    newColumn = _lineas.Columns.Add("Cod. Producto");
                    newColumn.DataType = System.Type.GetType("System.String");
                    newColumn.DefaultValue = "";
                    newColumn.AllowDBNull = false;

                    newColumn = _lineas.Columns.Add("Producto");
                    newColumn.DataType = System.Type.GetType("System.String");
                    newColumn.DefaultValue = "";
                    newColumn.AllowDBNull = false;

                    newColumn = _lineas.Columns.Add("Procesada");
                    newColumn.DataType = System.Type.GetType("System.Boolean");
                    newColumn.DefaultValue = false;

                    newColumn = _lineas.Columns.Add("Error");
                    newColumn.DataType = System.Type.GetType("System.Boolean");
                    newColumn.DefaultValue = false;
                }

                private void CreateItemDataTable()
                {
                    _lineasProducto = new DataTable("productos");
                    DataColumn newColumn;

                    newColumn = _lineasProducto.Columns.Add("Cod. Producto");
                    newColumn.DataType = System.Type.GetType("System.String");
                    newColumn.DefaultValue = "";
                    newColumn.AllowDBNull = false;

                    newColumn = _lineasProducto.Columns.Add("Producto");
                    newColumn.DataType = System.Type.GetType("System.String");
                    newColumn.DefaultValue = "";
                    newColumn.AllowDBNull = false;

                    newColumn = _lineasProducto.Columns.Add("Cantidad");
                    newColumn.DataType = System.Type.GetType("System.Int16");
                    newColumn.DefaultValue = 1;
                    newColumn.AllowDBNull = false;

                    if (_tipoLectura == cUniformidad.eTipoLectura.salida)
                    {
                        newColumn = _lineasProducto.Columns.Add("Pendiente");
                        newColumn.DataType = System.Type.GetType("System.Int16");
                        newColumn.AllowDBNull = false;
                    }

                }

                public void Add(string nSerie, string codProducto, string descProducto)
                {
                    DataRow newRow = _lineas.NewRow();
                    newRow["Num. Serie"] = nSerie;
                    newRow["Cod. Producto"] = codProducto;
                    newRow["Producto"] = descProducto;
                    _lineas.Rows.Add(newRow);
                    // se actualiza también la tabla de resumen de productos                    
                    if(_tipoLectura== eTipoLectura.entrada)
                        AddItem(codProducto, descProducto);
                    else
                        AddItem(codProducto, descProducto,0,-1);
                }

                public void Add(string nSerie, string codProducto, string descProducto, bool procesada)
                {
                    DataRow newRow = _lineas.NewRow();
                    newRow["Num. Serie"] = nSerie;
                    newRow["Cod. Producto"] = codProducto;
                    newRow["Producto"] = descProducto;
                    newRow["Procesada"] = procesada;
                    _lineas.Rows.Add(newRow);
                    // se actualiza también la tabla de resumen de productos                    
                    if (_tipoLectura == eTipoLectura.entrada)
                        AddItem(codProducto, descProducto);
                    else
                        AddItem(codProducto, descProducto, 0, -1);
                }

                public void Delete(string nSerie)
                {
                    try
                    {
                        DataRow linea = Select(nSerie);
                        string codProducto = linea["Cod. Producto"].ToString();
                        linea.Delete();
                        // se actualiza también la tabla de resumen de productos
                        DeleteItem(codProducto,false);
                    }
                    catch
                    { }
                }

                private void Delete(string nSerie,bool isClear)
                {                
                    try
                    {
                        DataRow linea = Select(nSerie);
                        string codProducto = linea["Cod. Producto"].ToString();
                        linea.Delete();                        
                        // se actualiza también la tabla de resumen de productos
                        DeleteItem(codProducto,isClear);
                    }
                    catch
                    {}
                }

                public DataRow Select(string nSerie)
                {
                    DataRow[] filtro;
                    try
                    {
                        filtro = _lineas.Select("[Num. Serie]='" + nSerie + "'");
                        _nSerie = filtro[0]["Num. Serie"].ToString();
                        _codProducto = filtro[0]["Cod. Producto"].ToString();
                        _descProducto = filtro[0]["Producto"].ToString();
                        return (filtro[0]);
                    }
                    catch(Exception)
                    {
                        return (null);
                    }
                }

                private void AddItem(string itemNo, string itemDesc)
                {
                    try
                    {
                        DataRow[] filtro = _lineasProducto.Select("[Cod. Producto]='" + itemNo + "'");
                        filtro[0]["Cantidad"] = Int16.Parse(filtro[0]["Cantidad"].ToString()) + 1;
                    }
                    catch (IndexOutOfRangeException)
                    {
                        DataRow newRow = _lineasProducto.NewRow();
                        newRow["Cod. Producto"] = itemNo;
                        newRow["Producto"] = itemDesc;
                        _lineasProducto.Rows.Add(newRow);
                    }

                }

                public void AddItem(string itemNo, string itemDesc, decimal qty, decimal remnQty)
                {
                    try
                    {
                        DataRow[] filtro = _lineasProducto.Select("[Cod. Producto]='" + itemNo + "'");
                        filtro[0]["Cantidad"] = Int16.Parse(filtro[0]["Cantidad"].ToString()) + qty;
                        filtro[0]["Pendiente"] = Int16.Parse(filtro[0]["Pendiente"].ToString()) + remnQty;
                    }
                    catch (IndexOutOfRangeException)
                    {
                        DataRow newRow = _lineasProducto.NewRow();
                        newRow["Cod. Producto"] = itemNo;
                        newRow["Producto"] = itemDesc;
                        newRow["Pendiente"] = remnQty;
                        _lineasProducto.Rows.Add(newRow);
                    }
                }

                private void DeleteItem(string itemNo,bool isClear)
                {
                    try
                    {
                        DataRow[] filtro = _lineasProducto.Select("[Cod. Producto]='" + itemNo + "'");
                        Int16 tempQty = Int16.Parse(filtro[0]["Cantidad"].ToString());
                        if ((tempQty - 1) > 0)
                        {
                            if ((_tipoLectura== eTipoLectura.entrada)|
                                ((_tipoLectura== eTipoLectura.salida) & isClear))                             
                                    filtro[0]["Cantidad"] = tempQty - 1;
                            filtro[0]["Pendiente"] = Int16.Parse(filtro[0]["Pendiente"].ToString()) + 1;
                        }
                        else
                            filtro[0].Delete();
                    }
                    catch
                    {}

                }

                public void ClearProcesadas()
                {
                    try
                    {
                        foreach (DataRow linea in _lineas.Rows)
                        {
                            if (linea.RowState != DataRowState.Deleted)
                            {
                                if (bool.Parse(linea["Procesada"].ToString()))
                                {
                                    Delete(linea["Num. Serie"].ToString(),true);
                                }
                            }
                        }                                                
                    }
                    catch
                    { }

                }
                
            }
        
        #endregion

    }
}
