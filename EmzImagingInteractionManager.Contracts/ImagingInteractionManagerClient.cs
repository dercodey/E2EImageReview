using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace EmzImagingInteractionManager.Contracts
{
    public class ImagingInteractionManagerClient :
        ClientBase<IImagingInteractionManagerWithSync>,
        IImagingInteractionManagerWithSync
    {
        public ImageData GetImageDataForReview(Guid imageId)
        {
            return base.Channel.GetImageDataForReview(imageId);
        }

        public Task<ImageData> GetImageDataForReviewAsync(Guid imageId)
        {
            return base.Channel.GetImageDataForReviewAsync(imageId);
        }

        public ImageInfo GetImageInfo(Guid imageId)
        {
            return base.Channel.GetImageInfo(imageId);
        }

        public Task<ImageInfo> GetImageInfoAsync(Guid imageId)
        {
            return base.Channel.GetImageInfoAsync(imageId);
        }

        public List<WorklistItem> GetWorklistForStaff(Guid staffId)
        {
            return base.Channel.GetWorklistForStaff(staffId);
        }

        public Task<List<WorklistItem>> GetWorklistForStaffAsync(Guid staffId)
        {
            return base.Channel.GetWorklistForStaffAsync(staffId);
        }
    }
}
