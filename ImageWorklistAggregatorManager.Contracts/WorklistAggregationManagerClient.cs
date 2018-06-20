using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

using ImagingTypes;

namespace ImageWorkListAggregatorManager.Contracts
{
    public class WorklistAggregationManagerClient :
        ClientBase<IWorklistAggregationManager>,
        IWorklistAggregationManager
    {
        public Task<List<WorklistItem>> GetWorklistForStaffAsync(Guid staffId) =>
            Channel.GetWorklistForStaffAsync(staffId);
    }
}
