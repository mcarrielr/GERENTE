﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// Microsoft.VSDesigner generó automáticamente este código fuente, versión=4.0.30319.42000.
// 
#pragma warning disable 1591

namespace IntegracionRosado.WS_CENTRO {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="SI_Centro_OUT_SYNCBinding", Namespace="urn:elrosado.com:nuo:kioskoTienda:centro")]
    public partial class SI_Centro_OUT_SYNCService : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback SI_Centro_OUT_SYNCOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public SI_Centro_OUT_SYNCService() {
            this.Url = global::IntegracionRosado.Properties.Settings.Default.IntegracionRosado_WS_CENTRO_SI_Centro_OUT_SYNCService;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event SI_Centro_OUT_SYNCCompletedEventHandler SI_Centro_OUT_SYNCCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://sap.com/xi/WebService/soap1.1", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Bare)]
        [return: System.Xml.Serialization.XmlElementAttribute("MT_Centro_Response", Namespace="urn:elrosado.com:nuo:kioskoTienda:centro")]
        public DT_Centro_Response SI_Centro_OUT_SYNC([System.Xml.Serialization.XmlElementAttribute(Namespace="urn:elrosado.com:nuo:kioskoTienda:centro")] DT_Centro_Request MT_Centro_Request) {
            object[] results = this.Invoke("SI_Centro_OUT_SYNC", new object[] {
                        MT_Centro_Request});
            return ((DT_Centro_Response)(results[0]));
        }
        
        /// <remarks/>
        public void SI_Centro_OUT_SYNCAsync(DT_Centro_Request MT_Centro_Request) {
            this.SI_Centro_OUT_SYNCAsync(MT_Centro_Request, null);
        }
        
        /// <remarks/>
        public void SI_Centro_OUT_SYNCAsync(DT_Centro_Request MT_Centro_Request, object userState) {
            if ((this.SI_Centro_OUT_SYNCOperationCompleted == null)) {
                this.SI_Centro_OUT_SYNCOperationCompleted = new System.Threading.SendOrPostCallback(this.OnSI_Centro_OUT_SYNCOperationCompleted);
            }
            this.InvokeAsync("SI_Centro_OUT_SYNC", new object[] {
                        MT_Centro_Request}, this.SI_Centro_OUT_SYNCOperationCompleted, userState);
        }
        
        private void OnSI_Centro_OUT_SYNCOperationCompleted(object arg) {
            if ((this.SI_Centro_OUT_SYNCCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.SI_Centro_OUT_SYNCCompleted(this, new SI_Centro_OUT_SYNCCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:elrosado.com:nuo:kioskoTienda:centro")]
    public partial class DT_Centro_Request {
        
        private string itemField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string item {
            get {
                return this.itemField;
            }
            set {
                this.itemField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:elrosado.com:nuo:kioskoTienda:centro")]
    public partial class DT_Centro_Response {
        
        private string cODERRORField;
        
        private string dESERRORField;
        
        private DT_Centro_ResponseItem[] itemField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string CODERROR {
            get {
                return this.cODERRORField;
            }
            set {
                this.cODERRORField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string DESERROR {
            get {
                return this.dESERRORField;
            }
            set {
                this.dESERRORField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("item", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public DT_Centro_ResponseItem[] item {
            get {
                return this.itemField;
            }
            set {
                this.itemField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="urn:elrosado.com:nuo:kioskoTienda:centro")]
    public partial class DT_Centro_ResponseItem {
        
        private string wERKSField;
        
        private string nAMEField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string WERKS {
            get {
                return this.wERKSField;
            }
            set {
                this.wERKSField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string NAME {
            get {
                return this.nAMEField;
            }
            set {
                this.nAMEField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    public delegate void SI_Centro_OUT_SYNCCompletedEventHandler(object sender, SI_Centro_OUT_SYNCCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class SI_Centro_OUT_SYNCCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal SI_Centro_OUT_SYNCCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public DT_Centro_Response Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((DT_Centro_Response)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591