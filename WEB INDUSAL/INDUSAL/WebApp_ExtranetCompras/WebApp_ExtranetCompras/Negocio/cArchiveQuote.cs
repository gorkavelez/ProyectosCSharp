using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp_ExtranetCompras.Negocio
{
    public class cArchiveQuote:cQuote
    {
        #region PROPIEDADES

        private int _version;
        private string _fechaVersion;

        public int Version
        {
            get { return _version; }
            set { _version = value; }
        }

        public string FechaVersion
        {
            get { return _fechaVersion; }
            set { _fechaVersion = value; }
        }

        #endregion

    }
}
