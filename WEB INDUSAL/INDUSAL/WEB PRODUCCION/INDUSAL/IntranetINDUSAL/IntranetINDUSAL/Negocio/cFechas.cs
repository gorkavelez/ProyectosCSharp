using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IntranetINDUSAL.Negocio
{
    public class cFechas
    {
        #region TIPOS Y CONSTANTES

        private enum diasSemana
        {
            Domingo = 0,
            Lunes = 1,
            Martes = 2,
            Miercoles = 3,
            Jueves = 4,
            Viernes = 5,
            Sabado = 6
        }
           
        
        #endregion

        #region VARIABLES PRIVADAS
        
        private DataTable _dtFechasEntrega;
        private DateTime _fecha;
        private int _diasCalcular;
        private string _fechaNula;

        #endregion

        #region PROPIEDADES

        public int DiasCalcular
        {
            get { return _diasCalcular; }
            set { _diasCalcular = value; }
        }

        public DateTime Fecha
        {
            get { return _fecha; }
            set { _fecha = value; }
        }

        public DataTable dtFechasEntrega
        {
            get { return _dtFechasEntrega; }
            set { _dtFechasEntrega = value; }
        }

        public string FechaNula
        {
            get { return _fechaNula; }
            set { _fechaNula = value; }
        } 

        #endregion

        #region METODOS PUBLICOS

        public cFechas(DateTime fecha, int dias, string fNula)
        {
            this._fecha = fecha;
            this._diasCalcular = dias;
            this._fechaNula = fNula;

            GenerarDataTable();
            CalcularFechas();
        }

        public void LoadDropDownList(ref System.Web.UI.WebControls.DropDownList oDDL)
        {
            oDDL.Items.Clear();
            oDDL.DataSource = _dtFechasEntrega;
            oDDL.DataValueField = _dtFechasEntrega.Columns["fecha"].ToString();
            oDDL.DataTextField = _dtFechasEntrega.Columns["nombre"].ToString();
            oDDL.DataBind();
            
            oDDL.SelectedIndex = oDDL.Items.Count - 1;
        }

        public void RecalcularFechas()
        {
            CalcularFechas();
        }

        #endregion

        #region METODOS PRIVADOS

        private void GenerarDataTable()
        {
            _dtFechasEntrega = new DataTable("fechasEntrega");
            DataColumn newColumn;

            newColumn = _dtFechasEntrega.Columns.Add("fecha");
            newColumn.DataType = Type.GetType("System.String");
            newColumn.AllowDBNull = false;

            newColumn = _dtFechasEntrega.Columns.Add("nombre");
            newColumn.DataType = Type.GetType("System.String");
            newColumn.AllowDBNull = true;
        }

        private void AddDate(DateTime fecha, string nombre)
        {
            DataRow newRow = _dtFechasEntrega.NewRow();
            newRow["fecha"] = fecha.ToShortDateString();
            newRow["nombre"] = nombre;
            _dtFechasEntrega.Rows.Add(newRow);
        }

        private void ClearDates()
        {
            _dtFechasEntrega.Clear();
        }

        /// <summary> Función que calcula días sucesivos a partir de una fecha
        /// <remarks>Función que calcula días sucesivos a partir de una fecha</remarks>
        /// </summary>
        private void CalcularFechas()
        {
            DateTime nuevaFecha;
            string nombreDia;

            for (int dia = 0; dia <= _diasCalcular; dia++)
            {
                nuevaFecha = _fecha.AddDays(dia);
                //nombreDia = nuevaFecha.DayOfWeek.ToString();
                if (dia == 0)
                {
                    nombreDia = "Hoy";
                }
                else
                {
                    nombreDia = DiaSemana((int)nuevaFecha.DayOfWeek);
                }
                AddDate(nuevaFecha, nombreDia);
            }
            AddDate(DateTime.Parse(_fechaNula), "");
        }

        private string DiaSemana(int ordinal)
        {
            diasSemana dia=0;

            switch (ordinal)
            {
                case 0:
                    dia = diasSemana.Domingo;
                    break;
                case 1:
                    dia = diasSemana.Lunes;
                    break;
                case 2:
                    dia = diasSemana.Martes;
                    break;
                case 3:
                    dia = diasSemana.Miercoles;
                    break;
                case 4:
                    dia = diasSemana.Jueves;
                    break;
                case 5:
                    dia = diasSemana.Viernes;
                    break;
                case 6:
                    dia = diasSemana.Sabado;
                    break;
                    
            }
            return dia.ToString();
        }

        #endregion




    }
}
