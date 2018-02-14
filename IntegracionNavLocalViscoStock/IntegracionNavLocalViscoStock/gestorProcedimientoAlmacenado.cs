using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace lanzadorIntegracion
{
    public class gestorProcedimientoAlmacenado
    {
        private string conString;
        private string nombreProcAlmacenado;
        private SqlConnection cnn;
        private SqlCommand cmd;
        private INIKER.ConfProcAlmacenados.AT_Conf_Proc_Almacenados_Service wsConfProcAlmacenado;
        private INIKER.ConfProcAlmacenados.AT_Conf_Proc_Almacenados confProcAlmacenado;

        public gestorProcedimientoAlmacenado(string empresa)
        {
            Properties.Settings miConfig = new Properties.Settings();
            wsConfProcAlmacenado = new INIKER.ConfProcAlmacenados.AT_Conf_Proc_Almacenados_Service();
            nombreProcAlmacenado = miConfig.spIntegracionViscoStock;
            confProcAlmacenado = wsConfProcAlmacenado.Read(nombreProcAlmacenado);
            conString = "Data Source=" + confProcAlmacenado.Servidor +
                        " ; Initial Catalog=" + confProcAlmacenado.Base_de_Datos +
                        " ; User ID=" + confProcAlmacenado.UserID +
                        " ; Password=" + confProcAlmacenado.Password + ";";
            cnn = new SqlConnection(conString);
        }

        public bool lanzarSPViscoStock(ref string mensajeError)
        {
            try
            {
                cmd = new SqlCommand(nombreProcAlmacenado, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                int resultadoSP = cmd.ExecuteNonQuery();
                if (resultadoSP < -1) return false;
                else return true;
            }
            catch (Exception ex)
            {
                mensajeError = ex.Message;
                return false;
            }
        }
    }
}
