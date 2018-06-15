using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace EmzWorklistInteractionManager
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class WorklistInteractionManagerService : IWorklistInteractionManager
    {
        public async Task<List<WorklistItem>> GetWorklistForStaff(Guid staffId)
        {
            var msqStaffId = MsqMappingHelper.MapIdToMsq(EntityType.Staff, staffId);

            var client = new MsqWorklistService.MsqWorklistServiceClient();
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

        public async Task<ImageInfo> GetImageInfo(Guid imageId)
        {
            var msqImgId = MsqMappingHelper.MapIdToMsq(EntityType.Image, imageId);

            var client = new MsqWorklistService.MsqWorklistServiceClient();
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

        public async Task<ImageData> GetImageDataForReview(Guid imageId)
        {
            var msqImgId = MsqMappingHelper.MapIdToMsq(EntityType.Image, imageId);

            var client = new MsqWorklistService.MsqWorklistServiceClient();
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
