using System;
using System.ServiceModel;
using System.Threading.Tasks;

using ImagingTypes;

namespace ImageReviewManager.Contracts
{
    public class ImageReviewManagerClient : 
        ClientBase<IImageReviewManager>, 
        IImageReviewManager
    {
        public Task<ImageReviewResponse> ReviewImageAsync(ImageReviewRequest request) => 
            Channel.ReviewImageAsync(request);

        public Task<ImageInfo> GetImageInfoAsync(Guid imageId) => 
            Channel.GetImageInfoAsync(imageId);
    }
}
