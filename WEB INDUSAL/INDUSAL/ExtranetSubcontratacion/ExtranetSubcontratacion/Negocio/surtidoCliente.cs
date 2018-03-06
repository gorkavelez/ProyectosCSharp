using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExtranetSubcontratacion.Negocio
{
    public class surtidoCliente
    {
        #region VARIABLES PRIVADAS

        private string _codCliente;
        private string _nomCliente;

        private DataTable _tablaSurtido;

        #endregion

        #region PROPIEDADES

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

        #endregion

        #region METODOS PUBLICOS

        public DataTable TablaSurtido
        {
            get { return _tablaSurtido; }
            set { _tablaSurtido = value; }
        }

        public DataTable GetFamilias()
        {
            DataTable tabla = CreateDataTable("familias");
            DataRow newRow;

            foreach (DataRow fila in _tablaSurtido.Rows)
            {
                if (!ExisteCodigo(fila["CodFamilia"].ToString(), tabla))
                {
                    newRow = tabla.NewRow();
                    newRow["codigo"] = fila["CodFamilia"].ToString();
                    newRow["descripcion"] = fila["DescrFamilia"].ToString();
                    tabla.Rows.Add(newRow);
                }
            }

            return (tabla);
        }

        public DataTable GetSubfamilias(string familia)
        {
            DataTable tabla = CreateDataTable("subfamilias");
            DataRow newRow;

            DataRow[] filtro = _tablaSurtido.Select("CodFamilia='" + familia + "'");

            foreach (DataRow fila in filtro)
            {
                if (!ExisteCodigo(fila["CodSubfamilia"].ToString(), tabla))
                {
                    newRow = tabla.NewRow();
                    newRow["codigo"] = fila["CodSubfamilia"].ToString();
                    newRow["descripcion"] = fila["DescrSubfamilia"].ToString();
                    tabla.Rows.Add(newRow);
                }
            }

            return (tabla);
        }

        public DataTable GetProductos()
        {
            DataTable tabla = CreateDataTableSurtido("productos");
            DataRow newRow;            

            foreach (DataRow fila in _tablaSurtido.Rows)
            {
                newRow = tabla.NewRow();
                newRow["codigo"] = fila["CodProducto"].ToString();
                newRow["descripcion"] = fila["DescrProducto"].ToString();
                newRow["cantidad"] = fila["Cantidad"];
                tabla.Rows.Add(newRow);
            }

            return (tabla);
        }

        public DataTable GetProductos(string familia)
        {
            DataTable tabla = CreateDataTableSurtido("productos");
            DataRow newRow;

            DataRow[] filtro = _tablaSurtido.Select("CodFamilia='" + familia + "'");

            foreach (DataRow fila in filtro)
            {
                if (!ExisteCodigo(fila["CodProducto"].ToString(), tabla))
                {
                    newRow = tabla.NewRow();
                    newRow["codigo"] = fila["CodProducto"].ToString();
                    newRow["descripcion"] = fila["DescrProducto"].ToString();
                    newRow["cantidad"] = fila["Cantidad"];
                    tabla.Rows.Add(newRow);
                }
            }

            return (tabla);
        }
        
        public DataTable GetProductos(string familia, string subfamilia)
        {
            DataTable tabla = CreateDataTableSurtido("productos");
            DataRow newRow;

            DataRow[] filtro = _tablaSurtido.Select("CodFamilia='" + familia + "' And CodSubfamilia='" + subfamilia + "'");

            foreach (DataRow fila in filtro)
            {
                if (!ExisteCodigo(fila["CodProducto"].ToString(), tabla))
                {
                    newRow = tabla.NewRow();
                    newRow["codigo"] = fila["CodProducto"].ToString();
                    newRow["descripcion"] = fila["DescrProducto"].ToString();
                    newRow["cantidad"]=fila["Cantidad"];
                    tabla.Rows.Add(newRow);
                }
            }

            return (tabla);
        }

        public void SetItemQty(string _itemNo, decimal _qty)
        {      
            bool lcFound=false;

            for (Int32 iDataRow = 0; (iDataRow < _tablaSurtido.Rows.Count) && !lcFound; iDataRow++)
            {
                if (_tablaSurtido.Rows[iDataRow]["CodProducto"].ToString() == _itemNo)
                {
                    _tablaSurtido.Rows[iDataRow]["Cantidad"] = _qty;
                    lcFound = true;
                }
            }       
        }

        public void ResetItemQtys()
        {
            foreach (DataRow row in _tablaSurtido.Rows)
            {
                row["Cantidad"] = 0;
            }
        }


        #endregion

        #region METODOS PRIVADOS

        private DataTable CreateDataTable(string nombre)
        {
            DataTable tabla = new DataTable(nombre);
            DataColumn newColumn;

            newColumn = tabla.Columns.Add("codigo");
            newColumn.DataType = System.Type.GetType("System.String");
            newColumn.AllowDBNull = false;

            newColumn = tabla.Columns.Add("descripcion");
            newColumn.DataType = System.Type.GetType("System.String");
            newColumn.AllowDBNull = false;

            return (tabla);
        }

        private DataTable CreateDataTableSurtido(string nombre)
        {
            DataTable tabla = new DataTable(nombre);
            DataColumn newColumn;

            newColumn = tabla.Columns.Add("codigo");
            newColumn.DataType = System.Type.GetType("System.String");
            newColumn.AllowDBNull = false;

            newColumn = tabla.Columns.Add("descripcion");
            newColumn.DataType = System.Type.GetType("System.String");
            newColumn.AllowDBNull = false;

            newColumn = tabla.Columns.Add("cantidad");
            newColumn.DataType = System.Type.GetType("System.Decimal");
            newColumn.AllowDBNull = true;

            return (tabla);
        }

        private bool ExisteCodigo(string codigo, DataTable tabla)
        {
            try
            {
                DataRow[] filtro = tabla.Select("Codigo='" + codigo + "'");
                return (filtro.Count() > 0);
            }
            catch
            {
                return (false);
            }
        }

        #endregion
    }
}
