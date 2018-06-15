using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace EndToEndImageReviewManager
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class ImageReviewManager : IImageReviewManager
    {
        public async Task<ImageInfo> GetImageInfo(Guid imageId)
        {
            var client = new EmzWorklistInteractionManagerService.WorklistInteractionManagerClient();
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
            throw new NotImplementedException();
        }
    }
}
