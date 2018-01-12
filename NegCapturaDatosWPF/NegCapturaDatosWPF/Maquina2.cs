using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFIntegration;


namespace CapaNegocio
{

    /// <summary>
    /// Inicializador de la sábana de la máquina 3
    /// </summary>
    [SabanaInitializer(NombreSabana = "Maquina2", Titulo = "Máquina 2")]
    public class Maquina2 : ISabanaInicializador
    {


        /// <summary>
        /// Método de inicialización
        /// </summary>
        /// <param name="sabana">Referencia a la sábana qe se inicializa</param>
        public void Inicializar(ISabana sabana)
        {
            //Inicializamos campos con la etiqueta y el tipo de datos.
            sabana.Initialize("Campo1", "TIPO PAPEL", ControlDatos.TipoDato.Numero, 1);
            sabana.Initialize("Campo2", "G/M2", ControlDatos.TipoDato.Fecha, 2);
            sabana.Initialize("Campo3", "ANCHO MAQ.", ControlDatos.TipoDato.Texto, 3);
            sabana.Initialize("Campo4", "VELOCIDAD", ControlDatos.TipoDato.Texto, 4);
            sabana.Initialize("Campo5", "PROD/HORA", ControlDatos.TipoDato.Texto, 5);

            sabana.Initialize("Campo10", "PRESIÓN CAJA", ControlDatos.TipoDato.Texto, 6);
            sabana.Initialize("Campo11", "REGLA HORIZONTAL", ControlDatos.TipoDato.Texto, 8);
            sabana.Initialize("Campo12", "CAUDAL", ControlDatos.TipoDato.Texto, 10);
            sabana.Initialize("Campo13", "REGLA VERTICAL", ControlDatos.TipoDato.Texto, 7);
            sabana.Initialize("Campo14", "P/TELA", ControlDatos.TipoDato.Texto, 9);


            //Nos asociamos al evento guardar de la sábana
            sabana.GuardarDatos += Sabana_GuardarDatos;

            //Nos asociamos al evento atrás de la sábana
            sabana.Atras += Sabana_Atras;

            //Establecemos textos( Título sábana, conductor?, observaciones?, campos? )

            sabana.Titulo = "Máquina 2";
            //sabana.SabanasMenu = new string[] { "Máquina 3", "Máquina 2", "Máquina 1" };
            sabana.SetValue("Campo14", "10");
            sabana.Conductor = "Gorka";
            //sabana.SoloLectura = true;

            sabana.FechaHoraRegistro = DateTime.Parse("21/10/17 12:15");
            sabana.CambioDeFocoDentro += Sabana_CambioDefoco;

        }

        private void Sabana_CambioDefoco(object sender, string e)
        {
            string valor = ((ISabana)sender).GetValue(e);

        }

        private void Sabana_Atras(object sender, ISabana e)
        {
            //Código para ejecutar en la acción "Atrás"
        }


        /// <summary>
        /// Guarda los datos de la sábana
        /// </summary>
        /// <param name="sender">Emisor del evento</param>
        /// <param name="e">Referencia a la sábana que tiene los datos a guardar</param>
        private void Sabana_GuardarDatos(object sender, ISabana e)
        {
            //recogemos los valores de los campos
            string Campo10 = e.GetValue("Campo10");
            string Campo11 = e.GetValue("Campo11");
            string Campo12 = e.GetValue("Campo12");
            string Campo13 = e.GetValue("Campo13");

            //Aquí irían las llamadas a la capa de negocio
            throw (new WPFIntegration.GuardarDatosException("No se ha podido guardar en Nav"));
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
                dr["Nueva"] = "sabana 2" + i;
                dr["desde"] = desde;
                dr["hasta"] = hasta;
                dt.Rows.Add(dr);
            }
            table.LoadFromDataTable(dt);
            table.Cargar();
        }

        public string[] ObtenerSabanasMenu(ISabana sabana)
        {
            return new string[] { "Máquina 3", "Máquina 2", "Máquina 1" };
        }
    }
}

