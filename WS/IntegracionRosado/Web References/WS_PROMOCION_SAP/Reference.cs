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

namespace IntegracionRosado.WS_PROMOCION_SAP {
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
    [System.Web.Services.WebServiceBindingAttribute(Name="SI_PromocionxArticulo_OUT_SYNCBinding", Namespace="urn:elrosado.com:jda:asr:PromocionxArticulo")]
    public partial class SI_PromocionxArticulo_OUT_SYNCService : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback SI_PromocionxArticulo_OUT_SYNCOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public SI_PromocionxArticulo_OUT_SYNCService() {
            this.Url = global::IntegracionRosado.Properties.Settings.Default.IntegracionRosado_WS_PROMOCION_SAP_SI_PromocionxArticulo_OUT_SYNCService;
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
        public event SI_PromocionxArticulo_OUT_SYNCCompletedEventHandler SI_PromocionxArticulo_OUT_SYNCCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://sap.com/xi/WebService/soap1.1", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Bare)]
        [return: System.Xml.Serialization.XmlElementAttribute("MT_PromocionxArticulo_Response", Namespace="urn:elrosado.com:jda:asr:PromocionxArticulo")]
        public DT_PromocionxArticulo_Response SI_PromocionxArticulo_OUT_SYNC([System.Xml.Serialization.XmlElementAttribute(Namespace="urn:elrosado.com:jda:asr:PromocionxArticulo")] DT_PromocionxArticulo_Request MT_PromocionxArticulo_Request) {
            object[] results = this.Invoke("SI_PromocionxArticulo_OUT_SYNC", new object[] {
                        MT_PromocionxArticulo_Request});
            return ((DT_PromocionxArticulo_Response)(results[0]));
        }
        
        /// <remarks/>
        public void SI_PromocionxArticulo_OUT_SYNCAsync(DT_PromocionxArticulo_Request MT_PromocionxArticulo_Request) {
            this.SI_PromocionxArticulo_OUT_SYNCAsync(MT_PromocionxArticulo_Request, null);
        }
        
        /// <remarks/>
        public void SI_PromocionxArticulo_OUT_SYNCAsync(DT_PromocionxArticulo_Request MT_PromocionxArticulo_Request, object userState) {
            if ((this.SI_PromocionxArticulo_OUT_SYNCOperationCompleted == null)) {
                this.SI_PromocionxArticulo_OUT_SYNCOperationCompleted = new System.Threading.SendOrPostCallback(this.OnSI_PromocionxArticulo_OUT_SYNCOperationCompleted);
            }
            this.InvokeAsync("SI_PromocionxArticulo_OUT_SYNC", new object[] {
                        MT_PromocionxArticulo_Request}, this.SI_PromocionxArticulo_OUT_SYNCOperationCompleted, userState);
        }
        
        private void OnSI_PromocionxArticulo_OUT_SYNCOperationCompleted(object arg) {
            if ((this.SI_PromocionxArticulo_OUT_SYNCCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.SI_PromocionxArticulo_OUT_SYNCCompleted(this, new SI_PromocionxArticulo_OUT_SYNCCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:elrosado.com:jda:asr:PromocionxArticulo")]
    public partial class DT_PromocionxArticulo_Request {
        
        private DT_PromocionxArticulo_RequestIT_PXITEM iT_PXITEMField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public DT_PromocionxArticulo_RequestIT_PXITEM IT_PXITEM {
            get {
                return this.iT_PXITEMField;
            }
            set {
                this.iT_PXITEMField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="urn:elrosado.com:jda:asr:PromocionxArticulo")]
    public partial class DT_PromocionxArticulo_RequestIT_PXITEM {
        
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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:elrosado.com:jda:asr:PromocionxArticulo")]
    public partial class DT_PromocionxArticulo_Response {
        
        private DT_PromocionxArticulo_ResponseItem[] t_OUTPUTField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute("item", Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=false)]
        public DT_PromocionxArticulo_ResponseItem[] T_OUTPUT {
            get {
                return this.t_OUTPUTField;
            }
            set {
                this.t_OUTPUTField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="urn:elrosado.com:jda:asr:PromocionxArticulo")]
    public partial class DT_PromocionxArticulo_ResponseItem {
        
        private string pXITEMField;
        
        private string pXBDATField;
        
        private string pXEDATField;
        
        private string wAKTIONField;
        
        private string aKTKTField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string PXITEM {
            get {
                return this.pXITEMField;
            }
            set {
                this.pXITEMField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string PXBDAT {
            get {
                return this.pXBDATField;
            }
            set {
                this.pXBDATField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string PXEDAT {
            get {
                return this.pXEDATField;
            }
            set {
                this.pXEDATField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string WAKTION {
            get {
                return this.wAKTIONField;
            }
            set {
                this.wAKTIONField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string AKTKT {
            get {
                return this.aKTKTField;
            }
            set {
                this.aKTKTField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    public delegate void SI_PromocionxArticulo_OUT_SYNCCompletedEventHandler(object sender, SI_PromocionxArticulo_OUT_SYNCCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class SI_PromocionxArticulo_OUT_SYNCCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal SI_PromocionxArticulo_OUT_SYNCCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public DT_PromocionxArticulo_Response Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((DT_PromocionxArticulo_Response)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591