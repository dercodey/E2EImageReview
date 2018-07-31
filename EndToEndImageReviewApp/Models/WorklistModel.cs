using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ProxyManager;

using ImageWorkListAggregatorManager.Contracts;

namespace EndToEndImageReviewApp
{
    public class WorklistModel
    {
        public async Task<List<WorklistItem>> PopulateWorklistForStaff(Guid staffId)
        {
            var client = ProxyManager.ProxyManager.Instance.CreateProxy<IWorklistAggregationManager>(); 
            var listResult = await client.GetWorklistForStaffAsync(staffId);
            ProxyManager.ProxyManager.Instance.CloseProxy(client);

            return listResult.Select(item =>
                    new WorklistItem()
                    {
                        PatientId = item.PatientId,
                        PatientName = item.PatientName,
                        MedRc = item.PatientMedRc,
                        ImageId = item.ImageId,
                        AcquisitionDateTime = item.AcquisitionDateTime
                    }).ToList();
        }
    }
}
