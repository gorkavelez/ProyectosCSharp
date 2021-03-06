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

namespace INIKER.WorkShift
{

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name = "WorkShiftList_Binding", Namespace = "urn:microsoft-dynamics-schemas/page/workshiftlist")]
    public partial class WorkShiftList_Service : System.Web.Services.Protocols.SoapHttpClientProtocol
    {

        private System.Threading.SendOrPostCallback ReadOperationCompleted;

        private System.Threading.SendOrPostCallback ReadMultipleOperationCompleted;

        private System.Threading.SendOrPostCallback IsUpdatedOperationCompleted;

        private System.Threading.SendOrPostCallback CreateOperationCompleted;

        private System.Threading.SendOrPostCallback CreateMultipleOperationCompleted;

        private System.Threading.SendOrPostCallback UpdateOperationCompleted;

        private System.Threading.SendOrPostCallback UpdateMultipleOperationCompleted;

        private System.Threading.SendOrPostCallback DeleteOperationCompleted;

        /// <remarks/>        
        public WorkShiftList_Service()
        {
            Entidades.Properties.Settings miConfig = new Entidades.Properties.Settings();
            this.Url = miConfig.urlInicioDefaultWS + "/Page/WorkShiftList?WSDL";
            this.UseDefaultCredentials = true;

            //this.Url = "http://serv-nsql2008:7047/GINDUSAL/WS/02 NAVARRA/Page/WorkShiftList?WSDL";
            //this.Credentials = new NetworkCredential("JCA", "Iniker1");
        }

        public WorkShiftList_Service(string IDusuario, string pwd)
        {
            // Este constructor se utiliza para obtener el nombre de la empresa con la que
            // trabaja el usuario que ha hecho login en el equipo.
            Entidades.Properties.Settings miConfig = new Entidades.Properties.Settings();
            this.Url = miConfig.urlInicioWS + "/Page/WorkShiftList?WSDL";
            if (miConfig.useDefCredts)
                this.UseDefaultCredentials = true;
            else
                this.Credentials = new NetworkCredential(IDusuario, pwd);
        }

        public WorkShiftList_Service(string IDusuario, string pwd, string empresa)
        {
            // Constructor para trabajar contra los web services una vez obtenida la empresa de trabajo
            Entidades.Properties.Settings miConfig = new Entidades.Properties.Settings();
            this.Url = miConfig.urlInicioWS + "/" + empresa + "/Page/WorkShiftList?WSDL";
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
        public event CreateCompletedEventHandler CreateCompleted;

        /// <remarks/>
        public event CreateMultipleCompletedEventHandler CreateMultipleCompleted;

        /// <remarks/>
        public event UpdateCompletedEventHandler UpdateCompleted;

        /// <remarks/>
        public event UpdateMultipleCompletedEventHandler UpdateMultipleCompleted;

        /// <remarks/>
        public event DeleteCompletedEventHandler DeleteCompleted;

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:microsoft-dynamics-schemas/page/workshiftlist:Read", RequestNamespace = "urn:microsoft-dynamics-schemas/page/workshiftlist", ResponseElementName = "Read_Result", ResponseNamespace = "urn:microsoft-dynamics-schemas/page/workshiftlist", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("WorkShiftList")]
        public WorkShiftList Read(string Code)
        {
            object[] results = this.Invoke("Read", new object[] {
                    Code});
            return ((WorkShiftList)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginRead(string Code, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("Read", new object[] {
                    Code}, callback, asyncState);
        }

        /// <remarks/>
        public WorkShiftList EndRead(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((WorkShiftList)(results[0]));
        }

        /// <remarks/>
        public void ReadAsync(string Code)
        {
            this.ReadAsync(Code, null);
        }

        /// <remarks/>
        public void ReadAsync(string Code, object userState)
        {
            if ((this.ReadOperationCompleted == null))
            {
                this.ReadOperationCompleted = new System.Threading.SendOrPostCallback(this.OnReadOperationCompleted);
            }
            this.InvokeAsync("Read", new object[] {
                    Code}, this.ReadOperationCompleted, userState);
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
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:microsoft-dynamics-schemas/page/workshiftlist:ReadMultiple", RequestNamespace = "urn:microsoft-dynamics-schemas/page/workshiftlist", ResponseElementName = "ReadMultiple_Result", ResponseNamespace = "urn:microsoft-dynamics-schemas/page/workshiftlist", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlArrayAttribute("ReadMultiple_Result")]
        [return: System.Xml.Serialization.XmlArrayItemAttribute(IsNullable = false)]
        public WorkShiftList[] ReadMultiple([System.Xml.Serialization.XmlElementAttribute("filter")] WorkShiftList_Filter[] filter, string bookmarkKey, int setSize)
        {
            object[] results = this.Invoke("ReadMultiple", new object[] {
                    filter,
                    bookmarkKey,
                    setSize});
            return ((WorkShiftList[])(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginReadMultiple(WorkShiftList_Filter[] filter, string bookmarkKey, int setSize, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("ReadMultiple", new object[] {
                    filter,
                    bookmarkKey,
                    setSize}, callback, asyncState);
        }

        /// <remarks/>
        public WorkShiftList[] EndReadMultiple(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((WorkShiftList[])(results[0]));
        }

        /// <remarks/>
        public void ReadMultipleAsync(WorkShiftList_Filter[] filter, string bookmarkKey, int setSize)
        {
            this.ReadMultipleAsync(filter, bookmarkKey, setSize, null);
        }

        /// <remarks/>
        public void ReadMultipleAsync(WorkShiftList_Filter[] filter, string bookmarkKey, int setSize, object userState)
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
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:microsoft-dynamics-schemas/page/workshiftlist:IsUpdated", RequestNamespace = "urn:microsoft-dynamics-schemas/page/workshiftlist", ResponseElementName = "IsUpdated_Result", ResponseNamespace = "urn:microsoft-dynamics-schemas/page/workshiftlist", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
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
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:microsoft-dynamics-schemas/page/workshiftlist:Create", RequestNamespace = "urn:microsoft-dynamics-schemas/page/workshiftlist", ResponseElementName = "Create_Result", ResponseNamespace = "urn:microsoft-dynamics-schemas/page/workshiftlist", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void Create(ref WorkShiftList WorkShiftList)
        {
            object[] results = this.Invoke("Create", new object[] {
                    WorkShiftList});
            WorkShiftList = ((WorkShiftList)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginCreate(WorkShiftList WorkShiftList, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("Create", new object[] {
                    WorkShiftList}, callback, asyncState);
        }

        /// <remarks/>
        public void EndCreate(System.IAsyncResult asyncResult, out WorkShiftList WorkShiftList)
        {
            object[] results = this.EndInvoke(asyncResult);
            WorkShiftList = ((WorkShiftList)(results[0]));
        }

        /// <remarks/>
        public void CreateAsync(WorkShiftList WorkShiftList)
        {
            this.CreateAsync(WorkShiftList, null);
        }

        /// <remarks/>
        public void CreateAsync(WorkShiftList WorkShiftList, object userState)
        {
            if ((this.CreateOperationCompleted == null))
            {
                this.CreateOperationCompleted = new System.Threading.SendOrPostCallback(this.OnCreateOperationCompleted);
            }
            this.InvokeAsync("Create", new object[] {
                    WorkShiftList}, this.CreateOperationCompleted, userState);
        }

        private void OnCreateOperationCompleted(object arg)
        {
            if ((this.CreateCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.CreateCompleted(this, new CreateCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:microsoft-dynamics-schemas/page/workshiftlist:CreateMultiple", RequestNamespace = "urn:microsoft-dynamics-schemas/page/workshiftlist", ResponseElementName = "CreateMultiple_Result", ResponseNamespace = "urn:microsoft-dynamics-schemas/page/workshiftlist", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void CreateMultiple([System.Xml.Serialization.XmlArrayItemAttribute(IsNullable = false)] ref WorkShiftList[] WorkShiftList_List)
        {
            object[] results = this.Invoke("CreateMultiple", new object[] {
                    WorkShiftList_List});
            WorkShiftList_List = ((WorkShiftList[])(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginCreateMultiple(WorkShiftList[] WorkShiftList_List, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("CreateMultiple", new object[] {
                    WorkShiftList_List}, callback, asyncState);
        }

        /// <remarks/>
        public void EndCreateMultiple(System.IAsyncResult asyncResult, out WorkShiftList[] WorkShiftList_List)
        {
            object[] results = this.EndInvoke(asyncResult);
            WorkShiftList_List = ((WorkShiftList[])(results[0]));
        }

        /// <remarks/>
        public void CreateMultipleAsync(WorkShiftList[] WorkShiftList_List)
        {
            this.CreateMultipleAsync(WorkShiftList_List, null);
        }

        /// <remarks/>
        public void CreateMultipleAsync(WorkShiftList[] WorkShiftList_List, object userState)
        {
            if ((this.CreateMultipleOperationCompleted == null))
            {
                this.CreateMultipleOperationCompleted = new System.Threading.SendOrPostCallback(this.OnCreateMultipleOperationCompleted);
            }
            this.InvokeAsync("CreateMultiple", new object[] {
                    WorkShiftList_List}, this.CreateMultipleOperationCompleted, userState);
        }

        private void OnCreateMultipleOperationCompleted(object arg)
        {
            if ((this.CreateMultipleCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.CreateMultipleCompleted(this, new CreateMultipleCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:microsoft-dynamics-schemas/page/workshiftlist:Update", RequestNamespace = "urn:microsoft-dynamics-schemas/page/workshiftlist", ResponseElementName = "Update_Result", ResponseNamespace = "urn:microsoft-dynamics-schemas/page/workshiftlist", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void Update(ref WorkShiftList WorkShiftList)
        {
            object[] results = this.Invoke("Update", new object[] {
                    WorkShiftList});
            WorkShiftList = ((WorkShiftList)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginUpdate(WorkShiftList WorkShiftList, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("Update", new object[] {
                    WorkShiftList}, callback, asyncState);
        }

        /// <remarks/>
        public void EndUpdate(System.IAsyncResult asyncResult, out WorkShiftList WorkShiftList)
        {
            object[] results = this.EndInvoke(asyncResult);
            WorkShiftList = ((WorkShiftList)(results[0]));
        }

        /// <remarks/>
        public void UpdateAsync(WorkShiftList WorkShiftList)
        {
            this.UpdateAsync(WorkShiftList, null);
        }

        /// <remarks/>
        public void UpdateAsync(WorkShiftList WorkShiftList, object userState)
        {
            if ((this.UpdateOperationCompleted == null))
            {
                this.UpdateOperationCompleted = new System.Threading.SendOrPostCallback(this.OnUpdateOperationCompleted);
            }
            this.InvokeAsync("Update", new object[] {
                    WorkShiftList}, this.UpdateOperationCompleted, userState);
        }

        private void OnUpdateOperationCompleted(object arg)
        {
            if ((this.UpdateCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.UpdateCompleted(this, new UpdateCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:microsoft-dynamics-schemas/page/workshiftlist:UpdateMultiple", RequestNamespace = "urn:microsoft-dynamics-schemas/page/workshiftlist", ResponseElementName = "UpdateMultiple_Result", ResponseNamespace = "urn:microsoft-dynamics-schemas/page/workshiftlist", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void UpdateMultiple([System.Xml.Serialization.XmlArrayItemAttribute(IsNullable = false)] ref WorkShiftList[] WorkShiftList_List)
        {
            object[] results = this.Invoke("UpdateMultiple", new object[] {
                    WorkShiftList_List});
            WorkShiftList_List = ((WorkShiftList[])(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginUpdateMultiple(WorkShiftList[] WorkShiftList_List, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("UpdateMultiple", new object[] {
                    WorkShiftList_List}, callback, asyncState);
        }

        /// <remarks/>
        public void EndUpdateMultiple(System.IAsyncResult asyncResult, out WorkShiftList[] WorkShiftList_List)
        {
            object[] results = this.EndInvoke(asyncResult);
            WorkShiftList_List = ((WorkShiftList[])(results[0]));
        }

        /// <remarks/>
        public void UpdateMultipleAsync(WorkShiftList[] WorkShiftList_List)
        {
            this.UpdateMultipleAsync(WorkShiftList_List, null);
        }

        /// <remarks/>
        public void UpdateMultipleAsync(WorkShiftList[] WorkShiftList_List, object userState)
        {
            if ((this.UpdateMultipleOperationCompleted == null))
            {
                this.UpdateMultipleOperationCompleted = new System.Threading.SendOrPostCallback(this.OnUpdateMultipleOperationCompleted);
            }
            this.InvokeAsync("UpdateMultiple", new object[] {
                    WorkShiftList_List}, this.UpdateMultipleOperationCompleted, userState);
        }

        private void OnUpdateMultipleOperationCompleted(object arg)
        {
            if ((this.UpdateMultipleCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.UpdateMultipleCompleted(this, new UpdateMultipleCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:microsoft-dynamics-schemas/page/workshiftlist:Delete", RequestNamespace = "urn:microsoft-dynamics-schemas/page/workshiftlist", ResponseElementName = "Delete_Result", ResponseNamespace = "urn:microsoft-dynamics-schemas/page/workshiftlist", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("Delete_Result")]
        public bool Delete(string Key)
        {
            object[] results = this.Invoke("Delete", new object[] {
                    Key});
            return ((bool)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginDelete(string Key, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("Delete", new object[] {
                    Key}, callback, asyncState);
        }

        /// <remarks/>
        public bool EndDelete(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((bool)(results[0]));
        }

        /// <remarks/>
        public void DeleteAsync(string Key)
        {
            this.DeleteAsync(Key, null);
        }

        /// <remarks/>
        public void DeleteAsync(string Key, object userState)
        {
            if ((this.DeleteOperationCompleted == null))
            {
                this.DeleteOperationCompleted = new System.Threading.SendOrPostCallback(this.OnDeleteOperationCompleted);
            }
            this.InvokeAsync("Delete", new object[] {
                    Key}, this.DeleteOperationCompleted, userState);
        }

        private void OnDeleteOperationCompleted(object arg)
        {
            if ((this.DeleteCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.DeleteCompleted(this, new DeleteCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:microsoft-dynamics-schemas/page/workshiftlist")]
    public partial class WorkShiftList
    {

        private string keyField;

        private string codeField;

        private string descriptionField;

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
        public string Code
        {
            get
            {
                return this.codeField;
            }
            set
            {
                this.codeField = value;
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
    }

    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:microsoft-dynamics-schemas/page/workshiftlist")]
    public partial class WorkShiftList_Filter
    {

        private WorkShiftList_Fields fieldField;

        private string criteriaField;

        /// <comentarios/>
        public WorkShiftList_Fields Field
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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:microsoft-dynamics-schemas/page/workshiftlist")]
    public enum WorkShiftList_Fields
    {

        /// <comentarios/>
        Code,

        /// <comentarios/>
        Description,
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
        public WorkShiftList Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((WorkShiftList)(this.results[0]));
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
        public WorkShiftList[] Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((WorkShiftList[])(this.results[0]));
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

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    public delegate void CreateCompletedEventHandler(object sender, CreateCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class CreateCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal CreateCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
            base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public WorkShiftList WorkShiftList
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((WorkShiftList)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    public delegate void CreateMultipleCompletedEventHandler(object sender, CreateMultipleCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class CreateMultipleCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal CreateMultipleCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
            base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public WorkShiftList[] WorkShiftList_List
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((WorkShiftList[])(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    public delegate void UpdateCompletedEventHandler(object sender, UpdateCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class UpdateCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal UpdateCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
            base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public WorkShiftList WorkShiftList
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((WorkShiftList)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    public delegate void UpdateMultipleCompletedEventHandler(object sender, UpdateMultipleCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class UpdateMultipleCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal UpdateMultipleCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
            base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public WorkShiftList[] WorkShiftList_List
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((WorkShiftList[])(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    public delegate void DeleteCompletedEventHandler(object sender, DeleteCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class DeleteCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal DeleteCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
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