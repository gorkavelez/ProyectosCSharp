using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using INIKER.Cliente;

namespace IntranetINDUSAL.Negocio
{
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
}
