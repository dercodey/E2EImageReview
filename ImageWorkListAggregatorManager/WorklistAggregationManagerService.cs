using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ImagingTypes;
using ImageWorkListAggregatorManager.Contracts;
using EmzImagingInteractionManager.Contracts;

namespace ImageWorkListAggregatorManager
{
    public class WorklistAggregationManagerService : IWorklistAggregationManager
    {
        public async Task<List<WorklistItem>> GetWorklistForStaffAsync(Guid staffId)
        {
            var client = new ImagingInteractionManagerClient();
            var resultList = await client.GetWorklistForStaffAsync(staffId);
            client.Close();

            // get the list of items from data access
            List<WorklistItem> dataAccessList = new List<WorklistItem>();

            // combine the legacy list with list acquired from data store
            return resultList.Concat(dataAccessList).ToList();
        }
    }
}
