﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ServiceModel;

using ImagingTypes;

using ImageWorkListAggregatorManager.Contracts;
using EmzImagingInteractionManager.Contracts;



namespace ImageWorkListAggregatorManager
{
    public class WorklistAggregationManagerService : IWorklistAggregationManager
    {
        public async Task<List<WorklistItem>> GetWorklistForStaffAsync(Guid staffId)
        {
#if USE_PROXY_GENERATOR
            var proxy = ProxyManager.ProxyManager.Instance.CreateProxy<IImagingInteractionManager>();
#else
            var proxy = new ImagingInteractionManagerClient();
#endif
            var resultList = await proxy.GetWorklistForStaffAsync(staffId);
#if USE_PROXY_GENERATOR
            ProxyManager.ProxyManager.Instance.CloseProxy(proxy);
#else
            proxy.Close();
#endif

            // get the list of items from data access
            List<WorklistItem> dataAccessList = new List<WorklistItem>();

            // combine the legacy list with list acquired from data store
            return resultList.Concat(dataAccessList).ToList();
        }
    }
}
