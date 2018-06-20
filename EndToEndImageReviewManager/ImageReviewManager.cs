using System;
using System.Threading.Tasks;

using ImagingTypes;
using ImageReviewManager.Contracts;
using EmzImagingInteractionManager.Contracts;

namespace ImageReviewManager
{
    public class ImageReviewManager : IImageReviewManager
    {
        public async Task<ImageInfo> GetImageInfoAsync(Guid imageId)
        {
            var client = new ImagingInteractionManagerClient();
            var imageInfo = await client.GetImageInfoAsync(imageId);
            client.Close();

            return imageInfo;
        }

        public async Task<ImageReviewResponse> ReviewImageAsync(ImageReviewRequest request)
        {
            var client = new ImagingInteractionManagerClient();
            var imageInfo = await client.GetImageInfoAsync(request.ImageId);
            ImageData dailyImageData = await client.GetImageDataForReviewAsync(request.ImageId);
            ImageData referenceImageData = null;
            client.Close();

            return new ImageReviewResponse()
            {
                DailyImage = dailyImageData, 
                ReferenceImage = referenceImageData,
            };
        }
    }
}
