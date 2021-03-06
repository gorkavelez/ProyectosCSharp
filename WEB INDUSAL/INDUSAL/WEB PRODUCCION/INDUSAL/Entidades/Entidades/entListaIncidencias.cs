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

namespace INIKER.IncidenciasTransporte
{

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name = "ListaIncidTransport_Binding", Namespace = "urn:microsoft-dynamics-schemas/page/listaincidtransport")]
    public partial class ListaIncidTransport_Service : System.Web.Services.Protocols.SoapHttpClientProtocol
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
        public ListaIncidTransport_Service()
        {
            Entidades.Properties.Settings miConfig = new Entidades.Properties.Settings();
            this.Url = miConfig.urlInicioDefaultWS + "/Page/ListaIncidTransport?WSDL";            
            this.UseDefaultCredentials = true;            

            //this.Url = "http://serv-nsql2008:7047/GINDUSAL/WS/C.N.S.I/Page/ListaIncidTransport?WSDL";
            //this.Credentials = new NetworkCredential("JCA", "Iniker1");
        }

        public ListaIncidTransport_Service(string IDusuario, string pwd)
        {
            // Este constructor se utiliza para obtener el nombre de la empresa con la que
            // trabaja el usuario que ha hecho login en el equipo.
            Entidades.Properties.Settings miConfig = new Entidades.Properties.Settings();
            this.Url = miConfig.urlInicioWS + "/Page/ListaIncidTransport?WSDL";
            if (miConfig.useDefCredts)
                this.UseDefaultCredentials = true;
            else
                this.Credentials = new NetworkCredential(IDusuario, pwd);
        }

        public ListaIncidTransport_Service(string IDusuario, string pwd, string empresa)
        {
            // Constructor para trabajar contra los web services una vez obtenida la empresa de trabajo
            Entidades.Properties.Settings miConfig = new Entidades.Properties.Settings();
            this.Url = miConfig.urlInicioWS + "/" + empresa + "/Page/ListaIncidTransport?WSDL";
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
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:microsoft-dynamics-schemas/page/listaincidtransport:Read", RequestNamespace = "urn:microsoft-dynamics-schemas/page/listaincidtransport", ResponseElementName = "Read_Result", ResponseNamespace = "urn:microsoft-dynamics-schemas/page/listaincidtransport", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("ListaIncidTransport")]
        public ListaIncidTransport Read(string Code)
        {
            object[] results = this.Invoke("Read", new object[] {
                    Code});
            return ((ListaIncidTransport)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginRead(string Code, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("Read", new object[] {
                    Code}, callback, asyncState);
        }

        /// <remarks/>
        public ListaIncidTransport EndRead(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((ListaIncidTransport)(results[0]));
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
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:microsoft-dynamics-schemas/page/listaincidtransport:ReadMultiple", RequestNamespace = "urn:microsoft-dynamics-schemas/page/listaincidtransport", ResponseElementName = "ReadMultiple_Result", ResponseNamespace = "urn:microsoft-dynamics-schemas/page/listaincidtransport", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlArrayAttribute("ReadMultiple_Result")]
        [return: System.Xml.Serialization.XmlArrayItemAttribute(IsNullable = false)]
        public ListaIncidTransport[] ReadMultiple([System.Xml.Serialization.XmlElementAttribute("filter")] ListaIncidTransport_Filter[] filter, string bookmarkKey, int setSize)
        {
            object[] results = this.Invoke("ReadMultiple", new object[] {
                    filter,
                    bookmarkKey,
                    setSize});
            return ((ListaIncidTransport[])(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginReadMultiple(ListaIncidTransport_Filter[] filter, string bookmarkKey, int setSize, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("ReadMultiple", new object[] {
                    filter,
                    bookmarkKey,
                    setSize}, callback, asyncState);
        }

        /// <remarks/>
        public ListaIncidTransport[] EndReadMultiple(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((ListaIncidTransport[])(results[0]));
        }

        /// <remarks/>
        public void ReadMultipleAsync(ListaIncidTransport_Filter[] filter, string bookmarkKey, int setSize)
        {
            this.ReadMultipleAsync(filter, bookmarkKey, setSize, null);
        }

        /// <remarks/>
        public void ReadMultipleAsync(ListaIncidTransport_Filter[] filter, string bookmarkKey, int setSize, object userState)
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
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:microsoft-dynamics-schemas/page/listaincidtransport:IsUpdated", RequestNamespace = "urn:microsoft-dynamics-schemas/page/listaincidtransport", ResponseElementName = "IsUpdated_Result", ResponseNamespace = "urn:microsoft-dynamics-schemas/page/listaincidtransport", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
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
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:microsoft-dynamics-schemas/page/listaincidtransport:Create", RequestNamespace = "urn:microsoft-dynamics-schemas/page/listaincidtransport", ResponseElementName = "Create_Result", ResponseNamespace = "urn:microsoft-dynamics-schemas/page/listaincidtransport", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void Create(ref ListaIncidTransport ListaIncidTransport)
        {
            object[] results = this.Invoke("Create", new object[] {
                    ListaIncidTransport});
            ListaIncidTransport = ((ListaIncidTransport)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginCreate(ListaIncidTransport ListaIncidTransport, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("Create", new object[] {
                    ListaIncidTransport}, callback, asyncState);
        }

        /// <remarks/>
        public void EndCreate(System.IAsyncResult asyncResult, out ListaIncidTransport ListaIncidTransport)
        {
            object[] results = this.EndInvoke(asyncResult);
            ListaIncidTransport = ((ListaIncidTransport)(results[0]));
        }

        /// <remarks/>
        public void CreateAsync(ListaIncidTransport ListaIncidTransport)
        {
            this.CreateAsync(ListaIncidTransport, null);
        }

        /// <remarks/>
        public void CreateAsync(ListaIncidTransport ListaIncidTransport, object userState)
        {
            if ((this.CreateOperationCompleted == null))
            {
                this.CreateOperationCompleted = new System.Threading.SendOrPostCallback(this.OnCreateOperationCompleted);
            }
            this.InvokeAsync("Create", new object[] {
                    ListaIncidTransport}, this.CreateOperationCompleted, userState);
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
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:microsoft-dynamics-schemas/page/listaincidtransport:CreateMultiple", RequestNamespace = "urn:microsoft-dynamics-schemas/page/listaincidtransport", ResponseElementName = "CreateMultiple_Result", ResponseNamespace = "urn:microsoft-dynamics-schemas/page/listaincidtransport", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void CreateMultiple([System.Xml.Serialization.XmlArrayItemAttribute(IsNullable = false)] ref ListaIncidTransport[] ListaIncidTransport_List)
        {
            object[] results = this.Invoke("CreateMultiple", new object[] {
                    ListaIncidTransport_List});
            ListaIncidTransport_List = ((ListaIncidTransport[])(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginCreateMultiple(ListaIncidTransport[] ListaIncidTransport_List, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("CreateMultiple", new object[] {
                    ListaIncidTransport_List}, callback, asyncState);
        }

        /// <remarks/>
        public void EndCreateMultiple(System.IAsyncResult asyncResult, out ListaIncidTransport[] ListaIncidTransport_List)
        {
            object[] results = this.EndInvoke(asyncResult);
            ListaIncidTransport_List = ((ListaIncidTransport[])(results[0]));
        }

        /// <remarks/>
        public void CreateMultipleAsync(ListaIncidTransport[] ListaIncidTransport_List)
        {
            this.CreateMultipleAsync(ListaIncidTransport_List, null);
        }

        /// <remarks/>
        public void CreateMultipleAsync(ListaIncidTransport[] ListaIncidTransport_List, object userState)
        {
            if ((this.CreateMultipleOperationCompleted == null))
            {
                this.CreateMultipleOperationCompleted = new System.Threading.SendOrPostCallback(this.OnCreateMultipleOperationCompleted);
            }
            this.InvokeAsync("CreateMultiple", new object[] {
                    ListaIncidTransport_List}, this.CreateMultipleOperationCompleted, userState);
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
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:microsoft-dynamics-schemas/page/listaincidtransport:Update", RequestNamespace = "urn:microsoft-dynamics-schemas/page/listaincidtransport", ResponseElementName = "Update_Result", ResponseNamespace = "urn:microsoft-dynamics-schemas/page/listaincidtransport", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void Update(ref ListaIncidTransport ListaIncidTransport)
        {
            object[] results = this.Invoke("Update", new object[] {
                    ListaIncidTransport});
            ListaIncidTransport = ((ListaIncidTransport)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginUpdate(ListaIncidTransport ListaIncidTransport, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("Update", new object[] {
                    ListaIncidTransport}, callback, asyncState);
        }

        /// <remarks/>
        public void EndUpdate(System.IAsyncResult asyncResult, out ListaIncidTransport ListaIncidTransport)
        {
            object[] results = this.EndInvoke(asyncResult);
            ListaIncidTransport = ((ListaIncidTransport)(results[0]));
        }

        /// <remarks/>
        public void UpdateAsync(ListaIncidTransport ListaIncidTransport)
        {
            this.UpdateAsync(ListaIncidTransport, null);
        }

        /// <remarks/>
        public void UpdateAsync(ListaIncidTransport ListaIncidTransport, object userState)
        {
            if ((this.UpdateOperationCompleted == null))
            {
                this.UpdateOperationCompleted = new System.Threading.SendOrPostCallback(this.OnUpdateOperationCompleted);
            }
            this.InvokeAsync("Update", new object[] {
                    ListaIncidTransport}, this.UpdateOperationCompleted, userState);
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
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:microsoft-dynamics-schemas/page/listaincidtransport:UpdateMultiple", RequestNamespace = "urn:microsoft-dynamics-schemas/page/listaincidtransport", ResponseElementName = "UpdateMultiple_Result", ResponseNamespace = "urn:microsoft-dynamics-schemas/page/listaincidtransport", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void UpdateMultiple([System.Xml.Serialization.XmlArrayItemAttribute(IsNullable = false)] ref ListaIncidTransport[] ListaIncidTransport_List)
        {
            object[] results = this.Invoke("UpdateMultiple", new object[] {
                    ListaIncidTransport_List});
            ListaIncidTransport_List = ((ListaIncidTransport[])(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginUpdateMultiple(ListaIncidTransport[] ListaIncidTransport_List, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("UpdateMultiple", new object[] {
                    ListaIncidTransport_List}, callback, asyncState);
        }

        /// <remarks/>
        public void EndUpdateMultiple(System.IAsyncResult asyncResult, out ListaIncidTransport[] ListaIncidTransport_List)
        {
            object[] results = this.EndInvoke(asyncResult);
            ListaIncidTransport_List = ((ListaIncidTransport[])(results[0]));
        }

        /// <remarks/>
        public void UpdateMultipleAsync(ListaIncidTransport[] ListaIncidTransport_List)
        {
            this.UpdateMultipleAsync(ListaIncidTransport_List, null);
        }

        /// <remarks/>
        public void UpdateMultipleAsync(ListaIncidTransport[] ListaIncidTransport_List, object userState)
        {
            if ((this.UpdateMultipleOperationCompleted == null))
            {
                this.UpdateMultipleOperationCompleted = new System.Threading.SendOrPostCallback(this.OnUpdateMultipleOperationCompleted);
            }
            this.InvokeAsync("UpdateMultiple", new object[] {
                    ListaIncidTransport_List}, this.UpdateMultipleOperationCompleted, userState);
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
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:microsoft-dynamics-schemas/page/listaincidtransport:Delete", RequestNamespace = "urn:microsoft-dynamics-schemas/page/listaincidtransport", ResponseElementName = "Delete_Result", ResponseNamespace = "urn:microsoft-dynamics-schemas/page/listaincidtransport", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:microsoft-dynamics-schemas/page/listaincidtransport")]
    public partial class ListaIncidTransport
    {

        private string keyField;

        private string codeField;

        private string interaction_Group_CodeField;

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
        public string Interaction_Group_Code
        {
            get
            {
                return this.interaction_Group_CodeField;
            }
            set
            {
                this.interaction_Group_CodeField = value;
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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:microsoft-dynamics-schemas/page/listaincidtransport")]
    public partial class ListaIncidTransport_Filter
    {

        private ListaIncidTransport_Fields fieldField;

        private string criteriaField;

        /// <comentarios/>
        public ListaIncidTransport_Fields Field
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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:microsoft-dynamics-schemas/page/listaincidtransport")]
    public enum ListaIncidTransport_Fields
    {

        /// <comentarios/>
        Code,

        /// <comentarios/>
        Interaction_Group_Code,

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
        public ListaIncidTransport Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((ListaIncidTransport)(this.results[0]));
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
        public ListaIncidTransport[] Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((ListaIncidTransport[])(this.results[0]));
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
        public ListaIncidTransport ListaIncidTransport
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((ListaIncidTransport)(this.results[0]));
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
        public ListaIncidTransport[] ListaIncidTransport_List
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((ListaIncidTransport[])(this.results[0]));
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
        public ListaIncidTransport ListaIncidTransport
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((ListaIncidTransport)(this.results[0]));
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
        public ListaIncidTransport[] ListaIncidTransport_List
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((ListaIncidTransport[])(this.results[0]));
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