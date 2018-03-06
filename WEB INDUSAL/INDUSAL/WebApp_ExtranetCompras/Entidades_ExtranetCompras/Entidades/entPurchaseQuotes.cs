﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión del motor en tiempo de ejecución:2.0.50727.3603
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Serialization;
using System.Net;

// 
// This source code was auto-generated by wsdl, Version=2.0.50727.42.
// 

namespace INIKER.PurchaseQuotes
{

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name = "PurchaseQuotes_Binding", Namespace = "urn:microsoft-dynamics-schemas/page/purchasequotes")]
    public partial class PurchaseQuotes_Service : System.Web.Services.Protocols.SoapHttpClientProtocol
    {

        private System.Threading.SendOrPostCallback ReadOperationCompleted;

        private System.Threading.SendOrPostCallback ReadMultipleOperationCompleted;

        private System.Threading.SendOrPostCallback IsUpdatedOperationCompleted;

        /// <remarks/>
        public PurchaseQuotes_Service()
        {
            Entidades_ExtranetCompras.Properties.Settings miConfig = new Entidades_ExtranetCompras.Properties.Settings();
            this.Url = miConfig.urlInicioDefaultWS + "/Page/PurchaseQuotes?WSDL";
            this.UseDefaultCredentials = true;
            //this.Url = "http://serv-nsql2008:7047/GINDUSAL/WS/02%20NAVARRA/Page/PurchaseQuotes?WSDL";
            //this.Credentials = new NetworkCredential("JCA", "Iniker1");
        }

        public PurchaseQuotes_Service(string IDusuario, string pwd)
        {
            // Este constructor se utiliza para obtener el nombre de la empresa con la que
            // trabaja el usuario que ha hecho login en el equipo.
            Entidades_ExtranetCompras.Properties.Settings miConfig = new Entidades_ExtranetCompras.Properties.Settings();
            this.Url = miConfig.urlInicioWS + "/Page/PurchaseQuotes?WSDL";
            if (miConfig.useDefCredts)
                this.UseDefaultCredentials = true;
            else
                this.Credentials = new NetworkCredential(IDusuario, pwd);
        }

        public PurchaseQuotes_Service(string IDusuario, string pwd, string empresa)
        {
            // Constructor para trabajar contra los web services una vez obtenida la empresa de trabajo
            Entidades_ExtranetCompras.Properties.Settings miConfig = new Entidades_ExtranetCompras.Properties.Settings();
            this.Url = miConfig.urlInicioWS + "/" + empresa + "/Page/PurchaseQuotes?WSDL";
            if (miConfig.useDefCredts)
                this.UseDefaultCredentials = true;
            else
                this.Credentials = new NetworkCredential(IDusuario, pwd);
        }

        /// <remarks/>
        public event ReadCompletedEventHandler ReadCompleted;

        /// <remarks/>
        public event ReadMultipleCompletedEventHandler ReadMultipleCompleted;

        /// <remarks/>
        public event IsUpdatedCompletedEventHandler IsUpdatedCompleted;

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:microsoft-dynamics-schemas/page/purchasequotes:Read", RequestNamespace = "urn:microsoft-dynamics-schemas/page/purchasequotes", ResponseElementName = "Read_Result", ResponseNamespace = "urn:microsoft-dynamics-schemas/page/purchasequotes", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("PurchaseQuotes")]
        public PurchaseQuotes Read(string No)
        {
            object[] results = this.Invoke("Read", new object[] {
                    No});
            return ((PurchaseQuotes)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginRead(string No, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("Read", new object[] {
                    No}, callback, asyncState);
        }

        /// <remarks/>
        public PurchaseQuotes EndRead(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((PurchaseQuotes)(results[0]));
        }

        /// <remarks/>
        public void ReadAsync(string No)
        {
            this.ReadAsync(No, null);
        }

        /// <remarks/>
        public void ReadAsync(string No, object userState)
        {
            if ((this.ReadOperationCompleted == null))
            {
                this.ReadOperationCompleted = new System.Threading.SendOrPostCallback(this.OnReadOperationCompleted);
            }
            this.InvokeAsync("Read", new object[] {
                    No}, this.ReadOperationCompleted, userState);
        }

        private void OnReadOperationCompleted(object arg)
        {
            if ((this.ReadCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ReadCompleted(this, new ReadCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:microsoft-dynamics-schemas/page/purchasequotes:ReadMultiple", RequestNamespace = "urn:microsoft-dynamics-schemas/page/purchasequotes", ResponseElementName = "ReadMultiple_Result", ResponseNamespace = "urn:microsoft-dynamics-schemas/page/purchasequotes", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlArrayAttribute("ReadMultiple_Result")]
        [return: System.Xml.Serialization.XmlArrayItemAttribute(IsNullable = false)]
        public PurchaseQuotes[] ReadMultiple([System.Xml.Serialization.XmlElementAttribute("filter")] PurchaseQuotes_Filter[] filter, string bookmarkKey, int setSize)
        {
            object[] results = this.Invoke("ReadMultiple", new object[] {
                    filter,
                    bookmarkKey,
                    setSize});
            return ((PurchaseQuotes[])(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginReadMultiple(PurchaseQuotes_Filter[] filter, string bookmarkKey, int setSize, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("ReadMultiple", new object[] {
                    filter,
                    bookmarkKey,
                    setSize}, callback, asyncState);
        }

        /// <remarks/>
        public PurchaseQuotes[] EndReadMultiple(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((PurchaseQuotes[])(results[0]));
        }

        /// <remarks/>
        public void ReadMultipleAsync(PurchaseQuotes_Filter[] filter, string bookmarkKey, int setSize)
        {
            this.ReadMultipleAsync(filter, bookmarkKey, setSize, null);
        }

        /// <remarks/>
        public void ReadMultipleAsync(PurchaseQuotes_Filter[] filter, string bookmarkKey, int setSize, object userState)
        {
            if ((this.ReadMultipleOperationCompleted == null))
            {
                this.ReadMultipleOperationCompleted = new System.Threading.SendOrPostCallback(this.OnReadMultipleOperationCompleted);
            }
            this.InvokeAsync("ReadMultiple", new object[] {
                    filter,
                    bookmarkKey,
                    setSize}, this.ReadMultipleOperationCompleted, userState);
        }

        private void OnReadMultipleOperationCompleted(object arg)
        {
            if ((this.ReadMultipleCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ReadMultipleCompleted(this, new ReadMultipleCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:microsoft-dynamics-schemas/page/purchasequotes:IsUpdated", RequestNamespace = "urn:microsoft-dynamics-schemas/page/purchasequotes", ResponseElementName = "IsUpdated_Result", ResponseNamespace = "urn:microsoft-dynamics-schemas/page/purchasequotes", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("IsUpdated_Result")]
        public bool IsUpdated(string Key)
        {
            object[] results = this.Invoke("IsUpdated", new object[] {
                    Key});
            return ((bool)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginIsUpdated(string Key, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("IsUpdated", new object[] {
                    Key}, callback, asyncState);
        }

        /// <remarks/>
        public bool EndIsUpdated(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((bool)(results[0]));
        }

        /// <remarks/>
        public void IsUpdatedAsync(string Key)
        {
            this.IsUpdatedAsync(Key, null);
        }

        /// <remarks/>
        public void IsUpdatedAsync(string Key, object userState)
        {
            if ((this.IsUpdatedOperationCompleted == null))
            {
                this.IsUpdatedOperationCompleted = new System.Threading.SendOrPostCallback(this.OnIsUpdatedOperationCompleted);
            }
            this.InvokeAsync("IsUpdated", new object[] {
                    Key}, this.IsUpdatedOperationCompleted, userState);
        }

        private void OnIsUpdatedOperationCompleted(object arg)
        {
            if ((this.IsUpdatedCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.IsUpdatedCompleted(this, new IsUpdatedCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        public new void CancelAsync(object userState)
        {
            base.CancelAsync(userState);
        }
    }

    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:microsoft-dynamics-schemas/page/purchasequotes")]
    public partial class PurchaseQuotes
    {

        private string keyField;

        private string noField;

        private string buy_from_Vendor_NoField;

        private string order_Address_CodeField;

        private string buy_from_Vendor_NameField;

        private string vendor_Authorization_NoField;

        private string buy_from_Post_CodeField;

        private string buy_from_Country_Region_CodeField;

        private string buy_from_ContactField;

        private string pay_to_Vendor_NoField;

        private string pay_to_NameField;

        private string pay_to_Post_CodeField;

        private string pay_to_Country_Region_CodeField;

        private string pay_to_ContactField;

        private string ship_to_CodeField;

        private string ship_to_NameField;

        private string ship_to_Post_CodeField;

        private string ship_to_Country_Region_CodeField;

        private string ship_to_ContactField;

        private System.DateTime posting_DateField;

        private bool posting_DateFieldSpecified;

        private string shortcut_Dimension_1_CodeField;

        private string shortcut_Dimension_2_CodeField;

        private string location_CodeField;

        private string purchaser_CodeField;

        private string assigned_User_IDField;

        private string currency_CodeField;

        private System.DateTime document_DateField;

        private bool document_DateFieldSpecified;

        private string campaign_NoField;

        private Status statusField;

        private bool statusFieldSpecified;

        private string shipment_Method_CodeField;

        private string payment_Method_CodeField;

        private decimal porcentaje_rappelField;

        private bool porcentaje_rappelFieldSpecified;

        private string concepto_rappelField;

        /// <comentarios/>
        public string Key
        {
            get
            {
                return this.keyField;
            }
            set
            {
                this.keyField = value;
            }
        }

        /// <comentarios/>
        public string No
        {
            get
            {
                return this.noField;
            }
            set
            {
                this.noField = value;
            }
        }

        /// <comentarios/>
        public string Buy_from_Vendor_No
        {
            get
            {
                return this.buy_from_Vendor_NoField;
            }
            set
            {
                this.buy_from_Vendor_NoField = value;
            }
        }

        /// <comentarios/>
        public string Order_Address_Code
        {
            get
            {
                return this.order_Address_CodeField;
            }
            set
            {
                this.order_Address_CodeField = value;
            }
        }

        /// <comentarios/>
        public string Buy_from_Vendor_Name
        {
            get
            {
                return this.buy_from_Vendor_NameField;
            }
            set
            {
                this.buy_from_Vendor_NameField = value;
            }
        }

        /// <comentarios/>
        public string Vendor_Authorization_No
        {
            get
            {
                return this.vendor_Authorization_NoField;
            }
            set
            {
                this.vendor_Authorization_NoField = value;
            }
        }

        /// <comentarios/>
        public string Buy_from_Post_Code
        {
            get
            {
                return this.buy_from_Post_CodeField;
            }
            set
            {
                this.buy_from_Post_CodeField = value;
            }
        }

        /// <comentarios/>
        public string Buy_from_Country_Region_Code
        {
            get
            {
                return this.buy_from_Country_Region_CodeField;
            }
            set
            {
                this.buy_from_Country_Region_CodeField = value;
            }
        }

        /// <comentarios/>
        public string Buy_from_Contact
        {
            get
            {
                return this.buy_from_ContactField;
            }
            set
            {
                this.buy_from_ContactField = value;
            }
        }

        /// <comentarios/>
        public string Pay_to_Vendor_No
        {
            get
            {
                return this.pay_to_Vendor_NoField;
            }
            set
            {
                this.pay_to_Vendor_NoField = value;
            }
        }

        /// <comentarios/>
        public string Pay_to_Name
        {
            get
            {
                return this.pay_to_NameField;
            }
            set
            {
                this.pay_to_NameField = value;
            }
        }

        /// <comentarios/>
        public string Pay_to_Post_Code
        {
            get
            {
                return this.pay_to_Post_CodeField;
            }
            set
            {
                this.pay_to_Post_CodeField = value;
            }
        }

        /// <comentarios/>
        public string Pay_to_Country_Region_Code
        {
            get
            {
                return this.pay_to_Country_Region_CodeField;
            }
            set
            {
                this.pay_to_Country_Region_CodeField = value;
            }
        }

        /// <comentarios/>
        public string Pay_to_Contact
        {
            get
            {
                return this.pay_to_ContactField;
            }
            set
            {
                this.pay_to_ContactField = value;
            }
        }

        /// <comentarios/>
        public string Ship_to_Code
        {
            get
            {
                return this.ship_to_CodeField;
            }
            set
            {
                this.ship_to_CodeField = value;
            }
        }

        /// <comentarios/>
        public string Ship_to_Name
        {
            get
            {
                return this.ship_to_NameField;
            }
            set
            {
                this.ship_to_NameField = value;
            }
        }

        /// <comentarios/>
        public string Ship_to_Post_Code
        {
            get
            {
                return this.ship_to_Post_CodeField;
            }
            set
            {
                this.ship_to_Post_CodeField = value;
            }
        }

        /// <comentarios/>
        public string Ship_to_Country_Region_Code
        {
            get
            {
                return this.ship_to_Country_Region_CodeField;
            }
            set
            {
                this.ship_to_Country_Region_CodeField = value;
            }
        }

        /// <comentarios/>
        public string Ship_to_Contact
        {
            get
            {
                return this.ship_to_ContactField;
            }
            set
            {
                this.ship_to_ContactField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime Posting_Date
        {
            get
            {
                return this.posting_DateField;
            }
            set
            {
                this.posting_DateField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool Posting_DateSpecified
        {
            get
            {
                return this.posting_DateFieldSpecified;
            }
            set
            {
                this.posting_DateFieldSpecified = value;
            }
        }

        /// <comentarios/>
        public string Shortcut_Dimension_1_Code
        {
            get
            {
                return this.shortcut_Dimension_1_CodeField;
            }
            set
            {
                this.shortcut_Dimension_1_CodeField = value;
            }
        }

        /// <comentarios/>
        public string Shortcut_Dimension_2_Code
        {
            get
            {
                return this.shortcut_Dimension_2_CodeField;
            }
            set
            {
                this.shortcut_Dimension_2_CodeField = value;
            }
        }

        /// <comentarios/>
        public string Location_Code
        {
            get
            {
                return this.location_CodeField;
            }
            set
            {
                this.location_CodeField = value;
            }
        }

        /// <comentarios/>
        public string Purchaser_Code
        {
            get
            {
                return this.purchaser_CodeField;
            }
            set
            {
                this.purchaser_CodeField = value;
            }
        }

        /// <comentarios/>
        public string Assigned_User_ID
        {
            get
            {
                return this.assigned_User_IDField;
            }
            set
            {
                this.assigned_User_IDField = value;
            }
        }

        /// <comentarios/>
        public string Currency_Code
        {
            get
            {
                return this.currency_CodeField;
            }
            set
            {
                this.currency_CodeField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime Document_Date
        {
            get
            {
                return this.document_DateField;
            }
            set
            {
                this.document_DateField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool Document_DateSpecified
        {
            get
            {
                return this.document_DateFieldSpecified;
            }
            set
            {
                this.document_DateFieldSpecified = value;
            }
        }

        /// <comentarios/>
        public string Campaign_No
        {
            get
            {
                return this.campaign_NoField;
            }
            set
            {
                this.campaign_NoField = value;
            }
        }

        /// <comentarios/>
        public Status Status
        {
            get
            {
                return this.statusField;
            }
            set
            {
                this.statusField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool StatusSpecified
        {
            get
            {
                return this.statusFieldSpecified;
            }
            set
            {
                this.statusFieldSpecified = value;
            }
        }

        /// <comentarios/>
        public string Shipment_Method_Code
        {
            get
            {
                return this.shipment_Method_CodeField;
            }
            set
            {
                this.shipment_Method_CodeField = value;
            }
        }

        /// <comentarios/>
        public string Payment_Method_Code
        {
            get
            {
                return this.payment_Method_CodeField;
            }
            set
            {
                this.payment_Method_CodeField = value;
            }
        }

        /// <comentarios/>
        public decimal Porcentaje_rappel
        {
            get
            {
                return this.porcentaje_rappelField;
            }
            set
            {
                this.porcentaje_rappelField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool Porcentaje_rappelSpecified
        {
            get
            {
                return this.porcentaje_rappelFieldSpecified;
            }
            set
            {
                this.porcentaje_rappelFieldSpecified = value;
            }
        }

        /// <comentarios/>
        public string Concepto_rappel
        {
            get
            {
                return this.concepto_rappelField;
            }
            set
            {
                this.concepto_rappelField = value;
            }
        }
    }

    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:microsoft-dynamics-schemas/page/purchasequotes")]
    public enum Status
    {

        /// <comentarios/>
        Open,

        /// <comentarios/>
        Released,

        /// <comentarios/>
        Pending_Approval,

        /// <comentarios/>
        Pending_Prepayment,
    }

    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:microsoft-dynamics-schemas/page/purchasequotes")]
    public partial class PurchaseQuotes_Filter
    {

        private PurchaseQuotes_Fields fieldField;

        private string criteriaField;

        /// <comentarios/>
        public PurchaseQuotes_Fields Field
        {
            get
            {
                return this.fieldField;
            }
            set
            {
                this.fieldField = value;
            }
        }

        /// <comentarios/>
        public string Criteria
        {
            get
            {
                return this.criteriaField;
            }
            set
            {
                this.criteriaField = value;
            }
        }
    }

    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:microsoft-dynamics-schemas/page/purchasequotes")]
    public enum PurchaseQuotes_Fields
    {

        /// <comentarios/>
        No,

        /// <comentarios/>
        Buy_from_Vendor_No,

        /// <comentarios/>
        Order_Address_Code,

        /// <comentarios/>
        Buy_from_Vendor_Name,

        /// <comentarios/>
        Vendor_Authorization_No,

        /// <comentarios/>
        Buy_from_Post_Code,

        /// <comentarios/>
        Buy_from_Country_Region_Code,

        /// <comentarios/>
        Buy_from_Contact,

        /// <comentarios/>
        Pay_to_Vendor_No,

        /// <comentarios/>
        Pay_to_Name,

        /// <comentarios/>
        Pay_to_Post_Code,

        /// <comentarios/>
        Pay_to_Country_Region_Code,

        /// <comentarios/>
        Pay_to_Contact,

        /// <comentarios/>
        Ship_to_Code,

        /// <comentarios/>
        Ship_to_Name,

        /// <comentarios/>
        Ship_to_Post_Code,

        /// <comentarios/>
        Ship_to_Country_Region_Code,

        /// <comentarios/>
        Ship_to_Contact,

        /// <comentarios/>
        Posting_Date,

        /// <comentarios/>
        Shortcut_Dimension_1_Code,

        /// <comentarios/>
        Shortcut_Dimension_2_Code,

        /// <comentarios/>
        Location_Code,

        /// <comentarios/>
        Purchaser_Code,

        /// <comentarios/>
        Assigned_User_ID,

        /// <comentarios/>
        Currency_Code,

        /// <comentarios/>
        Document_Date,

        /// <comentarios/>
        Campaign_No,

        /// <comentarios/>
        Status,

        /// <comentarios/>
        Shipment_Method_Code,

        /// <comentarios/>
        Payment_Method_Code,

        /// <comentarios/>
        Porcentaje_rappel,

        /// <comentarios/>
        Concepto_rappel,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    public delegate void ReadCompletedEventHandler(object sender, ReadCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ReadCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal ReadCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
            base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public PurchaseQuotes Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((PurchaseQuotes)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    public delegate void ReadMultipleCompletedEventHandler(object sender, ReadMultipleCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ReadMultipleCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal ReadMultipleCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
            base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public PurchaseQuotes[] Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((PurchaseQuotes[])(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    public delegate void IsUpdatedCompletedEventHandler(object sender, IsUpdatedCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class IsUpdatedCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal IsUpdatedCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
            base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public bool Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }
    }
}