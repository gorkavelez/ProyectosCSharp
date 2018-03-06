using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IntranetINDUSAL.Negocio
{
    public class cLineasConteo
    {

        #region Privado

        #region Variables
            // Variables privadas de la clase
            private DataTable lineas;
            private int filaSeleccionada;
            private int cantContada;
            private TipoConteo _tipo;
        #endregion

        #region Comunes

            private void GenerarDataTable()
            {
                lineas = new DataTable("datos");
                DataColumn newColumn;

                // Se crean las columnas comunes a todos los tipos de conteo

                newColumn = lineas.Columns.Add("Cod. Producto");
                newColumn.DataType = System.Type.GetType("System.String");
                newColumn.AllowDBNull = false;

                newColumn = lineas.Columns.Add("Desc. Producto");
                newColumn.DataType = System.Type.GetType("System.String");
                newColumn.AllowDBNull = false;

                newColumn = lineas.Columns.Add("Num. Serie");
                newColumn.DataType = System.Type.GetType("System.String");
                newColumn.AllowDBNull = true;

                switch (_tipo)
                {
                    case TipoConteo.Normal:
                        newColumn = lineas.Columns.Add("Cant. contada");
                        newColumn.DataType = System.Type.GetType("System.Int16");
                        newColumn.AllowDBNull = false;
                        break;

                    case TipoConteo.Planchado:
                        newColumn = lineas.Columns.Add("Paquetes");
                        newColumn.DataType = System.Type.GetType("System.Int16");
                        newColumn.DefaultValue = 0;
                        newColumn.AllowDBNull = false;

                        newColumn = lineas.Columns.Add("Etiquetas paq.");
                        newColumn.DataType = System.Type.GetType("System.Int16");
                        newColumn.DefaultValue = 0;
                        newColumn.AllowDBNull = false;

                        newColumn = lineas.Columns.Add("Uds.");
                        newColumn.DataType = System.Type.GetType("System.Int16");
                        newColumn.DefaultValue = 0;
                        newColumn.AllowDBNull = false;

                        newColumn = lineas.Columns.Add("Etiquetas uds.");
                        newColumn.DataType = System.Type.GetType("System.Int16");
                        newColumn.DefaultValue = 0;
                        newColumn.AllowDBNull = false;

                        newColumn = lineas.Columns.Add("Uds. paquete");
                        newColumn.DataType = System.Type.GetType("System.Int16");
                        newColumn.DefaultValue = 1;
                        newColumn.AllowDBNull = false;

                        newColumn = lineas.Columns.Add("Uds. totales");
                        newColumn.DataType = System.Type.GetType("System.Int16");
                        newColumn.DefaultValue = 0;
                        newColumn.AllowDBNull = false;

                        newColumn = lineas.Columns.Add("Maquina");
                        newColumn.DataType = System.Type.GetType("System.String");
                        newColumn.DefaultValue = "";
                        newColumn.AllowDBNull = false;
                        break;
                }

            }

            private bool FindItem(string codProducto)
            {
                DataRow oRow;

                cantContada = 0;
                filaSeleccionada = -1;

                for (int iRow = 0; iRow < lineas.Rows.Count; iRow++)
                {
                    oRow = lineas.Rows[iRow];
                    if (oRow["Cod. producto"].ToString() == codProducto)
                    {
                        //cantContada = int.Parse(oRow["Cant. contada"].ToString());
                        filaSeleccionada = iRow;
                        return (true);
                    }
                }
                return (false);
            }

        #endregion Comunes

        #region TipoConteo:Normal

            private void AddCountLine(string codProd, string descProd, string nSerie, int qty)
        {
            DataRow newRow = lineas.NewRow();
            newRow["Cod. Producto"] = codProd;
            newRow["Desc. Producto"] = descProd;
            newRow["Num. serie"] = nSerie;
            newRow["Cant. contada"] = qty;
            lineas.Rows.Add(newRow);
        }

            private void UpdateCountLine(int qty, bool edit)
        {
            DataRow existRow = lineas.Rows[filaSeleccionada];
            if (!edit)
            {
                existRow["Cant. contada"] = int.Parse(existRow["Cant. contada"].ToString()) + qty;
            }
            else
            {
                existRow["Cant. contada"] = qty;
            }
            existRow.AcceptChanges();
        }

        #endregion TipoConteo:Normal

        #region TipoConteo:Planchado

            private void AddCountLine(string codProd, string descProd, string nSerie, 
                int paquetes, int etiPaq, int uds, int etiUds, int udsPaq, string maquina)
            {
                DataRow newRow = lineas.NewRow();

                AsignarDatosFila(ref newRow,codProd,descProd, nSerie, 
                paquetes, etiPaq, uds ,etiUds, udsPaq, maquina, false);
                
                lineas.Rows.Add(newRow);
            }

            private void UpdateCountLine(string codProd, string descProd, string nSerie,
                int paquetes, int etiPaq, int uds, int etiUds, int udsPaq, string maquina, bool edit)
            {                
                DataRow existRow = lineas.Rows[filaSeleccionada];

                AsignarDatosFila(ref existRow,codProd,descProd, nSerie, 
                paquetes, etiPaq, uds,etiUds, udsPaq, maquina, edit);
                
                existRow.AcceptChanges();
            }

            private void AsignarDatosFila(ref DataRow oRow, string codProd, string descProd, string nSerie,
                int paquetes, int etiPaq, int uds, int etiUds, int udsPaq, string maquina, bool edit)
            {
                int pico;
                int nuevosPaq = 0;

                oRow["Cod. Producto"] = codProd;
                oRow["Desc. Producto"] = descProd;
                oRow["Num. serie"] = nSerie;
                oRow["Paquetes"] = edit ? paquetes : int.Parse(oRow["Paquetes"].ToString()) + paquetes;
                oRow["Etiquetas paq."] = edit ? etiPaq : int.Parse(oRow["Etiquetas paq."].ToString()) + etiPaq;

                pico = edit ? uds : int.Parse(oRow["Uds."].ToString()) + uds;
                EmpaquetarUdsSueltas(udsPaq, ref nuevosPaq, ref pico);

                oRow["Paquetes"] = int.Parse(oRow["Paquetes"].ToString()) + nuevosPaq;
                oRow["Etiquetas paq."] = oRow["Paquetes"];
                oRow["Uds."] = pico;

                oRow["Etiquetas uds."] = (oRow["Uds."].ToString() == "0") ? 0 : 1;
                oRow["Uds. paquete"] = udsPaq;
                oRow["Uds. totales"] = Totalizar(int.Parse(oRow["Paquetes"].ToString()), udsPaq, pico);

                oRow["Maquina"] = maquina;
            }

            private void EmpaquetarUdsSueltas(int udsPaq, ref int nuevosPaq, ref int pico)
            {
                if (pico >= udsPaq)
                {
                    nuevosPaq=(pico / udsPaq);                    
                    pico = (pico % udsPaq);
                }
                else
                {
                    nuevosPaq = 0;                    
                }
            }

            private int Totalizar(int paqs, int udsPaq, int uds)
            {
                return ((paqs * udsPaq) + uds);
            }

        #endregion TipoConteo:Planchado

        #endregion Privado

        #region Publico

            #region Propiedades
                public DataTable Lineas
                {
                    get { return lineas; }
                    set { lineas = value; }
                }

                public int CantContada
                {
                    get { return cantContada; }
                    set { cantContada = value; }
                }

            #endregion

            #region Tipos

                public enum TipoConteo
                {
                    Normal,
                    Planchado
                }
        
            #endregion
            
            #region Comunes

                public cLineasConteo(TipoConteo tipo)
                {
                    _tipo = tipo;
                    GenerarDataTable();
                } 

                public void DeleteCountLine(int iRow)
                {
                    try
                    {
                        lineas.Rows[iRow].Delete();
                    }
                    catch 
                    { }
                    finally
                    {
                        lineas.AcceptChanges();
                    }
                }
    
        #endregion Comunes

            #region TipoConteo:Normal

                public void UpdateCount(string codProd, string descProd, string nSerie, int qty, bool edit)
                {
                    if (this.FindItem(codProd))
                    {
                        UpdateCountLine(qty, edit);
                    }
                    else
                    {
                        AddCountLine(codProd, descProd, nSerie, qty);
                    }

                }
            
            #endregion TipoConteo:Normal
                
            #region TipoConteo:Planchado

                public void UpdateCount(string codProd, string descProd, string nSerie, int paquetes, 
                    int etiPaq, int uds, int etiUds, int udsPaq, string maquina, bool edit)
                {
                    if (this.FindItem(codProd))
                    {
                        UpdateCountLine(codProd, descProd, nSerie, paquetes, etiPaq, uds, etiUds,udsPaq, maquina, edit);
                    }
                    else
                    {
                        AddCountLine(codProd, descProd, nSerie, paquetes, etiPaq, uds, etiUds, udsPaq, maquina);
                    }

                }

            #endregion TipoConteo:Planchado

        #endregion Publico

        #region Estatico
            
        #endregion
    }
}
