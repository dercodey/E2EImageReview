using System;
using System.Runtime.Serialization;
using System.ComponentModel;
using System.ServiceModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmzImagingInteractionManager.Contracts {

    [DataContract(Name="WorklistItem", Namespace="http://schemas.datacontract.org/2004/07/EmzWorklistInteractionManager")]
    [Serializable]
    public partial class WorklistItem : IExtensibleDataObject
    {
        
        [NonSerialized]
        private ExtensionDataObject extensionDataField;
        
        [Browsable(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }

        [DataMember]
        public DateTime AcquisitionDateTime { get; set; }

        [DataMember]
        public Guid ImageId { get; set; }

        [DataMember]
        public Guid PatientId { get; set; }

        [DataMember]
        public string PatientName { get; set; }
    }
    
    [DataContract(Name="ImageInfo", Namespace="http://schemas.datacontract.org/2004/07/EmzWorklistInteractionManager")]
    [Serializable]
    public partial class ImageInfo : IExtensibleDataObject
    {
        
        [NonSerialized]
        private ExtensionDataObject extensionDataField;
        
        [Browsable(false)]
        public ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }

        [DataMember]
        public DateTime AcquisitionDateTime { get; set; }

        [DataMember]
        public Guid ImageId { get; set; }

        [DataMember]
        public Guid PatientId { get; set; }

        [DataMember]
        public string PatientName { get; set; }        
    }
    
    [DataContract(Name="ImageData", Namespace="http://schemas.datacontract.org/2004/07/EmzWorklistInteractionManager")]
    [Serializable()]
    public partial class ImageData : IExtensibleDataObject
    {
        
        [NonSerialized()]
        private ExtensionDataObject extensionDataField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return extensionDataField;
            }
            set {
                extensionDataField = value;
            }
        }

        [DataMember]
        public int Height { get; set; }

        [DataMember]
        public Guid ImageId { get; set; }

        [DataMember]
        public byte[] Pixels { get; set; }

        [DataMember]
        public int Width { get; set; }
    }
    
    [ServiceContract(ConfigurationName="EmzImagingInteractionManagerService.IImagingInteractionManager")]
    public interface IImagingInteractionManager {
        
        [OperationContract(Action="http://tempuri.org/IImagingInteractionManager/GetWorklistForStaff",
            ReplyAction="http://tempuri.org/IImagingInteractionManager/GetWorklistForStaffResponse")]
        List<WorklistItem> GetWorklistForStaff(Guid staffId);
        
        [OperationContract(Action="http://tempuri.org/IImagingInteractionManager/GetWorklistForStaff", 
            ReplyAction="http://tempuri.org/IImagingInteractionManager/GetWorklistForStaffResponse")]
        Task<List<WorklistItem>> GetWorklistForStaffAsync(Guid staffId);
        
        [OperationContract(Action="http://tempuri.org/IImagingInteractionManager/GetImageInfo", 
            ReplyAction="http://tempuri.org/IImagingInteractionManager/GetImageInfoResponse")]
        ImageInfo GetImageInfo(Guid imageId);
        
        [OperationContract(Action="http://tempuri.org/IImagingInteractionManager/GetImageInfo", 
            ReplyAction="http://tempuri.org/IImagingInteractionManager/GetImageInfoResponse")]
        Task<ImageInfo> GetImageInfoAsync(Guid imageId);
        
        [OperationContract(Action="http://tempuri.org/IImagingInteractionManager/GetImageDataForReview", 
            ReplyAction="http://tempuri.org/IImagingInteractionManager/GetImageDataForReviewResponse")]
        ImageData GetImageDataForReview(Guid imageId);
        
        [OperationContract(Action="http://tempuri.org/IImagingInteractionManager/GetImageDataForReview", 
            ReplyAction="http://tempuri.org/IImagingInteractionManager/GetImageDataForReviewResponse")]
        Task<ImageData> GetImageDataForReviewAsync(Guid imageId);
    }
    
    //public interface IImagingInteractionManagerChannel : 
    //    IImagingInteractionManager, 
    //    IClientChannel
    //{
    //}
    
    public partial class ImagingInteractionManagerClient : 
        ClientBase<IImagingInteractionManager>,
        IImagingInteractionManager
    {
        
        public ImagingInteractionManagerClient() {
        }
        
        public ImagingInteractionManagerClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ImagingInteractionManagerClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ImagingInteractionManagerClient(string endpointConfigurationName, EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ImagingInteractionManagerClient(System.ServiceModel.Channels.Binding binding, EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public List<WorklistItem> GetWorklistForStaff(Guid staffId) {
            return Channel.GetWorklistForStaff(staffId);
        }
        
        public Task<List<WorklistItem>> GetWorklistForStaffAsync(Guid staffId) {
            return Channel.GetWorklistForStaffAsync(staffId);
        }
        
        public ImageInfo GetImageInfo(Guid imageId) {
            return base.Channel.GetImageInfo(imageId);
        }
        
        public Task<ImageInfo> GetImageInfoAsync(Guid imageId) {
            return base.Channel.GetImageInfoAsync(imageId);
        }
        
        public ImageData GetImageDataForReview(Guid imageId) {
            return base.Channel.GetImageDataForReview(imageId);
        }
        
        public Task<ImageData> GetImageDataForReviewAsync(Guid imageId) {
            return base.Channel.GetImageDataForReviewAsync(imageId);
        }
    }
}
