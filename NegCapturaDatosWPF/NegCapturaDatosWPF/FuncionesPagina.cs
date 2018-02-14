using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFIntegration;
using System.Globalization;

namespace NegCapturaDatosWPF
{
    public class FuncionesPagina
    {

        public bool guardarDatosHistoricos(ISabana sabana, int idCampoIni, int idCampoFin)
        {
            bool procesadoOk = false;
            string[] valores = new string[1500];
            NegCapturaDatosWPF.FuncionesCapturaDatos.FuncionesCapturaWeb cFunWeb = new NegCapturaDatosWPF.FuncionesCapturaDatos.FuncionesCapturaWeb();


            //aqui van la lectura de todos los valores de los campos
            for (int indCtrlTxt = 1; indCtrlTxt <= idCampoFin; indCtrlTxt++)
            {
                valores[indCtrlTxt] = sabana.GetValue("Campo" + indCtrlTxt);
                if (valores[indCtrlTxt] == null) valores[indCtrlTxt] = "";
            }
            if (sabana.Conductor != string.Empty)
                valores[195] = sabana.Conductor;
            

            //guardar registro de observaciones                
            string[] obserTot = new string[100];
            if (sabana.Observaciones.ToString() != string.Empty)
            {
                int NumLinea = 0, contador = 0;
                for (int index = 0; index < sabana.Observaciones.Length; index++)
                {
                    if (contador == 250)
                    {
                        contador = 0;
                        NumLinea = NumLinea + 1;
                    }
                    else
                    {
                        obserTot[NumLinea] = obserTot[NumLinea] + sabana.Observaciones[index].ToString().ToString();
                        contador++;
                    }
                }
                sabana.Observaciones = string.Empty;
            }
            //añadimos los valores propios del formulario una vez recogidos los campos de lectura.               

            cFunWeb.GuardarDatosenNav(valores,
                      sabana.Titulo.ToString(),                    //sabana
                      sabana.Titulo,                               //usuario
                      sabana.FechaHoraRegistro.Value,              //fecha registro       
                      sabana.FechaHoraRegistro.Value,              //hora registro
                      DateTime.Now.Date,                           //fecha sistema
                      DateTime.Now,                                //hora sistema                                           
                      obserTot);                                   //string con las bservaciones


            procesadoOk = true;

            for (int vIndLimpiar = idCampoIni; vIndLimpiar <= idCampoFin; vIndLimpiar++)
            {
                sabana.SetValue("Campo" + vIndLimpiar, string.Empty);
            }
            //borramos observaciones
            sabana.Observaciones = string.Empty;


            //recogemos los valores de los campos
            //string Campo10 = e.GetValue("Campo10");
            //string Campo11 = e.GetValue("Campo11");
            //string Campo12 = e.GetValue("Campo12");
            //string Campo13 = e.GetValue("Campo13");            

            return procesadoOk;
        }


        public void CargarTodoFormularioDatosNav(ISabana sabana, int idCampoIni, int idCampoFin, ref historicoAdquisicionConceptos.CapturaDatos_HistoricoAdquisicionConceptos[] datosRecuperados, ref bool horaExacta)
        {

            //traer idioma BD
            var culture = new CultureInfo(traerIdiomaBD());
            horaExacta = true;
            FuncionesWs.FuncionesWs funcionesProxy = new FuncionesWs.FuncionesWs();

            string FiltroFechaFormatear = sabana.FechaHoraRegistro.Value.Date.ToString().Split(' ')[0];            

            string filtrofecha = string.Empty;   
            if (CultureInfo.CurrentCulture.ToString() != "es-ES")
                filtrofecha = FiltroFechaFormatear.Split('-')[1] + "-" + FiltroFechaFormatear.Split('-')[0] + "-" + FiltroFechaFormatear.Split('-')[2];
            else
                filtrofecha = FiltroFechaFormatear.Split('/')[1] + "/" + FiltroFechaFormatear.Split('/')[0] + "/" + FiltroFechaFormatear.Split('/')[2];

            string filtroHora = sabana.FechaHoraRegistro.Value.ToString("HH:mm");           
            
            DateTime hora = Convert.ToDateTime(filtroHora);
            NegCapturaDatosWPF.historicoAdquisicionConceptos.CapturaDatos_HistoricoAdquisicionConceptos[] datoshistorico = null;                        

            funcionesProxy.traerRegistrosUltimaHora(sabana.Titulo, filtrofecha, filtroHora, ref datoshistorico);
            if (datoshistorico.Count() == 0)
            {
                //si no hay registros con la misma hora a la que nos movemos sacará los datos de uno en uno.
                datoshistorico = null;
                filtroHora = hora.AddHours(-1).ToString("HH:00") + "|" + hora.ToString("HH:00"); //sacamos la hora anterior                              
                funcionesProxy.traerRegistrosUltimaHora(sabana.Titulo, filtrofecha, filtroHora, ref datoshistorico);
                horaExacta = false;
            }

            datosRecuperados = datoshistorico;

            //traer y pintar observaciones                    
            NegCapturaDatosWPF.Observaciones.CapturaDatos_Observaciones[] arrObservaciones = null;
            funcionesProxy.TraerObservaciones(sabana.Titulo + ";" + filtrofecha + ";" + filtroHora, ref arrObservaciones);
            for (int vIndex = 0; vIndex < arrObservaciones.Length; vIndex++)
            {
                sabana.Observaciones = sabana.Observaciones + arrObservaciones[vIndex].Observaciones;
            }
        }

        public string traerIdiomaBD()
        {
            //por defecto cogemos el de la maquina a lojada
            string languaje = string.Empty;
            
            //conectarnos a la base de datos.
            return languaje;
        }
    }
   
}
