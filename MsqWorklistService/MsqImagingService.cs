using System.Collections.Generic;
using System.Linq;

namespace MsqWorklistService
{
    public class MsqImagingService : IMsqImagingService
    {
        public List<MsqWorklistItem> GetWorklistForStaff(int staffId)
        {
            var worklist = 
                from imageRepositoryData in MsqImageRepository.GetAllImageData()
                where imageRepositoryData.ReferenceImageId != null && imageRepositoryData.Reviewed == null
                select new MsqWorklistItem()
                {
                    MsqPatId1 = imageRepositoryData.PatientId,
                    MsqImgId = imageRepositoryData.ImageId,
                    PatientName = GetPatientName(imageRepositoryData.PatientId),
                    AcquisitionDateTime = imageRepositoryData.AcquisitionDateTime
                };
            return worklist.ToList();
        }


        public MsqImageInfo GetImageInfo(int imgId)
        {
            var imageRepositoryData =
                MsqImageRepository
                    .GetAllImageData()
                    .FirstOrDefault(image => image.ImageId == imgId);

            return new MsqImageInfo()
            {
                MsqImgId = imageRepositoryData.ImageId,
                MsqPatId1 = imageRepositoryData.PatientId,
                PatientName = GetPatientName(imageRepositoryData.PatientId),
                AcquisitionDateTime = imageRepositoryData.AcquisitionDateTime,
                MsqReferenceImgId = imageRepositoryData.ReferenceImageId,
                MsqRegistrationId = null,
            };
        }

        private static string GetPatientName(int msqPatId)
        {
            return MsqImageRepository
                    .GetAllPatientData()
                    .FirstOrDefault(patient => patient.PatientId == msqPatId).PatientName;
        }

        public MsqImageData LoadImageData(int imgId)
        {
            var imageRepositoryData =
                MsqImageRepository
                    .GetAllImageData()
                    .FirstOrDefault(image => image.ImageId == imgId);

            var msqImageData =
                new MsqImageData()
                {
                    MsqImgId = imageRepositoryData.ImageId,
                    Width = imageRepositoryData.Pixels.GetLength(0),
                    Height = imageRepositoryData.Pixels.GetLength(1),
                };

            msqImageData.Pixels = new byte[ msqImageData.Width * msqImageData.Height ];

            for (int x = 0; x < msqImageData.Width; x++)
            {
                for (int y = 0; y < msqImageData.Height; y++)
                {
                    msqImageData.Pixels[y * msqImageData.Width + x] = imageRepositoryData.Pixels[x, y];
                }
            }

            return msqImageData;
        }
    }
}
