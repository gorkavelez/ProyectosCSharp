using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using INIKER.CrossReferences;

namespace IntranetINDUSAL
{
    public class cProductos
    {
        #region Variables Privadas    
        
            private string _userLogin;
            private string _pwdLogin;
        
        #endregion

        # region Miembros Publicos

        public cProductos()
        {
            DatosLogin();
        }        

        public DataTable ObtenerSurtidoClienteClasificado(string cliente, string empresa)
        {
            CrossReferencesINDUSAL_Service surtidoCliente = new CrossReferencesINDUSAL_Service(_userLogin, _pwdLogin, empresa);
            CrossReferencesINDUSAL[] productosSinClasificar = surtidoCliente.ReadMultiple(cliente, 0, 0, "");
            DataTable tablaProductos = new DataTable("productos");
            foreach (CrossReferencesINDUSAL producto in productosSinClasificar)
            {
                CrearColumna(ref tablaProductos,producto.Desc_categ_prod);
                AgregarValor(ref tablaProductos, producto.Desc_categ_prod, producto.Item_No+";"+producto.Descripcion_Planta);                
            }
            return (tablaProductos);
        }

        public DataTable ObtenerSurtidoClienteClasificado(string cliente, string empresa,Tipo_Planchado tipo)
        {
            string filtro = cliente + ";" + tipo;
            CrossReferencesINDUSAL_Service surtidoCliente = new CrossReferencesINDUSAL_Service(_userLogin, _pwdLogin, empresa);            
            CrossReferencesINDUSAL[] productosSinClasificar = surtidoCliente.ReadMultiple(filtro, 0, 0, "");
            DataTable tablaProductos = new DataTable("productos");
            foreach (CrossReferencesINDUSAL producto in productosSinClasificar)
            {             
                CrearColumna(ref tablaProductos, producto.Desc_categ_prod);
                AgregarValor(ref tablaProductos, producto.Desc_categ_prod, producto.Item_No + ";" + producto.Descripcion_Planta);
            }
            return (tablaProductos);
        }

        public DataSet ObtenerSurtidoCliente(string cliente, string empresa, Tipo_Planchado tipo)
        {
            string filtro = cliente + ";" + tipo;
            CrossReferencesINDUSAL_Service surtidoCliente = new CrossReferencesINDUSAL_Service(_userLogin, _pwdLogin, empresa);
            CrossReferencesINDUSAL[] productosSinClasificar = surtidoCliente.ReadMultiple(filtro, 0, 0, "");
            DataSet dsSurtidoCliente = new DataSet("surtido");
            foreach (CrossReferencesINDUSAL producto in productosSinClasificar)
            {
                //CrearTabla(ref dsSurtidoCliente, producto.Desc_categ_prod);
                //CrearColumna(ref dsSurtidoCliente, producto.Desc_categ_prod, producto.Desc_grupo_prod);
                //AgregarValor(ref dsSurtidoCliente, producto.Desc_categ_prod, producto.Desc_grupo_prod, producto.Item_No + ";" + producto.Descripcion_Planta);
                CrearTabla(ref dsSurtidoCliente, producto.Desc_categ_prod, producto.Cod_categ_prod);
                CrearColumna(ref dsSurtidoCliente, producto.Desc_categ_prod, producto.Cod_categ_prod, producto.Desc_grupo_prod, producto.Cod_grupo_prod);
                AgregarValor(ref dsSurtidoCliente, producto.Desc_categ_prod, producto.Desc_grupo_prod, producto.Item_No + ";" + producto.Descripcion_Planta);
            }
            return (dsSurtidoCliente);
        }

        public DataSet ObtenerSurtidoCliente(string cliente, string empresa)
        {
            string filtro = cliente;
            CrossReferencesINDUSAL_Service surtidoCliente = new CrossReferencesINDUSAL_Service(_userLogin, _pwdLogin, empresa);
            CrossReferencesINDUSAL[] productosSinClasificar = surtidoCliente.ReadMultiple(filtro, 0, 0, "");
            DataSet dsSurtidoCliente = new DataSet("surtido");
            foreach (CrossReferencesINDUSAL producto in productosSinClasificar)
            {
                CrearTabla(ref dsSurtidoCliente, producto.Desc_categ_prod,producto.Cod_categ_prod);
                CrearColumna(ref dsSurtidoCliente, producto.Desc_categ_prod,producto.Cod_categ_prod, producto.Desc_grupo_prod,producto.Cod_grupo_prod);
                AgregarValor(ref dsSurtidoCliente, producto.Desc_categ_prod, producto.Desc_grupo_prod, producto.Item_No + ";" + producto.Descripcion_Planta);
            }
            return (dsSurtidoCliente);
        }

        public DataTable GetCustomerReferences(string customer, string empresa)
        {
            CrossReferencesINDUSAL_Service surtidoCliente = new CrossReferencesINDUSAL_Service(_userLogin, _pwdLogin, empresa);
            CrossReferencesINDUSAL[] productosSinClasificar = surtidoCliente.ReadMultiple(customer, 0, 0, "");
            DataTable tablaProductos = new DataTable("productos");
            CrearColumna(ref tablaProductos, "codProducto");
            CrearColumna(ref tablaProductos, "codFactProducto");
            CrearColumna(ref tablaProductos, "desFactProducto");
            foreach (CrossReferencesINDUSAL producto in productosSinClasificar)
            {
                DataRow newRow = tablaProductos.NewRow();
                newRow["codProducto"] = producto.Item_No;
                newRow["codFactProducto"] = producto.Codigo_facturacion;
                newRow["desFactProducto"] = producto.Alias_Cod_Facturacion;
                tablaProductos.Rows.Add(newRow);
            }
            return (tablaProductos);
        }

        public INIKER.Item.ItemList[] GetWagons(string empresa)
        {
            INIKER.Item.ItemList_Service oItem = new INIKER.Item.ItemList_Service(_userLogin, _pwdLogin, empresa);
            return(oItem.ReadMultiple(";CARROS",0,0,""));
        }

        public INIKER.Item.ItemList[] GetSurtidoSubfamilia(string familia, string subfamilia, string empresa)
        {
            INIKER.Item.ItemList_Service oItem = new INIKER.Item.ItemList_Service(_userLogin, _pwdLogin, empresa);
            return (oItem.ReadMultiple(";" + familia + ";" + subfamilia, 0, 0, ""));
        }

        #endregion

        #region Miembros Privados

        private void DatosLogin()
        {
            IntranetINDUSAL.Properties.Settings mySettings = new IntranetINDUSAL.Properties.Settings();
            _userLogin = mySettings.usuarioPruebas;
            _pwdLogin = mySettings.passwordPruebas;
        }

        private void CrearTabla(ref DataSet myDS, string tableName)
        {            
            if (tableName != null)
            {
                DataTable myTable = myDS.Tables[tableName];
                if (myTable == null)
                {
                    myDS.Tables.Add(tableName);
                }
            }

        }

        private void CrearTabla(ref DataSet myDS, string tableName, string tableCode)
        {
            if (tableName != null)
            {
                DataTable myTable = myDS.Tables[tableName];
                if (myTable == null)
                {
                    myTable=myDS.Tables.Add(tableName);
                    myTable.ExtendedProperties.Add("Code", tableCode);
                }
            }

        }

        private void CrearColumna(ref DataTable tablaProductos, string categoria)
        {
            if((tablaProductos!=null)&&(categoria!=null))
            {
                DataColumn myCol = tablaProductos.Columns[categoria];
                if (myCol == null)
                {
                    tablaProductos.Columns.Add(categoria);
                }
            }
        }

        private void CrearColumna(ref DataTable tablaProductos, string categoria, string columnCode)
        {
            if ((tablaProductos != null) && (categoria != null))
            {
                DataColumn myCol = tablaProductos.Columns[categoria];
                if (myCol == null)
                {
                    myCol=tablaProductos.Columns.Add(categoria);
                    myCol.ExtendedProperties.Add("Code", columnCode);
                }
            }
        }

        private void CrearColumna(ref DataSet myDS, string tableName, string columnName)
        {
            if ((tableName != null) && (columnName != null))
            {
                DataTable myTable=myDS.Tables[tableName];
                if (myTable==null)
                {
                    CrearTabla(ref myDS, tableName);
                    myTable = myDS.Tables[tableName];
                }
                                
                DataColumn myCol = myTable.Columns[columnName];
                if(myCol==null)
                {
                    myTable.Columns.Add(columnName);
                }
            }
        }

        private void CrearColumna(ref DataSet myDS, string tableName, string tableCode ,string columnName, string columnCode)
        {
            if ((tableName != null) && (columnName != null))
            {
                DataTable myTable = myDS.Tables[tableName];
                if (myTable == null)
                {
                    CrearTabla(ref myDS, tableName);
                    myTable = myDS.Tables[tableName];
                    myTable.ExtendedProperties.Add("Code", tableCode);
                }

                DataColumn myCol = myTable.Columns[columnName];
                if (myCol == null)
                {
                    myCol=myTable.Columns.Add(columnName);
                    myCol.ExtendedProperties.Add("Code", columnCode);
                }
            }
        }

        private void AgregarValor(ref DataTable tablaProductos, string columna, string valor)
        {
            if ((tablaProductos != null) && (columna != null))
            {
                foreach (DataRow fila in tablaProductos.Rows)
                {
                    if (fila[columna].ToString() == "")
                    {
                        fila[columna] = valor;
                        return;
                    }
                }
                DataRow newRow = tablaProductos.NewRow();
                newRow[columna] = valor;
                tablaProductos.Rows.Add(newRow);
            }
        }        

        private void AgregarValor(ref DataSet myDS, string tableName, string columnName, string valor)
        {
            if ((myDS != null) && (tableName != null) && (columnName != null))
            {
                foreach (DataRow fila in myDS.Tables[tableName].Rows)
                {
                    if (fila[columnName].ToString() == "")
                    {
                        fila[columnName] = valor;
                        return;
                    }
                }

                DataRow newRow = myDS.Tables[tableName].NewRow();
                newRow[columnName] = valor;
                myDS.Tables[tableName].Rows.Add(newRow);
            }
        }

        #endregion

    }
}
