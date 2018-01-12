using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text.RegularExpressions;

namespace NegCapturaDatosWPF.FuncionesWs
{
    public class FuncionesWs
    {

        public void traerConceptosAdquisicion(int idConcepto, ref NegCapturaDatosWPF.AdquisicionConceptos.CapturaDatos_AdquisicionConceptos[] wsConceptos)
        {
            NegCapturaDatosWPF.AdquisicionConceptos.CapturaDatos_AdquisicionConceptos_Service wsSercvice = new NegCapturaDatosWPF.AdquisicionConceptos.CapturaDatos_AdquisicionConceptos_Service();
            wsConceptos = wsSercvice.ReadMultiple(idConcepto.ToString(), 0, 0, "");
        }

        public bool traerSabanasGruposNav(ref NegCapturaDatosWPF.SabanasGrupos.CapturaDatos_AdquisicionSabanasGrupos[] ArraySabanas, string Grupo)
        {
            NegCapturaDatosWPF.SabanasGrupos.CapturaDatos_AdquisicionSabanasGrupos_Service wsSabanas = new NegCapturaDatosWPF.SabanasGrupos.CapturaDatos_AdquisicionSabanasGrupos_Service();
            ArraySabanas = wsSabanas.ReadMultiple(Grupo, 0, 0, "");            
            return true;// SabanasGrupoaTabla(ArraySabanas, ref tSabanasGrupo);
        }

        public bool traerSabanaLectura(ref NegCapturaDatosWPF.SabanasGrupos.CapturaDatos_AdquisicionSabanasGrupos[] ArraySabanas, string Grupo, string Sabana)
        {
            string filtros = Grupo+";"+Sabana;
            NegCapturaDatosWPF.SabanasGrupos.CapturaDatos_AdquisicionSabanasGrupos_Service wsSabanas = new NegCapturaDatosWPF.SabanasGrupos.CapturaDatos_AdquisicionSabanasGrupos_Service();
            ArraySabanas = wsSabanas.ReadMultiple(filtros, 0, 0, "");
            return true;// SabanasGrupoaTabla(ArraySabanas, ref tSabanasGrupo);
        }

        public void traerRegistrosUltimaHora(string Sabana, string diaReg, string horaReg, ref NegCapturaDatosWPF.historicoAdquisicionConceptos.CapturaDatos_HistoricoAdquisicionConceptos[] arrayHistorico)
        {
            string filtros = Sabana + ";" + diaReg + ";" + horaReg;
            NegCapturaDatosWPF.historicoAdquisicionConceptos.CapturaDatos_HistoricoAdquisicionConceptos_Service wsHistorico = new NegCapturaDatosWPF.historicoAdquisicionConceptos.CapturaDatos_HistoricoAdquisicionConceptos_Service();
            arrayHistorico = wsHistorico.ReadMultiple(filtros, 0, 0, "");
        }

        public void TraerObservaciones(string idProceso, ref NegCapturaDatosWPF.Observaciones.CapturaDatos_Observaciones[] arrayObservaciones)
        {
            string filtros = idProceso;
            NegCapturaDatosWPF.Observaciones.CapturaDatos_Observaciones_Service servObesrvac = new NegCapturaDatosWPF.Observaciones.CapturaDatos_Observaciones_Service();
            arrayObservaciones = servObesrvac.ReadMultiple(filtros, 0, 0, "");
        }

        public bool SabanasGrupoaTabla(NegCapturaDatosWPF.SabanasGrupos.CapturaDatos_AdquisicionSabanasGrupos[] lineas, ref DataTable tSabanasgrupo)
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

            foreach (NegCapturaDatosWPF.SabanasGrupos.CapturaDatos_AdquisicionSabanasGrupos lineaResumen in lineas)
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
            NegCapturaDatosWPF.FuncionesCapturaDatos.FuncionesCapturaWeb FuncionesWeb = new NegCapturaDatosWPF.FuncionesCapturaDatos.FuncionesCapturaWeb();
            NegCapturaDatosWPF.historicoAdquisicionConceptos.CapturaDatos_HistoricoAdquisicionConceptos wsHistorico = new NegCapturaDatosWPF.historicoAdquisicionConceptos.CapturaDatos_HistoricoAdquisicionConceptos();
            NegCapturaDatosWPF.historicoAdquisicionConceptos.CapturaDatos_HistoricoAdquisicionConceptos_Service Servicio = new NegCapturaDatosWPF.historicoAdquisicionConceptos.CapturaDatos_HistoricoAdquisicionConceptos_Service();

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
                concepto[5] = concepto[5].Replace('.', ',');
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



    }
}