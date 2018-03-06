using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp_ExtranetCompras.Negocio
{
    public class proxyEntidades
    {
        #region VARIABLES PRIVADAS

            private string _empresaLogin;
            private string _userLogin;
            private string _pwdLogin;

        #endregion

        #region PROPIEDADES

            public string EmpresaLogin
            {
                get { return _empresaLogin; }
                set { _empresaLogin = value; }
            }

            public string UserLogin
            {
                get { return _userLogin; }
                set { _userLogin = value; }
            }

            public string PwdLogin
            {
                get { return _pwdLogin; }
                set { _pwdLogin = value; }
            }

        #endregion

        #region LOGIN

            private void ObtenerDatosLogin()
            {
                WebApp_ExtranetCompras.Properties.Settings mySettings = new WebApp_ExtranetCompras.Properties.Settings();
                _userLogin = mySettings.usuarioPruebas;
                _pwdLogin = mySettings.passwordPruebas;
            }

        #endregion

        #region CONSTRUCTORES

            public proxyEntidades()
            { }

            public proxyEntidades(string empresa)
            {
                // Constructor de clase
                _empresaLogin = empresa;
                ObtenerDatosLogin();
            }

        #endregion

        #region PURCHASE QUOTES

            //public DataTable GetVendorQuotes(string vendorNo_)
            //{
            //    INIKER.PurchaseQuotes.PurchaseQuotes_Service ofertas =
            //        new INIKER.PurchaseQuotes.PurchaseQuotes_Service(_userLogin, _pwdLogin, _empresaLogin);
            //    INIKER.PurchaseQuotes.PurchaseQuotes[] listaOfertas = ofertas.ReadMultiple(vendorNo_, 0, 0, "");
            //    return (ArrayOfertasToDataTable(listaOfertas));
            //}

            //private DataTable ArrayOfertasToDataTable(INIKER.PurchaseQuotes.PurchaseQuotes[] ofertas)
            //{
            //    DataTable tabla = new DataTable("ofertas");
            //    DataColumn newColumn;
            //    DataRow newRow;

            //    newColumn = tabla.Columns.Add("Nº Documento");
            //    newColumn.DataType = System.Type.GetType("System.String");
            //    newColumn.AllowDBNull = false;

            //    newColumn = tabla.Columns.Add("Portes");
            //    newColumn.DataType = System.Type.GetType("System.String");
            //    newColumn.AllowDBNull = false;

            //    newColumn = tabla.Columns.Add("% Rappel");
            //    newColumn.DataType = System.Type.GetType("System.String");
            //    newColumn.AllowDBNull = false;

            //    newColumn = tabla.Columns.Add("Concepto Rappel");
            //    newColumn.DataType = System.Type.GetType("System.String");
            //    newColumn.AllowDBNull = false;

            //    newColumn = tabla.Columns.Add("Forma Pago");
            //    newColumn.DataType = System.Type.GetType("System.String");
            //    newColumn.AllowDBNull = false;

            //    foreach (INIKER.PurchaseQuotes.PurchaseQuotes oferta in ofertas)
            //    {                    
            //        newRow = tabla.NewRow();
            //        newRow["Portes"] = oferta.Shipment_Method_Code;
            //        newRow["% Rappel"] = oferta.Porcentaje_rappel;
            //        newRow["Concepto Rappel"] = oferta.Concepto_rappel;
            //        newRow["Forma Pago"] = oferta.Payment_Method_Code;
            //        tabla.Rows.Add(newRow);
            //    }

            //    return (tabla);
            //}

            public cQuote GetVendorQuote(string vendorNo_)
            {
                cQuote vendorQuote=null;

                try
                {
                    vendorQuote = GetQuoteHeader(vendorNo_);
                    vendorQuote.Lineas = GetQuoteLines(vendorQuote.NDocument);
                    vendorQuote.Comment = GetQuoteComment(vendorQuote.NDocument);
                }
                catch (Exception)
                { }

                return (vendorQuote);

            }

            public void UpdateQuote(cQuote quote)
            {
                try
                {
                    UpdateQuoteHeader(quote);
                    foreach (cQuote.cLineaOferta linea in quote.Lineas)
                    {
                        UpdateQuoteLine(linea, quote.NDocument);
                    }
                }
                catch (Exception)
                { }
            }

            public cArchiveQuote[] GetQuoteVersions(string quoteNo_)
            {
                cArchiveQuote[] quoteVersions = null;

                try
                {
                    quoteVersions = GetQuoteArchiveHeader(quoteNo_);
                    //vendorQuote.Comment = GetQuoteComment(vendorQuote.NDocument);
                }
                catch (Exception)
                { }

                return (quoteVersions);

            }

            public void ArchiveDocument(string quoteNo_)
            {

                INIKER.FuncionesExtranet.ExtranetCompras funciones =
                    new INIKER.FuncionesExtranet.ExtranetCompras(_userLogin, _pwdLogin, _empresaLogin);
                funciones.ArchiveQuote(quoteNo_);
            }

            public void RestoreDocument(string quoteNo_, int versionNo_)
            {

                INIKER.FuncionesExtranet.ExtranetCompras funciones =
                    new INIKER.FuncionesExtranet.ExtranetCompras(_userLogin, _pwdLogin, _empresaLogin);
                funciones.RestoreQuote(quoteNo_, versionNo_);
            }

        #endregion

        #region PURCHASE QUOTE HEADER

            private cQuote GetQuoteHeader(string vendorNo_)
            {
                INIKER.PurchaseQuotes.PurchaseQuotes_Service ofertas =
                    new INIKER.PurchaseQuotes.PurchaseQuotes_Service(_userLogin, _pwdLogin, _empresaLogin);
                INIKER.PurchaseQuotes.PurchaseQuotes[] listaOfertas = ofertas.ReadMultiple(vendorNo_, 0, 0, "");
                return (QuoteArrayToObject(listaOfertas));
            }

            private cQuote QuoteArrayToObject(INIKER.PurchaseQuotes.PurchaseQuotes[] oferta)
            {
                cQuote vendorQuote = new cQuote();

                vendorQuote.NDocument = oferta[0].No;
                vendorQuote.ShipmentMethodCode = oferta[0].Shipment_Method_Code + "";
                vendorQuote.RappelPercent = oferta[0].Porcentaje_rappel;
                vendorQuote.RappelConcept = oferta[0].Concepto_rappel + "";
                vendorQuote.PaymentMethodCode = oferta[0].Payment_Method_Code;  

                return (vendorQuote);
            }

            private void UpdateQuoteHeader(cQuote quote)
            {
                INIKER.FuncionesExtranet.ExtranetCompras funciones =
                    new INIKER.FuncionesExtranet.ExtranetCompras(_userLogin, _pwdLogin, _empresaLogin);
                funciones.UpdateQuoteHeader(quote.NDocument, quote.ShipmentMethodCode, quote.PaymentMethodCode,
                                            quote.RappelPercent, quote.RappelConcept, quote.Comment);                
            }

        #endregion

        #region PURCHASE QUOTE LINES

            //public DataTable GetQuoteLines(string quoteNo_)
            //{
            //    INIKER.PurchaseLines.PurchaseLines_Service sLineasOferta =
            //        new INIKER.PurchaseLines.PurchaseLines_Service(_userLogin, _pwdLogin, _empresaLogin);
            //    INIKER.PurchaseLines.PurchaseLines[] lineasOferta = sLineasOferta.ReadMultiple(quoteNo_, 0, 0, "");
            //    return (ArrayLineasOfertaToDataTable(lineasOferta));
            //}

            //private DataTable ArrayLineasOfertaToDataTable(INIKER.PurchaseLines.PurchaseLines[] lineasOferta)
            //{
            //    DataTable tabla = new DataTable("lineasOferta");
            //    DataColumn newColumn;
            //    DataRow newRow;

            //    newColumn = tabla.Columns.Add("Linea");
            //    newColumn.DataType = System.Type.GetType("System.Int");
            //    newColumn.AllowDBNull = false;
                
            //    newColumn = tabla.Columns.Add("Producto");
            //    newColumn.DataType = System.Type.GetType("System.String");
            //    newColumn.AllowDBNull = false;

            //    newColumn = tabla.Columns.Add("Descripcion");
            //    newColumn.DataType = System.Type.GetType("System.String");
            //    newColumn.AllowDBNull = false;

            //    newColumn = tabla.Columns.Add("Cantidad");
            //    newColumn.DataType = System.Type.GetType("System.Decimal");
            //    newColumn.AllowDBNull = false;

            //    newColumn = tabla.Columns.Add("Precio");
            //    newColumn.DataType = System.Type.GetType("System.Currency");
            //    newColumn.AllowDBNull = true;

            //    newColumn = tabla.Columns.Add("Plazo Entrega");
            //    newColumn.DataType = System.Type.GetType("System.String");
            //    newColumn.AllowDBNull = true;

            //    newColumn = tabla.Columns.Add("% Dto");
            //    newColumn.DataType = System.Type.GetType("System.Decimal");
            //    newColumn.AllowDBNull = false;

            //    foreach (INIKER.PurchaseLines.PurchaseLines linea in lineasOferta)
            //    {
            //        newRow = tabla.NewRow();
            //        newRow["Producto"] = linea.No;
            //        newRow["Descripcion"] = linea.Description;
            //        newRow["Cantidad"] = linea.Quantity;
            //        newRow["Precio"] = linea.Unit_Price_LCY;
            //        newRow["Plazo Entrega"] = linea.Lead_Time_Calculation;
            //        newRow["% Dto"] = linea.Line_Discount_Percent;
            //        tabla.Rows.Add(newRow);
            //    }

            //    return (tabla);
            //}

            private cQuote.cLineaOferta[] GetQuoteLines(string quoteNo_)
            {
                INIKER.PurchaseLines.PurchaseLines_Service sLineasOferta =
                    new INIKER.PurchaseLines.PurchaseLines_Service(_userLogin, _pwdLogin, _empresaLogin);
                INIKER.PurchaseLines.PurchaseLines[] lineasOferta = sLineasOferta.ReadMultiple(quoteNo_, 0, 0, "");
                return (QuoteLinesArrayToObject(lineasOferta));
            }

            private cQuote.cLineaOferta[] QuoteLinesArrayToObject(INIKER.PurchaseLines.PurchaseLines[] lineasOferta)
            {
                int nLineas=lineasOferta.Count();
                cQuote.cLineaOferta[] lineas=null;

                if (nLineas>0)
                {
                    lineas=new cQuote.cLineaOferta[lineasOferta.Count()];

                    for (int iLinea=0;iLinea<nLineas;iLinea++)
                    {
                        lineas[iLinea] = new cQuote.cLineaOferta();

                        lineas[iLinea].NumLinea = lineasOferta[iLinea].Line_No;                 
                        lineas[iLinea].Articulo = lineasOferta[iLinea].No;
                        lineas[iLinea].Descripcion = lineasOferta[iLinea].Description;
                        lineas[iLinea].Cantidad = lineasOferta[iLinea].Quantity;
                        lineas[iLinea].CosteUnidad = lineasOferta[iLinea].Direct_Unit_Cost;
                        lineas[iLinea].PlazoEntrega = lineasOferta[iLinea].Plazo_Entrega;
                        lineas[iLinea].DescuentoLinea = lineasOferta[iLinea].Line_Discount_Percent;                    
                    }
                }

                return (lineas);
            }

            private void UpdateQuoteLine(cQuote.cLineaOferta quoteLine, string quoteNo)
            {
                INIKER.FuncionesExtranet.ExtranetCompras funciones =
                    new INIKER.FuncionesExtranet.ExtranetCompras(_userLogin, _pwdLogin, _empresaLogin);
                funciones.UpdateQuoteLine(quoteNo, quoteLine.NumLinea, quoteLine.CosteUnidad, quoteLine.PlazoEntrega,
                                            quoteLine.DescuentoLinea);
            }

        #endregion

        #region SHIPMENT METHODS

            public DataTable GetShipmentMethods(string code_)
            {
                INIKER.ShipmentMethods.ShipmentMethods_Service metodosEnvio =
                    new INIKER.ShipmentMethods.ShipmentMethods_Service(_userLogin, _pwdLogin, _empresaLogin);
                INIKER.ShipmentMethods.ShipmentMethods[] listaMetodos = metodosEnvio.ReadMultiple(code_, 0, 0, "");
                return (ArrayMetodosToDataTable(listaMetodos));
            }

            private DataTable ArrayMetodosToDataTable(INIKER.ShipmentMethods.ShipmentMethods[] metodosEnvio)
            {
                DataTable tabla = new DataTable("metodos");
                DataColumn newColumn;
                DataRow newRow;

                newColumn = tabla.Columns.Add("Codigo");
                newColumn.DataType = System.Type.GetType("System.String");
                newColumn.AllowDBNull = false;

                newColumn = tabla.Columns.Add("Descripcion");
                newColumn.DataType = System.Type.GetType("System.String");
                newColumn.AllowDBNull = false;

                foreach (INIKER.ShipmentMethods.ShipmentMethods metodo in metodosEnvio)
                {
                    newRow = tabla.NewRow();
                    newRow["Codigo"] = metodo.Code;
                    newRow["Descripcion"] = metodo.Description;
                    tabla.Rows.Add(newRow);
                }

                return (tabla);
            }

        #endregion

        #region PAYMENT METHODS

            public DataTable GetPaymentMethods(string code_)
            {
                INIKER.PaymentMethods.PaymentMethods_Service formasPago =
                    new INIKER.PaymentMethods.PaymentMethods_Service(_userLogin, _pwdLogin, _empresaLogin);
                INIKER.PaymentMethods.PaymentMethods[] listaFormas = formasPago.ReadMultiple(code_, 0, 0, "");
                return (PaymentMethodsArrayToDataTable(listaFormas));
            }

            private DataTable PaymentMethodsArrayToDataTable(INIKER.PaymentMethods.PaymentMethods[] formasPago)
            {
                DataTable tabla = new DataTable("formasPago");
                DataColumn newColumn;
                DataRow newRow;

                newColumn = tabla.Columns.Add("Codigo");
                newColumn.DataType = System.Type.GetType("System.String");
                newColumn.AllowDBNull = false;

                newColumn = tabla.Columns.Add("Descripcion");
                newColumn.DataType = System.Type.GetType("System.String");
                newColumn.AllowDBNull = false;

                foreach (INIKER.PaymentMethods.PaymentMethods forma in formasPago)
                {
                    newRow = tabla.NewRow();
                    newRow["Codigo"] = forma.Code;
                    newRow["Descripcion"] = forma.Description;
                    tabla.Rows.Add(newRow);
                }

                return (tabla);
            }

        #endregion

        #region PURCHASE COMMENTS

            private string GetQuoteComment(string quoteNo_)
            {
                INIKER.PurchaseComment.PurchaseComment_Service sComment =
                    new INIKER.PurchaseComment.PurchaseComment_Service(_userLogin, _pwdLogin, _empresaLogin);
                INIKER.PurchaseComment.PurchaseComment[] commentLines = sComment.ReadMultiple(quoteNo_, 0, 0, "");
                return (CommentLinesToString(commentLines));
            }

            private string CommentLinesToString(INIKER.PurchaseComment.PurchaseComment[] commentLines)
            {
                string strTemp = "";

                foreach (INIKER.PurchaseComment.PurchaseComment line in commentLines)
                {
                    strTemp += line.Comment + " ";
                }

                return (strTemp);
            }

            #endregion

        #region PURCHASE QUOTE ARCHIVE HEADER

            private cArchiveQuote[] GetQuoteArchiveHeader(string quoteNo_)
            {
                INIKER.PurchaseQuoteArchives.PurchQuoteArchiveHeader_Service sArchiveQuoteHd =
                    new INIKER.PurchaseQuoteArchives.PurchQuoteArchiveHeader_Service(_userLogin, _pwdLogin, _empresaLogin);
                INIKER.PurchaseQuoteArchives.PurchQuoteArchiveHeader[] listaVersiones = sArchiveQuoteHd.ReadMultiple(quoteNo_, 0, 0, "");
                return (ArchiveQuoteArrayToObject(listaVersiones));
            }

            private cArchiveQuote[] ArchiveQuoteArrayToObject(INIKER.PurchaseQuoteArchives.PurchQuoteArchiveHeader[] listaVersiones)
            {
                cArchiveQuote[] quoteArray = new cArchiveQuote[listaVersiones.Count()];

                for (int iVersion = 0; iVersion < quoteArray.Length; iVersion++)
                {
                    quoteArray[iVersion] = new cArchiveQuote();
                    quoteArray[iVersion].NDocument = listaVersiones[iVersion].No;
                    quoteArray[iVersion].ShipmentMethodCode = listaVersiones[iVersion].Shipment_Method_Code;
                    quoteArray[iVersion].RappelPercent = listaVersiones[iVersion].Porcentaje_rappel;
                    quoteArray[iVersion].RappelConcept = listaVersiones[iVersion].Concepto_rappel;
                    quoteArray[iVersion].PaymentMethodCode = listaVersiones[iVersion].Payment_Method_Code;
                    quoteArray[iVersion].Version = listaVersiones[iVersion].Version_No;
                    quoteArray[iVersion].FechaVersion = 
                        listaVersiones[iVersion].Version_No + " - " + 
                        listaVersiones[iVersion].Date_Archived.ToShortDateString() + " - " +
                        listaVersiones[iVersion].Time_Archived.ToShortTimeString();
                }

                return (quoteArray);
            }

        #endregion

        #region PURCHASE QUOTE ARCHIVE LINES

            public cQuote.cLineaOferta[] GetQuoteArchiveLines(string quoteNo_, int versionNo_)
            {
                INIKER.PurchaseQuoteArchiveLines.PurchQuoteArchiveLines_Service sLineasOferta =
                    new INIKER.PurchaseQuoteArchiveLines.PurchQuoteArchiveLines_Service(_userLogin, _pwdLogin, _empresaLogin);
                INIKER.PurchaseQuoteArchiveLines.PurchQuoteArchiveLines[] lineasOferta = sLineasOferta.ReadMultiple(quoteNo_ + ";" + versionNo_, 0, 0, "");
                return (QuoteArchiveLinesArrayToObject(lineasOferta));
            }

            private cQuote.cLineaOferta[] QuoteArchiveLinesArrayToObject(INIKER.PurchaseQuoteArchiveLines.PurchQuoteArchiveLines[] lineasOferta)
            {
                int nLineas = lineasOferta.Count();
                cQuote.cLineaOferta[] lineas = null;

                if (nLineas > 0)
                {
                    lineas = new cQuote.cLineaOferta[lineasOferta.Count()];

                    for (int iLinea = 0; iLinea < nLineas; iLinea++)
                    {
                        lineas[iLinea] = new cQuote.cLineaOferta();

                        lineas[iLinea].NumLinea = lineasOferta[iLinea].Line_No;
                        lineas[iLinea].Articulo = lineasOferta[iLinea].No;
                        lineas[iLinea].Descripcion = lineasOferta[iLinea].Description;
                        lineas[iLinea].Cantidad = lineasOferta[iLinea].Quantity;
                        lineas[iLinea].CosteUnidad = lineasOferta[iLinea].Direct_Unit_Cost;
                        lineas[iLinea].PlazoEntrega = lineasOferta[iLinea].Plazo_Entrega;
                        lineas[iLinea].DescuentoLinea = lineasOferta[iLinea].Line_Discount_Percent;
                    }
                }

                return (lineas);
            }            

        #endregion

        #region PURCHASE ARCHIVE COMMENTS

            public string GetQuoteArchiveComment(string quoteNo_,int versionNo_)
            {
                INIKER.PurchaseArchiveComments.PurchaseArchiveComment_Service sComment =
                    new INIKER.PurchaseArchiveComments.PurchaseArchiveComment_Service(_userLogin, _pwdLogin, _empresaLogin);
                INIKER.PurchaseArchiveComments.PurchaseArchiveComment[] commentLines = sComment.ReadMultiple(quoteNo_+";"+versionNo_, 0, 0, "");
                return (CommentLinesToString(commentLines));
            }

            private string CommentLinesToString(INIKER.PurchaseArchiveComments.PurchaseArchiveComment[] commentLines)
            {
                string strTemp = "";

                foreach (INIKER.PurchaseArchiveComments.PurchaseArchiveComment line in commentLines)
                {
                    strTemp += line.Comment + " ";
                }

                return (strTemp);
            }

        #endregion

        #region LOGIN

            public string VendorLogin(string userID, string userPassword)
            {
                if (userID.Length > 0 && userPassword.Length > 0)
                {
                    INIKER.Vendor.VendorList[] vendors;
                    INIKER.Vendor.VendorList_Service sVendorList =
                        new INIKER.Vendor.VendorList_Service(_userLogin, _pwdLogin, _empresaLogin);
                    vendors = sVendorList.ReadMultiple(userID + ";" + userPassword, 0, 0, "");

                    if (vendors.Count() > 0)
                        return (vendors[0].Name);
                    else
                        return ("");
                }
                else
                {
                    return ("");
                }
            }

        #endregion

    }
}
