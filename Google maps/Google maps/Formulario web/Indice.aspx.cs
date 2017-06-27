using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Text.RegularExpressions;
using System.Data;
namespace Formulario_web
{
    public partial class Indice : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //txtOrigen.Text = "calle indalecio prieto san sebastian";
            //txtDestino.Text = "plaza del deporte 3 San sebastian";
            txtOrigen.Text = "bilbao";
            txtDestino.Text = "san sebastian";
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            string UrlServicio = @"http://maps.googleapis.com/maps/api/directions/xml?origin=" + txtOrigen.Text + "&destination=" + txtDestino.Text + "&region=ES&language=es";
            
            XmlTextReader vReader = new XmlTextReader(UrlServicio);
            XmlDocument doc = new XmlDocument();
            doc.Load(vReader);
            //DirectionsResponse/route/leg/step"
            XmlNodeList nodos = doc.GetElementsByTagName("step");
            grvDatos.DataSource = meterDatosEntabla(nodos);
            grvDatos.DataBind();        
        }

        public void buscarCadaPaso()
        {


        }


        public DataTable meterDatosEntabla(XmlNodeList nodeList)
        {            
            DataTable tDatos = new DataTable();
            DataColumn lcNewColumn;
            DataRow lcNewRow;            
            
            lcNewColumn = tDatos.Columns.Add("Pasos");
            lcNewColumn.DataType = System.Type.GetType("System.String");
            lcNewColumn.AllowDBNull = false;

            lcNewColumn = tDatos.Columns.Add("Instrucciones");
            lcNewColumn.DataType = System.Type.GetType("System.String");
            lcNewColumn.AllowDBNull = false;

            lcNewColumn = tDatos.Columns.Add("distancia");
            lcNewColumn.DataType = System.Type.GetType("System.String");
            lcNewColumn.AllowDBNull = false;

            lcNewColumn = tDatos.Columns.Add("duracion");
            lcNewColumn.DataType = System.Type.GetType("System.String");
            lcNewColumn.AllowDBNull = false;

            int vIndex = 1;
            int Total = 0;
            foreach (XmlElement vNodo in nodeList)
            {
                XmlNodeList nodosVolcar = vNodo.ChildNodes;

                lcNewRow = tDatos.NewRow();
                lcNewRow["Pasos"] = vIndex.ToString();
                lcNewRow["Instrucciones"] = Regex.Replace(nodosVolcar[5].InnerText,"<.*?>",String.Empty);                

                XmlNodeList vDistance = nodosVolcar[6].ChildNodes;
                lcNewRow["distancia"] = vDistance[1].InnerText;
                Total += int.Parse(vDistance[0].InnerText);

                XmlNodeList vDuration = nodosVolcar[4].ChildNodes;
                lcNewRow["duracion"] = vDuration[1].InnerText;

                tDatos.Rows.Add(lcNewRow);
                vIndex++;
            }
            //linea de resumen
            lcNewRow = tDatos.NewRow();
            lcNewRow["Pasos"] = "";
            lcNewRow["Instrucciones"] = "";
            string vKm = string.Empty;
            if (Total > 1000)
            {
                Total = (Total / 1000);
                vKm = " kms";
            }
            else            
                vKm = "mts";
            
            lcNewRow["distancia"] = Total.ToString() + vKm;
            lcNewRow["duracion"] = "";
            tDatos.Rows.Add(lcNewRow);

            return (tDatos);
        }
    }
}