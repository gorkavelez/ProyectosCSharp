using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp_ExtranetCompras.Negocio
{
    public class cQuote
    {
        #region PROPIEDADES

        // variables
        private string _nDocument;
        private string _shipmentMethodCode;
        private string _paymentMethodCode;
        private decimal _rappelPercent;
        private string _rappelConcept;
        private string _comment;

        private cLineaOferta[] _lineas;
        private cArchiveQuote[] _versiones;

        private DataTable _paymentMethods;

        private DataTable _shipmentMethods;
        

        public string NDocument
        {
            get { return _nDocument; }
            set { _nDocument = value; }
        }

        public string ShipmentMethodCode
        {
            get { return _shipmentMethodCode; }
            set { _shipmentMethodCode = value; }
        }

        public string PaymentMethodCode
        {
            get { return _paymentMethodCode; }
            set { _paymentMethodCode = value; }
        }

        public decimal RappelPercent
        {
            get { return _rappelPercent; }
            set { _rappelPercent = value; }
        }

        public string RappelConcept
        {
            get { return _rappelConcept; }
            set { _rappelConcept = value; }
        }

        public string Comment
        {
            get { return _comment; }
            set { _comment = value; }
        }

        public cLineaOferta[] Lineas
        {
            get { return _lineas; }
            set { _lineas = value; }
        }

        public cArchiveQuote[] Versiones
        {
            get { return _versiones; }
            set { _versiones = value; }
        }

        public DataTable PaymentMethods
        {
            get { return _paymentMethods; }
            set { _paymentMethods = value; }
        }

        public DataTable ShipmentMethods
        {
            get { return _shipmentMethods; }
            set { _shipmentMethods = value; }
        }

        #endregion

        #region CONSTRUCTOR

            public cQuote()
            {
                _nDocument="";
                _shipmentMethodCode="";
                _paymentMethodCode="";
                _rappelPercent=0;
                _rappelConcept="";
                _comment="";                
            }

        #endregion

        #region METODOS PUBLICOS

            public void UpdateLine(int lineNo, int editProperty, string value)
            {
                foreach (cLineaOferta linea in _lineas)
                {
                    if (linea.NumLinea == lineNo)
                    {
                        switch (editProperty)
                        {
                            case 0:
                                linea.CosteUnidad= decimal.Parse(value);
                                break;
                            case 1:
                                linea.PlazoEntrega= int.Parse(value);
                                break;
                            case 2:
                                linea.DescuentoLinea = decimal.Parse(value);
                                break;
                        }
                        return;
                    }
                }
            }

            public cArchiveQuote SelectVersion(int versionID_)
            {
                try
                {
                    foreach (cArchiveQuote archivo in _versiones)
                    {
                        if (archivo.Version == versionID_)
                        {
                            return (archivo);
                        }
                    }
                }
                catch (Exception)
                { }

                return (null);
            }

            public string GetDescription(DataTable sourceTable, string code)
            {
                try
                {
                    DataRow[] filtro = sourceTable.Select("codigo='" + code + "'");
                    return (filtro[0]["Descripcion"].ToString());
                }
                catch (Exception)
                {
                    return ("");
                }
            }
        #endregion

        #region METODOS PRIVADOS
        #endregion


        #region LINEAS DE OFERTA

        public class cLineaOferta
        {
            #region PROPIEDADES

            private int _lineNo;
            private string _itemNo;
            private string _description;
            private decimal _quantity;
            private decimal _unitCost;
            private int _plazoEntrega;
            private decimal _lineDiscountPercent;

            //private DataTable _quoteLines;

            public int NumLinea
            {
                get { return _lineNo; }
                set { _lineNo = value; }
            }

            public string Articulo
            {
                get { return _itemNo; }
                set { _itemNo = value; }
            }

            public string Descripcion
            {
                get { return _description; }
                set { _description = value; }
            }

            public decimal Cantidad
            {
                get { return _quantity; }
                set { _quantity = value; }
            }

            public decimal CosteUnidad
            {
                get { return _unitCost; }
                set { _unitCost = value; }
            }

            public int PlazoEntrega
            {
                get { return _plazoEntrega; }
                set { _plazoEntrega = value; }
            }

            public decimal DescuentoLinea
            {
                get { return _lineDiscountPercent; }
                set { _lineDiscountPercent = value; }
            }

            //public DataTable QuoteLines
            //{
            //    get { return _quoteLines; }
            //    set { _quoteLines = value; }
            //}

            #endregion

            #region CONSTRUCTOR

            public cLineaOferta()
            {
                _lineNo=0;
                _itemNo="";
                _description="";
                _quantity=0;
                _unitCost=0;
                _plazoEntrega =0;
                _lineDiscountPercent=0;
            }

            #endregion
        }

        #endregion
    }
}
