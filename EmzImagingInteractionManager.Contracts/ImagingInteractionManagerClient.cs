using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

using ImagingTypes;

namespace EmzImagingInteractionManager.Contracts
{
    public class ImagingInteractionManagerClient :
        ClientBase<IImagingInteractionManager>,
        IImagingInteractionManager
    {
        public Task<ImageData> GetImageDataForReviewAsync(Guid imageId) =>
            Channel.GetImageDataForReviewAsync(imageId);

        public Task<ImageInfo> GetImageInfoAsync(Guid imageId) => 
            Channel.GetImageInfoAsync(imageId);

        public Task<List<WorklistItem>> GetWorklistForStaffAsync(Guid staffId) =>
            Channel.GetWorklistForStaffAsync(staffId);
    }
}
