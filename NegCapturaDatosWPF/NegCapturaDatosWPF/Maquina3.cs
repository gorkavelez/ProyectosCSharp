using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFIntegration;
using System.Globalization;
using System.Threading;

namespace NegCapturaDatosWPF
{

    /// <summary>
    /// Inicializador de la sábana de la máquina 3
    /// </summary>
    [SabanaInitializer(NombreSabana = "Maquina3", Titulo = "MAQUINA3")]
    public class Maquina3 : ISabanaInicializador
    {
        //inicializar variables globales
        NegCapturaDatosWPF.historicoAdquisicionConceptos.CapturaDatos_HistoricoAdquisicionConceptos[] CamposBuscados;
        FuncionesPagina funcGenerales = new FuncionesPagina();
        bool horaExacta = false;
        int idCampoIni = 140; int idCampoFin = 196;
        bool Lectura;      
        

        public bool _Lectura
        {
            get { return Lectura; }
            set { Lectura = value; }
        }


        /// <summary>
        /// Método de inicialización
        /// </summary>
        /// <param name="sabana">Referencia a la sábana qe se inicializa</param>
        public void Inicializar(ISabana sabana)
        { 
            
            //traer registros de la base de datos.
            AdquisicionConceptos.CapturaDatos_AdquisicionConceptos_Service sConcepto = new AdquisicionConceptos.CapturaDatos_AdquisicionConceptos_Service();
            String filtros = "MAQUINA3"; //prueba en visual studio code
            AdquisicionConceptos.CapturaDatos_AdquisicionConceptos[] arrConcepto = sConcepto.ReadMultiple(filtros, 0, 0, "");
            if (arrConcepto.Count() != 0)
            {
                foreach (AdquisicionConceptos.CapturaDatos_AdquisicionConceptos concepto in arrConcepto)
                {
                    //Inicializamos campos con la etiqueta y el tipo de datos.
                    // sabana.Initialize("Campo1", "TIPO PAPEL", ControlDatos.TipoDato.Numero, 1);
                    ControlDatos.TipoDato vTipoDatos = new ControlDatos.TipoDato();

                    switch (concepto.Tipo_Campo)
                    {
                        case AdquisicionConceptos.Tipo_Campo.Fecha:
                            vTipoDatos = ControlDatos.TipoDato.Fecha;
                            sabana.Initialize("Campo" + concepto.Codigo, concepto.Decripcion, vTipoDatos, concepto.Orden_Tabulacion, concepto.Ordenacion_Caja, concepto.Caja, concepto.Obligatorio, Lectura);
                            break;

                        case AdquisicionConceptos.Tipo_Campo.Numero:
                            vTipoDatos = ControlDatos.TipoDato.Numero;
                            sabana.Initialize("Campo" + concepto.Codigo, concepto.Decripcion, vTipoDatos, concepto.Orden_Tabulacion, concepto.Ordenacion_Caja, concepto.Caja, concepto.Obligatorio, Lectura, double.Parse(concepto.Limite_Inferior), double.Parse(concepto.Limite_Superior));
                            break;

                        case AdquisicionConceptos.Tipo_Campo.Texto:
                            vTipoDatos = ControlDatos.TipoDato.Texto;
                            sabana.Initialize("Campo" + concepto.Codigo, concepto.Decripcion, vTipoDatos, concepto.Orden_Tabulacion, concepto.Ordenacion_Caja, concepto.Caja, concepto.Obligatorio, Lectura);
                            break;

                        case AdquisicionConceptos.Tipo_Campo.Hora:
                            vTipoDatos = ControlDatos.TipoDato.Hora;
                            sabana.Initialize("Campo" + concepto.Codigo, concepto.Decripcion, vTipoDatos, concepto.Orden_Tabulacion, concepto.Ordenacion_Caja, concepto.Caja, concepto.Obligatorio, Lectura);
                            break;
                    }    
                    //Inicializamos campos con la etiqueta y el tipo de datos.
                    //sabana.Initialize("Campo1", "TIPO PAPEL", ControlDatos.TipoDato.Numero, 1, 0, "Agrupacion0", true);                        
                }
                                
                //Nos asociamos al evento guardar de la sábana
                sabana.GuardarDatos += Sabana_GuardarDatos;

                //Nos asociamos al evento atrás de la sábana
                //sabana.Atras += Sabana_Atras;

                //Establecemos textos( Título sábana, conductor?, observaciones?, campos? )
                sabana.Titulo = "MAQUINA3";
                sabana.SoloLectura = SabanaEsLectura(sabana);


                //if (CultureInfo.CurrentCulture.ToString() != "es-ES")
                //    Thread.CurrentThread.CurrentCulture = new CultureInfo("nl-NL");
                sabana.FechaHoraRegistro = DateTime.Parse(DateTime.Now.ToString("dd/MM/yyyy HH:00").Trim());                
                //sabana.FechaHoraRegistro = DateTime.Now;

                sabana.CambioFecha += Sabana_CambioFecha;

                //Obtener la instancia de un campo de captura de datos.
                //var d = sabana.ObtenerCampo("Campo140");                
                sabana.CambioDeFocoDentro += Sabana_CambioDefoco;
            }
            //Inicializamos campos con la etiqueta y el tipo de datos.
            //sabana.Initialize("Campo1", "TIPO PAPEL", ControlDatos.TipoDato.Numero, 1, 0, "Agrupacion0", true);

            MostrarDatosSabana(sabana);            
        }

        private void Sabana_CambioFecha(object sender, CambioFechaArgs e)
        {
            try
            {
                //Guardamos con fecha anterior y mostraoms con fecha siguiente.                
                var sabana = ((ISabana)sender);
                if (!sabana.EsValida())
                    throw new Exception("No se ha podido guardar, revise los campos marcados en rojo");

                sabana.FechaHoraRegistro = e.FechaAnterior;
                funcGenerales.guardarDatosHistoricos(sabana, idCampoIni, idCampoFin);

                sabana.FechaHoraRegistro = e.FechaNueva;
                MostrarDatosSabana(sabana);
            }
            catch(Exception ex)
            {
                throw(new WPFIntegration.GuardarDatosException(ex.Message));
            }            
        }

        /// <summary>
        /// Guarda los datos de la sábana y los muestra
        /// </summary>
        /// <param name="sender">Emisor del evento</param>
        /// <param name="e">Referencia a la sábana que tiene los datos a guardar</param>
        private void Sabana_GuardarDatos(object sender, ISabana e)
        {
            //string Campo10 = e.GetValue("Campo10");
            GuardarMostrarSabana(e);           
        }
              

        private void Sabana_CambioDefoco(object sender, string e)
        {            
            string valor = ((ISabana)sender).GetValue(e);
            if (valor != string.Empty)
                return;
                
            var sabana = (ISabana)sender;
            string numCampo = e.ToString().Remove(0, 5);
            var d = sabana.ObtenerCampo("Campo" + numCampo);
            try
            {
                var datoHistoricoRecuperado = CamposBuscados.Where(datoHist => datoHist.Codigo == numCampo);
                if (datoHistoricoRecuperado.Count() != 0)
                {                                        
                    switch (d.TipoDato)
                    { 
                        case ControlDatos.TipoDato.Fecha:
                            sabana.SetValue("Campo" + numCampo, datoHistoricoRecuperado.Last().Fecha.ToString());
                            break;

                        case ControlDatos.TipoDato.Hora:
                            sabana.SetValue("Campo" + numCampo, datoHistoricoRecuperado.Last().Hora.ToString());
                            break;

                        case ControlDatos.TipoDato.Numero:
                            sabana.SetValue("Campo" + numCampo, datoHistoricoRecuperado.Last().Valor.ToString());
                            break;

                        case ControlDatos.TipoDato.Texto:
                            sabana.SetValue("Campo" + numCampo, datoHistoricoRecuperado.Last().Texto);
                            break;
                    }                    
                }       
              //  ((ISabana)sender).SetValue("Campo"+e.ToString().Remove(0, 5), CamposBuscados[int.Parse(numCampo)-1].Valor.ToString());
            }
            catch { }                      
        }

        private void Sabana_Atras(object sender, ISabana e)
        {
            //Código para ejecutar en la acción "Atrás"
        }             
      


        public void LlamarDatosLista(DateTime desde, DateTime hasta, WPFIntegration.Data.ColeccionDatos table)
        {
            System.Data.DataTable dt = new DataTable();               
            dt.Columns.Add("Nueva");
            dt.Columns.Add("desde");
            dt.Columns.Add("hasta");
            for (int i = 0; i < 1000; i++)
            {
                var dr = dt.NewRow();
                dr["Nueva"] = "Ok" + i;
                dr["desde"] = desde;
                dr["hasta"] = hasta;
                dt.Rows.Add(dr);
            }
            table.LoadFromDataTable(dt);
            table.Cargar();
        }

        
        public string[] ObtenerSabanasMenu(ISabana sabana)
        {
            NegCapturaDatosWPF.SabanasGrupos.CapturaDatos_AdquisicionSabanasGrupos[] ArraySabanas = null;
            NegCapturaDatosWPF.FuncionesWs.FuncionesWs funWebService = new NegCapturaDatosWPF.FuncionesWs.FuncionesWs();     
            string codGrupo = sabana.UsuarioActual.Properties.ContainsKey("CodGrupo") ? sabana.UsuarioActual.Properties["CodGrupo"].ToString() : "";
            funWebService.traerSabanasGruposNav(ref ArraySabanas, codGrupo);

            List<string> StrListSabanas = new List<string>();
            string SabanaNombre = string.Empty;            

            foreach (NegCapturaDatosWPF.SabanasGrupos.CapturaDatos_AdquisicionSabanasGrupos Sabana in ArraySabanas)
            {
                SabanaNombre = Sabana.Sabana;
                StrListSabanas.Add(SabanaNombre);
            }
            string[] StrArrSabanas = StrListSabanas.ToArray();
            return StrArrSabanas;
            //return new string[] { "MAQUINA3", "Máquina 2", "Máquina 1" };
            
        }

        #region funciones
        /// <summary>
        /// funcion para guardar y mostar los datos de saban
        /// </summary>
        /// <param name="sabana"></param>
        public void GuardarMostrarSabana(ISabana sabana)
        {            
            if (!sabana.EsValida())
                throw( new WPFIntegration.GuardarDatosException("No se ha podido guardar, revise los campos marcados en rojo"));

            if (funcGenerales.guardarDatosHistoricos(sabana, idCampoIni, idCampoFin))
                MostrarDatosSabana(sabana);            
        }

        public bool SabanaEsLectura(ISabana sabana)
        {
            bool Lectura = false;            
            NegCapturaDatosWPF.SabanasGrupos.CapturaDatos_AdquisicionSabanasGrupos[] ArraySabanas = null;
            NegCapturaDatosWPF.FuncionesWs.FuncionesWs funWebService = new NegCapturaDatosWPF.FuncionesWs.FuncionesWs();
            string codGrupo = sabana.UsuarioActual.Properties.ContainsKey("CodGrupo") ? sabana.UsuarioActual.Properties["CodGrupo"].ToString() : "";
            funWebService.traerSabanaLectura(ref ArraySabanas, codGrupo, sabana.Titulo);
            Lectura = ArraySabanas[0].Lectura; 
            return Lectura;
        }


        /// <summary>
        /// mostrar la sabana unicamente sin guardado
        /// </summary>
        /// <param name="sabana"></param>
        public void MostrarDatosSabana(ISabana sabana)
        {

            //si no hay hora exacta limpiamos los campos.
            sabana.Conductor = "";
            for (int vIndLimpiar = idCampoIni; vIndLimpiar <= idCampoFin; vIndLimpiar++)
            {
                sabana.SetValue("Campo" + vIndLimpiar, string.Empty);
            }            

            funcGenerales.CargarTodoFormularioDatosNav(sabana, idCampoIni, idCampoFin, ref CamposBuscados, ref horaExacta);
            if (horaExacta)
            {
                int index = idCampoIni;
                foreach (historicoAdquisicionConceptos.CapturaDatos_HistoricoAdquisicionConceptos conceptoHist in CamposBuscados)
                {
                    var varTipoCampo = sabana.ObtenerCampo("Campo" + conceptoHist.Codigo);
                    if (varTipoCampo != null)
                    {
                        switch (varTipoCampo.TipoDato)
                        {
                            case ControlDatos.TipoDato.Fecha:
                                sabana.SetValue("Campo" + conceptoHist.Codigo, conceptoHist.Fecha.ToString());
                                break;

                            case ControlDatos.TipoDato.Hora:
                                sabana.SetValue("Campo" + conceptoHist.Codigo, conceptoHist.Hora.ToString());
                                break;

                            case ControlDatos.TipoDato.Numero:
                                sabana.SetValue("Campo" + conceptoHist.Codigo, conceptoHist.Valor.ToString());
                                break;

                            case ControlDatos.TipoDato.Texto:
                                sabana.SetValue("Campo" + conceptoHist.Codigo, conceptoHist.Texto);
                                break;
                        }
                        index++;
                    }                    
                    if (conceptoHist.Concepto == "CONDUCTOR")                        
                        sabana.Conductor = conceptoHist.Texto;
                }
            }
            else
            {
                //borramos observaciones
                sabana.Observaciones = string.Empty;

            }
        }             

        #endregion
    }
}

