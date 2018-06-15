using System;
using System.Threading.Tasks;

using EndToEndImageReviewManager.EmzImagingInteractionManagerService;

namespace EndToEndImageReviewManager
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class ImageReviewManager : IImageReviewManager
    {
        public async Task<ImageInfo> GetImageInfo(Guid imageId)
        {
            var client = new ImagingInteractionManagerClient();
            var imageInfo = await client.GetImageInfoAsync(imageId);
            return new ImageInfo()
            {
                PatientId = imageInfo.PatientId,
                PatientName = imageInfo.PatientName,
                ImageId = imageInfo.ImageId,
                AcquisitionDateTime = imageInfo.AcquisitionDateTime,
            };
        }

        public async Task<ImageReviewResponse> ReviewImage(ImageReviewRequest request)
        {
            var client = new ImagingInteractionManagerClient();
            var imageInfo = await client.GetImageInfoAsync(request.ImageId);
            EmzImagingInteractionManagerService.ImageData dailyImageData = await client.GetImageDataForReviewAsync(request.ImageId);
            EmzImagingInteractionManagerService.ImageData referenceImageData = null;
            

            return new ImageReviewResponse()
            {
                DailyImage = new ImageData()
                {
                    ImageId = dailyImageData.ImageId,
                    Height = dailyImageData.Height,
                    Width = dailyImageData.Width,
                    Pixels = dailyImageData.Pixels,                    
                },
            };
        }
    }
}
