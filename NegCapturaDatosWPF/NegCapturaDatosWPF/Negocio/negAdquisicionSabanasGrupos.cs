﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NegCapturaDatosWPF.SabanasGrupos
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Web.Services;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.Net;

    /// <remarks/>
    public partial class CapturaDatos_AdquisicionSabanasGrupos_Service : System.Web.Services.Protocols.SoapHttpClientProtocol
    {
        public const char ParCaseSensitive = '@';
        private int IdTabla = 50074;
        NegCapturaDatosWPF.Funciones.Funciones_WS Funciones = new NegCapturaDatosWPF.Funciones.Funciones_WS();

        [System.ComponentModel.Description("Devuelve grupos sabanas"), System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select, true)]
        public CapturaDatos_AdquisicionSabanasGrupos[] ReadMultiple(string Where, int pageSize, int numPage, string Sort)
        {
            bool flagResp = false;
            string lastReadKey = null;
            NegCapturaDatosWPF.SabanasGrupos.CapturaDatos_AdquisicionSabanasGrupos_Filter[] filtros = null;
            //Array.Resize(ref misFiltros, 0);
            CapturaDatos_AdquisicionSabanasGrupos[] consultaPage; // = new AT_Solicitudes();
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
        private bool AplicarFiltros(string Where, ref NegCapturaDatosWPF.SabanasGrupos.CapturaDatos_AdquisicionSabanasGrupos_Filter[] misFiltros)
        {
            //Where: Lo vamos a hacer pasando un string separado por ';'
            //0: Grupo                       

            bool flagOK = true;
            try
            {
                NegCapturaDatosWPF.SabanasGrupos.CapturaDatos_AdquisicionSabanasGrupos_Filter miFiltro;
                Array.Resize(ref misFiltros, 0);

                if ((Where != null) && Where.Length > 0)
                {
                    string[] strFiltros = null;
                    strFiltros = Where.Split(';');
                    //0: grupo
                    if ((strFiltros[0] != null) && strFiltros[0] != string.Empty)
                    {
                        miFiltro = new NegCapturaDatosWPF.SabanasGrupos.CapturaDatos_AdquisicionSabanasGrupos_Filter();
                        miFiltro.Field = NegCapturaDatosWPF.SabanasGrupos.CapturaDatos_AdquisicionSabanasGrupos_Fields.Grupo;
                        miFiltro.Criteria = strFiltros[0];
                        Array.Resize(ref misFiltros, misFiltros.Length + 1);
                        misFiltros[misFiltros.Length - 1] = miFiltro;
                    }

                    //1: Sabana
                    if (strFiltros.Length > 1 && (strFiltros[1] != null) && strFiltros[1] != string.Empty)
                    {
                        miFiltro = new NegCapturaDatosWPF.SabanasGrupos.CapturaDatos_AdquisicionSabanasGrupos_Filter();
                        miFiltro.Field = NegCapturaDatosWPF.SabanasGrupos.CapturaDatos_AdquisicionSabanasGrupos_Fields.Sabana;
                        miFiltro.Criteria = strFiltros[1];
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
            //0: Grupo

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