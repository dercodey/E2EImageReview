using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndToEndImageReviewApp
{
    public class WorklistModel
    {
        public async Task<List<WorklistItem>> PopulateWorklistForStaff(Guid staffId)
        {
            var client = new WorklistAggregatorManagerService.WorklistAggregationManagerClient();
            var listResult = await client.GetWorklistForStaffAsync(staffId);
            client.Close();

            return listResult.Select(item =>
                    new WorklistItem()
                    {
                        PatientId = item.PatientId,
                        PatientName = item.PatientName,
                        ImageId = item.ImageId,
                        AcquisitionDateTime = item.AcquisitionDateTime
                    }).ToList();
        }
    }
}
