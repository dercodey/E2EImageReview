using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreWCF;

using ImagingTypes;
using EmzImagingInteractionManager.Contracts;
using EmzImagingInteractionManager.MsqWorklistService;

namespace EmzImagingInteractionManager
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
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
                    PatientMedRc = item.PatientMedRc,
                    ImageId = MsqMappingHelper.MapIdFromMsq(EntityType.Image, item.MsqImgId),
                    AcquisitionDateTime = item.AcquisitionDateTime
                });

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
                PatientMedRc = msqImageInfo.PatientMedRc,
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
