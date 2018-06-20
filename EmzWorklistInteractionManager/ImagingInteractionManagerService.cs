using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmzImagingInteractionManager.Contracts;
using EmzImagingInteractionManager.MsqWorklistService;

namespace EmzImagingInteractionManager
{
    public class ImagingInteractionManagerService : IImagingInteractionManager
    {
        public async Task<List<WorklistItem>> GetWorklistForStaffAsync(Guid staffId)
        {
            var msqStaffId = MsqMappingHelper.MapIdToMsq(EntityType.Staff, staffId);

            var client = new MsqImagingServiceClient();
            var msqWorklist = await client.GetWorklistForStaffAsync(msqStaffId);
            client.Close();

            var worklist =
                msqWorklist.Select(item =>
                new WorklistItem()
                {
                    PatientId = MsqMappingHelper.MapIdFromMsq(EntityType.Patient, item.MsqPatId1),
                    PatientName = item.PatientName,
                    ImageId = MsqMappingHelper.MapIdFromMsq(EntityType.Image, item.MsqImgId),
                    AcquisitionDateTime = item.AcquisitionDateTime
                });

            // convert staff Id to 
            return worklist.ToList();
        }

        public async Task<ImageInfo> GetImageInfoAsync(Guid imageId)
        {
            var msqImgId = MsqMappingHelper.MapIdToMsq(EntityType.Image, imageId);

            var client = new MsqImagingServiceClient();
            var msqImageInfo = await client.GetImageInfoAsync(msqImgId);
            client.Close();

            return new ImageInfo()
            {
                ImageId = MsqMappingHelper.MapIdFromMsq(EntityType.Image, msqImageInfo.MsqImgId),
                PatientId = MsqMappingHelper.MapIdFromMsq(EntityType.Patient, msqImageInfo.MsqImgId),
                PatientName = msqImageInfo.PatientName,
                AcquisitionDateTime = msqImageInfo.AcquisitionDateTime,
            };
        }

        public async Task<ImageData> GetImageDataForReviewAsync(Guid imageId)
        {
            var msqImgId = MsqMappingHelper.MapIdToMsq(EntityType.Image, imageId);

            var client = new MsqImagingServiceClient();
            var msqImageData = await client.LoadImageDataAsync(msqImgId);
            return new ImageData()
            {
                ImageId = imageId,
                Height = msqImageData.Height,
                Width = msqImageData.Width,
                Pixels = msqImageData.Pixels,
            };
        }
    }
}
