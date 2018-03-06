using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using INIKER.CrossReferences;

namespace IntranetINDUSAL.Negocio
{
    public class cMaestros
    {
        private string _empresaLogin;
        private cClientes oClientes;
        private cPedidosVenta oPedidosVenta;
        private cSurtidoCliente oSurtidoCliente;
        private cTurnos oTurnos;
        private cEmpleados oEmpleados;

        public string EmpresaLogin
        {
            get { return _empresaLogin; }
            set { _empresaLogin = value; }
        }

        public cClientes Clientes
        {
            get { return oClientes; }
            set { oClientes = value; }
        }        

        public cSurtidoCliente SurtidoCliente
        {
            get { return oSurtidoCliente; }
            set { oSurtidoCliente = value; }
        }
        
        public cTurnos Turnos
        {
            get { return oTurnos; }
            set { oTurnos = value; }
        }
        
        public cEmpleados Empleados
        {
            get { return oEmpleados; }
            set { oEmpleados = value; }
        }
        
        public cPedidosVenta PedidosVenta
        {
            get { return oPedidosVenta; }
            set { oPedidosVenta = value; }
        }

        public cMaestros()
        { }

        public cMaestros(string pEmpresaLogin)
        {
            this._empresaLogin = pEmpresaLogin;            
        }

        public void GetClientes()
        {
            oClientes = new cClientes(this._empresaLogin);
        }

        public void GetPedidosVenta(string codCliente)
        {
            oPedidosVenta = new cPedidosVenta(this._empresaLogin, codCliente);
        }

        public void GetSurtidoCliente(string codCliente)
        {
            oSurtidoCliente = new cSurtidoCliente(codCliente, this._empresaLogin);
        }

        public void GetSurtidoCliente(string codCliente, Tipo_Planchado tipoPlanchado)
        {
            oSurtidoCliente = new cSurtidoCliente(codCliente, this._empresaLogin, tipoPlanchado);
        }

        public void GetTurnos()
        {
            oTurnos = new cTurnos(this._empresaLogin);
        }

        public void GetEmpleados()
        {
            oEmpleados = new cEmpleados(this._empresaLogin);
        }

        public class cClientes
        {
            #region VARIABLES PRIVADAS

            private string _empresaLogin;

            private DataTable _dtClientes;
            private string _codigo;
            private string _nombre;
            private string _nombre2;
            private string _alias;
            private string _almacen;

            #endregion

            #region PROPIEDADES

            public DataTable tablaClientes
            {
                get { return _dtClientes; }
                set { _dtClientes = value; }
            }

            public string Codigo
            {
                get { return _codigo; }
                set { _codigo = value; }
            }

            public string Nombre
            {
                get { return _nombre; }
                set { _nombre = value; }
            }

            public string Nombre2
            {
                get { return _nombre2; }
                set { _nombre2 = value; }
            }

            public string Alias
            {
                get { return _alias; }
                set { _alias = value; }
            }

            public string Almacen
            {
                get { return _almacen; }
                set { _almacen = value; }
            }

            #endregion

            #region METODOS PUBLICOS

            ///<sumary>Constructor por defecto de la clase
            ///<param name="empresaLogin">Código de empresa contra la que trabaja el usuario</param>
            ///</sumary>
            public cClientes(string empresaLogin)
            {
                this._empresaLogin = empresaLogin;
                // Al instanciar la clase, se obtiene un listado completo de los clientes de la empresa
                GetCustomers();
            }

            public bool Get(string codigo)
            {
                ///<summary>Método que busca un cliente en el DataTable de clientes</summary>
                ///<param name="codigo" type="string">Código de cliente a buscar</param>

                return (GetCustomer(codigo));
            }

            public void LoadDropDownList(ref System.Web.UI.WebControls.DropDownList oDDL)
            {
                oDDL.Items.Clear();
                oDDL.DataSource = _dtClientes;
                oDDL.DataValueField = _dtClientes.Columns["codigo"].ToString();
                oDDL.DataTextField = _dtClientes.Columns["alias"].ToString();
                oDDL.DataBind();
                // se añade un item vacío para cuando se quiera deseleccionar
                oDDL.Items.Add("");
                oDDL.SelectedIndex = oDDL.Items.Count - 1;
            }

            public void SelectDropDownList(ref System.Web.UI.WebControls.DropDownList oDDL)
            {
                oDDL.SelectedIndex = oDDL.Items.Count - 1;
                oDDL.SelectedIndex = oDDL.Items.IndexOf(oDDL.Items.FindByValue(_codigo));
            }

            #endregion

            #region METODOS PRIVADOS

            private void GetCustomers()
            {
                // Método que obtiene un objeto DataTable de clientes, con los siguientes datos:
                // codigo, nombre, nombre2, alias, almacen
                cProduccion oProduccion = new cProduccion(_empresaLogin);
                _dtClientes = oProduccion.GetCustomers();
            }

            private bool GetCustomer(string codigo)
            {
                try
                {
                    DataRow[] cliente = _dtClientes.Select("codigo='" + codigo + "'");
                    if (cliente.Count() != 0)
                    {
                        this._codigo = cliente[0]["codigo"].ToString();
                        this._nombre = cliente[0]["nombre"].ToString();
                        this._nombre2 = cliente[0]["nombre2"].ToString();
                        this._alias = cliente[0]["alias"].ToString();
                        this._almacen = cliente[0]["almacen"].ToString();
                        return (true);
                    }
                    else
                    {
                        InitProperties();
                        return (false);
                    }
                }
                catch
                {
                    return (false);
                }
            }

            private void InitProperties()
            {
                this._codigo = "";
                this._nombre = "";
                this._nombre2 = "";
                this._alias = "";
                this._almacen = "";
            }


            #endregion
        }

        public class cSurtidoCliente
        {
            private DataSet _dsSurtidoCLiente;  // un DataTable por cada familia de productos y un DataColumn por cada subfamilia
            private string _famSel;             // familia seleccionada
            private string _subfamSel;          // subfamilia seleccionada

            public string famSel
            {
                get { return _famSel; }
                set { _famSel = value; }
            }

            public string subfamSel
            {
                get { return _subfamSel; }
                set { _subfamSel = value; }
            }

            public cSurtidoCliente()
            {
                // Constructor de la clase
            }

            public cSurtidoCliente(string cliente, string empresa, INIKER.CrossReferences.Tipo_Planchado tipo)
            {
                cProductos surtido = new cProductos();
                // se obtiene el dataset clasificado en datatables
                _dsSurtidoCLiente = surtido.ObtenerSurtidoCliente(cliente, empresa, tipo);
            }

            public cSurtidoCliente(string cliente, string empresa)
            {
                cProductos surtido = new cProductos();
                // se obtiene el dataset clasificado en datatables
                _dsSurtidoCLiente = surtido.ObtenerSurtidoCliente(cliente, empresa);
            }

            public string[] ArrayFamilias()
            {
                // se declara e instancia un array de objetos String, con tantos elementos como
                // DataTables existan en el DataSet de surtido
                string[] familias = new string[_dsSurtidoCLiente.Tables.Count];
                int iFamilia = 0;
                foreach (DataTable famProductos in _dsSurtidoCLiente.Tables)
                {
                    string newFam = famProductos.TableName;
                    familias[iFamilia] = newFam;
                    iFamilia++;
                }
                return (familias);
            }

            public string[] ArrayFamiliasInclCodigo()
            {
                // se declara e instancia un array de objetos String, con tantos elementos como
                // DataTables existan en el DataSet de surtido
                string[] familias = new string[_dsSurtidoCLiente.Tables.Count];
                int iFamilia = 0;
                foreach (DataTable famProductos in _dsSurtidoCLiente.Tables)
                {
                    string newFam = famProductos.TableName + ";" + famProductos.ExtendedProperties["Code"].ToString();
                    familias[iFamilia] = newFam;
                    iFamilia++;
                }
                return (familias);
            }

            public string[] ArraySubfamilias()
            {
                // se declara e instancia un array de objetos String, con tantos elementos como
                // DataColumns existan en el DataTable que tiene los datos de la familia especificada
                DataTable dtFamilia = _dsSurtidoCLiente.Tables[_famSel];
                if (dtFamilia != null)
                {
                    string[] subfamilias = new string[dtFamilia.Columns.Count];
                    int iSubfamilia = 0;
                    foreach (DataColumn subfamProductos in dtFamilia.Columns)
                    {
                        string newSubfam = subfamProductos.ColumnName;
                        subfamilias[iSubfamilia] = newSubfam;
                        iSubfamilia++;
                    }
                    return (subfamilias);
                }
                return (null);
            }

            public string[] ArraySubfamiliasInclCodigo()
            {
                // se declara e instancia un array de objetos String, con tantos elementos como
                // DataColumns existan en el DataTable que tiene los datos de la familia especificada
                DataTable dtFamilia = _dsSurtidoCLiente.Tables[_famSel];
                if (dtFamilia != null)
                {
                    string[] subfamilias = new string[dtFamilia.Columns.Count];
                    int iSubfamilia = 0;
                    foreach (DataColumn subfamProductos in dtFamilia.Columns)
                    {
                        string newSubfam = subfamProductos.ColumnName + ";" + subfamProductos.ExtendedProperties["Code"].ToString();
                        subfamilias[iSubfamilia] = newSubfam;
                        iSubfamilia++;
                    }
                    return (subfamilias);
                }
                return (null);
            }

            public string ArrayProductos(string separador)
            {
                // se declara e instancia un array de objetos String, con tantos elementos como
                // DataColumns existan en el DataTable que tiene los datos de la familia especificada
                DataTable dtFamilia = _dsSurtidoCLiente.Tables[_famSel];
                string productos = "";
                if (dtFamilia != null)
                {
                    foreach (DataRow filaProductos in dtFamilia.Rows)
                    {
                        if (filaProductos[_subfamSel].ToString() != "")
                        {
                            if (productos.Length > 0)
                                productos += separador;
                            productos += filaProductos[_subfamSel].ToString();
                        }
                        else
                        {
                            return (productos);
                        }
                    }
                    return (productos);
                }
                return (null);
            }
        }

        public class cTurnos
        {
            #region VARIABLES
                        
            private DataTable _datosOrigen;

            #endregion

            #region PROPIEDADES

            public DataTable DatosOrigen
            {
                get { return _datosOrigen; }
                set { _datosOrigen = value; }
            }

            #endregion

            public cTurnos(string _empresaLogin)
            {
                cProduccion oProduccion = new cProduccion(_empresaLogin);
                _datosOrigen = oProduccion.GetTurnos();
            }
        }

        public class cEmpleados
        {
            #region VARIABLES PRIVADAS

            private string _empresaLogin;

            private DataTable _dtEmpleados;
            private string _codigo;
            private string _nombre;


            #endregion

            #region PROPIEDADES

            public DataTable tablaClientes
            {
                get { return _dtEmpleados; }
                set { _dtEmpleados = value; }
            }

            public string Codigo
            {
                get { return _codigo; }
                set { _codigo = value; }
            }

            public string Nombre
            {
                get { return _nombre; }
                set { _nombre = value; }
            }

            #endregion

            #region METODOS PUBLICOS

            ///<sumary>Constructor por defecto de la clase
            ///<param name="empresaLogin">Código de empresa contra la que trabaja el usuario</param>
            ///</sumary>
            public cEmpleados(string empresaLogin)
            {
                this._empresaLogin = empresaLogin;
                // Al instanciar la clase, se obtiene un listado completo de los clientes de la empresa
                GetEmployees();
            }

            public bool Get(string codigo)
            {
                ///<summary>Método que busca un cliente en el DataTable de clientes</summary>
                ///<param name="codigo" type="string">Código de cliente a buscar</param>

                return (GetEmployee(codigo));
            }

            public void LoadDropDownList(ref System.Web.UI.WebControls.DropDownList oDDL)
            {
                oDDL.Items.Clear();
                oDDL.DataSource = _dtEmpleados;
                oDDL.DataValueField = _dtEmpleados.Columns["codigo"].ToString();
                oDDL.DataTextField = _dtEmpleados.Columns["nombre"].ToString();
                oDDL.DataBind();
                // se añade un item vacío para cuando se quiera deseleccionar
                oDDL.Items.Add("");
                oDDL.SelectedIndex = oDDL.Items.Count - 1;
            }

            #endregion

            #region METODOS PRIVADOS

            private void GetEmployees()
            {
                // Método que obtiene un objeto DataTable de clientes, con los siguientes datos:
                // codigo, nombre, nombre2, alias, almacen
                cProduccion oProduccion = new cProduccion(_empresaLogin);
                _dtEmpleados = oProduccion.GetEmployees();
            }

            private bool GetEmployee(string codigo)
            {
                try
                {
                    DataRow[] cliente = _dtEmpleados.Select("codigo='" + codigo + "'");
                    if (cliente.Count() != 0)
                    {
                        this._codigo = cliente[0]["codigo"].ToString();
                        this._nombre = cliente[0]["nombre"].ToString();
                        return (true);
                    }
                    else
                    {
                        InitProperties();
                        return (false);
                    }
                }
                catch
                {
                    return (false);
                }
            }

            private void InitProperties()
            {
                this._codigo = "";
                this._nombre = "";
            }


            #endregion
        }

        public class cPedidosVenta
        {
            #region VARIABLES PRIVADAS

            private string _empresaLogin;

            private string _codCliente;
            private DataTable _dtPedidos;
            private int _selectIndex;

            #endregion

            #region METODOS PRIVADOS

            private void GetOrders()
            {
                cProduccion oProduccion = new cProduccion(_empresaLogin);
                _dtPedidos = oProduccion.GetCustomerOrders(_codCliente);
            }

            private void GetOrders(bool enPreparacion)
            {
                cProduccion oProduccion = new cProduccion(_empresaLogin);
                _dtPedidos = oProduccion.GetCustomerOrders(_codCliente, enPreparacion);
            }

            private int FindOrder(string order)
            {
                try
                {
                    DataRow[] filtro = _dtPedidos.Select("Numero='" + order + "'");
                    if (filtro.Count() != 0)
                        return (int.Parse(filtro[0]["Linea"].ToString()));
                    else
                        return (-1);
                }
                catch
                {
                    return (-1);
                }
            }

            #endregion

            #region PROPIEDADES

            public string CodCliente
            {
                get { return _codCliente; }
                set { _codCliente = value; }
            }

            public DataTable DtPedidos
            {
                get { return _dtPedidos; }
                set { _dtPedidos = value; }
            }

            public int SelectIndex
            {
                get { return _selectIndex; }
                set { _selectIndex = value; }
            }

            #endregion

            #region METODOS PUBLICOS

            public cPedidosVenta(string empresa, string cliente)
            {
                this._empresaLogin = empresa;
                this._codCliente = cliente;
                GetOrders();
            }

            public cPedidosVenta(string empresa, string cliente, bool enPreparacion)
            {
                this._empresaLogin = empresa;
                this._codCliente = cliente;
                GetOrders(enPreparacion);
            }

            public void SelectGridViewRow(ref System.Web.UI.WebControls.GridView oGrd, string pedido)
            {
                oGrd.SelectedIndex = FindOrder(pedido);
            }

            public bool ExistePedido(string numPedido)
            {
                DataRow[] pedidos = _dtPedidos.Select("Numero='" + numPedido + "'");
                return (pedidos.Count() != 0);
            }

            #endregion
        }

    }
}
