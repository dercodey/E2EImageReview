using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

using ImagingTypes;

namespace EmzImagingInteractionManager.Contracts
{
    [ServiceContract]
    public interface IImagingInteractionManager
    {
        [OperationContract]
        Task<List<WorklistItem>> GetWorklistForStaffAsync(Guid staffId);

        [OperationContract]
        Task<ImageInfo> GetImageInfoAsync(Guid imageId);

        [OperationContract]
        Task<ImageData> GetImageDataForReviewAsync(Guid imageId);
    }
}
