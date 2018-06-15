using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ImageWorkListAggregatorManager.EmzImagingInteractionManagerService;
 
namespace ImageWorkListAggregatorManager
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class WorklistAggregationManagerService : IWorklistAggregationManager
    {
        public async Task<List<WorklistItem>> GetWorklistForStaff(Guid staffId)
        {
            var client = new ImagingInteractionManagerClient();
            var resultList = await client.GetWorklistForStaffAsync(staffId);
            client.Close();

            var worklist =
                resultList.Select(item =>
                new WorklistItem()
                {
                    PatientId = item.PatientId,
                    PatientName = item.PatientName,
                    ImageId = item.ImageId,
                    AcquisitionDateTime = item.AcquisitionDateTime
                });

            // convert staff Id to 
            return worklist.ToList();
        }
    }
}
