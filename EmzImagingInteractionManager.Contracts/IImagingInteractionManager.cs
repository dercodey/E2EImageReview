using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Threading.Tasks;

namespace EmzImagingInteractionManager.Contracts
{
    [ServiceContract]
    public interface IImagingInteractionManager
    {
        [OperationContract]
        Task<List<WorklistItem>> GetWorklistForStaffAsync(Guid staffId);

        [OperationContract]
        Task<ImageInfo> GetImageInfoAsync(Guid imageId);

        [OperationContract]
        Task<ImageData> GetImageDataForReviewAsync(Guid imageId);
    }

    public interface IImagingInteractionManagerWithSync : IImagingInteractionManager
    {
        List<WorklistItem> GetWorklistForStaff(Guid staffId);

        ImageInfo GetImageInfo(Guid imageId);

        ImageData GetImageDataForReview(Guid imageId);
    }

    [DataContract]
    public class WorklistItem
    {
        [DataMember]
        public Guid PatientId { get; set; }

        [DataMember]
        public string PatientName { get; set; }

        [DataMember]
        public Guid ImageId { get; set; }

        [DataMember]
        public DateTime AcquisitionDateTime { get; set; }
    }

    [DataContract]
    public class ImageInfo
    {
        [DataMember]
        public Guid PatientId { get; set; }

        [DataMember]
        public string PatientName { get; set; }

        [DataMember]
        public Guid ImageId { get; set; }

        [DataMember]
        public DateTime AcquisitionDateTime { get; set; }
    }

    [DataContract]
    public class ImageData
    {
        [DataMember]
        public Guid ImageId { get; set; }

        [DataMember]
        public int Width { get; set; }

        [DataMember]
        public int Height { get; set; }

        [DataMember]
        public byte[] Pixels { get; set; }
    }
}
