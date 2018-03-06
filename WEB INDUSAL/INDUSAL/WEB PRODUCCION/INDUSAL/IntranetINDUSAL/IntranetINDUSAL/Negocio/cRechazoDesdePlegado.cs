using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IntranetINDUSAL.Negocio
{
    public class cRechazoDesdePlegado
    {
        #region VARIABLES

            private string _empresaLogin;
            private DataTable _turnos;
            private DataTable _empleados;

            private string _datoTeclado;

            // para el rechazo normal, que se informa una vez por turno
            private string _turno;
            private string _desTurno;
            private decimal _kilos;
            // para los rechazos a óxido y costura, que se van informando conforme se detectan en Plegado
            private string _cliente;
            private string _pedido;
            private string _producto;
            private string _nSerie;
            private string _operario;
            private string _nomOperario;
            private int _cantidad;

            private string _msj;

        #endregion

        #region PROPIEDADES

            public string Empresa
            {
                get { return _empresaLogin; }
                set { _empresaLogin = value; }
            }

            public string Turno
            {
                get { return _turno; }
                set { _turno = value; }
            }

            public string DesTurno
            {
                get { return _desTurno; }
                set { _desTurno = value; }
            }

            public string DatoTeclado
            {
                get { return _datoTeclado; }
                set { _datoTeclado = value; }
            }

            public decimal Kilos
            {
                get { return _kilos; }
                set { _kilos = value; }
            }

            public string Cliente
            {
                get { return _cliente; }
                set { _cliente = value; }
            }

            public string Pedido
            {
                get { return _pedido; }
                set { _pedido = value; }
            }

            public string Producto
            {
                get { return _producto; }
                set { _producto = value; }
            }

            public string NSerie
            {
                get { return _nSerie; }
                set { _nSerie = value; }
            }

            public string Operario
            {
                get { return _operario; }
                set { _operario = value; }
            }

            public string NomOperario
            {
                get { return _nomOperario; }
                set { _nomOperario = value; }
            }

            public int Cantidad
            {
                get { return _cantidad; }
                set { _cantidad = value; }
            }

            public string Mensaje
            {
                get { return _msj; }
                set { _msj = value; }
            }

            public DataTable Turnos
            {
                get { return _turnos; }
                set { _turnos = value; }
            }

        #endregion

        #region METODOS

            public cRechazoDesdePlegado(string _empresa)
            {
                this._empresaLogin = _empresa;
                GetTurnos();
                GetEmpleados();
            }

            public void Rechazar()
            {
                cProduccion oProduccion = new cProduccion(_empresaLogin);
                try
                {
                    oProduccion.RechazarPorTurno(_turno, _operario, _kilos, _producto);
                    ResetVariablesRechazoNormal();
                }
                catch (Exception)
                { }

                
            }

            public void ACostura()
            {
                cProduccion oProduccion = new cProduccion(_empresaLogin);
                oProduccion.RechazarACostura(_cliente, _pedido, _producto, _nSerie, _operario, _turno, _cantidad);                
            }

            public void DeOxidoACostura()
            {
                cProduccion oProduccion = new cProduccion(_empresaLogin);
                oProduccion.DeOxidoACostura(_cliente, _pedido, _producto, _nSerie, _operario, _turno, _cantidad);
            }

            public void AOxido()
            {
                cProduccion oProduccion = new cProduccion(_empresaLogin);
                oProduccion.RechazarAOxido(_cliente, _pedido, _producto, _nSerie, _operario, _turno, _cantidad);
                
            }

        #endregion

        #region METODOS PRIVADOS
        
            private void GetTurnos()
            {
                cProduccion oProduccion = new cProduccion(this._empresaLogin);
                _turnos = oProduccion.GetTurnos();
            }

            private void GetEmpleados()
            {
                cProduccion oProduccion = new cProduccion(this._empresaLogin);
                _empleados = oProduccion.GetEmployees();
            }

            private void ResetVariablesRechazoNormal()
            {
                _turno = "";
                _desTurno = "";
                _operario = "";
                _nomOperario = "";
                _kilos = 0;
            }

        #endregion
    }
}
