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
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="WorklistAggregatorManagerService.IWorklistAggregationManager")]
    public interface IWorklistAggregationManager {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWorklistAggregationManager/GetWorklistForStaff", ReplyAction="http://tempuri.org/IWorklistAggregationManager/GetWorklistForStaffResponse")]
        System.Collections.Generic.List<ImagingTypes.WorklistItem> GetWorklistForStaff(System.Guid staffId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWorklistAggregationManager/GetWorklistForStaff", ReplyAction="http://tempuri.org/IWorklistAggregationManager/GetWorklistForStaffResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.List<ImagingTypes.WorklistItem>> GetWorklistForStaffAsync(System.Guid staffId);
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
        
        public System.Collections.Generic.List<ImagingTypes.WorklistItem> GetWorklistForStaff(System.Guid staffId) {
            return base.Channel.GetWorklistForStaff(staffId);
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.List<ImagingTypes.WorklistItem>> GetWorklistForStaffAsync(System.Guid staffId) {
            return base.Channel.GetWorklistForStaffAsync(staffId);
        }
    }
}
