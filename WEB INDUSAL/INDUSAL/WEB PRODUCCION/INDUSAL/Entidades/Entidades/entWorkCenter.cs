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

namespace INIKER.WorkCenter
{

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name = "WorkCenterList_Binding", Namespace = "urn:microsoft-dynamics-schemas/page/workcenterlist")]
    public partial class WorkCenterList_Service : System.Web.Services.Protocols.SoapHttpClientProtocol
    {

        private System.Threading.SendOrPostCallback ReadOperationCompleted;

        private System.Threading.SendOrPostCallback ReadMultipleOperationCompleted;

        private System.Threading.SendOrPostCallback IsUpdatedOperationCompleted;

        /// <remarks/>
        public WorkCenterList_Service()
        {
            Entidades.Properties.Settings miConfig = new Entidades.Properties.Settings();
            this.Url = miConfig.urlInicioDefaultWS + "/Page/WorkCenterList?WSDL";
            this.UseDefaultCredentials = true;

            //this.Url = "http://serv-nsql2008:7047/GINDUSAL/WS/02 NAVARRA/Page/WorkCenterList?WSDL";
            //this.Credentials = new NetworkCredential("JCA", "Iniker1");
        }

        public WorkCenterList_Service(string IDusuario, string pwd)
        {
            // Este constructor se utiliza para obtener el nombre de la empresa con la que
            // trabaja el usuario que ha hecho login en el equipo.
            Entidades.Properties.Settings miConfig = new Entidades.Properties.Settings();
            this.Url = miConfig.urlInicioWS + "/Page/WorkCenterList?WSDL";
            if (miConfig.useDefCredts)
                this.UseDefaultCredentials = true;
            else
                this.Credentials = new NetworkCredential(IDusuario, pwd);
        }

        public WorkCenterList_Service(string IDusuario, string pwd, string empresa)
        {
            // Constructor para trabajar contra los web services una vez obtenida la empresa de trabajo
            Entidades.Properties.Settings miConfig = new Entidades.Properties.Settings();
            this.Url = miConfig.urlInicioWS + "/" + empresa + "/Page/WorkCenterList?WSDL";
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
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:microsoft-dynamics-schemas/page/workcenterlist:Read", RequestNamespace = "urn:microsoft-dynamics-schemas/page/workcenterlist", ResponseElementName = "Read_Result", ResponseNamespace = "urn:microsoft-dynamics-schemas/page/workcenterlist", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("WorkCenterList")]
        public WorkCenterList Read(string No)
        {
            object[] results = this.Invoke("Read", new object[] {
                    No});
            return ((WorkCenterList)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginRead(string No, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("Read", new object[] {
                    No}, callback, asyncState);
        }

        /// <remarks/>
        public WorkCenterList EndRead(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((WorkCenterList)(results[0]));
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
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:microsoft-dynamics-schemas/page/workcenterlist:ReadMultiple", RequestNamespace = "urn:microsoft-dynamics-schemas/page/workcenterlist", ResponseElementName = "ReadMultiple_Result", ResponseNamespace = "urn:microsoft-dynamics-schemas/page/workcenterlist", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlArrayAttribute("ReadMultiple_Result")]
        [return: System.Xml.Serialization.XmlArrayItemAttribute(IsNullable = false)]
        public WorkCenterList[] ReadMultiple([System.Xml.Serialization.XmlElementAttribute("filter")] WorkCenterList_Filter[] filter, string bookmarkKey, int setSize)
        {
            object[] results = this.Invoke("ReadMultiple", new object[] {
                    filter,
                    bookmarkKey,
                    setSize});
            return ((WorkCenterList[])(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginReadMultiple(WorkCenterList_Filter[] filter, string bookmarkKey, int setSize, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("ReadMultiple", new object[] {
                    filter,
                    bookmarkKey,
                    setSize}, callback, asyncState);
        }

        /// <remarks/>
        public WorkCenterList[] EndReadMultiple(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((WorkCenterList[])(results[0]));
        }

        /// <remarks/>
        public void ReadMultipleAsync(WorkCenterList_Filter[] filter, string bookmarkKey, int setSize)
        {
            this.ReadMultipleAsync(filter, bookmarkKey, setSize, null);
        }

        /// <remarks/>
        public void ReadMultipleAsync(WorkCenterList_Filter[] filter, string bookmarkKey, int setSize, object userState)
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
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:microsoft-dynamics-schemas/page/workcenterlist:IsUpdated", RequestNamespace = "urn:microsoft-dynamics-schemas/page/workcenterlist", ResponseElementName = "IsUpdated_Result", ResponseNamespace = "urn:microsoft-dynamics-schemas/page/workcenterlist", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:microsoft-dynamics-schemas/page/workcenterlist")]
    public partial class WorkCenterList
    {

        private string keyField;

        private string noField;

        private string nameField;

        private string alternate_Work_CenterField;

        private string work_Center_Group_CodeField;

        private string global_Dimension_1_CodeField;

        private string global_Dimension_2_CodeField;

        private decimal direct_Unit_CostField;

        private bool direct_Unit_CostFieldSpecified;

        private decimal indirect_Cost_PercentField;

        private bool indirect_Cost_PercentFieldSpecified;

        private decimal unit_CostField;

        private bool unit_CostFieldSpecified;

        private string unit_of_Measure_CodeField;

        private decimal capacityField;

        private bool capacityFieldSpecified;

        private decimal efficiencyField;

        private bool efficiencyFieldSpecified;

        private decimal maximum_EfficiencyField;

        private bool maximum_EfficiencyFieldSpecified;

        private decimal minimum_EfficiencyField;

        private bool minimum_EfficiencyFieldSpecified;

        private Simulation_Type simulation_TypeField;

        private bool simulation_TypeFieldSpecified;

        private string shop_Calendar_CodeField;

        private string search_NameField;

        private decimal overhead_RateField;

        private bool overhead_RateFieldSpecified;

        private System.DateTime last_Date_ModifiedField;

        private bool last_Date_ModifiedFieldSpecified;

        private Flushing_Method flushing_MethodField;

        private bool flushing_MethodFieldSpecified;

        private string subcontractor_NoField;

        private decimal peso_maximoField;

        private bool peso_maximoFieldSpecified;

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
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <comentarios/>
        public string Alternate_Work_Center
        {
            get
            {
                return this.alternate_Work_CenterField;
            }
            set
            {
                this.alternate_Work_CenterField = value;
            }
        }

        /// <comentarios/>
        public string Work_Center_Group_Code
        {
            get
            {
                return this.work_Center_Group_CodeField;
            }
            set
            {
                this.work_Center_Group_CodeField = value;
            }
        }

        /// <comentarios/>
        public string Global_Dimension_1_Code
        {
            get
            {
                return this.global_Dimension_1_CodeField;
            }
            set
            {
                this.global_Dimension_1_CodeField = value;
            }
        }

        /// <comentarios/>
        public string Global_Dimension_2_Code
        {
            get
            {
                return this.global_Dimension_2_CodeField;
            }
            set
            {
                this.global_Dimension_2_CodeField = value;
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
        public decimal Unit_Cost
        {
            get
            {
                return this.unit_CostField;
            }
            set
            {
                this.unit_CostField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool Unit_CostSpecified
        {
            get
            {
                return this.unit_CostFieldSpecified;
            }
            set
            {
                this.unit_CostFieldSpecified = value;
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
        public decimal Capacity
        {
            get
            {
                return this.capacityField;
            }
            set
            {
                this.capacityField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool CapacitySpecified
        {
            get
            {
                return this.capacityFieldSpecified;
            }
            set
            {
                this.capacityFieldSpecified = value;
            }
        }

        /// <comentarios/>
        public decimal Efficiency
        {
            get
            {
                return this.efficiencyField;
            }
            set
            {
                this.efficiencyField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool EfficiencySpecified
        {
            get
            {
                return this.efficiencyFieldSpecified;
            }
            set
            {
                this.efficiencyFieldSpecified = value;
            }
        }

        /// <comentarios/>
        public decimal Maximum_Efficiency
        {
            get
            {
                return this.maximum_EfficiencyField;
            }
            set
            {
                this.maximum_EfficiencyField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool Maximum_EfficiencySpecified
        {
            get
            {
                return this.maximum_EfficiencyFieldSpecified;
            }
            set
            {
                this.maximum_EfficiencyFieldSpecified = value;
            }
        }

        /// <comentarios/>
        public decimal Minimum_Efficiency
        {
            get
            {
                return this.minimum_EfficiencyField;
            }
            set
            {
                this.minimum_EfficiencyField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool Minimum_EfficiencySpecified
        {
            get
            {
                return this.minimum_EfficiencyFieldSpecified;
            }
            set
            {
                this.minimum_EfficiencyFieldSpecified = value;
            }
        }

        /// <comentarios/>
        public Simulation_Type Simulation_Type
        {
            get
            {
                return this.simulation_TypeField;
            }
            set
            {
                this.simulation_TypeField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool Simulation_TypeSpecified
        {
            get
            {
                return this.simulation_TypeFieldSpecified;
            }
            set
            {
                this.simulation_TypeFieldSpecified = value;
            }
        }

        /// <comentarios/>
        public string Shop_Calendar_Code
        {
            get
            {
                return this.shop_Calendar_CodeField;
            }
            set
            {
                this.shop_Calendar_CodeField = value;
            }
        }

        /// <comentarios/>
        public string Search_Name
        {
            get
            {
                return this.search_NameField;
            }
            set
            {
                this.search_NameField = value;
            }
        }

        /// <comentarios/>
        public decimal Overhead_Rate
        {
            get
            {
                return this.overhead_RateField;
            }
            set
            {
                this.overhead_RateField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool Overhead_RateSpecified
        {
            get
            {
                return this.overhead_RateFieldSpecified;
            }
            set
            {
                this.overhead_RateFieldSpecified = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime Last_Date_Modified
        {
            get
            {
                return this.last_Date_ModifiedField;
            }
            set
            {
                this.last_Date_ModifiedField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool Last_Date_ModifiedSpecified
        {
            get
            {
                return this.last_Date_ModifiedFieldSpecified;
            }
            set
            {
                this.last_Date_ModifiedFieldSpecified = value;
            }
        }

        /// <comentarios/>
        public Flushing_Method Flushing_Method
        {
            get
            {
                return this.flushing_MethodField;
            }
            set
            {
                this.flushing_MethodField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool Flushing_MethodSpecified
        {
            get
            {
                return this.flushing_MethodFieldSpecified;
            }
            set
            {
                this.flushing_MethodFieldSpecified = value;
            }
        }

        /// <comentarios/>
        public string Subcontractor_No
        {
            get
            {
                return this.subcontractor_NoField;
            }
            set
            {
                this.subcontractor_NoField = value;
            }
        }

        /// <comentarios/>
        public decimal Peso_maximo
        {
            get
            {
                return this.peso_maximoField;
            }
            set
            {
                this.peso_maximoField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool Peso_maximoSpecified
        {
            get
            {
                return this.peso_maximoFieldSpecified;
            }
            set
            {
                this.peso_maximoFieldSpecified = value;
            }
        }
    }

    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:microsoft-dynamics-schemas/page/workcenterlist")]
    public enum Simulation_Type
    {

        /// <comentarios/>
        Moves,

        /// <comentarios/>
        Moves_When_Necessary,

        /// <comentarios/>
        Critical,
    }

    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:microsoft-dynamics-schemas/page/workcenterlist")]
    public enum Flushing_Method
    {

        /// <comentarios/>
        Manual,

        /// <comentarios/>
        Forward,

        /// <comentarios/>
        Backward,
    }

    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:microsoft-dynamics-schemas/page/workcenterlist")]
    public partial class WorkCenterList_Filter
    {

        private WorkCenterList_Fields fieldField;

        private string criteriaField;

        /// <comentarios/>
        public WorkCenterList_Fields Field
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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:microsoft-dynamics-schemas/page/workcenterlist")]
    public enum WorkCenterList_Fields
    {

        /// <comentarios/>
        No,

        /// <comentarios/>
        Name,

        /// <comentarios/>
        Alternate_Work_Center,

        /// <comentarios/>
        Work_Center_Group_Code,

        /// <comentarios/>
        Global_Dimension_1_Code,

        /// <comentarios/>
        Global_Dimension_2_Code,

        /// <comentarios/>
        Direct_Unit_Cost,

        /// <comentarios/>
        Indirect_Cost_Percent,

        /// <comentarios/>
        Unit_Cost,

        /// <comentarios/>
        Unit_of_Measure_Code,

        /// <comentarios/>
        Capacity,

        /// <comentarios/>
        Efficiency,

        /// <comentarios/>
        Maximum_Efficiency,

        /// <comentarios/>
        Minimum_Efficiency,

        /// <comentarios/>
        Simulation_Type,

        /// <comentarios/>
        Shop_Calendar_Code,

        /// <comentarios/>
        Search_Name,

        /// <comentarios/>
        Overhead_Rate,

        /// <comentarios/>
        Last_Date_Modified,

        /// <comentarios/>
        Flushing_Method,

        /// <comentarios/>
        Subcontractor_No,

        /// <comentarios/>
        Peso_maximo,
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
        public WorkCenterList Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((WorkCenterList)(this.results[0]));
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
        public WorkCenterList[] Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((WorkCenterList[])(this.results[0]));
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