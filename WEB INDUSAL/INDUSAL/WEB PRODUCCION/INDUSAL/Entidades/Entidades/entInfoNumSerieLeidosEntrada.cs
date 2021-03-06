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

namespace INIKER.NumerosSerieProduccion
{

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name = "InfoNumSerieLeidosEntrada_Binding", Namespace = "urn:microsoft-dynamics-schemas/page/infonumserieleidosentrada")]
    public partial class InfoNumSerieLeidosEntrada_Service : System.Web.Services.Protocols.SoapHttpClientProtocol
    {

        private System.Threading.SendOrPostCallback ReadMultipleOperationCompleted;

        private System.Threading.SendOrPostCallback IsUpdatedOperationCompleted;

        private System.Threading.SendOrPostCallback CreateOperationCompleted;

        private System.Threading.SendOrPostCallback CreateMultipleOperationCompleted;

        private System.Threading.SendOrPostCallback UpdateOperationCompleted;

        private System.Threading.SendOrPostCallback UpdateMultipleOperationCompleted;

        private System.Threading.SendOrPostCallback DeleteOperationCompleted;

        /// <remarks/>        
        public InfoNumSerieLeidosEntrada_Service()
        {
            Entidades.Properties.Settings miConfig = new Entidades.Properties.Settings();
            this.Url = miConfig.urlInicioDefaultWS + "/Page/InfoNumSerieLeidosEntrada?WSDL";
            this.UseDefaultCredentials = true;
            //this.Url = "http://serv-nsql2008:7047/GINDUSAL/WS/02%20NAVARRA/Page/InfoNumSerieLeidosEntrada?WSDL";
            //this.Credentials = new NetworkCredential("JCA", "Iniker1");
        }

        public InfoNumSerieLeidosEntrada_Service(string IDusuario, string pwd)
        {
            // Este constructor se utiliza para obtener el nombre de la empresa con la que
            // trabaja el usuario que ha hecho login en el equipo.
            Entidades.Properties.Settings miConfig = new Entidades.Properties.Settings();
            this.Url = miConfig.urlInicioWS + "/Page/InfoNumSerieLeidosEntrada?WSDL";
            if (miConfig.useDefCredts)
                this.UseDefaultCredentials = true;
            else
                this.Credentials = new NetworkCredential(IDusuario, pwd);
        }

        public InfoNumSerieLeidosEntrada_Service(string IDusuario, string pwd, string empresa)
        {
            // Constructor para trabajar contra los web services una vez obtenida la empresa de trabajo
            Entidades.Properties.Settings miConfig = new Entidades.Properties.Settings();
            this.Url = miConfig.urlInicioWS + "/" + empresa + "/Page/InfoNumSerieLeidosEntrada?WSDL";
            if (miConfig.useDefCredts)
                this.UseDefaultCredentials = true;
            else
                this.Credentials = new NetworkCredential(IDusuario, pwd);
        }

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
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:microsoft-dynamics-schemas/page/infonumserieleidosentrada:ReadMultiple", RequestNamespace = "urn:microsoft-dynamics-schemas/page/infonumserieleidosentrada", ResponseElementName = "ReadMultiple_Result", ResponseNamespace = "urn:microsoft-dynamics-schemas/page/infonumserieleidosentrada", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlArrayAttribute("ReadMultiple_Result")]
        [return: System.Xml.Serialization.XmlArrayItemAttribute(IsNullable = false)]
        public InfoNumSerieLeidosEntrada[] ReadMultiple([System.Xml.Serialization.XmlElementAttribute("filter")] InfoNumSerieLeidosEntrada_Filter[] filter, string bookmarkKey, int setSize)
        {
            object[] results = this.Invoke("ReadMultiple", new object[] {
                    filter,
                    bookmarkKey,
                    setSize});
            return ((InfoNumSerieLeidosEntrada[])(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginReadMultiple(InfoNumSerieLeidosEntrada_Filter[] filter, string bookmarkKey, int setSize, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("ReadMultiple", new object[] {
                    filter,
                    bookmarkKey,
                    setSize}, callback, asyncState);
        }

        /// <remarks/>
        public InfoNumSerieLeidosEntrada[] EndReadMultiple(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((InfoNumSerieLeidosEntrada[])(results[0]));
        }

        /// <remarks/>
        public void ReadMultipleAsync(InfoNumSerieLeidosEntrada_Filter[] filter, string bookmarkKey, int setSize)
        {
            this.ReadMultipleAsync(filter, bookmarkKey, setSize, null);
        }

        /// <remarks/>
        public void ReadMultipleAsync(InfoNumSerieLeidosEntrada_Filter[] filter, string bookmarkKey, int setSize, object userState)
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
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:microsoft-dynamics-schemas/page/infonumserieleidosentrada:IsUpdated", RequestNamespace = "urn:microsoft-dynamics-schemas/page/infonumserieleidosentrada", ResponseElementName = "IsUpdated_Result", ResponseNamespace = "urn:microsoft-dynamics-schemas/page/infonumserieleidosentrada", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
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
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:microsoft-dynamics-schemas/page/infonumserieleidosentrada:Create", RequestNamespace = "urn:microsoft-dynamics-schemas/page/infonumserieleidosentrada", ResponseElementName = "Create_Result", ResponseNamespace = "urn:microsoft-dynamics-schemas/page/infonumserieleidosentrada", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void Create(ref InfoNumSerieLeidosEntrada InfoNumSerieLeidosEntrada)
        {
            object[] results = this.Invoke("Create", new object[] {
                    InfoNumSerieLeidosEntrada});
            InfoNumSerieLeidosEntrada = ((InfoNumSerieLeidosEntrada)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginCreate(InfoNumSerieLeidosEntrada InfoNumSerieLeidosEntrada, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("Create", new object[] {
                    InfoNumSerieLeidosEntrada}, callback, asyncState);
        }

        /// <remarks/>
        public void EndCreate(System.IAsyncResult asyncResult, out InfoNumSerieLeidosEntrada InfoNumSerieLeidosEntrada)
        {
            object[] results = this.EndInvoke(asyncResult);
            InfoNumSerieLeidosEntrada = ((InfoNumSerieLeidosEntrada)(results[0]));
        }

        /// <remarks/>
        public void CreateAsync(InfoNumSerieLeidosEntrada InfoNumSerieLeidosEntrada)
        {
            this.CreateAsync(InfoNumSerieLeidosEntrada, null);
        }

        /// <remarks/>
        public void CreateAsync(InfoNumSerieLeidosEntrada InfoNumSerieLeidosEntrada, object userState)
        {
            if ((this.CreateOperationCompleted == null))
            {
                this.CreateOperationCompleted = new System.Threading.SendOrPostCallback(this.OnCreateOperationCompleted);
            }
            this.InvokeAsync("Create", new object[] {
                    InfoNumSerieLeidosEntrada}, this.CreateOperationCompleted, userState);
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
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:microsoft-dynamics-schemas/page/infonumserieleidosentrada:CreateMultiple", RequestNamespace = "urn:microsoft-dynamics-schemas/page/infonumserieleidosentrada", ResponseElementName = "CreateMultiple_Result", ResponseNamespace = "urn:microsoft-dynamics-schemas/page/infonumserieleidosentrada", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void CreateMultiple([System.Xml.Serialization.XmlArrayItemAttribute(IsNullable = false)] ref InfoNumSerieLeidosEntrada[] InfoNumSerieLeidosEntrada_List)
        {
            object[] results = this.Invoke("CreateMultiple", new object[] {
                    InfoNumSerieLeidosEntrada_List});
            InfoNumSerieLeidosEntrada_List = ((InfoNumSerieLeidosEntrada[])(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginCreateMultiple(InfoNumSerieLeidosEntrada[] InfoNumSerieLeidosEntrada_List, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("CreateMultiple", new object[] {
                    InfoNumSerieLeidosEntrada_List}, callback, asyncState);
        }

        /// <remarks/>
        public void EndCreateMultiple(System.IAsyncResult asyncResult, out InfoNumSerieLeidosEntrada[] InfoNumSerieLeidosEntrada_List)
        {
            object[] results = this.EndInvoke(asyncResult);
            InfoNumSerieLeidosEntrada_List = ((InfoNumSerieLeidosEntrada[])(results[0]));
        }

        /// <remarks/>
        public void CreateMultipleAsync(InfoNumSerieLeidosEntrada[] InfoNumSerieLeidosEntrada_List)
        {
            this.CreateMultipleAsync(InfoNumSerieLeidosEntrada_List, null);
        }

        /// <remarks/>
        public void CreateMultipleAsync(InfoNumSerieLeidosEntrada[] InfoNumSerieLeidosEntrada_List, object userState)
        {
            if ((this.CreateMultipleOperationCompleted == null))
            {
                this.CreateMultipleOperationCompleted = new System.Threading.SendOrPostCallback(this.OnCreateMultipleOperationCompleted);
            }
            this.InvokeAsync("CreateMultiple", new object[] {
                    InfoNumSerieLeidosEntrada_List}, this.CreateMultipleOperationCompleted, userState);
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
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:microsoft-dynamics-schemas/page/infonumserieleidosentrada:Update", RequestNamespace = "urn:microsoft-dynamics-schemas/page/infonumserieleidosentrada", ResponseElementName = "Update_Result", ResponseNamespace = "urn:microsoft-dynamics-schemas/page/infonumserieleidosentrada", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void Update(ref InfoNumSerieLeidosEntrada InfoNumSerieLeidosEntrada)
        {
            object[] results = this.Invoke("Update", new object[] {
                    InfoNumSerieLeidosEntrada});
            InfoNumSerieLeidosEntrada = ((InfoNumSerieLeidosEntrada)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginUpdate(InfoNumSerieLeidosEntrada InfoNumSerieLeidosEntrada, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("Update", new object[] {
                    InfoNumSerieLeidosEntrada}, callback, asyncState);
        }

        /// <remarks/>
        public void EndUpdate(System.IAsyncResult asyncResult, out InfoNumSerieLeidosEntrada InfoNumSerieLeidosEntrada)
        {
            object[] results = this.EndInvoke(asyncResult);
            InfoNumSerieLeidosEntrada = ((InfoNumSerieLeidosEntrada)(results[0]));
        }

        /// <remarks/>
        public void UpdateAsync(InfoNumSerieLeidosEntrada InfoNumSerieLeidosEntrada)
        {
            this.UpdateAsync(InfoNumSerieLeidosEntrada, null);
        }

        /// <remarks/>
        public void UpdateAsync(InfoNumSerieLeidosEntrada InfoNumSerieLeidosEntrada, object userState)
        {
            if ((this.UpdateOperationCompleted == null))
            {
                this.UpdateOperationCompleted = new System.Threading.SendOrPostCallback(this.OnUpdateOperationCompleted);
            }
            this.InvokeAsync("Update", new object[] {
                    InfoNumSerieLeidosEntrada}, this.UpdateOperationCompleted, userState);
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
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:microsoft-dynamics-schemas/page/infonumserieleidosentrada:UpdateMultiple", RequestNamespace = "urn:microsoft-dynamics-schemas/page/infonumserieleidosentrada", ResponseElementName = "UpdateMultiple_Result", ResponseNamespace = "urn:microsoft-dynamics-schemas/page/infonumserieleidosentrada", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void UpdateMultiple([System.Xml.Serialization.XmlArrayItemAttribute(IsNullable = false)] ref InfoNumSerieLeidosEntrada[] InfoNumSerieLeidosEntrada_List)
        {
            object[] results = this.Invoke("UpdateMultiple", new object[] {
                    InfoNumSerieLeidosEntrada_List});
            InfoNumSerieLeidosEntrada_List = ((InfoNumSerieLeidosEntrada[])(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginUpdateMultiple(InfoNumSerieLeidosEntrada[] InfoNumSerieLeidosEntrada_List, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("UpdateMultiple", new object[] {
                    InfoNumSerieLeidosEntrada_List}, callback, asyncState);
        }

        /// <remarks/>
        public void EndUpdateMultiple(System.IAsyncResult asyncResult, out InfoNumSerieLeidosEntrada[] InfoNumSerieLeidosEntrada_List)
        {
            object[] results = this.EndInvoke(asyncResult);
            InfoNumSerieLeidosEntrada_List = ((InfoNumSerieLeidosEntrada[])(results[0]));
        }

        /// <remarks/>
        public void UpdateMultipleAsync(InfoNumSerieLeidosEntrada[] InfoNumSerieLeidosEntrada_List)
        {
            this.UpdateMultipleAsync(InfoNumSerieLeidosEntrada_List, null);
        }

        /// <remarks/>
        public void UpdateMultipleAsync(InfoNumSerieLeidosEntrada[] InfoNumSerieLeidosEntrada_List, object userState)
        {
            if ((this.UpdateMultipleOperationCompleted == null))
            {
                this.UpdateMultipleOperationCompleted = new System.Threading.SendOrPostCallback(this.OnUpdateMultipleOperationCompleted);
            }
            this.InvokeAsync("UpdateMultiple", new object[] {
                    InfoNumSerieLeidosEntrada_List}, this.UpdateMultipleOperationCompleted, userState);
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
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:microsoft-dynamics-schemas/page/infonumserieleidosentrada:Delete", RequestNamespace = "urn:microsoft-dynamics-schemas/page/infonumserieleidosentrada", ResponseElementName = "Delete_Result", ResponseNamespace = "urn:microsoft-dynamics-schemas/page/infonumserieleidosentrada", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:microsoft-dynamics-schemas/page/infonumserieleidosentrada")]
    public partial class InfoNumSerieLeidosEntrada_Filter
    {

        private InfoNumSerieLeidosEntrada_Fields fieldField;

        private string criteriaField;

        /// <comentarios/>
        public InfoNumSerieLeidosEntrada_Fields Field
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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:microsoft-dynamics-schemas/page/infonumserieleidosentrada")]
    public enum InfoNumSerieLeidosEntrada_Fields
    {

        /// <comentarios/>
        Cod_Cliente,

        /// <comentarios/>
        Alias_cliente,

        /// <comentarios/>
        Producto,

        /// <comentarios/>
        Num_serie,

        /// <comentarios/>
        Descripcion_num_serie,

        /// <comentarios/>
        Cantidad_enviar,

        /// <comentarios/>
        Cantidad_enviada,

        /// <comentarios/>
        Descripcion_Planta,

        /// <comentarios/>
        Num_pedido,
    }

    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:microsoft-dynamics-schemas/page/infonumserieleidosentrada")]
    public partial class InfoNumSerieLeidosEntrada
    {

        private string keyField;

        private string cod_ClienteField;

        private string alias_clienteField;

        private string productoField;

        private string num_serieField;

        private string descripcion_num_serieField;

        private decimal cantidad_enviarField;

        private bool cantidad_enviarFieldSpecified;

        private decimal cantidad_enviadaField;

        private bool cantidad_enviadaFieldSpecified;

        private string descripcion_PlantaField;

        private string num_pedidoField;

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
        public string Cod_Cliente
        {
            get
            {
                return this.cod_ClienteField;
            }
            set
            {
                this.cod_ClienteField = value;
            }
        }

        /// <comentarios/>
        public string Alias_cliente
        {
            get
            {
                return this.alias_clienteField;
            }
            set
            {
                this.alias_clienteField = value;
            }
        }

        /// <comentarios/>
        public string Producto
        {
            get
            {
                return this.productoField;
            }
            set
            {
                this.productoField = value;
            }
        }

        /// <comentarios/>
        public string Num_serie
        {
            get
            {
                return this.num_serieField;
            }
            set
            {
                this.num_serieField = value;
            }
        }

        /// <comentarios/>
        public string Descripcion_num_serie
        {
            get
            {
                return this.descripcion_num_serieField;
            }
            set
            {
                this.descripcion_num_serieField = value;
            }
        }

        /// <comentarios/>
        public decimal Cantidad_enviar
        {
            get
            {
                return this.cantidad_enviarField;
            }
            set
            {
                this.cantidad_enviarField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool Cantidad_enviarSpecified
        {
            get
            {
                return this.cantidad_enviarFieldSpecified;
            }
            set
            {
                this.cantidad_enviarFieldSpecified = value;
            }
        }

        /// <comentarios/>
        public decimal Cantidad_enviada
        {
            get
            {
                return this.cantidad_enviadaField;
            }
            set
            {
                this.cantidad_enviadaField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool Cantidad_enviadaSpecified
        {
            get
            {
                return this.cantidad_enviadaFieldSpecified;
            }
            set
            {
                this.cantidad_enviadaFieldSpecified = value;
            }
        }

        /// <comentarios/>
        public string Descripcion_Planta
        {
            get
            {
                return this.descripcion_PlantaField;
            }
            set
            {
                this.descripcion_PlantaField = value;
            }
        }

        /// <comentarios/>
        public string Num_pedido
        {
            get
            {
                return this.num_pedidoField;
            }
            set
            {
                this.num_pedidoField = value;
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
        public InfoNumSerieLeidosEntrada[] Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((InfoNumSerieLeidosEntrada[])(this.results[0]));
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
        public InfoNumSerieLeidosEntrada InfoNumSerieLeidosEntrada
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((InfoNumSerieLeidosEntrada)(this.results[0]));
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
        public InfoNumSerieLeidosEntrada[] InfoNumSerieLeidosEntrada_List
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((InfoNumSerieLeidosEntrada[])(this.results[0]));
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
        public InfoNumSerieLeidosEntrada InfoNumSerieLeidosEntrada
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((InfoNumSerieLeidosEntrada)(this.results[0]));
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
        public InfoNumSerieLeidosEntrada[] InfoNumSerieLeidosEntrada_List
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((InfoNumSerieLeidosEntrada[])(this.results[0]));
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