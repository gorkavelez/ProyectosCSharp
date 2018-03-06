using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using INIKER.Cliente;

namespace IntranetINDUSAL.Negocio
{
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
}
