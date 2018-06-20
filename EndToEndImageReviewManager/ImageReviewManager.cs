using System;
using System.Threading.Tasks;

using ImagingTypes;
using ImageReviewManager.Contracts;

namespace ImageReviewManager
{
    public class ImageReviewManager : IImageReviewManager
    {
        public async Task<ImageInfo> GetImageInfoAsync(Guid imageId)
        {
            var client = new EmzImagingInteractionManager.Contracts.ImagingInteractionManagerClient();
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
            var client = new EmzImagingInteractionManager.Contracts.ImagingInteractionManagerClient();
            var imageInfo = await client.GetImageInfoAsync(request.ImageId);
            ImageData dailyImageData = await client.GetImageDataForReviewAsync(request.ImageId);
            ImageData referenceImageData = null;


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
