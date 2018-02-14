using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NegCapturaDatosWPF.AdquisicionConceptos
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Web.Services;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.Net;

    /// <remarks/>
    public partial class CapturaDatos_AdquisicionConceptos_Service : System.Web.Services.Protocols.SoapHttpClientProtocol
    {
        public const char ParCaseSensitive = '@';
        private int IdTabla = 18;
        NegCapturaDatosWPF.Funciones.Funciones_WS Funciones = new NegCapturaDatosWPF.Funciones.Funciones_WS();

        //List<AT_Solicitudes> ??
        [System.ComponentModel.Description("Devuelve los Clientes"), System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select, true)]
        public CapturaDatos_AdquisicionConceptos[] ReadMultiple(string Where, int pageSize, int numPage, string Sort)
        {
            bool flagResp = false;
            string lastReadKey = null;
            NegCapturaDatosWPF.AdquisicionConceptos.CapturaDatos_AdquisicionConceptos_Filter[] filtros = null;
            //Array.Resize(ref misFiltros, 0);
            CapturaDatos_AdquisicionConceptos[] consultaPage; // = new AT_Solicitudes();
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
        private bool AplicarFiltros(string Where, ref NegCapturaDatosWPF.AdquisicionConceptos.CapturaDatos_AdquisicionConceptos_Filter[] misFiltros)
        {
            //Where: Lo vamos a hacer pasando un string separado por ';'
            //0: Codigo  
            //1: Sabana

            bool flagOK = true;
            try
            {
                NegCapturaDatosWPF.AdquisicionConceptos.CapturaDatos_AdquisicionConceptos_Filter miFiltro;

                Array.Resize(ref misFiltros, 0);

                if ((Where != null) && Where.Length > 0)
                {
                    string[] strFiltros = null;
                    strFiltros = Where.Split(';');

                    ////0: Codigo
                    //if ((strFiltros[0] != null) && strFiltros[0] != string.Empty)
                    //{
                    //    miFiltro = new NegCapturaDatosWPF.AdquisicionConceptos.CapturaDatos_AdquisicionConceptos_Filter();
                    //    miFiltro.Field = NegCapturaDatosWPF.AdquisicionConceptos.CapturaDatos_AdquisicionConceptos_Fields.Codigo;
                    //    miFiltro.Criteria = strFiltros[0];
                    //    Array.Resize(ref misFiltros, misFiltros.Length + 1);
                    //    misFiltros[misFiltros.Length - 1] = miFiltro;
                    //}

                    //0: SABANA
                    if ((strFiltros[0] != null) && strFiltros[0] != string.Empty)
                    {
                        miFiltro = new NegCapturaDatosWPF.AdquisicionConceptos.CapturaDatos_AdquisicionConceptos_Filter();
                        miFiltro.Field = NegCapturaDatosWPF.AdquisicionConceptos.CapturaDatos_AdquisicionConceptos_Fields.Sabana;
                        miFiltro.Criteria = strFiltros[0];
                        Array.Resize(ref misFiltros, misFiltros.Length + 1);
                        misFiltros[misFiltros.Length - 1] = miFiltro;
                    }


                    ////1:Sabana
                    //if (strFiltros.Length > 1 && (strFiltros[1] != null) && strFiltros[1] != string.Empty)
                    //{
                    //    miFiltro = new NegCapturaDatosWPF.AdquisicionConceptos.CapturaDatos_AdquisicionConceptos_Filter();
                    //    miFiltro.Field = NegCapturaDatosWPF.AdquisicionConceptos.CapturaDatos_AdquisicionConceptos_Fields.Sabana;
                    //    miFiltro.Criteria = strFiltros[1];
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
