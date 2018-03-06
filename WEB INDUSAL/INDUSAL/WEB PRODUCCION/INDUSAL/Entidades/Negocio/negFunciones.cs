using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace INIKER.Funciones
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Web.Services;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;

    /// <remarks/>
    public partial class Funciones_WS : System.Web.Services.Protocols.SoapHttpClientProtocol
    {
        public const char ParCaseSensitive = '@';
        //private int IdTabla = 0; //???? Tabla de Descriptores...
        //INIKER.Funciones.Funciones_WS Funciones = new INIKER.Funciones.Funciones_WS();
       
        [System.ComponentModel.Description("Devuelve las Auxiliar Familias"), System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select, true)]
        public INIKER.Funciones.ProdNivel[] GetProdNiveles2(string Where, int pageSize, int numPage, string Sort)
        {
            bool flagResp = false;
            INIKER.Funciones.Funciones_WS serv = new INIKER.Funciones.Funciones_WS();
            INIKER.Funciones.ProdNiveles objXML = new INIKER.Funciones.ProdNiveles();
            //INIKER.ATAuxiliarFamilias.AT_Auxiliar_Familias_Filter[] filtros = null;
            //Array.Resize(ref misFiltros, 0);
            INIKER.Funciones.ProdNivel[] consultaPage = null;
            Array.Resize(ref consultaPage, 0);
            //flagResp = AplicarFiltros(Where, ref filtros);
            try
            {
                if ((Where != null) && (Where != ""))
                {
                    //01/09/09 - GRE. Reemplazamos el carácter & por ? porque sino no devolvía nada.
                    Where = Where.Replace("&", "?");
                    flagResp = serv.GetProdNivel2(ref objXML, Where);

                    //INIKER.ATFunciones.ProdNivel item;
                    if ((objXML != null) && (objXML.ProdNivel != null))
                    {
                        consultaPage = objXML.ProdNivel;
                    }
                    //Añado una nueva fila, la de UNKNOWN
                    INIKER.Funciones.ProdNivel prodNivelUNKNOWN = new INIKER.Funciones.ProdNivel();
                    prodNivelUNKNOWN.nProNivel2 = "UNKNOWN";
                    Array.Resize(ref consultaPage, consultaPage.Length + 1);
                    //Añado el UNKNOWN en la primera posición
                    int pos = 0;
                    int i = 0;
                    for (i = consultaPage.Length - 1; i > pos; i--)
                        consultaPage[i] = consultaPage[i - 1];
                    consultaPage[pos] = prodNivelUNKNOWN;
                            

                //If listItems.Count = 0 And AddSelectOne Then 'Añadirle uno más para el combo
                //    itemconsultaPais.Code = "-1"
                //    itemconsultaPais.Name = My.Resources.EligeUno '"Elige uno"
                //    listItems.Add(itemconsultaPais)
                //    itemconsultaPais = New consultaPais
                //End If
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
        //public int GetRecCount(string Where, int pageSize, int numPage, string Sort)
        //{
        //    //Añadir la lista de filtros para el cliente
        //    //0: Codigo
        //    //1: Descripcion
        //    //2: VerEnSP

        //    string[] strFiltros = null;
        //    strFiltros = Where.Split(';');
        //    return Funciones.GetRecCount(IdTabla, strFiltros[0], 
        //         (strFiltros[1] != string.Empty ? ParCaseSensitive + strFiltros[1] : string.Empty),
        //         strFiltros[2], "", "", "", "", "", "", "", "", "", "","","");
        //}
#endregion
    }
 
}