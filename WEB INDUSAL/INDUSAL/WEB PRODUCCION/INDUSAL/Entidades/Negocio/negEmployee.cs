using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace INIKER.Employee
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Web.Services;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.Net;

    /// <remarks/>
    public partial class EmployeeList_Service : System.Web.Services.Protocols.SoapHttpClientProtocol
    {
        public const char ParCaseSensitive = '@';
        private int IdTabla = 18;
        INIKER.Funciones.Funciones_WS Funciones = new INIKER.Funciones.Funciones_WS();

        //List<AT_Solicitudes> ??
        [System.ComponentModel.Description("Devuelve las Categorias de Producto"), System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select, true)]
        public EmployeeList[] ReadMultiple(string Where, int pageSize, int numPage, string Sort)
        {
            bool flagResp = false;
            string lastReadKey = null;
            INIKER.Employee.EmployeeList_Filter[] filtros = null;            
            //Array.Resize(ref misFiltros, 0);
            EmployeeList[] consultaPage; // = new AT_Solicitudes();
            flagResp = AplicarFiltros(Where, ref filtros);
            consultaPage = null;
            try
            {
                if (numPage > 0)
                {
                    //En numPage viene el numPage * pageSize
                    numPage = numPage / pageSize;
                }
                for (int i = 0; i <= numPage; i++)
                {
                    consultaPage = null;
                    consultaPage = ReadMultiple(filtros, lastReadKey, pageSize);
                    if (numPage != 0)
                    {
                        if (numPage == i)
                        {
                            //Estamos en la última página
                        }
                        else
                        {
                            lastReadKey = consultaPage[pageSize - 1].Key;
                        }
                    }
                }
                if ((consultaPage != null))
                {

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }
            return consultaPage;
        }

        #region "Funciones Privadas"
        private bool AplicarFiltros(string Where, ref INIKER.Employee.EmployeeList_Filter[] misFiltros)
        {
            //Where: Lo vamos a hacer pasando un string separado por ';'
            //0: No
            //1: Name
            //2: 
            //3: 
            //4: 
            //5: 
            //6: 
            //7: 
            //8: 
            //9: 
            //10: 

            bool flagOK = true;
            try
            {
                INIKER.Employee.EmployeeList_Filter miFiltro;
                Array.Resize(ref misFiltros, 0);

                if ((Where != null) && Where.Length > 0)
                {
                    string[] strFiltros = null;
                    strFiltros = Where.Split(';');

                    //0: No.
                    if ((strFiltros[0] != null) && strFiltros[0] != string.Empty)
                    {
                        miFiltro = new INIKER.Employee.EmployeeList_Filter();
                        miFiltro.Field = INIKER.Employee.EmployeeList_Fields.No;
                        miFiltro.Criteria = strFiltros[0];
                        Array.Resize(ref misFiltros, misFiltros.Length + 1);
                        misFiltros[misFiltros.Length - 1] = miFiltro;
                    }
                    ////1: Item_Category_Code
                    //if (strFiltros.Length > 1 && (strFiltros[1] != null) && strFiltros[1] != string.Empty)
                    //{
                    //    miFiltro = new INIKER.Item.ItemList_Filter();
                    //    miFiltro.Field = INIKER.Item.ItemList_Fields.Item_Category_Code;
                    //    miFiltro.Criteria = ParCaseSensitive + strFiltros[1];
                    //    Array.Resize(ref misFiltros, misFiltros.Length + 1);
                    //    misFiltros[misFiltros.Length - 1] = miFiltro;
                    //}
                    ////2: Dia_semana
                    //if (strFiltros.Length > 2 && (strFiltros[2] != null) && strFiltros[2] != string.Empty)
                    //{
                    //    miFiltro = new INIKER.RutasTransporte.ListaRTransporteINDUSAL_Filter();
                    //    miFiltro.Field = INIKER.RutasTransporte.ListaRTransporteINDUSAL_Fields.Dia_semana;
                    //    miFiltro.Criteria = ParCaseSensitive + strFiltros[2];
                    //    Array.Resize(ref misFiltros, misFiltros.Length + 1);
                    //    misFiltros[misFiltros.Length - 1] = miFiltro;
                    //}
                    ////3: City
                    //if (strFiltros.Length > 3 && (strFiltros[3] != null) && strFiltros[3] != string.Empty)
                    //{
                    //    miFiltro = new INIKER.ATCliente.AT_Cliente_Filter();
                    //    miFiltro.Field = INIKER.ATCliente.AT_Cliente_Fields.City;
                    //    miFiltro.Criteria = ParCaseSensitive + strFiltros[3];
                    //    Array.Resize(ref misFiltros, misFiltros.Length + 1);
                    //    misFiltros[misFiltros.Length - 1] = miFiltro;
                    //}
                    ////4: Country_Region_Code
                    //if (strFiltros.Length > 4 && (strFiltros[4] != null) && strFiltros[4] != string.Empty)
                    //{
                    //    miFiltro = new INIKER.ATCliente.AT_Cliente_Filter();
                    //    miFiltro.Field = INIKER.ATCliente.AT_Cliente_Fields.Country_Region_Code;
                    //    miFiltro.Criteria = ParCaseSensitive + strFiltros[4];
                    //    Array.Resize(ref misFiltros, misFiltros.Length + 1);
                    //    misFiltros[misFiltros.Length - 1] = miFiltro;
                    //}
                    ////5: Pais
                    //if (strFiltros.Length > 5 && (strFiltros[5] != null) && strFiltros[5] != string.Empty)
                    //{
                    //    miFiltro = new INIKER.ATCliente.AT_Cliente_Filter();
                    //    miFiltro.Field = INIKER.ATCliente.AT_Cliente_Fields.Pais;
                    //    miFiltro.Criteria = ParCaseSensitive + strFiltros[5];
                    //    Array.Resize(ref misFiltros, misFiltros.Length + 1);
                    //    misFiltros[misFiltros.Length - 1] = miFiltro;
                    //}
                    ////6: Fecha_Solicitud
                    //if (strFiltros.Length > 6 && (strFiltros[6] != null) && strFiltros[6] != string.Empty)
                    //{
                    //    miFiltro = new INIKER.ATSolicitudes.AT_Solicitudes_Filter();
                    //    miFiltro.Field = INIKER.ATSolicitudes.AT_Solicitudes_Fields.Fecha_Solicitud;
                    //    miFiltro.Criteria = strFiltros[6];
                    //    Array.Resize(ref misFiltros, misFiltros.Length + 1);
                    //    misFiltros[misFiltros.Length - 1] = miFiltro;
                    //}
                    ////7: Cliente_Solicitud
                    //if (strFiltros.Length > 7 && (strFiltros[7] != null) && strFiltros[7] != string.Empty)
                    //{
                    //    miFiltro = new INIKER.ATSolicitudes.AT_Solicitudes_Filter();
                    //    miFiltro.Field = INIKER.ATSolicitudes.AT_Solicitudes_Fields.Cliente_Solicitud;
                    //    miFiltro.Criteria = ParCaseSensitive + strFiltros[7];
                    //    Array.Resize(ref misFiltros, misFiltros.Length + 1);
                    //    misFiltros[misFiltros.Length - 1] = miFiltro;
                    //}
                    ////8: Pais_Cliente
                    //if (strFiltros.Length > 8 && (strFiltros[8] != null) && strFiltros[8] != string.Empty)
                    //{
                    //    miFiltro = new INIKER.ATSolicitudes.AT_Solicitudes_Filter();
                    //    miFiltro.Field = INIKER.ATSolicitudes.AT_Solicitudes_Fields.Pais_Cliente;
                    //    miFiltro.Criteria = ParCaseSensitive + strFiltros[8];
                    //    Array.Resize(ref misFiltros, misFiltros.Length + 1);
                    //    misFiltros[misFiltros.Length - 1] = miFiltro;
                    //}
                    ////9: Cliente_Final
                    //if (strFiltros.Length > 9 && (strFiltros[9] != null) && strFiltros[9] != string.Empty)
                    //{
                    //    miFiltro = new INIKER.ATSolicitudes.AT_Solicitudes_Filter();
                    //    miFiltro.Field = INIKER.ATSolicitudes.AT_Solicitudes_Fields.Cod_Cliente_Final; //Cliente_Final?
                    //    miFiltro.Criteria = ParCaseSensitive + strFiltros[9];
                    //    Array.Resize(ref misFiltros, misFiltros.Length + 1);
                    //    misFiltros[misFiltros.Length - 1] = miFiltro;
                    //}
                    ////10: Solicitado_Por
                    //if (strFiltros.Length > 10 && (strFiltros[10] != null) && strFiltros[10] != string.Empty)
                    //{
                    //    miFiltro = new INIKER.ATSolicitudes.AT_Solicitudes_Filter();
                    //    miFiltro.Field = INIKER.ATSolicitudes.AT_Solicitudes_Fields.Solicitado_Por;
                    //    miFiltro.Criteria = ParCaseSensitive + strFiltros[10];
                    //    Array.Resize(ref misFiltros, misFiltros.Length + 1);
                    //    misFiltros[misFiltros.Length - 1] = miFiltro;
                    //}
                    ////11: Usuario_Tarea
                    //if (strFiltros.Length > 11 && (strFiltros[11] != null) && strFiltros[11] != string.Empty)
                    //{
                    //    miFiltro = new INIKER.ATSolicitudes.AT_Solicitudes_Filter();
                    //    miFiltro.Field = INIKER.ATSolicitudes.AT_Solicitudes_Fields.Usuario_Tarea;
                    //    miFiltro.Criteria = ParCaseSensitive + strFiltros[11];
                    //    Array.Resize(ref misFiltros, misFiltros.Length + 1);
                    //    misFiltros[misFiltros.Length - 1] = miFiltro;
                    //}

                }
            }
            catch (Exception ex)
            {
                flagOK = false;
                throw ex;
            }
            finally
            {
            }
            return flagOK;
        }

        public int GetRecCount(string Where, int pageSize, int numPage, string Sort)
        {
            //Añadir la lista de filtros para el cliente
            //0: No
            //1: Name
            //2: 
            //3: 
            //4: 
            //5: 
            //6: 
            //7: 
            //8: 
            //9: 
            //10: 

            string[] strFiltros = null;
            strFiltros = Where.Split(';');
            //return 0;
            return Funciones.GetRecCount(IdTabla,
                 strFiltros[0],
                 (strFiltros[1] != string.Empty ? ParCaseSensitive + strFiltros[1] : string.Empty),
                 (strFiltros[2] != string.Empty ? ParCaseSensitive + strFiltros[2] : string.Empty),
                 (strFiltros[3] != string.Empty ? ParCaseSensitive + strFiltros[3] : string.Empty),
                 (strFiltros[4] != string.Empty ? ParCaseSensitive + strFiltros[4] : string.Empty),
                 (strFiltros[5] != string.Empty ? ParCaseSensitive + strFiltros[5] : string.Empty),
                 "", "", "", "", "", "", "", "", "", "");
        }
        #endregion
    }

}