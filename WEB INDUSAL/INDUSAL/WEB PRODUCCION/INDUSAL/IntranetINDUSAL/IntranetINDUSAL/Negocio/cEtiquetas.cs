using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using INIKER.FuncFabricacion;

namespace IntranetINDUSAL.Negocio
{
    public class cEtiquetas
    {
        private string _empresaLogin;
        private string _userLogin;
        private string _pwdLogin;

        public cEtiquetas(string usuario, string password, string empresa)
        {
            this._userLogin = usuario;
            this._pwdLogin = password;
            this._empresaLogin = empresa;
        }

        public void ClienteProducto_120x80(string codCliente, string nomCliente,
                                            string codProducto, string nomProducto,
                                            int nEtiquetas)
        {
            FuncFabricacion oEtiquetas = new FuncFabricacion(_userLogin, _pwdLogin, _empresaLogin);
            oEtiquetas.ImprEtiqCteProd(codCliente, nomCliente, codProducto, nomProducto, nEtiquetas);
        }

        //private DataTable _dtEtiquetas;

        //private string _idEtiqueta;
        //private string _codCliente;
        //private string _nomCliente;
        //private string _codProducto;
        //private string _nomProducto;

        //public DataTable DtEtiquetas
        //{
        //    get { return _dtEtiquetas; }
        //    set { _dtEtiquetas = value; }
        //}

        //public string IdEtiqueta
        //{
        //    get { return _idEtiqueta; }
        //    set { _idEtiqueta = value; }
        //}
        
        //public string CodCliente
        //{
        //    get { return _codCliente; }
        //    set { _codCliente = value; }
        //}
        
        //public string NomCliente
        //{
        //    get { return _nomCliente; }
        //    set { _nomCliente = value; }
        //}
        
        //public string CodProducto
        //{
        //    get { return _codProducto; }
        //    set { _codProducto = value; }
        //}        

        //public string NomProducto
        //{
        //    get { return _nomProducto; }
        //    set { _nomProducto = value; }
        //}               

        //public cEtiquetas()
        //{
        //    CreateDataTable();
        //}

        //public void Add()
        //{
        //    DataRow newRow = _dtEtiquetas.NewRow();
        //    newRow["idEtiqueta"] = _idEtiqueta;
        //    newRow["codCliente"] = _codCliente;
        //    newRow["nomCliente"] = _nomCliente;
        //    newRow["codProducto"] = _codProducto;
        //    newRow["nomProducto"] = _nomProducto;
        //    _dtEtiquetas.Rows.Add(newRow);
        //}

        //public DataRow[] Select(string idEtiq)
        //{
        //    return (_dtEtiquetas.Select("idEtiqueta='" + idEtiq + "'"));
        //}

        //private void CreateDataTable()
        //{
        //    _dtEtiquetas = new DataTable("etiquetas");
        //    DataColumn newColumn;

        //    // Se crean las columnas comunes a todos los tipos de conteo                        
        //    newColumn = _dtEtiquetas.Columns.Add("nRegistro");
        //    newColumn.DataType = System.Type.GetType("System.Int16");
        //    newColumn.AutoIncrement = true;
        //    newColumn.AutoIncrementSeed = 1;
        //    newColumn.AutoIncrementStep = 1;

        //    newColumn = _dtEtiquetas.Columns.Add("idEtiqueta");
        //    newColumn.DataType = System.Type.GetType("System.String");            
        //    newColumn.AllowDBNull = false;

        //    newColumn = _dtEtiquetas.Columns.Add("codCliente");
        //    newColumn.DataType = System.Type.GetType("System.String");
        //    newColumn.DefaultValue = "";
        //    newColumn.AllowDBNull = true;

        //    newColumn = _dtEtiquetas.Columns.Add("NomCliente");
        //    newColumn.DataType = System.Type.GetType("System.String");
        //    newColumn.DefaultValue = "";
        //    newColumn.AllowDBNull = true;

        //    newColumn = _dtEtiquetas.Columns.Add("CodProducto");
        //    newColumn.DataType = System.Type.GetType("System.String");
        //    newColumn.DefaultValue = "";
        //    newColumn.AllowDBNull = true;

        //    newColumn = _dtEtiquetas.Columns.Add("NomProducto");
        //    newColumn.DataType = System.Type.GetType("System.String");
        //    newColumn.DefaultValue = "";
        //    newColumn.AllowDBNull = true;
        //}

    }
}
