using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace IntranetINDUSAL.Negocio
{
    public class cSurtidoCliente
    {
        private DataSet _dsSurtidoCLiente;  // un DataTable por cada familia de productos y un DataColumn por cada subfamilia
        private string _famSel;             // familia seleccionada
        private string _subfamSel;          // subfamilia seleccionada

        public string famSel
        {
            get { return _famSel; }
            set { _famSel = value; }
        }        

        public string subfamSel
        {
            get { return _subfamSel; }
            set { _subfamSel = value; }
        }

        public cSurtidoCliente()
        { 
            // Constructor de la clase
        }

        public cSurtidoCliente(string cliente, string empresa, INIKER.CrossReferences.Tipo_Planchado tipo)
        {
            cProductos surtido = new cProductos();
            // se obtiene el dataset clasificado en datatables
            _dsSurtidoCLiente= surtido.ObtenerSurtidoCliente(cliente,empresa,tipo);            
        }

        public cSurtidoCliente(string cliente, string empresa)
        {
            cProductos surtido = new cProductos();
            // se obtiene el dataset clasificado en datatables
            _dsSurtidoCLiente = surtido.ObtenerSurtidoCliente(cliente, empresa);
        }

        public string[] ArrayFamilias()
        {
            // se declara e instancia un array de objetos String, con tantos elementos como
            // DataTables existan en el DataSet de surtido
            string[] familias= new string[_dsSurtidoCLiente.Tables.Count];
            int iFamilia = 0;
            foreach (DataTable famProductos in _dsSurtidoCLiente.Tables)
            {
                string newFam = famProductos.TableName;
                familias[iFamilia] = newFam;
                iFamilia++;
            }
            return (familias);
        }

        public string[] ArrayFamiliasInclCodigo()
        {
            // se declara e instancia un array de objetos String, con tantos elementos como
            // DataTables existan en el DataSet de surtido
            string[] familias = new string[_dsSurtidoCLiente.Tables.Count];
            int iFamilia = 0;
            foreach (DataTable famProductos in _dsSurtidoCLiente.Tables)
            {
                string newFam = famProductos.TableName+";"+famProductos.ExtendedProperties["Code"].ToString();
                familias[iFamilia] = newFam;
                iFamilia++;
            }
            return (familias);
        }

        public string[] ArraySubfamilias()
        {
            // se declara e instancia un array de objetos String, con tantos elementos como
            // DataColumns existan en el DataTable que tiene los datos de la familia especificada
            DataTable dtFamilia = _dsSurtidoCLiente.Tables[_famSel];
            if (dtFamilia != null)
            {
                string[] subfamilias = new string[dtFamilia.Columns.Count];
                int iSubfamilia = 0;
                foreach (DataColumn subfamProductos in dtFamilia.Columns)
                {
                    string newSubfam = subfamProductos.ColumnName;
                    subfamilias[iSubfamilia] = newSubfam;
                    iSubfamilia++;
                }
                return (subfamilias);
            }
            return (null);
        }

        public string[] ArraySubfamiliasInclCodigo()
        {
            // se declara e instancia un array de objetos String, con tantos elementos como
            // DataColumns existan en el DataTable que tiene los datos de la familia especificada
            DataTable dtFamilia = _dsSurtidoCLiente.Tables[_famSel];
            if (dtFamilia != null)
            {
                string[] subfamilias = new string[dtFamilia.Columns.Count];
                int iSubfamilia = 0;
                foreach (DataColumn subfamProductos in dtFamilia.Columns)
                {
                    string newSubfam = subfamProductos.ColumnName+";"+subfamProductos.ExtendedProperties["Code"].ToString();
                    subfamilias[iSubfamilia] = newSubfam;
                    iSubfamilia++;
                }
                return (subfamilias);
            }
            return (null);
        }

        public string ArrayProductos(string separador)
        {
            // se declara e instancia un array de objetos String, con tantos elementos como
            // DataColumns existan en el DataTable que tiene los datos de la familia especificada
            DataTable dtFamilia = _dsSurtidoCLiente.Tables[_famSel];
            string productos="";
            if (dtFamilia != null)
            {                
                foreach (DataRow filaProductos in dtFamilia.Rows)
                {
                    if (filaProductos[_subfamSel].ToString() != "")
                    {
                        if (productos.Length > 0)
                            productos += separador;
                        productos += filaProductos[_subfamSel].ToString();
                    }
                    else
                    {
                        return (productos);
                    }
                }
                return (productos);
            }
            return (null);
        }
    }
}
