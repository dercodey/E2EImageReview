﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EndToEndImageReviewApp.WorklistAggregatorManagerService {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="WorklistItem", Namespace="http://schemas.datacontract.org/2004/07/ImageWorkListAggregatorManager")]
    [System.SerializableAttribute()]
    public partial class WorklistItem : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime AcquisitionDateTimeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Guid ImageIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Guid PatientIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PatientNameField;
        
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
        public System.Guid ImageId {
            get {
                return this.ImageIdField;
            }
            set {
                if ((this.ImageIdField.Equals(value) != true)) {
                    this.ImageIdField = value;
                    this.RaisePropertyChanged("ImageId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Guid PatientId {
            get {
                return this.PatientIdField;
            }
            set {
                if ((this.PatientIdField.Equals(value) != true)) {
                    this.PatientIdField = value;
                    this.RaisePropertyChanged("PatientId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PatientName {
            get {
                return this.PatientNameField;
            }
            set {
                if ((object.ReferenceEquals(this.PatientNameField, value) != true)) {
                    this.PatientNameField = value;
                    this.RaisePropertyChanged("PatientName");
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
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="WorklistAggregatorManagerService.IWorklistAggregationManager")]
    public interface IWorklistAggregationManager {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWorklistAggregationManager/GetWorklistForStaff", ReplyAction="http://tempuri.org/IWorklistAggregationManager/GetWorklistForStaffResponse")]
        System.Collections.Generic.List<EndToEndImageReviewApp.WorklistAggregatorManagerService.WorklistItem> GetWorklistForStaff(System.Guid staffId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWorklistAggregationManager/GetWorklistForStaff", ReplyAction="http://tempuri.org/IWorklistAggregationManager/GetWorklistForStaffResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.List<EndToEndImageReviewApp.WorklistAggregatorManagerService.WorklistItem>> GetWorklistForStaffAsync(System.Guid staffId);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IWorklistAggregationManagerChannel : EndToEndImageReviewApp.WorklistAggregatorManagerService.IWorklistAggregationManager, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class WorklistAggregationManagerClient : System.ServiceModel.ClientBase<EndToEndImageReviewApp.WorklistAggregatorManagerService.IWorklistAggregationManager>, EndToEndImageReviewApp.WorklistAggregatorManagerService.IWorklistAggregationManager {
        
        public WorklistAggregationManagerClient() {
        }
        
        public WorklistAggregationManagerClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public WorklistAggregationManagerClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public WorklistAggregationManagerClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public WorklistAggregationManagerClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public System.Collections.Generic.List<EndToEndImageReviewApp.WorklistAggregatorManagerService.WorklistItem> GetWorklistForStaff(System.Guid staffId) {
            return base.Channel.GetWorklistForStaff(staffId);
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.List<EndToEndImageReviewApp.WorklistAggregatorManagerService.WorklistItem>> GetWorklistForStaffAsync(System.Guid staffId) {
            return base.Channel.GetWorklistForStaffAsync(staffId);
        }
    }
}
