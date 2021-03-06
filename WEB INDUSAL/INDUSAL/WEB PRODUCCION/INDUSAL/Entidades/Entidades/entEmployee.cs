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

namespace INIKER.Employee
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name = "EmployeeList_Binding", Namespace = "urn:microsoft-dynamics-schemas/page/employeelist")]
    public partial class EmployeeList_Service : System.Web.Services.Protocols.SoapHttpClientProtocol
    {

        private System.Threading.SendOrPostCallback ReadOperationCompleted;

        private System.Threading.SendOrPostCallback ReadMultipleOperationCompleted;

        private System.Threading.SendOrPostCallback IsUpdatedOperationCompleted;

        /// <remarks/>

        public EmployeeList_Service()
        {
            Entidades.Properties.Settings miConfig = new Entidades.Properties.Settings();
            this.Url = miConfig.urlInicioDefaultWS + "/Page/EmployeeList?WSDL";
            this.UseDefaultCredentials = true;

            //Entidades.Properties.Settings miConfig = new Entidades.Properties.Settings();
            //this.Url = miConfig.urlInicioWS + "/Page/ItemList?WSDL";
            //this.Credentials = new NetworkCredential("JCA", "Iniker1");
        }


        public EmployeeList_Service(string IDusuario, string pwd)
        {
            // Este constructor se utiliza para obtener el nombre de la empresa con la que
            // trabaja el usuario que ha hecho login en el equipo.
            Entidades.Properties.Settings miConfig = new Entidades.Properties.Settings();
            this.Url = miConfig.urlInicioWS + "/Page/EmployeeList?WSDL";
            if (miConfig.useDefCredts)
                this.UseDefaultCredentials = true;
            else
                this.Credentials = new NetworkCredential(IDusuario, pwd);
        }

        public EmployeeList_Service(string IDusuario, string pwd, string empresa)
        {
            // Constructor para trabajar contra los web services una vez obtenida la empresa de trabajo
            Entidades.Properties.Settings miConfig = new Entidades.Properties.Settings();
            this.Url = miConfig.urlInicioWS + "/" + empresa + "/Page/EmployeeList?WSDL";
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
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:microsoft-dynamics-schemas/page/employeelist:Read", RequestNamespace = "urn:microsoft-dynamics-schemas/page/employeelist", ResponseElementName = "Read_Result", ResponseNamespace = "urn:microsoft-dynamics-schemas/page/employeelist", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("EmployeeList")]
        public EmployeeList Read(string No)
        {
            object[] results = this.Invoke("Read", new object[] {
                    No});
            return ((EmployeeList)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginRead(string No, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("Read", new object[] {
                    No}, callback, asyncState);
        }

        /// <remarks/>
        public EmployeeList EndRead(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((EmployeeList)(results[0]));
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
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:microsoft-dynamics-schemas/page/employeelist:ReadMultiple", RequestNamespace = "urn:microsoft-dynamics-schemas/page/employeelist", ResponseElementName = "ReadMultiple_Result", ResponseNamespace = "urn:microsoft-dynamics-schemas/page/employeelist", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlArrayAttribute("ReadMultiple_Result")]
        [return: System.Xml.Serialization.XmlArrayItemAttribute(IsNullable = false)]
        public EmployeeList[] ReadMultiple([System.Xml.Serialization.XmlElementAttribute("filter")] EmployeeList_Filter[] filter, string bookmarkKey, int setSize)
        {
            object[] results = this.Invoke("ReadMultiple", new object[] {
                    filter,
                    bookmarkKey,
                    setSize});
            return ((EmployeeList[])(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginReadMultiple(EmployeeList_Filter[] filter, string bookmarkKey, int setSize, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("ReadMultiple", new object[] {
                    filter,
                    bookmarkKey,
                    setSize}, callback, asyncState);
        }

        /// <remarks/>
        public EmployeeList[] EndReadMultiple(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((EmployeeList[])(results[0]));
        }

        /// <remarks/>
        public void ReadMultipleAsync(EmployeeList_Filter[] filter, string bookmarkKey, int setSize)
        {
            this.ReadMultipleAsync(filter, bookmarkKey, setSize, null);
        }

        /// <remarks/>
        public void ReadMultipleAsync(EmployeeList_Filter[] filter, string bookmarkKey, int setSize, object userState)
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
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:microsoft-dynamics-schemas/page/employeelist:IsUpdated", RequestNamespace = "urn:microsoft-dynamics-schemas/page/employeelist", ResponseElementName = "IsUpdated_Result", ResponseNamespace = "urn:microsoft-dynamics-schemas/page/employeelist", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:microsoft-dynamics-schemas/page/employeelist")]
    public partial class EmployeeList
    {

        private string keyField;

        private string noField;

        private string fullNameField;

        private string nameField;

        private string second_Family_NameField;

        private string first_Family_NameField;

        private string initialsField;

        private string job_TitleField;

        private string post_CodeField;

        private string country_Region_CodeField;

        private string extensionField;

        private string phone_NoField;

        private string mobile_Phone_NoField;

        private string e_MailField;

        private string statistics_Group_CodeField;

        private string resource_NoField;

        private string search_NameField;

        private bool commentField;

        private bool commentFieldSpecified;

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
        public string FullName
        {
            get
            {
                return this.fullNameField;
            }
            set
            {
                this.fullNameField = value;
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
        public string Second_Family_Name
        {
            get
            {
                return this.second_Family_NameField;
            }
            set
            {
                this.second_Family_NameField = value;
            }
        }

        /// <comentarios/>
        public string First_Family_Name
        {
            get
            {
                return this.first_Family_NameField;
            }
            set
            {
                this.first_Family_NameField = value;
            }
        }

        /// <comentarios/>
        public string Initials
        {
            get
            {
                return this.initialsField;
            }
            set
            {
                this.initialsField = value;
            }
        }

        /// <comentarios/>
        public string Job_Title
        {
            get
            {
                return this.job_TitleField;
            }
            set
            {
                this.job_TitleField = value;
            }
        }

        /// <comentarios/>
        public string Post_Code
        {
            get
            {
                return this.post_CodeField;
            }
            set
            {
                this.post_CodeField = value;
            }
        }

        /// <comentarios/>
        public string Country_Region_Code
        {
            get
            {
                return this.country_Region_CodeField;
            }
            set
            {
                this.country_Region_CodeField = value;
            }
        }

        /// <comentarios/>
        public string Extension
        {
            get
            {
                return this.extensionField;
            }
            set
            {
                this.extensionField = value;
            }
        }

        /// <comentarios/>
        public string Phone_No
        {
            get
            {
                return this.phone_NoField;
            }
            set
            {
                this.phone_NoField = value;
            }
        }

        /// <comentarios/>
        public string Mobile_Phone_No
        {
            get
            {
                return this.mobile_Phone_NoField;
            }
            set
            {
                this.mobile_Phone_NoField = value;
            }
        }

        /// <comentarios/>
        public string E_Mail
        {
            get
            {
                return this.e_MailField;
            }
            set
            {
                this.e_MailField = value;
            }
        }

        /// <comentarios/>
        public string Statistics_Group_Code
        {
            get
            {
                return this.statistics_Group_CodeField;
            }
            set
            {
                this.statistics_Group_CodeField = value;
            }
        }

        /// <comentarios/>
        public string Resource_No
        {
            get
            {
                return this.resource_NoField;
            }
            set
            {
                this.resource_NoField = value;
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
        public bool Comment
        {
            get
            {
                return this.commentField;
            }
            set
            {
                this.commentField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool CommentSpecified
        {
            get
            {
                return this.commentFieldSpecified;
            }
            set
            {
                this.commentFieldSpecified = value;
            }
        }
    }

    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:microsoft-dynamics-schemas/page/employeelist")]
    public partial class EmployeeList_Filter
    {

        private EmployeeList_Fields fieldField;

        private string criteriaField;

        /// <comentarios/>
        public EmployeeList_Fields Field
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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:microsoft-dynamics-schemas/page/employeelist")]
    public enum EmployeeList_Fields
    {

        /// <comentarios/>
        No,

        /// <comentarios/>
        FullName,

        /// <comentarios/>
        Name,

        /// <comentarios/>
        Second_Family_Name,

        /// <comentarios/>
        First_Family_Name,

        /// <comentarios/>
        Initials,

        /// <comentarios/>
        Job_Title,

        /// <comentarios/>
        Post_Code,

        /// <comentarios/>
        Country_Region_Code,

        /// <comentarios/>
        Extension,

        /// <comentarios/>
        Phone_No,

        /// <comentarios/>
        Mobile_Phone_No,

        /// <comentarios/>
        E_Mail,

        /// <comentarios/>
        Statistics_Group_Code,

        /// <comentarios/>
        Resource_No,

        /// <comentarios/>
        Search_Name,

        /// <comentarios/>
        Comment,
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
        public EmployeeList Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((EmployeeList)(this.results[0]));
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
        public EmployeeList[] Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((EmployeeList[])(this.results[0]));
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