using ImageReviewManager.Contracts;
using ImagingTypes;
using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading.Tasks;

namespace ProxyManager
{
    internal class ImageReviewManagerProxy : ClientBase<IImageReviewManager>, IImageReviewManager
    {
        protected static Binding _binding = new BasicHttpBinding();
        protected static EndpointAddress _endpointAddress = new EndpointAddress("http://localhost:8733/Design_Time_Addresses/EndToEndImageReviewManager/Service1/");

        public ImageReviewManagerProxy()
            : base(_binding, _endpointAddress)
        {
        }

        public Task<ImageInfo> GetImageInfoAsync(Guid imageId) => 
            Channel.GetImageInfoAsync(imageId);

        public Task<ImageReviewResponse> ReviewImageAsync(ImageReviewRequest request) => 
            Channel.ReviewImageAsync(request);
    }
}