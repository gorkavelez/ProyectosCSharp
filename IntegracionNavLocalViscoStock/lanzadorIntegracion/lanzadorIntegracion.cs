using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lanzadorIntegracion
{
    public class lanzadorIntegracion
    {        
        
        private gestorProcedimientoAlmacenado gestorProcAlmacenado;
        private string mensajeError;

        public lanzadorIntegracion()
        {

        }

        public bool lanzar(ref string msgError)
        {
            try
            {
                msgError = "";
                INIKER.VSLabelIntActiveCompanies.VS_Label_Int_Active_Companies wsEmpresasActivasViscoStock = new INIKER.VSLabelIntActiveCompanies.VS_Label_Int_Active_Companies();
                string empresasViscoStock = "";
                wsEmpresasActivasViscoStock.EmpresasIntegracionViscoStock(ref empresasViscoStock);
                string[] arrayEmpresasViscostock = empresasViscoStock.Split(';');
                foreach (string empresaViscoStock in arrayEmpresasViscostock)
                {
                    //generar tablas intermedias
                    INIKER.ViscoStockLabelIntegration.ViscoStock_Label_Integration wsLabelIntegration = new INIKER.ViscoStockLabelIntegration.ViscoStock_Label_Integration(empresaViscoStock);
                    wsLabelIntegration.GenerateViscoStockIntegInfo();

                    //lanzar procedimiento almacenado
                    gestorProcAlmacenado = new gestorProcedimientoAlmacenado(empresasViscoStock);
                    gestorProcAlmacenado.lanzarSPViscoStock(ref mensajeError);
                    if (mensajeError != null)
                    {
                        msgError = mensajeError;
                        return false;
                    }
                    //cerrar conexion
                    gestorProcAlmacenado.cerrarSPViscoStock();
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

        //**INI ES-6411 Running stock report GVE
        public bool RunColaTrabajo(ref string msgError)
        {
            try
            {
                msgError = string.Empty;                
                INIKER.VSLabelIntActiveCompanies.VS_Label_Int_Active_Companies ListaEmpresas = new INIKER.VSLabelIntActiveCompanies.VS_Label_Int_Active_Companies();
                string[] ListEmpresas = new string[10];
                ListaEmpresas.EmpresasListado(ref ListEmpresas);                
                foreach (string Empresa in ListEmpresas)
                {
                    INIKER.VSLabelIntActiveCompanies.VS_Label_Int_Active_Companies EmpresaLanzar = new INIKER.VSLabelIntActiveCompanies.VS_Label_Int_Active_Companies(Empresa);
                    //Llamar a función que lanza codeunit 
                    if (!EmpresaLanzar.LanzarColaProyecto())
                        throw new Exception("Error");
                }
                return true;
            }
            catch (Exception ex)
            {
                msgError = ex.Message;
                return false;
            }        
        }
        //**FIN ES-6411 Running stock report GVE
    }
}
