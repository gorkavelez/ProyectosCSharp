using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IntranetINDUSAL.Negocio
{
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
                _dtPedidos = oProduccion.GetCustomerOrders(_codCliente,enPreparacion);
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

            public cPedidosVenta(string empresa, string cliente,bool enPreparacion)
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
