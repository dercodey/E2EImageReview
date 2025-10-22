using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreWCF;

using ImagingTypes;
using ImageWorkListAggregatorManager.Contracts;
using EmzImagingInteractionManager.Contracts;

namespace ImageWorkListAggregatorManager
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class WorklistAggregationManagerService : IWorklistAggregationManager
    {
        public async Task<List<WorklistItem>> GetWorklistForStaffAsync(Guid staffId)
        {
            var proxy = ProxyManager.ProxyManager.Instance.CreateProxy<IImagingInteractionManager>();
            var resultList = await proxy.GetWorklistForStaffAsync(staffId);
            ProxyManager.ProxyManager.Instance.CloseProxy(proxy);

            // get the list of items from data access
            List<WorklistItem> dataAccessList = new List<WorklistItem>();

            // combine the legacy list with list acquired from data store
            return resultList.Concat(dataAccessList).ToList();
        }
    }
}
