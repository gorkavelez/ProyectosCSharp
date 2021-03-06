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

namespace INIKER.PurchaseLines
{

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name = "PurchaseLines_Binding", Namespace = "urn:microsoft-dynamics-schemas/page/purchaselines")]
    public partial class PurchaseLines_Service : System.Web.Services.Protocols.SoapHttpClientProtocol
    {

        private System.Threading.SendOrPostCallback ReadOperationCompleted;

        private System.Threading.SendOrPostCallback ReadMultipleOperationCompleted;

        private System.Threading.SendOrPostCallback IsUpdatedOperationCompleted;

        /// <remarks/>
        public PurchaseLines_Service()
        {
            Entidades_ExtranetCompras.Properties.Settings miConfig = new Entidades_ExtranetCompras.Properties.Settings();
            this.Url = miConfig.urlInicioDefaultWS + "/Page/PurchaseLines?WSDL";
            this.UseDefaultCredentials = true;
            //this.Url = "http://serv-nsql2008:7047/GINDUSAL/WS/02%20NAVARRA/Page/PurchaseLines?WSDL";
            //this.Credentials = new NetworkCredential("JCA", "Iniker1");
        }

        public PurchaseLines_Service(string IDusuario, string pwd)
        {
            // Este constructor se utiliza para obtener el nombre de la empresa con la que
            // trabaja el usuario que ha hecho login en el equipo.
            Entidades_ExtranetCompras.Properties.Settings miConfig = new Entidades_ExtranetCompras.Properties.Settings();
            this.Url = miConfig.urlInicioWS + "/Page/PurchaseLines?WSDL";
            if (miConfig.useDefCredts)
                this.UseDefaultCredentials = true;
            else
                this.Credentials = new NetworkCredential(IDusuario, pwd);
        }

        public PurchaseLines_Service(string IDusuario, string pwd, string empresa)
        {
            // Constructor para trabajar contra los web services una vez obtenida la empresa de trabajo
            Entidades_ExtranetCompras.Properties.Settings miConfig = new Entidades_ExtranetCompras.Properties.Settings();
            this.Url = miConfig.urlInicioWS + "/" + empresa + "/Page/PurchaseLines?WSDL";
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
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:microsoft-dynamics-schemas/page/purchaselines:Read", RequestNamespace = "urn:microsoft-dynamics-schemas/page/purchaselines", ResponseElementName = "Read_Result", ResponseNamespace = "urn:microsoft-dynamics-schemas/page/purchaselines", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("PurchaseLines")]
        public PurchaseLines Read(string Document_Type, string Document_No, int Line_No)
        {
            object[] results = this.Invoke("Read", new object[] {
                    Document_Type,
                    Document_No,
                    Line_No});
            return ((PurchaseLines)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginRead(string Document_Type, string Document_No, int Line_No, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("Read", new object[] {
                    Document_Type,
                    Document_No,
                    Line_No}, callback, asyncState);
        }

        /// <remarks/>
        public PurchaseLines EndRead(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((PurchaseLines)(results[0]));
        }

        /// <remarks/>
        public void ReadAsync(string Document_Type, string Document_No, int Line_No)
        {
            this.ReadAsync(Document_Type, Document_No, Line_No, null);
        }

        /// <remarks/>
        public void ReadAsync(string Document_Type, string Document_No, int Line_No, object userState)
        {
            if ((this.ReadOperationCompleted == null))
            {
                this.ReadOperationCompleted = new System.Threading.SendOrPostCallback(this.OnReadOperationCompleted);
            }
            this.InvokeAsync("Read", new object[] {
                    Document_Type,
                    Document_No,
                    Line_No}, this.ReadOperationCompleted, userState);
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
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:microsoft-dynamics-schemas/page/purchaselines:ReadMultiple", RequestNamespace = "urn:microsoft-dynamics-schemas/page/purchaselines", ResponseElementName = "ReadMultiple_Result", ResponseNamespace = "urn:microsoft-dynamics-schemas/page/purchaselines", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlArrayAttribute("ReadMultiple_Result")]
        [return: System.Xml.Serialization.XmlArrayItemAttribute(IsNullable = false)]
        public PurchaseLines[] ReadMultiple([System.Xml.Serialization.XmlElementAttribute("filter")] PurchaseLines_Filter[] filter, string bookmarkKey, int setSize)
        {
            object[] results = this.Invoke("ReadMultiple", new object[] {
                    filter,
                    bookmarkKey,
                    setSize});
            return ((PurchaseLines[])(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginReadMultiple(PurchaseLines_Filter[] filter, string bookmarkKey, int setSize, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("ReadMultiple", new object[] {
                    filter,
                    bookmarkKey,
                    setSize}, callback, asyncState);
        }

        /// <remarks/>
        public PurchaseLines[] EndReadMultiple(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((PurchaseLines[])(results[0]));
        }

        /// <remarks/>
        public void ReadMultipleAsync(PurchaseLines_Filter[] filter, string bookmarkKey, int setSize)
        {
            this.ReadMultipleAsync(filter, bookmarkKey, setSize, null);
        }

        /// <remarks/>
        public void ReadMultipleAsync(PurchaseLines_Filter[] filter, string bookmarkKey, int setSize, object userState)
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
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:microsoft-dynamics-schemas/page/purchaselines:IsUpdated", RequestNamespace = "urn:microsoft-dynamics-schemas/page/purchaselines", ResponseElementName = "IsUpdated_Result", ResponseNamespace = "urn:microsoft-dynamics-schemas/page/purchaselines", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:microsoft-dynamics-schemas/page/purchaselines")]
    public partial class PurchaseLines
    {

        private string keyField;

        private Document_Type document_TypeField;

        private bool document_TypeFieldSpecified;

        private string document_NoField;

        private string buy_from_Vendor_NoField;

        private int line_NoField;

        private bool line_NoFieldSpecified;

        private Type typeField;

        private bool typeFieldSpecified;

        private string noField;

        private string variant_CodeField;

        private string descriptionField;

        private string location_CodeField;

        private decimal quantityField;

        private bool quantityFieldSpecified;

        private decimal reserved_Qty_BaseField;

        private bool reserved_Qty_BaseFieldSpecified;

        private string unit_of_Measure_CodeField;

        private decimal direct_Unit_CostField;

        private bool direct_Unit_CostFieldSpecified;

        private decimal indirect_Cost_PercentField;

        private bool indirect_Cost_PercentFieldSpecified;

        private decimal unit_Cost_LCYField;

        private bool unit_Cost_LCYFieldSpecified;

        private decimal unit_Price_LCYField;

        private bool unit_Price_LCYFieldSpecified;

        private decimal line_AmountField;

        private bool line_AmountFieldSpecified;

        private string job_NoField;

        private string job_Task_NoField;

        private Job_Line_Type job_Line_TypeField;

        private bool job_Line_TypeFieldSpecified;

        private string shortcut_Dimension_1_CodeField;

        private string shortcut_Dimension_2_CodeField;

        private string shortcutDimCode3Field;

        private string shortcutDimCode4Field;

        private string shortcutDimCode5Field;

        private string shortcutDimCode6Field;

        private string shortcutDimCode7Field;

        private string shortcutDimCode8Field;

        private System.DateTime expected_Receipt_DateField;

        private bool expected_Receipt_DateFieldSpecified;

        private decimal outstanding_QuantityField;

        private bool outstanding_QuantityFieldSpecified;

        private decimal outstanding_Amount_LCYField;

        private bool outstanding_Amount_LCYFieldSpecified;

        private decimal amt_Rcd_Not_Invoiced_LCYField;

        private bool amt_Rcd_Not_Invoiced_LCYFieldSpecified;

        private decimal line_Discount_PercentField;

        private bool line_Discount_PercentFieldSpecified;

        private int plazo_EntregaField;

        private bool plazo_EntregaFieldSpecified;

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
        public Document_Type Document_Type
        {
            get
            {
                return this.document_TypeField;
            }
            set
            {
                this.document_TypeField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool Document_TypeSpecified
        {
            get
            {
                return this.document_TypeFieldSpecified;
            }
            set
            {
                this.document_TypeFieldSpecified = value;
            }
        }

        /// <comentarios/>
        public string Document_No
        {
            get
            {
                return this.document_NoField;
            }
            set
            {
                this.document_NoField = value;
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
        public int Line_No
        {
            get
            {
                return this.line_NoField;
            }
            set
            {
                this.line_NoField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool Line_NoSpecified
        {
            get
            {
                return this.line_NoFieldSpecified;
            }
            set
            {
                this.line_NoFieldSpecified = value;
            }
        }

        /// <comentarios/>
        public Type Type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool TypeSpecified
        {
            get
            {
                return this.typeFieldSpecified;
            }
            set
            {
                this.typeFieldSpecified = value;
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
        public string Variant_Code
        {
            get
            {
                return this.variant_CodeField;
            }
            set
            {
                this.variant_CodeField = value;
            }
        }

        /// <comentarios/>
        public string Description
        {
            get
            {
                return this.descriptionField;
            }
            set
            {
                this.descriptionField = value;
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
        public decimal Quantity
        {
            get
            {
                return this.quantityField;
            }
            set
            {
                this.quantityField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool QuantitySpecified
        {
            get
            {
                return this.quantityFieldSpecified;
            }
            set
            {
                this.quantityFieldSpecified = value;
            }
        }

        /// <comentarios/>
        public decimal Reserved_Qty_Base
        {
            get
            {
                return this.reserved_Qty_BaseField;
            }
            set
            {
                this.reserved_Qty_BaseField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool Reserved_Qty_BaseSpecified
        {
            get
            {
                return this.reserved_Qty_BaseFieldSpecified;
            }
            set
            {
                this.reserved_Qty_BaseFieldSpecified = value;
            }
        }

        /// <comentarios/>
        public string Unit_of_Measure_Code
        {
            get
            {
                return this.unit_of_Measure_CodeField;
            }
            set
            {
                this.unit_of_Measure_CodeField = value;
            }
        }

        /// <comentarios/>
        public decimal Direct_Unit_Cost
        {
            get
            {
                return this.direct_Unit_CostField;
            }
            set
            {
                this.direct_Unit_CostField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool Direct_Unit_CostSpecified
        {
            get
            {
                return this.direct_Unit_CostFieldSpecified;
            }
            set
            {
                this.direct_Unit_CostFieldSpecified = value;
            }
        }

        /// <comentarios/>
        public decimal Indirect_Cost_Percent
        {
            get
            {
                return this.indirect_Cost_PercentField;
            }
            set
            {
                this.indirect_Cost_PercentField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool Indirect_Cost_PercentSpecified
        {
            get
            {
                return this.indirect_Cost_PercentFieldSpecified;
            }
            set
            {
                this.indirect_Cost_PercentFieldSpecified = value;
            }
        }

        /// <comentarios/>
        public decimal Unit_Cost_LCY
        {
            get
            {
                return this.unit_Cost_LCYField;
            }
            set
            {
                this.unit_Cost_LCYField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool Unit_Cost_LCYSpecified
        {
            get
            {
                return this.unit_Cost_LCYFieldSpecified;
            }
            set
            {
                this.unit_Cost_LCYFieldSpecified = value;
            }
        }

        /// <comentarios/>
        public decimal Unit_Price_LCY
        {
            get
            {
                return this.unit_Price_LCYField;
            }
            set
            {
                this.unit_Price_LCYField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool Unit_Price_LCYSpecified
        {
            get
            {
                return this.unit_Price_LCYFieldSpecified;
            }
            set
            {
                this.unit_Price_LCYFieldSpecified = value;
            }
        }

        /// <comentarios/>
        public decimal Line_Amount
        {
            get
            {
                return this.line_AmountField;
            }
            set
            {
                this.line_AmountField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool Line_AmountSpecified
        {
            get
            {
                return this.line_AmountFieldSpecified;
            }
            set
            {
                this.line_AmountFieldSpecified = value;
            }
        }

        /// <comentarios/>
        public string Job_No
        {
            get
            {
                return this.job_NoField;
            }
            set
            {
                this.job_NoField = value;
            }
        }

        /// <comentarios/>
        public string Job_Task_No
        {
            get
            {
                return this.job_Task_NoField;
            }
            set
            {
                this.job_Task_NoField = value;
            }
        }

        /// <comentarios/>
        public Job_Line_Type Job_Line_Type
        {
            get
            {
                return this.job_Line_TypeField;
            }
            set
            {
                this.job_Line_TypeField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool Job_Line_TypeSpecified
        {
            get
            {
                return this.job_Line_TypeFieldSpecified;
            }
            set
            {
                this.job_Line_TypeFieldSpecified = value;
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
        [System.Xml.Serialization.XmlElementAttribute("ShortcutDimCode[3]")]
        public string ShortcutDimCode3
        {
            get
            {
                return this.shortcutDimCode3Field;
            }
            set
            {
                this.shortcutDimCode3Field = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute("ShortcutDimCode[4]")]
        public string ShortcutDimCode4
        {
            get
            {
                return this.shortcutDimCode4Field;
            }
            set
            {
                this.shortcutDimCode4Field = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute("ShortcutDimCode[5]")]
        public string ShortcutDimCode5
        {
            get
            {
                return this.shortcutDimCode5Field;
            }
            set
            {
                this.shortcutDimCode5Field = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute("ShortcutDimCode[6]")]
        public string ShortcutDimCode6
        {
            get
            {
                return this.shortcutDimCode6Field;
            }
            set
            {
                this.shortcutDimCode6Field = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute("ShortcutDimCode[7]")]
        public string ShortcutDimCode7
        {
            get
            {
                return this.shortcutDimCode7Field;
            }
            set
            {
                this.shortcutDimCode7Field = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute("ShortcutDimCode[8]")]
        public string ShortcutDimCode8
        {
            get
            {
                return this.shortcutDimCode8Field;
            }
            set
            {
                this.shortcutDimCode8Field = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime Expected_Receipt_Date
        {
            get
            {
                return this.expected_Receipt_DateField;
            }
            set
            {
                this.expected_Receipt_DateField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool Expected_Receipt_DateSpecified
        {
            get
            {
                return this.expected_Receipt_DateFieldSpecified;
            }
            set
            {
                this.expected_Receipt_DateFieldSpecified = value;
            }
        }

        /// <comentarios/>
        public decimal Outstanding_Quantity
        {
            get
            {
                return this.outstanding_QuantityField;
            }
            set
            {
                this.outstanding_QuantityField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool Outstanding_QuantitySpecified
        {
            get
            {
                return this.outstanding_QuantityFieldSpecified;
            }
            set
            {
                this.outstanding_QuantityFieldSpecified = value;
            }
        }

        /// <comentarios/>
        public decimal Outstanding_Amount_LCY
        {
            get
            {
                return this.outstanding_Amount_LCYField;
            }
            set
            {
                this.outstanding_Amount_LCYField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool Outstanding_Amount_LCYSpecified
        {
            get
            {
                return this.outstanding_Amount_LCYFieldSpecified;
            }
            set
            {
                this.outstanding_Amount_LCYFieldSpecified = value;
            }
        }

        /// <comentarios/>
        public decimal Amt_Rcd_Not_Invoiced_LCY
        {
            get
            {
                return this.amt_Rcd_Not_Invoiced_LCYField;
            }
            set
            {
                this.amt_Rcd_Not_Invoiced_LCYField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool Amt_Rcd_Not_Invoiced_LCYSpecified
        {
            get
            {
                return this.amt_Rcd_Not_Invoiced_LCYFieldSpecified;
            }
            set
            {
                this.amt_Rcd_Not_Invoiced_LCYFieldSpecified = value;
            }
        }

        /// <comentarios/>
        public decimal Line_Discount_Percent
        {
            get
            {
                return this.line_Discount_PercentField;
            }
            set
            {
                this.line_Discount_PercentField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool Line_Discount_PercentSpecified
        {
            get
            {
                return this.line_Discount_PercentFieldSpecified;
            }
            set
            {
                this.line_Discount_PercentFieldSpecified = value;
            }
        }

        /// <comentarios/>
        public int Plazo_Entrega
        {
            get
            {
                return this.plazo_EntregaField;
            }
            set
            {
                this.plazo_EntregaField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool Plazo_EntregaSpecified
        {
            get
            {
                return this.plazo_EntregaFieldSpecified;
            }
            set
            {
                this.plazo_EntregaFieldSpecified = value;
            }
        }
    }

    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:microsoft-dynamics-schemas/page/purchaselines")]
    public enum Document_Type
    {

        /// <comentarios/>
        Quote,

        /// <comentarios/>
        Order,

        /// <comentarios/>
        Invoice,

        /// <comentarios/>
        Credit_Memo,

        /// <comentarios/>
        Blanket_Order,

        /// <comentarios/>
        Return_Order,
    }

    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:microsoft-dynamics-schemas/page/purchaselines")]
    public enum Type
    {

        /// <comentarios/>
        _blank_,

        /// <comentarios/>
        G_L_Account,

        /// <comentarios/>
        Item,

        /// <comentarios/>
        Fixed_Asset,

        /// <comentarios/>
        Charge_Item,
    }

    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:microsoft-dynamics-schemas/page/purchaselines")]
    public enum Job_Line_Type
    {

        /// <comentarios/>
        _blank_,

        /// <comentarios/>
        Schedule,

        /// <comentarios/>
        Contract,

        /// <comentarios/>
        Both_Schedule_and_Contract,
    }

    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:microsoft-dynamics-schemas/page/purchaselines")]
    public partial class PurchaseLines_Filter
    {

        private PurchaseLines_Fields fieldField;

        private string criteriaField;

        /// <comentarios/>
        public PurchaseLines_Fields Field
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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:microsoft-dynamics-schemas/page/purchaselines")]
    public enum PurchaseLines_Fields
    {

        /// <comentarios/>
        Document_Type,

        /// <comentarios/>
        Document_No,

        /// <comentarios/>
        Buy_from_Vendor_No,

        /// <comentarios/>
        Line_No,

        /// <comentarios/>
        Type,

        /// <comentarios/>
        No,

        /// <comentarios/>
        Variant_Code,

        /// <comentarios/>
        Description,

        /// <comentarios/>
        Location_Code,

        /// <comentarios/>
        Quantity,

        /// <comentarios/>
        Reserved_Qty_Base,

        /// <comentarios/>
        Unit_of_Measure_Code,

        /// <comentarios/>
        Direct_Unit_Cost,

        /// <comentarios/>
        Indirect_Cost_Percent,

        /// <comentarios/>
        Unit_Cost_LCY,

        /// <comentarios/>
        Unit_Price_LCY,

        /// <comentarios/>
        Line_Amount,

        /// <comentarios/>
        Job_No,

        /// <comentarios/>
        Job_Task_No,

        /// <comentarios/>
        Job_Line_Type,

        /// <comentarios/>
        Shortcut_Dimension_1_Code,

        /// <comentarios/>
        Shortcut_Dimension_2_Code,

        /// <comentarios/>
        ShortcutDimCode_x005B_3_x005D_,

        /// <comentarios/>
        ShortcutDimCode_x005B_4_x005D_,

        /// <comentarios/>
        ShortcutDimCode_x005B_5_x005D_,

        /// <comentarios/>
        ShortcutDimCode_x005B_6_x005D_,

        /// <comentarios/>
        ShortcutDimCode_x005B_7_x005D_,

        /// <comentarios/>
        ShortcutDimCode_x005B_8_x005D_,

        /// <comentarios/>
        Expected_Receipt_Date,

        /// <comentarios/>
        Outstanding_Quantity,

        /// <comentarios/>
        Outstanding_Amount_LCY,

        /// <comentarios/>
        Amt_Rcd_Not_Invoiced_LCY,

        /// <comentarios/>
        Line_Discount_Percent,

        /// <comentarios/>
        Plazo_Entrega,
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
        public PurchaseLines Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((PurchaseLines)(this.results[0]));
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
        public PurchaseLines[] Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((PurchaseLines[])(this.results[0]));
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