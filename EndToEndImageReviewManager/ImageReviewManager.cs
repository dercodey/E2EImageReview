using System;
using System.Threading.Tasks;

using EmzImagingInteractionManager.Contracts;

namespace ImageReviewManager
{
    public class ImageReviewManager : IImageReviewManager
    {
        public async Task<ImageInfo> GetImageInfoAsync(Guid imageId)
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

        public async Task<ImageReviewResponse> ReviewImageAsync(ImageReviewRequest request)
        {
            var client = new ImagingInteractionManagerClient();
            var imageInfo = await client.GetImageInfoAsync(request.ImageId);
            EmzImagingInteractionManager.Contracts.ImageData dailyImageData = await client.GetImageDataForReviewAsync(request.ImageId);
            EmzImagingInteractionManager.Contracts.ImageData referenceImageData = null;


            return new ImageReviewResponse()
            {
                DailyImage = new ImageData()
                {
                    ImageId = dailyImageData.ImageId,
                    Height = dailyImageData.Height,
                    Width = dailyImageData.Width,
                    Pixels = dailyImageData.Pixels,
                },
                ReferenceImage = new ImageData() { },
            };
        }
    }
}
