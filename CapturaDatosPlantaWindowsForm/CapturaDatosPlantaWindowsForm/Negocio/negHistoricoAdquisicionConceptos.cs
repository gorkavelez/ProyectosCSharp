using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SK.historicoAdquisicionConceptos
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Web.Services;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.Net;

    public partial class CapturaDatos_HistoricoAdquisicionConceptos_Service : System.Web.Services.Protocols.SoapHttpClientProtocol
    {
        public const char ParCaseSensitive = '@';
        private int IdTabla = 18;
        SK.Funciones.Funciones_WS Funciones = new SK.Funciones.Funciones_WS();
        

        //List<AT_Solicitudes> ??
        [System.ComponentModel.Description("Devuelve los Clientes"), System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select, true)]
        public SK.historicoAdquisicionConceptos.CapturaDatos_HistoricoAdquisicionConceptos[] ReadMultiple(string Where, int pageSize, int numPage, string Sort)
        {
            bool flagResp = false;
            string lastReadKey = null;
            SK.historicoAdquisicionConceptos.CapturaDatos_HistoricoAdquisicionConceptos_Filter[] filtros = null;
            //Array.Resize(ref misFiltros, 0);
            SK.historicoAdquisicionConceptos.CapturaDatos_HistoricoAdquisicionConceptos[] consultaPage; // = new AT_Solicitudes();
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
        private bool AplicarFiltros(string Where, ref SK.historicoAdquisicionConceptos.CapturaDatos_HistoricoAdquisicionConceptos_Filter[] misFiltros)
        {
            //Where: Lo vamos a hacer pasando un string separado por ';'            
            //0: Sabana
            //1: Fecha Registro
            //2: Hora Registro

            bool flagOK = true;
            try
            {                
                SK.historicoAdquisicionConceptos.CapturaDatos_HistoricoAdquisicionConceptos_Filter miFiltro;
                Array.Resize(ref misFiltros, 0);

                if ((Where != null) && Where.Length > 0)
                {
                    string[] strFiltros = null;
                    strFiltros = Where.Split(';');

                    //0: sabana
                    if ((strFiltros[0] != null) && strFiltros[0] != string.Empty)
                    {
                        miFiltro = new SK.historicoAdquisicionConceptos.CapturaDatos_HistoricoAdquisicionConceptos_Filter();
                        miFiltro.Field = SK.historicoAdquisicionConceptos.CapturaDatos_HistoricoAdquisicionConceptos_Fields.Sabana;
                        miFiltro.Criteria = ParCaseSensitive + strFiltros[0];
                        Array.Resize(ref misFiltros, misFiltros.Length + 1);
                        misFiltros[misFiltros.Length - 1] = miFiltro;
                    }



                    //1:  Fecha registro
                    if (strFiltros.Length > 1 && (strFiltros[1] != null) && strFiltros[1] != string.Empty)
                    {
                        miFiltro = new SK.historicoAdquisicionConceptos.CapturaDatos_HistoricoAdquisicionConceptos_Filter();
                        miFiltro.Field = SK.historicoAdquisicionConceptos.CapturaDatos_HistoricoAdquisicionConceptos_Fields.Fecha_registro;
                        miFiltro.Criteria = strFiltros[1];
                        Array.Resize(ref misFiltros, misFiltros.Length + 1);
                        misFiltros[misFiltros.Length - 1] = miFiltro;
                    }
                    

                    //2: Hora registro
                    if (strFiltros.Length > 2 && (strFiltros[2] != null) && strFiltros[2] != string.Empty)
                    {
                        miFiltro = new SK.historicoAdquisicionConceptos.CapturaDatos_HistoricoAdquisicionConceptos_Filter();
                        miFiltro.Field = SK.historicoAdquisicionConceptos.CapturaDatos_HistoricoAdquisicionConceptos_Fields.Hora_registro;
                        miFiltro.Criteria = strFiltros[2];
                        Array.Resize(ref misFiltros, misFiltros.Length + 1);
                        misFiltros[misFiltros.Length - 1] = miFiltro;
                    }

                    
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