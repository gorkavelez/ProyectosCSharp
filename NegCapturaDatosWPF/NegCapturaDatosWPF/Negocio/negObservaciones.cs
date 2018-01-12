using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NegCapturaDatosWPF.Observaciones
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Web.Services;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.Net;

    /// <remarks/>
    public partial class CapturaDatos_Observaciones_Service : System.Web.Services.Protocols.SoapHttpClientProtocol
    {
        public const char ParCaseSensitive = '@';
        private int IdTabla = 18;
        NegCapturaDatosWPF.Funciones.Funciones_WS Funciones = new NegCapturaDatosWPF.Funciones.Funciones_WS();

        //List<AT_Solicitudes> ??
        [System.ComponentModel.Description("Devuelve los Clientes"), System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select, true)]
        public CapturaDatos_Observaciones[] ReadMultiple(string Where, int pageSize, int numPage, string Sort)
        {
            bool flagResp = false;
            string lastReadKey = null;
            NegCapturaDatosWPF.Observaciones.CapturaDatos_Observaciones_Filter[] filtros = null;
            //Array.Resize(ref misFiltros, 0);
            CapturaDatos_Observaciones[] consultaPage; // = new AT_Solicitudes();
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
        private bool AplicarFiltros(string Where, ref NegCapturaDatosWPF.Observaciones.CapturaDatos_Observaciones_Filter[] misFiltros)
        {
            //Where: Lo vamos a hacer pasando un string separado por ';'
            //0: idProceso            

            bool flagOK = true;
            try
            {
                NegCapturaDatosWPF.Observaciones.CapturaDatos_Observaciones_Filter miFiltro;

                Array.Resize(ref misFiltros, 0);

                if ((Where != null) && Where.Length > 0)
                {
                    string[] strFiltros = null;
                    strFiltros = Where.Split(';');

                    //0: sabana
                    if ((strFiltros[0] != null) && strFiltros[0] != string.Empty)
                    {
                        miFiltro = new NegCapturaDatosWPF.Observaciones.CapturaDatos_Observaciones_Filter();
                        miFiltro.Field = NegCapturaDatosWPF.Observaciones.CapturaDatos_Observaciones_Fields.Sabana;
                        miFiltro.Criteria = strFiltros[0];
                        Array.Resize(ref misFiltros, misFiltros.Length + 1);
                        misFiltros[misFiltros.Length - 1] = miFiltro;
                    }


                    //1:fecha                    
                    if (strFiltros.Length > 1 && (strFiltros[1] != null) && strFiltros[1] != string.Empty)
                    {
                        miFiltro = new NegCapturaDatosWPF.Observaciones.CapturaDatos_Observaciones_Filter();
                        miFiltro.Field = NegCapturaDatosWPF.Observaciones.CapturaDatos_Observaciones_Fields.Fecha_Registro;
                        miFiltro.Criteria = strFiltros[1];
                        Array.Resize(ref misFiltros, misFiltros.Length + 1);
                        misFiltros[misFiltros.Length - 1] = miFiltro;
                    }

                    //2:hora                    
                    if (strFiltros.Length > 2 && (strFiltros[2] != null) && strFiltros[2] != string.Empty)
                    {
                        miFiltro = new NegCapturaDatosWPF.Observaciones.CapturaDatos_Observaciones_Filter();
                        miFiltro.Field = NegCapturaDatosWPF.Observaciones.CapturaDatos_Observaciones_Fields.Hora_Registro;
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
