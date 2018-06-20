﻿using System;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Threading.Tasks;

namespace ImageReviewManager
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IImageReviewManager
    {
        [OperationContract]
        Task<ImageInfo> GetImageInfoAsync(Guid imageId);

        [OperationContract]
        Task<ImageReviewResponse> ReviewImageAsync(ImageReviewRequest request);
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
    public class ImageReviewRequest
    {
        [DataMember]
        public Guid ImageId { get; set; }
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

    [DataContract]
    public class ImageReviewResponse
    {
        [DataMember]
        public ImageData ReferenceImage { get; set; }

        [DataMember]
        public ImageData DailyImage { get; set; }
    }
}
