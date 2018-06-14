using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
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
                        AcquisitionDateTime = item.AcquisitionDateTime
                    }).ToList();
        }
    }
}
