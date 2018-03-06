using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IntranetINDUSAL.Negocio
{
    public class cConteoOLD
    {
        private cLineasConteo datosConteo;        
        private string empresaLogin;

        protected string EmpresaLogin
        {
            get { return empresaLogin; }
            set { empresaLogin = value; }
        }        

        public int CantContada
        {
            get { return datosConteo.CantContada; }
            set { datosConteo.CantContada = value; }
        }

        public DataTable DatosConteo
        {
            get { return datosConteo.Lineas; }
            set { datosConteo.Lineas = value; }
        }

        protected cLineasConteo oDatosConteo
        {
            get { return datosConteo; }
            set { datosConteo = value; }
        }


        /*
        public cConteo(string empresaLogin)
        { 
            // Constructor de la clase
            this.empresaLogin = empresaLogin;
            this.datosConteo = new cLineasConteo(cLineasConteo.TipoConteo.Normal);
        }
        */
        public cConteo(string empresaLogin,cLineasConteo.TipoConteo tipo)
        {
            // Constructor de la clase
            this.empresaLogin = empresaLogin;
            this.datosConteo = new cLineasConteo(tipo);
        } 

        public void UpdateCount(string codProd, string descProd, string nSerie, int qty, bool edit)
        {
            datosConteo.UpdateCount(codProd, descProd, nSerie, qty, edit);
        }

        public void UpdateCount(string codProd, string descProd, string nSerie, int paquetes,
                    int etiPaq, int uds, int etiUds, int udsPaq, string maquina, bool edit)
        {
            datosConteo.UpdateCount(codProd, descProd, nSerie, paquetes, etiPaq, uds, etiUds, udsPaq, maquina, edit);
        }

        public void DeleteCountLine(int iRow)
        {
            datosConteo.DeleteCountLine(iRow);
        }

        public void RegisterCount(string codCliente)
        {
            cProduccion oProduccion;
            if (datosConteo.Lineas.Rows.Count > 0)
            {
                oProduccion = new cProduccion(empresaLogin);
                foreach (DataRow oRow in datosConteo.Lineas.Rows)
                {
                    oProduccion.RegistrarConteo(codCliente, oRow["Cod. Producto"].ToString(),
                        Convert.ToDecimal(oRow["Cant. contada"].ToString()), "");                    
                }
            }
            datosConteo = null;
            datosConteo = new cLineasConteo(cLineasConteo.TipoConteo.Normal);
        }

    }
}
