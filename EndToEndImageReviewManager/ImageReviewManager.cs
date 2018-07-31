using System;
using System.Threading.Tasks;

using ProxyManager;

using ImagingTypes;
using ImageReviewManager.Contracts;
using EmzImagingInteractionManager.Contracts;


namespace ImageReviewManager
{
    public class ImageReviewManager : IImageReviewManager
    {
        public async Task<ImageInfo> GetImageInfoAsync(Guid imageId)
        {
            var client = ProxyManager.ProxyManager.Instance.CreateProxy<IImagingInteractionManager>();
            var imageInfo = await client.GetImageInfoAsync(imageId);
            ProxyManager.ProxyManager.Instance.CloseProxy(client);

            return imageInfo;
        }

        public async Task<ImageReviewResponse> ReviewImageAsync(ImageReviewRequest request)
        {
            var client = ProxyManager.ProxyManager.Instance.CreateProxy<IImagingInteractionManager>();
            var imageInfo = await client.GetImageInfoAsync(request.ImageId);
            ImageData dailyImageData = await client.GetImageDataForReviewAsync(request.ImageId);
            ImageData referenceImageData = null;
            ProxyManager.ProxyManager.Instance.CloseProxy(client);

            return new ImageReviewResponse()
            {
                DailyImage = dailyImageData, 
                ReferenceImage = referenceImageData,
            };
        }
    }
}
