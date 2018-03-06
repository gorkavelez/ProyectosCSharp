using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace IntranetINDUSAL.Negocio
{
    public class cListado
    {

        #region VARIABLES

            private DataTable _datos;
            private string _valueColumn;
            private string _textColumn;
            private string _selectValue;
            private string _selectText;

            private object _formData;
            private string _urlBack;
            private string _datoListado;

        #endregion

        #region PROPIEDADES

            public DataTable Datos
            {
                get { return _datos; }
                set { _datos = value; }
            }
            
            public string ValueColumn
            {
                get { return _valueColumn; }
                set { _valueColumn = value; }
            }

            public string TextColumn
            {
                get { return _textColumn; }
                set { _textColumn = value; }
            }

            public string SelectValue
            {
                get { return _selectValue; }
                set { _selectValue = value; }
            }

            public string SelectText
            {
                get { return _selectText; }
                set { _selectText = value; }
            }

            public object FormData
            {
                get { return _formData; }
                set { _formData = value; }
            }

            public string UrlBack
            {
                get { return _urlBack; }
                set { _urlBack = value; }
            }

            public string DatoListado
            {
                get { return _datoListado; }
                set { _datoListado = value; }
            }

        #endregion

        #region METODOS
        
            public void LoadListBox(ref ListBox lista)
            {
                lista.Items.Clear();
                lista.DataSource = _datos;
                lista.DataValueField = _valueColumn;
                lista.DataTextField = _textColumn;
                lista.DataBind();
            }

        #endregion
    }
}
