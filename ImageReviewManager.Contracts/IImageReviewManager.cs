using System;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Threading.Tasks;

using ImagingTypes;

namespace ImageReviewManager.Contracts
{
    [ServiceContract]
    public interface IImageReviewManager
    {
        [OperationContract]
        Task<ImageInfo> GetImageInfoAsync(Guid imageId);

        [OperationContract]
        Task<ImageReviewResponse> ReviewImageAsync(ImageReviewRequest request);
    }

    [DataContract]
    public class ImageReviewRequest
    {
        [DataMember]
        public Guid ImageId { get; set; }
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
