﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión del motor en tiempo de ejecución:2.0.50727.3082
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

namespace INIKER.CategoriasProducto
{

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name = "ListaCategoriasProductoINDUSAL_Binding", Namespace = "urn:microsoft-dynamics-schemas/page/listacategoriasproductoindusal")]
    public partial class ListaCategoriasProductoINDUSAL_Service : System.Web.Services.Protocols.SoapHttpClientProtocol
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
        public ListaCategoriasProductoINDUSAL_Service()
        {
            Entidades.Properties.Settings miConfig = new Entidades.Properties.Settings();
            this.Url = miConfig.urlInicioDefaultWS + "/Page/ListaCategoriasProductoINDUSAL?WSDL";            
            this.UseDefaultCredentials = true;            

            //this.Url = "http://serv-nsql2008:7047/GINDUSAL/WS/02 NAVARRA/Page/ListaCategoriasProductoINDUSAL?WSDL";
            //this.Credentials = new NetworkCredential("JCA", "Iniker1");
        }

        public ListaCategoriasProductoINDUSAL_Service(string IDusuario, string pwd)
        {
            // Este constructor se utiliza para obtener el nombre de la empresa con la que
            // trabaja el usuario que ha hecho login en el equipo.
            Entidades.Properties.Settings miConfig = new Entidades.Properties.Settings();
            this.Url = miConfig.urlInicioWS + "/Page/ListaCategoriasProductoINDUSAL?WSDL";
            if (miConfig.useDefCredts)
                this.UseDefaultCredentials = true;
            else
                this.Credentials = new NetworkCredential(IDusuario, pwd);
        }

        public ListaCategoriasProductoINDUSAL_Service(string IDusuario, string pwd, string empresa)
        {
            // Constructor para trabajar contra los web services una vez obtenida la empresa de trabajo
            Entidades.Properties.Settings miConfig = new Entidades.Properties.Settings();
            this.Url = miConfig.urlInicioWS + "/" + empresa + "/Page/ListaCategoriasProductoINDUSAL?WSDL";
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
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:microsoft-dynamics-schemas/page/listacategoriasproductoindusal:Read", RequestNamespace = "urn:microsoft-dynamics-schemas/page/listacategoriasproductoindusal", ResponseElementName = "Read_Result", ResponseNamespace = "urn:microsoft-dynamics-schemas/page/listacategoriasproductoindusal", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("ListaCategoriasProductoINDUSAL")]
        public ListaCategoriasProductoINDUSAL Read(string Code)
        {
            object[] results = this.Invoke("Read", new object[] {
                    Code});
            return ((ListaCategoriasProductoINDUSAL)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginRead(string Code, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("Read", new object[] {
                    Code}, callback, asyncState);
        }

        /// <remarks/>
        public ListaCategoriasProductoINDUSAL EndRead(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((ListaCategoriasProductoINDUSAL)(results[0]));
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
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:microsoft-dynamics-schemas/page/listacategoriasproductoindusal:ReadMultiple", RequestNamespace = "urn:microsoft-dynamics-schemas/page/listacategoriasproductoindusal", ResponseElementName = "ReadMultiple_Result", ResponseNamespace = "urn:microsoft-dynamics-schemas/page/listacategoriasproductoindusal", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlArrayAttribute("ReadMultiple_Result")]
        [return: System.Xml.Serialization.XmlArrayItemAttribute(IsNullable = false)]
        public ListaCategoriasProductoINDUSAL[] ReadMultiple([System.Xml.Serialization.XmlElementAttribute("filter")] ListaCategoriasProductoINDUSAL_Filter[] filter, string bookmarkKey, int setSize)
        {
            object[] results = this.Invoke("ReadMultiple", new object[] {
                    filter,
                    bookmarkKey,
                    setSize});
            return ((ListaCategoriasProductoINDUSAL[])(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginReadMultiple(ListaCategoriasProductoINDUSAL_Filter[] filter, string bookmarkKey, int setSize, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("ReadMultiple", new object[] {
                    filter,
                    bookmarkKey,
                    setSize}, callback, asyncState);
        }

        /// <remarks/>
        public ListaCategoriasProductoINDUSAL[] EndReadMultiple(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((ListaCategoriasProductoINDUSAL[])(results[0]));
        }

        /// <remarks/>
        public void ReadMultipleAsync(ListaCategoriasProductoINDUSAL_Filter[] filter, string bookmarkKey, int setSize)
        {
            this.ReadMultipleAsync(filter, bookmarkKey, setSize, null);
        }

        /// <remarks/>
        public void ReadMultipleAsync(ListaCategoriasProductoINDUSAL_Filter[] filter, string bookmarkKey, int setSize, object userState)
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
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:microsoft-dynamics-schemas/page/listacategoriasproductoindusal:IsUpdated", RequestNamespace = "urn:microsoft-dynamics-schemas/page/listacategoriasproductoindusal", ResponseElementName = "IsUpdated_Result", ResponseNamespace = "urn:microsoft-dynamics-schemas/page/listacategoriasproductoindusal", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
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
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:microsoft-dynamics-schemas/page/listacategoriasproductoindusal:Create", RequestNamespace = "urn:microsoft-dynamics-schemas/page/listacategoriasproductoindusal", ResponseElementName = "Create_Result", ResponseNamespace = "urn:microsoft-dynamics-schemas/page/listacategoriasproductoindusal", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void Create(ref ListaCategoriasProductoINDUSAL ListaCategoriasProductoINDUSAL)
        {
            object[] results = this.Invoke("Create", new object[] {
                    ListaCategoriasProductoINDUSAL});
            ListaCategoriasProductoINDUSAL = ((ListaCategoriasProductoINDUSAL)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginCreate(ListaCategoriasProductoINDUSAL ListaCategoriasProductoINDUSAL, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("Create", new object[] {
                    ListaCategoriasProductoINDUSAL}, callback, asyncState);
        }

        /// <remarks/>
        public void EndCreate(System.IAsyncResult asyncResult, out ListaCategoriasProductoINDUSAL ListaCategoriasProductoINDUSAL)
        {
            object[] results = this.EndInvoke(asyncResult);
            ListaCategoriasProductoINDUSAL = ((ListaCategoriasProductoINDUSAL)(results[0]));
        }

        /// <remarks/>
        public void CreateAsync(ListaCategoriasProductoINDUSAL ListaCategoriasProductoINDUSAL)
        {
            this.CreateAsync(ListaCategoriasProductoINDUSAL, null);
        }

        /// <remarks/>
        public void CreateAsync(ListaCategoriasProductoINDUSAL ListaCategoriasProductoINDUSAL, object userState)
        {
            if ((this.CreateOperationCompleted == null))
            {
                this.CreateOperationCompleted = new System.Threading.SendOrPostCallback(this.OnCreateOperationCompleted);
            }
            this.InvokeAsync("Create", new object[] {
                    ListaCategoriasProductoINDUSAL}, this.CreateOperationCompleted, userState);
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
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:microsoft-dynamics-schemas/page/listacategoriasproductoindusal:CreateMultiple" +
            "", RequestNamespace = "urn:microsoft-dynamics-schemas/page/listacategoriasproductoindusal", ResponseElementName = "CreateMultiple_Result", ResponseNamespace = "urn:microsoft-dynamics-schemas/page/listacategoriasproductoindusal", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void CreateMultiple([System.Xml.Serialization.XmlArrayItemAttribute(IsNullable = false)] ref ListaCategoriasProductoINDUSAL[] ListaCategoriasProductoINDUSAL_List)
        {
            object[] results = this.Invoke("CreateMultiple", new object[] {
                    ListaCategoriasProductoINDUSAL_List});
            ListaCategoriasProductoINDUSAL_List = ((ListaCategoriasProductoINDUSAL[])(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginCreateMultiple(ListaCategoriasProductoINDUSAL[] ListaCategoriasProductoINDUSAL_List, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("CreateMultiple", new object[] {
                    ListaCategoriasProductoINDUSAL_List}, callback, asyncState);
        }

        /// <remarks/>
        public void EndCreateMultiple(System.IAsyncResult asyncResult, out ListaCategoriasProductoINDUSAL[] ListaCategoriasProductoINDUSAL_List)
        {
            object[] results = this.EndInvoke(asyncResult);
            ListaCategoriasProductoINDUSAL_List = ((ListaCategoriasProductoINDUSAL[])(results[0]));
        }

        /// <remarks/>
        public void CreateMultipleAsync(ListaCategoriasProductoINDUSAL[] ListaCategoriasProductoINDUSAL_List)
        {
            this.CreateMultipleAsync(ListaCategoriasProductoINDUSAL_List, null);
        }

        /// <remarks/>
        public void CreateMultipleAsync(ListaCategoriasProductoINDUSAL[] ListaCategoriasProductoINDUSAL_List, object userState)
        {
            if ((this.CreateMultipleOperationCompleted == null))
            {
                this.CreateMultipleOperationCompleted = new System.Threading.SendOrPostCallback(this.OnCreateMultipleOperationCompleted);
            }
            this.InvokeAsync("CreateMultiple", new object[] {
                    ListaCategoriasProductoINDUSAL_List}, this.CreateMultipleOperationCompleted, userState);
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
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:microsoft-dynamics-schemas/page/listacategoriasproductoindusal:Update", RequestNamespace = "urn:microsoft-dynamics-schemas/page/listacategoriasproductoindusal", ResponseElementName = "Update_Result", ResponseNamespace = "urn:microsoft-dynamics-schemas/page/listacategoriasproductoindusal", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void Update(ref ListaCategoriasProductoINDUSAL ListaCategoriasProductoINDUSAL)
        {
            object[] results = this.Invoke("Update", new object[] {
                    ListaCategoriasProductoINDUSAL});
            ListaCategoriasProductoINDUSAL = ((ListaCategoriasProductoINDUSAL)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginUpdate(ListaCategoriasProductoINDUSAL ListaCategoriasProductoINDUSAL, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("Update", new object[] {
                    ListaCategoriasProductoINDUSAL}, callback, asyncState);
        }

        /// <remarks/>
        public void EndUpdate(System.IAsyncResult asyncResult, out ListaCategoriasProductoINDUSAL ListaCategoriasProductoINDUSAL)
        {
            object[] results = this.EndInvoke(asyncResult);
            ListaCategoriasProductoINDUSAL = ((ListaCategoriasProductoINDUSAL)(results[0]));
        }

        /// <remarks/>
        public void UpdateAsync(ListaCategoriasProductoINDUSAL ListaCategoriasProductoINDUSAL)
        {
            this.UpdateAsync(ListaCategoriasProductoINDUSAL, null);
        }

        /// <remarks/>
        public void UpdateAsync(ListaCategoriasProductoINDUSAL ListaCategoriasProductoINDUSAL, object userState)
        {
            if ((this.UpdateOperationCompleted == null))
            {
                this.UpdateOperationCompleted = new System.Threading.SendOrPostCallback(this.OnUpdateOperationCompleted);
            }
            this.InvokeAsync("Update", new object[] {
                    ListaCategoriasProductoINDUSAL}, this.UpdateOperationCompleted, userState);
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
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:microsoft-dynamics-schemas/page/listacategoriasproductoindusal:UpdateMultiple" +
            "", RequestNamespace = "urn:microsoft-dynamics-schemas/page/listacategoriasproductoindusal", ResponseElementName = "UpdateMultiple_Result", ResponseNamespace = "urn:microsoft-dynamics-schemas/page/listacategoriasproductoindusal", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void UpdateMultiple([System.Xml.Serialization.XmlArrayItemAttribute(IsNullable = false)] ref ListaCategoriasProductoINDUSAL[] ListaCategoriasProductoINDUSAL_List)
        {
            object[] results = this.Invoke("UpdateMultiple", new object[] {
                    ListaCategoriasProductoINDUSAL_List});
            ListaCategoriasProductoINDUSAL_List = ((ListaCategoriasProductoINDUSAL[])(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginUpdateMultiple(ListaCategoriasProductoINDUSAL[] ListaCategoriasProductoINDUSAL_List, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("UpdateMultiple", new object[] {
                    ListaCategoriasProductoINDUSAL_List}, callback, asyncState);
        }

        /// <remarks/>
        public void EndUpdateMultiple(System.IAsyncResult asyncResult, out ListaCategoriasProductoINDUSAL[] ListaCategoriasProductoINDUSAL_List)
        {
            object[] results = this.EndInvoke(asyncResult);
            ListaCategoriasProductoINDUSAL_List = ((ListaCategoriasProductoINDUSAL[])(results[0]));
        }

        /// <remarks/>
        public void UpdateMultipleAsync(ListaCategoriasProductoINDUSAL[] ListaCategoriasProductoINDUSAL_List)
        {
            this.UpdateMultipleAsync(ListaCategoriasProductoINDUSAL_List, null);
        }

        /// <remarks/>
        public void UpdateMultipleAsync(ListaCategoriasProductoINDUSAL[] ListaCategoriasProductoINDUSAL_List, object userState)
        {
            if ((this.UpdateMultipleOperationCompleted == null))
            {
                this.UpdateMultipleOperationCompleted = new System.Threading.SendOrPostCallback(this.OnUpdateMultipleOperationCompleted);
            }
            this.InvokeAsync("UpdateMultiple", new object[] {
                    ListaCategoriasProductoINDUSAL_List}, this.UpdateMultipleOperationCompleted, userState);
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
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:microsoft-dynamics-schemas/page/listacategoriasproductoindusal:Delete", RequestNamespace = "urn:microsoft-dynamics-schemas/page/listacategoriasproductoindusal", ResponseElementName = "Delete_Result", ResponseNamespace = "urn:microsoft-dynamics-schemas/page/listacategoriasproductoindusal", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:microsoft-dynamics-schemas/page/listacategoriasproductoindusal")]
    public partial class ListaCategoriasProductoINDUSAL
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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:microsoft-dynamics-schemas/page/listacategoriasproductoindusal")]
    public partial class ListaCategoriasProductoINDUSAL_Filter
    {

        private ListaCategoriasProductoINDUSAL_Fields fieldField;

        private string criteriaField;

        /// <comentarios/>
        public ListaCategoriasProductoINDUSAL_Fields Field
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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:microsoft-dynamics-schemas/page/listacategoriasproductoindusal")]
    public enum ListaCategoriasProductoINDUSAL_Fields
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
        public ListaCategoriasProductoINDUSAL Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((ListaCategoriasProductoINDUSAL)(this.results[0]));
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
        public ListaCategoriasProductoINDUSAL[] Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((ListaCategoriasProductoINDUSAL[])(this.results[0]));
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
        public ListaCategoriasProductoINDUSAL ListaCategoriasProductoINDUSAL
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((ListaCategoriasProductoINDUSAL)(this.results[0]));
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
        public ListaCategoriasProductoINDUSAL[] ListaCategoriasProductoINDUSAL_List
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((ListaCategoriasProductoINDUSAL[])(this.results[0]));
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
        public ListaCategoriasProductoINDUSAL ListaCategoriasProductoINDUSAL
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((ListaCategoriasProductoINDUSAL)(this.results[0]));
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
        public ListaCategoriasProductoINDUSAL[] ListaCategoriasProductoINDUSAL_List
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((ListaCategoriasProductoINDUSAL[])(this.results[0]));
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