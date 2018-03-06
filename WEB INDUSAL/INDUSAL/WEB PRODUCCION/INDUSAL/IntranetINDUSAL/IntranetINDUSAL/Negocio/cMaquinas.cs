using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using INIKER.Cliente;

namespace IntranetINDUSAL.Negocio
{
    public class cMaquinas
    {
        #region VARIABLES PRIVADAS

            private string _empresaLogin;

            private string _tipoMaquina;
            
            private DataTable _dtMaquinas;
            private int _nMaquinas;            

            private string _codigo;
            private string _descripcion;
            private string _alias;


        #endregion

        #region PROPIEDADES

            public string TipoMaquina
            {
                get { return _tipoMaquina; }
                set { _tipoMaquina = value; }
            }

            public DataTable tablaMaquinas
            {
                get { return _dtMaquinas; }
                set { _dtMaquinas = value; }
            }
        
            public int NMaquinas
            {
                get { return _nMaquinas; }
                set { _nMaquinas = value; }
            }

            public string Codigo
            {
                get { return _codigo; }
                set { _codigo = value; }
            }

            public string Nombre
            {
                get { return _descripcion; }
                set { _descripcion = value; }
            }

            public string Alias
            {
                get { return _alias; }
                set { _alias = value; }
            }

        #endregion

        #region METODOS PUBLICOS

            ///<sumary>Constructor por defecto de la clase
            ///<param name="empresaLogin">Código de empresa contra la que trabaja el usuario</param>
            ///<param name="tipoMaquina">Código del grupo de máquinas de trabajo</param>
            ///</sumary>
            public cMaquinas(string empresaLogin,string tipoMaquina)
            {          
                
                this._empresaLogin = empresaLogin;
                this._tipoMaquina = tipoMaquina;
                // Al instanciar la clase, se obtiene un listado completo de los clientes de la empresa
                GetWorkCenters();
            }

            public bool Get(string codigo)
            {
                ///<summary>Método que busca un cliente en el DataTable de clientes</summary>
                ///<param name="codigo" type="string">Código de cliente a buscar</param>

                return (GetWorkCenter(codigo));
            }

        #endregion

        #region METODOS PRIVADOS

            private void GetWorkCenters()
            {
                // Método que obtiene un objeto DataTable de clientes, con los siguientes datos:
                // codigo, nombre, nombre2, alias, almacen
                cProduccion oProduccion = new cProduccion(_empresaLogin);
                _dtMaquinas = oProduccion.GetWorkCenters(_tipoMaquina);
                _nMaquinas = _dtMaquinas.Rows.Count;
            }

            private bool GetWorkCenter(string codigo)
            {
                try
                {
                    DataRow[] cliente = _dtMaquinas.Select("codigo='" + codigo + "'");
                    this._codigo = cliente[0]["codigo"].ToString();
                    this._descripcion = cliente[0]["descripcion"].ToString();
                    this._alias = cliente[0]["alias"].ToString();
                    return (true);
                }
                catch 
                {
                    return (false);
                }
            }


        #endregion
    }
}
