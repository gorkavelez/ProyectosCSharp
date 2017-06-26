using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text.RegularExpressions;

namespace CapturaDatosPlantaWindowsForm.FuncionesWs
{
    public class FuncionesWs
    {

        public void traerConceptosAdquisicion(int idConcepto, ref SK.AdquisicionConceptos.CapturaDatos_AdquisicionConceptos[] wsConceptos)
        {
            SK.AdquisicionConceptos.CapturaDatos_AdquisicionConceptos_Service wsSercvice = new SK.AdquisicionConceptos.CapturaDatos_AdquisicionConceptos_Service();
            wsConceptos = wsSercvice.ReadMultiple(idConcepto.ToString(), 0, 0, "");
        }

        public bool traerSabanasGruposNav(ref DataTable tSabanasGrupo, string Grupo)
        {
            SK.SabanasGrupos.CapturaDatos_AdquisicionSabanasGrupos_Service wsSabanas = new SK.SabanasGrupos.CapturaDatos_AdquisicionSabanasGrupos_Service();
            SK.SabanasGrupos.CapturaDatos_AdquisicionSabanasGrupos[] ArraySabanas = wsSabanas.ReadMultiple(Grupo, 0, 0, "");
            return SabanasGrupoaTabla(ArraySabanas, ref tSabanasGrupo);
        }

        public void traerRegistrosUltimaHora(string Sabana, string diaReg, string horaReg, ref SK.historicoAdquisicionConceptos.CapturaDatos_HistoricoAdquisicionConceptos[] arrayHistorico)
        {
            string filtros = Sabana + ";" + diaReg +";" + horaReg;            
            SK.historicoAdquisicionConceptos.CapturaDatos_HistoricoAdquisicionConceptos_Service wsHistorico = new SK.historicoAdquisicionConceptos.CapturaDatos_HistoricoAdquisicionConceptos_Service();
            arrayHistorico = wsHistorico.ReadMultiple(filtros, 0, 0, "");
        }

        public void TraerObservaciones(string idProceso, ref SK.Observaciones.CapturaDatos_Observaciones[] arrayObservaciones)
        {
            string filtros = idProceso;             
            SK.Observaciones.CapturaDatos_Observaciones_Service servObesrvac = new SK.Observaciones.CapturaDatos_Observaciones_Service();
            arrayObservaciones = servObesrvac.ReadMultiple(filtros, 0, 0, "");
        }

        public bool SabanasGrupoaTabla(SK.SabanasGrupos.CapturaDatos_AdquisicionSabanasGrupos[] lineas, ref DataTable tSabanasgrupo)
        {
            tSabanasgrupo = new DataTable("SabanasGrupo");

            DataColumn lcNewColumn;
            DataRow lcNewRow;
            bool existenLineas = false;

            lcNewColumn = tSabanasgrupo.Columns.Add("Grupo");
            lcNewColumn.DataType = System.Type.GetType("System.String");
            lcNewColumn.AllowDBNull = false;

            lcNewColumn = tSabanasgrupo.Columns.Add("Sabana");
            lcNewColumn.DataType = System.Type.GetType("System.String");
            lcNewColumn.AllowDBNull = false;

            lcNewColumn = tSabanasgrupo.Columns.Add("Descripcion");
            lcNewColumn.DataType = System.Type.GetType("System.String");
            lcNewColumn.AllowDBNull = false;

            foreach (SK.SabanasGrupos.CapturaDatos_AdquisicionSabanasGrupos lineaResumen in lineas)
            {
                lcNewRow = tSabanasgrupo.NewRow();
                lcNewRow["Grupo"] = lineaResumen.Grupo;
                lcNewRow["Sabana"] = lineaResumen.Sabana;
                lcNewRow["Descripcion"] = lineaResumen.Descripcion_sabana;
                tSabanasgrupo.Rows.Add(lcNewRow);

                existenLineas = true;
            }
            return (existenLineas);
        }

        public bool SabanasHistorico(string[] concepto)
        {
            //se reciben varios conecptos por cada textbox 
            SK.FuncionesCapturaDatos.FuncionesCapturaWeb FuncionesWeb = new SK.FuncionesCapturaDatos.FuncionesCapturaWeb();
            SK.historicoAdquisicionConceptos.CapturaDatos_HistoricoAdquisicionConceptos wsHistorico = new SK.historicoAdquisicionConceptos.CapturaDatos_HistoricoAdquisicionConceptos();
            SK.historicoAdquisicionConceptos.CapturaDatos_HistoricoAdquisicionConceptos_Service Servicio = new SK.historicoAdquisicionConceptos.CapturaDatos_HistoricoAdquisicionConceptos_Service();

            //0 Codigo,1 Sabana,2 concepto, 3 Unidad, 4 Periodicidad, 5 Valor, 6 Observaciones, 7 usuario, 8 fecha registro,                    
            FuncionesWeb.CrearRegistro(concepto[0], DateTime.Parse(concepto[8]), concepto[9]);
            wsHistorico = Servicio.Read(concepto[0], DateTime.Parse(concepto[8]), DateTime.Parse(concepto[9]));
            
            wsHistorico.Sabana = concepto[1];
            wsHistorico.Concepto = concepto[2];
            wsHistorico.Unidad = concepto[3];
            wsHistorico.Periodicidad = concepto[4];
            wsHistorico.Usuario = concepto[7];

            wsHistorico.Fecha_sistema = DateTime.Now.Date;
            wsHistorico.Ano_sistema = wsHistorico.Fecha_sistema.Year;
            wsHistorico.Mes_sistema = wsHistorico.Fecha_sistema.Month;
            wsHistorico.Dia_sistema = wsHistorico.Fecha_sistema.Day;

            wsHistorico.Hora_sistema = DateTime.Now;

            if (concepto[10].ToString() == "True")
                wsHistorico.Texto = concepto[5];
            else
            {
                Regex regEx = new Regex(@"^[a-zA-Z]");
                if (regEx.IsMatch(concepto[5]))
                    throw new Exception("El campo " + concepto[2] + " tiene un formato erróneo");                
                concepto[5]= concepto[5].Replace('.',',');                
                wsHistorico.Valor = Decimal.Parse(concepto[5]);                
            }

            wsHistorico.Ano_registro = wsHistorico.Fecha_registro.Year;
            wsHistorico.Mes_registro = wsHistorico.Fecha_registro.Month;
            wsHistorico.Dia_registro = wsHistorico.Fecha_registro.Day;

            //DateTime horaRegistro = DateTime.Parse(concepto[9]);
            //DateTime FechaPapelera = DateTime.Parse(concepto[8]);
            //FuncionesWeb.SacarFechaPapelera(ref FechaPapelera, ref horaRegistro);

            //wsHistorico.Fecha_papelera = FechaPapelera;
            //wsHistorico.Ano_papelero = wsHistorico.Fecha_papelera.Year;
            //wsHistorico.Mes_papelero = wsHistorico.Fecha_papelera.Month;
            //wsHistorico.Dia_papelero = wsHistorico.Fecha_papelera.Day;
    
            Servicio.Update(ref wsHistorico);
            return true;
        }


        public bool SetColor(System.Web.UI.WebControls.Label label, System.Drawing.Color color)
        {
            label.ForeColor = color;
            return true;
        }

    }
}