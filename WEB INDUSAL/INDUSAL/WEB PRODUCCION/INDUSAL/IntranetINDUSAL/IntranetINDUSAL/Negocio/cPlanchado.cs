using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using INIKER.CrossReferences;

namespace IntranetINDUSAL.Negocio
{
    public class cPlanchado: cConteo
    {
        #region Variables

            private Tipo_Planchado _tipoPlanchado;              // identifica la zona de planchado          

        #endregion

        #region Propiedades

            public Tipo_Planchado TipoPlanchado
            {
                get { return _tipoPlanchado; }
                set { _tipoPlanchado = value; }
            }            

        #endregion

        #region Constructores

            public cPlanchado(string empresaLogin)
                : base(empresaLogin,cLineasConteo.TipoConteo.Planchado)
            {
                
            }

            public cPlanchado(string empresaLogin, int tipo)
                : base(empresaLogin,cLineasConteo.TipoConteo.Planchado)
            {
                switch (tipo)
                {
                    case 4:
                        this._tipoPlanchado = Tipo_Planchado.Calandra;
                        break;
                    case 5:
                        this._tipoPlanchado = Tipo_Planchado.Felpa;
                        break;
                    case 6:
                        this._tipoPlanchado = Tipo_Planchado.Forma;
                        break;
                }
            }
                    


        #endregion

            #region Metodos

            public int TipoPlanchadoToInt(Tipo_Planchado tipo)
            {
                int valor=0;
                switch (tipo)
                { 
                    case Tipo_Planchado.Calandra:
                        valor=4;
                        break;
                    case Tipo_Planchado.Felpa:
                        valor=5;
                        break;
                    case Tipo_Planchado.Forma:
                        valor=6;
                        break;                    
                }
                return (valor);

            }

            public void Registrar(string codCliente)
            {
                cProduccion oProduccion;
                if (base.DatosConteo.Rows.Count > 0)
                {
                    oProduccion = new cProduccion(EmpresaLogin);
                    foreach (DataRow oRow in base.DatosConteo.Rows)
                    {
                        oProduccion.RegistrarPlanchado(codCliente, oRow["Cod. Producto"].ToString(),
                            Convert.ToDecimal(oRow["Uds. totales"].ToString()), "", TipoPlanchadoToInt(_tipoPlanchado),
                            oRow["Maquina"].ToString());
                    }
                }
                oDatosConteo= null;
                oDatosConteo = new cLineasConteo(cLineasConteo.TipoConteo.Planchado);
            }

            public void Cancelar()
            {
                oDatosConteo = null;
                oDatosConteo = new cLineasConteo(cLineasConteo.TipoConteo.Planchado);
            }

        #endregion

        #region Metodos Privados
        
        #endregion
    }
}
