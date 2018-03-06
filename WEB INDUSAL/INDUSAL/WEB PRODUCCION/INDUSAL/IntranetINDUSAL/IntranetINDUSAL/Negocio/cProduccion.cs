using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IntranetINDUSAL
{
    public class cProduccion
    {
        public enum eTipoCliente
        {
            SAL,
            DESAPRESTADO
        }

        #region Variables Privadas

            private string _empresaLogin;
            private string _userLogin;
            private string _pwdLogin;           

        #endregion

        #region PROPIEDADES

            public string EmpresaLogin
            {
                get { return _empresaLogin; }
                set { _empresaLogin = value; }
            }

            public string UserLogin
            {
                get { return _userLogin; }
                set { _userLogin = value; }
            }

            public string PwdLogin
            {
                get { return _pwdLogin; }
                set { _pwdLogin = value; }
            }

        #endregion

        #region Metodos Privados

            private void ObtenerDatosLogin()
            {
                IntranetINDUSAL.Properties.Settings mySettings = new IntranetINDUSAL.Properties.Settings();
                _userLogin = mySettings.usuarioPruebas;
                _pwdLogin = mySettings.passwordPruebas;
            }
        
        #endregion

        #region Metodos Publicos

            public cProduccion()
            { }

            public cProduccion(string empresa)
            {
                // Constructor de clase
                _empresaLogin = empresa;
                ObtenerDatosLogin();
            }

            #region MOVIMIENTOS PRODUCCION

                public void Desaprestar(string producto, decimal cantidad, string nSerie, DateTime fecha, DateTime hora)
                {
                    INIKER.FuncFabricacion.FuncFabricacion fabricacion =
                        new INIKER.FuncFabricacion.FuncFabricacion(_userLogin, _pwdLogin, _empresaLogin);
                    fabricacion.GenerarMovDesaprestado(producto, cantidad, nSerie,fecha,hora);
                }

                public void RegistrarConteo(string cliente, string producto, decimal cantidad, string nSerie,DateTime fecha, DateTime hora, string operario)
                {
                    INIKER.FuncFabricacion.FuncFabricacion fabricacion =
                        new INIKER.FuncFabricacion.FuncFabricacion(_userLogin, _pwdLogin, _empresaLogin);
                    fabricacion.GenerarMovConteo(cliente, producto, cantidad, nSerie,1,fecha,hora,operario);
                }

                public void RegistrarPlanchado(string cliente,
                                                string producto,
                                                decimal cantidad,
                                                string nSerie,
                                                int tipoPlanchado,
                                                string maquina,
                                                string turno,
                                                string pedido,
                                                string operario)
                {
                    INIKER.FuncFabricacion.FuncFabricacion fabricacion =
                        new INIKER.FuncFabricacion.FuncFabricacion(_userLogin, _pwdLogin, _empresaLogin);
                    fabricacion.GenerarMovPlanchado(cliente, producto, cantidad, nSerie, tipoPlanchado, maquina, turno,pedido,operario,
                        DateTime.Today,DateTime.Now);
                }

                public void RegisterProdMovs(INIKER.MovsProduccion.MovsProdSinRegINDUSAL[] movs)
                {
                    INIKER.FuncFabricacion.FuncFabricacion oFuncFabr =
                        new INIKER.FuncFabricacion.FuncFabricacion(_userLogin, _pwdLogin, _empresaLogin);

                    foreach (INIKER.MovsProduccion.MovsProdSinRegINDUSAL oMov in movs)
                    {
                        //oFuncFabr.GenerarMovExpedicion( "",
                        //                                oMov.Fecha,
                        //                                oMov.Hora,
                        //                                oMov.Cod_Cliente,
                        //                                oMov.Producto,
                        //                                oMov.Cantidad_enviar,
                        //                                oMov.Cod_carro,
                        //                                oMov.Num_carro,
                        //                                oMov.Carro_completo,
                        //                                oMov.Num_movimiento);                   
                    }
                }

                public void RegistrarLavado(string operario,
                    string maquina,
                    string programa,
                    decimal peso,
                    string nLavado,
                    DateTime fecha,
                    DateTime hora,
                    string contenedor,
                    string turno,
                    string cliente,
                    string pedido,
                    decimal tiempoProg)
                {
                    INIKER.FuncFabricacion.FuncFabricacion fabricacion =
                        new INIKER.FuncFabricacion.FuncFabricacion(_userLogin, _pwdLogin, _empresaLogin);
                    fabricacion.GenerarMovLavadora(operario, maquina, programa, peso, nLavado, fecha, hora, contenedor, turno,cliente,pedido,tiempoProg);
                }

                public void RegistrarTunel(string operario,
                                                string maquina,
                                                decimal tiempo,
                                                decimal peso,
                                                DateTime fecha,
                                                DateTime hora,
                                                string turno,
                                                string cliente,
                                                string pedido)
                {
                    INIKER.FuncFabricacion.FuncFabricacion fabricacion =
                        new INIKER.FuncFabricacion.FuncFabricacion(_userLogin, _pwdLogin, _empresaLogin);
                    fabricacion.GenerarMovTunel(operario, maquina, tiempo, peso, fecha, hora, turno,cliente,pedido);
                }
        
                public string GetWashNumber()
                {
                    INIKER.FuncFabricacion.FuncFabricacion fabricacion =
                        new INIKER.FuncFabricacion.FuncFabricacion(_userLogin, _pwdLogin, _empresaLogin);
                    return (fabricacion.GetNoSerieLavado());
                }

                public void RegistrarRecepcion(string transportista, string cliente, decimal recibido, decimal entregado,
                    decimal peso, string ruta, string incidencia, bool sinConteo,int carrosVacios,bool tercerCircuito)
                {
                    INIKER.FuncFabricacion.FuncFabricacion fabricacion =
                        new INIKER.FuncFabricacion.FuncFabricacion(_userLogin, _pwdLogin, _empresaLogin);                    
                    fabricacion.GenerarMovRecepcion(transportista, cliente, recibido, entregado, peso, ruta, incidencia, sinConteo,carrosVacios,
                        DateTime.Today,DateTime.Now,tercerCircuito);
                }

                public string RegistrarPesaje(string empleado, string turno, string cliente, string pedido, string producto,
                    string nSerie, decimal peso, int nCarro, string codCarro, bool completo, bool consolidado, bool expedicion, int uds)
                {
                    INIKER.FuncFabricacion.FuncFabricacion fabricacion =
                        new INIKER.FuncFabricacion.FuncFabricacion(_userLogin, _pwdLogin, _empresaLogin);
                    return(fabricacion.GenerarMovPesaje(empleado, turno, cliente, pedido, producto, nSerie, peso, nCarro, codCarro, completo, consolidado, expedicion,uds,
                            DateTime.Today,DateTime.Now,0));
                }

                public void ActualizarPesaje(string nMovimiento, bool completo)
                {
                    INIKER.FuncFabricacion.FuncFabricacion fabricacion =
                        new INIKER.FuncFabricacion.FuncFabricacion(_userLogin, _pwdLogin, _empresaLogin);
                    fabricacion.ActualizarMovProdPesaje(nMovimiento,completo);
                }

                public void EliminarPesaje(string nMovimiento)
                {
                    INIKER.FuncFabricacion.FuncFabricacion fabricacion =
                        new INIKER.FuncFabricacion.FuncFabricacion(_userLogin, _pwdLogin, _empresaLogin);
                    fabricacion.EliminarMovProdPesaje(nMovimiento);
                }

                public void RegistrarSalidaCostura(string operario, string turno,string cliente,string producto, string nSerie,
                    decimal uds, string operacion)
                {
                    INIKER.FuncFabricacion.FuncFabricacion fabricacion =
                                            new INIKER.FuncFabricacion.FuncFabricacion(_userLogin, _pwdLogin, _empresaLogin);
                    fabricacion.SalidaCostura(operario, turno, cliente, producto, nSerie, uds,
                        DateTime.Today,DateTime.Now,operacion);
                }

                public void RegistrarMovUniformidad(string empleado, string turno, string cliente, string pedido, string producto, string nSerie, int cantidad)
                {
                    INIKER.FuncFabricacion.FuncFabricacion fabricacion =
                        new INIKER.FuncFabricacion.FuncFabricacion(_userLogin, _pwdLogin, _empresaLogin);
                    fabricacion.GenerarMovUniformidad(empleado, DateTime.Today, DateTime.Now, turno, 
                        cliente, pedido, producto, nSerie, cantidad);
                }

            #endregion
                       
            public INIKER.RutasTransporte.ListaRTransporteINDUSAL[] GetTransRoutes()
            {
                INIKER.RutasTransporte.ListaRTransporteINDUSAL_Service oRutas =
                    new INIKER.RutasTransporte.ListaRTransporteINDUSAL_Service(_userLogin, _pwdLogin, _empresaLogin);
                return (oRutas.ReadMultiple("", 0, 0, ""));
            }

            public INIKER.MovsProduccion.MovsProdSinRegINDUSAL[] GetUnregisterMovs(string customer)
            {
                INIKER.MovsProduccion.MovsProdSinRegINDUSAL_Service oMovs =
                    new INIKER.MovsProduccion.MovsProdSinRegINDUSAL_Service(_userLogin, _pwdLogin, _empresaLogin);
                return (oMovs.ReadMultiple(";EXPEDICION;" + customer, 0,0, ""));
            }

            public void RegistrarPedidoVenta(string numPedido)
            {
                INIKER.FuncFabricacion.FuncFabricacion fabricacion =
                        new INIKER.FuncFabricacion.FuncFabricacion(_userLogin, _pwdLogin, _empresaLogin);
                fabricacion.RegistrarPedidoVenta(numPedido);
            }

            public int GetUdsContadasPlegado(string tipoMov, string customer, string order, string item)
            {
                string lcFilterStr=";" + tipoMov + ";" + customer + ";" + order + ";" + item;
                if (customer == "000000")
                    lcFilterStr += ";;" + System.DateTime.Today.ToShortDateString();

                // tipoMov debe ser CALANDRA, FELPA o FORMA
                INIKER.MovsProduccion.MovsProdSinRegINDUSAL_Service oMovs =
                    new INIKER.MovsProduccion.MovsProdSinRegINDUSAL_Service(_userLogin, _pwdLogin, _empresaLogin);

                INIKER.MovsProduccion.MovsProdSinRegINDUSAL[] movsEmpaquetado = oMovs.ReadMultiple(lcFilterStr, 0, 0, "");
                return (AcumularQtys(movsEmpaquetado));
            }

            private int AcumularQtys(INIKER.MovsProduccion.MovsProdSinRegINDUSAL[] movsEmpaquetado)
            {
                decimal qty = 0;

                try
                {
                    foreach (INIKER.MovsProduccion.MovsProdSinRegINDUSAL movimiento in movsEmpaquetado)
                    {
                        qty += movimiento.Cantidad_recibida;
                    }
                }
                catch
                {
                    qty = 0;
                }

                return (int.Parse(qty.ToString()));
            }

            #region TURNOS DE TRABAJO

                public DataTable GetTurnos()
                {
                    INIKER.WorkShift.WorkShiftList_Service workShifts =
                        new INIKER.WorkShift.WorkShiftList_Service(_userLogin, _pwdLogin, _empresaLogin);
                    INIKER.WorkShift.WorkShiftList[] listaTurnos = workShifts.ReadMultiple("", 0, 0, "");
                    return (ArrayTurnosToDataTable(listaTurnos));
                }

                private DataTable ArrayTurnosToDataTable(INIKER.WorkShift.WorkShiftList[] turnos)
                {
                    DataTable tabla = new DataTable("turnos");
                    DataColumn newColumn;
                    DataRow newRow;

                    newColumn = tabla.Columns.Add("codigo");
                    newColumn.DataType = System.Type.GetType("System.String");
                    newColumn.AllowDBNull = false;

                    newColumn = tabla.Columns.Add("descripcion");
                    newColumn.DataType = System.Type.GetType("System.String");
                    newColumn.AllowDBNull = false;

                    foreach (INIKER.WorkShift.WorkShiftList turno in turnos)
                    {
                        newRow = tabla.NewRow();
                        newRow["codigo"] = turno.Code;
                        newRow["descripcion"] = turno.Description;                        
                        tabla.Rows.Add(newRow);
                    }

                    return (tabla);
                }

            #endregion

            #region EMPLEADOS

                public DataTable GetEmployees()
                {
                    INIKER.Employee.EmployeeList_Service employees =
                        new INIKER.Employee.EmployeeList_Service(_userLogin, _pwdLogin, _empresaLogin);
                    INIKER.Employee.EmployeeList[] listaEmpleados = employees.ReadMultiple("", 0, 0, "");
                    return (ArrayEmpleadosToDataTable(listaEmpleados));
                }

                private DataTable ArrayEmpleadosToDataTable(INIKER.Employee.EmployeeList[] empleados)
                {
                    DataTable tabla = new DataTable("empleados");
                    DataColumn newColumn;
                    DataRow newRow;

                    newColumn = tabla.Columns.Add("codigo");
                    newColumn.DataType = System.Type.GetType("System.String");
                    newColumn.AllowDBNull = false;

                    newColumn = tabla.Columns.Add("nombre");
                    newColumn.DataType = System.Type.GetType("System.String");
                    newColumn.AllowDBNull = false;

                    foreach (INIKER.Employee.EmployeeList empleado in empleados)
                    {
                        newRow = tabla.NewRow();
                        newRow["codigo"] = empleado.No;
                        newRow["nombre"] = empleado.Name + "";
                        tabla.Rows.Add(newRow);
                    }

                    return (tabla);
                }

                public string GetEmployeeName(string codigo)
                {
                    INIKER.Employee.EmployeeList_Service employees =
                        new INIKER.Employee.EmployeeList_Service(_userLogin, _pwdLogin, _empresaLogin);
                    INIKER.Employee.EmployeeList[] listaEmpleados = employees.ReadMultiple(codigo, 0, 0, "");
                    return (listaEmpleados[0].Name);
                }
            #endregion
        
            #region PROGRAMAS DE LAVADO

                public DataTable GetProgramasLavado()
                {
                    INIKER.ProgLavado.ListaPLavadoINDUSAL_Service programas=
                        new INIKER.ProgLavado.ListaPLavadoINDUSAL_Service(_userLogin, _pwdLogin, _empresaLogin);
                    INIKER.ProgLavado.ListaPLavadoINDUSAL[] listaProgramas = programas.ReadMultiple("", 0, 0, "");
                    return (ArrayProgramasToDataTable(listaProgramas));
                }                

                private DataTable ArrayProgramasToDataTable(INIKER.ProgLavado.ListaPLavadoINDUSAL[] programas)
                {
                    DataTable tabla = new DataTable("programas");
                    DataColumn newColumn;
                    DataRow newRow;

                    newColumn = tabla.Columns.Add("maquina");
                    newColumn.DataType = System.Type.GetType("System.String");
                    newColumn.AllowDBNull = false;

                    newColumn = tabla.Columns.Add("programa");
                    newColumn.DataType = System.Type.GetType("System.String");
                    newColumn.AllowDBNull = false;

                    newColumn = tabla.Columns.Add("tiempo");
                    newColumn.DataType = System.Type.GetType("System.Single");
                    newColumn.AllowDBNull = false;

                    foreach (INIKER.ProgLavado.ListaPLavadoINDUSAL programa in programas)
                    {
                        newRow = tabla.NewRow();
                        newRow["maquina"] = programa.Maquina;
                        newRow["programa"] = programa.Programa;
                        newRow["tiempo"] = programa.Tiempo_lavado;
                        tabla.Rows.Add(newRow);
                    }

                    return (tabla);
                }

            #endregion

            #region CARROS/SACAS 

                public DataTable GetCarrosSacas()
                {
                    cProductos oProductos = new cProductos();
                    INIKER.Item.ItemList[] carros = oProductos.GetWagons(_empresaLogin);
                    return (ArrayCarrosToDataTable(carros));
                }

                public decimal GetPesoCarro(string codigo)
                {
                    INIKER.Item.ItemList[] listaItems;
                    INIKER.Item.ItemList_Service oItem = new INIKER.Item.ItemList_Service(_userLogin, _pwdLogin, _empresaLogin);
                    listaItems = oItem.ReadMultiple(codigo, 0, 0, "");
                    if (listaItems.Count() > 0)
                    {
                        return (listaItems[0].Net_Weight);
                    }
                    else
                    {
                        return (0);
                    }

                }

                private DataTable ArrayCarrosToDataTable(INIKER.Item.ItemList[] carros)
                {
                    DataTable tabla = new DataTable("carros");
                    DataColumn newColumn;
                    DataRow newRow;

                    newColumn = tabla.Columns.Add("codigo");
                    newColumn.DataType = System.Type.GetType("System.String");
                    newColumn.AllowDBNull = false;

                    newColumn = tabla.Columns.Add("descripcion");
                    newColumn.DataType = System.Type.GetType("System.String");
                    newColumn.AllowDBNull = false;

                    newColumn = tabla.Columns.Add("peso");
                    newColumn.DataType = System.Type.GetType("System.Single");
                    newColumn.AllowDBNull = false;

                    foreach (INIKER.Item.ItemList carro in carros)
                    {
                        newRow = tabla.NewRow();
                        newRow["codigo"] = carro.No;
                        newRow["descripcion"] = carro.Search_Description;
                        newRow["peso"] = Single.Parse(carro.Net_Weight.ToString());
                        tabla.Rows.Add(newRow);
                    }

                    return (tabla);
                }
        
            #endregion

            #region RECHAZO

                // Rechazo desde formularios de rechazo

                public void Rechazar(string cliente, decimal cantidad, DateTime fecha, DateTime hora, string operario, string subfamilia, string turno)
                {
                    INIKER.FuncFabricacion.FuncFabricacion funciones =
                        new INIKER.FuncFabricacion.FuncFabricacion(_userLogin, _pwdLogin, _empresaLogin);
                    funciones.GenerarMovRechazo(cliente, cantidad, fecha, hora, operario, subfamilia, turno);
                }

                // 13/01/10 NUEVA IMPLEMENTACION RECHAZO

                public void RechazarPorTurno(string turno, string operario, decimal kilos, string tipoCarro)
                {
                    INIKER.FuncFabricacion.FuncFabricacion funciones =
                        new INIKER.FuncFabricacion.FuncFabricacion(_userLogin, _pwdLogin, _empresaLogin);
                    funciones.Rechazar(turno, operario, kilos,tipoCarro,DateTime.Today,DateTime.Now);                    
                }

                public void RechazarACostura(string cliente, string pedido, string producto, string nSerie, string operario, string turno, int cantidad)
                {
                    INIKER.FuncFabricacion.FuncFabricacion funciones =
                        new INIKER.FuncFabricacion.FuncFabricacion(_userLogin, _pwdLogin, _empresaLogin);
                        funciones.ACostura(cliente, pedido, producto, cantidad, turno, operario, nSerie,DateTime.Today,DateTime.Now);
                }

                public void DeOxidoACostura(string cliente, string pedido, string producto, string nSerie, string operario, string turno, int cantidad)
                {
                    INIKER.FuncFabricacion.FuncFabricacion funciones =
                        new INIKER.FuncFabricacion.FuncFabricacion(_userLogin, _pwdLogin, _empresaLogin);
                    funciones.DeOxidoACostura(cliente, pedido, producto, cantidad, turno, operario, nSerie, DateTime.Today, DateTime.Now);
                }

                public void RechazarAOxido(string cliente, string pedido, string producto, string nSerie, string operario, string turno, int cantidad)
                {
                    INIKER.FuncFabricacion.FuncFabricacion funciones =
                        new INIKER.FuncFabricacion.FuncFabricacion(_userLogin, _pwdLogin, _empresaLogin);
                    funciones.AOxido(cliente, pedido, producto, cantidad, turno, operario, nSerie, DateTime.Today, DateTime.Now);
                }
                
            #endregion

            #region RECEPCION

                public DataTable GetTransportistas()
                {
                    INIKER.Transportistas.ListaTransportistasINDUSAL_Service transportistas =
                        new INIKER.Transportistas.ListaTransportistasINDUSAL_Service(_userLogin, _pwdLogin, _empresaLogin);
                    INIKER.Transportistas.ListaTransportistasINDUSAL[] listaTransportistas = transportistas.ReadMultiple(";;" + _empresaLogin, 0, 0, "");
                    return (ArrayTransportistasToDataTable(listaTransportistas));
                }

                private DataTable ArrayTransportistasToDataTable(INIKER.Transportistas.ListaTransportistasINDUSAL[] transportistas)
                {
                    DataTable tabla = new DataTable("transportistas");
                    DataColumn newColumn;
                    DataRow newRow;

                    newColumn = tabla.Columns.Add("codigo");
                    newColumn.DataType = System.Type.GetType("System.String");
                    newColumn.AllowDBNull = false;

                    newColumn = tabla.Columns.Add("nombre");
                    newColumn.DataType = System.Type.GetType("System.String");
                    newColumn.AllowDBNull = false;


                    foreach (INIKER.Transportistas.ListaTransportistasINDUSAL transport in transportistas)
                    {
                        newRow = tabla.NewRow();
                        newRow["codigo"] = transport.Code;
                        newRow["nombre"] = transport.Name;                        
                        tabla.Rows.Add(newRow);
                    }

                    return (tabla);
                }

                public DataTable GetRutasTta(string sppAgent)
                {
                    //se declara una variable para componer la cadena de filtrado
                    // la asociación de la ruta debe ser de tipo transportista
                    string strFilter = INIKER.RutasTransportista.Tipo_relacion.Transportista.ToString();
                    // se añade el código de transportista para que devuelva las rutas que tiene asociadas
                    strFilter = strFilter + ";" + sppAgent;

                    INIKER.RutasTransportista.ListaRTransportistaINDUSAL_Service rutas =
                        new INIKER.RutasTransportista.ListaRTransportistaINDUSAL_Service(_userLogin, _pwdLogin, _empresaLogin);
                    INIKER.RutasTransportista.ListaRTransportistaINDUSAL[] listaRutasTransp = rutas.ReadMultiple(strFilter, 0, 0, "");
                    return (ArrayRutasToDataTable(listaRutasTransp));
                }

                private DataTable ArrayRutasToDataTable(INIKER.RutasTransportista.ListaRTransportistaINDUSAL[] rutas)
                {
                    DataTable tabla = new DataTable("rutas");
                    DataColumn newColumn;
                    DataRow newRow;

                    newColumn = tabla.Columns.Add("codigo");
                    newColumn.DataType = System.Type.GetType("System.String");
                    newColumn.AllowDBNull = false;

                    newColumn = tabla.Columns.Add("nombre");
                    newColumn.DataType = System.Type.GetType("System.String");
                    newColumn.AllowDBNull = false;
                    
                    foreach (INIKER.RutasTransportista.ListaRTransportistaINDUSAL ruta in rutas)
                    {
                        newRow = tabla.NewRow();
                        newRow["codigo"] = ruta.Cod_ruta;
                        newRow["nombre"] = ruta.Nombre_ruta;                        
                        tabla.Rows.Add(newRow);
                    }

                    return (tabla);

                }

                public DataTable GetClientes(string codRuta)
                {
                    //se declara una variable para componer la cadena de filtrado
                    // la asociación de la ruta debe ser de tipo transportista
                    string strFilter = INIKER.RutasTransportista.Tipo_relacion.Cliente.ToString();
                    // se añade el código de transportista para que devuelva las rutas que tiene asociadas
                    strFilter = strFilter + ";;" + codRuta;

                    INIKER.RutasTransportista.ListaRTransportistaINDUSAL_Service clientesRuta =
                        new INIKER.RutasTransportista.ListaRTransportistaINDUSAL_Service(_userLogin, _pwdLogin, _empresaLogin);
                    INIKER.RutasTransportista.ListaRTransportistaINDUSAL[] listaClientesTransp = clientesRuta.ReadMultiple(strFilter, 0, 0, "");
                    return (ArrayClientesRutaToDataTable(listaClientesTransp));
                }

                private DataTable ArrayClientesRutaToDataTable(INIKER.RutasTransportista.ListaRTransportistaINDUSAL[] clientes)
                {
                    DataTable tabla = new DataTable("clientes");
                    DataColumn newColumn;
                    DataRow newRow;

                    newColumn = tabla.Columns.Add("codigo");
                    newColumn.DataType = System.Type.GetType("System.String");
                    newColumn.AllowDBNull = false;

                    newColumn = tabla.Columns.Add("alias");
                    newColumn.DataType = System.Type.GetType("System.String");
                    newColumn.AllowDBNull = false;

                    foreach (INIKER.RutasTransportista.ListaRTransportistaINDUSAL cliente in clientes)
                    {
                        newRow = tabla.NewRow();
                        newRow["codigo"] = cliente.Cod_relacion;
                        newRow["alias"] = cliente.Nombre_relacion;                        
                        tabla.Rows.Add(newRow);
                    }

                    return (tabla);

                }

                public DataTable GetListaIncidencias()
                {                    
                    string strFilter = "INC_TRANSP";

                    INIKER.IncidenciasTransporte.ListaIncidTransport_Service incidencias =
                        new INIKER.IncidenciasTransporte.ListaIncidTransport_Service(_userLogin, _pwdLogin, _empresaLogin);
                    INIKER.IncidenciasTransporte.ListaIncidTransport[] listaIncidencias = incidencias.ReadMultiple(strFilter, 0, 0, "");
                    return (ArrayIncidTransporteToDataTable(listaIncidencias));
                }

                private DataTable ArrayIncidTransporteToDataTable(INIKER.IncidenciasTransporte.ListaIncidTransport[] incidencias)
                {
                    DataTable tabla = new DataTable("incidencias");
                    DataColumn newColumn;
                    DataRow newRow;

                    newColumn = tabla.Columns.Add("codigo");
                    newColumn.DataType = System.Type.GetType("System.String");
                    newColumn.AllowDBNull = false;

                    newColumn = tabla.Columns.Add("descripcion");
                    newColumn.DataType = System.Type.GetType("System.String");
                    newColumn.AllowDBNull = false;

                    foreach (INIKER.IncidenciasTransporte.ListaIncidTransport incidencia in incidencias)
                    {
                        newRow = tabla.NewRow();
                        newRow["codigo"] = incidencia.Code;
                        newRow["descripcion"] = incidencia.Description;
                        tabla.Rows.Add(newRow);
                    }

                    return (tabla);

                }
                
                public DataTable GetMovsRecepcion(string transportista, string ruta)
                {
                    INIKER.MovsProduccion.MovsProdSinRegINDUSAL_Service oMovs =
                        new INIKER.MovsProduccion.MovsProdSinRegINDUSAL_Service(_userLogin, _pwdLogin, _empresaLogin);
                    string DateFilter = DateTime.Today.Month.ToString() + "/" +
                                        DateTime.Today.Day.ToString() + "/" +
                                        DateTime.Today.Year.ToString();
                    string filterStr = ";RECEPCION;;;;" + transportista + ";" + ruta + ";" + DateFilter;
                    INIKER.MovsProduccion.MovsProdSinRegINDUSAL[] movsRecepcion =oMovs.ReadMultiple(filterStr, 0, 0, "");
                        
                    return (ArrayMovsRecepcionToDataTable(movsRecepcion));
                }

                private DataTable ArrayMovsRecepcionToDataTable(INIKER.MovsProduccion.MovsProdSinRegINDUSAL[] movimientos)
                {
                    DataTable tabla = new DataTable("movimientos");
                    DataColumn newColumn;
                    DataRow newRow;

                    newColumn = tabla.Columns.Add("MOVIMIENTO");
                    newColumn.DataType = System.Type.GetType("System.String");
                    newColumn.AllowDBNull = true;

                    newColumn = tabla.Columns.Add("FECHA");
                    newColumn.DataType = System.Type.GetType("System.String");
                    newColumn.AllowDBNull = true;

                    newColumn = tabla.Columns.Add("RUTA");
                    newColumn.DataType = System.Type.GetType("System.String");
                    newColumn.AllowDBNull = true;

                    newColumn = tabla.Columns.Add("CLIENTE");
                    newColumn.DataType = System.Type.GetType("System.String");
                    newColumn.AllowDBNull = true;

                    newColumn = tabla.Columns.Add("PEDIDO");
                    newColumn.DataType = System.Type.GetType("System.String");
                    newColumn.AllowDBNull = true;

                    foreach (INIKER.MovsProduccion.MovsProdSinRegINDUSAL movimiento in movimientos)
                    {
                        newRow = tabla.NewRow();
                        newRow["MOVIMIENTO"] = movimiento.Num_movimiento;
                        newRow["FECHA"] = movimiento.Fecha.ToShortDateString();
                        newRow["RUTA"] = movimiento.Nombre_ruta;
                        newRow["CLIENTE"] = movimiento.Alias_cliente;
                        newRow["PEDIDO"] = movimiento.Num_pedido;                        
                        tabla.Rows.Add(newRow);
                    }

                    return (tabla);

                }
            
            #endregion 

            #region WORK CENTERS (LAVADORAS - TUNELES - CALANDRAS - CANALES CONTEO)
        
                public DataTable GetWorkCenters(string workCenterGroup)
                {
                    INIKER.WorkCenter.WorkCenterList_Service workCenter =
                        new INIKER.WorkCenter.WorkCenterList_Service(_userLogin, _pwdLogin, _empresaLogin);
                    INIKER.WorkCenter.WorkCenterList[] workCenterList = workCenter.ReadMultiple(workCenterGroup, 0, 0, "");
                    return (ArrayWCenterToDataTable(workCenterList));
                }

                private DataTable ArrayWCenterToDataTable(INIKER.WorkCenter.WorkCenterList[] workCenters)
                {
                    DataTable tabla = new DataTable("maquinas");
                    DataColumn newColumn;
                    DataRow newRow;

                    newColumn = tabla.Columns.Add("codigo");
                    newColumn.DataType = System.Type.GetType("System.String");
                    newColumn.AllowDBNull = false;

                    newColumn = tabla.Columns.Add("descripcion");
                    newColumn.DataType = System.Type.GetType("System.String");
                    newColumn.AllowDBNull = false;

                    newColumn = tabla.Columns.Add("alias");
                    newColumn.DataType = System.Type.GetType("System.String");
                    newColumn.AllowDBNull = false;

                    newColumn = tabla.Columns.Add("pesoMaximo");
                    newColumn.DataType = System.Type.GetType("System.Decimal");
                    newColumn.AllowDBNull = true;

                    foreach (INIKER.WorkCenter.WorkCenterList workCenter in workCenters)
                    {
                        newRow = tabla.NewRow();
                        newRow["codigo"] = workCenter.No;
                        newRow["descripcion"] = workCenter.Name;
                        newRow["alias"] = workCenter.Search_Name;
                        newRow["pesoMaximo"] = workCenter.Peso_maximo;
                        tabla.Rows.Add(newRow);
                    }

                    return (tabla);
                }

            #endregion

            #region EXPEDICIONES

                public INIKER.Cliente.ListaClientesINDUSAL[] GetOpenOrderCustList()
                {
                    // obtiene una lista de clientes que tienen algún pedido abierto
                    string queryString = ";;TRUE";
                    INIKER.Cliente.ListaClientesINDUSAL[] clientes;
                    INIKER.Cliente.ListaClientesINDUSAL_Service oCustomer = new INIKER.Cliente.ListaClientesINDUSAL_Service(_userLogin, _pwdLogin, _empresaLogin);
                    clientes = oCustomer.ReadMultiple(queryString, 0, 0, "");
                    return (clientes);
                }

                public string GetLastOpenOrderNo(string custNo)
                {
                    INIKER.FuncFabricacion.FuncFabricacion oFunciones =
                        new INIKER.FuncFabricacion.FuncFabricacion(_userLogin, _pwdLogin, _empresaLogin);
                    
                    return(oFunciones.UltPedVentaAbiertoCte(custNo));
                }

                public INIKER.LineasVenta.ListaLineasVentaINDUSAL[] GetOutstandingLines(string nPedido)
                {
                    string queryString = ";" + nPedido;
                    INIKER.LineasVenta.ListaLineasVentaINDUSAL[] lineas;
                    INIKER.LineasVenta.ListaLineasVentaINDUSAL_Service oVentas =
                        new INIKER.LineasVenta.ListaLineasVentaINDUSAL_Service(_userLogin, _pwdLogin, _empresaLogin);
                    lineas = oVentas.ReadMultiple(queryString, 0, 0, "");
                    return (lineas);
                }

                public void UpdateOutstandingLines(INIKER.LineasVenta.ListaLineasVentaINDUSAL[] lineas)
                {
                    INIKER.LineasVenta.ListaLineasVentaINDUSAL_Service salesLine =
                        new INIKER.LineasVenta.ListaLineasVentaINDUSAL_Service(_userLogin, _pwdLogin, _empresaLogin);
                    INIKER.LineasVenta.ListaLineasVentaINDUSAL auxLinea;

                    foreach (INIKER.LineasVenta.ListaLineasVentaINDUSAL oLinea in lineas)
                    {
                        auxLinea = salesLine.Read(INIKER.LineasVenta.Document_Type.Order.ToString(),
                                                oLinea.Document_No, oLinea.Line_No);
                        if (auxLinea != null)
                        {
                            auxLinea.Quantity = oLinea.Quantity;
                            salesLine.Update(ref auxLinea);
                        }
                    }
                }
        
            #endregion

            #region CLIENTES

                public DataTable GetCustomers()
                {
                    INIKER.Cliente.ListaClientesINDUSAL[] listaClientes;
                    INIKER.Cliente.ListaClientesINDUSAL_Service oCustomer = new INIKER.Cliente.ListaClientesINDUSAL_Service(_userLogin, _pwdLogin, _empresaLogin);
                    listaClientes = oCustomer.ReadMultiple("", 0, 0, "");

                    return (ArrayClientesToDataTable(listaClientes));
                }

                public string GetCustomerData(string customerNo_)
                {
                    INIKER.Cliente.ListaClientesINDUSAL[] listaClientes;
                    INIKER.Cliente.ListaClientesINDUSAL_Service oCustomer = new INIKER.Cliente.ListaClientesINDUSAL_Service(_userLogin, _pwdLogin, _empresaLogin);
                    listaClientes = oCustomer.ReadMultiple(";" + customerNo_, 0, 0, "");
                    if (listaClientes.Count() > 0)
                    {
                        return (listaClientes[0].Search_Name + ";" + listaClientes[0].Location_Code);
                    }
                    else
                    {
                        return ("");
                    }
                }

                private DataTable ArrayClientesToDataTable(INIKER.Cliente.ListaClientesINDUSAL[] clientes)
                {
                    DataTable tabla = new DataTable("clientes");
                    DataColumn newColumn;
                    DataRow newRow;

                    newColumn = tabla.Columns.Add("codigo");
                    newColumn.DataType = System.Type.GetType("System.String");
                    newColumn.AllowDBNull = false;

                    newColumn = tabla.Columns.Add("nombre");
                    newColumn.DataType = System.Type.GetType("System.String");
                    newColumn.AllowDBNull = true;

                    newColumn = tabla.Columns.Add("nombre2");
                    newColumn.DataType = System.Type.GetType("System.String");
                    newColumn.AllowDBNull = true;

                    newColumn = tabla.Columns.Add("alias");
                    newColumn.DataType = System.Type.GetType("System.String");
                    newColumn.AllowDBNull = true;

                    newColumn = tabla.Columns.Add("almacen");
                    newColumn.DataType = System.Type.GetType("System.String");
                    newColumn.AllowDBNull = true;

                    foreach (INIKER.Cliente.ListaClientesINDUSAL cliente in clientes)
                    {
                        newRow = tabla.NewRow();
                        newRow["codigo"] = cliente.No;
                        newRow["nombre"] = cliente.Name;
                        newRow["nombre2"] = cliente.Name_2;
                        newRow["alias"] = cliente.Search_Name;
                        newRow["almacen"] = cliente.Location_Code;
                        tabla.Rows.Add(newRow);
                    }

                    return (tabla);

                }

                public string GetInternalCustomer(eTipoCliente tipoCliente)
                {
                    INIKER.FuncFabricacion.FuncFabricacion oProduccion =
                        new INIKER.FuncFabricacion.FuncFabricacion(_userLogin, _pwdLogin, _empresaLogin);
                    return(oProduccion.GetInternalCustomer(tipoCliente.ToString()));                    
                }

            #endregion

            #region ITEMS

                public string GetItemDescription(string itemNo_)
                {
                    INIKER.Item.ItemList[] listaItems;
                    INIKER.Item.ItemList_Service oItem = new INIKER.Item.ItemList_Service(_userLogin, _pwdLogin, _empresaLogin);
                    listaItems = oItem.ReadMultiple(itemNo_, 0, 0, "");
                    if (listaItems.Count() > 0)
                    {
                        return (listaItems[0].Description);
                    }
                    else
                    {
                        return ("");
                    }
                }

                public int GetItemPaqQty(string itemNo_)
                {
                    INIKER.Item.ItemList[] listaItems;
                    INIKER.Item.ItemList_Service oItem = new INIKER.Item.ItemList_Service(_userLogin, _pwdLogin, _empresaLogin);
                    listaItems = oItem.ReadMultiple(itemNo_, 0, 0, "");
                    if (listaItems.Count() > 0)
                    {
                        return (listaItems[0].Cantidad_por_paquete);
                    }
                    else
                    {
                        return (0);
                    }
                }

                public int GetItemLocationInventory(string itemNo_, string LocCode_)
                {
                    int inventario;
                    INIKER.FuncFabricacion.FuncFabricacion oFab = new INIKER.FuncFabricacion.FuncFabricacion(_userLogin, _pwdLogin, _empresaLogin);
                    inventario = (int) oFab.InventProdAlmacen(itemNo_, LocCode_);
                    return (inventario);
                }

                public DataTable GetItemList()
                {
                    INIKER.Item.ItemList[] listaItems;
                    INIKER.Item.ItemList_Service oItem = new INIKER.Item.ItemList_Service(_userLogin, _pwdLogin, _empresaLogin);
                    listaItems = oItem.ReadMultiple("", 0, 0,"Description");
                    return (ArrayItemsToDataTable(listaItems));
                }

                private DataTable ArrayItemsToDataTable(INIKER.Item.ItemList[] productos)
                {
                    DataTable tabla = new DataTable("productos");
                    DataColumn newColumn;
                    DataRow newRow;

                    newColumn = tabla.Columns.Add("codigo");
                    newColumn.DataType = System.Type.GetType("System.String");
                    newColumn.AllowDBNull = false;

                    newColumn = tabla.Columns.Add("descripcion");
                    newColumn.DataType = System.Type.GetType("System.String");
                    newColumn.AllowDBNull = true;

                    newColumn = tabla.Columns.Add("alias");
                    newColumn.DataType = System.Type.GetType("System.String");
                    newColumn.AllowDBNull = true;
                                        
                    foreach (INIKER.Item.ItemList producto in productos)
                    {
                        newRow = tabla.NewRow();
                        newRow["codigo"] = producto.No;
                        newRow["descripcion"] = producto.Description;
                        newRow["alias"] = producto.Search_Description;                        
                        tabla.Rows.Add(newRow);
                    }

                    
                    return (tabla);

                }
        
            #endregion

            #region PEDIDOS

                /// <summary>
                /// Función que obtiene una lista de pedidos abiertos del cliente en el sistema
                /// </summary>
                /// <param name="cliente">Código de cliente del que se filtrarán los pedidos</param>
                /// <returns>DataTable con el número y la fecha de entrega de cada pedido el cliente</returns>
                /// 
                public DataTable GetCustomerOrders(string cliente)
                {
                    INIKER.PedidosAbiertos.ListaPedidosAbiertosINDUSAL[] listaPedidos;
                    INIKER.PedidosAbiertos.ListaPedidosAbiertosINDUSAL_Service oOrder = new INIKER.PedidosAbiertos.ListaPedidosAbiertosINDUSAL_Service(_userLogin, _pwdLogin, _empresaLogin);
                    listaPedidos = oOrder.ReadMultiple(cliente , 0, 0, "");

                    return (ArrayPedidosToDataTable(listaPedidos));
                }

                public DataTable GetCustomerOrders(string cliente,bool enPreparacion)
                {
                    INIKER.PedidosAbiertos.ListaPedidosAbiertosINDUSAL[] listaPedidos;
                    INIKER.PedidosAbiertos.ListaPedidosAbiertosINDUSAL_Service oOrder = new INIKER.PedidosAbiertos.ListaPedidosAbiertosINDUSAL_Service(_userLogin, _pwdLogin, _empresaLogin);
                    listaPedidos = oOrder.ReadMultiple(cliente+";;;"+enPreparacion.ToString(), 0, 0, "");

                    return (ArrayPedidosToDataTable(listaPedidos));
                }

                /// <summary>
                /// Función que convierte una lista de pedidos en un DataTable
                /// </summary>
                /// <param name="pedidos">Lista de pedidos</param>
                /// <returns>DataTable con el número y la fecha de entrega de cada pedido</returns>
                private DataTable ArrayPedidosToDataTable(INIKER.PedidosAbiertos.ListaPedidosAbiertosINDUSAL[] pedidos)
                {
                    DataTable tabla = new DataTable("pedidos");
                    DataColumn newColumn;
                    DataRow newRow;                    

                    newColumn = tabla.Columns.Add("Linea");
                    newColumn.DataType = System.Type.GetType("System.Int16");
                    newColumn.AutoIncrement = true;
                    newColumn.AutoIncrementSeed = 0;
                    newColumn.AutoIncrementStep = 1;                    

                    newColumn = tabla.Columns.Add("Numero");
                    newColumn.DataType = System.Type.GetType("System.String");
                    newColumn.AllowDBNull = false;

                    newColumn = tabla.Columns.Add("Fecha");
                    newColumn.DataType = System.Type.GetType("System.String");
                    newColumn.AllowDBNull = false;

                    foreach (INIKER.PedidosAbiertos.ListaPedidosAbiertosINDUSAL pedido in pedidos)
                    {
                        newRow = tabla.NewRow();
                        newRow["Numero"] = pedido.No;
                        newRow["Fecha"] = pedido.Posting_Date;                        
                        tabla.Rows.Add(newRow);
                    }

                    return (tabla);

                }


            #endregion

            #region EMPAQUETADO

                public DataTable GetMovsEmpaquetado(string tipoMov,string customer,string order)
                {
                    INIKER.MovsProduccion.MovsProdSinRegINDUSAL_Service oMovs =
                        new INIKER.MovsProduccion.MovsProdSinRegINDUSAL_Service(_userLogin, _pwdLogin, _empresaLogin);
                    INIKER.MovsProduccion.MovsProdSinRegINDUSAL[] movsEmpaquetado =oMovs.ReadMultiple(";EMPAQUETADO|EXPEDICION;" + customer + ";" + order, 0, 0, "");
                    //INIKER.MovsProduccion.MovsProdSinRegINDUSAL[] movsEmpaquetado = oMovs.ReadMultiple(";" + tipoMov + ";" + customer + ";" + order, 0, 0, "");
                    return (ArrayMovsEmpaquetadoToDataTable(movsEmpaquetado));
                }

                private DataTable ArrayMovsEmpaquetadoToDataTable(INIKER.MovsProduccion.MovsProdSinRegINDUSAL[] movimientos)
                {
                    DataTable tabla = new DataTable("movimientos");
                    DataColumn newColumn;
                    DataRow newRow;

                    newColumn = tabla.Columns.Add("movimiento");
                    newColumn.DataType = System.Type.GetType("System.String");
                    newColumn.AllowDBNull = true;

                    newColumn = tabla.Columns.Add("movExpedicion");
                    newColumn.DataType = System.Type.GetType("System.Boolean");
                    newColumn.AllowDBNull = true;

                    newColumn = tabla.Columns.Add("turno");
                    newColumn.DataType = System.Type.GetType("System.String");
                    newColumn.AllowDBNull = true;

                    newColumn = tabla.Columns.Add("empleado");
                    newColumn.DataType = System.Type.GetType("System.String");
                    newColumn.AllowDBNull = true;

                    newColumn = tabla.Columns.Add("producto");
                    newColumn.DataType = System.Type.GetType("System.String");
                    newColumn.AllowDBNull = true;

                    newColumn = tabla.Columns.Add("nCarro");
                    newColumn.DataType = System.Type.GetType("System.Int16");
                    newColumn.AllowDBNull = true;

                    newColumn = tabla.Columns.Add("tipoCarro");
                    newColumn.DataType = System.Type.GetType("System.String");
                    newColumn.AllowDBNull = true;

                    newColumn = tabla.Columns.Add("peso");
                    newColumn.DataType = System.Type.GetType("System.Decimal");
                    newColumn.AllowDBNull = true;

                    newColumn = tabla.Columns.Add("completo");
                    newColumn.DataType = System.Type.GetType("System.Boolean");
                    newColumn.AllowDBNull = true;

                    foreach (INIKER.MovsProduccion.MovsProdSinRegINDUSAL movimiento in movimientos)
                    {
                        newRow = tabla.NewRow();
                        newRow["movimiento"] = movimiento.Num_movimiento;
                        newRow["movExpedicion"] = (movimiento.Tipo_Movimiento == INIKER.MovsProduccion.Tipo_Movimiento.EXPEDICION);
                        newRow["turno"] = movimiento.Turno;
                        newRow["empleado"] = movimiento.Operario;
                        newRow["producto"] = movimiento.Producto;
                        newRow["nCarro"] = movimiento.Num_carro;
                        newRow["tipoCarro"] = movimiento.Cod_carro;
                        newRow["peso"] = movimiento.Cantidad_enviar;
                        newRow["completo"] = movimiento.Carro_completo;
                        tabla.Rows.Add(newRow);
                    }

                    return (tabla);

                }

                public void EliminarLineasPesoPedVenta(string _nPedVenta)
                {
                    INIKER.FuncFabricacion.FuncFabricacion fabricacion =
                    new INIKER.FuncFabricacion.FuncFabricacion(_userLogin, _pwdLogin, _empresaLogin);
                    fabricacion.EliminarLineasKilos(_nPedVenta);
                }

                public void GenerarLineasPesoPedVenta(string _nPedVenta)
                {
                    INIKER.FuncFabricacion.FuncFabricacion fabricacion =
                    new INIKER.FuncFabricacion.FuncFabricacion(_userLogin, _pwdLogin, _empresaLogin);
                    fabricacion.GenerarLineasPesoPV(_nPedVenta);
                }

            #endregion

            #region ETIQUETAS

                private Exception RegistrarEtiqueta(string idEquipo, string codCliente, string nomCliente, string codProducto,
                    string desProducto, string numPedido, int cantidad, decimal nCarro,string tipoEtiqueta)
                {
                    try
                    {
                        INIKER.FuncFabricacion.FuncFabricacion oProduccion =
                        new INIKER.FuncFabricacion.FuncFabricacion(_userLogin, _pwdLogin, _empresaLogin);
                        oProduccion.InsertarRegistroEtiqueta(codCliente, nomCliente, codProducto, desProducto, numPedido,
                            nCarro, cantidad, tipoEtiqueta, idEquipo);
                        return (null);
                    }
                    catch (Exception ex)
                    {
                        return (ex);
                    }
                }

                public Exception RegistrarEtiquetaPaqueteProducto(string idEquipo,string codCliente, string nomCliente,
                        string codProducto, string desProducto, string numPedido, int cantidad, string tipoEtiqueta)
                {
                    return(RegistrarEtiqueta(idEquipo,codCliente,nomCliente,codProducto,desProducto,
                            numPedido,cantidad,0,tipoEtiqueta));
                }

                public Exception RegistrarEtiquetaCarroTransportista(string idEquipo, string codCliente, string nomCliente,
                        int cantidad, string tipoEtiqueta)
                {
                    return(RegistrarEtiqueta(idEquipo,codCliente,nomCliente,"","",
                            "",cantidad,0,tipoEtiqueta));
                }

                public Exception RegistrarEtiquetaCarroLavado(string idEquipo,string codCliente, string nomCliente,
                        string numPedido, int cantidad, string tipoEtiqueta)
                {
                    return(RegistrarEtiqueta(idEquipo,codCliente,nomCliente,"","",
                            numPedido,cantidad,0,tipoEtiqueta));
                }

                public Exception RegistrarEtiquetaCarroOxido(string idEquipo,string codCliente, int cantidad, string tipoEtiqueta)
                {
                    return(RegistrarEtiqueta(idEquipo,codCliente,"","","","",cantidad,0,tipoEtiqueta));
                }

                public Exception RegistrarEtiquetaCarroIncompleto(string idEquipo,string codCliente, string nomCliente,
                        string numPedido, int cantidad,int nCarro, string tipoEtiqueta)
                {
                    return(RegistrarEtiqueta(idEquipo,codCliente,nomCliente,"","",
                            numPedido,cantidad,nCarro,tipoEtiqueta));
                }



            #endregion

            #region EMPRESAS

                public DataTable GetCompanyList()
                {
                    INIKER.ListaEmpresas.ListaEmpresas[] listaEmpresas;
                    INIKER.ListaEmpresas.ListaEmpresas_Service oCompanyList = new INIKER.ListaEmpresas.ListaEmpresas_Service(_userLogin, _pwdLogin, _empresaLogin);
                    listaEmpresas = oCompanyList.ReadMultiple("", 0, 0, "");
                    
                    return(ArrayEmpresasToDataTable(listaEmpresas));                    
                }

                private DataTable ArrayEmpresasToDataTable(INIKER.ListaEmpresas.ListaEmpresas[] listaEmpresas)
                {
                    DataTable tabla = new DataTable("empresas");
                    DataColumn newColumn;
                    DataRow newRow;

                    newColumn = tabla.Columns.Add("nombre");
                    newColumn.DataType = System.Type.GetType("System.String");
                    newColumn.AllowDBNull = false;

                    newColumn = tabla.Columns.Add("etiquetas");
                    newColumn.DataType = System.Type.GetType("System.String");
                    newColumn.AllowDBNull = true;


                    foreach (INIKER.ListaEmpresas.ListaEmpresas empresa in listaEmpresas)
                    {
                        newRow = tabla.NewRow();
                        newRow["nombre"] = empresa.Name;
                        newRow["etiquetas"] = empresa.DimensionesEtiqueta;
                        tabla.Rows.Add(newRow);
                    }

                    return (tabla);

                }
            #endregion

            #region TRAPOS

                public DataTable GetTrapos(string familia, string subfamilia)
                {
                    cProductos oProductos = new cProductos();
                    INIKER.Item.ItemList[] trapos = oProductos.GetSurtidoSubfamilia(familia, subfamilia,_empresaLogin);
                    return (ArrayTraposToDataTable(trapos));
                }

                private DataTable ArrayTraposToDataTable(INIKER.Item.ItemList[] trapos)
                {
                    DataTable tabla = new DataTable("trapos");
                    DataColumn newColumn;
                    DataRow newRow;

                    newColumn = tabla.Columns.Add("codigo");
                    newColumn.DataType = System.Type.GetType("System.String");
                    newColumn.AllowDBNull = false;

                    newColumn = tabla.Columns.Add("descripcion");
                    newColumn.DataType = System.Type.GetType("System.String");
                    newColumn.AllowDBNull = false;                    

                    foreach (INIKER.Item.ItemList trapo in trapos)
                    {
                        newRow = tabla.NewRow();
                        newRow["codigo"] = trapo.No;
                        newRow["descripcion"] = trapo.Search_Description;                        
                        tabla.Rows.Add(newRow);
                    }

                    return (tabla);
                }

            #endregion

            #region NUMEROS SERIE

                public DataTable GetCustomerSerialNumberInfo(string customerNo_, string almacenOrigen, string serialNo_)
                {
                    INIKER.NumerosSerie.InformacionNumSerie_Service seriales =
                        new INIKER.NumerosSerie.InformacionNumSerie_Service(_userLogin, _pwdLogin, _empresaLogin);
                    INIKER.NumerosSerie.InformacionNumSerie[] listaNSerie = seriales.ReadMultiple(customerNo_ + ";" + serialNo_, 0, 0, "");
                    return (ArraySerialesToDataTable(listaNSerie, almacenOrigen));
                }

                public DataTable GetCustomerSerialNumbers(string customerNo_, string almacenOrigen)
                {
                    INIKER.NumerosSerie.InformacionNumSerie_Service seriales =
                        new INIKER.NumerosSerie.InformacionNumSerie_Service(_userLogin, _pwdLogin, _empresaLogin);
                    INIKER.NumerosSerie.InformacionNumSerie[] listaNSerie = seriales.ReadMultiple(customerNo_, 0, 0, "");
                    return (ArraySerialesToDataTable(listaNSerie,almacenOrigen));
                }

                public DataTable GetCustomerSerialNumbers(string orderNo_)
                {
                    INIKER.NumerosSerieProduccion.InfoNumSerieLeidosEntrada_Service seriales =
                        new INIKER.NumerosSerieProduccion.InfoNumSerieLeidosEntrada_Service(_userLogin, _pwdLogin, _empresaLogin);
                    INIKER.NumerosSerieProduccion.InfoNumSerieLeidosEntrada[] listaNSerie = seriales.ReadMultiple(orderNo_, 0, 0, "");
                    return (ArraySerialesToDataTable(listaNSerie));
                }

                private DataTable ArraySerialesToDataTable(INIKER.NumerosSerie.InformacionNumSerie[] seriales, string almacenOrigen)
                {
                    DataTable tabla = new DataTable("seriales");
                    DataColumn newColumn;
                    DataRow newRow;

                    newColumn = tabla.Columns.Add("Cod_Cliente");
                    newColumn.DataType = System.Type.GetType("System.String");
                    newColumn.AllowDBNull = false;

                    newColumn = tabla.Columns.Add("Alias_Cliente");
                    newColumn.DataType = System.Type.GetType("System.String");
                    newColumn.AllowDBNull = false;
                    
                    newColumn = tabla.Columns.Add("Item_No");
                    newColumn.DataType = System.Type.GetType("System.String");
                    newColumn.AllowDBNull = false;

                    newColumn = tabla.Columns.Add("Descripcion_Planta");
                    newColumn.DataType = System.Type.GetType("System.String");
                    newColumn.AllowDBNull = false;

                    newColumn = tabla.Columns.Add("Serial_No");
                    newColumn.DataType = System.Type.GetType("System.String");
                    newColumn.AllowDBNull = false;

                    newColumn = tabla.Columns.Add("Description");
                    newColumn.DataType = System.Type.GetType("System.String");
                    newColumn.AllowDBNull = false;

                    foreach (INIKER.NumerosSerie.InformacionNumSerie serial in seriales)
                    {
                        if (serial.Almacen_con_stock == almacenOrigen)
                        {
                            newRow = tabla.NewRow();
                            newRow["Cod_Cliente"] = serial.Cod_Cliente;
                            newRow["Alias_Cliente"] = serial.Alias_Cliente;
                            newRow["Item_No"] = serial.Item_No;
                            newRow["Descripcion_Planta"] = serial.Descripcion_Planta;
                            newRow["Serial_No"] = serial.Serial_No;
                            newRow["Description"] = serial.Description;
                            tabla.Rows.Add(newRow);
                        }
                    }

                    return (tabla);
                }

                private DataTable ArraySerialesToDataTable(INIKER.NumerosSerieProduccion.InfoNumSerieLeidosEntrada[] seriales)
                {
                    DataTable tabla = new DataTable("seriales");
                    DataColumn newColumn;
                    DataRow newRow;

                    newColumn = tabla.Columns.Add("Cod_Cliente");
                    newColumn.DataType = System.Type.GetType("System.String");
                    newColumn.AllowDBNull = false;

                    newColumn = tabla.Columns.Add("Alias_Cliente");
                    newColumn.DataType = System.Type.GetType("System.String");
                    newColumn.AllowDBNull = false;

                    newColumn = tabla.Columns.Add("Item_No");
                    newColumn.DataType = System.Type.GetType("System.String");
                    newColumn.AllowDBNull = false;

                    newColumn = tabla.Columns.Add("Descripcion_Planta");
                    newColumn.DataType = System.Type.GetType("System.String");
                    newColumn.AllowDBNull = false;

                    newColumn = tabla.Columns.Add("Serial_No");
                    newColumn.DataType = System.Type.GetType("System.String");
                    newColumn.AllowDBNull = false;

                    newColumn = tabla.Columns.Add("Description");
                    newColumn.DataType = System.Type.GetType("System.String");
                    newColumn.AllowDBNull = false;

                    newColumn = tabla.Columns.Add("Enviado");
                    newColumn.DataType = System.Type.GetType("System.Decimal");
                    newColumn.AllowDBNull = false;

                    foreach (INIKER.NumerosSerieProduccion.InfoNumSerieLeidosEntrada serial in seriales)
                    {                        
                        newRow = tabla.NewRow();
                        newRow["Cod_Cliente"] = serial.Cod_Cliente;
                        newRow["Alias_Cliente"] = serial.Alias_cliente;
                        newRow["Item_No"] = serial.Producto;
                        newRow["Descripcion_Planta"] = serial.Descripcion_Planta;
                        newRow["Serial_No"] = serial.Num_serie;
                        newRow["Description"] = serial.Descripcion_num_serie;
                        newRow["Enviado"] = serial.Cantidad_enviada;
                        tabla.Rows.Add(newRow);
                    }

                    return (tabla);
                }

            #endregion
        
        #endregion
    }
}

