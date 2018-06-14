﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EmzWorklistInteractionManager.MsqWorklistService {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="MsqWorklistItem", Namespace="http://schemas.datacontract.org/2004/07/MsqWorklistService")]
    [System.SerializableAttribute()]
    public partial class MsqWorklistItem : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime AcquisitionDateTimeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int MsqImgIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int MsqPatId1Field;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime AcquisitionDateTime {
            get {
                return this.AcquisitionDateTimeField;
            }
            set {
                if ((this.AcquisitionDateTimeField.Equals(value) != true)) {
                    this.AcquisitionDateTimeField = value;
                    this.RaisePropertyChanged("AcquisitionDateTime");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int MsqImgId {
            get {
                return this.MsqImgIdField;
            }
            set {
                if ((this.MsqImgIdField.Equals(value) != true)) {
                    this.MsqImgIdField = value;
                    this.RaisePropertyChanged("MsqImgId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int MsqPatId1 {
            get {
                return this.MsqPatId1Field;
            }
            set {
                if ((this.MsqPatId1Field.Equals(value) != true)) {
                    this.MsqPatId1Field = value;
                    this.RaisePropertyChanged("MsqPatId1");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="MsqWorklistService.IMsqWorklistService")]
    public interface IMsqWorklistService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMsqWorklistService/GetWorklistForStaff", ReplyAction="http://tempuri.org/IMsqWorklistService/GetWorklistForStaffResponse")]
        System.Collections.Generic.List<EmzWorklistInteractionManager.MsqWorklistService.MsqWorklistItem> GetWorklistForStaff(System.Guid staffId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMsqWorklistService/GetWorklistForStaff", ReplyAction="http://tempuri.org/IMsqWorklistService/GetWorklistForStaffResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.List<EmzWorklistInteractionManager.MsqWorklistService.MsqWorklistItem>> GetWorklistForStaffAsync(System.Guid staffId);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IMsqWorklistServiceChannel : EmzWorklistInteractionManager.MsqWorklistService.IMsqWorklistService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class MsqWorklistServiceClient : System.ServiceModel.ClientBase<EmzWorklistInteractionManager.MsqWorklistService.IMsqWorklistService>, EmzWorklistInteractionManager.MsqWorklistService.IMsqWorklistService {
        
        public MsqWorklistServiceClient() {
        }
        
        public MsqWorklistServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public MsqWorklistServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public MsqWorklistServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public MsqWorklistServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public System.Collections.Generic.List<EmzWorklistInteractionManager.MsqWorklistService.MsqWorklistItem> GetWorklistForStaff(System.Guid staffId) {
            return base.Channel.GetWorklistForStaff(staffId);
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.List<EmzWorklistInteractionManager.MsqWorklistService.MsqWorklistItem>> GetWorklistForStaffAsync(System.Guid staffId) {
            return base.Channel.GetWorklistForStaffAsync(staffId);
        }
    }
}
