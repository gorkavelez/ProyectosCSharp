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
using System.Net;
using System.ComponentModel;
using System.Diagnostics;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Serialization;

// 
// This source code was auto-generated by wsdl, Version=2.0.50727.42.
// 

namespace INIKER.Transportistas
{

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name = "ListaTransportistasINDUSAL_Binding", Namespace = "urn:microsoft-dynamics-schemas/page/listatransportistasindusal")]
    public partial class ListaTransportistasINDUSAL_Service : System.Web.Services.Protocols.SoapHttpClientProtocol
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
        public ListaTransportistasINDUSAL_Service()
        {
            Entidades.Properties.Settings miConfig = new Entidades.Properties.Settings();
            this.Url = miConfig.urlInicioDefaultWS + "/Page/ListaTransportistasINDUSAL?WSDL";
            this.UseDefaultCredentials = true;

            //this.Url = "http://serv-nsql2008:7047/GINDUSAL/WS/02 NAVARRA/Page/ListaTransportistasINDUSAL?WSDL";                
            //this.Credentials = new NetworkCredential("JCA", "Iniker1");
        }

        public ListaTransportistasINDUSAL_Service(string IDusuario, string pwd)
        {
            // Este constructor se utiliza para obtener el nombre de la empresa con la que
            // trabaja el usuario que ha hecho login en el equipo.
            Entidades.Properties.Settings miConfig = new Entidades.Properties.Settings();
            this.Url = miConfig.urlInicioWS + "/Page/ListaTransportistasINDUSAL?WSDL";
            if (miConfig.useDefCredts)
                this.UseDefaultCredentials = true;
            else
                this.Credentials = new NetworkCredential(IDusuario, pwd);
        }

        public ListaTransportistasINDUSAL_Service(string IDusuario, string pwd, string empresa)
        {
            // Constructor para trabajar contra los web services una vez obtenida la empresa de trabajo
            Entidades.Properties.Settings miConfig = new Entidades.Properties.Settings();
            this.Url = miConfig.urlInicioWS + "/" + empresa + "/Page/ListaTransportistasINDUSAL?WSDL";
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
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:microsoft-dynamics-schemas/page/listatransportistasindusal:Read", RequestNamespace = "urn:microsoft-dynamics-schemas/page/listatransportistasindusal", ResponseElementName = "Read_Result", ResponseNamespace = "urn:microsoft-dynamics-schemas/page/listatransportistasindusal", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("ListaTransportistasINDUSAL")]
        public ListaTransportistasINDUSAL Read(string Code)
        {
            object[] results = this.Invoke("Read", new object[] {
                    Code});
            return ((ListaTransportistasINDUSAL)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginRead(string Code, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("Read", new object[] {
                    Code}, callback, asyncState);
        }

        /// <remarks/>
        public ListaTransportistasINDUSAL EndRead(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((ListaTransportistasINDUSAL)(results[0]));
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
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:microsoft-dynamics-schemas/page/listatransportistasindusal:ReadMultiple", RequestNamespace = "urn:microsoft-dynamics-schemas/page/listatransportistasindusal", ResponseElementName = "ReadMultiple_Result", ResponseNamespace = "urn:microsoft-dynamics-schemas/page/listatransportistasindusal", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlArrayAttribute("ReadMultiple_Result")]
        [return: System.Xml.Serialization.XmlArrayItemAttribute(IsNullable = false)]
        public ListaTransportistasINDUSAL[] ReadMultiple([System.Xml.Serialization.XmlElementAttribute("filter")] ListaTransportistasINDUSAL_Filter[] filter, string bookmarkKey, int setSize)
        {
            object[] results = this.Invoke("ReadMultiple", new object[] {
                    filter,
                    bookmarkKey,
                    setSize});
            return ((ListaTransportistasINDUSAL[])(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginReadMultiple(ListaTransportistasINDUSAL_Filter[] filter, string bookmarkKey, int setSize, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("ReadMultiple", new object[] {
                    filter,
                    bookmarkKey,
                    setSize}, callback, asyncState);
        }

        /// <remarks/>
        public ListaTransportistasINDUSAL[] EndReadMultiple(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((ListaTransportistasINDUSAL[])(results[0]));
        }

        /// <remarks/>
        public void ReadMultipleAsync(ListaTransportistasINDUSAL_Filter[] filter, string bookmarkKey, int setSize)
        {
            this.ReadMultipleAsync(filter, bookmarkKey, setSize, null);
        }

        /// <remarks/>
        public void ReadMultipleAsync(ListaTransportistasINDUSAL_Filter[] filter, string bookmarkKey, int setSize, object userState)
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
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:microsoft-dynamics-schemas/page/listatransportistasindusal:IsUpdated", RequestNamespace = "urn:microsoft-dynamics-schemas/page/listatransportistasindusal", ResponseElementName = "IsUpdated_Result", ResponseNamespace = "urn:microsoft-dynamics-schemas/page/listatransportistasindusal", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
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
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:microsoft-dynamics-schemas/page/listatransportistasindusal:Create", RequestNamespace = "urn:microsoft-dynamics-schemas/page/listatransportistasindusal", ResponseElementName = "Create_Result", ResponseNamespace = "urn:microsoft-dynamics-schemas/page/listatransportistasindusal", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void Create(ref ListaTransportistasINDUSAL ListaTransportistasINDUSAL)
        {
            object[] results = this.Invoke("Create", new object[] {
                    ListaTransportistasINDUSAL});
            ListaTransportistasINDUSAL = ((ListaTransportistasINDUSAL)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginCreate(ListaTransportistasINDUSAL ListaTransportistasINDUSAL, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("Create", new object[] {
                    ListaTransportistasINDUSAL}, callback, asyncState);
        }

        /// <remarks/>
        public void EndCreate(System.IAsyncResult asyncResult, out ListaTransportistasINDUSAL ListaTransportistasINDUSAL)
        {
            object[] results = this.EndInvoke(asyncResult);
            ListaTransportistasINDUSAL = ((ListaTransportistasINDUSAL)(results[0]));
        }

        /// <remarks/>
        public void CreateAsync(ListaTransportistasINDUSAL ListaTransportistasINDUSAL)
        {
            this.CreateAsync(ListaTransportistasINDUSAL, null);
        }

        /// <remarks/>
        public void CreateAsync(ListaTransportistasINDUSAL ListaTransportistasINDUSAL, object userState)
        {
            if ((this.CreateOperationCompleted == null))
            {
                this.CreateOperationCompleted = new System.Threading.SendOrPostCallback(this.OnCreateOperationCompleted);
            }
            this.InvokeAsync("Create", new object[] {
                    ListaTransportistasINDUSAL}, this.CreateOperationCompleted, userState);
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
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:microsoft-dynamics-schemas/page/listatransportistasindusal:CreateMultiple", RequestNamespace = "urn:microsoft-dynamics-schemas/page/listatransportistasindusal", ResponseElementName = "CreateMultiple_Result", ResponseNamespace = "urn:microsoft-dynamics-schemas/page/listatransportistasindusal", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void CreateMultiple([System.Xml.Serialization.XmlArrayItemAttribute(IsNullable = false)] ref ListaTransportistasINDUSAL[] ListaTransportistasINDUSAL_List)
        {
            object[] results = this.Invoke("CreateMultiple", new object[] {
                    ListaTransportistasINDUSAL_List});
            ListaTransportistasINDUSAL_List = ((ListaTransportistasINDUSAL[])(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginCreateMultiple(ListaTransportistasINDUSAL[] ListaTransportistasINDUSAL_List, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("CreateMultiple", new object[] {
                    ListaTransportistasINDUSAL_List}, callback, asyncState);
        }

        /// <remarks/>
        public void EndCreateMultiple(System.IAsyncResult asyncResult, out ListaTransportistasINDUSAL[] ListaTransportistasINDUSAL_List)
        {
            object[] results = this.EndInvoke(asyncResult);
            ListaTransportistasINDUSAL_List = ((ListaTransportistasINDUSAL[])(results[0]));
        }

        /// <remarks/>
        public void CreateMultipleAsync(ListaTransportistasINDUSAL[] ListaTransportistasINDUSAL_List)
        {
            this.CreateMultipleAsync(ListaTransportistasINDUSAL_List, null);
        }

        /// <remarks/>
        public void CreateMultipleAsync(ListaTransportistasINDUSAL[] ListaTransportistasINDUSAL_List, object userState)
        {
            if ((this.CreateMultipleOperationCompleted == null))
            {
                this.CreateMultipleOperationCompleted = new System.Threading.SendOrPostCallback(this.OnCreateMultipleOperationCompleted);
            }
            this.InvokeAsync("CreateMultiple", new object[] {
                    ListaTransportistasINDUSAL_List}, this.CreateMultipleOperationCompleted, userState);
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
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:microsoft-dynamics-schemas/page/listatransportistasindusal:Update", RequestNamespace = "urn:microsoft-dynamics-schemas/page/listatransportistasindusal", ResponseElementName = "Update_Result", ResponseNamespace = "urn:microsoft-dynamics-schemas/page/listatransportistasindusal", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void Update(ref ListaTransportistasINDUSAL ListaTransportistasINDUSAL)
        {
            object[] results = this.Invoke("Update", new object[] {
                    ListaTransportistasINDUSAL});
            ListaTransportistasINDUSAL = ((ListaTransportistasINDUSAL)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginUpdate(ListaTransportistasINDUSAL ListaTransportistasINDUSAL, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("Update", new object[] {
                    ListaTransportistasINDUSAL}, callback, asyncState);
        }

        /// <remarks/>
        public void EndUpdate(System.IAsyncResult asyncResult, out ListaTransportistasINDUSAL ListaTransportistasINDUSAL)
        {
            object[] results = this.EndInvoke(asyncResult);
            ListaTransportistasINDUSAL = ((ListaTransportistasINDUSAL)(results[0]));
        }

        /// <remarks/>
        public void UpdateAsync(ListaTransportistasINDUSAL ListaTransportistasINDUSAL)
        {
            this.UpdateAsync(ListaTransportistasINDUSAL, null);
        }

        /// <remarks/>
        public void UpdateAsync(ListaTransportistasINDUSAL ListaTransportistasINDUSAL, object userState)
        {
            if ((this.UpdateOperationCompleted == null))
            {
                this.UpdateOperationCompleted = new System.Threading.SendOrPostCallback(this.OnUpdateOperationCompleted);
            }
            this.InvokeAsync("Update", new object[] {
                    ListaTransportistasINDUSAL}, this.UpdateOperationCompleted, userState);
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
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:microsoft-dynamics-schemas/page/listatransportistasindusal:UpdateMultiple", RequestNamespace = "urn:microsoft-dynamics-schemas/page/listatransportistasindusal", ResponseElementName = "UpdateMultiple_Result", ResponseNamespace = "urn:microsoft-dynamics-schemas/page/listatransportistasindusal", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void UpdateMultiple([System.Xml.Serialization.XmlArrayItemAttribute(IsNullable = false)] ref ListaTransportistasINDUSAL[] ListaTransportistasINDUSAL_List)
        {
            object[] results = this.Invoke("UpdateMultiple", new object[] {
                    ListaTransportistasINDUSAL_List});
            ListaTransportistasINDUSAL_List = ((ListaTransportistasINDUSAL[])(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginUpdateMultiple(ListaTransportistasINDUSAL[] ListaTransportistasINDUSAL_List, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("UpdateMultiple", new object[] {
                    ListaTransportistasINDUSAL_List}, callback, asyncState);
        }

        /// <remarks/>
        public void EndUpdateMultiple(System.IAsyncResult asyncResult, out ListaTransportistasINDUSAL[] ListaTransportistasINDUSAL_List)
        {
            object[] results = this.EndInvoke(asyncResult);
            ListaTransportistasINDUSAL_List = ((ListaTransportistasINDUSAL[])(results[0]));
        }

        /// <remarks/>
        public void UpdateMultipleAsync(ListaTransportistasINDUSAL[] ListaTransportistasINDUSAL_List)
        {
            this.UpdateMultipleAsync(ListaTransportistasINDUSAL_List, null);
        }

        /// <remarks/>
        public void UpdateMultipleAsync(ListaTransportistasINDUSAL[] ListaTransportistasINDUSAL_List, object userState)
        {
            if ((this.UpdateMultipleOperationCompleted == null))
            {
                this.UpdateMultipleOperationCompleted = new System.Threading.SendOrPostCallback(this.OnUpdateMultipleOperationCompleted);
            }
            this.InvokeAsync("UpdateMultiple", new object[] {
                    ListaTransportistasINDUSAL_List}, this.UpdateMultipleOperationCompleted, userState);
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
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:microsoft-dynamics-schemas/page/listatransportistasindusal:Delete", RequestNamespace = "urn:microsoft-dynamics-schemas/page/listatransportistasindusal", ResponseElementName = "Delete_Result", ResponseNamespace = "urn:microsoft-dynamics-schemas/page/listatransportistasindusal", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:microsoft-dynamics-schemas/page/listatransportistasindusal")]
    public partial class ListaTransportistasINDUSAL
    {

        private string keyField;

        private string codeField;

        private string nameField;

        private string internet_AddressField;

        private string account_NoField;

        private string empresa_transportistaField;

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
        public string Internet_Address
        {
            get
            {
                return this.internet_AddressField;
            }
            set
            {
                this.internet_AddressField = value;
            }
        }

        /// <comentarios/>
        public string Account_No
        {
            get
            {
                return this.account_NoField;
            }
            set
            {
                this.account_NoField = value;
            }
        }

        /// <comentarios/>
        public string Empresa_transportista
        {
            get
            {
                return this.empresa_transportistaField;
            }
            set
            {
                this.empresa_transportistaField = value;
            }
        }
    }

    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:microsoft-dynamics-schemas/page/listatransportistasindusal")]
    public partial class ListaTransportistasINDUSAL_Filter
    {

        private ListaTransportistasINDUSAL_Fields fieldField;

        private string criteriaField;

        /// <comentarios/>
        public ListaTransportistasINDUSAL_Fields Field
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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:microsoft-dynamics-schemas/page/listatransportistasindusal")]
    public enum ListaTransportistasINDUSAL_Fields
    {

        /// <comentarios/>
        Code,

        /// <comentarios/>
        Name,

        /// <comentarios/>
        Internet_Address,

        /// <comentarios/>
        Account_No,

        /// <comentarios/>
        Empresa_transportista,
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
        public ListaTransportistasINDUSAL Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((ListaTransportistasINDUSAL)(this.results[0]));
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
        public ListaTransportistasINDUSAL[] Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((ListaTransportistasINDUSAL[])(this.results[0]));
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
        public ListaTransportistasINDUSAL ListaTransportistasINDUSAL
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((ListaTransportistasINDUSAL)(this.results[0]));
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
        public ListaTransportistasINDUSAL[] ListaTransportistasINDUSAL_List
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((ListaTransportistasINDUSAL[])(this.results[0]));
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
        public ListaTransportistasINDUSAL ListaTransportistasINDUSAL
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((ListaTransportistasINDUSAL)(this.results[0]));
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
        public ListaTransportistasINDUSAL[] ListaTransportistasINDUSAL_List
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((ListaTransportistasINDUSAL[])(this.results[0]));
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