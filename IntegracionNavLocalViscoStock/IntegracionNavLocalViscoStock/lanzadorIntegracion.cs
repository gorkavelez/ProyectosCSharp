using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lanzadorIntegracion
{
    public class lanzadorIntegracion
    {
        private INIKER.VSLabelIntActiveCompanies.VS_Label_Int_Active_Companies wsEmpresasActivasViscoStock;
        private INIKER.ViscoStockLabelIntegration.ViscoStock_Label_Integration wsLabelIntegration;
        private gestorProcedimientoAlmacenado gestorProcAlmacenado;
        private string mensajeError;

        public lanzadorIntegracion()
        {

        }

        public bool lanzar(ref string msgError)
        {
            try
            {
                wsEmpresasActivasViscoStock = new INIKER.VSLabelIntActiveCompanies.VS_Label_Int_Active_Companies();
                string empresasViscoStock = "";
                wsEmpresasActivasViscoStock.EmpresasIntegracionViscoStock(ref empresasViscoStock);
                string[] arrayEmpresasViscostock = empresasViscoStock.Split(';');
                foreach (string empresaViscoStock in arrayEmpresasViscostock)
                {
                    //generar tablas intermedias
                    wsLabelIntegration = new INIKER.ViscoStockLabelIntegration.ViscoStock_Label_Integration(empresasViscoStock);
                    wsLabelIntegration.GenerateViscoStockIntegInfo();

                    //lanzar procedimiento almacenado
                    gestorProcAlmacenado = new gestorProcedimientoAlmacenado(empresasViscoStock);
                    gestorProcAlmacenado.lanzarSPViscoStock(ref mensajeError);
                    if (mensajeError != "")
                    {
                        msgError = mensajeError;
                        return false;
                    }
                    //borrar tablas intermedias
                    wsLabelIntegration.DeleteViscoStockIntegInfo();

                    wsEmpresasActivasViscoStock = null;
                    gestorProcAlmacenado = null;
                }
                return true;
            }
            catch(Exception ex)
            {
                msgError = ex.Message;
                return false;
            }
        }
    }
}
