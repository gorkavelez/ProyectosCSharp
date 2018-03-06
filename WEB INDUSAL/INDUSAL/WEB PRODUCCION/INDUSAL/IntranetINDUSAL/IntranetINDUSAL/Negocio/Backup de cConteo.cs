using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IntranetINDUSAL.Negocio
{
    public class cConteo
    {        
        private DataTable datosConteo;
        private int filaSeleccionada;
        private int cantContada;
        private string empresaLogin;

        public int CantContada
        {
            get { return cantContada; }
            set { cantContada = value; }
        }

        public DataTable DatosConteo
        {
            get { return datosConteo; }
            set { datosConteo = value; }
        }

        public cConteo(string empresaLogin)
        { 
            // Constructor de la clase
            this.empresaLogin = empresaLogin;
            GenerarDataTable();

        }

        private void GenerarDataTable()
        {
            datosConteo = new DataTable("datos");
            DataColumn newColumn;

            newColumn = datosConteo.Columns.Add("Cod. Producto");
            newColumn.DataType = System.Type.GetType("System.String");
            newColumn.AllowDBNull = false;

            newColumn = datosConteo.Columns.Add("Desc. Producto");
            newColumn.DataType = System.Type.GetType("System.String");
            newColumn.AllowDBNull = false;

            newColumn = datosConteo.Columns.Add("Cant. contada");
            newColumn.DataType = System.Type.GetType("System.Int16");
            newColumn.AllowDBNull = false;
        }

        public bool FindItem(string codProducto)
        {
            DataRow oRow;

            cantContada = 0;
            filaSeleccionada = -1;

            for (int iRow = 0; iRow < datosConteo.Rows.Count;iRow++ )
            {
                oRow = datosConteo.Rows[iRow];
                if (oRow["Cod. producto"].ToString() == codProducto)
                {
                    cantContada = int.Parse(oRow["Cant. contada"].ToString());
                    filaSeleccionada = iRow;
                    return (true);
                }
            }
            return (false);
        }

        private void AddCountLine(string codProd, string descProd, int qty)
        {
            DataRow newRow = datosConteo.NewRow();
            newRow["Cod. Producto"] = codProd;
            newRow["Desc. Producto"] = descProd;
            newRow["Cant. contada"] = qty;
            datosConteo.Rows.Add(newRow);
        }

        private void UpdateCountLine(int qty, bool edit)
        {
            DataRow existRow = datosConteo.Rows[filaSeleccionada];
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

        public void UpdateCount(string codProd, string descProd, int qty, bool edit)
        {
            if (this.FindItem(codProd))
            {
                UpdateCountLine(qty, edit);
            }
            else
            {
                AddCountLine(codProd, descProd, qty);
            }

        }

        public void DeleteCountLine(int iRow)
        {
            try
            {
                datosConteo.Rows[iRow].Delete();
            }
            catch (IndexOutOfRangeException e)
            { }
            finally
            {
                datosConteo.AcceptChanges();
            }
        }

        public void RegisterCount(string codCliente)
        {
            cProduccion oProduccion;
            if (datosConteo.Rows.Count > 0)
            {
                oProduccion = new cProduccion(empresaLogin);
                foreach (DataRow oRow in datosConteo.Rows)
                {
                    oProduccion.RegistrarConteo(codCliente, oRow["Cod. Producto"].ToString(),
                        Convert.ToDecimal(oRow["Cant. contada"].ToString()), "");                    
                }
            }
            datosConteo = null;
            GenerarDataTable();
        }

    }
}
