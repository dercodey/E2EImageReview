using EmzImagingInteractionManager.Contracts;
using ImagingTypes;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading.Tasks;

namespace ProxyManager
{
    internal class ImagingInteractionManagerProxy : ClientBase<IImagingInteractionManager>, IImagingInteractionManager
    {
        protected static Binding _binding = new BasicHttpBinding();
        protected static EndpointAddress _endpointAddress = new EndpointAddress("http://localhost:8733/Design_Time_Addresses/EmzWorklistInteractionManager/Service1/");

        public ImagingInteractionManagerProxy()
            : base(_binding, _endpointAddress)
        {
        }

        public Task<ImageData> GetImageDataForReviewAsync(Guid imageId) =>
            Channel.GetImageDataForReviewAsync(imageId);

        public Task<ImageInfo> GetImageInfoAsync(Guid imageId) =>
            Channel.GetImageInfoAsync(imageId);

        public Task<List<WorklistItem>> GetWorklistForStaffAsync(Guid staffId) =>
            Channel.GetWorklistForStaffAsync(staffId);
    }
}